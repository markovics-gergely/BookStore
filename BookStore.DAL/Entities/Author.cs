using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Entities
{
    public class Author
    {
        public byte[] RowVersion { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthPlace { get; set; }
        public DateTime? BirthDate { get; set; }

        public ICollection<Book> Books { get; } = new List<Book>();
    }
}
