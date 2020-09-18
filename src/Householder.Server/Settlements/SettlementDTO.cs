namespace Householder.Server.Settlements
{
    public class SettlementDTO
    {
        public int Id { get; set; }
        public int ReconciliationId { get; set; }
        public int CreditorId { get; set; }
        public string CreditorName { get; set; }
        public int DebtorId { get; set; }
        public string DebtorName { get; set; }
        public decimal Amount { get; set; }
        public int StatusId { get; set; }
    }
}