using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Enquiry
{
    public class WorkOrderEntity
    {
        public string ContractAmendment { get; set; }
        public int LastDay { get; set; }
        public long MonthTotalDaysId { get; set; }
        public int SampleLocationId { get; set; }
        public string Location { get; set; }
        public long EnquirySampleId { get; set; } 
        public long LocationSampleCollectionID { get; set; }
        public string LocationSampleName { get; set; }
        public Nullable<bool> IsIGST { get; set; }
        public int WOMasterSampleCollectionDateId { get; set; }
        public int CustomerMasterId { get; set; }
        public Nullable<long> Duration { get; set; }
        public int WorkOrderSampleCollectionDateId { get; set; }
        public int? WorkOrderId { get; set; }
        public string Remarks { get; set; }
        public long QuotationLogId { get; set; }
        public long QuotationId { get; set; }
        public string RegistrationName { get; set; }
        public Nullable<System.DateTime> ExpectSampleCollDate { get; set; }
        public Nullable<System.DateTime> WORecieveDate { get; set; }
        public Nullable<System.DateTime> WOEndDate { get; set; }
        public string WOUpload { get; set; }
        public string Status { get; set; }
        public string WorkOrderNo { get; set; }
        public string GST { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public decimal? QuotedAmount { get; set; }
        public string FileName { get; set; }
    }

    public class WorkOrderHODEntity
    {
        public int EnteredBy { get; set; }
        public int WorkOrderSampleCollectionDateId { get; set; }
        public Nullable<int> LabMasterId { get; set; }
        public string FileName { get; set; }
        public string WOUpload { get; set; }
        public System.DateTime WORecieveDate { get; set; }
        public string RegistrationName { get; set; }
        public int? CompanyTypeId { get; set; }
        public string EnquiryNo { get; set; }
        public string WorkOrderNo { get; set; }
        public long EnquiryId { get; set; }
        public int WorkOrderId { get; set; }
        public string ApprovedAmount { get; set; }
        public string CurrentStatus { get; set; }
        public string Remarks { get; set; }
        public string Date { get; set; }
        public int? AssignToId { get; set; }
        public Nullable<bool> IsIGST { get; set; }
        public long SampleCollectionId { get; set; }
    }

    public class ParameterPCBEntity
    {
        public long EnquiryParameterDetailId { get; set; }
        public long EnquirySampleId { get; set; }
        public long ParameterMappingId { get; set; }
        public string PCBLimit { get; set; }
        public long ParameterMasterId { get; set; }
        public string ParameterName { get; set; }
    }

}
