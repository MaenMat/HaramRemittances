using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Haram.Remittance.Data;
using Volo.Abp.DependencyInjection;

namespace Haram.Remittance.EntityFrameworkCore;

public class EntityFrameworkCoreRemittanceDbSchemaMigrator
    : IRemittanceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreRemittanceDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the RemittanceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */
        await _serviceProvider
            .GetRequiredService<RemittanceDbContext>()
            .Database
            .MigrateAsync();
    }
}
