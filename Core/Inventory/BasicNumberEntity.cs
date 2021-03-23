using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Inventory
{
    public class BasicNumberEntity
    {
         public long SRNOID { get; set; }
            public long FinancialYearID { get; set; }
            public long FinancialMonthID { get; set; }
            public Nullable<long> NumberValue { get; set; }
            public bool IsActive { get; set; }
            public string NumberType { get; set; }
      
    }
}