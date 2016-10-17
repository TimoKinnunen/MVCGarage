using System.ComponentModel.DataAnnotations;

namespace MVCGarage.Models
{
    public class VehicleType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
    }
}