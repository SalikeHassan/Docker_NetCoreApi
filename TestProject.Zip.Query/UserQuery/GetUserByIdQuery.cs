using MediatR;
using Microsoft.EntityFrameworkCore;
using TestProject.ZipPay.Common.HelperMethod;
using TestProject.ZipPay.Contract.Response;
using TestProject.ZipPay.Infrastructure.Context;

namespace TestProject.ZipPay.Query.UserQuery
{
    /// <summary>
    /// Wrapper class to get the user account detail based on id
    /// </summary>
    public class GetUserByIdQuery : IRequest<UserDetailsResponse>
    {
        /// <summary>
        /// User id
        /// </summary>
        public int id { get; set; }
        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetailsResponse>
        {
            private readonly ZipPayContext zipPayContext;

            public GetUserByIdQueryHandler(ZipPayContext zipPayContext)
            {
                this.zipPayContext = zipPayContext;
            }
            public async Task<UserDetailsResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                return await this.zipPayContext.User
                    .AsNoTracking()
                    .Where(x => x.IsActive && x.Id == request.id)
                    .Select(user => new UserDetailsResponse()
                    {

                        Id = user.Id,
                        Name = $"{Helper.GetFullName(user.FirstName, user.MiddleName, user.LastName)}",
                        Email = user.EmailId,
                        Salary = user.Salary,
                        Expense = user.Expense,
                        Gender = user.Gender
                    }).FirstOrDefaultAsync();
            }
        }
    }
}
