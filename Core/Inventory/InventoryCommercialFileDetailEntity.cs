using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Inventory
{
    public class InventoryCommercialFileDetailEntity
    {
        public int ID { get; set; }
        public Nullable<int> InventoryBasicDetailID { get; set; }
        public string FileName { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}