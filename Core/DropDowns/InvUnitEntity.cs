using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.DropDowns
{
    public class InvUnitEntity
    {
        public short UnitId { get; set; }
        public string Unit { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedTime { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedTime { get; set; }
    }
}