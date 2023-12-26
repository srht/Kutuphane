using KutuphaneCase.Core.Entities;
using KutuphaneCase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneCase.Service
{
    public class BooksService : IBooksService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BooksService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Book> CreateBook(Book book)
        {
             await _unitOfWork.Books.CreateAsync(book);
             await _unitOfWork.Save();
            return book;
        }

        public Task DeleteBook(Book book)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var books = _unitOfWork.Books.GetAll().AsEnumerable();
            return books;
        }

        public async Task<Book> GetBookById(Guid id)
        {
            var book = await _unitOfWork.Books.GetAsync(id);
            return book;
        }

        public IEnumerable<Book> GetBooksWithTitle(string title)
        {
            var foundBooks = _unitOfWork.Books.GetAll().Where(i => i.Title.Contains(title));
            return foundBooks;
        }

        public async Task UpdateBook(Book bookToBeUpdated)
        {
            await _unitOfWork.Books.UpdateAsync(bookToBeUpdated.Id, bookToBeUpdated);
        }
    }
}
