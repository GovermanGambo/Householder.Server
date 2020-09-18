using System.Collections.Generic;
using CQRS.Query.Abstractions;
using Householder.Server.Models;

namespace Householder.Server.Residents
{
    public class GetResidentQuery : IQuery<Resident>
    {
        public long Id { get; set; }
    }
}