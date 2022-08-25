using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication_SampleApp.Models.Entities;

namespace WebApplication_SampleApp.Models.Entities
{
    [Table("Users")]
    public class User : EntityLogBase
    {
        [Key]
        public int ID { get; set; }

        [StringLength(60), Display(Name = "Name Surname")]
        public string? Fullname { get; set; }

        [Required, StringLength(30), Display(Name = "Username")]
        public string Username { get; set; }

        [Required, StringLength(60), EmailAddress, Display(Name = "E-mail Address")]
        public string Email { get; set; }

        [Required, StringLength(150), Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }

        public virtual List<Order> Orders { get; set; }
        public virtual List<Service> Services { get; set; }
        public virtual List<OrderedService> OrderedServices { get; set; }







    }

}

