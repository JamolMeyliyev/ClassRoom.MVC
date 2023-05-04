using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ClassRoom.MVCP.Models
{
    public class CreateSchoolModel
    {
        [StringLength(30,MinimumLength = 3)]
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
