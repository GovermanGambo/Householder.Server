using DbReader;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using Householder.Server.Database;

namespace Householder.Server.Expenses
{
    public class DeleteExpenseCommandHandler : ICommandHandler<DeleteExpenseCommand>
    {
        private readonly IDbConnection dbConnection;
        private readonly ISqlProvider sqlProvider;

        public DeleteExpenseCommandHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task HandleAsync(DeleteExpenseCommand command, CancellationToken cancellationToken = default)
        {
            var rowsAffected = await dbConnection.ExecuteAsync(sqlProvider.DeleteExpense, command);

            command.RowsAffected = rowsAffected;
        }
    }

    public class DeleteExpenseCommand
    {
        public long Id { get; set; }
        public int RowsAffected { get; set; }
    }
}