using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Inventory
{
    public class InventoryMaintenanceEntity
    {
        public Nullable<long> ScheduleID { get; set; }
        public Nullable<long> CompletionDateID { get; set; }
        public Nullable<DateTime> CompletionDate { get; set; }
        public Nullable<long> NextDateID { get; set; }
        public Nullable<DateTime> NextDate { get; set; }
        public Nullable<long> InventoryBasicItemDetailsID { get; set; }
        public string InventoryBasicItemDetailsNumber { get; set; }
        public Nullable<long> InventoryBasicDetailsID { get; set; }
        public Nullable<long> ItemID { get; set; }
        public string ItemName { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Frequency { get; set; }
    }
}