using CleanArchitecture.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
        {
            if(!context.Streamers!.Any())
            {
                //sino tiene datos, que lso llene con una coleccion de datos
                context.Streamers!.AddRange(GetPreconfiguredStramer());
                
                await context.SaveChangesAsync();
                logger.LogInformation("Estamos insertando nuevos records al db {context}", typeof(StreamerDbContext).Name);
            }
        }

        private static IEnumerable<Streamer> GetPreconfiguredStramer()
        {
            return new List<Streamer>
            {
                new Streamer {CreatedBy = "Jose", Name = "Maxi HBP", Url = "http://www.hbp.com" },
                new Streamer {CreatedBy = "Jose", Name = "Maxi HBP", Url = "http://www.hbp.com" },
            };
        }
    }

}
