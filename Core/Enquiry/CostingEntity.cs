using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Enquiry
{
    public class CostingEntity
    {
        public string SampleTypeProductCode { get; set; }
        public string SampleTypeProductName { get; set; }
        public long EnquiryMasterSampleTypeId { get; set; }
        public int  SampleTypeProductId {get; set;}
        public string DisplaySampleName { get; set; }
        public long? CostingId { get; set; }
        public long EnquirySampleID { get; set; }
        public long EnquiryDetailId { get; set; }
        public Nullable<decimal> TotalCharges { get; set; }
        public Nullable<decimal> CollectionCharges { get; set; }
        public Nullable<decimal> TransportCharges { get; set; }
        public Nullable<decimal> TestingCharges { get; set; }
        public Nullable<decimal> GST { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }

        public string SampleName { get; set; }
        public string ProductGroupName { get; set; }
        public string SubGroupName { get; set; }
        public string MatrixName { get; set; }
        public bool? IsSetPCBLimit { get; set; }
        public int? NoOfSample { get; set; }
        public string RegistrationName { get; set; }
        public string WorkOrderNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public bool? IsIGST { get; set; }
    }
}
