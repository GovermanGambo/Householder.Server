using Newtonsoft.Json;

namespace Householder.Server.Models
{
    public class Settlement
    {
        [JsonProperty("id")]
        public int ID { get; }
        [JsonProperty("creditor")]
        public Resident Creditor { get; }
        [JsonProperty("debtor")]
        public Resident Debtor { get; }
        [JsonProperty("amount")]
        public decimal Amount { get; }
        [JsonProperty("status")]
        public SettlementStatus Status { get; }

        public Settlement(Resident creditor, Resident debtor, decimal amount, SettlementStatus status)
        {
            ID = -1;
            Creditor = creditor;
            Debtor = debtor;
            Amount = amount;
            Status = status;
        }

        [JsonConstructor]
        public Settlement(int id, Resident creditor, Resident debtor, decimal amount, SettlementStatus status)
        {
            ID = id;
            Creditor = creditor;
            Debtor = debtor;
            Amount = amount;
            Status = status;
        }
    }
}