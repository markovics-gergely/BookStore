using AutoMapper;
using BookStore.BLL.DTO;
using BookStore.BLL.Services;
using BookStore.DAL;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace BookStore.TEST.Services
{
    public class BookServiceTest
    {
        private static int DEFAULT_BOOK_COUNT = 8;

        private static IMapper _mapper;

        public BookServiceTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new BookStoreProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async void Books_AddBook_CountIncrements()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var newBook = new Book()
            {
                Id = 1000,
                ISBN = "1010101010",
                Title = "Test1",
                AuthorId = 1
            };

            try
            {
                var options = new DbContextOptionsBuilder<NorthwindContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new NorthwindContext(options))
                {
                    //EnsureCreated(context);
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new NorthwindContext(options))
                {
                    var service = new BookService(context, _mapper);
                    await service.InsertBookAsync(newBook);
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new NorthwindContext(options))
                {
                    Assert.Equal(DEFAULT_BOOK_COUNT + 1, context.Books.Count());
                    Assert.NotNull(context.Books.Find(1000));
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async void Books_EditBook_Changes()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var editedBook = new Book()
            {
                Id = 1,
                ISBN = "",
                Title = "TestTitle",
                AuthorId = 1
            };

            try
            {
                var options = new DbContextOptionsBuilder<NorthwindContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new NorthwindContext(options))
                {
                    //EnsureCreated(context);
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new NorthwindContext(options))
                {
                    var service = new BookService(context, _mapper);
                    await service.UpdateBookAsync(1, editedBook);
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new NorthwindContext(options))
                {
                    Assert.Equal(DEFAULT_BOOK_COUNT, context.Books.Count());
                    Assert.NotNull(context.Books.Find(1));
                    Assert.Equal("TestTitle", context.Books.Find(1).Title);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async void Books_DeleteBook_CountDecrements()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<NorthwindContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new NorthwindContext(options))
                {
                    //EnsureCreated(context);
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new NorthwindContext(options))
                {
                    var service = new BookService(context, _mapper);
                    await service.DeleteBookAsync(1);
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new NorthwindContext(options))
                {
                    Assert.Equal(DEFAULT_BOOK_COUNT - 1, context.Books.Count());
                    Assert.Null(context.Books.Find(1));
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async void Books_GetAllBook_DefaultValue()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<NorthwindContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new NorthwindContext(options))
                {
                    context.Database.EnsureCreated();
                }

                int bookCount = 0;
                // Run the test against one instance of the context
                using (var context = new NorthwindContext(options))
                {
                    var service = new BookService(context, _mapper);
                    var books = await service.GetBooksAsync();
                    bookCount = books.Count();
                }

                Assert.Equal(DEFAULT_BOOK_COUNT, bookCount);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
