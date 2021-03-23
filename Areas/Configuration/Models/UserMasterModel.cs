using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class UserMasterModel
    {
        public int UserMasterID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string VerificationCode { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<System.DateTime> EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public Nullable<byte> DisciplineId { get; set; }
        public Nullable<bool> ResetActive { get; set; }
        public string DisciplineName { get; set; }
    }
}