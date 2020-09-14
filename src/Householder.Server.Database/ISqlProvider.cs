namespace Householder.Server.Database
{
    public interface ISqlProvider
    {
        string CreateDatabase { get; }
        string GetLastInsertId { get; }
        string InsertResident { get; }
        string GetResident { get; }
        string GetResidents { get; }
        string GetResidentId { get; }
        string GetExpense { get; }
        string GetExpenses { get; }
        string InsertExpense { get; }
        string UpdateExpense { get; }
        string DeleteExpense { get; }
    }
}