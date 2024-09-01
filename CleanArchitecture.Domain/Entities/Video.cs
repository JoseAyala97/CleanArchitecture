using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class Video : BaseDomainModel
    {
        public Video() {
            Actors = new HashSet<Actor>();
        }
        public string Name { get; set; }
        public int StreamerId { get; set; }
        public virtual Streamer? Streamer { get; set; }
        public ICollection<Actor>? Actors { get; set; }
        public virtual Director Director { get; set; }
    }
}
