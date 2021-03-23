using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Inventory
{
    public class InventoryMaintainanceAndCalibrationScheduleEntity
    {
        public int ID { get; set; }

        public Nullable<int> InventoryCommercialDetailsID { get; set; }

        public Nullable<int> InventoryBasicItemDetailsID { get; set; }

        public string AMCVendorName { get; set; }

        public string AMCNumber { get; set; }

        public Nullable<DateTime> AMCDate { get; set; }

        public Nullable<decimal> AMCValue { get; set; }

        public Nullable<short> AMCPeriod { get; set; }

        public Nullable<DateTime> AMCStartDate { get; set; }

        public Nullable<short> Frequency { get; set; }

        public string Type { get; set; }

        public Nullable<bool> IsActive { get; set; }

        public Nullable<int> InsertedBy { get; set; }

        public Nullable<System.DateTime> InsertedDate { get; set; }

        public Nullable<int> UpdatedBy { get; set; }

        public Nullable<System.DateTime> UpdatedDate { get; set; }

        //public List<InventoryMaintainanceAndCalibrationScheduleDates> ListScheduledDate { get; set; }

        //public List<InventoryMaintainanceAndCalibrationScheduleFiles> ListFiles { get; set; }

        public string UploadedFileNames { get; set; }
        public Nullable<DateTime> CalibrationAMCStartDate { get; set; }
        public Nullable<DateTime> CalibrationAMCDate { get; set; }
    }
}