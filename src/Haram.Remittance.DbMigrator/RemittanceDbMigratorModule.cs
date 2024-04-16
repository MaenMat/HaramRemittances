using Haram.Remittance.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Haram.Remittance.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(RemittanceEntityFrameworkCoreModule),
    typeof(RemittanceApplicationContractsModule)
    )]
public class RemittanceDbMigratorModule : AbpModule
{
}
