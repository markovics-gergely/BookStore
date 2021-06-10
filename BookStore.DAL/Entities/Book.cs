using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Entities
{
    public class Book
    {
        public byte[] RowVersion { get; set; }

        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Book must have a title")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Book must have an author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        [Required(ErrorMessage = "Book must have an ISBN number")]
        [RegularExpression(@"^(?:ISBN(?:-13)?:?\ )?(?=[0-9]{13}$|(?=(?:[0-9]+[-\ ]){4})[-\ 0-9]{17}$)97[89][-\ ]?[0-9]{1,5}[-\ ]?[0-9]+[-\ ]?[0-9]+[-\ ]?[0-9]$",
            ErrorMessage = "ISBN must be in the specific ISBN format")]
        public string ISBN { get; set; }
        public int? PublishedYear { get; set; }

        public ICollection<Borrowing> Borrows { get; set; } = new List<Borrowing>();

        public ICollection<SubjectType> Subjects { get; set; } = new List<SubjectType>();
    }
}
