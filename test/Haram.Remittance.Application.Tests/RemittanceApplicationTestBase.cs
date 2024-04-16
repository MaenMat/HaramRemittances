using Volo.Abp.Modularity;

namespace Haram.Remittance;

public abstract class RemittanceApplicationTestBase<TStartupModule> : RemittanceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
