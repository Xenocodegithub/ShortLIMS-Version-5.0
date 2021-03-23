using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Inventory
{
    public class InventoryBasicItemDetailEntity
    {
        public int ID { get; set; }
        public string InventoryBasicItemDetailsNumber { get; set; }
        public Nullable<int> InventoryBasicDetailsID { get; set; }
        public string BatchNumber { get; set; }
        public string ModelNumber { get; set; }
        public string BarcodeNumber { get; set; }
        public string LOTNo { get; set; }
        public string SRNO { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> RowDeletedCode { get; set; }
        public string Manufacturer { get; set; }
        public Nullable<System.DateTime> DOM { get; set; }
        public Nullable<System.DateTime> DOE { get; set; }
    }
}