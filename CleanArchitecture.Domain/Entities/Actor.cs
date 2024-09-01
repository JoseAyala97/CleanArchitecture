using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class Actor : BaseDomainModel
    {
        //inicializar la lista de videos en Actor
        public Actor() {
        Videos = new HashSet<Video>();
        }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public ICollection<Video>? Videos { get; set; }
    }
}
