using CleanArchitecture.Domain.Entities;
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
        //Fluent API (Se recomienda cuando no se defina las claves foraneas en los modelso)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //entidad que se desea evaluar
            modelBuilder.Entity<Streamer>()
                //muchos videos (muchas instancias de videos)
                .HasMany(m => m.Videos)
                //un streamer (una instancia de streamer)
                .WithOne(m => m.Streamer)
                //llave foranea
                .HasForeignKey(m => m.StreamerId)
                //si puede ser nulleable
                .IsRequired()
                //eliminacion en cascada
                .OnDelete(DeleteBehavior.Restrict);
                
        }
        public DbSet<Streamer>? Streamers {  get; set; } 
        public DbSet<Video>? Videos { get; set; }
    }
}
