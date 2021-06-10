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
    public class AuthorServiceTest
    {
        private static int DEFAULT_AUTHOR_COUNT = 3;

        private static IMapper _mapper;

        public AuthorServiceTest()
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
        public async void Authors_AddAuthor_CountIncrements()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var newAuthor = new Author()
            {
                Id = 1000,
                Name = "TestAuthor"
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
                    var service = new AuthorService(context, _mapper);
                    await service.InsertAuthorAsync(newAuthor);
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new NorthwindContext(options))
                {
                    Assert.Equal(DEFAULT_AUTHOR_COUNT + 1, context.Authors.Count());
                    Assert.NotNull(context.Authors.Find(1000));
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async void Authors_EditAuthor_Changes()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var editedAuthor = new Author()
            {
                RowVersion = new byte[] { 1 },
                Id = 1,
                Name = "TestAuthor1"
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
                    var service = new AuthorService(context, _mapper);
                    await service.UpdateAuthorAsync(1, editedAuthor);
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new NorthwindContext(options))
                {
                    Assert.Equal(DEFAULT_AUTHOR_COUNT, context.Authors.Count());
                    Assert.NotNull(context.Authors.Find(1));
                    Assert.Equal("TestAuthor1", context.Authors.Find(1).Name);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async void Authors_DeleteAuthor_CountDecrements()
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
                    var service = new AuthorService(context, _mapper);
                    await service.DeleteAuthorAsync(1);
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new NorthwindContext(options))
                {
                    Assert.Equal(DEFAULT_AUTHOR_COUNT - 1, context.Authors.Count());
                    Assert.Null(context.Authors.Find(1));
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async void Authors_GetAllAuthor_DefaultValue()
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

                int authorCount = 0;
                // Run the test against one instance of the context
                using (var context = new NorthwindContext(options))
                {
                    var service = new AuthorService(context, _mapper);
                    var authors = await service.GetAuthorsAsync();
                    authorCount = authors.Count();
                }

                Assert.Equal(DEFAULT_AUTHOR_COUNT, authorCount);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
