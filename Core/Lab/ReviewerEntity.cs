using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Lab
{
    public class ReviewerEntity
    {
        public long SampleCollectionId { get; set; }
        public string SampleNo { get; set; }
        public string SampleName { get; set; }
        public string SampleTypeProductName { get; set; }
        public string SampleNameOriginal { get; set; }
        public string ProductGroupName { get; set; }
        public string SubGroupName { get; set; }
        public string MatrixName { get; set; }
        public string StatusName { get; set; }
        public int NotApproveCount { get; set; }
        public int ReviewActive { get; set; }
    }
    public class SampleParameterInfoReview
    {
        public ReviewerEntity SampleDetails { get; set; }
        public List<ParameterInfoReview> ParameterInfosReview { get; set; }
    }
    public class ParameterInfoReview
    {
        public long ParameterGroupId { get; set; }
        public long ParameterMasterId { get; set; }
        public string ParameterName { get; set; }
        public long UnitId { get; set; }
        public string Unit { get; set; }
        public long TestMethodId { get; set; }
        public string TestMethod { get; set; }
        public string Remark { get; set; }
        public int DisciplineId { get; set; }
        public int? AnalystUserMasterID { get; set; }

        public bool IsActive { get; set; }
        public int? CurrentStatus { get; set; }

    }
    public class TestingHours
    {

        public int SampleParameterFormulaValueId { get; set; }
        public int SampleCollectionId { get; set; }
        public string NotationValue { get; set; }
        public int ParameterGroupId { get; set; }
        public int ParameterMasterId { get; set; }
        public int ParameterFormulaID { get; set; }
        public string TestingHour { get; set; }
        public bool IsFinalResult { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public bool IsFDV { get; set; }
    }
    public class SampleParameterReview
    {
        public string SampleTypeProductName { get; set; }
        public string SampleNameOriginal { get; set; }
        public long SampleCollectionId { get; set; }
        public string SampleNo { get; set; }
        public string SampleName { get; set; }
        public string ProductGroupName { get; set; }
        public string SubGroupName { get; set; }
        public string MatrixName { get; set; }
        public int? TestMethodId { get; set; }
        public string TestMethodName { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public long ParameterGroupId { get; set; }
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
        public string Rejectreviewstatus { get; set; }
        public List<User> Analysts { get; set; }
    }
}