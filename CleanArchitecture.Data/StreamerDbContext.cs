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
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-S18FHFS;Database=Streamer;Integrated Security=True;TrustServerCertificate=True")
                //Para imprimir todas las consultas por consola
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
                //permite describir cada una de las operaciones
                .EnableSensitiveDataLogging();
        }
        public DbSet<Streamer>? Streamers {  get; set; } 
        public DbSet<Video>? Videos { get; set; }
    }
}
