using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MinhHaiShop.Model.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(256)]
        public string FullName { set; get; }

        [Required]
        public string Password { get; set; }

        [MaxLength(256)]
        public string Address { set; get; }

        public DateTime? BirthDay { set; get; }

    }
}
