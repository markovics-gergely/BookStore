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
    public class BorrowPageViewModel : ViewModelBase
    {
        public ObservableCollection<Borrowing> Borrows { get; set; } =
            new ObservableCollection<Borrowing>();

        public BorrowPageViewModel()
        {
        }

        public async Task loadListAsync()
        {
            var borrowservice = new BorrowService();
            var borrows = await borrowservice.GetAllBorrowAsync();
            foreach (var b in borrows)
            {
                Borrows.Add(b);
            }
        }
    }
}

