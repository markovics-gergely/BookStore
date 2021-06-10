using BookStore.DAL.Conventions;
using BookStore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BookStore.DAL
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;initial catalog=BookStore",
                    options => options.EnableRetryOnFailure())
                    .LogTo(m => Debug.WriteLine(m), LogLevel.Information);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Member>()
                .Property(m => m.Name)
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<Borrowing>()
                .Property(b => b.DeadLine)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .HasMaxLength(50)
                .IsRequired();

            var converter = new ValueConverter<ICollection<SubjectType>, string>(
                    v => string.Join(", ", v.ToArray()) ?? "",
                    v => v.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(i => (SubjectType)Enum.Parse(typeof(SubjectType), i)).ToList() ?? new List<SubjectType>()
                );
            var valueComparer = new ValueComparer<ICollection<SubjectType>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => (ICollection<SubjectType>)c.ToHashSet());

            modelBuilder.Entity<Book>()
                .Property(b => b.Subjects)
                .HasConversion(converter)
                .Metadata.SetValueComparer(valueComparer);

            modelBuilder.Entity<Book>()
                .HasAlternateKey(b => b.ISBN);


            modelBuilder.Entity<Author>().HasData(
                    new Author { Id = 1, Name = "Fjodor Mihajlovics Dosztojevszkij", 
                                 BirthDate = new DateTime(1821, 11, 11), BirthPlace = "Moszkva" },
                    new Author { Id = 2, Name = "George Orwell", 
                                 BirthDate = new DateTime(1903, 06, 25), BirthPlace = "Motihari" },
                    new Author
                    {
                        Id = 3,
                        Name = "Mary Shelley",
                        BirthDate = new DateTime(1797, 08, 30),
                        BirthPlace = "Sommers Town"
                    }
                );

            modelBuilder.Entity<Member>().HasData(
                    new Member() { Id = 1, Name = "Teszt Elek", ZipCode = 1011, BirthDay = new DateTime(1983, 10, 2) },
                    new Member() { Id = 2, Name = "Bádog Béla", PhoneNumber = "36203459876" }
                );

            modelBuilder.Entity<Book>().HasData(
                    new Book() { Id = 1, Title = "Bűn és bűnhődés", AuthorId = 1,
                                 PublishedYear = 1866, ISBN = "ISBN 963-07-0048-4",
                        Subjects = new List<SubjectType>() {
                            SubjectType.Romance
                        }
                    },
                    new Book() { Id = 2, Title = "A Karamazov testvérek", AuthorId = 1,
                        PublishedYear = 1880,
                        ISBN = "ISBN 963 07 1282 2",
                        Subjects = new List<SubjectType>() { 
                            SubjectType.Scifi, SubjectType.Action
                        }
                    },
                    new Book() { Id = 3, Title = "A félkegyelmű", AuthorId = 1,
                        PublishedYear = 1869,
                        ISBN = "ISBN 9630720876",
                        Subjects = new List<SubjectType>() {
                            SubjectType.Drama, SubjectType.Mystery
                        }
                    },
                    new Book() { Id = 4, Title = "1984", AuthorId = 2,
                        PublishedYear = 1949,
                        ISBN = "ISBN 963-07-4815-0",
                        Subjects = new List<SubjectType>() {
                            SubjectType.Crime
                        }
                    },
                    new Book() { Id = 5, Title = "Állatfarm", AuthorId = 2,
                        PublishedYear = 1945,
                        ISBN = "ISBN 9630749467",
                        Subjects = new List<SubjectType>() {
                            SubjectType.Children, SubjectType.Romance
                        }
                    },
                    new Book() { Id = 6, Title = "Légszomj", AuthorId = 2,
                        PublishedYear = 1939,
                        ISBN = "ISBN 9630773201",
                        Subjects = new List<SubjectType>() {
                            SubjectType.Thriller, SubjectType.Scifi, SubjectType.Classic
                        }
                    },
                    new Book() { Id = 7, Title = "Frankenstein", AuthorId = 3,
                        PublishedYear = 1818,
                        ISBN = "ISBN 9780008325923",
                        Subjects = new List<SubjectType>() {
                            SubjectType.Fantasy, SubjectType.Horror, SubjectType.Drama
                        }
                    },
                    new Book() { Id = 8, Title = "Mathilda", AuthorId = 3,
                        PublishedYear = 1859,
                        ISBN = " ISBN 9789633440414",
                        Subjects = new List<SubjectType>() {
                            SubjectType.Action, SubjectType.Crime
                        }
                    }
                );

            modelBuilder.Entity<Borrowing>().HasData(
                    new Borrowing() { Id = 1, BorrowerId = 1, BorrowedBookId = 1,
                                      DeadLine = new DateTime(2021, 05, 20)},
                    new Borrowing()
                    {
                        Id = 2,
                        BorrowerId = 1,
                        BorrowedBookId = 2,
                        DeadLine = new DateTime(2021, 05, 23)
                    },
                    new Borrowing()
                    {
                        Id = 3,
                        BorrowerId = 2,
                        BorrowedBookId = 3,
                        DeadLine = new DateTime(2021, 05, 17),
                        ReturnDate = new DateTime(2021, 05, 07)
                    },
                    new Borrowing()
                    {
                        Id = 4,
                        BorrowerId = 2,
                        BorrowedBookId = 5,
                        DeadLine = new DateTime(2021, 05, 06),
                        ReturnDate = new DateTime(2021, 04, 29)
                    },
                    new Borrowing()
                    {
                        Id = 5,
                        BorrowerId = 1,
                        BorrowedBookId = 7,
                        DeadLine = new DateTime(2021, 05, 27)
                    }
                );

            modelBuilder.Entity<Member>()
                .HasMany(m => m.BorrowedBooks)
                .WithOne(b => b.Borrower);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Borrows)
                .WithOne(b => b.BorrowedBook);

            modelBuilder.Entity<Book>()
                .Property(p => p.RowVersion)
                .IsRowVersion();
            modelBuilder.Entity<Member>()
                .Property(p => p.RowVersion)
                .IsRowVersion();
            modelBuilder.Entity<Author>()
                .Property(p => p.RowVersion)
                .IsRowVersion();
            modelBuilder.Entity<Borrowing>()
                .Property(p => p.RowVersion)
                .IsRowVersion();
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrowing> Borrows { get; set; }
    }
}
