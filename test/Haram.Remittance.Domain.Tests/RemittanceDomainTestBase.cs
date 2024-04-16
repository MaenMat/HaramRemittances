using Volo.Abp.Modularity;

namespace Haram.Remittance;

/* Inherit from this class for your domain layer tests. */
public abstract class RemittanceDomainTestBase<TStartupModule> : RemittanceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
