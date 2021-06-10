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
    public class AuthorControllerTest
    {
        private Mock<IAuthorService> serviceMock = new Mock<IAuthorService>();

        [Fact]
        public async void Authors_GetAllAuthors_CallsGetAll()
        {
            //Arrange
            serviceMock.Setup(b => b.GetAuthorsAsync()).ReturnsAsync(
                new List<Author>() {
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
                    } }
                );

            //Act
            var authors = await serviceMock.Object.GetAuthorsAsync();

            //Assert
            Assert.Equal(3, authors.Count());
            serviceMock.Verify(b => b.GetAuthorsAsync(), Times.Once);
        }

        [Fact]
        public async void Authors_DeleteAuthor_CallsDelete()
        {
            //Arrange
            serviceMock.Setup(b => b.DeleteAuthorAsync(It.IsAny<int>()));
            
            //Act
            await serviceMock.Object.DeleteAuthorAsync(1);

            //Assert
            serviceMock.Verify(b => b.DeleteAuthorAsync(1), Times.Once);
        }

        [Fact]
        public async void Authors_UpdateAuthor_CallsUpdate()
        {
            //Arrange
            serviceMock.Setup(b => b.UpdateAuthorAsync(It.IsAny<int>(), It.IsAny<Author>()));
            var editedAuthor = new Author() { Id = 1, Name = "TestName" };

            //Act
            await serviceMock.Object.UpdateAuthorAsync(1, editedAuthor);

            //Assert
            serviceMock.Verify(b => b.UpdateAuthorAsync(1, editedAuthor), Times.Once);
        }

        [Fact]
        public async void Authors_AddAuthor_CallsAdd()
        {
            //Arrange
            serviceMock.Setup(b => b.InsertAuthorAsync(It.IsAny<Author>()));
            var editedAuthor = new Author() { Id = 1000, Name = "TestName" };

            //Act
            await serviceMock.Object.InsertAuthorAsync(editedAuthor);

            //Assert
            serviceMock.Verify(b => b.InsertAuthorAsync(editedAuthor), Times.Once);
        }
    }
}
