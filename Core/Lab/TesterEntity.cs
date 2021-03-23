using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Lab
{
    public class TesterEntity
    {
        public long SampleCollectionId { get; set; }
        public int? UserMasterId { get; set; }
        public string SampleNo { get; set; }
        public string SampleNameOriginal { get; set; }
        public string SampleTypeProductName { get; set; }
        public string ProductGroupName { get; set; }
        public string SubGroupName { get; set; }
        public string MatrixName { get; set; }
        public string StatusName { get; set; }
        public int NotApproveCount { get; set; }
        public int AnalysisActive { get; set; }
        public string FinalResult { get; set; }
        public string QualityControl { get; set; }
        public string TotalDuration { get; set; }
        public string Analyst { get; set; }
        public DateTime? AnalysisStartAt { get; set; }
        public int ParameterGroupId { get; set; }
        public int AnalysisProcessScheduleDetailId { get; set; }
        public long ParameterMasterId { get; set; }
        public int AnalystUserMasterID { get; set; }
        public int SrNo { get; set; }
        public bool? IsNAblSave { get; set; }
    }
    public class SampleParameterFileInfo
    {
        public long SampleCollectionId { get; set; }
        public int ParameterGroupId { get; set; }
        public long ParameterMasterId { get; set; }
        public int UserMasterID { get; set; }
        public long FileID { get; set; }
        public string FileName { get; set; }
        public bool IsActive { get; set; }
    }
    public class ParameterInfos
    {
        public long ParameterGroupId { get; set; }
        public long ParameterMasterId { get; set; }
        public string ParameterName { get; set; }
        public long UnitId { get; set; }
        public string Unit { get; set; }
        public string ReviewerComment { get; set; }
        public long TestMethodId { get; set; }
        public string TestMethod { get; set; }
        public int DisciplineId { get; set; }
        public int? AnalystUserMasterID { get; set; }
        public bool IsActive { get; set; }
        public int? CurrentStatus { get; set; }
    }
    public class SampleParameterAnalysis
    {
        public long SampleCollectionId { get; set; }
        public long FDStackEmissionId { get; set; }
        public string SampleNo { get; set; }
        public string SampleNameOriginal { get; set; }
        public long EnquiryId { get; set; }
        public string Url { get; set; }
        public string SampleTypeProductName { get; set; }
        public string ProductGroupName { get; set; }
        public string SubGroupName { get; set; }
        public string MatrixName { get; set; }
        public int? TestMethodId { get; set; }
        public int FieldId { get; set; }
        public long EnquirySampleID { get; set; }
        public string TestMethodName { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public long ParameterGroupId { get; set; }
        public long ParameterMasterId { get; set; }
        public string ParameterName { get; set; }
        public string Analyst { get; set; }
        public string Reviewer { get; set; }
        public string Approver { get; set; }
        public DateTime? StartOfAnalysis { get; set; }
        public DateTime? EndOfAnalysis { get; set; }
        public DateTime? DateReceiptOfSampling { get; set; }
        public string QRCodePath { get; set; }
        public string RegulatoryMin { get; set; }
        public string RegulatoryMax { get; set; }
        public string PermissibleMin { get; set; }
        public string PermissibleMax { get; set; }
        public string LOD { get; set; }
        public string ReviewerComment { get; set; }
        public string ApproverComment { get; set; }

    }
    public class SampleParameterInfo
    {
        public TesterEntity SampleDetails { get; set; }
        public List<ParameterInfos> ParameterInfos { get; set; }
    }
    public class SolutionPreparationData
    {
        public int SolutionPreparationDataId { get; set; }

        public int SampleCollectionId { get; set; }

        public int ParameterGroupId { get; set; }
        public int ParameterMasterId { get; set; }

        public DateTime DateOfPreparation { get; set; }
        public string NameOfSolution { get; set; }
        public string NameofChemicalUsed { get; set; }
        public string QualityofChemicalUsed { get; set; }
        public decimal QtyOfSolutionPrepared { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
    }
    public class AnalysisProcessScheduleDetail
    {
        public string SampleNameOriginal { get; set; }
        public long AnalysisProcessScheduleDetailId { get; set; }
        public long SampleCollectionId { get; set; }
        public int ParameterGroupId { get; set; }
        public long ParameterMasterId { get; set; }
        public int AnalystUserMasterID { get; set; }
        public DateTime? AnalysisStartAt { get; set; }
        public DateTime? AnalysisEndAt { get; set; }
        public DateTime? PauseAnalysis { get; set; }
        public DateTime? ResumeAnalysis { get; set; }
        public DateTime? StartOfAnalysis { get; set; }
        public TimeSpan? AnalysisDuration { get; set; }
        public string FinalResult { get; set; }
        public string TotalDuration { get; set; }
        public string QualityControl { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }

    }
    public class TestProcessScheduleDetail
    {
        public long AnalysisProcessScheduleDetailId { get; set; }
        public long SampleCollectionId { get; set; }

        public int ParameterGroupId { get; set; }
        public long ParameterMasterId { get; set; }
        public int AnalystUserMasterID { get; set; }
        public int SrNo { get; set; }
        public string FinalResult { get; set; }
        public string QualityControl { get; set; }
        public DateTime? StartOfAnalysis { get; set; }
        public DateTime? EndOfAnalysis { get; set; }
        public string TotalDuration { get; set; }
        public int EnteredBy { get; set; }
    }
}