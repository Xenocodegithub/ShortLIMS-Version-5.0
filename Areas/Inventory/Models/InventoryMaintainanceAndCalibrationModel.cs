using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class InventoryMaintainanceAndCalibrationModel
    {
        public int DateID { get; set; }
        public Nullable<long> ScheduleID { get; set; }
        public Nullable<long> CompletionDateID { get; set; }
        public Nullable<DateTime> CompletionDate { get; set; }
        public Nullable<long> NextDateID { get; set; }
        public Nullable<DateTime> NextDate { get; set; }
        public int ID { get; set; }
        public string Type { get; set; }
        public Nullable<long> InventoryBasicDetailsID { get; set; }
        [Required(ErrorMessage ="Please Enter Auditor Name")]
        public string Auditor { get; set; }
        [Required(ErrorMessage = "Please Select Audit Date")]
        public Nullable<System.DateTime> AuditDate { get; set; }
        [Required(ErrorMessage = "Please Select AMC Date")]
        public Nullable<System.DateTime> AMCDate { get; set; }
        
        public string AuditObservations { get; set; }
        public string ActionTaken { get; set; }
        [Required(ErrorMessage = "Please Select Item Name")]
        public Nullable<int> ItemID { get; set; }
        [Required(ErrorMessage = "Please Select AMCStartDate")]
        public Nullable<System.DateTime> AMCStartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> ScheduleDate { get; set; }
        public Nullable<int> InventoryMaintainanceAndCalibrationScheduleDatesID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string CalibratorName { get; set; }
        [Required(ErrorMessage ="Please Selec Frequency")]
        public int Frequency { get; set; }
        public Nullable<System.DateTime> CalibrationStartDate { get; set; }
        public Nullable<System.DateTime> CalibrationEndDate { get; set; }
        [Required(ErrorMessage = "Please Select Completion Status")]
        public string CompletionStatus { get; set; }

        public decimal AMCValue { get; set; }
        [Required(ErrorMessage = "Please Select Item Name")]
        public string ItemName { get; set; }
        [Required(ErrorMessage = "Please Select Category Name")]
        public Nullable<long> CategoryID { get; set; }
        public string CategoryName { get; set; }
        public Nullable<long> InventoryBasicItemDetailsID { get; set; }
        [Required(ErrorMessage = "Please Enter AMC Period")]
        public int AMCPeriod { get; set; }
        [Required(ErrorMessage = "Please Enter AMC Number")]
        public string AMCNumber { get; set; }
        [Required(ErrorMessage = "Please Enter AMC Vendor Name")]
        public string AMCVendorName { get; set; }
        public int CalibrationInventoryBasicItemDetailsID { get; set; }

        public string CalibrationAMCVendorName { get; set; }
        public string CalibrationAMCNumber { get; set; }

        public Nullable<System.DateTime> CalibrationAMCDate { get; set; }

        public int CalibrationAMCPeriod { get; set; }
        public int CalibrationFrequency { get; set; }
        public decimal CalibrationAMCValue { get; set; }
        
        public Nullable<System.DateTime> CalibrationAMCStartDate { get; set; }
        public Nullable<System.DateTime> NextCalibrationDate { get; set; }
        public string  ndate { get; set; }
        [Required(ErrorMessage = "Please Enter InventoryBasicItemDetailsNumber")]
        public string InventoryBasicItemDetailsNumber { get; set; }
       
    }
}