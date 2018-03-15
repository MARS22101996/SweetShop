using System.Threading.Tasks;
using SweetShop.BLL.Dto;

namespace SweetShop.BLL.Interfaces
{
    public interface ICustomerService
    {
       Task CreateAsync(CustomerDto customertDto);

       Task<CustomerDto> Get(string id);
    }
}
