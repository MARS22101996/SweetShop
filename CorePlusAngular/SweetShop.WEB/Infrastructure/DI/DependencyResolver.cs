using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SweetShop.BLL.Infrastructure.DI;
using SweetShop.BLL.Interfaces;
using SweetShop.BLL.Services;

namespace SweetShop.WEB.Infrastructure.DI
{
   public static class DependencyResolver
   {
      public static void Resolve(IServiceCollection services, IConfiguration configuration)
      {
         services.AddSingleton(configuration);
         services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
         services.AddTransient<IProductService, ProductService>();
         services.AddTransient<ICompanyService, CompanyService>();
         services.AddTransient<ICustomerService, CustomerService>();
         services.AddTransient<IStatisticService, StatisticService>();
         services.AddTransient<IBasketService, BasketService>();
         services.AddTransient<IOrderService, OrderService>();

         DependencyResolverModule.Configure(services);
      }
   }
}