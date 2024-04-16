using Haram.Remittance.Currencies;
using Haram.Remittance.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Haram.Remittance.Remittances
{
    public class RemittanceDto : AuditedEntityDto<Guid>
    {
        public RemmitanceType Type { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public float Amount { get; set; }
        public float TotalAmount { get; set; }
        public string IssuedBy { get; set; } = string.Empty;
        public string ReleasedBy { get; set; } = string.Empty;
        public string ApprovedBy { get; set; } = string.Empty;
        public ICollection<RemittanceStatusDto> HistoryStatus { get; set; } = new List<RemittanceStatusDto>();
        public Guid SenderId { get; set; } = Guid.Empty;
        public CustomerDto Sender { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public Guid ReceiverId { get; set; } = Guid.Empty;
        public CustomerDto Receiver { get; set; }
        public Guid CurrencyId { get; set; } = Guid.Empty;
        public DateTime CreationTime{ get; set; }
        public CurrencyDto Currency { get; set; }
        public string SenderFullName
        {
            get
            {
                if (Sender != null)
                {
                    return Sender.FirstName + " " + Sender.LastName;
                }
                else return "Unknown";
            }
        }
        public RemittanceStatusDto CurrentStatus
        {
            get
            {
                if (HistoryStatus != null)
                {
                    return HistoryStatus.OrderByDescending(s => s.StatusDate).FirstOrDefault();
                }
                else return null;
            }
        }
    }
}
