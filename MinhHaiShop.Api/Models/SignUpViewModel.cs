namespace MinhHaiShop.Web.Models
{
    public class SignUpViewModel
    {

        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string Address { set; get; } = null!;
        public DateTime? BirthDay { set; get; }
    }
}
