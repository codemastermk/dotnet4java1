using ConsoleTest.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleTest.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-RQ4JEVV;Database=Test;Integrated Security=true; TrustServerCertificate=true;");
        }
    }
}
