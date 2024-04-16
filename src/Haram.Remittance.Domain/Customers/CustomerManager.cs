using System;
using System.Net;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Haram.Remittance.Customers
{
    public class CustomerManager : DomainService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Customer> CreateAsync(string firstName, string lastName, string fatherName, string motherName,
            DateTime birthDate, string phone, Gender gender, string? address = null)
        {
            Check.NotNullOrWhiteSpace(firstName, nameof(firstName));
            Check.NotNullOrWhiteSpace(lastName, nameof(lastName));
            Check.NotNullOrWhiteSpace(fatherName, nameof(fatherName));
            Check.NotNullOrWhiteSpace(motherName, nameof(motherName));
            Check.NotNull(birthDate, nameof(birthDate));
            Check.NotNullOrWhiteSpace(phone, nameof(phone));

            var existingCustomer = await _customerRepository.FindByNameCombinationAsync(firstName, lastName, fatherName, motherName);
            if (existingCustomer != null)
            {
                throw new CustomerAlreadyExistsException();
            }
            return new Customer(
                GuidGenerator.Create(),
                firstName, lastName, fatherName, motherName,
                birthDate, phone, gender, address
            );
        }
        public bool IsOver18(DateTime birthDate)
            {
            // Calculate the age by subtracting the birth date from the current date
            TimeSpan ageDifference = DateTime.Now - birthDate;

            // Convert the age difference to years
            int ageInYears = (int)(ageDifference.Days / 365.25);

            // Check if the age is greater than or equal to 18
            return ageInYears >= 18;
        }
    }
}
