using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Data
{
    public class StreamerDbContext : DbContext
    {
        //manejar conexion con base de datos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=localhost; Initial Catalog=Streamer;Integrated Security=False");
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-S18FHFS;Database=Streamer;Integrated Security=True;TrustServerCertificate=True");
        }
        public DbSet<Streamer>? Streamers {  get; set; } 
        public DbSet<Video>? Videos { get; set; }
    }
}
