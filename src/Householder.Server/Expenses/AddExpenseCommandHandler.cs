using CQRS.Command.Abstractions;
using System.Threading.Tasks;
using MySqlConnector;
using System.Threading;
using System.Data;
using Householder.Server.Database;
using DbReader;
using System;

namespace Householder.Server.Expenses
{
    public class AddExpenseCommandHandler : ICommandHandler<AddExpenseCommand>
    {
        private IDbConnection dbConnection;
        private ISqlProvider sqlProvider;

        public AddExpenseCommandHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task HandleAsync(AddExpenseCommand command, CancellationToken cancellationToken = default)
        {
            await dbConnection.ExecuteAsync(sqlProvider.InsertExpense, command);
            command.Id = await dbConnection.ExecuteScalarAsync<long>(sqlProvider.GetLastInsertId);
        }
    }
    
    public class AddExpenseCommand
    {
        public long Id { get; set; }
        public long ResidentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
    }
}