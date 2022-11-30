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
    public class GetUserCountQuery : IRequest<int>
    {
        public class GetUserCountQueryHandler : IRequestHandler<GetUserCountQuery, int>
        {
            private readonly ZipPayContext zipPayContext;

            public GetUserCountQueryHandler(ZipPayContext zipPayContext)
            {
                this.zipPayContext = zipPayContext;
            }
            public async Task<int> Handle(GetUserCountQuery request, CancellationToken cancellationToken)
            {
                return await this.zipPayContext.User
                    .AsNoTracking()
                    .CountAsync();
            }
        }
    }
}