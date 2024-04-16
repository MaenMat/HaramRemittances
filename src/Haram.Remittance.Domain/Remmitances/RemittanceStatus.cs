using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Haram.Remittance.Remittances
{
    public class RemittanceStatus : Entity<Guid>
    {
        public Guid RemittanceId { get; set; }
        public RemittanceClass Remittance { get; set; }
        public DateTime StatusDate { get; set; }
        public Status Status{ get; set; }
        private RemittanceStatus()
        {
            
        }
        public RemittanceStatus(Guid id): base(id) 
        {
            
        }
    }
}
