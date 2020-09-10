using System.Collections.Generic;
using System.Threading.Tasks;
using Householder.Server.DataAccess;
using Householder.Server.Models;
using MySqlConnector;

namespace Householder.Server.Queries
{
    public class GetAllExpensesQueryHandler : IQueryHandler<GetAllExpensesQuery, IEnumerable<Expense>>
    {
        private IMySqlDatabase database;

        public GetAllExpensesQueryHandler(IMySqlDatabase database)
        {
            this.database = database;
        }

        public async Task<IEnumerable<Expense>> Handle(GetAllExpensesQuery query)
        {
            var results = new List<Expense>();
            var limit = query.Limit;

            var cmd = database.Connection.CreateCommand();

            var limitQuery = query.Limit != null ? $" LIMIT {limit}" : "";

            var statusQuery = query.Status != null ? $" WHERE status_id={(int)(query.Status + 1)}" : "";

            var residentQuery = query.ResidentId != null ? ((statusQuery == "" ? " WHERE " : " AND ") + $"resident_id={query.ResidentId}") : "";

            cmd.CommandText = @"SELECT e.id, r.id AS resident_id, r.name AS resident_name, e.amount, e.transaction_date, e.note, e.status_id FROM `expense` e LEFT JOIN `resident` r ON r.id=e.resident_id " + statusQuery + residentQuery + limitQuery + ";";

            cmd.Parameters.Add(new MySqlParameter("@limit", limit));

            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                results.Add(new Expense(
                    reader.GetInt32("id"),
                    new Resident(reader.GetInt32("resident_id"), reader.GetString("resident_name")),
                    reader.GetDouble("amount"),
                    reader.GetDateTime("transaction_date"),
                    reader.GetString("note"),
                    (ExpenseStatus)(reader.GetInt32("status_id") - 1)
                ));
            }

            return results;
        }
    }
}