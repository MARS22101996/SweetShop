﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweetShop.BLL.Dto;
using SweetShop.BLL.Interfaces;
using SweetShop.WEB.Model;

namespace SweetShop.WEB.Controllers
{
   [Route("api/basket")]
   public class BasketController : Controller
   {
      private const string ClaimsType = "id";
      private readonly IMapper _mapper;
      private readonly IBasketService _basketService;
      private readonly ClaimsPrincipal _caller;

      public BasketController(
         IBasketService basketService,
         IMapper mapper,
         IHttpContextAccessor httpContextAccessor)
      {
         _mapper = mapper;
         _basketService = basketService;
         _caller = httpContextAccessor.HttpContext.User;
      }

      [HttpGet]
      public OrderApiModel Get()
      {
         var userId = _caller.Claims.Single(c => c.Type == ClaimsType);
         var basketForUser = _basketService.GetBasketForUser(userId.Value);
         var basketApiModel = _mapper.Map<OrderApiModel>(basketForUser);

         return basketApiModel;
      }

      [HttpPost]
      public IActionResult Post([FromBody] OrderDetailsApiModel orderDetails)
      {
         if (ModelState.IsValid)
         {
            var userId = _caller.Claims.Single(c => c.Type == ClaimsType);

            var orderDetailsDto = _mapper.Map<OrderDetailsDto>(orderDetails);
            _basketService.BuyProduct(orderDetailsDto, userId.Value);

            return Ok(orderDetails);
         }

         return BadRequest(ModelState);
      }

      [HttpDelete("{id}")]
      public IActionResult Delete(int id)
      {
         _basketService.Delete(id);

         return Ok();
      }
   }
}
