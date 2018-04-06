using System.Collections.Generic;
using System.Linq;
using SweetShop.BLL.Dto;
using SweetShop.BLL.Interfaces;
using SweetShop.DAL.Interfaces;

namespace SweetShop.BLL.Services
{
   public class StatisticService : IStatisticService
   {
      private readonly IUnitOfWork _unitOfWork;

      public StatisticService(IUnitOfWork unitOfWork)
      {
         _unitOfWork = unitOfWork;
      }

      public IEnumerable<StatisticByProductsDto> GetStatisticByProducts()
      {
         var statistic = _unitOfWork.Products.GetAll()
         .GroupBy(grp => new { grp.Name })
         .Select(result => new StatisticByProductsDto
         {
            Name = result.Key.Name,
            Likes = result.Sum(x => x.Likes)
         });

         return statistic;
      }

      public IEnumerable<StatisticByProductsDto> GetStatisticByCompany()
      {
         var statistic = _unitOfWork.Products.GetAll()
         .Join(
            _unitOfWork.Companies.GetAll(),
            product => product.CompanyId,
            company => company.Id,
            (product, company) => new { company, product })
         .GroupBy(grp => new { grp.company.Name })
         .Select(result => new StatisticByProductsDto
         {
            Name = result.Key.Name,
            Likes = result.Sum(x => x.product.Likes)
         });

         return statistic;
      }
   }
}