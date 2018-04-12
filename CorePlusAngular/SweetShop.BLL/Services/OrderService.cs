using AutoMapper;
using SweetShop.BLL.Dto;
using SweetShop.BLL.Interfaces;
using SweetShop.DAL.Entities;
using SweetShop.DAL.Interfaces;

namespace SweetShop.BLL.Services
{
   public class OrderService : IOrderService
   {
      private readonly IUnitOfWork _unitOfWork;
      private readonly IMapper _mapper;

      public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
      {
         _unitOfWork = unitOfWork;
         _mapper = mapper;
      }

      public void Update(OrderDto orderDto)
      {
         var order = _mapper.Map<Order>(orderDto);

         _unitOfWork.Orders.Update(order);

         _unitOfWork.Save();
      }
   }
}