using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SampleApp.Models.Entities
{
    [Table("OrderedServices")]
    public class OrderedService : EntityLogBase
    {
        [Key]
        public int ID { get; set; }
        public int? UserID { get; set; }
        public int? ServiceID { get; set; }
        public virtual User user { get; set; }
        public virtual Service service { get; set; }
    }
}
