using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class MenuMasterModel
    {
        [Required(ErrorMessage = "Menu field is Required")]
        public int MenuMasterId { get; set; }

        [Display(Name = "Menu Name")]
        [Required(ErrorMessage = "Menu field is Required")]
        public string Menu { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        public int UserMasterId { get; set; }
        public string UserName { get; set; }
        public int ParentId { get; set; }

        [Required(ErrorMessage = "Target URL field is Required")]
        [Display(Name = "Target Url")]
        public string TargetUrl { get; set; }

        //public string Description { get; set; }

        //public string Code { get; set; }

        //[Display(Name = "Is Disposal")]
        //public bool IsDisposal { get; set; }

        //[Display(Name = "Disposed Day")]
        //public string DisposedDay { get; set; }


        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Is Child")]
        public bool IsChild { get; set; }
        [Display(Name = "All")]
        public bool All { get; set; }

        [Display(Name = "ParentMenu Name")]
        public string ParentMenu { get; set; }

        [Display(Name = "SubMenu Name")]
        [Required(ErrorMessage = "Please enter SubMenu")]
        public string SubMenu { get; set; }
        [Required(ErrorMessage = "ParentMenu Name field is Required")]
        public int ParentMenuId { get; set; }
        public int SubMenuId { get; set; }

        //Role
        [Required(ErrorMessage = "Role Name field is Required")]
        public int RoleId { get; set; }

        public int MenuRoleBarnchMappingId { get; set; }
    }
}