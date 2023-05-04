using ClassRoomData.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace ClassRoomData.Context
{
    public class AppDbContext:IdentityDbContext<IdentityUser<Guid>,IdentityRole<Guid>,Guid>
    {

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=sql.bsite.net\\MSSQL2016; Database=classroommvc_database; User Id=classroommvc_database; Password=Jamolbek3660; TrustServerCertificate=True");
        }
        public DbSet<School> Schools { get; set; }
        public DbSet<UserSchool> UserSchools { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // builder.ApplyConfiguration(new UserSchoolConfigurations());

               base.OnModelCreating(builder);
            

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            builder.Entity<User>().ToTable("users");

            builder
                .Entity<User>()
                .Property(user => user.Firstname)
                .HasColumnName("fisrtname")
                .HasMaxLength(50).IsRequired(false);

            builder
                .Entity<User>()
                .Property(user => user.Lastname)
                .HasColumnName("lastname")
                .HasMaxLength(50).IsRequired(false);
        }
    }
}
