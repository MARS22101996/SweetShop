using System;
using System.Linq;
using AutoMapper;
using SweetShop.BLL.Dto;
using SweetShop.BLL.Infrastructure.Exceptions;
using SweetShop.BLL.Interfaces;
using SweetShop.CORE.Enums;
using SweetShop.DAL.Entities;
using SweetShop.DAL.Interfaces;

namespace SweetShop.BLL.Services
{
   public class BasketService : IBasketService
   {
      private readonly IUnitOfWork _unitOfWork;
      private readonly IMapper _mapper;

      public BasketService(IUnitOfWork unitOfWork, IMapper mapper)
      {
         _unitOfWork = unitOfWork;
         _mapper = mapper;
      }

      public void BuyProduct(OrderDetailsDto orderDetailsDto, string userId)
      {
         if (orderDetailsDto == null)
         {
            throw new ArgumentNullException(nameof(orderDetailsDto));
         }

         var customer = GetCustomerById(userId);

         CheckBasketForUserAndManageDetails(orderDetailsDto, customer.Id);
      }

      public OrderDetails GetOrderDetailsForProduct(int productId, int customerId)
      {
         var order = GetBasketByUserWithDetails(customerId);

         var detailsForProduct = order.OrderDetailses.FirstOrDefault(x => x.ProductId == productId);

         return detailsForProduct;
      }

      private void CheckBasketForUserAndManageDetails(OrderDetailsDto orderDetailsDto, int customerId)
      {
         if (!IsBasketExistsForUser(customerId))
         {
            CreateBasket(customerId);

            ManageDetailsInBasket(orderDetailsDto, customerId);
         }
         else
         {
            ManageDetailsInBasket(orderDetailsDto, customerId);
         }
      }

      private void ManageDetailsInBasket(OrderDetailsDto orderDetailsDto, int customerId)
      {
         CreateOrUpdateDetail(orderDetailsDto, customerId);

         UpdateBasketAfterAddDetail(customerId);
      }

      private void UpdateBasketAfterAddDetail(int customerId)
      {
         var userBasket = GetBasketByUserWithDetails(customerId);

         userBasket.Sum = userBasket.OrderDetailses.Sum(x => x.Price * x.Quantity);

         _unitOfWork.Orders.Update(userBasket);
         _unitOfWork.Save();
      }

      private void CreateBasket(int customerId)
      {
         var order = new Order
         {
            CustomerId = customerId,
            Date = DateTime.UtcNow,
            PaymentState = OrderStatus.New
         };

         _unitOfWork.Orders.Create(order);
         _unitOfWork.Save();
      }

      private void CreateOrUpdateDetail(OrderDetailsDto orderDetailsDto, int customerId)
      {
         var userBasket = GetBasketByUser(customerId);

         var orderDetail = GetDetailForSuchProduct(orderDetailsDto.ProductId, userBasket.OrderId);

         CheckOrderDetailAndCreateOrUpdateIt(orderDetailsDto, userBasket, orderDetail);

         _unitOfWork.Save();
      }

      private void CheckOrderDetailAndCreateOrUpdateIt(OrderDetailsDto orderDetailsDto, Order userBasket,
         OrderDetails orderDetail)
      {
         if (orderDetail != null)
         {
            UpdateQuantityOfDetail(orderDetailsDto, orderDetail);
         }
         else
         {
            CreateNewDetailInBasket(orderDetailsDto, userBasket);
         }
      }

      private void CreateNewDetailInBasket(OrderDetailsDto orderDetailsDto, Order userBasket)
      {
         var orderDetailsObj = Mapper.Map<OrderDetails>(orderDetailsDto);
         orderDetailsObj.OrderId = userBasket.OrderId;

         _unitOfWork.OrderDetails.Create(orderDetailsObj);
      }

      private void UpdateQuantityOfDetail(OrderDetailsDto orderDetailsDto, OrderDetails orderDetail)
      {
         if (orderDetail.Quantity != orderDetailsDto.Quantity)
         {
            var orderDetailsObj = Mapper.Map<OrderDetails>(orderDetail);
            orderDetailsObj.Quantity = orderDetailsDto.Quantity;

            _unitOfWork.OrderDetails.Update(orderDetailsObj);
         }
      }

      private OrderDetails GetDetailForSuchProduct(int productId, int orderId)
      {
         return _unitOfWork.OrderDetails.GetOne(x => x.ProductId == productId && x.OrderId == orderId);
      }

      private Order GetBasketByUser(int id)
      {
         var order = _unitOfWork.Orders.GetOne(o => o.PaymentState == OrderStatus.New && o.CustomerId == id);

         return order;
      }

      private bool IsBasketExistsForUser(int id)
      {
         var order = _unitOfWork.Orders.GetOne(o => o.PaymentState == OrderStatus.New && o.CustomerId == id);

         return order != null;
      }

      private Order GetBasketByUserWithDetails(int id)
      {
         var order = _unitOfWork.Orders.GetOneWithDetails(o => o.PaymentState == OrderStatus.New && o.CustomerId == id);

         return order;
      }

      private CustomerDto GetCustomerById(string userId)
      {
         var customer = _unitOfWork.Customers.GetByUserId(userId).Result;

         if (customer == null)
         {
            throw new EntityNotFoundException($"Customer with such user id doesn't exist. Id: {userId}");
         }

         var customerDto = _mapper.Map<CustomerDto>(customer);

         return customerDto;
      }
   }
}
