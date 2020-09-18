using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using DbReader;
using Householder.Server.Database;
using Newtonsoft.Json;

namespace Householder.Server.Reconciliations
{
    public class InsertReconciliationCommandHandler : ICommandHandler<InsertReconciliationCommand>
    {
        private readonly IDbConnection dbConnection;
        private readonly ISqlProvider sqlProvider;

        public InsertReconciliationCommandHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task HandleAsync(InsertReconciliationCommand command, CancellationToken cancellationToken = default)
        {
            await dbConnection.ExecuteAsync(sqlProvider.InsertReconciliation, command);
            command.Id = await dbConnection.ExecuteScalarAsync<long>(sqlProvider.GetLastInsertId);
        }
    }

    public class InsertReconciliationCommand
    {
        [JsonIgnore]
        public long Id { get; set; }
        public long CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}