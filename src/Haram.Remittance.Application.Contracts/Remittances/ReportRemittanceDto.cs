using Haram.Remittance.Currencies;
using Haram.Remittance.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Haram.Remittance.Remittances
{
    public class ReportRemittanceDto
    {
        public RemmitanceType Type { get; set; }
        public string SerialNumber { get; set; }
        public float Amount { get; set; }
        public float TotalAmount { get; set; }
        public Guid? SenderId { get; set; }
        public CustomerDto Sender { get; set; }
        public Guid? ReceiverId { get; set; }
        public CustomerDto Receiver { get; set; }
        public Guid CurrencyId { get; set; }
        public CurrencyDto? Currency { get; set; }
        public RemittanceStatusDto LatestStatusInRange { get; set; }
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
    }
}
