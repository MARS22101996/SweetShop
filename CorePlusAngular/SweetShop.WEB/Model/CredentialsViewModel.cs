using FluentValidation.Attributes;
using SweetShop.WEB.Validation;

namespace SweetShop.WEB.Model
{
   [Validator(typeof(CredentialsViewModelValidator))]
   public class CredentialsViewModel
   {
      public string UserName { get; set; }
      public string Password { get; set; }
   }
}
