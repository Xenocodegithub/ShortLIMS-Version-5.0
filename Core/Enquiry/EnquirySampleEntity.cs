using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Enquiry
{
    public class EnquirySampleEntity
    {
        public int WorkOrderId { get; set; }

        public int SampleTypeProductId { get; set; }
        public long EnquiryMasterSampleTypeId { get; set; }
        public string Remarks { get; set; }
        public string CurrentStatus { get; set; }
        public long EnquirySampleID { get; set; }
        public long EnquiryDetailId { get; set; }
        public Nullable<int> SampleDescriptionId { get; set; }
        public Nullable<int> SampleTypeId { get; set; }
        public Nullable<int> SampleDeviceId { get; set; }
        public Nullable<int> SampleQtyId { get; set; }
        public Nullable<int> ProcedureId { get; set; }
        public Nullable<byte> FrequencyMasterId { get; set; }
        public Nullable<int> EnvCondtId { get; set; }
        public string DisplaySampleName { get; set; }
        public string SampleName { get; set; }
        public Nullable<int> SampleCollectedBy { get; set; }
        public string EmployeeId { get; set; }
        public Nullable<bool> IsWitness { get; set; }
        public Nullable<bool> IsReturn { get; set; }
        public Nullable<bool> IsUrgent { get; set; }
        public Nullable<System.DateTime> CollectionDate { get; set; }
        public Nullable<System.TimeSpan> SampleCollectionTime { get; set; }
        public string SampleLocation { get; set; }
        public Nullable<decimal> TotalCharges { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public string ProductGroupName { get; set; }
        public string SubGroupName { get; set; }
        public string MatrixName { get; set; }
        public string SampleTypeProductName { get; set; }
        public string Parameters { get; set; }
        public decimal Cost { get; set; }
        public int? NoOfSample { get; set; }
    }
}
