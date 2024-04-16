using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Haram.Remittance.Currencies
{
    public class CurrencyAlreadyExistsException : BusinessException
    {
        public CurrencyAlreadyExistsException() : base(RemittanceDomainErrorCodes.CurrencyAlreadyExists)
        { }
    }
}
