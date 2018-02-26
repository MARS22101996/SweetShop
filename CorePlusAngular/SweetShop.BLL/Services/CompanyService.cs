using System.Collections.Generic;
using AutoMapper;
using SweetShop.BLL.Dto;
using SweetShop.BLL.Infrastructure.Exceptions;
using SweetShop.BLL.Interfaces;
using SweetShop.DAL.Entities;
using SweetShop.DAL.Interfaces;

namespace SweetShop.BLL.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<CompanyDto> GetAll()
        {
            var companies = _unitOfWork.Companies.GetAll();
            var companiesDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            return companiesDtos;
        }

        public CompanyDto Get(int id)
        {
            var product =  _unitOfWork.Companies.Get(id);

            if (product == null)
            {
                throw new EntityNotFoundException($"Product with such id doesn't exist. Id: {id}");
            }
            var productDto = _mapper.Map<CompanyDto>(product);

            return productDto;
        }

        public void Create(CompanyDto companyDto)
        {
            var product = _mapper.Map<Company>(companyDto);

            _unitOfWork.Companies.Create(product);

            _unitOfWork.Save();
        }

        public void Update(CompanyDto companyDto)
        {
            var product = _mapper.Map<Company>(companyDto);

            _unitOfWork.Companies.Update(product);

            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var product = _unitOfWork.Companies.Get(id);

            if (product == null)
            {
                throw new EntityNotFoundException($"Product with such id doesn't exist. Id: {id}");
            }

            _unitOfWork.Companies.Delete(id);

            _unitOfWork.Save();
        }
    }
}
