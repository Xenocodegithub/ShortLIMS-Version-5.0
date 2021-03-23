using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Dashboard
{
    public class DashboardEntity
    {
        public string SampleTypeProductName { get; set; }
        public string ItemName { get; set; }
        public long enquiry { get; set; }
        public Nullable<decimal> TotalRevenue { get; set; }
        public Nullable<int> count { get; set; }
        public long countsamplecollected { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Mode{get;set; }
        public string Month { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> MonthCount { get; set; }
    }
}