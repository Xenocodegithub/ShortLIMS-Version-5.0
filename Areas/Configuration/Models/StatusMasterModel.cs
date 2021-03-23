using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class StatusMasterModel
    {
        [Key]
        public byte StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Module { get; set; }
        public Nullable<int> Hierarchy { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    }
}