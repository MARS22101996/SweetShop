using System;
using System.IO;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using SweetShop.DAL.Context;
using SweetShop.DAL.Entities;
using SweetShop.WEB.Infrastructure.Auth;
using SweetShop.WEB.Infrastructure.DI;
using SweetShop.WEB.Model;

namespace SweetShop.WEB
{
   public class Startup
   {
      // This method gets called by the runtime. Use this method to add services to the container.
      // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
      private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure

      private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }


      public IConfiguration Configuration { get; }

      public void ConfigureServices(IServiceCollection services)
      {
         DependencyResolver.Resolve(services, Configuration);

         var connectionString = Configuration.GetConnectionString("DefaultConnection");

         services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("SweetShop.DAL")));

         services.AddSingleton<IJwtFactory, JwtFactory>();

         services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

         services.Configure<FacebookAuthSettings>(Configuration.GetSection(nameof(FacebookAuthSettings)));

         // Get options from app settings
         var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

         // Configure JwtIssuerOptions
         services.Configure<JwtIssuerOptions>(options =>
         {
            options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
            options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
            options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
         });

         var tokenValidationParameters = new TokenValidationParameters
         {
            ValidateIssuer = true,
            ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

            ValidateAudience = true,
            ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _signingKey,

            RequireExpirationTime = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
         };

         services.AddAuthentication(options =>
         {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

         }).AddJwtBearer(configureOptions =>
         {
            configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
            configureOptions.TokenValidationParameters = tokenValidationParameters;
            configureOptions.SaveToken = true;
         });

         // api user claim policy
         services.AddAuthorization(options =>
         {
            options.AddPolicy("ApiUser", policy => policy.RequireClaim(JwtConstants.Rol, JwtConstants.ApiAccess));
         });

         // add identity
         var builder = services.AddIdentityCore<AppUser>(o =>
         {
            // configure identity options
            o.Password.RequireDigit = false;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 6;
         });
         builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
         builder.AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

         services.AddAutoMapper();
         services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();

            app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
            {
               HotModuleReplacement = true
            });
         }

         app.UseAuthentication();
         app.UseDefaultFiles();
         app.UseStaticFiles();
         app.UseMvc();


         app.Run(async context =>
         {
            context.Response.ContentType = "text/html";
            await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
         });
      }
   }
}
