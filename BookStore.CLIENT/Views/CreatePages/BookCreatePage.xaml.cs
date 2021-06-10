using BookStore.CLIENT.API;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace BookStore.CLIENT.Views
{
    public sealed partial class BookCreatePage : Page
    {
        public BookCreatePage()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.loadListAsync();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SubmitCommand.RaiseCanExecuteChanged();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.SubmitCommand.RaiseCanExecuteChanged();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.RemovedItems.Count > 0) ViewModel.selectedSubjects.Remove((SubjectType)e.RemovedItems[0]);
            if (e.AddedItems.Count > 0) ViewModel.selectedSubjects.Add((SubjectType)e.AddedItems[0]);
            foreach (var s in ViewModel.selectedSubjects)
            {
                Debug.WriteLine(s);
            }
        }
    }
}