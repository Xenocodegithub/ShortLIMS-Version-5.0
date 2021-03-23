using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Models
{
    public class LoginModel
    {
        [Display(Name ="User Name")]
        [Required(ErrorMessage ="Please Enter User Name")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int UserMasterID { get; set; }
    }
}