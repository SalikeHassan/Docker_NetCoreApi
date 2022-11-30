using MediatR;
using Microsoft.EntityFrameworkCore;
using TestProject.ZipPay.Common.HelperMethod;
using TestProject.ZipPay.Contract.Response;
using TestProject.ZipPay.Infrastructure.Context;

namespace TestProject.ZipPay.Query.AccountQuery
{
    /// <summary>
    /// Wrapper class to get the users accounts details
    /// </summary>
    public class GetAccountDetailsQuery : IRequest<List<AccountDetailsResponse>>
    {
        public int pageNum { get; set; }

        public int pageSize { get; set; }

        public class GetAccountDetailsQueryHandler : IRequestHandler<GetAccountDetailsQuery, List<AccountDetailsResponse>>
        {
            private readonly ZipPayContext zipPayContext;

            public GetAccountDetailsQueryHandler(ZipPayContext zipPayContext)
            {
                this.zipPayContext = zipPayContext;
            }
            public async Task<List<AccountDetailsResponse>> Handle(GetAccountDetailsQuery request, CancellationToken cancellationToken)
            {
                return await this.zipPayContext.Account
                    .Include(x => x.User)
                    .AsNoTracking()
                    .Where(x => x.IsActive && x.User.IsActive)
                    .Select(account => new AccountDetailsResponse()
                    {
                        Id = account.Id,
                        AccountHolderName = Helper.GetFullName(account.User.FirstName, account.User.MiddleName, account.User.LastName),
                        AccountNumber = account.AccountNumber,
                        AccountType = account.AccountType,
                        Email = account.User.EmailId,
                        Salary = account.User.Salary,
                        Expense = account.User.Expense
                    }).Skip((request.pageNum - 1) * request.pageSize)
                    .Take(request.pageSize)
                    .ToListAsync();
            }
        }
    }
}
