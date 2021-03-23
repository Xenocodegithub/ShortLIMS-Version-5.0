using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Enquiry.Models
{
    public class CostingModel
    {
        public string DisplaySampleName { get; set; }
        public int SrNo { get; set; }
        public long EnquiryMasterSampleTypeId { get; set; }
        public long EnquirySampleID { get; set; }
        public string SampleName { get; set; }
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
        public string Parameters { get; set; }
    }
    public class CostingListModel
    {
      
        public string Remarks { get; set; }
        public long EnquiryId { get; set; }

        [Display(Name = "Enquiry No.")]
        public long EnquiryNo { get; set; }//EnquiryMaster tbl

        [Display(Name = "Customer Name")]
        public string CustomerTypeName { get; set; }//CustomerTypeMaster & CustomerMaster tbls

        [Display(Name = "Enquiry On")]
        public System.DateTime EnquiryOn { get; set; }//EnquiryMaster tbl

        public List<SampleAndParametersModel> SampleList { get; set; }

        public List<TermsAndConditionsModel> TermsList { get; set; }
        //public List<CostingModel> costingList { get; set; }

    }
}