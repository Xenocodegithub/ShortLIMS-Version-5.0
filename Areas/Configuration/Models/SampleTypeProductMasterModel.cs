using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class SampleTypeProductMasterModel
    {
        [Key]
        public int SampleTypeProductId { get; set; }
        public string SampleTypeProductName { get; set; }
        public string SampleTypeProductCode { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    }
}