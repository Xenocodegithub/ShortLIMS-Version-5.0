using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace LIMS_DEMO.Areas.Enquiry.Models
{
    public class ContractModel
    {
        [Required(ErrorMessage ="Please select Sample Type")]
        [Display(Name = "Sample Type Name")]
        public int SampleTypeProductId { get; set; }

        [Display(Name = "Sample Type Name")]
        public string SampleTypeProductName { get; set; }
        public string SampleTypeProductCode { get; set; }
       
        [Display(Name = "EnquiryId")]
        public long EnquiryId { get; set; }//EnquiryMaster tbl
        public long EnquiryDetailId { get; set; }//EnquiryDetail tbl
        public long EnquirySampleID { get; set; }//EnquirySampleDetail tbl 

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }//CustomerTypeMaster & CustomerMaster tbls
        public int? CustomerMasterId { get; set; }

        [Display(Name = "Enquiry On")]
        public System.DateTime EnquiryOn { get; set; }//EnquiryMaster tbl

        [Display(Name = "Number of Samples")]
        [Required(ErrorMessage = "The No of Samples field is Required")]
        public int NumberofSamples { get; set; }// EnquiryDetail tbl

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

        public byte FrequencyMasterId { get; set; }//FrequencyMaster tbl for DropDown fetch/EnquirySampleDetail tbl

        [Display(Name = "Frequency")]
        [Required(ErrorMessage = "The Frequency field is Required")]
        public string Frequency { get; set; }//FrequencyMaster tbl for DropDown fetch/EnquirySampleDetail tbl

        [Display(Name = "Sample Collection By")]
        public int SampleCollectedBy { get; set; }//EnquirySampleDetail tbl 

        [Display(Name = "Remark")]
        public string Remark { get; set; }//EnquirySampleDetail tbl 

        [Display(Name = "IsWitness")]
        public bool IsWitness { get; set; }//EnquirySampleDetail tbl 

        [Display(Name = "IsUrgent")]
        public bool IsUrgent { get; set; }//EnquirySampleDetail tbl 

        [Display(Name = " IsReturn ")]
        public bool IsReturn { get; set; }//EnquirySampleDetail tbl 

        public int SerialNo { get; set; }
    }

}