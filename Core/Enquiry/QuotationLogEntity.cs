using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Enquiry
{
    public class QuotationLogEntity
    {
        public Nullable<System.DateTime> MailedOn { get; set; }
        public string QuotationNo { get; set; }
        public long QuotationLogId { get; set; }
        public long EnquiryId { get; set; }
        public Nullable<decimal> QuotedAmount { get; set; }
        public Nullable<bool> IsRevised { get; set; }
        public Nullable<System.DateTime> RevisedOn { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public string CustomerName { get; set; }
        public Nullable<System.DateTime> StatusUpdatedOn { get; set; }
        public string Status { get; set; }
    }
}
