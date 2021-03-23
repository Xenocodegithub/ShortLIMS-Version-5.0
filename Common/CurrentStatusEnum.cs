using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Common
{
    public enum CurrentStatusEnum
    {
        [Display(Name = "Under Testing")]
        Under_Testing = 1,
        [Display(Name = "Under Reviewer")]
        Under_Reviewer = 2,
        [Display(Name = "Under Approver")]
        Under_Approver = 3,
        [Display(Name = "Re-Test")]
        Re_Test = 4,
        [Display(Name = "Re-Plan")]
        Re_Plan = 5,
        [Display(Name = "Approved")]
        Approved = 6,
        Reject = 7,
        submit = 8,//for purchase by khushboo
        notsubmit = 9//for purchase by khushboo
    }
}