using ClassRoomData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRoomData.Context.Configurations
{
    public class UserSchoolConfigurations:IEntityTypeConfiguration<UserSchool>
    {
        public void Configure(EntityTypeBuilder<UserSchool> builder)
        {
            builder.ToTable("user_schools");

            builder.HasKey(userSchool => new
            {
                userSchool.UserId,
                userSchool.SchoolId
            });
            builder
                .HasOne(userSchool => userSchool.User)
                .WithMany(user => user.UserSchools)
                .HasForeignKey(userSchool => userSchool.UserId);

            builder.
                HasOne(userSchool => userSchool.School)
                .WithMany(user => user.UserSchools)
                .HasForeignKey(userSchool => userSchool.SchoolId);
        }
    }
}
