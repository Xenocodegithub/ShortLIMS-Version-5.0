using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.AppSettings
{
    public class MenuEntity
    {
        public int MenuMasterId { get; set; }
        public int? ParentId { get; set; }
        public string Menu { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string TargetUrl { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<MenuEntity> SubMenu { get; set; }
    }
}