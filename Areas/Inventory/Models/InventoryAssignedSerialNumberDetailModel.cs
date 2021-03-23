using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class InventoryAssignedSerialNumberDetailModel
    {
        public int ID { get; set; }
        public Nullable<int> InventoryAssignmentDetailsID { get; set; }
        public Nullable<int> InventorySerialNumber { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}