using Haram.Remittance.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Haram.Remittance.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class RemittanceController : AbpControllerBase
{
    protected RemittanceController()
    {
        LocalizationResource = typeof(RemittanceResource);
    }
}
