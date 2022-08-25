using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplication_SampleApp.Models
{
    public class RegisterViewModel
    {
        [Required, StringLength(30), Display(Name = "Username")]

        public string Username { get; set; }
        
        [Required, StringLength(60), EmailAddress, Display(Name = "E-mail Address")]

        public string Email { get; set; }
        [Required, StringLength(16, MinimumLength = 6), Display(Name = "Password")]

        public string Password { get; set; }
        [Required, StringLength(16,MinimumLength =6), Display(Name = "Repassword"),Compare(nameof(Password))]

        public string RePassword { get; set; }


    }
}
