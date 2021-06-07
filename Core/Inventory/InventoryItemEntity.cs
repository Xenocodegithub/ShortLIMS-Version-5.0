using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Inventory
{
    public class InventoryItemEntity
    {
        public int ItemMasterID { get; set; } // ItemMasterId =ID

        public Nullable<int> CatagoryMasterID { get; set; } // CatagoryMasterId
        public Nullable<int> CatagoryID { get; set; } // CatagoryMasterId
        public string CategoryName { get; set; }
        public Nullable<short> InventoryTypeID { get; set; } //InventoryTypeID
        public Nullable<short> TypeID { get; set; } //InventoryTypeID
        public string InventoryTypeName { get; set; }
        public string NameOfStock { get; set; }
        public string Code { get; set; }
        public Nullable<short> UnitID { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> MinimumStock { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<short> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<short> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<short> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string AvailableStock { get; set; }
        public Nullable<long> InventoryCapacityMasterId { get; set; }
        public string Capacity { get; set; }
        public int ItemID { get; set; }


        // for stock log data
        public int IssueToID { get; set; }
        public int IssueToNameID { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public int IssueQty { get; set; }
        public string EmpName { get; set; }
        public int EmpId { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
    }
}