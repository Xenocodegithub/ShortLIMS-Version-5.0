using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.FieldDetermination.Models
{
    public class FDWorkplaceEnvAndFugutiveEmissionModel
    {
        public Nullable<int> Vs { get; set; }
        public string PreservationDone { get; set; }
        public Nullable<int> Sampling_Duration { get; set; }
        public Nullable<int> AvgFlowRate { get; set; }
        public Nullable<int> TotalVolAirPassed_L { get; set; }
        public Nullable<int> TotalVolumeAirPassed_m3 { get; set; }
        public Nullable<int> StartTime { get; set; }
        public Nullable<int> EndTime { get; set; }
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
        public int InstrumentId { get; set; }
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