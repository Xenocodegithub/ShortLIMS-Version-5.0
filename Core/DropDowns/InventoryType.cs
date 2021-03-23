using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.DropDowns
{
    public class InventoryType
    {
        public short InventoryTypeID { get; set; } // ID
        public string InventoryTypeName { get; set; }
        public short CatagoryMasterID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public int ID { get; set; }
        public string InventoryName { get; set; }
    }
}