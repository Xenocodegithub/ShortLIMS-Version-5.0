using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class EmployeeMasterModel
    {
        public string dbo { get; set; }
        public int UserDetailId { get; set; }
        public Nullable<int> UserMasterId { get; set; }
        public string UserName { get; set; }
        public string Salutation { get; set; }
        [Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Select Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please Select DOB")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string Email { get; set; }
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Valid MobileNumber")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Please Enter Employee Code")]
        public string EmployeeCode { get; set; }
        [Required(ErrorMessage = "Please Select Designation")]
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
        [RegularExpression(@"^([0-9]{6})$", ErrorMessage = "Please Enter Valid PinCode")]
        public string Pincode { get; set; }
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Valid MobileNumber")]
        public string AlternateNumber { get; set; }
        public string ContactPersonName { get; set; }
        public string PANNo { get; set; }
        [RegularExpression(@"^([0-9]{12})$", ErrorMessage = "Please Enter Valid Adhar Number")]
        public string AdharNo { get; set; }
        public bool IsActive { get; set; }
        public bool All { get; set; }
    }
}