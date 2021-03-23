using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.DropDowns
{
    public class ParentMenuEntity
    {
        public int ParentMenuId { get; set; }
        public string ParentMenuName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
    }
}