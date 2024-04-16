using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Haram.Remittance.Customers
{
    public class Customer : FullAuditedAggregateRoot<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FatherName { get; private set; }
        public string MotherName { get; private set; }
        public DateTime BirthDate { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }

        private Customer()
        {

        }   
        internal Customer(Guid id, string firstName, string lastName, string fatherName,string motherName,
                          DateTime birthDate, string phone, Gender gender, string? address = null) 
        : base(id)
        {
            SetFullName(firstName,lastName,fatherName,motherName);
            BirthDate = birthDate;
            Address = address;
            Phone = phone;
            Gender = gender;
        }
        private void SetFullName(string firstName, string lastName, string fatherName, string motherName)
        {
            this.FirstName = Check.NotNullOrWhiteSpace(firstName, nameof(firstName), maxLength: CustomerConsts.MaxNameLength);
            this.LastName = Check.NotNullOrWhiteSpace(lastName, nameof(lastName), maxLength: CustomerConsts.MaxNameLength);
            this.FatherName = Check.NotNullOrWhiteSpace(fatherName, nameof(fatherName), maxLength: CustomerConsts.MaxNameLength);
            this.MotherName = Check.NotNullOrWhiteSpace(motherName, nameof(motherName), maxLength: CustomerConsts.MaxNameLength);
        }
    }
}
