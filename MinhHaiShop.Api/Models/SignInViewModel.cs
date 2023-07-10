using System.ComponentModel.DataAnnotations;

namespace MinhHaiShop.Web.Models
{
    public class SignInViewModel
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
