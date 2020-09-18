using CQRS.Command.Abstractions;
using System.Threading.Tasks;
using Householder.Server.DataAccess;
using Householder.Server.Models;
using MySqlConnector;
using System.Threading;
using System.Data;
using Householder.Server.Database;
using DbReader;

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
}