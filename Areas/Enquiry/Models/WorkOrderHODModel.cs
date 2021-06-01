using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace LIMS_DEMO.Areas.Enquiry.Models
{
    public class WorkOrderHODModel
    {
        public int EnteredBy { get; set; }
        public int WorkOrderSampleCollectionDateId { get; set; }
        [Display(Name = "Customer Name")]
        public string RegistrationName { get; set; }
        public int EnquirySampleID { get; set; }

        public int QuotationId { get; set; }
        [Display(Name = "Enquiry No")]
        public int? CompanyTypeId { get; set; }

        [Display(Name = "Enquiry No")]
        public string EnquiryNo { get; set; }

        [Display(Name = "WorkOrder No")]
        public string WorkOrderNo { get; set; }

        public long EnquiryId { get; set; }
        public int WorkOrderId { get; set; }

        [Display(Name = "Approved Amount")]
        public string ApprovedAmount { get; set; }

        [Display(Name = "Status")]
        public string CurrentStatus { get; set; }

        public string Remarks { get; set; }

        public string Date { get; set; }

        public int? AssignToId { get; set; }

        public string UserName { get; set; }
        public string WOUpload { get; set; }
        public string FileName { get; set; }
        public string FilePath{ get; set; }

        [Display(Name = "WorkOrder Recieved Date")]
        public System.DateTime WORecieveDate { get; set; }
        public string WORecvDate { get; set; }
        public bool IsIGST { get; set; }
    }
 
}
