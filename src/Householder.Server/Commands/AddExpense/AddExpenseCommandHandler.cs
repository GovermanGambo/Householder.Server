using System.Threading.Tasks;
using Householder.Server.DataAccess;
using Householder.Server.Models;
using MySqlConnector;

namespace Householder.Server.Commands
{
    public class AddExpenseCommandHandler : ICommandHandler<AddExpenseCommand, long>
    {
        private IMySqlDatabase database;

        public AddExpenseCommandHandler(IMySqlDatabase database)
        {
            this.database = database;
        }

        public async Task<long> Handle(AddExpenseCommand command)
        {
            var expense = command.Expense;

            if (expense.Amount <= 0)
            {
                return -1;
            }

            var cmd = database.Connection.CreateCommand();

            cmd.CommandText = @"INSERT INTO `expense` (`resident_id`, `amount`, `transaction_date`, `note`, `status_id`) VALUES (@residentId, @amount, @transactionDate, @note, @status)";

            cmd.Parameters.Add(new MySqlParameter("@residentId", expense.Payee.ID));
            cmd.Parameters.Add(new MySqlParameter("@amount", expense.Amount));
            cmd.Parameters.Add(new MySqlParameter("@transactionDate", expense.Date));
            cmd.Parameters.Add(new MySqlParameter("@note", expense.Note));
            cmd.Parameters.Add(new MySqlParameter("@status", (int)expense.Status + 1));

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return cmd.LastInsertedId;
            }
            catch(MySqlException ex)
            {
                if (ex.ErrorCode == MySqlErrorCode.DataTooLong || ex.ErrorCode == MySqlErrorCode.CannotAddForeignConstraint)
                {
                    return -1;
                }
                else
                {
                    throw ex;
                }
            }
        }
    }
}