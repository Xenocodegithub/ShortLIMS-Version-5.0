using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
//using LIMS_DEMO.Core.Collection;

namespace LIMS_DEMO.Areas.Collection.Models
{
    public class SampleCollectionModel
    {
        public bool IsFieldSelected { get; set; }
       
        
        public string IsReturnedOrIsRetained { get; set; }
        public bool IsLabSelected { get; set; }
        public Nullable<long> No { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<int> WorkOrderSampleCollectionDateId { get; set; }
        public int LocationSampleCollectionID { get; set; }
        public int SampleTypeProductId { get; set; }
        public string SampleTypeProductName { get; set; }
        public string DisplaySampleName { get; set; }
        public string Duration { get; set; }
        public string SubGroupName { get; set; }
        public int SubGroupId { get; set; }
        public int CollBy { get; set; }

        [Required]
        public bool IsSelected { get; set; }
        public string StatusCodeLab { get; set; }
        public string StatusCodeField { get; set; }
        public long ARCId { get; set; }
        public int UserRoleId { get; set; }
        public int SampleDeviceId2 { get; set; }
        public int ProcedureId2 { get; set; }
        public int SampleCollectionDevicesId { get; set; }
        public int SampleCollectionProcedureId { get; set; }
        public int SerialNo { get; set; }
        public int SerialNo2 { get; set; }
        public long SampleCollectionId { get; set; }//SampleCollection tbl
        public int WorkOrderID { get; set; }//SampleCollection tbl

        [Display(Name = "WorkOrder No.")]
        public string WorkOrderNo { get; set; }// WorkOrder tbl

        [Display(Name = " Sample to be Collected On")]
        public string CollectedOn { get; set; }
        public System.DateTime ExpectSampleCollDate { get; set; }// WorkOrder tbl
        public long EnquirySampleID { get; set; }//SampleCollection tbl
        public byte StatusId { get; set; }//SampleCollection tbl
        [Display(Name = "Status")]
        public string CurrentStatus { get; set; }// StatusMaster tbls
        public Nullable<int> FieldDeterminationId { get; set; }//SampleCollection tbl

        [Display(Name = "Iteration")]
        public Nullable<int> Iteration { get; set; }//SampleCollection tbl
        //public Nullable<bool> IsWitness { get; set; }//SampleCollection tbl
        public bool IsWitness { get; set; }//SampleCollection tbl

        [Display(Name = "In the Presence Of")]
        public string WitnessName { get; set; }//SampleCollection tbl
        public string SampleNo { get; set; }//SampleCollection tbl
        public bool IsSampleIntact { get; set; }//SampleCollection tbl

        [Display(Name = "Industry Type")]
        public string IndustryType { get; set; }//SampleCollection tbl

      [Required]
        public string Source { get; set; }//SampleCollection tbl
        public string Location { get; set; }//EnquirySampleDetail tbl
        public string RequestNo { get; set; }//SampleCollection tbl
        public string ULRNo { get; set; }//SampleCollection tbl
        public bool IsActive { get; set; }//SampleCollection tbl
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public long EnquiryDetailId { get; set; }//EnquirySampleDetail tbl
        public long EnquiryId { get; set; }//EnquiryMaster 
        public int CustomerMasterId { get; set; }//EnquiryMaster & CustomerMaster tbl

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }//CustomerMaster tbl
        public string Address { get; set; }//CustomerMaster tbl

        [Display(Name = "Contact NO.")]
        public string ContactNO { get; set; }//CustomerMaster tblF

        [Display(Name = "Sample No.")]
        public string SampleName { get; set; }//EnquirySampleDetail tbl
        public Nullable<int> SampleCollectedBy { get; set; }//EnquirySampleDetail tbl

        [Display(Name = "Collected By")]
        public string CollectedBy { get; set; }//Variable
        public string Url { get; set; }//Variable

        [Display(Name = "Collector Name")]
        public string EmployeeId { get; set; }//EnquirySampleDetail tbl
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
         [Required]
        [Display(Name = "Sampling Date")]
        public Nullable<System.DateTime> CollectionDate { get; set; }//EnquirySampleDetail tbl
        [Required]
        [Display(Name = "Sampling Time")]
        public Nullable<System.TimeSpan> SampleCollectionTime { get; set; }//EnquirySampleDetail tbl
        [Required]
        [Display(Name = "Location")]
        public string SampleLocation { get; set; }//SampleCollection tbl

        [Display(Name = "Remarks")]
        public string Remark { get; set; }//EnquirySampleDetail tbl

        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// 
        
        [Display(Name = "Sample Description")]
        public Nullable<int> SampleDescriptionId { get; set; }//SampleDescriptionMaster tbl same as <sample source>

        [Required]
        [Display(Name = "Sample Description")]
        public string SampleDescription { get; set; }//SampleDescriptionMaster tbl same as <sample source>

        [Required]
        [Display(Name = "Sample Device")]
        public List<int> SampleDeviceId { get; set; }//SampleDeviceMaster tbl

        [Display(Name = "Sampling Media")]
        public string SampleDevice { get; set; }//SampleDeviceMaster tbl

        [Required]
        [Display(Name = "Procedure")]
        //public Nullable<int> ProcedureId { get; set; }//ProcedureMaster tbl
        public List<int> ProcedureId { get; set; }//ProcedureMaster tbl

        [Display(Name = "Procedure")]
        public string ProcedureName { get; set; }//ProcedureMaster tbl

        [Required]
        [Display(Name = "Sample Type")]
        public Nullable<int> SampleTypeId { get; set; }//SampleType same as <subgroup> SampleTypeMaster tbl

     
        [Display(Name = "Sample Type")]
        public string SampleType { get; set; }//SampleType same as <subgroup> SampleTypeMaster tbl

        [Required]
        public Nullable<int> SampleQtyId { get; set; }
        //public int SampleQtyId { get; set; }//SampleQtyMaster tbl & QuantityPreservative tbl

        [Display(Name = "Quantity")]
        public string SampleQty { get; set; }//SampleQtyMaster tbl
        public string Preservation { get; set; }//SampleQtyMaster tbl 
        public int QtyPreservativeId { get; set; }//QuantityPreservative tbl
        public bool ISGivenPreservative { get; set; }//QuantityPreservative tbl

        [Required]
        [Display(Name = "Environmental Condition")]
        public Nullable<int> EnvCondtId { get; set; }//EnvironmentalCondition tbl

        [Display(Name = "Environmental Condition")]
        public string EnvCondts { get; set; }//EnvironmentalCondition tbl
       
        public int ProductGroupId { get; set; }//EnquiryDetail tbl
        public string ProductGroupName { get; set; }//EnquiryDetail tbl
        public Nullable<int> MatrixId { get; set; }
        public string MatrixName { get; set; }//EnquiryDetail tbl
        public string ReturnedRemarks { get; set; }
        public string AckRemarks { get; set; }
        public bool StatutoryLimits { get; set; }
        public string SubContractedParameters { get; set; }
        public int Quantity { get; set; }

    }

    public class CollectionListModel
    {
        public Nullable<System.DateTime> ProbableDateOfReport { get; set; }
        public Nullable<System.DateTime> CollDate { get; set; }//EnquirySampleDetail tbl
        public Nullable<System.DateTime> CollectionDate { get; set; }//EnquirySampleDetail tbl
        public List<SampleCollectionModel> CollectionList { get; set; }
        public List<SampleCollectionModel> DeviceList { get; set; }
        public List<SampleCollectionModel> ProcedureList { get; set; }
        public SampleCollectionModel Collection { get; set; }
    }
}