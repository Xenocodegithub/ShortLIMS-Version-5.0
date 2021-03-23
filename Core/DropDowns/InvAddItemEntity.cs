using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.DropDowns
{
    public class InvAddItemEntity
    {
        public int ItemMasterID { get; set; } // ItemMasterId =ID
        public Nullable<short> CatagoryMasterID { get; set; } // CatagoryMasterId
        public string CategoryName { get; set; }
        public Nullable<short> InventoryTypeID { get; set; } //InventoryTypeID
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
        public Nullable<decimal> AvailableStock { get; set; }
        public Nullable<long> InventoryCapacityMasterId { get; set; }
        public string Capacity { get; set; }
        public int ItemID { get; set; }


        // for stock log data
        public int IssueToID { get; set; }
        public int IssueToNameID { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public int IssueQty { get; set; }
        public string EmpName { get; set; }
    }
}