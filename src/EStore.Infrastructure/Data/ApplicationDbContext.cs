using EStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EStore.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Author> Authors => Set<Author>();
        public virtual DbSet<Book> Books => Set<Book>();
        public virtual DbSet<BookAuthor> BookAuthors => Set<BookAuthor>();
        public virtual DbSet<Publisher> Publishers => Set<Publisher>();
        public virtual DbSet<Role> Roles => Set<Role>();
        public virtual DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.HasKey(e => new { e.AuthorId, e.BookId });
            });

            modelBuilder.Entity<Publisher>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Publisher)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Role)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasOne(e => e.Publisher)
                .WithMany(e => e.Books)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
