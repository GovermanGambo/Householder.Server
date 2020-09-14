using DbReader;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using Householder.Server.Database;
using Householder.Server.Models;
using System.Linq;

namespace Householder.Server.Expenses
{
    public class GetExpenseQueryHandler : IQueryHandler<GetExpenseQuery, Expense>
    {
        private IDbConnection dbConnection;
        private ISqlProvider sqlProvider;

        public GetExpenseQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<Expense> HandleAsync(GetExpenseQuery query, CancellationToken cancellationToken)
        {
            var results = await dbConnection.ReadAsync<Expense>(sqlProvider.GetExpense, query);

            return results.SingleOrDefault();
        }
    }
}