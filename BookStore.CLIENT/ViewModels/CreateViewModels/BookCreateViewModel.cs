using BookStore.CLIENT.API;
using BookStore.CLIENT.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace BookStore.CLIENT.ViewModels.CreateViewModels
{
    class BookCreateViewModel : ViewModelBase
    {
        public DelegateCommand SubmitCommand { get; }
        public bool Submitable { get; set; } = true;
        public List<SubjectType> subjects { get; set; } = new List<SubjectType>();
        private ObservableCollection<SubjectType> _selectedSubjects = new ObservableCollection<SubjectType>();
        public ObservableCollection<SubjectType> selectedSubjects
        {
            get
            {
                return _selectedSubjects;
            }
            set
            {
                Set(ref _selectedSubjects, value);
                SubmitCommand.RaiseCanExecuteChanged();
            }
        }
        private string _publishYear = "";
        public string publishYear
        {
            get
            {
                return _publishYear;
            }
            set
            {
                Set(ref _publishYear, value);
                SubmitCommand.RaiseCanExecuteChanged();
            }
        }
        public BookCreateViewModel()
        {
            SubmitCommand = new DelegateCommand(SubmitBook, CanSubmit);
            subjects = Enum.GetValues(typeof(SubjectType)).Cast<SubjectType>().ToList();
        }

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

        public async void SubmitBook()
        {
            Submitable = false;
            SubmitCommand.RaiseCanExecuteChanged();

            var bookservice = new BookService();
            try
            {
                if(Book.Author != null)
                {
                    Book.AuthorId = Book.Author.Id;
                    Book.Author = null;
                }
                Book.RowVersion = new byte[] { 1 };
                Book.Subjects = selectedSubjects;
                Book.PublishedYear = int.Parse(publishYear);
                var book = await bookservice.PostBookAsync(Book);
                Book = new Book();
            }
            catch(Exception e)
            {
                Book = new Book();
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
            Debug.WriteLine(Book?.Isbn?.Length);
            int year;
            if (!Submitable || Book.Author == null || string.IsNullOrEmpty(Book.Title) || string.IsNullOrEmpty(Book.Isbn) || Book.Isbn.Length < 10
                || !int.TryParse(publishYear, out year))
                return false;
            return true;
        }

        public async Task loadListAsync()
        {
            var authorservice = new AuthorService();
            try
            {
                var authors = await authorservice.GetAllAuthorAsync();
                foreach (var a in authors)
                {
                    Authors.Add(a);
                }
            }
            catch(Exception e)
            {
                Authors.Clear();
                Debug.WriteLine(e.Message);
            }
        }
    }
}
