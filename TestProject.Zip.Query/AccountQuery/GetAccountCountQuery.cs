using MediatR;
using Microsoft.EntityFrameworkCore;
using TestProject.ZipPay.Infrastructure.Context;

namespace TestProject.ZipPay.Query.AccountQuery
{
    /// <summary>
    /// Wrapper class to get the users accounts details
    /// </summary>
    public class GetAccountCountQuery : IRequest<int>
    {
        public class GetAccountCountQueryHandler : IRequestHandler<GetAccountCountQuery, int>
        {
            private readonly ZipPayContext zipPayContext;

            public GetAccountCountQueryHandler(ZipPayContext zipPayContext)
            {
                this.zipPayContext = zipPayContext;
            }
            public async Task<int> Handle(GetAccountCountQuery request, CancellationToken cancellationToken)
            {
                return await this.zipPayContext.Account
                    .Include(x => x.User)
                    .AsNoTracking()
                    .CountAsync();
            }
        }
    }
}
