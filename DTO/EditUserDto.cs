using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO
{
    public class EditUserDto
    {
        [Required] public string Name { get; set; }
        [Required] public string MobileNo { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public DateTime Dob { get; set; }
        public bool IsEmailOptIn { get; set; }
    }
}
