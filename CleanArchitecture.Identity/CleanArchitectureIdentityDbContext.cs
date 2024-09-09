using CleanArchitecture.Identity.Configurations;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Identity
{
    //ApplicationUser clase usuario que representara en el contexto
    public class CleanArchitectureIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        //cadena de conexion se inyecte en la conexion
        public CleanArchitectureIdentityDbContext(DbContextOptions<CleanArchitectureIdentityDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());

        }
    }
}
