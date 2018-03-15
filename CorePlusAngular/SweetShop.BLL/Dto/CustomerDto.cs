namespace SweetShop.BLL.Dto
{
   public class CustomerDto
   {
      public int Id { get; set; }
      public string IdentityId { get; set; }
      public AppUserDto Identity { get; set; }
      public string Location { get; set; }
      public string Locale { get; set; }
      public string Gender { get; set; }
   }
}