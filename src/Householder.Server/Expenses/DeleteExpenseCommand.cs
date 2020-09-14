namespace Householder.Server.Expenses
{
    public class DeleteExpenseCommand
    {
        public long Id { get; set; }
        public int RowsAffected { get; set; }
    }
}