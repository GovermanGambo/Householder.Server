namespace Householder.Server.Database
{
    public interface ISqlProvider
    {
        string CreateDatabase { get; }
        string GetLastInsertId { get; }
        string InsertResident { get; }
        string GetResident { get; }
        string GetResidents { get; }
        string GetExpensesByResident { get; }
        string GetExpensesByStatus { get; }
        string GetExpensesByResidentAndStatus { get; }
        string GetResidentId { get; }
        string GetExpense { get; }
        string GetExpenses { get; }
        string InsertExpense { get; }
        string UpdateExpense { get; }
        string DeleteExpense { get; }
        string GetSettlements { get; }
        string GetSettlementById { get; }
        string InsertSettlement { get; }
        string UpdateSettlementStatus { get; }
        string InsertReconciliation { get; }
        string UpdateExpenseStatus { get; }
        string GetReconciliations { get; }
        string GetSettlementsByReconciliationId { get; }
        string GetReconciliationById { get; }
    }
}