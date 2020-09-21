using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using DbReader;
using Householder.Server.Database;

namespace Householder.Server.Reconciliations
{
    public class GetReconciliationByIdQueryHandler : IQueryHandler<GetReconciliationByIdQuery, ReconciliationWithSettlementsDTO>
    {
        private readonly IDbConnection dbConnection;
        private readonly ISqlProvider sqlProvider;

        public GetReconciliationByIdQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<ReconciliationWithSettlementsDTO> HandleAsync(GetReconciliationByIdQuery query, CancellationToken cancellationToken = default)
        {
            var results = await dbConnection.ReadAsync<ReconciliationWithSettlementsDTO>(sqlProvider.GetReconciliationById, query);

            return results.SingleOrDefault();
        }
    }

    public class GetReconciliationByIdQuery : IQuery<ReconciliationWithSettlementsDTO>
    {
        public long Id { get; set; }
    }
}