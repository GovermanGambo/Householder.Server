using DbReader;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using Householder.Server.Database;
using Householder.Server.Models;
using MySqlConnector;

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
            await dbConnection.ExecuteAsync(sqlProvider.UpdateExpense, command);
        }
    }
}