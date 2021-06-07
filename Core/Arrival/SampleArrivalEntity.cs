using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Arrival
{
    public class SampleArrivalEntity
    {
        //public string SampleCollectedBy { get; set; }
        public Nullable<long> WorkOrderID { get; set; }
        public Nullable<long> No { get; set; }
        public Nullable<System.DateTime> ReturnedDate { get; set; }
        public string ReturnedRemark { get; set; }
        public Nullable<bool> IsNABLAccredited { get; set; }
        public Nullable<bool> InField { get; set; }
        public int? TestMethodId { get; set; }
        public string TestMethodName { get; set; }
        
        public string IsReturnedOrIsRetained { get; set; }
        public Nullable<bool> StatutoryLimits { get; set; }
        public string SubContractedParameters { get; set; }
        public string AckRemarks { get; set; }
        public string SampleTypeProductName { get; set; }
        public int SampleTypeProductId { get; set; }
        public int LocationSampleCollectionID { get; set; }
        public string Duration { get; set; }
        public int ParameterGroupId { get; set; }
        public Nullable<byte> DisciplineId { get; set; }
        public string Discipline { get; set; }
        public Nullable<int> PlannerId { get; set; }
        public Nullable<int> ApproverId { get; set; }
        public Nullable<int> PlannerIdChem { get; set; }
        public Nullable<int> PlannerIdBio { get; set; }

        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public long ARCId { get; set; }
        public int UserRoleId { get; set; }
        public Nullable<System.DateTime> ActionDate { get; set; }
        public Nullable<System.DateTime> ProbableDateOfReport { get; set; }
        public long EnquiryParameterDetailID { get; set; }//EnquiryParameterDetail
        public int ParameterMappingId { get; set; }//EnquiryParameterDetail
        public int UnitId { get; set; }//EnquiryParameterDetail
        public string Unit { get; set; }//UnitMaster
        public long ParameterMasterId { get; set; }//ParameterMapping
        public string ParameterName { get; set; }//ParameterMaster
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public string SrNo { get; set; }
        public long SampleCollectionId { get; set; }//SampleCollection tbl
        public Nullable<long> EnquirySampleID { get; set; }//EnquirySampleDetail tbl/SampleCollection
        public long EnquiryDetailId { get; set; }//EnquirySampleDetail tbl
        public long EnquiryId { get; set; }
       // public int WorkOrderID { get; set; }// SampleCollection tbl
        public long QuotationId { get; set; }// WorkOrder tbl
        //public int FieldDeterminationId { get; set; }
        public int WorkOrderNo { get; set; }// WorkOrder tbl
        public int CustomerMasterId { get; set; }//EnquirySampleDetail tbl
        public string CustomerName { get; set; }
        public string CityName { get; set; }
        public string ContactNO { get; set; }
        public string SampleNo { get; set; }//SampleCollection tbl
        public string SampleName { get; set; }
        public string SampleNameOriginal { get; set; }
        public Nullable<System.DateTime> Date { get; set; }//Doubt
        public Nullable<byte> StatusId { get; set; }//SampleCollection tbl
        public string CurrentStatus { get; set; }// EnquiryStatus and StatusMaster tbls
        public string StatusCode { get; set; }
        public string RequestNo { get; set; }
        public string ULRNo { get; set; }
        public Nullable<int> FieldDeterminationId { get; set; }//SampleCollection tbl
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public int ProductGroupId { get; set; }//EnquiryDetail tbl
        public string ProductGroupName { get; set; }//EnquiryDetail tbl
        public int SubGroupId { get; set; }//EnquiryDetail tbl
        public string SubGroupName { get; set; }//EnquiryDetail tbl
        public string SubGroupCode { get; set; }
        public Nullable<int> MatrixId { get; set; }
        public string MatrixName { get; set; }//EnquiryDetail tbl
        public Nullable<System.DateTime> CollectionDate { get; set; }//EnquirySampleDetail tbl
        public Nullable<System.TimeSpan> SampleCollectionTime { get; set; }//EnquirySampleDetail tbl
        public string SampleLocation { get; set; }//SampleCollection tbl
        public int SampleCollectionDevicesId { get; set; }//SampleCollectionDevice tbl
        public List<int> SampleDeviceId { get; set; }//SampleDeviceMaster tbl
        public int SampleDeviceId1 { get; set; }
        public string SampleDevice { get; set; }//SampleDeviceMaster tbl
        public Nullable<int> SampleTypeId { get; set; }//SampleType same as <subgroup> SampleTypeMaster tbl
        public string SampleType { get; set; }//SampleType same as <subgroup> SampleTypeMaster tbl
        public Nullable<int> EnvCondtId { get; set; }//EnvironmentalCondition tbl
        public string EnvCondts { get; set; }//EnvironmentalCondition tbl
        public Nullable<int> SampleQtyId { get; set; }//SampleQtyMaster tbl & QuantityPreservative tbl
        public string SampleQty { get; set; }//SampleQtyMaster tbl
        public string Preservation { get; set; }//SampleQtyMaster tbl 
        public int QtyPreservativeId { get; set; }//QuantityPreservative tbl
        public bool ISGivenPreservative { get; set; }//QuantityPreservative tbl

        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public string Parameters { get; set; }//Doubt
        public string Units { get; set; }//Doubt
        public Nullable<int> SampleCollectedBy { get; set; }//EnquirySampleDetail tbl
        public string CollectedBy { get; set; }
        public string EmployeeId { get; set; }//EnquirySampleDetail tbl
        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////
        /// </summary>
        public string SampleReceviedLabBy { get; set; }//Doubt
        public Nullable<System.DateTime> Date2 { get; set; }//Doubt
        public Nullable<System.TimeSpan> Time2 { get; set; }//Doubt
        public string DateofReport { get; set; }//Doubt
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public Nullable<bool> IsWitness { get; set; }//SampleCollection tbl
        public string WitnessName { get; set; }//SampleCollection tbl
        public string Condition { get; set; }
        public bool IsSampleIntact { get; set; }//SampleCollection tbl
    }
}
