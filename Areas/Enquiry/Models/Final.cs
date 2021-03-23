using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace LIMS_DEMO.Areas.Enquiry.Models
{
    public class Final
    {
        [Display(Name = "Test Method")]
        public string TestMethod { get; set; }

        [Display(Name = "Discipline")]
        public string Discipline { get; set; }

        [Display(Name = "Unit")]
        public int Unit { get; set; }

        [Display(Name = "Lower Limit")]
        public int LowerLimit { get; set; }

        [Display(Name = "Upper Limit")]
        public int UpperLimit { get; set; }

        [Display(Name = "Field Date")]
        public DateTime FieldDate { get; set; }

        [Display(Name = "Charges")]
        public int Charges { get; set; }

        [Display(Name = "Parameter Name")]
        public string ParameterName { get; set; }
    }
}