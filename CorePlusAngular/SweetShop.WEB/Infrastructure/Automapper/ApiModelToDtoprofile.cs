using AutoMapper;
using SweetShop.BLL.Dto;
using SweetShop.DAL.Entities;
using SweetShop.WEB.Model;

namespace SweetShop.WEB.Infrastructure.Automapper
{
   public class ApiModelToDtoProfile : Profile
   {
      public ApiModelToDtoProfile()
      {
         CreateMap<ProductApiModel, ProductDto>();
         CreateMap<CompanyApiModel, CompanyDto>();
         CreateMap<RegistrationViewModel, AppUser>()
         .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
         CreateMap<OrderDetailsApiModel, OrderDetailsDto>()
         .ForMember(x => x.Product, y => y.Ignore());
         CreateMap<OrderApiModel, OrderDto>();
         CreateMap<FeedbackApiModel, FeedbackDto>();
      }
   }
}