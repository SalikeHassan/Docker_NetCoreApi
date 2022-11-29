using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using TestProject.ZipPay.Domain.Entities;
using TestProject.ZipPay.Infrastructure.Context;
using TestProject.ZipPay.Command.AccountCommand;
using static TestProject.ZipPay.Command.AccountCommand.CreateAccountCommand;

namespace TestProject.ZipPay.Command.Test
{
    [TestFixture]
    public class AccountCommandTest
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
        public async Task Should_Create_Account()
        {
            var mockDbSet = new Mock<DbSet<Account>>();

            this.mockContext.Setup(x => x.Account).Returns(mockDbSet.Object);

            var command = new CreateAccountCommand();
            var handler = new CreateAccountCommandHandler(this.mockContext.Object);

            var res = await handler.Handle(command, new CancellationToken());

            this.mockContext.Verify(m => m.Account.Add(It.IsAny<Account>()), Times.Once());
            this.mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}