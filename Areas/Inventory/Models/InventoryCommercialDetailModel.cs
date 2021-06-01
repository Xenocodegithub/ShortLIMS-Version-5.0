using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class InventoryCommercialDetailModel
    {
        
        public int ID { get; set; }
        //[Required(ErrorMessage ="Please Enter Supplier Name")]
        public string VendorName { get; set; }
        //[Required(ErrorMessage = "Please Enter Purchase Order Number")]
        public string PurchaseOrderNumber { get; set; }
        //[Required(ErrorMessage = "Please Enter Purchase Order Value ")]
        public Nullable<decimal> PurchaseOrderValue { get; set; }
        //[Required(ErrorMessage = "Please Select Purchase Date")]
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
        //[Required(ErrorMessage = "Please Enter Delivery Challan No ")]
        public string DeliveryChallanNo { get; set; }
        public Nullable<System.DateTime> DeliveryChallanDate { get; set; }
        public Nullable<System.DateTime> BillDate { get; set; }
        public string DeliveryTime { get; set; }
    }
}