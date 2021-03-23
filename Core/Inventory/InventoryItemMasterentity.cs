using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Inventory
{
    public class InventoryItemMasterentity
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public Nullable<short> InventoryTypeID { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string CategoryName { get; set; }
        public string Code { get; set; }
        public Nullable<short> UnitID { get; set; }
        public Nullable<decimal> MinimumStock { get; set; }
        public string AvailableStock { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<short> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<short> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<short> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }

        // public InventoryCategoryMaster InventoryCategoryMasterDTO { get; set; }
        //public InventoryTypeMaster InventoryTypeMasterDTO { get; set; }
        //public UnitMaster UnitMasterDTO { get; set; }

        public string ReturnUrl { get; set; }

        //StockLogData 
        public int IssueToID { get; set; }
        public int IssueToNameID { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public int IssueQty { get; set; }
        public int ItemID { get; set; }
        public string EmpName { get; set; }
        public string Unit { get; set; }
        public string Capacity { get; set; }
        public int EmpId { get; set; }
        public Nullable<long> InventoryCapacityMasterId { get; set; }
    }
}