using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AngularASPNETCore2WebApiAuth.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SweetShop.DAL.Context;
using SweetShop.DAL.Entities;
using SweetShop.WEB.Helpers;
using SweetShop.WEB.Infrastructure.Auth;
using SweetShop.WEB.Model;

namespace SweetShop.WEB.Controllers
{
  [Route("api/[controller]")]
  public class AuthController : Controller
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtFactory _jwtFactory;
    private readonly JwtIssuerOptions _jwtOptions;
    private readonly ClaimsPrincipal _caller;
    private readonly ApplicationContext _appDbContext;

    public AuthController(UserManager<AppUser> userManager,
      IJwtFactory jwtFactory,
      IOptions<JwtIssuerOptions> jwtOptions,
      IHttpContextAccessor httpContextAccessor,
       ApplicationContext appDbContext)
    {
      _caller = httpContextAccessor.HttpContext.User;
      _userManager = userManager;
      _jwtFactory = jwtFactory;
      _jwtOptions = jwtOptions.Value;
       _appDbContext = appDbContext;
    }

    // POST api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Post([FromBody] CredentialsViewModel credentials)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
      if (identity == null)
      {
        return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
      }

      var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions,
        new JsonSerializerSettings {Formatting = Formatting.Indented});
      return new OkObjectResult(jwt);
    }

    private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
    {
      if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        return await Task.FromResult<ClaimsIdentity>(null);

      var userToVerify = await _userManager.FindByNameAsync(userName);

      if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

      if (await _userManager.CheckPasswordAsync(userToVerify, password))
      {
        return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
      }

      return await Task.FromResult<ClaimsIdentity>(null);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUser()
   {
      var userId = _caller.Claims.Single(c => c.Type == "id");

      var customer = await _appDbContext.Customers.Include(c => c.Identity)
      .SingleAsync(c => c.Identity.Id == userId.Value);

      return new OkObjectResult(new
      {
        Message = "This is secure API and user data!",
        customer.Identity.FirstName,
        customer.Identity.LastName,
        customer.Identity.PictureUrl,
        customer.Identity.FacebookId,
        customer.Location,
        customer.Locale,
        customer.Gender
      });
    }
  }
}
