using System.Collections.Generic;
using SweetShop.BLL.Dto;

namespace SweetShop.BLL.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAll();

        CompanyDto Get(int id);

        void Create(CompanyDto companyDto);

        void Update(CompanyDto companyDto);

        void Delete(int id);
    }
}
