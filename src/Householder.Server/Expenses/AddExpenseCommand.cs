using System;
using CQRS.Command.Abstractions;
using Householder.Server.Models;

namespace Householder.Server.Expenses
{
    public class AddExpenseCommand
    {
        public long Id { get; set; }
        public long PayeeId { get; set; }
        public string PayeeName { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public ExpenseStatus Status { get; set; }
    }
}