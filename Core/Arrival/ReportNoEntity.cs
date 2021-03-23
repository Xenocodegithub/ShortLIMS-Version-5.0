using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Arrival
{
    public class ReportNoEntity
    {
        public long ReportNumberId { get; set; }
        public long ReportCount { get; set; }
        public string ULRNo { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        
    }
}
