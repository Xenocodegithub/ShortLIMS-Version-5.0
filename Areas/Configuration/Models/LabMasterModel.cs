using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class LabMasterModel
    {
        [Key]
        public int LabMasterId { get; set; }
        public Nullable<int> ParentID { get; set; }
        public byte LabTypeId { get; set; }
        public byte BranchCategoryId { get; set; }
        public string LabBranchName { get; set; }
        public string LabBranchCode { get; set; }
        public string Description { get; set; }
        public Nullable<int> CountryCode { get; set; }
        public string ContactNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public System.DateTime SubscriptionDate { get; set; }
        public string SubscriptionKey { get; set; }
        public Nullable<bool> IsNABLAccredited { get; set; }
        public string ConformityAssessmentId { get; set; }
        public Nullable<System.DateTime> AccreditationValidity { get; set; }
        public string VerificationCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string WardNo { get; set; }
        public string Taluka { get; set; }
        public string Village { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string Pincode { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    }
}