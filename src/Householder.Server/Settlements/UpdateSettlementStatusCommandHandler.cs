using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using DbReader;
using Householder.Server.Database;

namespace Householder.Server.Settlements
{
    public class UpdateSettlementStatusCommandHandler : ICommandHandler<UpdateSettlementStatusCommand>
    {
        private readonly IDbConnection dbConnection;
        private readonly ISqlProvider sqlProvider;

        public UpdateSettlementStatusCommandHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task HandleAsync(UpdateSettlementStatusCommand command, CancellationToken cancellationToken = default)
        {
            var rowsAffected = await dbConnection.ExecuteAsync(sqlProvider.UpdateSettlementStatus, command);

            command.RowsAffected = rowsAffected;
        }
    }

    public class UpdateSettlementStatusCommand
    {
        public long Id { get; set; }
        public int StatusId { get; set; }
        public int RowsAffected { get; set; }
    }
}