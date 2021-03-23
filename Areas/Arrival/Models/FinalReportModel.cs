using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Arrival.Models
{
    public class FinalReportModel
    {
        public int WOMasterSampleCollectionDateId { get; set; }
        [Display(Name = "Location:")]
        public string SampleLocation { get; set; }//SampleCollection tbl
        public int LocationSampleCollectionID { get; set; }
        public string SampleTypeProductName { get; set; }
        public int SampleTypeProductId { get; set; }
        public int SerialNo { get; set; }
        public long SampleCollectionId { get; set; }
        public int WorkOrderID { get; set; }
        public Nullable<long> EnquirySampleID { get; set; }
        public long EnquiryDetailId { get; set; }//EnquirySampleDetail tbl
        public long EnquiryId { get; set; }
        public Nullable<byte> StatusId { get; set; }
        public string CurrentStatus { get; set; }// EnquiryStatus and StatusMaster tbls
        public string StatusCode { get; set; }
        public string SampleNo { get; set; }
        public string SampleName { get; set; }
        public long QuotationId { get; set; }
        public string WorkOrderNo { get; set; }// WorkOrder tbl
        public int CustomerMasterId { get; set; }//EnquirySampleDetail tbl
        public string CustomerName { get; set; }
        public string CityName { get; set; }
        public string ContactNO { get; set; }
        public string Url { get; set; }//Variable
        public string MatrixName { get; set; }//EnquiryDetail tbl

    }
}