using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication_SampleApp.Models.Entities;

[Table("EmailMemmberships")]
public class EmailMembership : EntityLogBase
{
    [Key]
    public int ID { get; set; }

    [Required,EmailAddress, StringLength(100), Display(Name =("E-mail Address"))]
    public string EmailAddress { get; set; }
}