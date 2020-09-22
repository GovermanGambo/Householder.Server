using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using DbReader;
using Householder.Server.Database;

namespace Householder.Server.Users
{
    public class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, UserDTO>
    {
        private readonly IDbConnection dbConnection;
        private readonly ISqlProvider sqlProvider;

        public GetUserByEmailQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<UserDTO> HandleAsync(GetUserByEmailQuery query, CancellationToken cancellationToken = default)
        {
            var results = await dbConnection.ReadAsync<UserDTO>(sqlProvider.GetUserByEmail, query);

            return results.SingleOrDefault();
        }
    }

    public class GetUserByEmailQuery : IQuery<UserDTO>
    {
        public string Email { get; set; }
    }
}