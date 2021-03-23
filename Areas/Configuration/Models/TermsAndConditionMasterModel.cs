using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class TermsAndConditionMasterModel
    {
        [Key]
        public int TermAndCondtId { get; set; }
        public Nullable<int> ProductGroupId { get; set; }
        public Nullable<int> SubGroupId { get; set; }
        public Nullable<int> MatrixId { get; set; }
        public string TermAndCondt { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    }
}