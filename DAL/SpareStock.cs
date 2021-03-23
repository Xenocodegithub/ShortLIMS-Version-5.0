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
    
    public partial class SpareStock
    {
        public long SpareStockId { get; set; }
        public string Description { get; set; }
        public Nullable<long> InventoryItemMasterId { get; set; }
        public string MaterialCode { get; set; }
        public Nullable<long> UnitId { get; set; }
        public string InventoryName { get; set; }
        public Nullable<decimal> OpeningStock { get; set; }
        public Nullable<decimal> Receiving { get; set; }
        public Nullable<decimal> Consumption { get; set; }
        public Nullable<decimal> ClosginStock { get; set; }
        public Nullable<int> StockMonth { get; set; }
        public Nullable<int> StockYear { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedTime { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedTime { get; set; }
    }
}
