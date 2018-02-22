using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SweetShop.BLL.Dto;

namespace SweetShop.BLL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAll();

        ProductDto Get(int id);

        void Create(ProductDto productDto);

        void Update(ProductDto productDto);

        void Delete(int id);
    }
}
