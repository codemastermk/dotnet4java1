using Bookstore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetBooks();
    }
}
