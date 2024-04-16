using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Haram.Remittance.Currencies
{
    public interface ICurrencyAppService : ICrudAppService<CurrencyDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateCurrencyDto>
    {
        Task<List<CurrencyDto>> GetAllCurrenciesAsync();
    }
}
