using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassRoomData.Entities
{
    [Table("schools")]
    public  class School
    {
        
        [Column("id")]
        
        public Guid Id { get; set; }

        [Column("name")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        public List<UserSchool> UserSchools { get; set; }
    }
}
