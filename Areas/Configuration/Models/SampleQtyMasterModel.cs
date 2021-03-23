using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class SampleQtyMasterModel
    {
        [Key]
        public int SampleQtyId { get; set; }
        public string SampleQty { get; set; }
        public string PrintDesc { get; set; }
        public string Preservation { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public Nullable<int> SampleTypeProductId { get; set; }
    }
}