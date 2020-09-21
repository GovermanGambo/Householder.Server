using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using DbReader;
using Householder.Server.Database;
using Householder.Server.Models;

namespace Householder.Server.Residents
{
    public class GetResidentsQueryHandler : IQueryHandler<GetResidentsQuery, IEnumerable<Resident>>
    {
        private readonly IDbConnection dbConnection;

        private readonly ISqlProvider sqlProvider;

        public GetResidentsQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<IEnumerable<Resident>> HandleAsync(GetResidentsQuery query, CancellationToken cancellationToken = default)
        {
            var results = await dbConnection.ReadAsync<Resident>(sqlProvider.GetResidents);
            
            return results;
        }
    }

    public class GetResidentsQuery : IQuery<IEnumerable<Resident>>
    {
    }
}