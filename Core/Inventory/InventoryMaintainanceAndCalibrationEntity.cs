using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Inventory
{
    public class InventoryMaintainanceAndCalibrationEntity
    {
        //calibration
        public int CalibrationInventoryBasicItemDetailsID { get; set; }
        public string CalibrationAMCVendorName { get; set; }
        public string CalibrationAMCNumber { get; set; }
        public Nullable<System.DateTime> CalibrationStartDate { get; set; }
        public Nullable<System.DateTime> CalibrationEndDate { get; set; }
        public Nullable<System.DateTime> CalibrationAMCDate { get; set; }
        public string CalibrationAMCValue { get; set; }

        public string CalibrationAMCPeriod { get; set; }
        public string CalibrationFrequency { get; set; }
        public Nullable<System.DateTime> CalibrationAMCStartDate { get; set; }
        public Nullable<long> ScheduleID { get; set; }
        public Nullable<long> CompletionDateID { get; set; }
        public Nullable<DateTime> CompletionDate { get; set; }
        public Nullable<long> NextDateID { get; set; }
        public Nullable<DateTime> NextDate { get; set; }
        public Nullable<long> InventoryBasicItemDetailsID { get; set; }
        public string InventoryBasicItemDetailsNumber { get; set; }
        public string ItemName { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int ID { get; set; }
        public string Type { get; set; }
        public decimal AMCValue { get; set; }
        public Nullable<long> InventoryBasicDetailsID { get; set; }
        public string Auditor { get; set; }
        public Nullable<System.DateTime> AuditDate { get; set; }
        public string AuditObservations { get; set; }
        public string ActionTaken { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<int> InventoryMaintainanceAndCalibrationScheduleDatesID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string CalibratorName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> ScheduleDate { get; set; }
        public Nullable<System.DateTime> FrequencyDate { get; set; }
        public string CompletionStatus { get; set; }
        public string AMCVendorName { get; set; }
        public Nullable<System.DateTime> AMCDate { get; set; }
        public int DateID { get; set; }
        public int AMCPeriod { get; set; }
        public string AMCNumber { get; set; }
        public int Frequency { get; set; }
        public Nullable<System.DateTime> NextCalibrationDate { get; set; }
    }
}