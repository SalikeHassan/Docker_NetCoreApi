using Microsoft.EntityFrameworkCore;
using TestProject.ZipPay.Command.AccountCommand;
using TestProject.ZipPay.Command.UserCommand;
using TestProject.ZipPay.Infrastructure.Context;
using TestProject.ZipPay.Query.AccountQuery;
using TestProject.ZipPay.Query.UserQuery;
using static TestProject.ZipPay.Command.AccountCommand.CreateAccountCommand;
using static TestProject.ZipPay.Command.UserCommand.CreateUserCommand;
using static TestProject.ZipPay.Query.AccountQuery.GetAccountCountQuery;
using static TestProject.ZipPay.Query.AccountQuery.GetAccountDetailsByIdQuery;
using static TestProject.ZipPay.Query.AccountQuery.GetAccountDetailsQuery;
using static TestProject.ZipPay.Query.UserQuery.GetUserCountQuery;

namespace TestProject.ZipPay.Query.Tests
{
    [TestFixture]
    public class AccountQueryTests
    {
        private DbContextOptions<ZipPayContext> options;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ZipPayContext>()
            .UseInMemoryDatabase(databaseName: "temp_zippay", b => b.EnableNullChecks(false)).Options;
        }

        private static async Task Init(ZipPayContext context)
        {
            context.Database.EnsureDeleted();
            var userCommand = new CreateUserCommand()
            {
                createUserRequest = new Contract.Request.CreateUserRequest()
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "test@test.com",
                    Salary = 2000,
                    Expense = 100,
                    Gender = "Male"
                }
            };
            var commandHandler = new CreateUserCommandHandler(context);

            await commandHandler.Handle(userCommand, new CancellationToken());

            var accountCommand = new CreateAccountCommand()
            {
                userId = 1
            };
            var accountCommandHandler = new CreateAccountCommandHandler(context);

            await accountCommandHandler.Handle(accountCommand, new CancellationToken());
        }

        [Test]
        [Order(1)]
        public async Task Should_Not_Get_User_AccountsDetails()
        {

            using (var context = new ZipPayContext(this.options))
            {
                var query = new GetAccountDetailsQuery() { pageNum = 1, pageSize = 10 };

                var handler = new GetAccountDetailsQueryHandler(context);

                var result = await handler.Handle(query, new CancellationToken());

                Assert.That(result.Count, Is.EqualTo(0));
            }
        }

        [Test]
        [Order(2)]
        public async Task Should_Get_User_AccountsDetailsById()
        {

            using (var context = new ZipPayContext(this.options))
            {
                await Init(context);

                var query = new GetAccountDetailsByIdQuery() { id = 1 };

                var handler = new GetAccountDetailsByIdQueryHandler(context);

                var result = await handler.Handle(query, new CancellationToken());

                Assert.That(result.Email, Is.EqualTo("test@test.com"));
            }
        }

        [Test]
        [Order(3)]
        public async Task Should_Not_Get_User_AccountsDetailsById()
        {

            using (var context = new ZipPayContext(this.options))
            {
                var query = new GetAccountDetailsByIdQuery() { id = 2 };

                var handler = new GetAccountDetailsByIdQueryHandler(context);

                var result = await handler.Handle(query, new CancellationToken());

                Assert.IsNull(result);
            }
        }

        [Test]
        [Order(4)]
        public async Task Should_Get_User_AccountsDetails()
        {

            using (var context = new ZipPayContext(this.options))
            {
                var query = new GetAccountDetailsQuery() { pageNum = 1, pageSize = 10 };

                var handler = new GetAccountDetailsQueryHandler(context);

                var result = await handler.Handle(query, new CancellationToken());

                Assert.That(result.Count, Is.EqualTo(1));
            }
        }

        [Test]
        [Order(5)]
        public async Task Should_UserAccountCount_GreaterThanZero()
        {

            using (var context = new ZipPayContext(this.options))
            {
                var query = new GetAccountCountQuery();

                var handler = new GetAccountCountQueryHandler(context);

                var result = await handler.Handle(query, new CancellationToken());

                Assert.That(result, Is.GreaterThan(0));
            }
        }
    }
}