using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace LIMS_DEMO.Areas.Invoice.Models
{
    public class InvoiceModel
    {
        public long EnquiryMasterSampleTypeId { get; set; }
        public long EnquirySampleID { get; set; }
        public string SampleName { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string ProductGroupName { get; set; }
        public string SampleTypeProductName { get; set; }
        public string SubGroupName { get; set; }
        public string MatrixName { get; set; }
        public string Particulars { get; set; }
        public long? CostingId { get; set; }

        [Display(Name = "Total Charges")]
        public decimal? TotalCharges { get; set; }

        [Display(Name = "Sample Collection Charges")]
        public decimal? SampleAmount { get; set; }
        [Display(Name = "Testing Charges")]
        public decimal? TestingCharges { get; set; }

        [Display(Name = "Transportation")]
        public decimal? TransportationAmount { get; set; }

        [Display(Name = "Discount Amount(%)")]
        public decimal? DiscountAmount { get; set; }

        [Display(Name = "Net Amount")]
        public decimal? NetAmount { get; set; }

        [Display(Name = "GST ")]
        public decimal? GSTCharges { get; set; }

        [Display(Name = "CGST ")]
        public decimal? CGSTCharges { get; set; }

        [Display(Name = "SGST")]
        public decimal? SGSTCharges { get; set; }

        [Display(Name = "IGST")]
        public decimal? IGSTCharges { get; set; }

        [Display(Name = "Total Amount")]
        public decimal? TotalAmount { get; set; }

        public decimal? Total { get; set; }
        public int WorkOrderSampleCollectionDateId { get; set; }
        public Nullable<int> LabMasterId { get; set; }
        public string FileName { get; set; }
        public string WOUpload { get; set; }
        public System.DateTime WORecieveDate { get; set; }
        public string RegistrationName { get; set; }
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
    }
}