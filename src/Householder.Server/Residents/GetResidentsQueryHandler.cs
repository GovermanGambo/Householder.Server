using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using DbReader;
using Householder.Server.Database;

namespace Householder.Server.Residents
{
    public class GetResidentsQueryHandler : IQueryHandler<GetResidentsQuery, IEnumerable<ResidentDTO>>
    {
        private readonly IDbConnection dbConnection;

        private readonly ISqlProvider sqlProvider;

        public GetResidentsQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<IEnumerable<ResidentDTO>> HandleAsync(GetResidentsQuery query, CancellationToken cancellationToken = default)
        {
            var results = await dbConnection.ReadAsync<ResidentDTO>(sqlProvider.GetResidents);
            
            return results;
        }
    }

    public class GetResidentsQuery : IQuery<IEnumerable<ResidentDTO>>
    {
    }
}