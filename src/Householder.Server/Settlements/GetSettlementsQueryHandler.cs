using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using DbReader;
using Householder.Server.Database;
using Householder.Server.Models;

namespace Householder.Server.Settlements
{
    public class GetSettlementsQueryHandler : IQueryHandler<GetSettlementsQuery, IEnumerable<SettlementDTO>>
    {
        private readonly IDbConnection dbConnection;
        private readonly ISqlProvider sqlProvider;

        public GetSettlementsQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<IEnumerable<SettlementDTO>> HandleAsync(GetSettlementsQuery query, CancellationToken cancellationToken = default)
        {
            var results = await dbConnection.ReadAsync<SettlementDTO>(sqlProvider.GetSettlements, query);

            return results;
        }
    }

    public class GetSettlementsQuery : IQuery<IEnumerable<SettlementDTO>>
    {
    }
}