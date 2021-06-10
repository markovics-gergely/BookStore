using BookStore.CLIENT.API;
using BookStore.CLIENT.Services;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace BookStore.CLIENT.ViewModels.EditViewModels
{
    class BookEditViewModel : ViewModelBase
    {
        public DelegateCommand SubmitCommand { get; }
        public bool Submitable { get; set; } = true;

        public InAppNotification Notification { get; set; }

        public BookEditViewModel()
        {
            SubmitCommand = new DelegateCommand(SubmitBook, CanSubmit);
            subjects = Enum.GetValues(typeof(SubjectType)).Cast<SubjectType>().ToList();
        }
        public List<SubjectType> subjects { get; set; } = new List<SubjectType>();
        public List<SubjectType> selectedSubjects { get; set; } = new List<SubjectType>();

        public ObservableCollection<Book> Books { get; set; } =
            new ObservableCollection<Book>();

        public ObservableCollection<Author> Authors { get; set; } =
            new ObservableCollection<Author>();

        private Book _book = new Book();
        public Book Book
        {
            get
            {
                return _book;
            }
            set
            {
                Set(ref _book, value);
                SubmitCommand.RaiseCanExecuteChanged();
            }
        }
        private Author _selectedAuthor;
        public Author SelectedAuthor
        {
            get
            {
                return _selectedAuthor;
            }
            set
            {
                Set(ref _selectedAuthor, value);
                SubmitCommand.RaiseCanExecuteChanged();
            }
        }
        public void setAuthorById(int id)
        {
            SelectedAuthor = Authors.Where(a => a.Id == id).FirstOrDefault();
        }
        public async void SubmitBook()
        {
            Submitable = false;
            SubmitCommand.RaiseCanExecuteChanged();

            var bookservice = new BookService();
            try
            {
                if(SelectedAuthor != null)
                {
                    Book.AuthorId = SelectedAuthor.Id;
                }

                await bookservice.PutBookAsync(Book.Id, Book);
                Notification.Show(2000);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                Submitable = true;
                SubmitCommand.RaiseCanExecuteChanged();
            }
        }
        public bool CanSubmit()
        {
            if (!Submitable || SelectedAuthor == null || string.IsNullOrEmpty(Book.Title))
                return false;
            return true;
        }

        public async Task loadListAsync()
        {
            var bookservice = new BookService();
            var books = await bookservice.GetAllBookAsync();
            foreach (var b in books)
            {
                Books.Add(b);
            }

            var authorservice = new AuthorService();
            var authors = await authorservice.GetAllAuthorAsync();
            foreach (var a in authors)
            {
                Authors.Add(a);
            }
        }
    }
}
