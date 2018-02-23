﻿using System.Collections.Generic;
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
            var products = _unitOfWork.Products.GetAll();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productDtos;
        }

        public ProductDto Get(int id)
        {
            var product =  _unitOfWork.Products.Get(id);

            if (product == null)
            {
                throw new EntityNotFoundException($"Product with such id doesn't exist. Id: {id}");
            }
            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public void Create(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            _unitOfWork.Products.Create(product);

            _unitOfWork.Save();
        }

        public void Update(ProductDto productDto)
        {
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
    }
}