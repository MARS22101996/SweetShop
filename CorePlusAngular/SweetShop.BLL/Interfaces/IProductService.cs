using System.Collections.Generic;
using SweetShop.BLL.Dto;

namespace SweetShop.BLL.Interfaces
{
   public interface IProductService
   {
      IEnumerable<ProductDto> GetAll();

      ProductDto Get(int id);

      IEnumerable<ProductDto> GetFilteredByCompany(int id);

      void Create(ProductDto productDto);

      void Update(ProductDto productDto);

      void Delete(int id);

      IEnumerable<StatisticByProductsDto> GetStatisticByProducts();

      IEnumerable<StatisticByProductsDto> GetStatisticByCompany();

      bool CheckExistanseOfLikesByUserId(string userId, int productId);

      IEnumerable<ProductDto> GetFavourites(string userId);

      void UpdateWithManagingLikes(ProductDto productDto, string userId);
   }
}
