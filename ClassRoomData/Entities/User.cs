using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRoomData.Entities
{
    public  class User:IdentityUser<Guid>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        
        
        //public string Photo_Url { get; set; }

        public List<UserSchool> UserSchools { get; set; }
        // public UserStatus Status { get; set; }


    }
}
