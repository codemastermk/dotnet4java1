using ConsoleTest.Data;
using ConsoleTest.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // var context = new DataContext();

            // Read
            // var result = context.Blogs.OrderBy(b => b.Url).ToList();
            // var t = 0;
            // Create
            // Blog blog = new Blog { Url = "https://petar.com.mk" };
            // context.Add(blog);
            // context.SaveChanges();

            // Update
            // var blog = context.Blogs.First(b => b.Id == 1);
            // context.Posts.Add(new Post { Title = "test post 1", Content = "jkhjkhjkhjksf", Blog = blog });
            // context.SaveChanges();

            // Delete
            // var blog = context.Blogs.Find(1);
            // context.Remove(blog);
            // context.SaveChanges();
        }
    }
}