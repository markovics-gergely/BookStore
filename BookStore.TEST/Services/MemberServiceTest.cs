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
    public class MemberServiceTest
    {
        private static int DEFAULT_MEMBER_COUNT = 2;

        private static IMapper _mapper;

        public MemberServiceTest()
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
        public async void Members_AddMember_CountIncrements()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var newMember = new Member()
            {
                Id = 1000, 
                Name = "TestName"
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
                    var service = new MemberService(context, _mapper);
                    await service.InsertMemberAsync(newMember);
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new NorthwindContext(options))
                {
                    Assert.Equal(DEFAULT_MEMBER_COUNT + 1, context.Members.Count());
                    Assert.NotNull(context.Members.Find(1000));
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async void Members_EditMember_Changes()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var editedMember = new Member()
            {
                Id = 1,
                Name = "TestName1"
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
                    var service = new MemberService(context, _mapper);
                    await service.UpdateMemberAsync(1, editedMember);
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new NorthwindContext(options))
                {
                    Assert.Equal(DEFAULT_MEMBER_COUNT, context.Members.Count());
                    Assert.NotNull(context.Members.Find(1));
                    Assert.Equal("TestName1", context.Members.Find(1).Name);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async void Members_DeleteMember_CountDecrements()
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
                    var service = new MemberService(context, _mapper);
                    await service.DeleteMemberAsync(1);
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new NorthwindContext(options))
                {
                    Assert.Equal(DEFAULT_MEMBER_COUNT - 1, context.Members.Count());
                    Assert.Null(context.Members.Find(1));
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async void Members_GetAllMember_DefaultValue()
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

                int memberCount = 0;
                // Run the test against one instance of the context
                using (var context = new NorthwindContext(options))
                {
                    var service = new MemberService(context, _mapper);
                    var members = await service.GetMembersAsync();
                    memberCount = members.Count();
                }

                Assert.Equal(DEFAULT_MEMBER_COUNT, memberCount);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
