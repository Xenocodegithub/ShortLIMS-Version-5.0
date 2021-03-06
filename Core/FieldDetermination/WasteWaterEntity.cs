using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.FieldDetermination
{
    public class WasteWaterEntity
    {
        public Nullable<bool> IsNABLAccredited { get; set; }
        public Nullable<bool> InField { get; set; }
        public string TestMethodName { get; set; }
        public string CurrentStatus { get; set; }// StatusMaster tbls
        public int WasteWaterID { get; set; }
        public Nullable<long> EnquiryId { get; set; }
        public Nullable<byte> StatusId { get; set; }
        public Nullable<int> ParameterMappingId { get; set; }
        public long SampleCollectionId { get; set; }
        public string Parameters { get; set; }
        public string TestResults { get; set; }
        public string AnyOtherObservation { get; set; }
        public string WaterUse { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    }
    public class FDWaterInfo
    {
        public WasteWaterEntity WaterDetails { get; set; }
        public List<WasteWaterEntity> WaterInfos { get; set; }
    }
}
