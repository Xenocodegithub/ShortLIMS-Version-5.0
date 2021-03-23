using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Enquiry.Models
{
    public class EnquiryLogModel
    {
        public int SerialNo { get; set; }
        public long EnquiryLogId { get; set; }
        public long EnquiryId { get; set; }
        [Display(Name = "Mode Of Communication")]
        public string CommunicationMode { get; set; }//ModeOfCommunication tbl
        public byte ModeOfCommunicationId { get; set; }
        public string Remarks { get; set; }

        [Display(Name = "Enquiry On")]
        [Required(ErrorMessage = "Enquiry date is required")]
        public Nullable<System.DateTime> CommunicationDate { get; set; }
        public string Date { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }//CustomerTypeMaster & CustomerMaster tbls
        public int? CustomerMasterId { get; set; }
    }
}