using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Collection
{
    public class QuantityPreservativeEntity
    {
        public int WorkOrderID { get; set; }//SampleCollection tbl
        public long EnquirySampleID { get; set; }//SampleCollection tbl
        public Nullable<long> No { get; set; }
        public string SampleTypeProductName { get; set; }
        public int SampleTypeProductId { get; set; }
        public int QtyPreservativeId { get; set; }
        public long SampleCollectionId { get; set; }
        public int SampleQtyId { get; set; }
        public bool ISGivenPreservative { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public int SerialNo { get; set; }
        public string SampleQty { get; set; }//SampleQtyMaster tbl
        public string Preservation { get; set; }//SampleQtyMaster tbl 
        public int ProductGroupId { get; set; }//EnquiryDetail tbl
    }
}
