using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Enquiry.Models
{
    public class QuotationLogModel
    {
        public Nullable<System.DateTime> MailedOn { get; set; }
        public string QuotationNo { get; set; }
        public string RevisedDate { get; set; }
        public long QuotationLogId { get; set; }
        public long EnquiryId { get; set; }
        public Nullable<decimal> QuotedAmount { get; set; }
        public Nullable<bool> IsRevised { get; set; }
        public Nullable<System.DateTime> RevisedOn { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Status Updated On")]
        public Nullable<System.DateTime> StatusUpdatedOn { get; set; }
        [Required(ErrorMessage = "Please select status")]
        public string Status { get; set; }

        //[Required]
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public string CustomerName { get; set; }
    }
}