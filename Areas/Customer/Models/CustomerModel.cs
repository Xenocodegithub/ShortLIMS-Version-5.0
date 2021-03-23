using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace LIMS_DEMO.Areas.Customer.Models
{
    public class CustomerModel
    {
       [Display(Name = "Customer Code")]
            public int CustomerMasterId { get; set; }

            [Display(Name = "Customer Type")]
            [Required(ErrorMessage = "Please Select Customer Type.")]
            public int CustomerTypeId { get; set; }
            [Required(ErrorMessage = "Please Select Company Type.")]
            [Display(Name = "Company Type")]
            public int? CompanyTypeId { get; set; }

            [Display(Name = "Customer Group")]
            [Required(ErrorMessage = "Please Select Customer Group.")]
            public int CustomerGroupId { get; set; }

            [Display(Name = "Registration Date")]
            [Required(ErrorMessage = "Registration Date Required.")]
            public Nullable<System.DateTime> RegistrationDate { get; set; }

            [Display(Name = "Registration Name")]
            [Required(ErrorMessage = "Please Enter Registration Name.")]
            public string RegistrationName { get; set; }
            public string NameTitle { get; set; }

            [Display(Name = "Contact Person Name")]
            [Required(ErrorMessage = "Please Enter Contact Person Name.")]
            public string ContactPersonName { get; set; }

            [Display(Name = "Contact Person Designation")]
            public string ContactDesignation { get; set; }

            [Display(Name = "Contact Person Mobile No")]
            //[Required(ErrorMessage = "Please Enter Mobile Number.")]
            [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Valid MobileNumber")]
            public string ContactMobileNo { get; set; }

            [Display(Name = "Contact Person Email")]
            [DataType(DataType.EmailAddress)]
            //[Required(ErrorMessage = "Please Enter Email.")]
            [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
            public string ContactEmail { get; set; }

            [Required(ErrorMessage = "Please Enter Address.")]
            public string Address1 { get; set; }

            [Display(Name = "Ward No")]
            public string WardNo { get; set; }
            public string Taluka { get; set; }
            public string Village { get; set; }
            public string LandMark { get; set; }

            [Display(Name = "City")]
            [Required(ErrorMessage = "Please Enter City.")]
            public string CityName { get; set; }

            [Display(Name = "State")]
            [Required(ErrorMessage = "Please Enter State.")]
            public string StateName { get; set; }

            [Display(Name = "Country")]
            [Required(ErrorMessage = "Please Enter Country.")]
            public string CountryName { get; set; }

            [RegularExpression(@"^([0-9]{6})$", ErrorMessage = "Please Enter Valid Pincode")]
            [Required(ErrorMessage = "Please Enter Pincode.")]
            public string Pincode { get; set; }

            [Display(Name = "Phone No")]
            //[RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Please Enter Valid MobileNumber")]
            [DataType(DataType.PhoneNumber)]
            [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
            public string PhoneNo { get; set; }

            [Display(Name = "Mobile No")]
            //[Required(ErrorMessage = "Please Enter Mobile Number.")]
            [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Valid MobileNumber")]
            public string MobileNo { get; set; }

            // [Required(ErrorMessage = "Please Enter Email.")]
            [DataType(DataType.EmailAddress)]
            [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
            public string Email { get; set; }

            [Display(Name = "PAN No")]
            public string PANNo { get; set; }

            [Display(Name = "PAN Upload")]
            public string PANUpload { get; set; }

            [Display(Name = "Address Proof Upload")]
            public string AddressProofUpload { get; set; }

            [Display(Name = "CIN No")]
            public string CINNumber { get; set; }

            [Display(Name = "FAX No")]
            public string FaxNo { get; set; }

            [Display(Name = "GST No")]
            public string GSTNo { get; set; }

            [Display(Name = "Is Active")]

            public bool IsActive { get; set; }
            public string Individual { get; set; }

            public string Company { get; set; }


            public string CompanyType { get; set; }

            public int EnteredBy { get; set; }
            public System.DateTime EnteredDate { get; set; }
            public Nullable<int> ModifiedBy { get; set; }
            public byte[] ModifiedDate { get; set; }
        
    }
}
