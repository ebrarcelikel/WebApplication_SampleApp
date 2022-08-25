using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SampleApp.Models.Entities
{
    [Table("Categories")]
    public class Category : EntityLogBase
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(40), Display(Name = "Category Name")]
        public string Name { get; set; }

        [StringLength(160), Display(Name = "Explanation")]
        public string Explanation { get; set; }

        public virtual List<Service> Services { get; set; }
    }
}
