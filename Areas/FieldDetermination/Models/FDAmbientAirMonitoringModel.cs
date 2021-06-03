using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.FieldDetermination.Models
{
    public class FDAmbientAirMonitoringModel
    {
        //FieldAmbientairMonitoring/////////////////////////////////
        public Nullable<byte> StatusId { get; set; }
        public long ParameterMasterId { get; set; }//ParameterMapping
        public int FieldId { get; set; }
        public Nullable<int> EnquiryId { get; set; }
        public Nullable<int> ParameterId { get; set; }
        public Nullable<int> ParameterMappingId { get; set; }
        public string ParameterName { get; set; }
        public Nullable<long> SampleCollectionId { get; set; }
        public long EnquirySampleID { get; set; }//EnquirySampleDetail tbl/SampleCollection
        public string CurrentStatus { get; set; }// StatusMaster tbls
        public string InstrumentId { get; set; }
        //public int? InstrumentId { get; set; }
        public Nullable<int> SamplingDuration { get; set; }
        public Nullable<int> WindVelocity { get; set; }
        public string WindDirection_ { get; set; }
        public string RelativeHumidity { get; set; }
        public string Temperature { get; set; }
        public Nullable<int> AverageWindVelocity { get; set; }
        [Display(Name = "Relative Humidity")]
        public string RelativeHumidity24Hr { get; set; }
        [Display(Name = "Temperature")]
        public string Temperature24Hr { get; set; }
        [Display(Name = "FilterPaperNo")]
        public Nullable<int> FilterPaperNo2 { get; set; }
        public string SampleReceivedBy { get; set; }

        //FieldAmbientAirMatterSize///////////////////////////////////////
        public int MatterSizeId { get; set; }
        public string MatterSize { get; set; }
        public string MatterSize1 { get; set; }
        public string MatterSize2 { get; set; }
        public Nullable<System.TimeSpan> MatterSizeStartTime { get; set; }
        public Nullable<System.TimeSpan> MatterSizeEndTime { get; set; }
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
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Nullable<int> MatterSamplingDuration { get; set; }
        public Nullable<int> AvgFlowRate { get; set; }
        public Nullable<int> TotalVolAirPassed_L { get; set; }
        public Nullable<int> TotalVolumeAirPassed_m3 { get; set; }

        public Nullable<int> FilterPaperNos2 { get; set; }
        public Nullable<int> CycloneCupNos2 { get; set; }
        public Nullable<System.TimeSpan> MatterSizeStartTime2 { get; set; }
        public Nullable<System.TimeSpan> MatterSizeEndTime2 { get; set; }
        public Nullable<int> FlowRateFinals2 { get; set; }
        public Nullable<int> FlowRateInitials2 { get; set; }
        public Nullable<int> FlowRateDiffs2 { get; set; }
        public Nullable<int> TimeTotalizerFinals2 { get; set; }
        public Nullable<int> TimeTotalizerInitials2 { get; set; }
        public Nullable<int> TimeTotalizerDiffs2 { get; set; }
        public Nullable<int> SamplingDurationFinals2 { get; set; }
        public Nullable<int> SamplingDurationInitials2 { get; set; }
        public Nullable<int> SamplingDurationDiffs2 { get; set; }

        public Nullable<int> FilterPaperNos3 { get; set; }
        public Nullable<int> CycloneCupNos3 { get; set; }
        public Nullable<System.TimeSpan> MatterSizeStartTime3 { get; set; }
        public Nullable<System.TimeSpan> MatterSizeEndTime3 { get; set; }
        public Nullable<int> FlowRateFinals3 { get; set; }
        public Nullable<int> FlowRateInitials3 { get; set; }
        public Nullable<int> FlowRateDiffs3 { get; set; }
        public Nullable<int> TimeTotalizerFinals3 { get; set; }
        public Nullable<int> TimeTotalizerInitials3 { get; set; }
        public Nullable<int> TimeTotalizerDiffs3 { get; set; }
        public Nullable<int> SamplingDurationFinals3 { get; set; }
        public Nullable<int> SamplingDurationInitials3 { get; set; }
        public Nullable<int> SamplingDurationDiffs3 { get; set; }
        public string ShiftNoAir { get; set; }
        public string ShiftNoAir24Hr { get; set; }
        //FieldAmbientAirGasesSampling//////////////////////////////////////
        public int GasesSampledId { get; set; }
        public int SrNo { get; set; }
        public int ShiftNo { get; set; }
        public int ShiftNos2 { get; set; }
        public int ShiftNos3 { get; set; }
        public string GasesSampled { get; set; }
        public Nullable<int> VolImpingingSolution { get; set; }
        public Nullable<int> BottleNo { get; set; }
        public Nullable<int> RotaMeterFlow { get; set; }
        public Nullable<int> Duration { get; set; }
        public Nullable<int> Vs { get; set; }
        public string PreservationDone { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public List<FDAmbientAirMonitoringModel> GridModel { get; set; }
        public List<FDAmbientAirMonitoringModel> Grid24HrModel { get; set; }
    }
}