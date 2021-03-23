using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace LIMS_DEMO.Models
{
    public class MenuModel
    {
        public int MenuMasterId { get; set; }
        public int? ParentId { get; set; }
        public string Menu { get; set; }
        public string IconValues { get; set; }
        public string TargetUrl { get; set; }
        public bool IsActive { get; set; }
        public IList<MenuModel> SubMenu { get; set; }
    }
}