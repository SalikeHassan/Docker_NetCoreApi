using Microsoft.EntityFrameworkCore;
using TestProject.ZipPay.Command.UserCommand;
using TestProject.ZipPay.Infrastructure.Context;
using TestProject.ZipPay.Query.UserQuery;
using static TestProject.ZipPay.Command.UserCommand.CreateUserCommand;
using static TestProject.ZipPay.Query.UserQuery.GetUserByEmailQuery;
using static TestProject.ZipPay.Query.UserQuery.GetUserByIdQuery;
using static TestProject.ZipPay.Query.UserQuery.GetUserCountQuery;
using static TestProject.ZipPay.Query.UserQuery.GetUsersQuery;

namespace TestProject.ZipPay.Query.Tests
{
    [TestFixture]
    public class UserQueryTest
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
        }


        [Test]
        [Order(1)]
        public async Task Should_Not_Get_UsersDetails()
        {

            using (var context = new ZipPayContext(this.options))
            {
                context.Database.EnsureDeleted();
                var query = new GetUsersQuery() { pageNum = 1, pageSize = 10 };

                var handler = new GetUsersQueryHandler(context);

                var result = await handler.Handle(query, new CancellationToken());

                Assert.That(result.Count, Is.EqualTo(0));
            }
        }

        [Test]
        [Order(2)]
        public async Task Should_Get_UserDetailsById()
        {

            using (var context = new ZipPayContext(this.options))
            {
                await Init(context);

                var query = new GetUserByIdQuery() { id = 1 };

                var handler = new GetUserByIdQueryHandler(context);

                var result = await handler.Handle(query, new CancellationToken());

                Assert.Multiple(() =>
                {
                    Assert.That(result.Email, Is.EqualTo("test@test.com"));
                    Assert.That(result.Id, Is.EqualTo(1));
                });
            }
        }

        [Test]
        [Order(3)]
        public async Task Should_Not_Get_User_AccountsDetailsById()
        {

            using (var context = new ZipPayContext(this.options))
            {
                var query = new GetUserByIdQuery() { id = 2 };

                var handler = new GetUserByIdQueryHandler(context);

                var result = await handler.Handle(query, new CancellationToken());

                Assert.IsNull(result);
            }
        }

        [Test]
        [Order(4)]
        public async Task Should_Get_UsersDetails()
        {

            using (var context = new ZipPayContext(this.options))
            {
                var query = new GetUsersQuery() { pageNum = 1, pageSize = 10 };

                var handler = new GetUsersQueryHandler(context);

                var result = await handler.Handle(query, new CancellationToken());

                Assert.That(result.Count, Is.EqualTo(1));
            }
        }

        [Test]
        [Order(5)]
        public async Task Should_Get_UserDetails_ByEmail()
        {

            using (var context = new ZipPayContext(this.options))
            {
                var query = new GetUserByEmailQuery() { email = "test@test.com" };

                var handler = new GetUserByEmailQueryHandler(context);

                var result = await handler.Handle(query, new CancellationToken());

                Assert.That(result.EmailId, Is.EqualTo("test@test.com"));
            }
        }

        [Test]
        [Order(6)]
        public async Task Should_Not_Get_UserDetails_ByEmail()
        {

            using (var context = new ZipPayContext(this.options))
            {
                var query = new GetUserByEmailQuery() { email = "test2@test.com" };

                var handler = new GetUserByEmailQueryHandler(context);

                var result = await handler.Handle(query, new CancellationToken());

                Assert.IsNull(result);
            }
        }

        [Test]
        [Order(7)]
        public async Task Should_UserCount_GreaterThanZero()
        {

            using (var context = new ZipPayContext(this.options))
            {
                var query = new GetUserCountQuery();

                var handler = new GetUserCountQueryHandler(context);

                var result = await handler.Handle(query, new CancellationToken());

                Assert.That(result, Is.GreaterThan(0));
            }
        }
    }
}
