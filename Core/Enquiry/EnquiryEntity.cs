using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Enquiry
{
    public class EnquiryEntity
    {
        public long EnquiryId { get; set; }
        public int SampleTypeProductId { get; set; }
        public long EnquiryMasterSampleTypeId { get; set; }
        public byte ModeOfCommunicationId { get; set; }
        public int CustomerMasterId { get; set; }
        public byte StatusId { get; set; }
        public string StatusCode { get; set; }
        public int LabMasterId { get; set; }
        public string ReferenceNumber { get; set; }
        public System.DateTime EnquiryOn { get; set; }
        public string Remarks { get; set; }
        public string LegalObligations { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public string CustomerName { get; set; }
        public string CommunicationMode { get; set; }
        public string CurrentStatus { get; set; }
        public long? EnquiryParameterDetailID { get; set; }
        public long EnquirySampleID { get; set; }
        public long EnquiryDetailId { get; set; }
    }
}