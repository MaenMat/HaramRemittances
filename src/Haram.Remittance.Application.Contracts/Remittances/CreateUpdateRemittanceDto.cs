using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Haram.Remittance.Currencies;
using Haram.Remittance.Customers;
using Volo.Abp.Domain.Entities;

namespace Haram.Remittance.Remittances
{
    public class CreateUpdateRemittanceDto 
    {
        [Required]
        public RemmitanceType Type { get; set; }
        [Required]
        public string SerialNumber { get; set; } = string.Empty;
        [Range(5000, 1000000000, ErrorMessage = "Please enter a value bigger than 5000")]
        public float Amount { get; set; }
        [Required(ErrorMessage = "Sender is required")]
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Cannot use default Guid")]
        public Guid SenderId { get; set; }
        [Required(ErrorMessage = "Currency is required")]
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Cannot use default Guid")]
        public Guid CurrencyId { get; set; }
        [Required]
        public string ReceiverName { get; set; }
        [Required]
        public string ReceiverPhone { get; set; }
       

    }
}
