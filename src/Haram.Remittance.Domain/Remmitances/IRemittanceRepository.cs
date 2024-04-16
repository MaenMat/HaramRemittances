using Haram.Remittance.Remittances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Haram.Remittance.Remmitances
{
    public interface IRemittanceRepository : IRepository<RemittanceClass,Guid>
    {
        Task<List<RemittanceClass>> GetPagedIssuedRemittances(int skipCount, int maxResultCount, string? sorting);
        Task<int> CountIssuedRemittances();
        Task<List<RemittanceClass>> GetPagedReadyRemittances(int skipCount, int maxResultCount, string? sorting);
        Task<int> CountReadyRemittances();
        Task<List<RemittanceClass>> GetPagedApprovedReleasedRemittances(int skipCount, int maxResultCount, string? sorting);
        Task<int> CountApprovedReleasedRemittances();
        Task<List<ReportRemittance>> GetReportListAsync(DateTime StrDate, DateTime EndDate, int skipCount, int maxResultCount, string? sorting);
        Task<int> GetReportListCountAsync(DateTime StrDate, DateTime EndDate);
        Task<RemittanceClass> SearchBySerialNumber(string SerialNumber);
        Task<List<RemittanceClass>> filterByName(string name);
    }
}
