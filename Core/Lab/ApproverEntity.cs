using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Lab
{
    public class ApproverEntity
    {
        public long AnalysisProcessScheduleDetailId { get; set; }
        public bool? IsNAblSave { get; set; }
        public long ParameterGroupId { get; set; }
        public long SampleCollectionId { get; set; }
        public string SampleNo { get; set; }
        public string SampleName { get; set; }
        public string SampleTypeProductName { get; set; }
        public string ProductGroupName { get; set; }
        public string SubGroupName { get; set; }
        public string MatrixName { get; set; }
        public string StatusName { get; set; }
        public string FinalResult { get; set; }
        public int NotApproveCount { get; set; }
        public int ApprovalActive { get; set; }
    }
    public class SampleParameterInfoapprover
    {
        public ApproverEntity SampleDetails { get; set; }
        public List<ParameterInfo> ParameterInfosApprove { get; set; }
    }
    public class SampleParameterApprove
    {
        public Nullable<long> EnquirySampleID { get; set; }
        public int FieldId { get; set; }
        public Nullable<System.DateTime> ProbableDateOfReport { get; set; }
        public long SampleCollectionId { get; set; }
        public string SampleNo { get; set; }
        public string SampleName { get; set; }
        public string SampleNameOriginal { get; set; }
        public string SampleTypeProductName { get; set; }
        public string ProductGroupName { get; set; }
        public string SubGroupName { get; set; }
        public string MatrixName { get; set; }
        public int? TestMethodId { get; set; }
        public string TestMethodName { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public long ParameterGroupId { get; set; }
        public int AnalysisProcessScheduleDetailId { get; set; }
        public int WorkOrderId { get; set; }
        public long ParameterMasterId { get; set; }
        public string ParameterName { get; set; }
        public string Analyst { get; set; }
        public string Reviewer { get; set; }
        public string Approver { get; set; }
        public DateTime? DateReceiptOfSampling { get; set; }
        public string QRCodePath { get; set; }
        public string RegulatoryMin { get; set; }
        public string RegulatoryMax { get; set; }
        public string PermissibleMin { get; set; }
        public string PermissibleMax { get; set; }
        public string LOD { get; set; }
        public int? AnalystApprovedStatus { get; set; }
        public string AnalystComments { get; set; }
        public int? ReviewerApprovedStatus { get; set; }
        public string ReviewerComments { get; set; }
        public int? ApproverApprovedStatus { get; set; }
        public string ApproverComments { get; set; }

    }
    public class ParameterFormulaList
    {
        public int ParameterGroupId { get; set; }
        public int ParameterId { get; set; }
        public int UnitId { get; set; }
        public int TestMethodId { get; set; }
        public int DisciplineId { get; set; }
        public int SampleCollectionId { get; set; }
        public int EnteredBy { get; set; }
        //public Dictionary<string, List<FormulaList>> FormulaLists { get; set; }
        public List<FormulaList> FormulaList { get; set; }

    }
    public class FormulaList
    {
        public long Id { get; set; }
        public int SrNo { get; set; }
        public string Notation { get; set; }
        public string DisplayName { get; set; }
        public string Formula { get; set; }
        public bool? IsFDV { get; set; }
        public string NotationValue { get; set; }
        public int Unit { get; set; }
        public int DataType { get; set; }
        public int Precision { get; set; }
        public int ParameterGroupId { get; set; }
        public int ParameterMasterId { get; set; }


    }
    public class TestProcessScheduleDetailApprove
    {
        public long AnalysisProcessScheduleDetailId { get; set; }
        public long SampleCollectionId { get; set; }
        public int ParameterGroupId { get; set; }
        public long ParameterMasterId { get; set; }
        public int AnalystUserMasterID { get; set; }
        public DateTime? AnalysisStartAt { get; set; }
        public DateTime? AnalysisEndAt { get; set; }
        public TimeSpan? AnalysisDuration { get; set; }
        public string FinalResult { get; set; }
        public string QualityControl { get; set; }
        public string TotalDuration { get; set; }
        public int EnteredBy { get; set; }
    }
}