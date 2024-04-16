using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Haram.Remittance.Remittances
{
    public interface IRemittanceAppService : IApplicationService
    {
        Task<PagedResultDto<RemittanceDto>> GetIssuedListAsync(PagedAndSortedResultRequestDto input);
        Task<PagedResultDto<RemittanceDto>> GetReadyListAsync(PagedAndSortedResultRequestDto input);
        Task<PagedResultDto<RemittanceDto>> GetApprovedReleasedListAsync(PagedAndSortedResultRequestDto input);
        Task<RemittanceDto> CreateAsync(CreateUpdateRemittanceDto input);
        Task UpdateAsync(Guid id, CreateUpdateRemittanceDto input);
        Task SetAsReadyAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task ApproveAsync(Guid id);
        Task ReleaseAsync(Guid id, Guid receiver);
        Task<PagedResultDto<ReportRemittanceDto>> GetReportListAsync(DateTime startDate, DateTime endDate, PagedAndSortedResultRequestDto input);
        Task<List<RemittanceDto>> SearchByNameAsync(string? name);
        Task<RemittanceDto> SearchRemittanceBySerialNumber(string SerialNumber);
    }
}
