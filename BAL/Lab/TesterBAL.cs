using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.DAL.Lab;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Lab;


namespace LIMS_DEMO.BAL.Lab
{
    public class TesterBAL
    {
        public TesterBAL()
        {
            CoreFactory.tester = new TesterDAL();
        }
        public IList<TesterEntity> GetList(int DisciplineId, int UserMasterId)
        {
            return CoreFactory.tester.GetList(DisciplineId, UserMasterId);
        }
        public SampleParameterInfo GetParameterInfo(int SampleId, int DisciplineId, int UserMasterId)
        {
            return CoreFactory.tester.GetParameterInfo(SampleId, DisciplineId, UserMasterId);
        }
        public SampleParameterAnalysis GetSampleParameterAnalysis(int SampleId, int DisciplineId, int UserMasterId, long ParameterMasterId)
        {
            return CoreFactory.tester.GetSampleParameterAnalysis(SampleId, DisciplineId, UserMasterId, ParameterMasterId);
        }
        public long SaveSolutionPreparationData(SolutionPreparationData solutionPreparationData)
        {
            return CoreFactory.tester.SaveSolutionPreparationData(solutionPreparationData);
        }
        public IList<SolutionPreparationData> GetSolutionPreparationData(SolutionPreparationData solutionPreparationData)
        {
            return CoreFactory.tester.GetSolutionPreparationData(solutionPreparationData);
        }
        public IList<Core.Lab.SolutionPreparationData> GetChemicalUsedData(Core.Lab.SolutionPreparationData solutionPreparationData)
        {
            return CoreFactory.tester.GetChemicalUsedData(solutionPreparationData);

        }
        public Core.Configuration.ParameterFormulaList GetFormula(Core.Configuration.ParameterFormulaList objParam)
        {
            return CoreFactory.tester.GetFormula(objParam);
        }
        public Core.Configuration.ParameterFormulaList GetFinalResultRows(Core.Configuration.ParameterFormulaList objParam)
        {
            return CoreFactory.tester.GetFinalResultRows(objParam);
        }
        public bool UpdateFinalResult(AnalysisProcessScheduleDetail analysisProcessScheduleDetail, bool isSubmitted)
        {
            return CoreFactory.tester.UpdateFinalResult(analysisProcessScheduleDetail, isSubmitted);
        }
        public bool UpdateCalculatedValue(List<Core.Configuration.FormulaList> formulaList, int SampleCollectionId, int UserMasterId, string TestingHours)
        {
            return CoreFactory.tester.UpdateCalculatedValue(formulaList, SampleCollectionId, UserMasterId, TestingHours);
        }
        public bool UpdateAvgValue(int SampleCollectionId, int ParameterMasterId, int ParameterGroupId, string[] avgResult)
        {
            return CoreFactory.tester.UpdateAvgValue(SampleCollectionId, ParameterMasterId, ParameterGroupId, avgResult);
        }
        public long AddNotification(string Msg, string RoleName, AnalysisProcessScheduleDetail analysisProcessScheduleDetail)
        {
            return CoreFactory.tester.AddNotification(Msg, RoleName, analysisProcessScheduleDetail);
        }
        public TestProcessScheduleDetail GetTestProcessScheduler(TestProcessScheduleDetail testProcessScheduleDetail)
        {
            return CoreFactory.tester.GetTestProcessScheduler(testProcessScheduleDetail);
        }
        public IList<SampleParameterFileInfo> GetParameterFileInfo(SampleParameterFileInfo sampleParameterFileInfo)
        {
            return CoreFactory.tester.GetParameterFileInfo(sampleParameterFileInfo);
        }
        public bool SaveAnalysisProcessFileInfo(List<SampleParameterFileInfo> sampleParameterFileInfos)
        {
            return CoreFactory.tester.SaveAnalysisProcessFileInfo(sampleParameterFileInfos);
        }
        public bool DeleteFile(string FileIdToDelete)
        {
            return CoreFactory.tester.DeleteFile(FileIdToDelete);

        }
        public SampleParameterAnalysis GetFDData(int SampleId)
        {
            return CoreFactory.tester.GetFDData(SampleId);
        }
    }
}