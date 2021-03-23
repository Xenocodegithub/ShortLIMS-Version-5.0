using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Inventory
{
    public class EmployeeEntity
    {
        public int UserDetailID { get; set; }

        public Nullable<int> UserMasterID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string UserName { get; set; }
        public string Salutation { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmpName { get; set; }
        public int EmpId { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string EmployeeCode { get; set; }
        public Nullable<int> MasterDesignationID { get; set; }
        public string MasterDesignation { get; set; }
        public string AreaOfExpertise { get; set; }
        public Nullable<decimal> ExperienceInYear { get; set; }
        public string Qualification { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Area { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string Pincode { get; set; }
        public string AlternateNumber { get; set; }
        public string ContactPersonName { get; set; }
        public string PANNo { get; set; }
        public string AdharNo { get; set; }
        public string PhotoUpload { get; set; }
        public string PANUpload { get; set; }
        public string AdharUpload { get; set; }
        public string AddressProofUpload { get; set; }


        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}