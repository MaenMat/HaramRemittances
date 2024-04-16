using Haram.Remittance.Currencies;
using Haram.Remittance.Customers;
using Haram.Remittance.Remittances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haram.Remittance.Remmitances
{
    public class ReportRemittance
    {
        public RemmitanceType Type { get; set; }
        public string SerialNumber { get; set; }
        public float Amount { get; set; }
        public float TotalAmount { get; set; }
        public Guid? SenderId { get; set; }
        public Customer Sender { get; set; }
        public Guid? ReceiverId { get; set; }
        public Customer Receiver { get; set; }
        public Guid CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public RemittanceStatus LatestStatusInRange { get; set; }
    }
}
