using Bookstore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookstoreContext _context;

        private readonly AuthorsRepository _authorsRepository;

        private readonly BookRepository _bookRepository;

        public UnitOfWork(BookstoreContext context)
        {
                _context = context;
        }

        public IBookRepository BookRepository => _bookRepository ?? new BookRepository(_context);

        public IAuthorsRepository AuthorsRepository => _authorsRepository ?? new AuthorsRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
