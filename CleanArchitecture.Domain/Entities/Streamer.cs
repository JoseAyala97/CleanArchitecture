namespace CleanArchitecture.Domain.Entities
{
    public class Streamer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        //ICollection, se usa porque mas adelante puede transformarse en List o en otra coleccion de datos (relacion uno a muchos)
        public ICollection<Video>? Videos { get; set; }
    }
}
