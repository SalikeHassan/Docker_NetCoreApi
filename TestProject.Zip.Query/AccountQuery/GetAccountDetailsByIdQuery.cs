using MediatR;
using Microsoft.EntityFrameworkCore;
using TestProject.ZipPay.Common.HelperMethod;
using TestProject.ZipPay.Contract.Response;
using TestProject.ZipPay.Infrastructure.Context;

namespace TestProject.ZipPay.Query.AccountQuery
{
    /// <summary>
    /// Wrapper class to get the user account detail based on account id
    /// </summary>
    public class GetAccountDetailsByIdQuery : IRequest<AccountDetailsResponse>
    {
        /// <summary>
        /// Account id
        /// </summary>
        public int id { get; set; }
        public class GetAccountDetailsByIdQueryHandler : IRequestHandler<GetAccountDetailsByIdQuery, AccountDetailsResponse>
        {
            private readonly ZipPayContext zipPayContext;

            public GetAccountDetailsByIdQueryHandler(ZipPayContext zipPayContext)
            {
                this.zipPayContext = zipPayContext;
            }
            public async Task<AccountDetailsResponse> Handle(GetAccountDetailsByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await this.zipPayContext.Account
                      .Include(x => x.User)
                      .AsNoTracking()
                      .Where(x => x.IsActive && x.User.IsActive && x.Id == request.id)
                      .Select(account => new AccountDetailsResponse()
                      {
                          Id = account.Id,
                          AccountHolderName = Helper.GetFullName(account.User.FirstName, account.User.MiddleName, account.User.LastName),
                          AccountNumber = account.AccountNumber,
                          AccountType = account.AccountType,
                          Email = account.User.EmailId
                      }).SingleOrDefaultAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }
               
            }
        }
    }
}
