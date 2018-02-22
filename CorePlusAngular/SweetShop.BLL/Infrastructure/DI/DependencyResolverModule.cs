using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SweetShop.DAL.Interfaces;
using SweetShop.DAL.UnitOfWorks;

namespace SweetShop.BLL.Infrastructure.DI
{
    public class DependencyResolverModule
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}