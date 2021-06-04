using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Lab
{
    public class PlanEntity
    {
        public long SampleCollectionId { get; set; }
        public string SampleNo { get; set; }
        public string SampleName { get; set; }
        public string ProductGroupName { get; set; }
        public string SubGroupName { get; set; }
        public string SampleTypeProductName { get; set; }
        public string SampleNameOriginal { get; set; }
        public string MatrixName { get; set; }
        public string StatusName { get; set; }
        public int PlanActive { get; set; }
    }
    public class SampleInfoEntity
    {
        public string SampleLocation { get; set; }
        public string Quantity { get; set; }
        public string CollectedBy { get; set; }
        public string Procedure { get; set; }
        public string EnvCondition { get; set; }
        public DateTime? ReportSent { get; set; }
        public DateTime? SamplingDateTime { get; set; }
        public DateTime? DateReceiptOfSampling { get; set; }
        public List<ParameterInfo> ParameterInfos { get; set; }

        public List<User> Analysts { get; set; }
        public List<User> Reviewers { get; set; }
        public List<User> Approvers { get; set; }

    }
    public class ParameterInfo
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
        public long? ReviewerUserMasterID { get; set; }
        public long? ApproverUserMasterID { get; set; }
        public List<TestMethods> TestMethodList { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsField { get; set; }
        public long SelectedTestMethodId { get; set; }
        public string FinalResult { get; set; }
        public int? CurrentStatus { get; set; }
    }
    public class TestMethods
    {
        public int? TestMethodId { get; set; }
        public string TestMethodName { get; set; }
    }
    public class User
    {
        public long UserId { get; set; }
        public string Username { get; set; }
    }
    public class ParameterSelectedDetails
    {
        public int SampleCollectionId { get; set; }
        public int ParameterGroupId { get; set; }
        public int ParameterMasterId { get; set; }
        public int TestMethodID { get; set; }
        public int UnitId { get; set; }
        public int AnalystUserMasterID { get; set; }
        public int ReviewerUserMasterID { get; set; }
        public int ApproverUserMasterID { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
    }
}