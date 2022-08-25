using System.ComponentModel.DataAnnotations;

namespace WebApplication_SampleApp.Models
{
    public class UserViewModel
    {
        [StringLength(60), Display(Name = "Name Surname")]
        public string Fullname { get; set; }

        [Required, StringLength(30), Display(Name = "Username")]

        public string Username { get; set; }

        [Required, StringLength(60), EmailAddress, Display(Name = "E-mail Address")]

        public string Email { get; set; }
        [Required, StringLength(16, MinimumLength = 6), Display(Name = "Password")]

        public string Password { get; set; }
        [Required, StringLength(16, MinimumLength = 6), Display(Name = "Repassword"), Compare(nameof(Password))]

        public string RePassword { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }


    }
}
