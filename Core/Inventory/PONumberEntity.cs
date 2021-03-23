using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Inventory
{
    public class PONumberEntity
    {

        public long PONumberId { get; set; }
        public long POCount { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
    }
}