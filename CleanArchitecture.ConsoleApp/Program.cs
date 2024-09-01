
using CleanArchitecture.Data;
using CleanArchitecture.Domain;

//instancia de dbContext
StreamerDbContext dbContext = new();
//objeto directo de dbcontext -> permitira insertar en la tabla Streamer
Streamer streamer = new Streamer()
{
    Name = "Amazon Prime",
    Url = "https://www.amazonprime.com"
};

//es signo de admiracion me permite informar que el objeto existe
dbContext!.Streamers!.Add(streamer);
//para guardar la insercion
await dbContext.SaveChangesAsync();

var movies = new List<Video>
{
    new Video
    {
        Name = "Mad Max",
        StreamerId = 1
    },
    new Video
    {
        Name = "Accidente",
        StreamerId = streamer.Id
    },
        new Video
    {
        Name = "Crepusculo",
        StreamerId = streamer.Id
    },
            new Video
    {
        Name = "Citizen Kane",
        StreamerId = streamer.Id
    }
};

//para guardar un cambio en una lista un arreglo
await dbContext.AddRangeAsync(movies);
//para guardar en el contexto
await dbContext.SaveChangesAsync();