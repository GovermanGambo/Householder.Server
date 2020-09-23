using DbReader;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Query.Abstractions;
using Householder.Server.Database;
using System.Linq;

namespace Householder.Server.Expenses
{
    public class GetExpenseQueryHandler : IQueryHandler<GetExpenseQuery, ExpenseDTO>
    {
        private IDbConnection dbConnection;
        private ISqlProvider sqlProvider;

        public GetExpenseQueryHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task<ExpenseDTO> HandleAsync(GetExpenseQuery query, CancellationToken cancellationToken)
        {
            var results = await dbConnection.ReadAsync<ExpenseDTO>(sqlProvider.GetExpense, query);

            return results.SingleOrDefault();
        }
    }

    public class GetExpenseQuery : IQuery<ExpenseDTO>
    {
        public long Id { get; set; }
    }
}