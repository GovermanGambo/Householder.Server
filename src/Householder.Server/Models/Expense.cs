using Newtonsoft.Json;
using System;

namespace Householder.Server.Models
{
    public class Expense
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("residentId")]
        public long ResidentId;
        [JsonProperty("residentName")]
        public string ResidentName;
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("date")]
        public DateTime TransactionDate { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
    }
}