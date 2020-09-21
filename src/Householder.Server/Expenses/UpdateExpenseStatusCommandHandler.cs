using DbReader;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using Householder.Server.Database;

namespace Householder.Server.Expenses
{
    public class UpdateExpenseStatusCommandHandler : ICommandHandler<UpdateExpenseStatusCommand>
    {
        private IDbConnection dbConnection;
        private ISqlProvider sqlProvider;

        public UpdateExpenseStatusCommandHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task HandleAsync(UpdateExpenseStatusCommand command, CancellationToken cancellationToken = default)
        {
            var rowsAffected = await dbConnection.ExecuteAsync(sqlProvider.UpdateExpenseStatus, command);
            command.RowsAffected = rowsAffected;
        }
    }

    public class UpdateExpenseStatusCommand
    {
        public long Id { get; set; }
        public ExpenseStatus Status { get; set; }
        public int RowsAffected { get; set; }
    }
}