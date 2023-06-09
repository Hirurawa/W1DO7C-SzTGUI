using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CarShop.Models.Entities
{
  [Table("Cars")]
  public class Car
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int Price { get; set; }

    [Required]
    [MaxLength(50)]
    public string Model { get; set; }

    public int BrandId { get; set; }

    [JsonIgnore]
    [NotMapped]
    public virtual Brand Brand { get; set; }
  }
}
