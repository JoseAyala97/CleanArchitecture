using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    { 
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "5ed02801 - 13ef - 4343 - 8e1c - a846908efdf4",
                    UserId = "7602a591-c8ef-4687-80b3-81c00c1b8530"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "74186481 - d066 - 406d - bcc1 - d3154ef10a20",
                    UserId = "8e6b6dcf-6e01-4ed4-9440-fe778fb72cc9"
                });
        }
    }
}
