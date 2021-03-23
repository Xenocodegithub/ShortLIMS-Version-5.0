using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using LIMS_DEMO.Core.DropDowns;


namespace LIMS_DEMO.Areas.Arrival.Models
{
    public class SampleArrivalModel
    {
        public string PDR { get; set; }
        public long No { get; set; }
        public int ApproverId { get; set; }
        public bool flag { get; set; }

        [Display(Name = "Remarks")]
        public string ReturnedRemarks { get; set; }
        public string DateofRecieptofSample { get; set; }
        public string returnDate { get; set; }
        public Nullable<DateTime> ReturnedDate { get; set; }
        public string dt2 { get; set; }
        public string   podr { get; set; }
        public string Field { get; set; }
        public Nullable<bool> InField { get; set; }

        [Required]
        public string IsReturnedOrIsRetained { get; set; }

        [Required]
        public Nullable<bool> StatutoryLimits { get; set; }

        [Required]
        public string SubContractedParameters { get; set; }

        [Required]
        public string AckRemarks { get; set; }
        public int WorkOrderSampleCollectionDateId { get; set; }
        public string SampleNameOriginal { get; set; }
        public string Parameters { get; set; }//Doubt

        [Display(Name = "SampleType ProductName")]
        public string SampleTypeProductName { get; set; }
        public int SampleTypeProductId { get; set; }
        public int LocationSampleCollectionID { get; set; }
        public string Duration { get; set; }
        public int ParameterGroupId { get; set; }
        public Nullable<byte> DisciplineId { get; set; }
        public string Discipline { get; set; }
        public Nullable<int> PlannerId{ get; set; }
        public string CityName { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public int SerialNo { get; set; }
        public string Url { get; set; }//Variable
        public long ARCId { get; set; }

        [Required]
        [Display(Name = "Sample Recevied in Lab By")]
        public int UserRoleId { get; set; }
        public Nullable<System.DateTime> ActionDate { get; set; }

        [Required]
        [Display(Name = "Probable Date of Report:")]
        public Nullable<System.DateTime> ProbableDateOfReport { get; set; }
        public long EnquiryParameterDetailID { get; set; }//EnquiryParameterDetail
        public int ParameterMappingId { get; set; }//EnquiryParameterDetail
        public int UnitId { get; set; }//EnquiryParameterDetail
        public string Unit { get; set; }//UnitMaster
        public long ParameterMasterId { get; set; }//ParameterMapping
        public string ParameterName { get; set; }//ParameterMaster
        public string RequestNo { get; set; }
        public string ULRNo { get; set; }
        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public long SampleCollectionId { get; set; }//SampleCollection tbl
        public long EnquirySampleID { get; set; }//EnquirySampleDetail tbl/SampleCollection
        public long EnquiryDetailId { get; set; }//EnquirySampleDetail tbl
        public long EnquiryId { get; set; }
        public int WorkOrderID { get; set; }// SampleCollection tbl
        public long QuotationId { get; set; }// WorkOrder tbl
        //public int FieldDeterminationId { get; set; }

        [Display(Name = "WorkOrder No.")]
        public int WorkOrderNo { get; set; }// WorkOrder tbl
        public int CustomerMasterId { get; set; }//EnquirySampleDetail tbl

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Contact NO.")]
        public string ContactNO { get; set; }

        [Display(Name = "Sample No.:")]
        public string SampleNo { get; set; }//SampleCollection tbl
        public string SampleName { get; set; }
        public string Date { get; set; }//Doubt
        public Nullable<byte> StatusId { get; set; }//SampleCollection tbl

        [Display(Name = "Status")]
        public string CurrentStatus { get; set; }// EnquiryStatus and StatusMaster tbls
        public string StatusCode { get; set; }
        public Nullable<int> FieldDeterminationId { get; set; }//SampleCollection tbl
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        [Required(ErrorMessage = "Product Group field is Required")]
        public int ProductGroupId { get; set; }//EnquiryDetail tbl

        [Display(Name = "Product Group:")]
        public string ProductGroupName { get; set; }//EnquiryDetail tbl

        [Required(ErrorMessage = "Sub Group field is Required")]
        public int SubGroupId { get; set; }//EnquiryDetail tbl

        [Display(Name = "Sub Group:")]
        public string SubGroupName { get; set; }//EnquiryDetail tbl
        public string SubGroupCode { get; set; }

        [Required(ErrorMessage = "Matrix field is Required")]
        public Nullable<int> MatrixId { get; set; }

        [Display(Name = "Matrix:")]
        public string MatrixName { get; set; }//EnquiryDetail tbl

        [Display(Name = "Date of Sampling:")]
        public Nullable<System.DateTime> CollectionDate { get; set; }//EnquirySampleDetail tbl

        [Display(Name = "Time of Sampling:")]
        public Nullable<System.TimeSpan> SampleCollectionTime { get; set; }//EnquirySampleDetail tbl

        [Display(Name = "Location:")]
        public string SampleLocation { get; set; }//SampleCollection tbl
        public int SampleCollectionDevicesId { get; set; }//SampleCollectionDevice tbl
        public Nullable<int> SampleDeviceId { get; set; }//SampleDeviceMaster tbl

        [Display(Name = "Sample Device:")]
        public string SampleDevice { get; set; }//SampleDeviceMaster tbl
        public Nullable<int> SampleTypeId { get; set; }//SampleType same as <subgroup> SampleTypeMaster tbl

        [Display(Name = "Sample Type:")]
        public string SampleType { get; set; }//SampleType same as <subgroup> SampleTypeMaster tbl
        public Nullable<int> EnvCondtId { get; set; }//EnvironmentalCondition tbl

        [Display(Name = "Environmental Condition:")]
        public string EnvCondts { get; set; }//EnvironmentalCondition tbl

        public int SampleQtyId { get; set; }//SampleQtyMaster tbl & QuantityPreservative tbl

        [Display(Name = "Quantity:")]
        public string SampleQty { get; set; }//SampleQtyMaster tbl
        public string Preservation { get; set; }//SampleQtyMaster tbl 
        public int QtyPreservativeId { get; set; }//QuantityPreservative tbl
        public bool ISGivenPreservative { get; set; }//QuantityPreservative tbl
        public string GivenPreservative { get; set; }

        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
      

        public string Units { get; set; }//Doubt
        public Nullable<int> SampleCollectedBy { get; set; }//EnquirySampleDetail tbl

        [Display(Name = "Collected By:")]
        public string CollectedBy { get; set; }

        [Display(Name = "Collector Name:")]
        public string EmployeeId { get; set; }//EnquirySampleDetail tbl
        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////
        /// </summary>
        
        [Display(Name = "Sample Recevied in Lab By:")]
        public string SampleReceviedLabBy { get; set; }//Doubt

        [Required]
        [Display(Name = "Date:")]
        public Nullable<System.DateTime> Date2 { get; set; }//Doubt

        [Required]
        [Display(Name = "Time:")]
        public Nullable<System.TimeSpan> Time2 { get; set; }//Doubt

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public Nullable<bool> IsWitness { get; set; }//SampleCollection tbl

        [Display(Name = "In the Presence Of :")]
        public string WitnessName { get; set; }//SampleCollection tbl
        public string Condition { get; set; }
        public bool IsSampleIntact { get; set; }//SampleCollection tbl
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        public List<ParameterDisciplineModel> ParaDisciplineList { get; set; }
    

    }
   
    public class ParameterDisciplineModel
    {
        public string Parameters { get; set; }//Doubt
        public int SerialNo { get; set; }
        public long EnquirySampleID { get; set; }//EnquirySampleDetail tbl/SampleCollection
        public int ParameterGroupId { get; set; }
        public Nullable<byte> DisciplineId { get; set; }
        public string Discipline { get; set; }
        public List<UserListEntity> Planner { get; set; }
        public List<UserListEntity> Approver { get; set; }
        public Nullable<int> PlannerId { get; set; }
        public Nullable<int> ApproverId { get; set; }
        public long EnquiryParameterDetailID { get; set; }//EnquiryParameterDetail
        public int ParameterMappingId { get; set; }//EnquiryParameterDetail
        public int UnitId { get; set; }//EnquiryParameterDetail
        public string Unit { get; set; }//UnitMaster
        public long ParameterMasterId { get; set; }//ParameterMapping
        public string ParameterName { get; set; }//ParameterMaster
    }
}