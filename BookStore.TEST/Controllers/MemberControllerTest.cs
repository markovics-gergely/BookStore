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
    public class MemberControllerTest
    {
        private Mock<IMemberService> serviceMock = new Mock<IMemberService>();

        [Fact]
        public async void Members_GetAllMembers_CallsGetAll()
        {
            //Arrange
            serviceMock.Setup(b => b.GetMembersAsync()).ReturnsAsync(
                new List<Member>() {
                    new Member() { Id = 1, Name = "Teszt Elek" },
                    new Member() { Id = 2, Name = "Bádog Béla" } }
                );

            //Act
            var members = await serviceMock.Object.GetMembersAsync();

            //Assert
            Assert.Equal(2, members.Count());
            serviceMock.Verify(b => b.GetMembersAsync(), Times.Once);
        }

        [Fact]
        public async void Members_DeleteMember_CallsDelete()
        {
            //Arrange
            serviceMock.Setup(b => b.DeleteMemberAsync(It.IsAny<int>()));
            
            //Act
            await serviceMock.Object.DeleteMemberAsync(1);

            //Assert
            serviceMock.Verify(b => b.DeleteMemberAsync(1), Times.Once);
        }

        [Fact]
        public async void Members_UpdateMember_CallsUpdate()
        {
            //Arrange
            serviceMock.Setup(b => b.UpdateMemberAsync(It.IsAny<int>(), It.IsAny<Member>()));
            var editedMember = new Member() { Id = 1, Name = "TestName" };

            //Act
            await serviceMock.Object.UpdateMemberAsync(1, editedMember);

            //Assert
            serviceMock.Verify(b => b.UpdateMemberAsync(1, editedMember), Times.Once);
        }

        [Fact]
        public async void Members_AddMember_CallsAdd()
        {
            //Arrange
            serviceMock.Setup(b => b.InsertMemberAsync(It.IsAny<Member>()));
            var newMember = new Member() { Id = 1000, Name = "TestName" };

            //Act
            await serviceMock.Object.InsertMemberAsync(newMember);

            //Assert
            serviceMock.Verify(b => b.InsertMemberAsync(newMember), Times.Once);
        }
    }
}
