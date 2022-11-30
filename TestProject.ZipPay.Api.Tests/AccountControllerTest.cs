using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TestProject.ZipPay.Api.Controllers;
using TestProject.ZipPay.Command.AccountCommand;
using TestProject.ZipPay.Contract.Request;
using TestProject.ZipPay.Contract.Response;
using TestProject.ZipPay.Domain.Entities;
using TestProject.ZipPay.Query.AccountQuery;
using TestProject.ZipPay.Query.UserQuery;

namespace TestProject.ZipPay.Api.Tests
{
    [TestFixture]
    public class AccountControllerTest
    {
        private Mock<IMediator> mediator;

        [SetUp]
        public void Setup()
        {
            this.mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Should_GetAccountDetails_WithStatus200Ok()
        {
            var accountResponse = new List<AccountDetailsResponse>
           {
                new AccountDetailsResponse()
                {
                    AccountHolderName  = "test",
                    AccountNumber ="121",
                    AccountType = "Credit",
                    Email ="test",
                    Id = 1
                }
           };

            this.mediator.Setup(x => x.Send(It.IsAny<GetAccountDetailsQuery>(), new CancellationToken())).
                ReturnsAsync(accountResponse);

            var accountController = new AccountController(this.mediator.Object);

            var result = await accountController.Get(1, 10) as OkObjectResult;

            var accountDetails = result?.Value as PaginationResponse<AccountDetailsResponse>;

            Assert.Multiple(() =>
            {
                Assert.That(result, !Is.EqualTo(null));
                Assert.That(result?.StatusCode, Is.EqualTo(200));
                Assert.That(result?.Value, Is.TypeOf<PaginationResponse<AccountDetailsResponse>>());
                Assert.That(accountDetails?.Data.Count, Is.EqualTo(1));
            });
        }

        [Test]
        public async Task Should_GetAccountDetails_WithStatus204NoContent()
        {
            this.mediator.Setup(x => x.Send(It.IsAny<GetAccountDetailsQuery>(), new CancellationToken())).
              ReturnsAsync(new List<AccountDetailsResponse>());

            var accountController = new AccountController(this.mediator.Object);

            var result = await accountController.Get(1, 10) as NoContentResult;

            Assert.That(result?.StatusCode, Is.EqualTo(204));
        }

        [Test]
        public async Task Should_GetAccountDetailsById_WithStatus200Ok()
        {
            var accountResponse =
                new AccountDetailsResponse()
                {
                    AccountHolderName = "test",
                    AccountNumber = "121",
                    AccountType = "Credit",
                    Email = "test",
                    Id = 1
                };

            this.mediator.Setup(x => x.Send(It.IsAny<GetAccountDetailsByIdQuery>(), new CancellationToken())).
                ReturnsAsync(accountResponse);

            var accountController = new AccountController(this.mediator.Object);

            var result = await accountController.Get(1) as OkObjectResult;

            var accountDetailsResponse = result?.Value as AccountDetailsResponse;

            Assert.Multiple(() =>
            {
                Assert.That(result, !Is.EqualTo(null));
                Assert.That(result?.StatusCode, Is.EqualTo(200));
                Assert.That(result?.Value, Is.TypeOf<AccountDetailsResponse>());
                Assert.That(accountDetailsResponse?.Id, Is.EqualTo(1));
            });
        }

        [Test]
        public async Task Should_Not_GetAccountDetailsById_WithStatus404()
        {


            this.mediator.Setup(x => x.Send(It.IsAny<GetAccountDetailsByIdQuery>(), new CancellationToken()));

            var accountController = new AccountController(this.mediator.Object);

            var result = await accountController.Get(1) as NotFoundResult;

            Assert.That(result?.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task Should_Create_New_Account_WithStatus200Ok()
        {
            var createAccountRequest = new CreateAccountRequest()
            {
                Email = "test@test.com"
            };

            var user = new User()
            {
                EmailId = "test@test.com",
                Id = 1,
                IsActive = true,
                Salary = 2000,
                Expense = 200
            };


            this.mediator.Setup(x => x.Send(It.IsAny<GetUserByEmailQuery>(), new CancellationToken()))
                .ReturnsAsync(user);

            this.mediator.Setup(x => x.Send(It.IsAny<CreateAccountCommand>(), new CancellationToken()));

            var accountController = new AccountController(this.mediator.Object);

            var result = await accountController.Post(createAccountRequest) as OkResult;

            Assert.That(result?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task Should_NotCreate_NewAccount_WithStatus400()
        {
            var createAccountRequest = new CreateAccountRequest()
            {
                Email = "test@test.com"
            };

            var user = new User()
            {
                EmailId = "test@test.com",
                Id = 1,
                IsActive = true,
                Salary = 2000,
                Expense = 200
            };


            this.mediator.Setup(x => x.Send(It.IsAny<GetUserByEmailQuery>(), new CancellationToken()))
                .ReturnsAsync(new User());

            this.mediator.Setup(x => x.Send(It.IsAny<CreateAccountCommand>(), new CancellationToken()));

            var accountController = new AccountController(this.mediator.Object);

            var result = await accountController.Post(createAccountRequest) as BadRequestObjectResult;

            Assert.Multiple(() =>
            {
                Assert.That(result?.StatusCode, Is.EqualTo(400));
                Assert.That(result?.Value, Is.EqualTo("You are not elegibel to create the account."));
            });
        }

        [Test]
        public async Task Should_NotCreate_NewAccount_WithStatus400_ForInValidModel()
        {
            this.mediator.Setup(x => x.Send(It.IsAny<GetUserByEmailQuery>(), new CancellationToken()))
            .ReturnsAsync(new User());

            this.mediator.Setup(x => x.Send(It.IsAny<CreateAccountCommand>(), new CancellationToken()));

            var accountController = new AccountController(this.mediator.Object);
            accountController.ModelState.AddModelError("Email", "Email id is required.");

            var result = await accountController.Post(new CreateAccountRequest()) as BadRequestResult;

            Assert.That(result?.StatusCode, Is.EqualTo(400));
        }
    }
}