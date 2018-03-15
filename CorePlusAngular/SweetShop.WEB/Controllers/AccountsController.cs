using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SweetShop.BLL.Dto;
using SweetShop.BLL.Interfaces;
using SweetShop.DAL.Context;
using SweetShop.DAL.Entities;
using SweetShop.WEB.Helpers;
using SweetShop.WEB.Model;

namespace SweetShop.WEB.Controllers
{
   [Route("api/[controller]")]
   public class AccountsController : Controller
   {
      private readonly ApplicationContext _appDbContext;
      private readonly ICustomerService _customerService;
      private readonly UserManager<AppUser> _userManager;
      private readonly IMapper _mapper;

      public AccountsController(UserManager<AppUser> userManager,
         IMapper mapper,
         ApplicationContext appDbContext,
         ICustomerService customerService)
      {
         _userManager = userManager;
         _mapper = mapper;
         _appDbContext = appDbContext;
         _customerService = customerService;
      }

      // POST api/accounts
      [HttpPost]
      public async Task<IActionResult> Post([FromBody] RegistrationViewModel model)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         var userIdentity = _mapper.Map<AppUser>(model);

         var result = await _userManager.CreateAsync(userIdentity, model.Password);

         if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

         await _customerService.CreateAsync(new CustomerDto {IdentityId = userIdentity.Id, Location = model.Location});

         return new OkObjectResult("Account created");
      }
   }
}
