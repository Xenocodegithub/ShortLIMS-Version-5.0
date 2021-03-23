using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Configuration
{
    public class RoleMasterEntity
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public int UserMasterId { get; set; }
        public string UserName { get; set; }
        public int UserRoleId { get; set; }
        public int LabId { get; set; }
    }
}