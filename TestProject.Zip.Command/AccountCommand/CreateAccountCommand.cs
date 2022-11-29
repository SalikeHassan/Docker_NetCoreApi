using AutoFixture;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestProject.ZipPay.Common;
using TestProject.ZipPay.Contract.Request;
using TestProject.ZipPay.Domain.Entities;
using TestProject.ZipPay.Infrastructure.Context;

namespace TestProject.ZipPay.Command.AccountCommand
{
    /// <summary>
    /// Class create new account in database
    /// Wrapper class encapsulates create account command
    /// </summary>
    public class CreateAccountCommand : IRequest<int>
    {
        /// <summary>
        /// Primary key value of user table
        /// </summary>
        public int userId { get; set; }

        /// <summary>
        /// Object contains the data to create the new user account
        /// </summary>
        public CreateAccountRequest createAccountRequest { get; set; }

        public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
        {
            private readonly ZipPayContext zipPayContext;

            public CreateAccountCommandHandler(ZipPayContext zipPayContext)
            {
                this.zipPayContext = zipPayContext;
            }
            public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
            {
                var account = new Account()
                {
                    AccountCreateDateTime = DateTimeOffset.Now,
                    AccountNumber = Guid.NewGuid().ToString(),
                    UserId = request.userId,
                    AccountType = Constant.AccountType,
                    IsActive = true
                };

                this.zipPayContext.Account.Add(account);

                return await this.zipPayContext.SaveChangesAsync();
            }
        }
    }
}
