using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Enquiry
{
    public class QuotationNumberEntity
    {
        public long QuotationNumberId { get; set; }
        public long QuotationCount { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
    }
}
