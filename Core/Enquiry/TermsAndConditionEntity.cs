using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Enquiry
{
    public class TermsAndConditionEntity
    {
        public int TermAndCondtId { get; set; }
        public string TermAndCondt { get; set; }
        public bool IsSelected { get; set; }
        public Nullable<int> WorkOrderID { get; set; }

        public string Remark { get; set; }
    }
}
