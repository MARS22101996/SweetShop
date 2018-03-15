﻿using AutoMapper;
using SweetShop.BLL.Dto;
using SweetShop.DAL.Entities;

namespace SweetShop.BLL.Infrastructure.Automapper
{
   public class EntityToDtoProfile : Profile
   {
      public EntityToDtoProfile()
      {
         CreateMap<Product, ProductDto>();
         CreateMap<Company, CompanyDto>();
         CreateMap<Customer, CustomerDto>();
         CreateMap<AppUser, AppUserDto>();
      }
   }
}