using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Haram.Remittance.Remmitances
{
    public class OnRemittanceUpdateException : BusinessException
    {
        public OnRemittanceUpdateException() : base(RemittanceDomainErrorCodes.OnRemittanceUpdateException)
        {
            
        }
    }
}
