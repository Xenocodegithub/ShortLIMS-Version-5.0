using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.WorkOrderCustomer
{
    public class WorkOrderCustomerEntity
    {
        public Nullable<bool> OVC { get; set; }
        public Nullable<int> QuotationId { get; set; }
        public Nullable<int> DeliverId { get; set; }
        public string ContractAmendment { get; set; }
        public Nullable<bool> IsIGST { get; set; }
        public Nullable<long> Duration { get; set; }
        public Nullable<System.DateTime> ExpectSampleCollDate { get; set; }
        public int? AssignToId { get; set; }
        public string CurrentStatus { get; set; }
        public string CustomerName { get; set; }
        public string CommunicationMode { get; set; }
        public int WorkOrderId { get; set; }
        public Nullable<System.DateTime> WORecieveDate { get; set; }
        public Nullable<System.DateTime> WOEndDate { get; set; }
        public string WOUpload { get; set; }
        public string FileName { get; set; }
        public string WorkOrderNo { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<byte> ModeOfCommunicationId { get; set; }
        public Nullable<int> CustomerMasterId { get; set; }
        public Nullable<byte> StatusId { get; set; }
        public Nullable<int> LabMasterId { get; set; }
    }
}
