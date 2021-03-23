using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class InventoryMaintAndCalibBreakDownModel
    {
        [Key]
        public long InventoryMaintAndCalibBreakDownId { get; set; }
        public long NameOfInstrumentId { get; set; }
        public long InventoryBasicItemDetailsId { get; set; }
        public long InventoryBasicDetailId { get; set; }
        public string ProblemDescription { get; set; }
        public string ProblemReportedBy { get; set; }
        public Nullable<System.DateTime> ProblemReportedDate { get; set; }
        public string NameOfAgency { get; set; }
        public Nullable<System.DateTime> DateOfStartRepair { get; set; }
        public Nullable<System.DateTime> DateOfCompletionRepair { get; set; }
        public string MaintenanceInspectedBy { get; set; }
        public Nullable<long> Expenses { get; set; }
        public string Remark { get; set; }
    }
}