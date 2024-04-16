using Haram.Remittance.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Haram.Remittance.Blazor;

public abstract class RemittanceComponentBase : AbpComponentBase
{
    protected RemittanceComponentBase()
    {
        LocalizationResource = typeof(RemittanceResource);
    }
}
