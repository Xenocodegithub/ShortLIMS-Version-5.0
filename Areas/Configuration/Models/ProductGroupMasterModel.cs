using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class ProductGroupMasterModel
    {
        [Display(Name = "Sample Type Name")]
        [Required(ErrorMessage = "Please Enter Sample Type Product Name")]
        public int SampleTypeProductId { get; set; }
        [Display(Name = "Sample Type Name")]
        [Required(ErrorMessage = "SampleTypeProduct Name field is Required")]
        public string SampleTypeProductName { get; set; }
        [Required(ErrorMessage = "Please Enter Sample Type Product Code")]
        public string SampleTypeProductCode { get; set; }
        [Required(ErrorMessage = "Product Group field is Required")]
        public int ProductGroupId { get; set; }

        [Display(Name = "Product Group")]
        [Required(ErrorMessage = "Product Group field is Required")]
        public string ProductGroupName { get; set; }

        //public string Description { get; set; }

        public string Code { get; set; }

        //[Display(Name = "Is Disposal")]
        //public bool IsDisposal { get; set; }

        //[Display(Name = "Disposed Day")]
        //public string DisposedDay { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "All")]
        public bool All { get; set; }
    }
}