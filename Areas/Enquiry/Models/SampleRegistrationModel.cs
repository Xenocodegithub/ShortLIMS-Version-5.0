using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LIMS_DEMO.Areas.Collection.Models;
using LIMS_DEMO.Areas.WorkOrderCustomer.Models;
using LIMS_DEMO.Core.DropDowns;

namespace LIMS_DEMO.Areas.Enquiry.Models
{
    public class SampleRegistrationModel
    {
     
        public int Quantity { get; set; }
        public int QuotationId { get; set; }
        public int EnvCondtId { get; set; }
        public List<SampleRegistrationModel> ParaDisciplineList { get; set; }
        public int UserRoleId { get; set; }
        public Nullable< bool> StatutoryLimits { get; set; }
        public string AckRemarks { get; set; }
        public bool IsSampleIntact { get; set; }
        public string ReturnedRemarks { get; set; }
        public Nullable<Byte> StatusId { get; set; }
        public string SubContractedParameters { get; set; }
        public string SampleQty { get; set; }
        public string Url { get; set; }
        public string EnvCondts { get; set; }
        public string SampleType { get; set; }
        public string SampleLocation { get; set; }
        public string SampleNameOriginal { get; set; }
        public string SampleNo { get; set; }
        public string RequestNo { get; set; }
        
        public Nullable<DateTime> Date { get; set; }
        public Nullable<DateTime> Date2 { get; set; }
        public Nullable<DateTime> dt2 { get; set; }
        public Nullable<System.TimeSpan> Time2 { get; set; }
        public Nullable<DateTime> ReturnedDate { get; set; }
        public Nullable<DateTime> PDR { get; set; }
        public Nullable<DateTime> returnDate { get; set; }
        public Nullable<DateTime> ProbableDateOfReport { get; set; }
        public long ARCId { get; set; }
        public long EnquiryId { get; set; }
        public string StatusCode { get; set; }
        public bool flag { get; set; }
        public long LocationSampleCollectionID { get; set; }
        public Nullable<DateTime> CollectionDate { get; set; }
        public List<int> ProcedureId { get; set; }
        public bool OVC { get; set; }
        public long EmployeeId { get; set; }
        public long UserMasterID { get; set; }
        public long DeliverId { get; set; }
        public string DeliverName { get; set; }
        public DateTime expSampleDate { get; set; }
        public Nullable<TimeSpan> SampleCollectionTime { get; set; }
        public string WORecvDate { get; set; }
        public string WOEDate { get; set; }
        public bool IsIGST { get; set; }
        public bool IsCGST { get; set; }
        public bool IsSGST { get; set; }
        public int NoOfDays { get; set; }
        public int CountNoOfSample { get; set; }
        public int EnteredBy { get; set; }
        public Nullable<long> Duration { get; set; }
        public Nullable<System.DateTime> SampleCollectionDate { get; set; }
        public string CurrentStatus { get; set; }//EnquiryStatus and StatusMaster tbls
        public int SampleCollectedBy { get; set; }
        public int? AssignToId { get; set; }
        public long EnquiryDetailId { get; set; }
        public long EnquirySampleID { get; set; }
        //public int WorkOrderId { get; set; }
        public int SerialNo { get; set; }
        public string WOUpload { get; set; }
        public List<SampleDeviceEntity> Coll { get; set; }


        [Required]
        
        public string IsReturnedOrIsRetained { get; set; }

        [Required]
       
        public string podr { get; set; }
        
        
        public long SampleCollectionId { get; set; }//SampleCollection tbl
        
        public int WorkOrderID { get; set; }// SampleCollection tbl
        
       
        
        [Display(Name = "Collected By:")]
        public string CollectedBy { get; set; }
        public string CityName { get; set; }
        public string ULRNo { get; set; }
        
        public List<int> SampleDeviceId { get; set; }//SampleDeviceMaster tbl
        public string SampleDevice { get; set; }//SampleDeviceMaster tbl
        public int SampleCollectionDevicesId { get; set; }//SampleCollectionDevice tbl
       
        


        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "The Customer name field is required")]
        public string CustomerName { get; set; }//CustomerTypeMaster & CustomerMaster tbls

        [Display(Name = "Mode Of Communication")]
        public string CommunicationMode { get; set; }//ModeOfCommunication tbl
        public byte ModeOfCommunicationId { get; set; }

        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "The Customer name field is required")]
        public int? CustomerMasterId { get; set; }

        [Display(Name = "WorkOrder Received On")]
        [Required(ErrorMessage = "Please select date")]
        public Nullable<System.DateTime> WorkOrderReceivedDate { get; set; }

        [Display(Name = "Workorder No")]
        [Required(ErrorMessage = "Please enter  Workorder no")]
        public string WorkOrderNo { get; set; }

        [Display(Name = "WorkOrder End Date")]
        [Required(ErrorMessage = "Please select date")]
        public Nullable<System.DateTime> WorkOrderEndDate { get; set; }

        [Display(Name = "Number of Samples")]
        [Required(ErrorMessage = "The No of Samples field is Required")]
        public int NumberofSamples { get; set; }// EnquiryDetail tbl
        [Required(ErrorMessage = "SampleTypeProduct Name is Required")]
        [Display(Name = "Sample Type Name")]
        public int SampleTypeProductId { get; set; }

        [Display(Name = "Sample Type Name")]
        [Required(ErrorMessage = "SampleTypeProduct Name is Required")]
        public string SampleTypeProductName { get; set; }
        public string SampleTypeProductCode { get; set; }

        [Required(ErrorMessage = "The Product Group field is Required")]
        public int ProductGroupId { get; set; }//ProductGroupMaster tbl for DropDown fetch/EnquiryDetail tbl

        [Display(Name = "Product Group")]
        public string ProductGroupName { get; set; }//ProductGroupMaster tbl for DropDown fetch/EnquiryDetail tbl

        [Required(ErrorMessage = "The Sub Group field is Required")]
        public int SubGroupId { get; set; }//SubGroupMaster tbl for DropDown fetch/EnquiryDetail tbl

        [Display(Name = "Sub Group ")]
        public string SubGroupName { get; set; }//SubGroupMaster tbl for DropDown fetch/EnquiryDetail tbl
        public string SubGroupCode { get; set; }

        [Required(ErrorMessage = "The Matrix field is Required")]
        public Nullable<int> MatrixId { get; set; }

        [Display(Name = "Matrix")]
        public string MatrixName { get; set; }//MatrixMaster tbl for DropDown fetch/EnquiryDetail tbl
        public bool IsSelected { get; set; }
        public string ParameterName { get; set; }
        public string Discipline { get; set; }

        [Required]
        public Nullable<decimal> Charges { get; set; }
        public decimal TotalCharges { get; set; }
        public string Remarks { get; set; }
        public string Parameters { get; set; }
        public decimal Cost { get; set; }
        public string SampleName { get; set; }
        public string DisplaySampleName { get; set; }
        [Display(Name = "Discount Amount(%)")]
        public decimal? DiscountAmount { get; set; }

        [Display(Name = "Sample Collection Charges")]
        public decimal? SampleAmount { get; set; }
        [Display(Name = "GST ")]
        public decimal? GSTCharges { get; set; }
        [Display(Name = "Transportation")]
        public decimal? TransportationAmount { get; set; }
        [Display(Name = "Total Amount")]
        public decimal? TotalAmount { get; set; }
        [Display(Name = "Testing Charges")]
        public decimal? TestingCharges { get; set; }
        [Display(Name = "Net Amount")]
        public decimal? NetAmount { get; set; }
        public string WorkOrderUpload { get; set; }
        public string FileName { get; set; }
        public long? CostingId { get; set; }
        public int getID { get; set; }
        public string ContractAmendment { get; set; }
        public int SampleDeviceId2 { get; set; }
        public int ProcedureId2 { get; set; }
        
        public int SampleCollectionProcedureId { get; set; }
       
        public int SerialNo2 { get; set; }
        
        public string ProcedureName { get; set; }
        public List<int> SampleDeviceId1 { get; set; }

    }
    public class WorkOrderCustomerListModel
    {
        public List<SampleCollectionModel> CollectionList { get; set; }
        public long SampleCollectionId { get; set; }
        public long EnquiryId { get; set; }
        public List<int> SampleDeviceId { get; set; }
        [Required(ErrorMessage ="Please select sample type")]
        public int SampleTypeId { get; set; }
        public string SampleType { get; set; }
        public int ProcedureId { get; set; }
        public int EnvCondtId { get; set; }
        public int Quantity { get; set; }
        public string Procedure { get; set; }
        public String EnvCondts { get; set; }
        public List<SampleRegistrationModel> CostList { get; set; }

        public string ContractAmendment { get; set; }
        public bool IsIGST { get; set; }
        public int NoOfDays { get; set; }
        public int CountNoOfSample { get; set; }
        public List<SampleRegistrationModel> WorkOrderCustomerList { get; set; }
        public List<SampleRegistrationModel> ParaList { get; set; }
        public SampleRegistrationModel WorkOrderCustomer { get; set; }
        public List<ParameterModel> ParameterList { get; set; }
        
        public List<TermsAndConditionsModel> TermsList { get; set; }
        public List<FinalWorkOrderModel> FinalWorkOrderList { get; set; }
        public List<SampleLocationModel> locationList { get; set; }
        public SampleLocationModel location { get; set; }
        public string SubGroupCode { get; set; }
        public string SampleTypeProductCode { get; set; }
        public string ProductGroupName { get; set; }
        public string DisplaySampleName { get; set; }
        public string SubGroupName { get; set; }
        public string MatrixName { get; set; }
        public int MatrixId { get; set; }
        [Required(ErrorMessage ="Please Select Sample Type")]
        public int SampleTypeProductId { get; set; }
        public string SampleTypeProductName { get; set; }
        public int ProductGroupId { get; set; }
        public int SubGroupId { get; set; }
        public int WorkOrderId { get; set; }
        public long EnquiryDetailId { get; set; }
        public string SampleName { get; set; }
       public SampleRegistrationModel Sample { get; set; }
        public List<SampleRegistrationModel> DeviceList { get; set; }
        public List<SampleRegistrationModel> ProcedureList { get; set; }

    }
    public class ParameterModel
    {
        
        public string Remarks { get; set; }
        public bool IsSelected { get; set; }
        public long? EnquiryParameterDetailID { get; set; }
        public long? EnquirySampleID { get; set; }
        public long? ParameterMasterId { get; set; }
        public long? ParameterGroupId { get; set; }
        public int? ParameterMappingId { get; set; }
        public string ParameterName { get; set; }
        public byte? DisciplineId { get; set; }
        public string Discipline { get; set; }
        public Nullable<int> UnitId { get; set; }
        public Nullable<decimal> LowerLimit { get; set; }
        public Nullable<decimal> UpperLimit { get; set; }

        [Required]
        public Nullable<decimal> Charges { get; set; }
        public Nullable<int> TestMethodId { get; set; }
        public Nullable<int> LabMasterId { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }

        public string PCBLimit { get; set; }
        public List<TestMethodEntity> TestMethods { get; set; }
        public List<UnitEntity> Units { get; set; }
    }
    public class TermsAndConditionsModel1
    {
        public int TermAndCondtId { get; set; }
        public string TermAndCondt { get; set; }
        public bool IsSelected { get; set; }
        public Nullable<int> WorkOrderID { get; set; }

    }
    public class FinalWorkOrderModel
    {
        public string SampleTypeProductCode { get; set; }
        public int SerialNo { get; set; }
        public bool IsSetPCBLimit { get; set; }
        public string PCBLimit { get; set; }
        public long EnquiryDetailId { get; set; }
        public long EnquirySampleID { get; set; }
        public int WorkOrderId { get; set; }
        public long? CostingId { get; set; }
        public string SampleName { get; set; }
        public string DisplaySampleName { get; set; }
        public string SampleTypeProductName { get; set; }

        [Display(Name = "Product Group")]
        [Required]
        public string ProductGroupName { get; set; }

        [Display(Name = "Sub Group")]
        [Required]
        public string SubGroupName { get; set; }

        [Display(Name = "Matrix")]
        [Required]
        public string MatrixName { get; set; }
        public string ParameterName { get; set; }

        [Display(Name = "Unit Price")]
        [Required]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Quantity")]
        [Required]
        public int Quantity { get; set; }

        [Display(Name = "Total")]
        [Required]
        public decimal? Total { get; set; }

        [Display(Name = "Frequency")]
        [Required(ErrorMessage ="Please Select")]
        public byte? FrequencyMasterId { get; set; }

        [Required]
        public string Location { get; set; }
    }
    public class SampleLocationModel1
    {
        public int count { get; set; }
        public int SampleLocationId { get; set; }
        public string Location { get; set; }
        public int SrNo { get; set; }
        public long EnquiryId { get; set; }//EnquiryMaster tbl
        public long EnquirySampleID { get; set; }
    }
}
   