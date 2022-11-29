using MediatR;
using Microsoft.EntityFrameworkCore;
using TestProject.ZipPay.Domain.Entities;
using TestProject.ZipPay.Infrastructure.Context;

namespace TestProject.ZipPay.Query.UserQuery
{
    /// <summary>
    /// Wrapper class to get the user account detail based on email id
    /// </summary>
    public class GetUserByEmailQuery : IRequest<User>
    {
        /// <summary>
        /// User email id
        /// </summary>
        public string email { get; set; }
        public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User>
        {
            private readonly ZipPayContext zipPayContext;

            public GetUserByEmailQueryHandler(ZipPayContext zipPayContext)
            {
                this.zipPayContext = zipPayContext;
            }
            public async Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
            {
                return await this.zipPayContext.User
                    .AsNoTracking()
                    .Where(x => x.IsActive && x.EmailId == request.email)
                    .SingleOrDefaultAsync();
            }
        }
    }
}
