using DbReader;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using Householder.Server.Database;
using Householder.Server.Models;

namespace Householder.Server.Expenses
{
    public class GetExpensesQueryHandler : IQueryHandler<GetExpensesQuery, IEnumerable<Expense>>
    {
        private IDbConnection dbConnection;
        private ISqlProvider sqlProvider;

        public GetExpensesQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<IEnumerable<Expense>> HandleAsync(GetExpensesQuery query, CancellationToken cancellationToken)
        {
            var results = await dbConnection.ReadAsync<Expense>(sqlProvider.GetExpenses, query);

            return results;
        }
    }
}