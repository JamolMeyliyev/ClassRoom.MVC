using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ClassRoom.MVCP.Models
{
    public class CreateUserDto
    {

        [StringLength(30,MinimumLength =3)]
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        
        public string PhoneNumber { get; set; }


        [StringLength(30, MinimumLength = 3)]
        public string Username { get; set; }

        public string Password { get; set; }
        [Required]
        
        public IFormFile? Photo { get; set; }
    }
}
