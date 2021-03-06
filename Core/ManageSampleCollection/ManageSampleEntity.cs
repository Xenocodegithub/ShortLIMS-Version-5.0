using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.ManageSampleCollection
{
    public class ManageSampleEntity
    {
        public int WorkOrderSampleCollectionDateId { get; set; }
        public Nullable<int> WorkOrderId { get; set; }
        public Nullable<System.DateTime> ExpectSampleCollDate { get; set; }
        public Nullable<System.DateTime> ActualSampleCollDate { get; set; }
        public Nullable<int> Status { get; set; }
        public bool IsActive { get; set; }
        public int Sr { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public string CustomerName { get; set; }// CustomerMaster tbl
        public string DisplaySampleName { get; set; }
        public long SampleCollectionId { get; set; }
    }
}
