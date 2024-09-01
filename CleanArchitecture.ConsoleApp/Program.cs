
using CleanArchitecture.Data;
using CleanArchitecture.Domain;

//instancia de dbContext
StreamerDbContext dbContext = new();

//llamado al metodo de insercion
//await AddNewRecords();
//llamado a metodos que realizan consulta
QueryStreaming();
QueryVideo();


//Para retornar la lista de el objeto que se le pase
void QueryStreaming()
{
    var streamers = dbContext.Streamers!.ToList();
    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Name}");
    }
}
//Para retornar la lista de el objeto que se le pase
void QueryVideo()
{
    var videos = dbContext.Videos!.ToList();
    foreach (var video in videos)
    {
        Console.WriteLine($"{video.Id} - {video.Name} - {video.StreamerId}");
    }
}
//Metodo que servira para realizar la insercion
async Task AddNewRecords()
{
    //objeto directo de dbcontext -> permitira insertar en la tabla Streamer
    Streamer streamer = new Streamer()
    {
        Name = "Disney",
        Url = "https://www.disney.com"
    };

    //es signo de admiracion me permite informar que el objeto existe
    dbContext!.Streamers!.Add(streamer);
    //para guardar la insercion
    await dbContext.SaveChangesAsync();

    var movies = new List<Video>
{
    new Video
    {
        Name = "Mickey Mouse",
        StreamerId = streamer.Id
    },
    new Video
    {
        Name = "La Cenicienta",
        StreamerId = streamer.Id
    },
        new Video
    {
        Name = "101 Dalmatas",
        StreamerId = streamer.Id
    },
            new Video
    {
        Name = "Los Increibles",
        StreamerId = streamer.Id
    }
};

    //para guardar un cambio en una lista un arreglo
    await dbContext.AddRangeAsync(movies);
    //para guardar en el contexto
    await dbContext.SaveChangesAsync();
}