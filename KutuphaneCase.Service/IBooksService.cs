using KutuphaneCase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneCase.Service
{
    public interface IBooksService
    {
        Task<Book> CreateBook(Book book);
        Task DeleteBook(Book book);
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookById(Guid id);
        IEnumerable<Book> GetBooksWithTitle(string title);
        Task UpdateBook(Book bookToBeUpdated);
    }
}
