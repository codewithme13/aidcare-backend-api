using System.ComponentModel.DataAnnotations;

namespace AidCare.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
        public string TcKimlikNo { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [StringLength(11)]
        public string? PhoneNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        //Kullanıcının kan şekeri kayıtları
        public virtual ICollection<BloodGlucose> BloodGlucoseRecords { get; set; } = new List<BloodGlucose>();

        public string FullName => $"{FirstName} {LastName}";
    }
}