using System;
using System.Collections.Generic;
using System.Text;
using Haram.Remittance.Localization;
using Volo.Abp.Application.Services;

namespace Haram.Remittance;

/* Inherit your application services from this class.
 */
public abstract class RemittanceAppService : ApplicationService
{
    protected RemittanceAppService()
    {
        LocalizationResource = typeof(RemittanceResource);
    }
}
