using System.ComponentModel.DataAnnotations;

namespace WebApplication_SampleApp.Models.Entities
{
	public class EntityLogBase
    {
       
        [Required, StringLength(60)]
        public string CreatedUser { get; set; }

        public DateTime CreatedAt { get; set; }

        [StringLength(50)]
        public string? ModifiedUser { get; set; }

        public DateTime? ModifiedAt { get; set; }

    }
}
