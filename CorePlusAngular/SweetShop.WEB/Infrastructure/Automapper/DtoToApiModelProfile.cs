using AutoMapper;
using SweetShop.BLL.Dto;
using SweetShop.WEB.Model;

namespace TeamService.WEB.Infrastructure.Automapper
{
    public class DtoToApiModelProfile : Profile
    {
        public DtoToApiModelProfile()
        {
            CreateMap<ProductDto, ProductApiModel>();
        }
    }
}