using System;
using Householder.Server.Models;

namespace Householder.Server.Expenses
{
    public class UpdateExpenseCommand
    {
        public long Id { get; set; }
        public long ResidentId { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
    }
}