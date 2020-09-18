using System.Collections.Generic;
using CQRS.Query.Abstractions;
using Householder.Server.Models;

namespace Householder.Server.Residents
{
    public class GetResidentsQuery : IQuery<IEnumerable<Resident>>
    {
    }
}