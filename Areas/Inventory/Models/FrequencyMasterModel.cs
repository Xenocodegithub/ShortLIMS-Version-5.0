using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class FrequencyMasterModel
    {
        [Key]
        public byte FrequencyMasterID { get; set; }
        public string Frequency { get; set; }
        public string PrintDesc { get; set; }
        public Nullable<int> NumFreq { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    }
}