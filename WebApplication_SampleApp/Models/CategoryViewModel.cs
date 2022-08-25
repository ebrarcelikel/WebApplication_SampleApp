using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplication_SampleApp.Models
{
    public class CategoryViewModel
	{
        [Required, StringLength(40), Display(Name = "Category Name")]
        public string Name { get; set; }

        [StringLength(160), Display(Name = "Explanation")]
        public string Explanation { get; set; }

    }
}
