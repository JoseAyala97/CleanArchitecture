
using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

//instancia de dbContext
StreamerDbContext dbContext = new();

//llamado al metodo de insercion
//await AddNewRecords();
//llamado a metodos que realizan consulta
//QueryStreaming();
//QueryVideo();
//await QueryFilter();
//await QueryMethods();
await QueryLinq();

Console.WriteLine("presione cualquier tecla para terminar el programa");
//Para cerrar la consola con cualquier tecla
Console.ReadKey();


//sintaxis de LinQ
async Task QueryLinq()
{
    var stream = dbContext!.Streamers!;
    Console.WriteLine($"Ingrese el servicio de streaming");
    var streamerName = Console.ReadLine();
    //Sintaxis  linQ i representa la data - columnas de la entidad (select informa lo que se quiere retornar en el ejemplo todo lo que representa i)
    var streamers = await (from i in stream
                           where EF.Functions.Like(i.Name, $"%{streamerName}%")
                           select i).
                           ToListAsync();
    foreach(var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Name}");
    }
}
async Task QueryFilter()
{
    Console.WriteLine($"Ingrese una compania de streaming");
    //recibir dato por consola
    var streamingName = Console.ReadLine();
    //expresion lamda condicionara lso resultados (x => x.Name == streamingName.ToListAsync();
    //tambien se puede realizar usando sintaxis de .Net y Entity Equals compara dos strings
    var streamers = await dbContext!.Streamers!.Where(x => x.Name.Equals(streamingName)).ToListAsync();
    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Name}");
    }
    //sintaxis de .Net y Entity Equals compara dos strings - a diferencia de Equals Contains no compara valores exactos, sino que trae aproximacion
    //var streamerPartialResults = await dbContext!.Streamers!.Where(x => x.Name.Contains(streamingName)).ToListAsync();
    //tambien se puede realizar con otra estructura de entity 
    var streamerPartialResults = await dbContext!.Streamers!.Where(x => EF.Functions.Like(x.Name, $"%{streamingName}%")).ToListAsync();
    foreach (var streamer in streamerPartialResults)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Name}");
    }

}
async Task QueryMethods()
{
    //factorizacion para minimizar las lineas de coidog
    var stream = dbContext!.Streamers!;
    //FirstAsync() asume que existe la data, en caso de que no exista, no tendra ningun resultado y dispara exception o error, detiene el programa
    var firstAsync = await stream.Where(y => y.Name.Contains("a")).FirstAsync();
    //FirstOrDefaultAsync() sino encuentra nada, retorna un valor por defecto en null, no detiene el programa
    var firstOrDefaultAsync = await stream.Where(y => y.Name.Contains("a")).FirstOrDefaultAsync();
    //al eliminar el where y reemplazar por FirstOrDefaultAsync() con expresion lambda
    var strefirstOrDefaultAsyncSinWhere = await stream.FirstOrDefaultAsync(y => y.Name.Contains("a"));
    //resultado debe ser un unico valor,  si el resultado tiene mas de un valor el resultado sera una exception
    var singleAsync = await stream.Where(y => y.Id == 1).SingleAsync();
    //no retorna exception, retorna valor null o valor que corresponde
    var singleOrDefaultAsync = await stream.Where(y => y.Id == 1).SingleOrDefaultAsync();

    var resultado = await stream.FindAsync(3);

    //var count = await stream.CountAsync();
    //var longAccount = await stream.LongCountAsync();
    //var min = await stream.MinAsync(2);
    //var max = await stream.MaxAsync(1);


}
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