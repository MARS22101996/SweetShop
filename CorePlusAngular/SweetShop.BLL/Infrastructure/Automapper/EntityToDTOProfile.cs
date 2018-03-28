using AutoMapper;
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
         CreateMap<ProductCustomer, ProductDto>()
         .ForMember(au => au.Id, map => map.MapFrom(vm => vm.Product.Id))
         .ForMember(au => au.CompanyId, map => map.MapFrom(vm => vm.Product.CompanyId))
         .ForMember(au => au.Description, map => map.MapFrom(vm => vm.Product.Description))
         .ForMember(au => au.Likes, map => map.MapFrom(vm => vm.Product.Likes))
         .ForMember(au => au.Price, map => map.MapFrom(vm => vm.Product.Price))
         .ForMember(au => au.Name, map => map.MapFrom(vm => vm.Product.Name));

      }
   }
}