using BookStore.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Interfaces
{
    public interface IAuthorService
    {
        Task<Author> GetAuthorAsync(int authorId);
        Task<IEnumerable<Author>> GetAuthorsByNameAsync(string authorName);
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<Author> InsertAuthorAsync(Author newAuthor);
        Task UpdateAuthorAsync(int authorId, Author updatedAuthor);
        Task DeleteAuthorAsync(int authorId);
    }
}
