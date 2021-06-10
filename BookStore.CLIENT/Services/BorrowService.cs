using BookStore.CLIENT.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.CLIENT.Services
{
    public class BorrowService : Service<Borrowing>
    {
        public BorrowService() : base("http://localhost:5000/api/borrow") { }

        public async Task<ObservableCollection<Borrowing>> GetAllBorrowAsync()
        {
            return await GetAllAsync();
        }

        public async Task<Borrowing> PostBorrowAsync(Borrowing borrow)
        {
            return await PostAsync(borrow);
        }

        public async Task PutBorrowAsync(int id, Borrowing borrow)
        {
            await PutAsync(borrow, id);
        }

        public async Task DeleteBorrowAsync(int id)
        {
            await DeleteAsync(id);
        }
    }
}
