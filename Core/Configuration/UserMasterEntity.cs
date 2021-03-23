using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Configuration
{
    public class UserMasterEntity
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
        public int LabId { get; set; }
        public Nullable<int> MasterDesignationID { get; set; }
        public string MasterDesignation { get; set; }
    }
}