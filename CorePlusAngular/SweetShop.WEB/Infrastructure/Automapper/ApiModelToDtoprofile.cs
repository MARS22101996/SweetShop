using AutoMapper;
using SweetShop.BLL.Dto;
using SweetShop.WEB.Model;

namespace SweetShop.WEB.Infrastructure.Automapper
{
    public class ApiModelToDtoProfile : Profile
    {
        public ApiModelToDtoProfile()
        {
            CreateMap<ProductApiModel, ProductDto>();
        }
    }
}