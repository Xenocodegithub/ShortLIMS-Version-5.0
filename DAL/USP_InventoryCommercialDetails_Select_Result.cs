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
    
    public partial class USP_InventoryCommercialDetails_Select_Result
    {
        public int ID { get; set; }
        public Nullable<int> InventoryBasicDetailsID { get; set; }
        public string VendorName { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public Nullable<decimal> PurchaseOrderValue { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string DeliveryChallanNo { get; set; }
        public Nullable<System.DateTime> DeliveryChallanDate { get; set; }
        public Nullable<System.DateTime> BillDate { get; set; }
        public string DeliveryTime { get; set; }
    }
}