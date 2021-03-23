using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Lab;
using LIMS_DEMO.DAL.Lab;

namespace LIMS_DEMO.BAL.Lab
{
    public class ReviewerBAL
    {
        public ReviewerBAL()
        {
            CoreFactory.reviewer = new ReviewerDAL();
        }

        public IList<ReviewerEntity> GetList(int DisciplineId, int UserMasterId)
        {
            return CoreFactory.reviewer.GetList(DisciplineId, UserMasterId);
        }
        public SampleParameterInfoReview GetParameterInfo(int SampleId, int DisciplineId, int UserMasterId)
        {
            return CoreFactory.reviewer.GetParameterInfo(SampleId, DisciplineId, UserMasterId);
        }
        public SampleParameterReview GetSampleParameterReview(int SampleId, int DisciplineId, int UserMasterId, long ParameterMasterId)
        {
            return CoreFactory.reviewer.GetSampleParameterReview(SampleId, DisciplineId, UserMasterId, ParameterMasterId);
        }
        public TestProcessScheduleDetail GetTestProcessScheduler(TestProcessScheduleDetail testProcessScheduleDetail)
        {
            return CoreFactory.reviewer.GetTestProcessScheduler(testProcessScheduleDetail);
        }
        public IList<SolutionPreparationData> GetSolutionPreparationData(SolutionPreparationData solutionPreparationData)
        {
            return CoreFactory.reviewer.GetSolutionPreparationData(solutionPreparationData);
        }
        public IList<Core.Lab.TestingHours> GetTestingHoursData(Core.Lab.TestingHours testingData)
        {
            return CoreFactory.reviewer.GetTestingHoursData(testingData);
        }
        public Core.Configuration.ParameterFormulaList GetFormula(Core.Configuration.ParameterFormulaList objParam)
        {
            return CoreFactory.reviewer.GetFormula(objParam);
        }
        public bool UpdateReviewerComment(SampleParameterReview sampleParameterReview)
        {
            return CoreFactory.reviewer.UpdateReviewerComment(sampleParameterReview);
        }
        public long AddNotification(string Msg, string RoleName, SampleParameterReview sampleParameterReview)
        {
            return CoreFactory.reviewer.AddNotification(Msg, RoleName, sampleParameterReview);
        }
        public IList<SampleParameterFileInfo> GetParameterFileInfo(SampleParameterFileInfo sampleParameterFileInfo)
        {
            return CoreFactory.reviewer.GetParameterFileInfo(sampleParameterFileInfo);
        }
    }
}