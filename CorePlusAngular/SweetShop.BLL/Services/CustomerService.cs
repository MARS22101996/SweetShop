using System;
using System.Threading.Tasks;
using AutoMapper;
using SweetShop.BLL.Dto;
using SweetShop.BLL.Infrastructure.Exceptions;
using SweetShop.BLL.Interfaces;
using SweetShop.DAL.Entities;
using SweetShop.DAL.Interfaces;

namespace SweetShop.BLL.Services
{
   public class CustomerService : ICustomerService
   {
      private readonly IUnitOfWork _unitOfWork;
      private readonly IMapper _mapper;

      public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
      {
         _unitOfWork = unitOfWork;
         _mapper = mapper;
      }

      public async Task CreateAsync(CustomerDto customertDto)
      {
         if (customertDto == null)
         {
            throw new ArgumentNullException();
         }

         var customer = _mapper.Map<Customer>(customertDto);

         await _unitOfWork.Customers.CreateAsync(customer);

         await _unitOfWork.SaveChangesAsync();
      }

      public async Task<CustomerDto> Get(string id)
      {
         var customer = await _unitOfWork.Customers.GetByUserId(id);

         if (customer == null)
         {
            throw new EntityNotFoundException($"Product with such id doesn't exist. Id: {id}");
         }
         var customerDto = _mapper.Map<CustomerDto>(customer);

         return customerDto;
      }
   }
}
