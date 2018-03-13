//using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SweetShop.BLL.Dto;
using SweetShop.BLL.Interfaces;
using SweetShop.DAL.Entities;
using SweetShop.WEB.Model;


namespace SweetShop.WEB.Controllers
{
   //[Authorize(Policy = "ApiUser")]
   [Route("api/companies")]
    public class CompanyController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;

        public CompanyController(UserManager<AppUser> userManager,
            ICompanyService companyService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _companyService = companyService;
        }

        [HttpGet]
        public IEnumerable<CompanyApiModel> Get()
        {
            var productDtos = _companyService.GetAll();
            var productApiModels = _mapper.Map<IEnumerable<CompanyApiModel>>(productDtos);

            return productApiModels;
        }

        [HttpGet("{id}")]
        public CompanyApiModel Get(int id)
        {
            var productDto = _companyService.Get(id);
            var productApiModel = _mapper.Map<CompanyApiModel>(productDto);

            return productApiModel;
        }

        [HttpPost]
        public IActionResult Post([FromBody]CompanyApiModel company)
        {
            if (ModelState.IsValid)
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                _companyService.Create(companyDto);

                return Ok(company);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CompanyApiModel company)
        {
            if (ModelState.IsValid)
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                _companyService.Update(companyDto);

                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _companyService.Get(id);
            _companyService.Delete(id);

            return Ok(product);
        }
    }
}
