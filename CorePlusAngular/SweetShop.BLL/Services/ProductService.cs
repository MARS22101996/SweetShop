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

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<ProductDto> GetAll()
        {
            var products = _unitOfWork.Products.GetAllProducts();

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
            var product =  _unitOfWork.Products.GetProduct(id);

            if (product == null)
            {
                throw new EntityNotFoundException($"Product with such id doesn't exist. Id: {id}");
            }
            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }


       public ProductDto ManageProductsLikes(ProductDto product, string userId)
       {
          if (product.Likes > 0)
          {
             var customer = GetCustomerById(userId);

             var customerProducts = GetProductCustomers(customer.Id, product.Id);

             if (customerProducts.Any())
             {
                DeleteLikeIfCustomerLiked(product, customerProducts);
             }
             else
             {
                AddLikeIfCustomerDidNotLike(product, customer);
             }
          }

          return product;
       }

      private void AddLikeIfCustomerDidNotLike(ProductDto product, Customer customer)
      {
         var productCustomerDto = new ProductCustomerDto
         {
            ProductId = product.Id,
            CustomerId = customer.Id
         };

         CreateProductCustomer(productCustomerDto);
      }

      private void DeleteLikeIfCustomerLiked(ProductDto product, List<ProductCustomer> customerProducts)
      {
         product.Likes = product.Likes - 2;

         DeleteProductCustomer(customerProducts.First().Id);
      }

      private Customer GetCustomerById(string userId)
      {
         return _unitOfWork.Customers.GetByUserId(userId).Result;
      }

      private List<ProductCustomer> GetProductCustomers(int customerId, int productId)
      {
         return _unitOfWork.ProductCustomers.Get(x => x.CustomerId == customerId && x.ProductId == productId).ToList();
      }

       public bool CheckExistanseOfLikesForCustomer(string userId, int productId)
       {
         var customer = GetCustomerById(userId);

         return GetProductCustomers(customer.Id, productId).Any();
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

      public void CreateProductCustomer(ProductCustomerDto productCustomerDto)
      {
         if (productCustomerDto == null)
         {
            throw new ArgumentNullException();
         }

         var productCustomer = _mapper.Map<ProductCustomer>(productCustomerDto);

         _unitOfWork.ProductCustomers.Create(productCustomer);

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

       public void DeleteProductCustomer(int id)
       {
          _unitOfWork.ProductCustomers.Delete(id);

          _unitOfWork.Save();
       }

      public IEnumerable<StatisticByProductsDto> GetStatisticByProducts()
        {
            var statistic = _unitOfWork.Products.GetAll()
                .GroupBy(grp => new {grp.Name})
                .Select(result => new StatisticByProductsDto
                {
                    Name = result.Key.Name,
                    Likes = result.Sum(x => x.Likes)
                });

            return statistic;
        }

        public IEnumerable<StatisticByProductsDto> GetStatisticByCompany()
        {
            var statistic = _unitOfWork.Products.GetAll()
                .Join(_unitOfWork.Companies.GetAll(),
                    product => product.CompanyId,
                    company => company.Id,
                    (product, company) => new {company, product})
                .GroupBy(grp => new {grp.company.Name})
                .Select(result => new StatisticByProductsDto
                {
                    Name = result.Key.Name,
                    Likes = result.Sum(x => x.product.Likes)
                });

            return statistic;
        }
    }
}
