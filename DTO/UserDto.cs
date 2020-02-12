using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTO
{
    public class UserDto
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Dob { get; set; }
        public bool IsEmailOptIn { get; set; }
    }
}
