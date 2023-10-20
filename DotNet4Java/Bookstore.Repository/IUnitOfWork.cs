using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Repository
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }

        IAuthorsRepository AuthorsRepository { get; }
        Task CommitAsync();
    }
}
