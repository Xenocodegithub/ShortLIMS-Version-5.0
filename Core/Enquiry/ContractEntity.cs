using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Enquiry
{
    public class ContractEntity
    {
        public bool IsActive { get; set; }
        public long EnquiryMasterSampleTypeId { get; set; }
        public long EnquiryId { get; set; }
        public long EnquiryDetailId { get; set; }
        public long EnquirySampleID { get; set; }
        public System.DateTime EnquiryOn { get; set; }
        public int CustomerMasterId { get; set; }
        public string CustomerName { get; set; }
        public int ProductGroupId { get; set; }
        public string ProductGroupName { get; set; }
        public int SubGroupId { get; set; }
        public string SubGroupName { get; set; }

        public string SubGroupCode { get; set; }
        //public int MatrixId { get; set; }
        public Nullable<int> MatrixId { get; set; }
        public Nullable<int> SampleTypeProductId { get; set; }
        public string SampleTypeProductName { get; set; }
        public string SampleTypeProductCode { get; set; }
        public string MatrixName { get; set; }
        public int NumberofSamples { get; set; }
        public byte FrequencyMasterId { get; set; }
        public string Frequency { get; set; }
        public int SampleCollectedBy { get; set; }
        public bool IsWitness { get; set; }
        public bool IsReturn { get; set; }
        public bool IsUrgent { get; set; }
        public string Remark { get; set; }
        public int EnteredBy { get; set; }
    }
}
