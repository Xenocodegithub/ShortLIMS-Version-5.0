using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class TestMethodMasterModel
    {
        public int TestMethodId { get; set; }
        public string TestMethod { get; set; }
        public string TestStd { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public int TestMthodId { get; set; }
        public string TestMethodName { get; set; }
    }
}