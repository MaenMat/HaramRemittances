using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Haram.Remittance.Currencies
{
    public class CurrencyDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}
