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
            CreateMap<ProductDto, ProductViewApiModel>()
                .ForMember(x => x.Company, y => y.MapFrom(z => z.Company.Name))
                .ForSourceMember(x => x.Company, opt => opt.Ignore());
            CreateMap<StatisticByProductsDto, StatisticByProductsApiModel>();
        }
    }
}