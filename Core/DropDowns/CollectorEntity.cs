using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.DropDowns
{
    public class CollectorEntity
    {
        public int UserRoleId { get; set; }//tbl UserRole
        public int UserMasterId { get; set; }//tbl UserRole
        public int RoleId { get; set; }//tbl UserRole,RoleMaster
        public string RoleName { get; set; }//tbl RoleMaster
        public int UserMasterID { get; set; }//tbl UserMaster
        public string UserName { get; set; }//tbl UserMaster
        public string Password { get; set; }//tbl UserMaster
        public string VerificationCode { get; set; }//tbl UserMaster
        public bool IsActive { get; set; }//tbl UserRole
    }
}
