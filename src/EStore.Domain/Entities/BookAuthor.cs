namespace EStore.Domain.Entities
{
    public class BookAuthor
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public int AuthorOrder { get; set; }
        public float RoyalityPercentage { get; set; }
        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
