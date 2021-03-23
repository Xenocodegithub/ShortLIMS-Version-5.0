using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Areas.FieldDetermination.Models
{
    public class FDBuildingMaterialModel
    {
        public Nullable<bool> IsNABLAccredited { get; set; }
        public Nullable<bool> InField { get; set; }
        public int ParameterMasterId { get; set; }
        public string ParameterName { get; set; }
        public string TestMethodName { get; set; }
        public int TestMethodId { get; set; }
        public int FieldBuildingMaterialId { get; set; }
        public Nullable<int> EnquiryId { get; set; }
        public Nullable<long> SampleCollectionId { get; set; }
        public long EnquirySampleID { get; set; }//EnquirySampleDetail tbl/SampleCollection

        public Nullable<byte> StatusId { get; set; }
        public int SrNo { get; set; }
        public string Parameters { get; set; }
        public string CurrentStatus { get; set; }// StatusMaster tbls
        public string TestResults { get; set; }

        public string AnyOtherObservation { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public List<FDBuildingMaterialModel> GridModel { get; set; }
    }
}