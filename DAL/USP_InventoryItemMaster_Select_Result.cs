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
    
    public partial class USP_InventoryItemMaster_Select_Result
    {
        public int ID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<short> UnitID { get; set; }
        public Nullable<decimal> MinimumStock { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<short> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<short> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<short> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<short> InventoryTypeID { get; set; }
        public string AvailableStock { get; set; }
        public string CategoryName { get; set; }
        public string TypeName { get; set; }
        public string Capacity { get; set; }
    }
}
