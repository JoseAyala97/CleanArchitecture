using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "7602a591-c8ef-4687-80b3-81c00c1b8530",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "admin@localhost.com",
                    Name = "Jose",
                    LastName = "Ayala",
                    UserName = "JoseAyala",
                    NormalizedUserName = "Jose",
                    PasswordHash = hasher.HashPassword(null, "gato"),
                    EmailConfirmed = true,
                },
                new ApplicationUser
                {
                    Id = "8e6b6dcf-6e01-4ed4-9440-fe778fb72cc9",
                    Email = "luis@localhost2.com",
                    NormalizedEmail = "luis@localhost2.com",
                    Name = "Luis",
                    LastName = "Pinto",
                    UserName = "LuisPinto",
                    NormalizedUserName = "Luis",
                    PasswordHash = hasher.HashPassword(null, "gato1"),
                    EmailConfirmed = true,
                }
                );
        }
    }
}
