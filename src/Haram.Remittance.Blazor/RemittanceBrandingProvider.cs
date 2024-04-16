using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Haram.Remittance.Blazor;

[Dependency(ReplaceServices = true)]
public class RemittanceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Remittance";
}
