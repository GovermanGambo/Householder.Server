using System.Threading.Tasks;
using Householder.Server.DataAccess;
using Householder.Server.Models;
using MySqlConnector;

namespace Householder.Server.Commands
{
    public class UpdateExpenseDetailsCommandHandler : ICommandHandler<UpdateExpenseDetailsCommand, bool>
    {
        private IMySqlDatabase database;

        public UpdateExpenseDetailsCommandHandler(IMySqlDatabase database)
        {
            this.database = database;
        }

        public async Task<bool> Handle(UpdateExpenseDetailsCommand command)
        {
            var id = command.ExpenseId;
            var residentId = command.Expense.Payee.ID;
            var amount = command.Expense.Amount;
            var transactionDate = command.Expense.Date;
            var note = command.Expense.Note;

            var cmd = database.Connection.CreateCommand();

            cmd.CommandText = @"UPDATE expense SET resident_id = @residentId, amount = @amount, transaction_date = @transactionDate, note = @note WHERE id = @id";

            cmd.Parameters.Add(new MySqlParameter("@id", id));
            cmd.Parameters.Add(new MySqlParameter("@residentId", residentId));
            cmd.Parameters.Add(new MySqlParameter("@amount", amount));
            cmd.Parameters.Add(new MySqlParameter("@transactionDate", transactionDate));
            cmd.Parameters.Add(new MySqlParameter("@note", note));

            var affectedRows = await cmd.ExecuteNonQueryAsync();

            return (affectedRows == 1);
        }
    }
}