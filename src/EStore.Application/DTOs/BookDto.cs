using EStore.Application.Mappings;
using EStore.Domain.Entities;
using System.ComponentModel;

namespace EStore.Application.DTOs
{
    public class BookDto : IMapFrom<Book>
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public double Advance { get; set; }
        public double Royalty { get; set; }
        public double YtdSales { get; set; }
        public string Notes { get; set; }
        public DateTime? PublishedDate { get; set; }

        [DefaultValue(null)]
        public int? PubId { get; set; }

    }
}
