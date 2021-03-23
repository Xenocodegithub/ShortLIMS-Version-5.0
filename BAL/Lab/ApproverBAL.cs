using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Lab;
using LIMS_DEMO.DAL.Lab;


namespace LIMS_DEMO.BAL.Lab
{
    public class ApproverBAL
    {
        public ApproverBAL()
        {
            CoreFactory.approver = new ApproverDAL();
        }

        public IList<ApproverEntity> GetList(int DisciplineId, int UserMasterId)
        {
            return CoreFactory.approver.GetList(DisciplineId, UserMasterId);
        }
        public SampleParameterInfoapprover GetParameterInfo(int SampleId, int DisciplineId, int UserMasterId)
        {
            return CoreFactory.approver.GetParameterInfo(SampleId, DisciplineId, UserMasterId);
        }
        public SampleParameterApprove GetSampleParameterApprove(int SampleId, int DisciplineId, int UserMasterId, long ParameterMasterId)
        {
            return CoreFactory.approver.GetSampleParameterApprove(SampleId, DisciplineId, UserMasterId, ParameterMasterId);
        }
        public IList<Core.Lab.TestingHours> GetTestingHoursData(Core.Lab.TestingHours testingData)
        {
            return CoreFactory.approver.GetTestingHoursData(testingData);
        }
        public IList<SolutionPreparationData> GetSolutionPreparationData(SolutionPreparationData solutionPreparationData)
        {
            return CoreFactory.approver.GetSolutionPreparationData(solutionPreparationData);
        }
        public Core.Configuration.ParameterFormulaList GetFormula(Core.Configuration.ParameterFormulaList objParam)
        {
            return CoreFactory.approver.GetFormula(objParam);
        }
        public TestProcessScheduleDetailApprove GetTestProcessScheduler(TestProcessScheduleDetailApprove testProcessScheduleDetail)
        {
            return CoreFactory.approver.GetTestProcessScheduler(testProcessScheduleDetail);
        }
        public IList<ApproverEntity> GetISNABLSave(int ParameterGroupId, int ParameterMasterId, int WorkOrderId)
        {
            return CoreFactory.approver.GetISNABLSave(ParameterGroupId, ParameterMasterId, WorkOrderId);
        }
        public Core.Configuration.ParameterMasterEntity GetIsnabl(int ParameterMasterId, int ParameterGroupId)
        {
            return CoreFactory.approver.GetIsnabl(ParameterMasterId, ParameterGroupId);
        }
        public Core.Configuration.ParameterMasterEntity GetMaxRange(int ParameterMasterId, int ParameterGroupId)
        {
            return CoreFactory.approver.GetMaxRange(ParameterMasterId, ParameterGroupId);
        }
        public Core.Arrival.ReportNoEntity GetULRNo(long SampleCollectionId)
        {
            return CoreFactory.approver.GetULRNo(SampleCollectionId);
        }
        public AnalysisProcessScheduleDetail GetFinalResult(long SampleCollectionId, int ParameterMasterId, int ParameterGroupId)
        {
            return CoreFactory.approver.GetFinalResult(SampleCollectionId, ParameterMasterId, ParameterGroupId);
        }
        public bool UpdateApproverComment(SampleParameterApprove sampleParameterApprove)
        {
            return CoreFactory.approver.UpdateApproverComment(sampleParameterApprove);
        }
        public string UpdateApproveStatus(long SampleCollectionId)
        {
            return CoreFactory.approver.UpdateApproveStatus(SampleCollectionId);

        }
        public long AddNotification(string Msg, string RoleName, SampleParameterApprove sampleParameterApprove)
        {
            return CoreFactory.approver.AddNotification(Msg, RoleName, sampleParameterApprove);
        }
        public string UpdateULRNo(long SampleCollectionId, string ULRNO, bool IsNABLAccredited, int AnalysisProcessScheduleDetailId, int WorkOrderId)
        {
            return CoreFactory.approver.UpdateULRNo(SampleCollectionId, ULRNO, IsNABLAccredited, AnalysisProcessScheduleDetailId, WorkOrderId);
        }
        public bool UpdateIsNablSave(int ParameterGroupId, int ParameterMasterId, int WorkOrderId, int AnalysisProcessScheduleDetailId)
        {
            return CoreFactory.approver.UpdateIsNablSave(ParameterGroupId, ParameterMasterId, WorkOrderId, AnalysisProcessScheduleDetailId);
        }
        public Core.Configuration.ParameterFormulaList GetFinalResultRows(Core.Configuration.ParameterFormulaList objParam)
        {
            return CoreFactory.approver.GetFinalResultRows(objParam);
        }
        public IList<SampleParameterFileInfo> GetParameterFileInfo(SampleParameterFileInfo sampleParameterFileInfo)
        {
            return CoreFactory.approver.GetParameterFileInfo(sampleParameterFileInfo);
        }
        public SampleParameterApprove GetFDData(int SampleId)
        {
            return CoreFactory.approver.GetFDData(SampleId);
        }
    }
}