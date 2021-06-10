using BookStore.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Interfaces
{
    public interface IBookService
    {
        Task<Book> GetBookAsync(int bookId);
        Task<IEnumerable<Book>> GetBooksByTitleAsync(string bookTitle);
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> InsertBookAsync(Book newBook);
        Task UpdateBookAsync(int bookId, Book updatedBook);
        Task DeleteBookAsync(int bookId);
    }
}
