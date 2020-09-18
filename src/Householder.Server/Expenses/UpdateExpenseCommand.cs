using System;
using Householder.Server.Models;
using Newtonsoft.Json;

namespace Householder.Server.Expenses
{
    public class UpdateExpenseCommand
    {
        [JsonIgnore]
        public long Id { get; set; }
        public long ResidentId { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public int RowsAffected { get; set; }
    }
}