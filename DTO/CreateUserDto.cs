using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO
{
    public class CreateUserDto
    {
        public long Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] [EmailAddress] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string MobileNo { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public DateTime Dob { get; set; }
        public bool IsEmailOptIn { get; set; }
    }
}
