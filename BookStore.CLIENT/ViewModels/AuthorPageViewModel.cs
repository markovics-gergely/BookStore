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
    public class AuthorPageViewModel : ViewModelBase
    {
        public ObservableCollection<Author> Authors { get; set; } =
            new ObservableCollection<Author>();

        public AuthorPageViewModel()
        {
        }

        public async Task loadListAsync()
        {
            var authorservice = new AuthorService();
            var authors = await authorservice.GetAllAuthorAsync() ?? new ObservableCollection<Author>();
            foreach (var a in authors)
            {
                Authors.Add(a);
            }
        }
    }
}

