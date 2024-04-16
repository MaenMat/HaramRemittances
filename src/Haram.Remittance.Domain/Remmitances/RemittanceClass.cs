using Haram.Remittance.Currencies;
using Haram.Remittance.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Haram.Remittance.Remittances
{
    public class RemittanceClass : AuditedAggregateRoot<Guid>
    {
        public RemmitanceType Type { get; set; }
        public string SerialNumber { get; set; } 
        public float Amount { get; set; }
        public float TotalAmount { get; set; }
        public string? IssuedBy { get; set; } 
        public string? ApprovedBy { get; set; } 
        public string? ReleasedBy { get; set; }
        public Guid? SenderId { get; set; } 
        public Customer Sender { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public Guid? ReceiverId { get; set; }
        public Customer Receiver {  get; set; }
        public ICollection<RemittanceStatus> HistoryStatus { get; set; } = new List<RemittanceStatus>();
        public Guid CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        private RemittanceClass()
        {
            
        }
        public RemittanceClass(Guid id) : base(id)
        {
            
        }
    }
}
