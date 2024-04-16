using AutoMapper.Internal.Mappers;
using Haram.Remittance.Currencies;
using Haram.Remittance.Customers;
using Haram.Remittance.Permissions;
using Haram.Remittance.Remmitances;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Haram.Remittance.Remittances
{
    public class RemittanceAppService : ApplicationService, IRemittanceAppService
    {
        private IRemittanceRepository _remittanceRepository;
        private ICustomerRepository _customerRepository;
        private CustomerManager _customerManager;
        private IRepository<Currency, Guid> _currencyRepository;
        private readonly IGuidGenerator _guidGenerator;
        public RemittanceAppService( IRemittanceRepository remittanceRepository,
            ICustomerRepository customerRepository,
            CustomerManager customerManager,
            IRepository<Currency, Guid> currencyRepository,
            IGuidGenerator guidGenerator )
        {
            _remittanceRepository = remittanceRepository;
            _customerRepository = customerRepository;
            _customerManager = customerManager;
            _currencyRepository = currencyRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task<PagedResultDto<RemittanceDto>> GetIssuedListAsync(PagedAndSortedResultRequestDto input)
        {
            var issuedRemittances = await _remittanceRepository.GetPagedIssuedRemittances(input.SkipCount, input.MaxResultCount, input.Sorting);
            var totalCount = await _remittanceRepository.CountIssuedRemittances();
            var issuedRemittancesDto = ObjectMapper.Map<List<RemittanceClass>, List<RemittanceDto>>(issuedRemittances);
            return new PagedResultDto<RemittanceDto>(totalCount, issuedRemittancesDto);
        }

        public async Task<PagedResultDto<RemittanceDto>> GetReadyListAsync(PagedAndSortedResultRequestDto input)
        {
            var readyRemittances = await _remittanceRepository.GetPagedReadyRemittances(input.SkipCount, input.MaxResultCount, input.Sorting);
            var totalCount = await _remittanceRepository.CountReadyRemittances();
            var readyRemittancesDto = ObjectMapper.Map<List<RemittanceClass>, List<RemittanceDto>>(readyRemittances);
            return new PagedResultDto<RemittanceDto>(totalCount, readyRemittancesDto);
        }

        public async Task<PagedResultDto<RemittanceDto>> GetApprovedReleasedListAsync(PagedAndSortedResultRequestDto input)
        {
            var ApprovedReleasedRemittances = await _remittanceRepository.GetPagedApprovedReleasedRemittances(input.SkipCount, input.MaxResultCount, input.Sorting);
            var totalCount = await _remittanceRepository.CountApprovedReleasedRemittances();
            var readyRemittancesDto = ObjectMapper.Map<List<RemittanceClass>, List<RemittanceDto>>(ApprovedReleasedRemittances);
            return new PagedResultDto<RemittanceDto>(totalCount, readyRemittancesDto);
        }

        [Authorize(RemittancePermissions.Remittances.Create)]
        public async Task<RemittanceDto> CreateAsync(CreateUpdateRemittanceDto input)
        {
            var Sender = await _customerRepository.GetAsync(input.SenderId);
            var Currency = await _currencyRepository.GetAsync(input.CurrencyId);
            if (!_customerManager.IsOver18(Sender.BirthDate))
            {
                    throw new CustomerUnderAgeException();
            }
            if (input.Type == RemmitanceType.Internal && Currency.Name != "SYP")
            {
                    throw new InternalRemittanceException();
            }
            if (input.Type == RemmitanceType.External && Currency.Name == "SYP")
            {
                    throw new ExternalRemittanceException();
            }
            var newRemittance = new RemittanceClass(_guidGenerator.Create());
            await _remittanceRepository.InsertAsync(newRemittance);
            ObjectMapper.Map(input, newRemittance);
            var CreationStatus = new RemittanceStatus(_guidGenerator.Create())
            {
                    RemittanceId = newRemittance.Id,
                    StatusDate = DateTime.Now,
                    Status = Status.Issued
            };
            newRemittance.TotalAmount = (float)(newRemittance.Amount * 1.05);
            newRemittance.HistoryStatus.Add(CreationStatus);
            newRemittance.IssuedBy = CurrentUser.UserName;
            await _remittanceRepository.UpdateAsync(newRemittance);
            var returnObj = ObjectMapper.Map<RemittanceClass, RemittanceDto>(newRemittance);
            return returnObj;
        }

        [Authorize(RemittancePermissions.Remittances.Edit)]
        public async Task UpdateAsync(Guid id, CreateUpdateRemittanceDto input)
        {
            var Sender = await _customerRepository.GetAsync(input.SenderId);
            var Currency = await _currencyRepository.GetAsync(input.CurrencyId);
            if (!_customerManager.IsOver18(Sender.BirthDate))
            {
                throw new CustomerUnderAgeException();
            }
            if (input.Type == RemmitanceType.Internal && Currency.Name != "SYP")
            {
                throw new InternalRemittanceException();
            }
            if (input.Type == RemmitanceType.External && Currency.Name == "SYP")
            {
                throw new ExternalRemittanceException();
            }
            var UpdatedRemittance = await _remittanceRepository.GetAsync(id);
            ObjectMapper.Map(input, UpdatedRemittance);
            UpdatedRemittance.TotalAmount = (float)(UpdatedRemittance.Amount * 1.05);
            await _remittanceRepository.UpdateAsync(UpdatedRemittance);
        }

        [Authorize(RemittancePermissions.Remittances.SetAsReady)]
        public async Task SetAsReadyAsync(Guid id)
        {
            var theRemittance = await _remittanceRepository.GetAsync(id);
            await _remittanceRepository.EnsureCollectionLoadedAsync(theRemittance, x => x.HistoryStatus);
            if (theRemittance.HistoryStatus.MaxBy(s => s.StatusDate).Status != Status.Issued)
            {
                throw new SetAsReadyException();
            }
            var ReadyStatus = new RemittanceStatus(_guidGenerator.Create())
            {
                RemittanceId = id,
                StatusDate = DateTime.Now,
                Status = Status.Ready
            };
            theRemittance.HistoryStatus.Add(ReadyStatus);
            await _remittanceRepository.UpdateAsync(theRemittance);
        }

        [Authorize(RemittancePermissions.Remittances.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var remittance = await _remittanceRepository.GetAsync(id, includeDetails: false);
            await _remittanceRepository.EnsureCollectionLoadedAsync(remittance, x => x.HistoryStatus);
            if (remittance.HistoryStatus.MaxBy(s => s.StatusDate)?.Status != Status.Issued)
            {
                throw new OnRemittanceDeletionException();
            }
            await _remittanceRepository.DeleteAsync(id);
        }

        [Authorize(RemittancePermissions.Remittances.Approve)]
        public async Task ApproveAsync(Guid id)
        {
            var theRemittance = await _remittanceRepository.GetAsync(id);
            await _remittanceRepository.EnsureCollectionLoadedAsync(theRemittance, x => x.HistoryStatus);
            var ApprovedStatus = new RemittanceStatus(_guidGenerator.Create())
            {
                RemittanceId = id,
                StatusDate = DateTime.Now,
                Status = Status.Approved
            };
            theRemittance.HistoryStatus.Add(ApprovedStatus);
            theRemittance.ApprovedBy = CurrentUser.UserName;
            await _remittanceRepository.UpdateAsync(theRemittance);
        }

        [Authorize(RemittancePermissions.Remittances.Release)]
        public async Task ReleaseAsync(Guid id, Guid receiver)
        {
            var theRemittance = await _remittanceRepository.GetAsync(id);
            await _remittanceRepository.EnsureCollectionLoadedAsync(theRemittance, x => x.HistoryStatus);
            await _remittanceRepository.EnsurePropertyLoadedAsync(theRemittance, x => x.Sender);
            var ReleasedStatus = new RemittanceStatus(_guidGenerator.Create())
            {
                RemittanceId = id,
                StatusDate = DateTime.Now,
                Status = Status.Released
            };
            theRemittance.HistoryStatus.Add(ReleasedStatus);
            theRemittance.ReleasedBy = CurrentUser.UserName;
            theRemittance.ReceiverId = receiver;
            await _remittanceRepository.UpdateAsync(theRemittance);
        }

        public async Task<PagedResultDto<ReportRemittanceDto>> GetReportListAsync(DateTime startDate, DateTime endDate, PagedAndSortedResultRequestDto input)
        {
            var ReportList = await _remittanceRepository.GetReportListAsync(startDate, endDate, input.SkipCount, input.MaxResultCount, input.Sorting);
            var ReportListDto = ObjectMapper.Map<List<ReportRemittance>, List<ReportRemittanceDto>>(ReportList);
            var Count = await _remittanceRepository.GetReportListCountAsync(startDate, endDate);
            return new PagedResultDto<ReportRemittanceDto>(Count, ReportListDto);
        }

        public async Task<List<RemittanceDto>> SearchByNameAsync(string? name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var SearchResult = await _remittanceRepository.filterByName(name);
                var SearchResultDto = ObjectMapper.Map<List<RemittanceClass>, List<RemittanceDto>>(SearchResult);
                return SearchResultDto;
            }
            else return null;
        }

        public async Task<RemittanceDto> SearchRemittanceBySerialNumber(string SerialNumber)
        {
            var theRemittance = await _remittanceRepository.SearchBySerialNumber(SerialNumber);
            //await _remittanceRepository.EnsureCollectionLoadedAsync(theRemittance, x => x.HistoryStatus);
            var theRemittanceDto = ObjectMapper.Map<RemittanceClass, RemittanceDto>(theRemittance);
            return theRemittanceDto;
        }
    }
}
