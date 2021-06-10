using BookStore.CLIENT.API;
using BookStore.CLIENT.Services;
using BookStore.CLIENT.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace BookStore.CLIENT.ViewModels
{
    public class BookListPageViewModel : ViewModelBase
    {
        public ObservableCollection<Book> Books { get; set; } =
            new ObservableCollection<Book>();
        public ObservableCollection<string> subjects { get; set; } =
            new ObservableCollection<string>();
        public BookListPageViewModel()
        {
        }

        public async Task loadListAsync()
        {
            var bookservice = new BookService();
            var books = await bookservice.GetAllBookAsync();
            foreach (var b in books)
            {
                Books.Add(b);
                subjects.Add(string.Join(", ", b.Subjects));
            }
        }
    }
}

