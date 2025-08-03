using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AidCare.Entities
{
    public class BloodGlucose : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(0, 500, ErrorMessage = "Kan şekeri değeri 0-500 arasında olmalıdır")]
        public decimal GlucoseValue { get; set; }

        [Required]
        public DateTime MeasurementDate { get; set; }

        [StringLength(200)]
        public string? Notes { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; } = null!;
    }
}