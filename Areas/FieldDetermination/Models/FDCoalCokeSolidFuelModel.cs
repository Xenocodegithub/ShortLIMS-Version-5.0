using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Areas.FieldDetermination.Models
{
    public class FDCoalCokeSolidFuelModel
    {
        public Nullable<bool> IsNABLAccredited { get; set; }
        public Nullable<bool> InField { get; set; }
        public string ParameterName { get; set; }
        public int ParameterMappingId { get; set; }
        public int ParameterMasterId { get; set; }
        public string TestMethodName { get; set; }
        public int TestMethodId { get; set; }
        public int CoalCokeId { get; set; }
        public Nullable<long> EnquiryId { get; set; }
        public Nullable<long> SampleCollectionId { get; set; }
        public long EnquirySampleID { get; set; }//EnquirySampleDetail tbl/SampleCollection

        public string CurrentStatus { get; set; }// StatusMaster tbls
        public int SrNo { get; set; }
        public string Parameters { get; set; }
        [Display(Name = "Test Results")]
        public string TestResults { get; set; }
        public Nullable<byte> StatusId { get; set; }
        [Display(Name = "Any Other Observation")]
        public string AnyOtherObservation { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public List<FDCoalCokeSolidFuelModel> GridModel { get; set; }
    }
}