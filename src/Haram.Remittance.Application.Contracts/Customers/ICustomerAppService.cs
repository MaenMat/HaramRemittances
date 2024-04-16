using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Haram.Remittance.Customers
{
    public interface ICustomerAppService : IApplicationService
    {
        Task<CustomerDto> GetAsync(Guid id);

        Task<List<CustomerDto>> GetAllCustomersAsync();

        Task<PagedResultDto<CustomerDto>> GetListAsync(GetCustomerListDto input);

        Task<CustomerDto> CreateAsync(CreateCustomerDto input);

        Task UpdateAsync(Guid id, UpdateCustomerDto input);

        Task DeleteAsync(Guid id);
    }
}
