using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class Director : BaseDomainModel
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int VideoId { get; set; }
        public virtual Video? Video { get; set; }
    }
}
