using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.WorkOrderCustomer
{
    public class WorkOrderSampleEntity
    {
        public long EnquiryDetailId { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public Nullable<int> SampleTypeProductId { get; set; }
        public int ProductGroupId { get; set; }
        public int SubGroupId { get; set; }
        public Nullable<int> MatrixId { get; set; }
        public string SampleTypeProductName { get; set; }
        public string SampleTypeProductCode { get; set; }
        public string ProductGroupName { get; set; }
        public string SubGroupName { get; set; }
        public string SubGroupCode { get; set; }
        public string MatrixName { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
    }
}
