using Scriban.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Haram.Remittance.Customers
{
    public class CustomerAlreadyExistsException : BusinessException
    {
        public CustomerAlreadyExistsException() : base(RemittanceDomainErrorCodes.CustomerAlreadyExists)
        { }
    }
}
