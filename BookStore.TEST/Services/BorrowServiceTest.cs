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
    public class BorrowServiceTest
    {
        private static int DEFAULT_BORROWING_COUNT = 5;

        private static IMapper _mapper;

        public BorrowServiceTest()
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
        public async void Borrows_AddBorrow_CountIncrements()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var newBorrow = new Borrowing()
            {
                Id = 1000,
                BorrowedBookId = 1,
                BorrowerId = 1,
                DeadLine = new DateTime(2020, 10, 11)
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
                    var service = new BorrowingService(context, _mapper);
                    await service.InsertBorrowAsync(newBorrow);
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new NorthwindContext(options))
                {
                    Assert.Equal(DEFAULT_BORROWING_COUNT + 1, context.Borrows.Count());
                    Assert.NotNull(context.Borrows.Find(1000));
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async void Borrows_EditBorrow_Changes()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var editedborrow = new Borrowing()
            {
                Id = 1,
                BorrowedBookId = 1,
                BorrowerId = 1,
                DeadLine = new DateTime(2020, 10, 11)
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
                    var service = new BorrowingService(context, _mapper);
                    await service.UpdateBorrowAsync(1, editedborrow);
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new NorthwindContext(options))
                {
                    Assert.Equal(DEFAULT_BORROWING_COUNT, context.Borrows.Count());
                    Assert.NotNull(context.Borrows.Find(1));
                    Assert.Equal(editedborrow.DeadLine, context.Borrows.Find(1).DeadLine);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async void Borrows_DeleteBorrow_CountDecrements()
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
                    var service = new BorrowingService(context, _mapper);
                    await service.DeleteBorrowAsync(1);
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new NorthwindContext(options))
                {
                    Assert.Equal(DEFAULT_BORROWING_COUNT - 1, context.Borrows.Count());
                    Assert.Null(context.Borrows.Find(1));
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async void Borrows_GetAllBorrow_DefaultValue()
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

                int borrowCount = 0;
                // Run the test against one instance of the context
                using (var context = new NorthwindContext(options))
                {
                    var service = new BorrowingService(context, _mapper);
                    var borrows = await service.GetBorrowsAsync();
                    borrowCount = borrows.Count();
                }

                Assert.Equal(DEFAULT_BORROWING_COUNT, borrowCount);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
