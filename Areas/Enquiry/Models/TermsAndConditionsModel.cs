using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Areas.Enquiry.Models
{
    public class TermsAndConditionsModel
    {
        public int TermAndCondtId { get; set; }
        public string TermAndCondt { get; set; }
        public bool IsSelected { get; set; }
       
    }
}