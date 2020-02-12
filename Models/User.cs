using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class User
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public byte[] Password { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Dob { get; set; }
        public bool IsEmailOptIn { get; set; }
    }
}