using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class InventoryBreakDownModel
    {
        public long InventoryMaintAndCalibBreakDownId { get; set; }
        //public long NameOfInstrumentId { get; set; }
        public long ItemMasterId { get; set; }
        public long InventoryBasicDetailId { get; set; }
        public long InventoryBasicItemDetailsId { get; set; }

        [Required(ErrorMessage = "Please Enter Problem Description")]
        public string ProblemDescription { get; set; }
        [Required(ErrorMessage = "Please Enter Problem Reported By")]
        public string ProblemReportedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please Select Problem Reported Date ")]
        public Nullable<System.DateTime> ProblemReportedDate { get; set; }
        public string NameOfAgency { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateOfStartRepair { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateOfCompletionRepair { get; set; }
        public string MaintenanceInspectedBy { get; set; }
        public Nullable<long> Expenses { get; set; }
        public string Remark { get; set; }
        public bool isrepair { get; set; }
        [Required(ErrorMessage ="Please Select Name of Instruments")]
        public string NameOfInstrument { get; set; }
        public Nullable<bool> IsRepair { get; set; }
        [Required(ErrorMessage = "Please Select Serial Number")]
        public string IDNO { get; set; }
        public int ID { get; set; }
    }
}