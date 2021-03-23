using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Inventory
{
    public class InventoryCommercialDetailEntity
    {
        public int ID { get; set; }
        public string VendorName { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public Nullable<decimal> PurchaseOrderValue { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<System.DateTime> PurchaseOrderDate { get; set; }
        public Nullable<int> PurchaseRequestID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> InventoryBasicDetailsID { get; set; }
        public string DeliveryChallanNo { get; set; }
        public Nullable<System.DateTime> DeliveryChallanDate { get; set; }
        public Nullable<System.DateTime> BillDate { get; set; }
        public string DeliveryTime { get; set; }
    }
}