using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace LIMS_DEMO.Areas.Enquiry.Models
{
    public class WorkOrderModel
    {
        public string SampleTypeProductCode { get; set; }
        public int CountNoOfSample { get; set; }
        public string PCBLimit { get; set; }
        public int SrNo { get; set; }
        public long EnquirySampleID { get; set; }
        public long? CostingId { get; set; }
        public string SampleName { get; set; }
        public string DisplaySampleName { get; set; }


        [Required(ErrorMessage = "Product Group field is Required")]
        public int ProductGroupId { get; set; }//ProductGroupMaster tbl for DropDown fetch/EnquiryDetail tbl

        [Display(Name = "Product Group")]
        public string ProductGroupName { get; set; }//ProductGroupMaster tbl for DropDown fetch/EnquiryDetail tbl
        [Display(Name = "Product Group")]
        public string SampleTypeProductName { get; set; }
        [Required(ErrorMessage = "Sub Group field is Required")]
        public int SubGroupId { get; set; }//SubGroupMaster tbl for DropDown fetch/EnquiryDetail tbl

        [Display(Name = "Sub Group ")]
        public string SubGroupName { get; set; }//SubGroupMaster tbl for DropDown fetch/EnquiryDetail tbl
        public string SubGroupCode { get; set; }

        [Required(ErrorMessage = "Matrix field is Required")]
        public Nullable<int> MatrixId { get; set; }

        [Display(Name = "Matrix")]
        public string MatrixName { get; set; }

        [Display(Name = "Parameter Name")]
        public string ParameterName { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Status")]
        public string CurrentStatus { get; set; }

        [Display(Name = "Frequency")]
        public byte? FrequencyMasterId { get; set; }
        public string Frequency { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Unit Price")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Total")]
        public decimal? Total { get; set; }

        public string Location { get; set; }
        public bool IsSetPCBLimit { get; set; }
    }
    public class WorkOrderListModel
    {
        public string ContractAmendment { get; set; }
        public bool IsIGST { get; set; }
        public int CountNoOfSample { get; set; }
        public int NoOfDays { get; set; }
        public string DisplaySampleName { get; set; }
        public string FileName { get; set; }
        //public string ParameterName { get; set; }

        [Display(Name = "Customer Name")]
        public string RegistrationName { get; set; }
        public int? CustomerMasterId { get; set; }
        public long QuotationLogId { get; set; }
        public long QuotationId { get; set; }
        public int EnquiryId { get; set; }

        public int? WorkOrderId { get; set; }
        public int WorkOrderSampleCollectionDateId { get; set; }

        [Required(ErrorMessage = "Please Enter Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "WorkOrder Received On")]
        [Required(ErrorMessage ="Please select date")]
        public Nullable<System.DateTime> WorkOrderReceivedDate { get; set; }

        [Display(Name = "Workorder No")]
        [Required(ErrorMessage = "Please enter  Workorder no")]
        public string WorkOrderNo { get; set; }

        [Display(Name = "WorkOrder End Date")]
        [Required(ErrorMessage = "Please select date")]
        public Nullable<System.DateTime> WorkOrderEndDate { get; set; }

        public string WOEDate { get; set; }

        [Display(Name = "Sample Collection Date")]
        [Required(ErrorMessage = "Please select date")]
        public Nullable<System.DateTime> SampleCollectionDate { get; set; }

        [Required(ErrorMessage ="Please Enter Duration")]
        public Nullable<long> Duration { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        [Display(Name = "Quotation Amount")]
        public decimal? QuotationAmount { get; set; }
        public List<WorkOrderModel> workorderList { get; set; }
        public string WorkOrderUpload { get; set; }
    }

}
