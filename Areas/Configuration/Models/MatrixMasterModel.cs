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
        public string MatrixName { get; set; }
        public string MatrixCode { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public Nullable<int> SampleTypeProductId { get; set; }
        public string SampleTypeProductName { get; set; }
        public string ProductGroupName { get; set; }
        public string SubGroupName { get; set; }
    }
}