using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Invoice
{
    public class InvoiceEntity
    {
        public int ID { get; set; }
        public string RegistrationName { get; set; }
        public string WorkOrderNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<decimal> BalanceAmount { get; set; }
        public string BranchName { get; set; }
        public int Count { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public int EnquirySampleID { get; set; }
        public int CostingId { get; set; }
        public Nullable<decimal> TotalCharges { get; set; }
        public Nullable<decimal> CollectionCharges { get; set; }
        public Nullable<decimal> TransportCharges { get; set; }
        public Nullable<decimal> TestingCharges { get; set; }
        public Nullable<decimal> GST { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public int WorkOrderSampleCollectionDateId { get; set; }
        public Nullable<int> LabMasterId { get; set; }
        public string FileName { get; set; }
        public string WOUpload { get; set; }
        public System.DateTime WORecieveDate { get; set; }
        
        public int? CompanyTypeId { get; set; }
        public string EnquiryNo { get; set; }
        public string WorkOrderNo { get; set; }
        public long EnquiryId { get; set; }
        public int WorkOrderId { get; set; }
        public string ApprovedAmount { get; set; }
        public string CurrentStatus { get; set; }
        public string Remarks { get; set; }
        public string Date { get; set; }
        public int? AssignToId { get; set; }
        public Nullable<bool> IsIGST { get; set; }

        public long PaymentDetailsId { get; set; }
        public string RecieptNo { get; set; }
        //public Nullable<decimal> PaidAmount { get; set; }
        //public System.DateTime Date { get; set; }
        public string PaymentMode { get; set; }
        public string BankName { get; set; }
        public string DD_ChequeNo_Neft { get; set; }
        public System.DateTime DD_ChequeDate { get; set; }
        public string Print { get; set; }
        public DateTime EnteredDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date1 { get; set; }

        public long InvoiceRejectDetailsId { get; set; }
        public Nullable<long> InvoiceDetailsId { get; set; }

        public int EnteredBy { get; set; }
      
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public string RejectStatus { get; set; }
    }
    public class invoice_list
    {
        public InvoiceEntity invoiceDetails { get; set; }
        public List<InvoiceEntity> invoiceDetailsList { get; set; }
        public InvoiceEntity DeleteInvoice { get; set; }
    }
    
}