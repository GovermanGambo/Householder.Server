using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using DbReader;
using Householder.Server.Database;

namespace Householder.Server.Reconciliations
{
    public class GetReconciliationsQueryHandler : IQueryHandler<GetReconciliationsQuery, IEnumerable<ReconciliationWithSettlementsDTO>>
    {
        private readonly IDbConnection dbConnection;
        private readonly ISqlProvider sqlProvider;

        public GetReconciliationsQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<IEnumerable<ReconciliationWithSettlementsDTO>> HandleAsync(GetReconciliationsQuery query, CancellationToken cancellationToken = default)
        {
            var results = await dbConnection.ReadAsync<ReconciliationWithSettlementsDTO>(sqlProvider.GetReconciliations, query);

            return results;
        }
    }

    public class GetReconciliationsQuery : IQuery<IEnumerable<ReconciliationWithSettlementsDTO>>
    {
        public long Id { get; set; }
        public long CreatorId { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}