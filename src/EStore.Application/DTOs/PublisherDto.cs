using EStore.Application.Mappings;
using EStore.Domain.Entities;

namespace EStore.Application.DTOs
{
    public class PublisherDto : IMapFrom<Publisher>
    {
        public int PubId { get; set; }
        public string PublisherName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
