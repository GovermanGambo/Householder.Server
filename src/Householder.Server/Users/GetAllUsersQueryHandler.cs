using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using DbReader;
using Householder.Server.Database;

namespace Householder.Server.Users
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, IEnumerable<UserDTO>>
    {
        private readonly IDbConnection dbConnection;
        private readonly ISqlProvider sqlProvider;

        public GetAllUsersQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<IEnumerable<UserDTO>> HandleAsync(GetAllUsersQuery query, CancellationToken cancellationToken = default)
        {
            var results = await dbConnection.ReadAsync<UserDTO>(sqlProvider.GetAllUsers, query);

            return results;
        }
    }

    public class GetAllUsersQuery : IQuery<IEnumerable<UserDTO>>
    {
    }
}