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
    public class BookControllerTest
    {
        private Mock<IBookService> serviceMock = new Mock<IBookService>();

        [Fact]
        public async void Books_GetAllBooks_CallsGetAll()
        {
            //Arrange
            serviceMock.Setup(b => b.GetBooksAsync()).ReturnsAsync(
                new List<Book>() {
                    new Book() { Id = 1, Title = "Bűn és bűnhődés", AuthorId = 1 },
                    new Book() { Id = 2, Title = "A Karamazov testvérek", AuthorId = 1 },
                    new Book() { Id = 3, Title = "A félkegyelmű", AuthorId = 1 } }
                );

            //Act
            var books = await serviceMock.Object.GetBooksAsync();

            //Assert
            Assert.Equal(3, books.Count());
            serviceMock.Verify(b => b.GetBooksAsync(), Times.Once);
        }

        [Fact]
        public async void Books_DeleteBook_CallsDelete()
        {
            //Arrange
            serviceMock.Setup(b => b.DeleteBookAsync(It.IsAny<int>()));
            
            //Act
            await serviceMock.Object.DeleteBookAsync(1);

            //Assert
            serviceMock.Verify(b => b.DeleteBookAsync(1), Times.Once);
        }

        [Fact]
        public async void Books_UpdateBook_CallsUpdate()
        {
            //Arrange
            serviceMock.Setup(b => b.UpdateBookAsync(It.IsAny<int>(), It.IsAny<Book>()));
            var editedBook = new Book() { Id = 1, Title = "TestTitle" };

            //Act
            await serviceMock.Object.UpdateBookAsync(1, editedBook);

            //Assert
            serviceMock.Verify(b => b.UpdateBookAsync(1, editedBook), Times.Once);
        }

        [Fact]
        public async void Books_AddBook_CallsAdd()
        {
            //Arrange
            serviceMock.Setup(b => b.InsertBookAsync(It.IsAny<Book>()));
            var newBook = new Book() { Id = 1000, Title = "TestTitle" };

            //Act
            await serviceMock.Object.InsertBookAsync(newBook);

            //Assert
            serviceMock.Verify(b => b.InsertBookAsync(newBook), Times.Once);
        }
    }
}
