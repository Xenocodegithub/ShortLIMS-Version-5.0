using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class RoleMasterModel
    {
       
        public int RoleId { get; set; }
        public int UserRoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public int UserMasterId { get; set; }
        public string UserName { get; set; }
    }
}