using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication_SampleApp.Models.Entities;

namespace WebApplication_SampleApp.Models.Entities
{
    [Table("Services")]
    public class Service : EntityLogBase
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(60), Display(Name = "Name")]
        public string Name { get; set; }

        [Required, StringLength(230), Display(Name = "Explanation")]
        public string Explanation { get; set; }

        [Required,  Display(Name = "Price")]
        public int Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        public int OwnerID { get; set; }
        public virtual User Owner { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<Order> Orders { get; set; }
        public virtual List<OrderedService> OrderedServices { get; set; }


    }




}



