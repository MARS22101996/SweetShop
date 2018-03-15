using AutoMapper;
using SweetShop.BLL.Dto;
using SweetShop.DAL.Entities;

namespace SweetShop.BLL.Infrastructure.Automapper
{
   public class DtoToEntityProfile : Profile
   {
      public DtoToEntityProfile()
      {
         CreateMap<ProductDto, Product>()
         .ForSourceMember(x => x.Company, opt => opt.Ignore());
         CreateMap<CompanyDto, Company>();
         CreateMap<ProductCustomerDto, ProductCustomer>();
         CreateMap<CustomerDto, Customer>();
         CreateMap<AppUserDto, AppUser>();
      }
   }
}