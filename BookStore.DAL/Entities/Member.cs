using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Entities
{
    public class Member
    {
        public byte[] RowVersion { get; set; }

        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Member must have a name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Range(1000, 9999)]
        public int? ZipCode { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? BirthDay { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public ICollection<Borrowing> BorrowedBooks { get; set; } = new List<Borrowing>();
    }
}
