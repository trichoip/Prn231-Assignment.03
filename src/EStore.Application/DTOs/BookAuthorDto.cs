using EStore.Application.Mappings;
using EStore.Domain.Entities;

namespace EStore.Application.DTOs
{
    public class BookAuthorDto : IMapFrom<BookAuthor>
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public int AuthorOrder { get; set; }
        public float RoyalityPercentage { get; set; }
    }
}
