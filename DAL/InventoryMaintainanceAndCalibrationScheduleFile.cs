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
    
    public partial class InventoryMaintainanceAndCalibrationScheduleFile
    {
        public int ID { get; set; }
        public Nullable<int> InventoryMaintainanceAndCalibrationScheduleID { get; set; }
        public string Type { get; set; }
        public string FileName { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}