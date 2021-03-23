using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class InventoryMaintainanceAndCalibrationScheduleModel
    {
        [Key]
        public int ID { get; set; }
        public Nullable<int> InventoryBasicItemDetailsID { get; set; }
        public string AMCVendorName { get; set; }
        public string AMCNumber { get; set; }
        public Nullable<System.DateTime> AMCDate { get; set; }
        public Nullable<decimal> AMCValue { get; set; }
        public Nullable<short> AMCPeriod { get; set; }
        public Nullable<short> Frequency { get; set; }
        public string Type { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<System.DateTime> AMCStartDate { get; set; }
    }
}