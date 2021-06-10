using BookStore.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Interfaces
{
    public interface IBorrowingService
    {
        Task<Borrowing> GetBorrowAsync(int borrowId);
        Task<IEnumerable<Borrowing>> GetBorrowsByMemberNameOrBookTitleAsync(string memberName, string bookTitle);
        Task<IEnumerable<Borrowing>> GetBorrowsActiveAsync();
        Task<IEnumerable<Borrowing>> GetBorrowsAsync();
        Task<Borrowing> InsertBorrowAsync(Borrowing newBorrow);
        Task UpdateBorrowAsync(int borrowId, Borrowing updatedBorrow);
        Task InsertBorrowReturnDateAsync(int borrowId);
        Task DeleteBorrowAsync(int borrowId);
    }
}
