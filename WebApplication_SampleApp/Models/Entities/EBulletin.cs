using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SampleApp.Models.Entities
{
	[Table("EBulletins")]
    public class EBulletin
    {
        [Key]
        public int ID { get; set; }

        [Required, EmailAddress, StringLength(60), Display(Name = ("E-mail Address"))]
        public string EmailAddress { get; set; }
        public bool Banned { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
