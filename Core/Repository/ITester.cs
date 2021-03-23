using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Lab;

namespace LIMS_DEMO.Core.Repository
{
   public interface ITester
    {
        bool UpdateAvgValue(int SampleCollectionId, int ParameterMasterId, int ParameterGroupId, string[] avgResult);
        IList<SolutionPreparationData> GetSolutionPreparationData(SolutionPreparationData solutionPreparationData);
        IList<Core.Lab.SolutionPreparationData> GetChemicalUsedData(Core.Lab.SolutionPreparationData solutionPreparationData);
        long SaveSolutionPreparationData(SolutionPreparationData solutionPreparationData);
        IList<TesterEntity> GetList(int DisciplineId, int UserMasterId);
        SampleParameterInfo GetParameterInfo(int SampleId, int DisciplineId, int UserMasterId);
        SampleParameterAnalysis GetSampleParameterAnalysis(int SampleId, int DisciplineId, int UserMasterId, long ParameterMasterId);
        Core.Configuration.ParameterFormulaList GetFormula(Core.Configuration.ParameterFormulaList objParam);
        Core.Configuration.ParameterFormulaList GetFinalResultRows(Core.Configuration.ParameterFormulaList objParam);
        bool UpdateCalculatedValue(List<Core.Configuration.FormulaList> formulaList, int SampleCollectionId, int UserMasterId, string TestingHours);
        bool UpdateFinalResult(AnalysisProcessScheduleDetail analysisProcessScheduleDetail, bool isSubmitted);
        long AddNotification(string Msg, string RoleName, AnalysisProcessScheduleDetail analysisProcessScheduleDetail);
        TestProcessScheduleDetail GetTestProcessScheduler(TestProcessScheduleDetail testProcessScheduleDetail);
        bool SaveAnalysisProcessFileInfo(List<SampleParameterFileInfo> sampleParameterFileInfos);
        bool DeleteFile(string FileIdToDelete);
        IList<SampleParameterFileInfo> GetParameterFileInfo(SampleParameterFileInfo sampleParameterFileInfo);
        SampleParameterAnalysis GetFDData(int SampleId);
    }
}
