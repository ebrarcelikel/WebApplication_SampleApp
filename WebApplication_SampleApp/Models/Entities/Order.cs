using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication_SampleApp.Models.Entities;

namespace WebApplication_SampleApp.Models.Entities
{
    [Table("Orders")]

    public class Order : EntityLogBase
    {
        [Key]
        public int ID { get; set; }

        public int NumOfService { get; set; }

        [Display(Name ="Orderer")]
        public int? UserID { get; set; }
        public int? ServiceID { get; set; }

        public virtual User User { get; set; }
        public virtual Service Service { get; set; }



    }

}

