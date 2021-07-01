using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace LIMS_DEMO.Areas.Enquiry.Models
{
    public class SampleAndParametersModel
    {
        public int LocationId { get; set; }
        public bool Infield { get; set; }
        public long EnquiryMasterSampleTypeId { get; set; }
        public string DisplaySampleName { get; set; }
        public string Remarks { get; set; }
        public string CurrentStatus { get; set; }
        public string PGSGM { get; set; }
        public int SrNo { get; set; }
        public long EnquiryId { get; set; }//EnquiryMaster tbl
        public long EnquirySampleID { get; set; }
        
        public long EnquiryDetailId { get; set; }
        public string SampleName { get; set; }
        public decimal TotalCharges { get; set; }
        public List<EnquiryParameterModel> ParameterList { get; set; }
        public string ProductGroupName { get; set; }
        public string SubGroupName { get; set; }
        public string SampleTypeProductName { get; set; }
        public string MatrixName { get; set; }
        public string Parameters { get; set; }
        public decimal Cost { get; set; }
        public List<SampleLocationModel> locationList { get; set; }
        public SampleLocationModel location { get; set; }


    }
    public class SampleLocationModel
    {
        public int count { get; set; }
        public int SampleLocationId { get; set; }
        public string Location { get; set; }
        public int SrNo { get; set; }
        public long EnquiryId { get; set; }//EnquiryMaster tbl
        public long EnquirySampleID { get; set; }
    }
}