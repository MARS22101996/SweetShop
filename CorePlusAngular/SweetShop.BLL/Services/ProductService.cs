using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SweetShop.BLL.Dto;
using SweetShop.BLL.Infrastructure.Exceptions;
using SweetShop.BLL.Interfaces;
using SweetShop.DAL.Entities;
using SweetShop.DAL.Interfaces;

namespace SweetShop.BLL.Services
{
   public class ProductService : IProductService
   {
      private readonly IUnitOfWork _unitOfWork;
      private readonly IMapper _mapper;
      private readonly IBasketService _basketService;

      public ProductService(
         IUnitOfWork unitOfWork,
         IMapper mapper,
         IBasketService basketService)
      {
         _unitOfWork = unitOfWork;
         _mapper = mapper;
         _basketService = basketService;
      }

      public IEnumerable<ProductDto> GetAll()
      {
         var products = _unitOfWork.Products.GetAllProducts();

         var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

         return productDtos;
      }

      public IEnumerable<ProductDto> GetAllWithQuantity(string userId)
      {
         var customer = GetCustomerById(userId);

         var products = _unitOfWork.Products.GetAllProducts();

         var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products).ToList();

         SetQuantityInBasketForCurrentUser(customer, productDtos);

         return productDtos;
      }

      public IEnumerable<ProductDto> GetFavourites(string userId)
      {
         var customer = GetCustomerById(userId);

         var products = _unitOfWork.ProductCustomers.GetWithProducts(x => x.CustomerId == customer.Id).ToList();

         var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

         return productDtos;
      }

      public IEnumerable<ProductDto> GetFilteredByCompany(int id)
      {
         var products = _unitOfWork.Products.Get(x => x.CompanyId == id);

         var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

         return productDtos;
      }

      public ProductDto Get(int id)
      {
         var product = _unitOfWork.Products.GetProduct(id);

         if (product == null)
         {
            throw new EntityNotFoundException($"Product with such id doesn't exist. Id: {id}");
         }

         var productDto = _mapper.Map<ProductDto>(product);

         return productDto;
      }

      public void UpdateWithManagingLikes(ProductDto productDto, string userId)
      {
         var customer = GetCustomerById(userId);

         productDto = ManageProductsLikes(productDto, customer.Id);

         Update(productDto);

         ManageProductCustomerLikes(productDto, customer);
      }

      public bool CheckExistanseOfLikesByUserId(string userId, int productId)
      {
         var customer = GetCustomerById(userId);

         var productCustomer = GetProductCustomerByCustomerId(customer.Id, productId);

         return productCustomer != null;
      }

      public void Create(ProductDto productDto)
      {
         if (productDto == null)
         {
            throw new ArgumentNullException();
         }

         var product = _mapper.Map<Product>(productDto);

         var company = _unitOfWork.Companies.Get(product.CompanyId);

         product.Company =
         company ?? throw new EntityNotFoundException(
            $"Company with such id doesn't exist. Id: {product.CompanyId}");

         _unitOfWork.Products.Create(product);

         _unitOfWork.Save();
      }

      public void Update(ProductDto productDto)
      {
         if (productDto == null)
         {
            throw new ArgumentNullException();
         }

         var product = _mapper.Map<Product>(productDto);

         _unitOfWork.Products.Update(product);

         _unitOfWork.Save();
      }

      public void Delete(int id)
      {
         var product = _unitOfWork.Products.Get(id);

         if (product == null)
         {
            throw new EntityNotFoundException($"Product with such id doesn't exist. Id: {id}");
         }

         _unitOfWork.Products.Delete(id);

         _unitOfWork.Save();
      }

      private ProductDto ManageProductsLikes(ProductDto product, int customerId)
      {
         if (product.Likes > 0)
         {
            var productCustomer = GetProductCustomerByCustomerId(customerId, product.Id);

            if (productCustomer != null)
            {
               product.Likes = product.Likes - 2;
               product.IsLikedByUser = false;
               product.CustomerProductId = productCustomer.Id;
            }
            else
            {
               product.IsLikedByUser = true;
            }
         }

         return product;
      }

      private void ManageProductCustomerLikes(ProductDto product, CustomerDto customer)
      {
         if (!product.IsLikedByUser)
         {
            if (product.CustomerProductId != null)
            {
               DeleteLikeIfCustomerLiked(product.CustomerProductId.Value);
            }
         }
         else
         {
            AddLikeIfCustomerDidNotLike(product.Id, customer.Id);
         }
      }

      private void AddLikeIfCustomerDidNotLike(int productId, int customerId)
      {
         var productCustomerDto = new ProductCustomerDto
         {
            ProductId = productId,
            CustomerId = customerId
         };

         CreateProductCustomer(productCustomerDto);
      }

      private void DeleteLikeIfCustomerLiked(int id)
      {
         _unitOfWork.ProductCustomers.Delete(id);

         _unitOfWork.Save();
      }

      private CustomerDto GetCustomerById(string userId)
      {
         var customer = _unitOfWork.Customers.GetByUserId(userId).Result;

         var customerDto = _mapper.Map<CustomerDto>(customer);

         return customerDto;
      }

      private ProductCustomer GetProductCustomers(int customerId, int productId)
      {
         return _unitOfWork.ProductCustomers.Get(x => x.CustomerId == customerId && x.ProductId == productId)
         .FirstOrDefault();
      }

      private void CreateProductCustomer(ProductCustomerDto productCustomerDto)
      {
         if (productCustomerDto == null)
         {
            throw new ArgumentNullException();
         }

         var productCustomer = _mapper.Map<ProductCustomer>(productCustomerDto);

         _unitOfWork.ProductCustomers.Create(productCustomer);

         _unitOfWork.Save();
      }

      private void SetQuantityInBasketForCurrentUser(CustomerDto customer, List<ProductDto> productDtos)
      {
         foreach (var product in productDtos)
         {
            product.Quantity = GetQuantityOfProducts(product.Id, customer.Id);
         }
      }

      private int GetQuantityOfProducts(int productId, int customerId)
      {
         var detailsForProduct = _basketService.GetOrderDetailsForProduct(productId, customerId);

         return detailsForProduct?.Quantity ?? 0;
      }

      private ProductCustomer GetProductCustomerByCustomerId(int customerId, int productId)
      {
         var productCustomer = GetProductCustomers(customerId, productId);

         return productCustomer;
      }
   }
}
