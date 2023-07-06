using System.ComponentModel.DataAnnotations;

namespace MinhHaiShop.Model.Models
{
    public class SignIn
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
