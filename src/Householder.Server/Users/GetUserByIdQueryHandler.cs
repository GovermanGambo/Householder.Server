using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using DbReader;
using Householder.Server.Database;

namespace Householder.Server.Users
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly IDbConnection dbConnection;
        private readonly ISqlProvider sqlProvider;

        public GetUserByIdQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<UserDTO> HandleAsync(GetUserByIdQuery query, CancellationToken cancellationToken = default)
        {
            var results = await dbConnection.ReadAsync<UserDTO>(sqlProvider.GetUserById, query);

            return results.SingleOrDefault();
        }
    }

    public class GetUserByIdQuery : IQuery<UserDTO>
    {
        public long Id { get; set; }
    }
}