using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using DbReader;
using Householder.Server.Database;

namespace Householder.Server.Users
{
    public class GetUserLoginByEmailQueryHandler : IQueryHandler<GetUserLoginByEmailQuery, UserLoginDTO>
    {
        private readonly IDbConnection dbConnection;
        private readonly ISqlProvider sqlProvider;

        public GetUserLoginByEmailQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<UserLoginDTO> HandleAsync(GetUserLoginByEmailQuery query, CancellationToken cancellationToken = default)
        {
            var results = await dbConnection.ReadAsync<UserLoginDTO>(sqlProvider.GetUserLoginByEmail, query);

            return results.SingleOrDefault();
        }
    }

    public class GetUserLoginByEmailQuery : IQuery<UserLoginDTO>
    {
        public string Email { get; set; }
    }
}