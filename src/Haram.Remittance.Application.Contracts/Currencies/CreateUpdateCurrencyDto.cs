using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Haram.Remittance.Currencies
{
    public class CreateUpdateCurrencyDto
        {

            [Required]
            public string Name { get; set; } = string.Empty;
            [Required]
            public string Symbol { get; set; } = string.Empty;

        }
    
}
