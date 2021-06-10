using BookStore.CLIENT.API;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.CLIENT.Services
{
    public class AuthorService : Service<Author>
    {
        public AuthorService() : base("http://localhost:5000/api/author") { }

        public async Task<ObservableCollection<Author>> GetAllAuthorAsync()
        {
            return await GetAllAsync();
        }

        public async Task<Author> PostAuthorAsync(Author author)
        {
            return await PostAsync(author);
        }

        public async Task PutAuthorAsync(int id, Author author)
        {
            await PutAsync(author, id);
        }

        public async Task DeleteAuthorAsync(int id)
        {
            await DeleteAsync(id);
        }
    }
}
