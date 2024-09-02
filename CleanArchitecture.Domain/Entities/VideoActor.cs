using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class VideoActor : BaseDomainModel
    {
        public int VideoId { get; set; }
        public int ActorId { get; set; }
    }
}
