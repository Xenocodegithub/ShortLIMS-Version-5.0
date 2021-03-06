//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LIMS_DEMO.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class AnalysisProcessScheduleDetail
    {
        public long AnalysisProcessScheduleDetailId { get; set; }
        public long SampleCollectionId { get; set; }
        public int ParameterGroupId { get; set; }
        public long ParameterMasterId { get; set; }
        public int AnalystUserMasterID { get; set; }
        public Nullable<System.DateTime> AnalysisStartAt { get; set; }
        public Nullable<System.DateTime> AnalysisEndAt { get; set; }
        public Nullable<System.DateTime> PauseAnalysis { get; set; }
        public Nullable<System.DateTime> ResumeAnalysis { get; set; }
        public Nullable<System.TimeSpan> AnalysisDuration { get; set; }
        public string FinalResult { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string TotalDuration { get; set; }
        public string QualityControl { get; set; }
        public Nullable<bool> IsNablSave { get; set; }
        public Nullable<int> WorkOrderId { get; set; }
    }
}
