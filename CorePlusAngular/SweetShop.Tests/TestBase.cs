using AutoMapper;
using SweetShop.WEB.Infrastructure.Automapper;

namespace SweetShop.Tests
{
    public class TestBase
    {
        protected IMapper GetMapper()
        {
            var config = new AutoMapperConfiguration();

            return config.Configure().CreateMapper();
        }
    }
}
