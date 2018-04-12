using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweetShop.BLL.Dto;
using SweetShop.BLL.Interfaces;
using SweetShop.WEB.Model;

namespace SweetShop.WEB.Controllers
{
   [Route("api/order")]
   public class OrderController : Controller
   {
      private const string ClaimsType = "id";
      private readonly IMapper _mapper;
      private readonly IOrderService _orderService;
      private readonly ClaimsPrincipal _caller;
      public OrderController(
       IMapper mapper,
       IOrderService orderService,
       IHttpContextAccessor httpContextAccessor)
      {
         _mapper = mapper;
         _orderService = orderService;
         _caller = httpContextAccessor.HttpContext.User;
      }

      [HttpPost]
      public IActionResult Post([FromBody] OrderApiModel orderApiModel)
      {
         if (ModelState.IsValid)
         {
            var orderDto = _mapper.Map<OrderDto>(orderApiModel);
            _orderService.Update(orderDto);

            return Ok(orderApiModel);
         }

         return BadRequest(ModelState);
      }
   }
}