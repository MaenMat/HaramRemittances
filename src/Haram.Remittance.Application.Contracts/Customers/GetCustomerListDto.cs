using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Haram.Remittance.Customers
{
    public class GetCustomerListDto : PagedAndSortedResultRequestDto
       {
            public string? Filter { get; set; }
       }
}
