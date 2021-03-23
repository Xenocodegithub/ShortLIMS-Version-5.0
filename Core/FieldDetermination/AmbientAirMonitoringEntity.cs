using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.FieldDetermination
{
    public class AmbientAirMonitoringEntity
    {  
        public string CurrentStatus { get; set; }// StatusMaster tbls
        public long ParameterMasterId { get; set; }//ParameterMapping
        public string ParameterName { get; set; }
        public int FieldId { get; set; }
        public Nullable<long> EnquiryId { get; set; }
        public int GasesSampledId { get; set; }
        public long SampleCollectionId { get; set; }
        public Nullable<int> ParameterMappingId { get; set; }
        public Nullable<byte> StatusId { get; set; }
        public string InstrumentId { get; set; }
        public string SamplingLocation { get; set; }
        public Nullable<int> SamplingDuration { get; set; }
        public string SamplingProcedure { get; set; }
        public Nullable<int> WindVelocity { get; set; }
        public string WindDirection_ { get; set; }
        public string RelativeHumidity { get; set; }
        public string Temperature { get; set; }
        public Nullable<int> AverageWindVelocity { get; set; }
        public string RelativeHumidity24Hr { get; set; }
        public string Temperature24Hr { get; set; }
        public Nullable<int> FilterPaperNo2 { get; set; }
        public string SampleReceivedBy { get; set; }

        public int MatterSizeId { get; set; }
        public Nullable<System.TimeSpan> MatterSizeStartTime { get; set; }
        public Nullable<System.TimeSpan> MatterSizeEndTime { get; set; }
        public int ShiftNo { get; set; }
        public int ShiftNoGases { get; set; }
        public string MatterSize { get; set; }
        public Nullable<int> FilterPaperNo { get; set; }
        public Nullable<int> CycloneCupNo { get; set; }
        public Nullable<int> FlowRateFinal { get; set; }
        public Nullable<int> FlowRateInitial { get; set; }
        public Nullable<int> FlowRateDiff { get; set; }
        public Nullable<int> TimeTotalizerFinal { get; set; }
        public Nullable<int> TimeTotalizerInitial { get; set; }
        public Nullable<int> TimeTotalizerDiff { get; set; }
        public Nullable<int> SamplingDurationFinal { get; set; }
        public Nullable<int> SamplingDurationInitial { get; set; }
        public Nullable<int> SamplingDurationDiff { get; set; }
        public Nullable<int> StartTime { get; set; }
        public Nullable<int> EndTime { get; set; }
        public Nullable<int> MatterSamplingDuration { get; set; }
        public Nullable<int> AvgFlowRate { get; set; }
        public Nullable<int> TotalVolAirPassed_L { get; set; }
        public Nullable<int> TotalVolumeAirPassed_m3 { get; set; }
        public string GasesSampled { get; set; }
        public Nullable<int> VolImpingingSolution { get; set; }
        public Nullable<int> BottleNo { get; set; }
        public Nullable<int> RotaMeterFlow { get; set; }
        public Nullable<int> Duration { get; set; }
        public Nullable<int> Vs { get; set; }
        public string PreservationDone { get; set; }
        public bool IsActive { get; set; }
        public bool Testing { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    }
    public class FDAirInfo
    {
        public AmbientAirMonitoringEntity AirDetails { get; set; }
        public List<AmbientAirMonitoringEntity> AirInfos { get; set; }
        public List<AmbientAirMonitoringEntity> AirInfos24Hr { get; set; }


    }
}
