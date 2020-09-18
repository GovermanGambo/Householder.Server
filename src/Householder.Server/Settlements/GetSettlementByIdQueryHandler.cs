using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using DbReader;
using Householder.Server.Database;
using Householder.Server.Models;

namespace Householder.Server.Settlements
{
    public class GetSettlementByIdQueryHandler : IQueryHandler<GetSettlementByIdQuery, SettlementDTO>
    {
        private readonly IDbConnection dbConnection;
        private readonly ISqlProvider sqlProvider;

        public GetSettlementByIdQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<SettlementDTO> HandleAsync(GetSettlementByIdQuery query, CancellationToken cancellationToken = default)
        {
            var results = await dbConnection.ReadAsync<SettlementDTO>(sqlProvider.GetSettlementById, query);

            return results.SingleOrDefault();
        }
    }

    public class GetSettlementByIdQuery : IQuery<SettlementDTO>
    {
        public string Id { get; set; }
    }
}