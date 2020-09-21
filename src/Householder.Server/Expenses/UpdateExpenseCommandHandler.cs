using DbReader;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using Householder.Server.Database;
using Householder.Server.Models;
using MySqlConnector;
using Newtonsoft.Json;
using System;

namespace Householder.Server.Expenses
{
    public class UpdateExpenseCommandHandler : ICommandHandler<UpdateExpenseCommand>
    {
        private IDbConnection dbConnection;
        private ISqlProvider sqlProvider;

        public UpdateExpenseCommandHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task HandleAsync(UpdateExpenseCommand command, CancellationToken cancellationToken = default)
        {
            var rowsAffected = await dbConnection.ExecuteAsync(sqlProvider.UpdateExpense, command);
            command.RowsAffected = rowsAffected;
        }
    }

    public class UpdateExpenseCommand
    {
        [JsonIgnore]
        public long Id { get; set; }
        public long ResidentId { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public int RowsAffected { get; set; }
    }
}