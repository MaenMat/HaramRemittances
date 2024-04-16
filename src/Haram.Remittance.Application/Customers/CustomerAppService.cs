using Haram.Remittance.Currencies;
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

namespace Haram.Remittance.Customers
{
    public class CustomerAppService : RemittanceAppService, ICustomerAppService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerManager _customerManager;
        private readonly IRemittanceRepository _remittanceRepository;
        public CustomerAppService(ICustomerRepository _customerRepository, CustomerManager _customerManager,
            IRemittanceRepository remittanceRepository) 
        {
            this._customerRepository = _customerRepository;
            this._customerManager = _customerManager;
            _remittanceRepository = remittanceRepository;
        }
        public async Task<CustomerDto> GetAsync(Guid id)
        {
            var customer = await _customerRepository.GetAsync(id);
            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }
        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            try
            {
                var customers = await _customerRepository.GetListAsync();
                var CustomersListDto = ObjectMapper.Map<List<Customer>, List<CustomerDto>>(customers);
                return CustomersListDto;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public async Task<PagedResultDto<CustomerDto>> GetListAsync(GetCustomerListDto input)
        {

            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Customer.LastName);
            }
            var customers = await _customerRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);
            var totalCount = input.Filter == null ? await _customerRepository.CountAsync() : await _customerRepository.CountAsync(
            c => c.LastName.Contains(input.Filter));
            return new PagedResultDto<CustomerDto>(totalCount, ObjectMapper.Map<List<Customer>, List<CustomerDto>>(customers));
        }

        [Authorize(RemittancePermissions.Customers.Create)]
        public async Task<CustomerDto> CreateAsync(CreateCustomerDto input)
        {
            var newCustomer = await _customerManager.CreateAsync(input.FirstName,
                input.LastName,
                input.FatherName,
                input.MotherName,
                input.BirthDate,
                input.Phone,
                input.Gender,
                input.Address);
            await _customerRepository.InsertAsync(newCustomer);
            var customerDto = ObjectMapper.Map<Customer, CustomerDto>(newCustomer);
            return customerDto;
        }
       
        [Authorize(RemittancePermissions.Customers.Edit)]
        public async Task UpdateAsync(Guid id, UpdateCustomerDto input)
        {
            var customer = await _customerRepository.GetAsync(id);
            var updatedCustomer = ObjectMapper.Map<UpdateCustomerDto, Customer>(input,customer);
            await _customerRepository.UpdateAsync(updatedCustomer);
        }
        [Authorize(RemittancePermissions.Customers.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var remittanceCount = await _remittanceRepository.CountAsync(r => r.SenderId == id || r.ReceiverId == id);
            if (remittanceCount > 0)
            {
                throw new CustomerDeletionException();
            }
            await _customerRepository.DeleteAsync(id);
        }
    }
}
