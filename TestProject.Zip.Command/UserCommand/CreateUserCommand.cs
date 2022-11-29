using MediatR;
using TestProject.ZipPay.Contract.Request;
using TestProject.ZipPay.Domain.Entities;
using TestProject.ZipPay.Infrastructure.Context;

namespace TestProject.ZipPay.Command.UserCommand
{
    /// <summary>
    /// Class create new user in database
    /// Wrapper class encapsulates create user command
    /// </summary>
    public class CreateUserCommand : IRequest<int>
    {
        /// <summary>
        /// Object contains the data to create the new user
        /// </summary>
        public CreateUserRequest createUserRequest { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
        {
            private readonly ZipPayContext zipPayContext;

            public CreateUserCommandHandler(ZipPayContext zipPayContext)
            {
                this.zipPayContext = zipPayContext;
            }
            public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = new User()
                {
                    FirstName = request.createUserRequest.FirstName,
                    MiddleName = request.createUserRequest.MiddleName,
                    LastName = request.createUserRequest.LastName,
                    EmailId = request.createUserRequest.Email,
                    Gender = request.createUserRequest.Gender,
                    Expense = request.createUserRequest.Expense,
                    Salary = request.createUserRequest.Salary,
                    IsActive = true
                };

                this.zipPayContext.User.Add(user);

                return await this.zipPayContext.SaveChangesAsync();
            }
        }
    }
}
