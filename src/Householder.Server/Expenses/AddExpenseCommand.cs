using System;
using CQRS.Command.Abstractions;
using Householder.Server.Models;

namespace Householder.Server.Expenses
{
    public class AddExpenseCommand
    {
        public long Id { get; set; }
        public long ResidentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Note { get; set; }
        public ExpenseStatus Status { get; set; }
    }
}