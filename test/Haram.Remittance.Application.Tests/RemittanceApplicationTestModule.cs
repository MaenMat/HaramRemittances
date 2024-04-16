using Volo.Abp.Modularity;

namespace Haram.Remittance;

[DependsOn(
    typeof(RemittanceApplicationModule),
    typeof(RemittanceDomainTestModule)
)]
public class RemittanceApplicationTestModule : AbpModule
{

}
