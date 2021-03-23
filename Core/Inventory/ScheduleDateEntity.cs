using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Inventory
{
    public class ScheduleDateEntity
    {
        public string scdate { get; set; }
        public Nullable<int> InventoryMaintainanceAndCalibrationScheduleDatesID { get; set; }
        public Nullable<long> DateID { get; set; }
        public Nullable<System.DateTime> ScheduleDate { get; set; }
    }
}