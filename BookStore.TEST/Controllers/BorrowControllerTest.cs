using BookStore.BLL.DTO;
using BookStore.BLL.Interfaces;
using BookStore.BLL.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.TEST.Controllers
{
    public class BorrowControllerTest
    {
        private Mock<IBorrowingService> serviceMock = new Mock<IBorrowingService>();

        [Fact]
        public async void Borrows_GetAllBorrows_CallsGetAll()
        {
            //Arrange
            serviceMock.Setup(b => b.GetBorrowsAsync()).ReturnsAsync(
                new List<Borrowing>() {
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
                    } 
                });

            //Act
            var borrows = await serviceMock.Object.GetBorrowsAsync();

            //Assert
            Assert.Equal(2, borrows.Count());
            serviceMock.Verify(b => b.GetBorrowsAsync(), Times.Once);
        }

        [Fact]
        public async void Borrows_DeleteBorrow_CallsDelete()
        {
            //Arrange
            serviceMock.Setup(b => b.DeleteBorrowAsync(It.IsAny<int>()));
            
            //Act
            await serviceMock.Object.DeleteBorrowAsync(1);

            //Assert
            serviceMock.Verify(b => b.DeleteBorrowAsync(1), Times.Once);
        }

        [Fact]
        public async void Borrows_UpdateBorrow_CallsUpdate()
        {
            //Arrange
            serviceMock.Setup(b => b.UpdateBorrowAsync(It.IsAny<int>(), It.IsAny<Borrowing>()));
            var editedBorrow = new Borrowing() {
                Id = 1,
                BorrowedBookId = 1,
                BorrowerId = 1,
                DeadLine = new DateTime(2020, 10, 11)
            };

            //Act
            await serviceMock.Object.UpdateBorrowAsync(1, editedBorrow);

            //Assert
            serviceMock.Verify(b => b.UpdateBorrowAsync(1, editedBorrow), Times.Once);
        }

        [Fact]
        public async void Borrows_AddBorrow_CallsAdd()
        {
            //Arrange
            serviceMock.Setup(b => b.InsertBorrowAsync(It.IsAny<Borrowing>()));
            var newBorrow = new Borrowing() {
                Id = 1000,
                BorrowedBookId = 1,
                BorrowerId = 1,
                DeadLine = new DateTime(2020, 10, 11)
            };

            //Act
            await serviceMock.Object.InsertBorrowAsync(newBorrow);

            //Assert
            serviceMock.Verify(b => b.InsertBorrowAsync(newBorrow), Times.Once);
        }
    }
}
