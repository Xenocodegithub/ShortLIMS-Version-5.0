using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace LIMS_DEMO.Areas.Invoice.Models
{
    public class InvoiceModel
    {
        public long PaymentModeMasterId { get; set; }
        public bool IsActive { get; set; }
        public string Print { get; set; }
        public string DD_ChequeNo_Neft { get; set; }
        public string RecieptNo { get; set; }
        public long PaymentDetailsId { get; set; }
        public Nullable<decimal> BalanceAmount { get; set; }
        public int Count { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        [Display(Name = "Mode of Payment :")]
        public string ModeofPayment { get; set; }
        [Display(Name = "Bank Name :")]
        public string BankName { get; set; }
        [Display(Name = "Branch Name :")]
        public string BranchName { get; set; }
        [Display(Name = "Cheque No :")]
        public string ChequeNo { get; set; }
        public DateTime DD_ChequeDate { get; set; }
        [Display(Name = "Actual Amount :")]
        public string ActualAmount { get; set; }
        [Display(Name = "Payment Date:")]
        public DateTime PaymentDate { get; set; }
        public string Amount { get; set; }
        public long EnquiryMasterSampleTypeId { get; set; }
        public long EnquirySampleID { get; set; }
        [Display(Name = "Invoice Number:")]
        public string InvoiceNumber { get; set; }
        public string SampleName { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string ProductGroupName { get; set; }
        public string SampleTypeProductName { get; set; }
        public string SubGroupName { get; set; }
        public string MatrixName { get; set; }
        public string Particulars { get; set; }
        public long? CostingId { get; set; }

        [Display(Name = "Total Charges:")]
        public decimal? TotalCharges { get; set; }

        [Display(Name = "Sample Collection Charges:")]
        public decimal? SampleAmount { get; set; }
        [Display(Name = "Testing Charges:")]
        public decimal? TestingCharges { get; set; }

        [Display(Name = "Transportation Charge:")]
        public decimal? TransportationAmount { get; set; }

        [Display(Name = "Discount Amount(%):")]
        public decimal? DiscountAmount { get; set; }

        [Display(Name = "Net Amount:")]
        public decimal? NetAmount { get; set; }

        [Display(Name = "GST: ")]
        public decimal? GSTCharges { get; set; }

        [Display(Name = "CGST: ")]
        public decimal? CGSTCharges { get; set; }

        [Display(Name = "SGST:")]
        public decimal? SGSTCharges { get; set; }

        [Display(Name = "IGST:")]
        public decimal? IGSTCharges { get; set; }

        [Display(Name = "Total Amount:")]
        public decimal? TotalAmount { get; set; }

        [Display(Name = "Paid Amount:")]
        public decimal? PaidAmount { get; set; }
        public decimal? Balance { get; set; }
        public decimal? Total { get; set; }
      
        public string  Action { get; set; }
        public int WorkOrderSampleCollectionDateId { get; set; }
        public Nullable<int> LabMasterId { get; set; }
        public string FileName { get; set; }
        public string WOUpload { get; set; }
        public System.DateTime WORecieveDate { get; set; }
        [Display(Name = "Registration Name:")]
        
        public string RegistrationName { get; set; }
        public int? CompanyTypeId { get; set; }
        public string EnquiryNo { get; set; }
        [Display(Name = "Work Order No:")]
        public string WorkOrderNo { get; set; }
        
        public DateTime ChequeDate { get; set; }
        public long EnquiryId { get; set; }
        public long ReceiptNo { get; set; }
        public int WorkOrderId { get; set; }
        public string ApprovedAmount { get; set; }
        public string CurrentStatus { get; set; }
        public string Remarks { get; set; }
        public string Date { get; set; }
        public int? AssignToId { get; set; }
        public Nullable<bool> IsIGST { get; set; }
        public int SrNo { get; set; }
        [Display(Name = "Payment Mode:")]
        public string PaymentMode { get; set; }
        public List<InvoiceModel> GridModel { get; set; }

        public long InvoiceRejectDetailsId { get; set; }
        public Nullable<long> InvoiceDetailsId { get; set; }
        
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    }
}