using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace LIMS_DEMO.Areas.Enquiry.Models
{
    public class Parameters
    {
        [Display(Name = "Sr.No.")]
        public string SrNo { get; set; }

        [Display(Name = "Product Group")]
        public string ProductGroup { get; set; }

        [Display(Name = "Sub Group ")]
        public string SubGroup { get; set; }

        [Display(Name = "Matrix")]
        public string Matrix { get; set; }

        [Display(Name = "Action")]
        public string Action { get; set; }
    }
}