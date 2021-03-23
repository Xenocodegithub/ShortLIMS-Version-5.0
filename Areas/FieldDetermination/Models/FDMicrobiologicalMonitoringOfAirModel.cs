using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Areas.FieldDetermination.Models
{
    public class FDMicrobiologicalMonitoringOfAirModel
    {
        public string CurrentStatus { get; set; }// StatusMaster tbls
        public int MicrobiologicalID { get; set; }
        public Nullable<long> EnquiryId { get; set; }
        public Nullable<long> SampleCollectionId { get; set; }
        public long EnquirySampleID { get; set; }//EnquirySampleDetail tbl/SampleCollection

        public Nullable<byte> StatusId { get; set; }
        public string FlowRate { get; set; }
        public string AreaSwabbed { get; set; }
        public string MediaUsed { get; set; }
        public string IndustryType { get; set; }
        public Nullable<int> EquipmentId { get; set; }
        public string RelativeHumidity { get; set; }
        public string AnyOtherObservation { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public int SrNo { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public List<FDMicrobiologicalMonitoringOfAirModel> GridModel { get; set; }
    }
}