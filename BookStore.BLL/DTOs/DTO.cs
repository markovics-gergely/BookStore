using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookStore.DAL.Entities;

namespace BookStore.BLL.DTO
{
    public record Member
    {
        [Required(ErrorMessage = "RowVersion is required")]
        public byte[] RowVersion { get; set; }

        public int Id { get; init; }
        [Required(ErrorMessage = "Member name is required.", AllowEmptyStrings = false)]
        public string Name { get; init; }

        [Range(1000, 9999)]
        public int? ZipCode { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? BirthDay { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public List<Borrowing> BorrowedBooks { get; init; }
    }

    public record Book
    {
        [Required(ErrorMessage = "RowVersion is required")]
        public byte[] RowVersion { get; set; }

        public int Id { get; init; }
        [Required(ErrorMessage = "Book title is required.", AllowEmptyStrings = false)]
        public string Title { get; init; }

        public int AuthorId { get; init; }
        public Author Author { get; init; }

        public List<Borrowing> Borrows { get; init; }

        [Required(ErrorMessage = "Book must have an ISBN number")]
        [RegularExpression(@"^(?:ISBN(?:-13)?:?\ )?(?=[0-9]{13}$|(?=(?:[0-9]+[-\ ]){4})[-\ 0-9]{17}$)97[89][-\ ]?[0-9]{1,5}[-\ ]?[0-9]+[-\ ]?[0-9]+[-\ ]?[0-9]$",
            ErrorMessage = "ISBN must be in the specific ISBN format")]
        public string ISBN { get; set; }
        public int? PublishedYear { get; set; }

        public ICollection<SubjectType> Subjects { get; set; } = new List<SubjectType>();
    }

    public record Author
    {
        [Required(ErrorMessage = "RowVersion is required")]
        public byte[] RowVersion { get; set; }

        public int Id { get; init; }

        
        [Required(ErrorMessage = "Member name is required.", AllowEmptyStrings = false)]
        /// <example>Thor Author</example>
        public string Name { get; init; }

        public string BirthPlace { get; init; }
        public DateTime BirthDate { get; init; }

        public List<Book> Books { get; init; }
    }

    public record Borrowing
    {
        [Required(ErrorMessage = "RowVersion is required")]
        public byte[] RowVersion { get; set; }

        public int Id { get; init; }

        [Required(ErrorMessage = "Borrowing deadline is required.", AllowEmptyStrings = false)]
        public DateTime DeadLine { get; init; }
        public DateTime? ReturnDate { get; init; }

        public int BorrowedBookId { get; init; }
        public Book BorrowedBook { get; init; }

        public int BorrowerId { get; init; }
        public Member Borrower { get; init; }
    }
}
