using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Enquiry.Models
{
    public class EnquiryModel
    {
        public long EnquiryId { get; set; }//EnquiryMaster tbl

        [Display(Name = "Enquiry On")]
        [Required(ErrorMessage = "The Enquiry date field is required")]
        public Nullable<System.DateTime> EnquiryOn { get; set; }//EnquiryMaster tbl
        public string EnquiryDate { get; set; }//Doubt

        [Display(Name = "Remarks")]
        //[Required(ErrorMessage = "Remarks field is required")]
        public string Remarks { get; set; } //EnquiryMaster tbl

        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "The Customer name field is required")]
        public string CustomerName { get; set; }//CustomerTypeMaster & CustomerMaster tbls
        [Display(Name = "Status")]
        public string CurrentStatus { get; set; }//EnquiryStatus and StatusMaster tbls

        [Display(Name = "Mode Of Communication")]
        public string CommunicationMode { get; set; }//ModeOfCommunication tbl

        public byte ModeOfCommunicationId { get; set; }

        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "The Customer name field is required")]
        public int? CustomerMasterId { get; set; }
        public byte StatusId { get; set; }
        public string StatusCode { get; set; }
        public int LabMasterId { get; set; }
        public string ReferenceNumber { get; set; }
        public string LegalObligations { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public long EnquirySampleID { get; set; }
        public long EnquiryDetailId { get; set; }

    }
}