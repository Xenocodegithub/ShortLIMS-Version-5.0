using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class InventoryMaintainanceAndCalibrationScheduleFileModel
    {
        [Key]
        public int ID { get; set; }
        public Nullable<int> InventoryMaintainanceAndCalibrationScheduleID { get; set; }
        public Nullable<System.DateTime> ScheduleDate { get; set; }
        public string CompletionStatus { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}