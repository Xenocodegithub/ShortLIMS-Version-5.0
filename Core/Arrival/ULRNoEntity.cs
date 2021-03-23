using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Arrival
{
    public class ULRNoEntity
    {
        public long ULRNumberId { get; set; }
        public long ULRCount { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
      
    }
}
