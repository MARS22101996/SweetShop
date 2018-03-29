using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
      private readonly ClaimsPrincipal _caller;
      private readonly IMapper _mapper;
      private readonly IProductService _productService;
      private readonly IStatisticService _statisticService;
      private const string ClaimsType = "id";

      public ProductController(UserManager<AppUser> userManager,
         IProductService productService,
         IMapper mapper,
         IHttpContextAccessor httpContextAccessor, 
         IStatisticService statisticService)
      {
         _mapper = mapper;
         _statisticService = statisticService;
         _productService = productService;
         _caller = httpContextAccessor.HttpContext.User;
      }

      [HttpGet]
      public IEnumerable<ProductViewApiModel> Get()
      {
         var userId = _caller.Claims.Single(c => c.Type == ClaimsType);
         var productDtos = _productService.GetAllWithQuantity(userId.Value);
         var productApiModels = _mapper.Map<IEnumerable<ProductViewApiModel>>(productDtos).ToList();

         SetFieldIsLIkedByUser(userId, productApiModels);
         return productApiModels;
      }

      [HttpGet("favourites")]
      public IEnumerable<ProductViewApiModel> GetFavotites()
      {
         var userId = _caller.Claims.Single(c => c.Type == ClaimsType);
         var productDtos = _productService.GetFavourites(userId.Value);
         var productApiModels = _mapper.Map<IEnumerable<ProductViewApiModel>>(productDtos).ToList();

         SetProductsAsLiked(productApiModels);
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
         var productDtos = _statisticService.GetStatisticByProducts();
         var statisticApiModels = _mapper.Map<IEnumerable<StatisticByProductsApiModel>>(productDtos);

         return statisticApiModels;
      }

      [HttpGet("statistic/company")]
      public IEnumerable<StatisticByProductsApiModel> GetStatisticByCompany()
      {
         var productDtos = _statisticService.GetStatisticByCompany();
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
      public IActionResult Post([FromBody] ProductApiModel product)
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
      public IActionResult Put(int id, [FromBody] Product product)
      {
         if (ModelState.IsValid)
         {
            var productDto = _mapper.Map<ProductDto>(product);

            var userId = _caller.Claims.Single(c => c.Type == ClaimsType);

            _productService.UpdateWithManagingLikes(productDto, userId.Value);

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


      [HttpGet("checkLikes/{id}")]
      public bool CheckExistanseOfLikesForCustomer(int id)
      {
         var userId = _caller.Claims.Single(c => c.Type == ClaimsType);

         var isLikeExists = _productService.CheckExistanseOfLikesByUserId(userId.Value, id);

         return isLikeExists;
      }

      private void SetFieldIsLIkedByUser(Claim userId, IEnumerable<ProductViewApiModel> productApiModels)
      {
         foreach (var product in productApiModels)
         {
            product.IsLikedByUser = _productService.CheckExistanseOfLikesByUserId(userId.Value, product.Id);
         }
      }

      private static void SetProductsAsLiked(List<ProductViewApiModel> productApiModels)
      {
         foreach (var product in productApiModels)
         {
            product.IsLikedByUser = true;
         }
      }
   }
}