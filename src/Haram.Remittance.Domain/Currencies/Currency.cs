using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Haram.Remittance.Currencies
{
    public class Currency : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}
