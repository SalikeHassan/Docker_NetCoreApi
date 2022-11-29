using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using TestProject.ZipPay.Command.UserCommand;
using TestProject.ZipPay.Contract.Request;
using TestProject.ZipPay.Domain.Entities;
using TestProject.ZipPay.Infrastructure.Context;
using static TestProject.ZipPay.Command.AccountCommand.CreateAccountCommand;
using static TestProject.ZipPay.Command.UserCommand.CreateUserCommand;

namespace TestProject.ZipPay.Command.Test
{
    [TestFixture]
    public class UserCommandTest
    {
        private Mock<ZipPayContext> mockContext;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture() { OmitAutoProperties = true };
            var param = fixture.Build<DbContextOptions<ZipPayContext>>().Create();
            this.mockContext = new Mock<ZipPayContext>(param);
            this.mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task Should_Create_User()
        {
            var mockDbSet = new Mock<DbSet<User>>();

            this.mockContext.Setup(x => x.User).Returns(mockDbSet.Object);

            var command = new CreateUserCommand();
            command.createUserRequest = new CreateUserRequest();


            var handler = new CreateUserCommandHandler(this.mockContext.Object);

            var res = await handler.Handle(command, new CancellationToken());

            this.mockContext.Verify(m => m.User.Add(It.IsAny<User>()), Times.Once());
            this.mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
