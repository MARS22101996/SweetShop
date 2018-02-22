using AutoMapper;
using SweetShop.BLL.Dto;
using SweetShop.DAL.Entities;

namespace SweetShop.BLL.Infrastructure.Automapper
{
    public class DtoToEntityProfile : Profile
    {
        public DtoToEntityProfile()
        {
            CreateMap<ProductDto, Product>();
        }
    }
}