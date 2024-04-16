using Haram.Remittance.Currencies;
using Haram.Remittance.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Haram.Remittance
{
    public class HaramRemittanceDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Currency, Guid> _currencyRepository;
        private readonly ICustomerRepository _customerRepository;
        private CustomerManager _customerManager;
        public HaramRemittanceDataSeederContributor(IRepository<Currency, Guid> currencyRepository,
            ICustomerRepository customerRepository,
            CustomerManager customerManager)
        {
            _currencyRepository = currencyRepository;
            _customerRepository = customerRepository;
            _customerManager = customerManager;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _currencyRepository.CountAsync() <= 0 ) 
            {
                await _currencyRepository.InsertAsync(
                new Currency
                {
                    Name = "USD",
                    Symbol = "$"
                },
                autoSave: true );
                await _currencyRepository.InsertAsync(
                new Currency
                {
                    Name = "EUR",
                    Symbol = "€"
                },
                autoSave: true );
                await _currencyRepository.InsertAsync(
                new Currency
                {
                    Name = "GBP",
                    Symbol = "£"
                },
                autoSave: true );
                await _currencyRepository.InsertAsync(
               new Currency
               {
                   Name = "SYP",
                   Symbol = "L"
               },
               autoSave: true);
            }
            if (await _customerRepository.GetCountAsync() <= 0)
            {
                await _customerRepository.InsertAsync(
                    await _customerManager.CreateAsync(
                        "Caldoun","Matouk","Moe","Layla",new DateTime(1960,9,11),"0997777429",Gender.Male,"Eastern Mazzeh"
                    )
                );

                await _customerRepository.InsertAsync(
                     await _customerManager.CreateAsync(
                        "Nada", "Azmeh", "Maen", "Salma", new DateTime(1967,4,16), "0997777429", Gender.Male, "Western Mazzeh"
                    )
                );
            }
        }
    }
}
