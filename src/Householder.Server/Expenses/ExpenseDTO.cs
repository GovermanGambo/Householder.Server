using System;

namespace Householder.Server.Expenses
{
    public class ExpenseDTO
    {
        public long Id { get; set; }
        public long ResidentId { get; set; }
        public string ResidentName { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
    }
}