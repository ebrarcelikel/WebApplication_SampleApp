using System.ComponentModel.DataAnnotations;

namespace WebApplication_SampleApp.Models
{
	public class ServiceViewModel{
        [Required, StringLength(60), Display(Name = "Name")]
    public string Name { get; set; }

    [Required, StringLength(230), Display(Name = "Explanation")]
    public string Explanation { get; set; }

    [Required, Display(Name = "Price")]
    public int Price { get; set; }

    [Display(Name = "Category")]
    public int CategoryID { get; set; } }
}
