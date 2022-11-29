using MediatR;
using Microsoft.EntityFrameworkCore;
using TestProject.ZipPay.Common.HelperMethod;
using TestProject.ZipPay.Contract.Response;
using TestProject.ZipPay.Infrastructure.Context;

namespace TestProject.ZipPay.Query.UserQuery
{
    /// <summary>
    /// Wrapper class to get the users
    /// </summary>
    public class GetUsersQuery : IRequest<List<UserDetailsResponse>>
    {
        public int pageNum { get; set; }

        public int pageSize { get; set; }
        public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDetailsResponse>>
        {
            private readonly ZipPayContext zipPayContext;

            public GetUsersQueryHandler(ZipPayContext zipPayContext)
            {
                this.zipPayContext = zipPayContext;
            }
            public async Task<List<UserDetailsResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
            {
                return await this.zipPayContext.User
                    .AsNoTracking()
                    .Where(x => x.IsActive)
                    .Select
                    (
                      user => new UserDetailsResponse
                      {
                          Id = user.Id,
                          Name = $"{Helper.GetFullName(user.FirstName, user.MiddleName, user.LastName)}",
                          Email = user.EmailId,
                          Salary = user.Salary,
                          Expense = user.Expense,
                          Gender = user.Gender
                      }
                    ).Skip((request.pageNum - 1) * request.pageSize)
                    .Take(request.pageSize)
                    .ToListAsync();
            }
        }
    }
}