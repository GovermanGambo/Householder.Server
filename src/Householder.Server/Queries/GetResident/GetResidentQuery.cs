using Householder.Server.Models;

namespace Householder.Server.Queries
{
    public class GetResidentQuery : IQuery<Resident>
    {
        public int ID { get; }

        public GetResidentQuery(int id)
        {
            ID = id;
        }
    }
}