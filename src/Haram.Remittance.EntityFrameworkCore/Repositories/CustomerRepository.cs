using Haram.Remittance.Customers;
using Haram.Remittance.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
namespace Haram.Remittance.Repositories
{
    public class CustomerRepository : EfCoreRepository<RemittanceDbContext, Customer, Guid>, ICustomerRepository
    {
        public CustomerRepository(
        IDbContextProvider<RemittanceDbContext> dbContextProvider)
        : base(dbContextProvider)
        {
        }
        public async Task<Customer> FindByNameCombinationAsync(string firstName, string lastName, string fatherName, string motherName)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(customer => customer.FirstName == firstName
            && customer.LastName == lastName
            && customer.FatherName == fatherName
            && customer.MotherName == motherName);
        }

        public async Task<List<Customer>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.WhereIf(
            !filter.IsNullOrWhiteSpace(),
                    customer => customer.LastName.Contains(filter))
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();

        }
    }
}
