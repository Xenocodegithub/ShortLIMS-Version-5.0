using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Customer
{
  public  class CustomerEntity
    {
        public int CustomerMasterId { get; set; }
        public byte CustomerTypeId { get; set; }
        public Nullable<byte> CompanyTypeId { get; set; }
        public byte CustomerGroupId { get; set; }
        public Nullable<System.DateTime> RegistrationDate { get; set; }
        public string RegistrationName { get; set; }
        public string NameTitle { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactDesignation { get; set; }
        public string ContactMobileNo { get; set; }
        public string ContactEmail { get; set; }
        public string Address1 { get; set; }
        public string WardNo { get; set; }
        public string Taluka { get; set; }
        public string Village { get; set; }
        public string Area { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string Pincode { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public Nullable<bool> IsIndividual { get; set; }
        public Nullable<bool> IsCompany { get; set; }
        public string PANNo { get; set; }
        public string PANUpload { get; set; }
        public string AddressProofUpload { get; set; }
        public string CINNumber { get; set; }
        public string FaxNo { get; set; }
        public string GSTNo { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
