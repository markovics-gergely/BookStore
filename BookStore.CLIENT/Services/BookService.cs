using BookStore.CLIENT.API;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookStore.CLIENT.Services
{
    public class BookService : Service<Book>
    {
        public BookService() : base("http://localhost:5000/api/book") { }

        public async Task<ObservableCollection<Book>> GetAllBookAsync()
        {
            return await GetAllAsync();
        }

        public async Task<Book> PostBookAsync(Book book)
        {
            return await PostAsync(book);
        }

        public async Task PutBookAsync(int id, Book book)
        {
            await PutAsync(book, id);
        }

        public async Task DeleteBookAsync(int id)
        {
            await DeleteAsync(id);
        }
    }
}
