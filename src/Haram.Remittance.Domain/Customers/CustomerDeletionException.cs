using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Haram.Remittance.Customers
{
    public class CustomerDeletionException : BusinessException
    {
        public CustomerDeletionException() : base(RemittanceDomainErrorCodes.OnCustomerDeletionException)
        {
            
        }
    }
}
