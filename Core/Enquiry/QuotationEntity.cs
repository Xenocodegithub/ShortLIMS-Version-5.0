using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Enquiry
{
    public class QuotationEntity
    {
        public long QuotationId { get; set; }
        public string QuotationNo { get; set; }
        public long EnquiryId { get; set; }
        public string EnquiryNo { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public Nullable<decimal> QuotedAmount { get; set; }
        public Nullable<bool> IsRevised { get; set; }
        public Nullable<System.DateTime> RevisedOn { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
    }

    public class QuotationTNCEntity
    {
        public int QuotationTNCId { get; set; }
        public long QuotationId { get; set; }
        public int TermAndCondtId { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public Nullable<int> WorkOrderID { get; set; }

    }

    public class QuotationPreviewEntity
    {
        public long EnquiryId { get; set; }
        public string LabName { get; set; }
        public string RefName { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Designation { get; set; }
        public string ClientMobileNo { get; set; }
        public string ContactEmail { get; set; }
        public string PAN { get; set; }
        public string GSTNO { get; set; }
        public string SACNO { get; set; }
        public string BANKERS { get; set; }
        public string PFNO { get; set; }
        public string ESICNO { get; set; }
        public string PTNO { get; set; }
        public string TDSNO { get; set; }
        public string MSMEDA { get; set; }
        public string IECNO { get; set; }
        public string LABAddress { get; set; }
        public string LABPhone { get; set; }
        public string LABMobileFax { get; set; }
        public string LABEmail { get; set; }
        public string RevisedDates { get; set; }
        public string ContactMobile { get; set; }
        public string ClientFax { get; set; }
        public string LABCity { get; set; }
    }

}
