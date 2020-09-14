using Newtonsoft.Json;
using System;

namespace Householder.Server.Models
{
    public class Expense
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("payee")]
        public Resident Payee { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("status")]
        public ExpenseStatus Status { get; set; }
    }
}