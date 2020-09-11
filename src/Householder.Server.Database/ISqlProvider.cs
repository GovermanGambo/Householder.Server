namespace Householder.Server.Database
{
    public interface ISqlProvider
    {
        string CreateDatabase { get; }
        string InsertResident { get; }
        string GetResident { get; }
        string GetResidents { get; }
        string GetResidentId { get; }

    }
}