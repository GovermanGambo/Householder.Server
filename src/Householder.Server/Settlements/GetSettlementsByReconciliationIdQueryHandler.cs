using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using DbReader;
using Householder.Server.Database;

namespace Householder.Server.Settlements
{
    public class GetSettlementsByReconciliationIdQueryHandler: IQueryHandler<GetSettlementsByReconciliationIdQuery, IEnumerable<SettlementDTO>>
    {
        private readonly IDbConnection dbConnection;
        private readonly ISqlProvider sqlProvider;

        public GetSettlementsByReconciliationIdQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<IEnumerable<SettlementDTO>> HandleAsync(GetSettlementsByReconciliationIdQuery query, CancellationToken cancellationToken = default)
        {
            var results = await dbConnection.ReadAsync<SettlementDTO>(sqlProvider.GetSettlementsByReconciliationId, query);

            return results;
        }
    }

    public class GetSettlementsByReconciliationIdQuery : IQuery<IEnumerable<SettlementDTO>>
    {
        public long ReconciliationId { get; set; }
    }
}