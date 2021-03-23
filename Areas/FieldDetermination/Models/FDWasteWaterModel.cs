using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.FieldDetermination.Models
{
    public class FDWasteWaterModel
    {
        public Nullable<bool> IsNABLAccredited { get; set; }
        public Nullable<bool> InField { get; set; }
        public int ParameterMasterId { get; set; }

        [Display(Name ="TestMethod")]
        public string TestMethodName { get; set; }
        public int TestMethodId { get; set; }
        public string CurrentStatus { get; set; }// StatusMaster tbls
        public int WasteWaterID { get; set; }
        public Nullable<long> EnquiryId { get; set; }
        public Nullable<long> SampleCollectionId { get; set; }
        public long EnquirySampleID { get; set; }//EnquirySampleDetail tbl/SampleCollection

        [Display(Name = "Water Use")]
        public string WaterUse { get; set; }

        public int SrNo { get; set; }

        [Display(Name = "Parameter")]
        public string ParameterName { get; set; }

        [Display(Name = "Test Results")]
        public string TestResults { get; set; }
        public Nullable<int> ParameterMappingId { get; set; }

        [Display(Name = "Any Other Observation")]
        public string AnyOtherObservation { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<byte> StatusId { get; set; }

        //public List<FDWasteWaterModel> WaterList { get; set; }

        public List<FDWasteWaterModel> GridModel { get; set; }

    }

    //public class FDWaterListModel
    //{
    //    internal List<FDWasteWaterModel> GridModel;
    //    public FDWaterListModel()
    //    {
    //        FDWater = new FDWasteWaterModel();
    //        WaterList = new List<FDWasteWaterModel>();
    //    }
    //    public List<FDWasteWaterModel> WaterList { get; set; }
    //    public FDWasteWaterModel FDWater { get; set; }
    //}

}