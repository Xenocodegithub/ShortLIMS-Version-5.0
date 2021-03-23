using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Inventory
{
    public class InventoryMaintainanceAndCalibrationScheduleDatesEntity
    {
        public int ID { get; set; }

        public Nullable<int> InventoryMaintainanceAndCalibrationScheduleID { get; set; }

        public Nullable<DateTime> ScheduleDate { get; set; }

        public string CompletionStatus { get; set; }

        public Nullable<bool> IsActive { get; set; }

        public Nullable<int> InsertedBy { get; set; }

        public Nullable<System.DateTime> InsertedDate { get; set; }

        public Nullable<int> UpdatedBy { get; set; }

        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public string Name { get; set; }
        public string InventoryBasicItemDetailsNumber { get; set; }
        public Nullable<int> AMCPeriods { get; set; }

        public Nullable<int> OccuranceFrequency { get; set; }
        public Nullable<int> Frequency { get; set; }
        public int InventoryBasicDetailsID { get; set; }
        public int InventoryBasicItemDetailsID { get; set; }

    }

    public class InventoryMaintainanceAndCalibrationScheduleFiles
    {
    public int ID { get; set; }
    public Nullable<int> InventoryMaintainanceAndCalibrationScheduleID { get; set; }
    public string Type { get; set; }
    public string FileName { get; set; }
    public Nullable<bool> IsActive { get; set; }
    }
}