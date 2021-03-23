using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Configuration
{
    public class EmployeeMasterEntity
    {
        public int UserID { get; set; }
        public string dbo { get; set; }
        public int UserDetailId { get; set; }
        public Nullable<int> UserMasterId { get; set; }
        public string UserName { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        //[RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string Email { get; set; }
        //[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Valid MobileNumber")]
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
        public bool IsActive { get; set; }
        public bool All { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> EnteredDate { get; set; }
    }
}