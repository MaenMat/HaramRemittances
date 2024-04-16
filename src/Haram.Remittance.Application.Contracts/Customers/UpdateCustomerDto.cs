using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Haram.Remittance.Customers
{
    public class UpdateCustomerDto 
    {
        [Required]
        [StringLength(CustomerConsts.MaxNameLength)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(CustomerConsts.MaxNameLength)]
        public string LastName { get; set; }
        [Required]
        [StringLength(CustomerConsts.MaxNameLength)]
        public string FatherName { get; set; }
        [Required]
        [StringLength(CustomerConsts.MaxNameLength)]
        public string MotherName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public string? Address { get; set; }
    }
}
