using System.Threading.Tasks;

namespace Haram.Remittance.Data;

public interface IRemittanceDbSchemaMigrator
{
    Task MigrateAsync();
}
