using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class CustomerGroupMasterModel
    {
        [Key]
        public byte CustomerGroupId { get; set; }
        public string CustomerGroupType { get; set; }
        public bool IsActive { get; set; }
        public int Enteredby { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] modifiedDate { get; set; }
    }
}