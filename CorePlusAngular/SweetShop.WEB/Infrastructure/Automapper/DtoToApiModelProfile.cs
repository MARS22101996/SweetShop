using AutoMapper;
using SweetShop.BLL.Dto;
using SweetShop.WEB.Model;

namespace SweetShop.WEB.Infrastructure.Automapper
{
    public class DtoToApiModelProfile : Profile
    {
        public DtoToApiModelProfile()
        {
            CreateMap<ProductDto, ProductApiModel>();
            CreateMap<CompanyDto, CompanyApiModel>();
        }
    }
}