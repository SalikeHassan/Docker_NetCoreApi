using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TestProject.ZipPay.Api.Controllers;
using TestProject.ZipPay.Command.AccountCommand;
using TestProject.ZipPay.Command.UserCommand;
using TestProject.ZipPay.Contract.Request;
using TestProject.ZipPay.Contract.Response;
using TestProject.ZipPay.Domain.Entities;
using TestProject.ZipPay.Query.UserQuery;

namespace TestProject.ZipPay.Api.Tests
{
    [TestFixture]
    public class UserControllerTest
    {
        private Mock<IMediator> mediator;

        [SetUp]
        public void Setup()
        {
            this.mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Should_GetUserDetails_WithStatus200Ok()
        {
            var userDetailsResponses = new List<UserDetailsResponse>
           {
                new UserDetailsResponse()
                {
                    Id = 1,
                    Email = "test@test.com",
                    Salary = 2000,
                    Expense = 200
                }
           };

            this.mediator.Setup(x => x.Send(It.IsAny<GetUsersQuery>(), new CancellationToken())).
                ReturnsAsync(userDetailsResponses);

            var userController = new UserController(this.mediator.Object);

            var result = await userController.Get(1, 10) as OkObjectResult;

            var accountDetails = result?.Value as PaginationResponse<UserDetailsResponse>;

            Assert.Multiple(() =>
            {
                Assert.That(result, !Is.EqualTo(null));
                Assert.That(result?.StatusCode, Is.EqualTo(200));
                Assert.That(result?.Value, Is.TypeOf<PaginationResponse<UserDetailsResponse>>());
                Assert.That(accountDetails?.Data.Count, Is.EqualTo(1));
            });
        }

        [Test]
        public async Task Should_Not_GetUserDetails_WithStatus204NoContent()
        {
            this.mediator.Setup(x => x.Send(It.IsAny<GetUsersQuery>(), new CancellationToken())).
              ReturnsAsync(new List<UserDetailsResponse>());

            var userController = new UserController(this.mediator.Object);

            var result = await userController.Get(1, 10) as NoContentResult;

            Assert.That(result?.StatusCode, Is.EqualTo(204));
        }

        [Test]
        public async Task Should_GetUserDetailsById_WithStatus200Ok()
        {
            var userDetails =
                new UserDetailsResponse()
                {
                    Id = 1,
                    Email = "test@test.com",
                    Salary = 2000,
                    Expense = 200
                };

            this.mediator.Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), new CancellationToken())).
                ReturnsAsync(userDetails);

            var userController = new UserController(this.mediator.Object);

            var result = await userController.Get(1) as OkObjectResult;

            var userDetailsResponse = result?.Value as UserDetailsResponse;

            Assert.Multiple(() =>
            {
                Assert.That(result, !Is.EqualTo(null));
                Assert.That(result?.StatusCode, Is.EqualTo(200));
                Assert.That(result?.Value, Is.TypeOf<UserDetailsResponse>());
                Assert.That(userDetailsResponse?.Id, Is.EqualTo(1));
            });
        }

        [Test]
        public async Task Should_Not_GetUserDetailsById_WithStatus404()
        {
            this.mediator.Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), new CancellationToken()));

            var userController = new UserController(this.mediator.Object);

            var result = await userController.Get(1) as NotFoundResult;

            Assert.That(result?.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task Should_Create_New_User_WithStatus200Ok()
        {
            var createUserRequest = new CreateUserRequest()
            {
                FirstName = "Test First Name",
                LastName = "Test Last Name",
                Email = "test@test.com",
                Salary = 2000,
                Expense = 100,
                Gender = "Male"
            };

            this.mediator.Setup(x => x.Send(It.IsAny<GetUserByEmailQuery>(), new CancellationToken()));

            this.mediator.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), new CancellationToken()));

            var userController = new UserController(this.mediator.Object);

            var result = await userController.Post(createUserRequest) as OkResult;

            Assert.That(result?.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task Should_NotCreate_NewUser_WithDuplicateEmail_WithStatus400()
        {
            var createUserRequest = new CreateUserRequest()
            {
                FirstName = "Test First Name",
                LastName = "Test Last Name",
                Email = "test@test.com",
                Salary = 2000,
                Expense = 100,
                Gender = "Male"
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

            var userController = new UserController(this.mediator.Object);

            var result = await userController.Post(createUserRequest) as BadRequestObjectResult;

            Assert.Multiple(() =>
            {
                Assert.That(result?.StatusCode, Is.EqualTo(400));
                Assert.That(result?.Value, Is.EqualTo("Email is already in use."));
            });
        }

        [Test]
        public async Task Should_NotCreate_NewUser_WithStatus400_ForInValidModel()
        {
            var createUserRequest = new CreateUserRequest();

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

            var userController = new UserController(this.mediator.Object);
            userController.ModelState.AddModelError("Email", "Email id is required.");

            var result = await userController.Post(createUserRequest) as BadRequestResult;

            Assert.That(result?.StatusCode, Is.EqualTo(400));
        }
    }
}