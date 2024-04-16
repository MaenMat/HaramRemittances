﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Haram.Remittance.Customers
{
    public interface ICustomerRepository : IRepository<Customer,Guid>
    {
        Task<Customer> FindByNameCombinationAsync(string firstName, string lastName,string fatherName, string motherName);
        Task<List<Customer>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, string filter = null );
    }
}
