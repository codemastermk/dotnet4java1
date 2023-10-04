using Bookstore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Data
{
    public class BookstoreContext : DbContext
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options) : base(options)
        {
                
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AuthorBooksCount> BooksCount { get; set; }

        public async Task CreateViewAsync()
        {
            await Database.ExecuteSqlInterpolatedAsync(@$"create view View_AuthorBooksCount as select a.Name, Count(b.Id) as BookCount from Authors a inner join Books b on a.Id = b.AuthorId group by a.Name");
        }

        public async Task<List<AuthorBooksCount>> CountAuthorsAsync()
        {
            return await BooksCount.FromSqlInterpolated($@"select * from View_AuthorBooksCount").ToListAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasKey(a => a.Id);
            modelBuilder.Entity<Author>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Author>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<Author>().HasMany(a => a.Books).WithOne(b => b.Author).HasForeignKey(b => b.AuthorId);
            
            modelBuilder.Entity<Book>().HasKey(b => b.Id);
            modelBuilder.Entity<Book>().Property(b => b.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Book>().Property(b => b.Title).IsRequired();
            modelBuilder.Entity<Book>().Property(b => b.ISBN).IsRequired();

            modelBuilder.Entity<Store>().HasKey(s => s.Id);
            modelBuilder.Entity<Store>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Store>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<Store>().Property(a => a.Address).IsRequired();
            modelBuilder.Entity<Store>().HasMany(s => s.Books).WithMany(b => b.Stores).UsingEntity<StoreBook>(
                b => b.HasOne(b => b.Book).WithMany(s => s.StoreBooks).HasForeignKey(s => s.BookId),
                s => s.HasOne(s => s.Store).WithMany(s => s.StoreBooks).HasForeignKey(s => s.StoreId));

            modelBuilder.Entity<AuthorBooksCount>().HasNoKey().ToView("View_AuthorBooksCount");
        }
    }
}
