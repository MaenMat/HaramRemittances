using System;
using System.Collections.Generic;
using System.Text;

namespace Haram.Remittance.Remittances
{
    public class RemittanceStatusDto
    {
        public Guid RemittanceId { get; set; }
        public DateTime StatusDate { get; set; }
        public Status Status { get; set; }
    }
}
