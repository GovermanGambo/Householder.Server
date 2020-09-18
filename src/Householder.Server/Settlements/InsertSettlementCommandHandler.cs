using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using DbReader;
using Householder.Server.Database;

namespace Householder.Server.Settlements
{
    public class InsertSettlementCommandHandler : ICommandHandler<InsertSettlementCommand>
    {
        private readonly IDbConnection dbConnection;
        private readonly ISqlProvider sqlProvider;

        public InsertSettlementCommandHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task HandleAsync(InsertSettlementCommand command, CancellationToken cancellationToken = default)
        {
            await dbConnection.ExecuteAsync(sqlProvider.InsertSettlement, command);

            command.Id = await dbConnection.ExecuteScalarAsync<long>(sqlProvider.GetLastInsertId);
        }
    }

    public class InsertSettlementCommand
    {
        public long Id { get; set; }
        public long ReconciliationId { get; set; }
        public long CreditorId { get; set; }
        public long DebtorId { get; set; }
        public decimal Amount { get; set; }
        public int StatusId { get; set; }
    }
}