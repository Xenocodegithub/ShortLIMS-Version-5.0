//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LIMS_DEMO.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class InventoryMaintainanceAndCalibrationSchedule
    {
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