using Haram.Remittance.Permissions;
using Haram.Remittance.Remmitances;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Haram.Remittance.Currencies
{
    public class CurrencyAppService : CrudAppService<Currency,
        CurrencyDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateCurrencyDto >
        ,ICurrencyAppService
    {
        private readonly IRemittanceRepository _remittanceRepository;
        private readonly IRepository<Currency, Guid> _currencyRepository;

        public CurrencyAppService(IRepository<Currency, Guid> currencyRepository, IRemittanceRepository remittanceRepository) : base(currencyRepository)
        {
            _currencyRepository = currencyRepository;
            _remittanceRepository = remittanceRepository;
        }
        public async Task<List<CurrencyDto>> GetAllCurrenciesAsync()
        {
            var currencies = await _currencyRepository.GetListAsync();
            var currenciesDto = ObjectMapper.Map<List<Currency>, List<CurrencyDto>>(currencies);
            return currenciesDto;
        }
        [Authorize(RemittancePermissions.Currencies.Delete)]

        public async override Task DeleteAsync(Guid id)
        {
            var remittanceCount = await _remittanceRepository.CountAsync(r=>r.CurrencyId == id);
            if (remittanceCount > 0)
            {
                throw new CurrencyDeletionException();
            }
            else base.DeleteAsync(id);
            return;
        }
        [Authorize(RemittancePermissions.Currencies.Create)]
        public override Task<CurrencyDto> CreateAsync(CreateUpdateCurrencyDto input)
        {
            var ExistCurrency = _currencyRepository.FirstOrDefaultAsync(c => c.Name == input.Name || c.Symbol == input.Symbol);

            if (ExistCurrency != null)
            {
                throw new CurrencyAlreadyExistsException();
            }
            return base.CreateAsync(input);
        }
    }
}
