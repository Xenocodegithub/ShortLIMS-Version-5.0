using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.LIMS
{
    public static class User
    {
        public static int ForgotpassCount { get; set; }
        public static int TotalEnquiryCount { get; set; }
        public static int TotalWOCount { get; set; }
        public static int TotalSampleTestedCount { get; set; }
        public static int TotalWOComp { get; set; }
        public static int TotalWOExe { get; set; }
        public static int TotalQuotationCount { get; set; }
        public static decimal? TotalPOAmount { get; set; }
        public static int TotalSampleCollectedCount { get; set; }
        public static decimal? TotalRevenue { get; set; }
        public static int NotifCount { get; set; }
        public static int RoleId { get; set; }
        public static string RoleName { get; set; }
        public static int UserDetailID { get; set; }
        public static int UserMasterID { get; set; }
        public static string UserName { get; set; }
        public static string Salutation { get; set; }
        public static string FirstName { get; set; }
        public static string MiddleName { get; set; }
        public static string LastName { get; set; }
        public static string Gender { get; set; }
        public static Nullable<System.DateTime> DateOfBirth { get; set; }
        public static string Email { get; set; }
        public static string Mobile { get; set; }
        public static string EmployeeCode { get; set; }
        public static int LabId { get; set; }
    }
}