using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.FieldDetermination
{
    public class MicrobiologicalMonitoringOfAirEntity
    {
        public string CurrentStatus { get; set; }// StatusMaster tbls
        public int MicrobiologicalID { get; set; }
        public Nullable<long> EnquiryId { get; set; }
        public long SampleCollectionId { get; set; }
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
        public System.DateTime EnteredDate { get; set; }
    }
    public class FDMicrobiologicalInfo
    {
        public MicrobiologicalMonitoringOfAirEntity MicrobiologicalDetails { get; set; }
        public List<MicrobiologicalMonitoringOfAirEntity> MicrobiologicalInfos { get; set; }
    }
}
