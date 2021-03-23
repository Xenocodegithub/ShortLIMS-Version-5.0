using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.DropDowns
{
    public class SPSMEntity
    {
        public long SampleCollectionId { get; set; }
        public Nullable<long> EnquirySampleID { get; set; }
        public string SampleName { get; set; }
        public int SampleTypeProductId { get; set; }
        public string SampleTypeProductName { get; set; }
        public string SampleTypeProductCode { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public int SubGroupId { get; set; }
        public string SubGroupName { get; set; }
        public int ProductGroupId { get; set; }
        public string ProductGroupName { get; set; }
        public int MatrixId { get; set; }
        public string MatrixName { get; set; }
    }
}