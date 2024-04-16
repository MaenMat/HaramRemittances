using Volo.Abp.Modularity;

namespace Haram.Remittance;

[DependsOn(
    typeof(RemittanceDomainModule),
    typeof(RemittanceTestBaseModule)
)]
public class RemittanceDomainTestModule : AbpModule
{

}
