using System.Collections.Generic;
using SweetShop.BLL.Dto;

namespace SweetShop.BLL.Interfaces
{
   public interface IStatisticService
   {
      IEnumerable<StatisticByProductsDto> GetStatisticByProducts();

      IEnumerable<StatisticByProductsDto> GetStatisticByCompany();
   }
}
