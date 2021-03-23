using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Collection
{
    public class SampleCollectionEntity
    {
        public string mode { get; set; } 
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public long FDId { get; set; }
        public Nullable<int> WorkOrderSampleCollectionDateId { get; set; }
        public int SampleLocationId { get; set; }
        public string SampleTypeProductName { get; set; }
        public int SampleTypeProductId { get; set; }
        public int LocationSampleCollectionID { get; set; }
        public string DisplaySampleName { get; set; }
        public string Duration { get; set; }
        public string SubGroupName { get; set; }
        public int SubGroupId { get; set; }
        public string ContactMobileNo { get; set; }
        public string Address1 { get; set; }
        public string CollectedOn { get; set; }
        public string StatusCodeLab { get; set; }
        public string StatusCodeField { get; set; }
        public long ARCId { get; set; }
        public int UserRoleId { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public long SampleCollectionId { get; set; }//SampleCollection tbl
        public int WorkOrderID { get; set; }//SampleCollection tbl
        public string WorkOrderNo { get; set; }// WorkOrder tbl
        public System.DateTime ExpectSampleCollDate { get; set; }// WorkOrder tbl
        public long EnquirySampleID { get; set; }//SampleCollection tbl
        public byte StatusId { get; set; }//SampleCollection tbl
        public string CurrentStatus { get; set; }// StatusMaster tbls
        public Nullable<int> FieldDeterminationId { get; set; }//SampleCollection tbl
        public Nullable<int> Iteration { get; set; }//SampleCollection tbl
        public Nullable<bool> IsWitness { get; set; }//SampleCollection tbl
        public string WitnessName { get; set; }//SampleCollection tbl
        public string SampleNo { get; set; }//SampleCollection tbl
        public bool IsSampleIntact { get; set; }//SampleCollection tbl
        public string IndustryType { get; set; }//SampleCollection tbl
        public string Source { get; set; }//SampleCollection tbl
        public string SampleLocation { get; set; }//SAmpleColletion tbl
        public string RequestNo { get; set; }//SampleCollection tbl
        public string ULRNo { get; set; }//SampleCollection tbl
      
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        public long EnquiryDetailId { get; set; }//EnquirySampleDetail tbl
        public long EnquiryId { get; set; }//EnquiryMaster 
        public int CustomerMasterId { get; set; }//EnquiryMaster & CustomerMaster tbl
        public string CustomerName { get; set; }// CustomerMaster tbl
        public string Address { get; set; }// CustomerMaster tbl
        public string ContactNO { get; set; }//CustomerMaster tbl
        public string SampleName { get; set; }//EnquirySampleDetail tbl
        public Nullable<int> SampleCollectedBy { get; set; }//EnquirySampleDetail tbl
        public string EmployeeId { get; set; }//EnquirySampleDetail tbl
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        public Nullable<System.DateTime> CollectionDate { get; set; }//EnquirySampleDetail tbl
        public Nullable<System.TimeSpan> SampleCollectionTime { get; set; }//EnquirySampleDetail tbl
      
        public string Remark { get; set; }//EnquirySampleDetail tbl
        public string Location { get; set; }//SampleCollection tbl

        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public Nullable<int> SampleDescriptionId { get; set; }//SampleDescriptionMaster tbl same as <sample source>
        public string SampleDescription { get; set; }//SampleDescriptionMaster tbl same as <sample source>
        public Nullable<int> SampleDeviceId { get; set; }//SampleDeviceMaster tbl
        public string SampleDevice { get; set; }//SampleDeviceMaster tbl
        public Nullable<int> SampleTypeId { get; set; }//SampleType same as <subgroup> SampleTypeMaster tbl
        public string SampleType { get; set; }//SampleType same as <subgroup> SampleTypeMaster tbl
        public Nullable<int> SampleQtyId { get; set; }
        //public int SampleQtyId { get; set; }//SampleQtyMaster tbl & QuantityPreservative tbl
        public string SampleQty { get; set; }//SampleQtyMaster tbl
        public string Preservation { get; set; }//SampleQtyMaster tbl 
        public int QtyPreservativeId { get; set; }//QuantityPreservative tbl
        public bool ISGivenPreservative { get; set; }//QuantityPreservative tbl
        public Nullable<int> EnvCondtId { get; set; }//EnvironmentalCondition tbl
        public string EnvCondts { get; set; }//EnvironmentalCondition tbl
        public Nullable<int> ProcedureId { get; set; }//ProcedureMaster tbl
        public string ProcedureName { get; set; }//ProcedureMaster tbl
        public int ProductGroupId { get; set; }//EnquiryDetail tbl
        public string ProductGroupName { get; set; }//EnquiryDetail tbl
        public Nullable<int> MatrixId { get; set; }
        public string MatrixName { get; set; }//EnquiryDetail tbl
    }
}
