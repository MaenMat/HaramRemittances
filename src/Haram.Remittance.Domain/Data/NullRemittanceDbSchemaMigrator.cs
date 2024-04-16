using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Haram.Remittance.Data;

/* This is used if database provider does't define
 * IRemittanceDbSchemaMigrator implementation.
 */
public class NullRemittanceDbSchemaMigrator : IRemittanceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
