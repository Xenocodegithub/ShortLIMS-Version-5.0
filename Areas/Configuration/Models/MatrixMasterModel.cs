using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class MatrixMasterModel
    {
        [Key]
        public int MatrixId { get; set; }
        public int ProductGroupId { get; set; }
        public int SubGroupId { get; set; }
        [Required(ErrorMessage = "Please Enter Matrix Name")]
        public string MatrixName { get; set; }
        [Required(ErrorMessage = "Please Enter Matrix Code")]
        public string MatrixCode { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        [Required(ErrorMessage ="Please Select Sample Type Product Name")]
        public Nullable<int> SampleTypeProductId { get; set; }
        public string SampleTypeProductName { get; set; }
        [Required(ErrorMessage = "Please Select Product Group Name")]
        public string ProductGroupName { get; set; }
        [Required(ErrorMessage = "Please Select Sub Group Name")]
        public string SubGroupName { get; set; }
    }
}