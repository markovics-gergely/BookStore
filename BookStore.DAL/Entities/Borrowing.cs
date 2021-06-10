using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Entities
{
    public class Borrowing
    {
        public byte[] RowVersion { get; set; }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Borrowing must have a borrower")]
        public int BorrowerId { get; set; }
        public Member Borrower { get; set; }

        [Required(ErrorMessage = "Borrowing must have a borrowed book")]
        public int BorrowedBookId { get; set; }
        public Book BorrowedBook { get; set; }

        [Required(ErrorMessage = "Borrowing must have a dead line")]
        public DateTime DeadLine { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
