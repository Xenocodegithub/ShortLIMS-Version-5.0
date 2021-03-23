using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class SubGroupMasterModel
    {
        public int SampleTypeProductId { get; set; }

        [Display(Name = "Sample Type Product Name ")]
        public string SampleTypeProductName { get; set; }
        [Display(Name = "Sample Type Product Code ")]
        public string SampleTypeProductCode { get; set; }

        [Required(ErrorMessage = "Product Group field is Required")]
        public int ProductGroupId { get; set; }

        [Display(Name = "Product Group")]
        public string ProductGroupName { get; set; }

        public int SubGroupId { get; set; }

        [Display(Name = "Sub Group")]
        [Required(ErrorMessage = "Sub Group field is Required")]
        public string SubGroupName { get; set; }

        public string Description { get; set; }

        [Display(Name = "Sub Group Print Code ")]
        public string SubGroupPrintCode { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "All")]
        public bool All { get; set; }
    }
}