using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
        new IdentityRole
        {
            Id = "5ed02801-13ef-4343-8e1c-a846908efdf4",
            Name = "Administrador",
            NormalizedName = "ADMINISTRADOR"
        },
        new IdentityRole
        {
            Id = "74186481-d066-406d-bcc1-d3154ef10a20",
            Name = "Cliente",
            NormalizedName = "CLIENTE"
        });
        }
    }
}
