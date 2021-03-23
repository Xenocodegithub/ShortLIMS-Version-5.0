using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.FieldDetermination
{
    public class NoiseLevelMonitoringEntity
    {
        public Nullable<long> LeqAvgDayTime { get; set; }
        public Nullable<long> LeqAvgNightTime { get; set; }
        public Nullable<long> L10AvgDayTime { get; set; }
        public Nullable<long> L10AvgNightTime { get; set; }
        public Nullable<long> L50AvgDayTime { get; set; }
        public Nullable<long> L50AvgNightTime { get; set; }
        public Nullable<long> L90AvgDayTime { get; set; }
        public Nullable<long> L90AvgNightTime { get; set; }
        public Nullable<long> LminAvgDayTime { get; set; }
        public Nullable<long> LminAvgNightTime { get; set; }
        public Nullable<long> LmaxAvgDayTime { get; set; }
        public Nullable<long> LmaxAvgNightTime { get; set; }
        public Nullable<long> DistancefrmSource { get; set; }
        public string ProminentNoiseSource { get; set; }
        public Nullable<long> BackgroundNoiselevel { get; set; }
        public string SampleTypeName { get; set; }
        public Nullable<bool> IsNABLAccredited { get; set; }
        public Nullable<bool> InField { get; set; }
        public string UnitName { get; set; }
        public string TestMethodName { get; set; }
        public string ParameterName { get; set; }
        public string CurrentStatus { get; set; }// StatusMaster tbls
        public int FieldNoiseId { get; set; }
        public int ParameterId { get; set; }
        public Nullable<int> EnquiryId { get; set; }
        public Nullable<int> SampleCollectionId { get; set; }
        public Nullable<byte> StatusId { get; set; }
        public Nullable<System.DateTime> MonitoringDate { get; set; }
        public string MonitoringTime { get; set; }
        public string Location { get; set; }
        public string LocationZone { get; set; }
        public string InstrumentMake { get; set; }
        public string InstrumentModel { get; set; }
        public string InstrumentID { get; set; }
        public Nullable<long> LocationHeight { get; set; }
        public Nullable<long> MicrophoneHeight { get; set; }
        public string Obstacles { get; set; }
        public string Isolation { get; set; }
        public string Weather { get; set; }
        public string WindType { get; set; }
        public string TimeDuration { get; set; }
        public string FileName { get; set; }
        public string DayNight { get; set; }
        public int NoiseSourceParameterId { get; set; }
        public Nullable<long> TimeSource { get; set; }
        public string ShiftName { get; set; }
        public Nullable<long> Inside { get; set; }
        public Nullable<long> Outside { get; set; }
        public Nullable<long> InsertionLoss { get; set; }
        public string LocationGrid { get; set; }
        public Nullable<long> LeqFastResponse { get; set; }
        public Nullable<long> LeqSlowResponse { get; set; }
        public Nullable<long> L10FastResponse { get; set; }
        public Nullable<long> L10SlowResponse { get; set; }
        public Nullable<long> L50FastResponse { get; set; }
        public Nullable<long> L50SlowResponse { get; set; }
        public Nullable<long> L90FastResponse { get; set; }
        public Nullable<long> L90SlowResponse { get; set; }
        public Nullable<long> LminFastResponse { get; set; }
        public Nullable<long> LminSlowResponse { get; set; }
        public Nullable<long> LmaxFastResponse { get; set; }
        public Nullable<long> LmaxSlowResponse { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public class FDNoiseInfo
        {
            public NoiseLevelMonitoringEntity NoiseDetails { get; set; }
            public List<NoiseLevelMonitoringEntity> NoiseInfos { get; set; }
            public List<NoiseLevelMonitoringEntity> SourceNoiseInfos { get; set; }
        }
    }
}
