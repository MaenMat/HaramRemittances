using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Haram.Remittance.Currencies
{
    public class CurrencyDeletionException : BusinessException
    {
        public CurrencyDeletionException() : base(RemittanceDomainErrorCodes.OnCurrencyDeletionException)
        {
            
        }
    }
}
