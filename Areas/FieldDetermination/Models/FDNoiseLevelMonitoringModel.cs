using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Areas.FieldDetermination.Models
{
    public class FDNoiseLevelMonitoringModel
    {
        public string ShiftNoAmbientWorkplaceNoise { get; set; }
        public string ShiftNoSourceNoise { get; set; }
        public bool flag { get; set; }
        public string SampleTypeProductName { get; set; }
        public Nullable<bool> IsNABLAccredited { get; set; }
        public Nullable<bool> InField { get; set; }
        public int ParameterMasterId { get; set; }
        public string ParameterName { get; set; }
        public int TestMethodId { get; set; }
        public string TestMethodName { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string CurrentStatus { get; set; }// StatusMaster tbls
        public int FieldNoiseId { get; set; }
        public int ParameterId { get; set; }
        public Nullable<int> EnquiryId { get; set; }
        public Nullable<byte> StatusId { get; set; }
        public Nullable<int> SampleCollectionId { get; set; }
        public long EnquirySampleID { get; set; }//EnquirySampleDetail tbl/SampleCollection
        public string Location { get; set; }
        public string LocationZone { get; set; }
        public Nullable<long> LocationHeight { get; set; }
        public Nullable<long> MicrophoneHeight { get; set; }
        public string InstrumentMake { get; set; }
        public string InstrumentModel { get; set; }
        public string InstrumentID { get; set; }
       
        public string Obstacles { get; set; }
        public string Isolation { get; set; }
        public string Weather { get; set; }
        public string WindType { get; set; }
        public string SamplingProcedure { get; set; }
        public Nullable<System.DateTime> MonitoringDate { get; set; }
        public string MonitoringTime { get; set; }
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
        /////////////////////////////////////////////////////For Grid///////////////////////////////////
        ///
        public int SrNo { get; set; }
        public string DayNight { get; set; }
        public string LocationGrid { get; set; }
        public string TimeDuration { get; set; }
        public string FileName { get; set; }
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
        public int NoiseSourceParameterId { get; set; }
        public Nullable<long> TimeSource { get; set; }
        public string ShiftName { get; set; }
        public Nullable<long> Inside { get; set; }
        public Nullable<long> Outside { get; set; }
        public Nullable<long> InsertionLoss { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public List<FDNoiseLevelMonitoringModel> GridModel { get; set; }
        public List<FDNoiseLevelMonitoringModel> GridSourceModel { get; set; }
    }
}