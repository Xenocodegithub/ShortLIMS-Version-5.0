﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.FieldDetermination
{
    public class WorkplaceAndFugitiveEmissionEntity
    {
        public Nullable<int> Sampling_Duration { get; set; }
        public Nullable<int> AvgFlowRate { get; set; }
        public Nullable<int> FilterPaperNo2 { get; set; }
        public Nullable<int> StartTime { get; set; }
        public Nullable<int> EndTime { get; set; }
        public Nullable<int> TotalVolAirPassed_L { get; set; }
        public Nullable<int> TotalVolumeAirPassed_m3 { get; set; }
        public Nullable<int> Vs { get; set; }
        public string PreservationDone { get; set; }
        public Nullable<bool> IsNABLAccredited { get; set; }
        public Nullable<bool> InField { get; set; }
        public string Parameters { get; set; }
        public string TestMethodName { get; set; }
        public string CurrentStatus { get; set; }// StatusMaster tbls
        public int WorkplaceID { get; set; }
        public int WorkplaceGasID { get; set; }
        public long SampleCollectionId { get; set; }
        public Nullable<long> EnquiryId { get; set; }
        public Nullable<byte> StatusId { get; set; }
        public Nullable<System.DateTime> SurveyDate { get; set; }
        public Nullable<int> InstrumentId { get; set; }
        public Nullable<int> DurationOfSampling { get; set; }
        public string FilterPaperAnalyzed { get; set; }
        public Nullable<int> FilterPaperNo { get; set; }
        public Nullable<int> FlowRate_Final { get; set; }
        public Nullable<int> FlowRate_Initial { get; set; }
        public Nullable<int> FlowRate_Avg { get; set; }
       
        public string RelativeHumidity { get; set; }
        public string Temperature { get; set; }
        public string GasesSampled { get; set; }
        public string VolImpingingSol { get; set; }
        public string BottleNo { get; set; }
        public Nullable<int> RotameterFlow { get; set; }
        public Nullable<int> Duration { get; set; }
        public string AnyObservation { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    }
    public class FDWorkplaceInfo
    {
        public WorkplaceAndFugitiveEmissionEntity WorkplaceDetails { get; set; }
        public List<WorkplaceAndFugitiveEmissionEntity> WorkplaceInfos { get; set; }
    }
}
