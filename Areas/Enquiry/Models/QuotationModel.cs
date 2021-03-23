using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Enquiry.Models
{
    public class QuotationModel
    {
        //-----------------custmer---------------------------
        public long QuotationId { get; set; }
        public string QuotationNo { get; set; }
        public string EnquiryNo { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }
        [Display(Name = "Company Name:")]
        public string Company { get; set; }

        [Display(Name = "Address:")]
        public string Address1 { get; set; }

        [Display(Name = "Phone No:")]
        public string PhoneNo { get; set; }

        [Display(Name = "Mobile No:")]
        public string MobileNo { get; set; }

        [Display(Name = "Email Address:")]
        public string Email { get; set; }

        [Display(Name = "FAX No")]
        public string FaxNo { get; set; }


        //-------------------Enquiry---------------------------

        [Display(Name = "EnquiryId")]
        public long EnquiryId { get; set; }//EnquiryMaster tbl
        public long EnquiryDetailId { get; set; }//EnquiryDetail tbl
        public decimal? TotalCharges { get; set; }

        public List<QuotationListModel> GetDetails { get; set; }
    }
    public class QuotationListModel
    {
        public string SampleName { get; set; }
        public string ProductGroupName { get; set; }//ProductGroupMaster tbl for DropDown fetch
        public int SubGroupId { get; set; }//SubGroupMaster tbl for DropDown fetch
        public string SubGroupName { get; set; }//SubGroupMaster tbl for DropDown fetch
        public long EnquirySampleID { get; set; }//EnquirySampleDetail tbl 
        public int ProductGroupId { get; set; }//ProductGroupMaster tbl for DropDown fetch
        public Nullable<int> MatrixId { get; set; }
        public string MatrixName { get; set; }//MatrixMaster tbl for DropDown fetch
        public int ProdGrpSubGrpMatId { get; set; }//ProdGrpSubGrpMatrixMapping tbl for Mapping
        public byte FrequencyMasterId { get; set; }//FrequencyMaster tbl for DropDown fetch
        public string Frequency { get; set; }//FrequencyMaster tbl for DropDown fetch
        public int NumberofSamples { get; set; }// EnquiryDetail tbl
        //---------------------Parameter & costing----------------------------
        public string ParameterName { get; set; }
        public string Discipline { get; set; }
        public int Unit { get; set; }
        public int ParameterRate { get; set; }
        public int Charges { get; set; }
        public int NetAmount { get; set; }
        public int TotalAmount { get; set; }

    }

    public class QuotationPreviewModel
    {
        public long EnquiryId { get; set; }
        public string LabName { get; set; }
        public string RefName { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Designation { get; set; }
        public string ClientMobileNo { get; set; }
        public string ClientEmail { get; set; }
        public string PAN { get; set; }
        public string GSTNO { get; set; }
        public string SACNO { get; set; }
        public string BANKERS { get; set; }
        public string PFNO { get; set; }
        public string ESICNO { get; set; }
        public string PTNO { get; set; }
        public string TDSNO { get; set; }
        public string MSMEDA { get; set; }
        public string IECNO { get; set; }
        public string LABAddress { get; set; }
        public string LABPhone { get; set; }
        public string LABMobileFax { get; set; }
        public string LABEmail { get; set; }
        public string RevisedDates { get; set; }
        public string ContactMobile { get; set; }
        public string ClientFax { get; set; }
        public string LABCity { get; set; }
        public List<CostingModel> costing { get; set; }
        public List<TermsAndConditionsModel> TermsList { get; set; }
        public HeadOfficeModel headOffice { get; set; }
    }
}