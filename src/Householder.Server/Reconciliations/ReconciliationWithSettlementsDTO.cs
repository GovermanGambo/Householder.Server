using System.Collections.Generic;
using Householder.Server.Models;
using Householder.Server.Settlements;

namespace Householder.Server.Reconciliations
{
    public class ReconciliationWithSettlementsDTO
    {
        public long Id { get; set; }
        public long CreatorId { get; set; }
        public string CreatorName { get; set; }
        public IEnumerable<SettlementDTO> Settlements { get; set; }
    }
}