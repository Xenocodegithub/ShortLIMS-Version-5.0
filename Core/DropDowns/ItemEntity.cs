using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.DropDowns
{
    public class ItemEntity
    {
        public int CatagoryMasterId { get; set; }
        public short InventoryTypeID { get; set; }
        public string CategoryName { get; set; }
        public string InventoryType { get; set; }
        public string Item { get; set; }
        public int ItemMasterId { get; set; }
        public int PurchaseSupplierID { get; set; }
        public string SupplierName { get; set; }
    }
}