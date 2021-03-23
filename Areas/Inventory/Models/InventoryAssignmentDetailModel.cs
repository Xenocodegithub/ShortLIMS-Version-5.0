using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class InventoryAssignmentDetailModel
    {
        public int ID { get; set; }
        public Nullable<int> InventoryAssignmentMasterID { get; set; }
        public Nullable<int> InventoryTypeID { get; set; }
        public Nullable<int> InventoryItemID { get; set; }
        public Nullable<int> InventorySerialNumber { get; set; }
        public Nullable<decimal> RequestQuantity { get; set; }
        public Nullable<decimal> AssignedQuantity { get; set; }
        public Nullable<decimal> ReturnQuantity { get; set; }
        public Nullable<System.DateTime> AssignedDate { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
        public Nullable<bool> IsReturned { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> AssignorID { get; set; }
        public Nullable<bool> IsAssigned { get; set; }
    }
}