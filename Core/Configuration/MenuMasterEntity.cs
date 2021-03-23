using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Configuration
{
    public class MenuMasterEntity
    {
        public int MenuMasterId { get; set; }

        public string MenuName { get; set; }

        public Nullable<int> ParentId { get; set; }

        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }


        //SubMenu
        public Nullable<int> ParentMenuId { get; set; }
        public int SubMenuId { get; set; }
        public string ParentMenuName { get; set; }
        public string SubMenuName { get; set; }
        public string TargetURL { get; set; }

        //Mapping

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int MenuRoleBranchMappingId { get; set; }
    }
}