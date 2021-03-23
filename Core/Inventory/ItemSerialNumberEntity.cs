using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Inventory
{
    public class ItemSerialNumberEntity
    {
        public int ID { get; set; }
        public Nullable<int> InventoryBasicDetailsID { get; set; }
        public string InventoryBasicItemDetailsNumber { get; set; }
    }
}