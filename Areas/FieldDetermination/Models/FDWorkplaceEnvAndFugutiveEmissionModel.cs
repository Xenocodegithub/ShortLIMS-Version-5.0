using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.FieldDetermination.Models
{
    public class FDWorkplaceEnvAndFugutiveEmissionModel
    {
        public int MatterSizeId { get; set; }
        public string MatterSize { get; set; }
        public string MatterSize1 { get; set; }
        public string MatterSize2 { get; set; }
        public string MatterSizeStartTime { get; set; }
        public string MatterSizeEndTime { get; set; }

        public string CycloneCupNo { get; set; }
        public Nullable<int> FlowRateFinal { get; set; }
        public Nullable<int> FlowRateInitial { get; set; }
        public Nullable<int> FlowRateDiff { get; set; }
        public string FlowRateFinalM { get; set; }
        public string FlowRateInitialM { get; set; }
        public string FlowRateDiffM { get; set; }
        public string TimeTotalizerFinal { get; set; }
        public string TimeTotalizerInitial { get; set; }
        public string TimeTotalizerDiff { get; set; }
        public string SamplingDurationFinal { get; set; }
        public string SamplingDurationInitial { get; set; }
        public string SamplingDurationDiff { get; set; }
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
        public int ShiftNo { get; set; }
        public int ShiftNos3 { get; set; }
        public int ShiftNos2 { get; set; }
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
        public int SampleTypeProductId { get; set; }
        public Nullable<int> Vs { get; set; }
        public string PreservationDone { get; set; }
        public Nullable<int> Sampling_Duration { get; set; }

        public Nullable<int> FilterPaperNo2 { get; set; }
        public Nullable<bool> IsNABLAccredited { get; set; }
        public Nullable<bool> InField { get; set; }
        public string SurveyDateDisplay { get; set; }
        public string Parameters { get; set; }

        [Display(Name = "Parameter")]
        public string ParameterName { get; set; }
        public int ParameterMasterId { get; set; }

        [Display(Name = "TestMethod")]
        public string TestMethodName { get; set; }
        public int TestMethodId { get; set; }
        public string CurrentStatus { get; set; }// StatusMaster tbls
        [Display(Name = "Enquiry Id ")]
        public Nullable<long> EnquiryId { get; set; }
        public Nullable<long> SampleCollectionId { get; set; }
        public long EnquirySampleID { get; set; }//EnquirySampleDetail tbl/SampleCollection
        public Nullable<byte> StatusId { get; set; }
        public int WorkplaceGasID { get; set; }
        public int WorkplaceID { get; set; }
        public string GasesSampled { get; set; }
        [Display(Name = "Vol Impinging Sol")]
        public string VolImpingingSol { get; set; }
        public int SrNo { get; set; }
        [Display(Name = "Bottle No")]
        public string BottleNo { get; set; }

        [Display(Name = "Rotameter Flow")]
        public Nullable<int> RotameterFlow { get; set; }
        public Nullable<int> Duration { get; set; }
        [Display(Name = "Instrument Id")]
        public string InstrumentId { get; set; }
        [Display(Name = "Bottle No")]
        public int FieldId { get; set; }
        [Display(Name = "Survey Date")]
        public Nullable<System.DateTime> SurveyDate { get; set; }

        [Display(Name = "Duration Of Sampling")]
        public int DurationOfSampling { get; set; }

        [Display(Name = "Filter Paper Analyzed")]
        public string FilterPaperAnalyzed { get; set; }
        [Display(Name = "Filter Paper No")]
        public int FilterPaperNo { get; set; }
        [Display(Name = "FlowRate Final")]
        public int FlowRate_Final { get; set; }
        [Display(Name = "FlowRate Initial")]
        public int FlowRate_Initial { get; set; }
        [Display(Name = "FlowRate Avg ")]
        public int FlowRate_Avg { get; set; }
        [Display(Name = "Sampling Duration Initial")]
        public int Sampling_DurationInitial { get; set; }
        [Display(Name = "Sampling Duration Final")]
        public int Sampling_DurationFinal { get; set; }
        [Display(Name = "Sampling Duration Avg")]
        public int Sampling_DurationAvg { get; set; }
        //[Display(Name = "Enquiry Id ")]
        // public int EnquiryId { get; set; }

        [Display(Name = "Relative Humidity")]
        public string RelativeHumidity { get; set; }
        public string Temperature { get; set; }
        [Display(Name = "Any Observation")]
        public string AnyObservation { get; set; }
        public bool IsActive { get; set; }
        public List<FDWorkplaceEnvAndFugutiveEmissionModel> GridModel { get; set; }
    }
}