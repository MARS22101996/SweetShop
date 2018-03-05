using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SweetShop.BLL.Dto;
using SweetShop.BLL.Interfaces;
using SweetShop.DAL.Entities;
using SweetShop.WEB.Model;

namespace SweetShop.WEB.Controllers
{
    [Route("api/products")]
    public class ProductController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(IProductService productService,
            IMapper mapper)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        public IEnumerable<ProductViewApiModel> Get()
        {
            var productDtos = _productService.GetAll();
            var productApiModels = _mapper.Map<IEnumerable<ProductViewApiModel>>(productDtos);

            return productApiModels;
        }

        [HttpGet("{id}")]
        public ProductViewApiModel GetForView(int id)
        {
            var productDto = _productService.Get(id);
            var productApiModel = _mapper.Map<ProductViewApiModel>(productDto);

            return productApiModel;
        }

        [HttpGet("company/{id}")]
        public IEnumerable<ProductViewApiModel> GetForCompany(int id)
        {
            var productDtos = _productService.GetFilteredByCompany(id);
            var productApiModels = _mapper.Map<IEnumerable<ProductViewApiModel>>(productDtos);

            return productApiModels;
        }

        [HttpGet("statistic")]
        public IEnumerable<StatisticByProductsApiModel> GetStatisticByProducts()
        {
            var productDtos = _productService.GetStatisticByProducts();
            var statisticApiModels = _mapper.Map<IEnumerable<StatisticByProductsApiModel>>(productDtos);

            return statisticApiModels;
        }

        [HttpGet("statistic/company")]
        public IEnumerable<StatisticByProductsApiModel> GetStatisticByCompany()
        {
            var productDtos = _productService.GetStatisticByCompany();
            var statisticApiModels = _mapper.Map<IEnumerable<StatisticByProductsApiModel>>(productDtos);

            return statisticApiModels;
        }

        [HttpGet("raw/{id}")]
        public ProductApiModel Get(int id)
        {
            var productDto = _productService.Get(id);
            var productApiModel = _mapper.Map<ProductApiModel>(productDto);

            return productApiModel;
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProductApiModel product)
        {
            if (ModelState.IsValid)
            {
                var producDto = _mapper.Map<ProductDto>(product);
                _productService.Create(producDto);

                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                var producDto = _mapper.Map<ProductDto>(product);
                _productService.Update(producDto);

                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _productService.Get(id);
            _productService.Delete(id);

            return Ok(product);
        }
    }
}