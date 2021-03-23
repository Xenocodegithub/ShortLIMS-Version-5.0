using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Lab;


namespace LIMS_DEMO.Core.Repository
{
   public interface IApprover
    {
        IList<ApproverEntity> GetList(int DisciplineId, int UserMasterId);
        SampleParameterInfoapprover GetParameterInfo(int SampleId, int DisciplineId, int UserMasterId);
        SampleParameterApprove GetSampleParameterApprove(int SampleId, int DisciplineId, int UserMasterId, long ParameterMasterId);
        IList<Core.Lab.TestingHours> GetTestingHoursData(Core.Lab.TestingHours testingData);
        IList<SolutionPreparationData> GetSolutionPreparationData(SolutionPreparationData solutionPreparationData);
        Core.Configuration.ParameterFormulaList GetFormula(Core.Configuration.ParameterFormulaList objParam);
        TestProcessScheduleDetailApprove GetTestProcessScheduler(TestProcessScheduleDetailApprove testProcessScheduleDetail);
        IList<ApproverEntity> GetISNABLSave(int ParameterGroupId, int ParameterMasterId, int WorkOrderId);
        Core.Configuration.ParameterMasterEntity GetIsnabl(int ParameterMasterId, int ParameterGroupId);
        Core.Configuration.ParameterMasterEntity GetMaxRange(int ParameterMasterId, int ParameterGroupId);
        AnalysisProcessScheduleDetail GetFinalResult(long SampleCollectionId, int ParameterMasterId, int ParameterGroupId);
        bool UpdateApproverComment(SampleParameterApprove sampleParameterApprove);
        string UpdateApproveStatus(long SampleCollectionId);
        long AddNotification(string Msg, string RoleName, SampleParameterApprove sampleParameterApprove);
        string UpdateULRNo(long SampleCollectionId, string ULRNO, bool IsNABLAccredited, int AnalysisProcessScheduleDetailId, int WorkOrderId);
        bool UpdateIsNablSave(int ParameterGroupId, int ParameterMasterId, int WorkOrderId, int AnalysisProcessScheduleDetailId);
        Core.Arrival.ReportNoEntity GetULRNo(long SampleCollectionId);
        Core.Configuration.ParameterFormulaList GetFinalResultRows(Core.Configuration.ParameterFormulaList objParam);
        IList<SampleParameterFileInfo> GetParameterFileInfo(SampleParameterFileInfo sampleParameterFileInfo);
        SampleParameterApprove GetFDData(int SampleId);

    }
}
