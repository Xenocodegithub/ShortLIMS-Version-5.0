using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Lab;

namespace LIMS_DEMO.Core.Repository
{
    public interface IReviewer : IDisposable
    {
        IList<ReviewerEntity> GetList(int DisciplineId, int UserMasterId);
        SampleParameterInfoReview GetParameterInfo(int SampleId, int DisciplineId, int UserMasterId);
        TestProcessScheduleDetail GetTestProcessScheduler(TestProcessScheduleDetail testProcessScheduleDetail);
        IList<Core.Lab.TestingHours> GetTestingHoursData(Core.Lab.TestingHours testingData);
        IList<SolutionPreparationData> GetSolutionPreparationData(SolutionPreparationData solutionPreparationData);
        SampleParameterReview GetSampleParameterReview(int SampleId, int DisciplineId, int UserMasterId, long ParameterMasterId);
        Core.Configuration.ParameterFormulaList GetFormula(Core.Configuration.ParameterFormulaList objParam);
        bool UpdateReviewerComment(SampleParameterReview sampleParameterReview);
        long AddNotification(string Msg, string RoleName, SampleParameterReview sampleParameterReview);
        IList<SampleParameterFileInfo> GetParameterFileInfo(SampleParameterFileInfo sampleParameterFileInfo);
    }
}
