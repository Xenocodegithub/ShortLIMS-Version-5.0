using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Collection;

namespace LIMS_DEMO.Core.Repository
{
    public interface ISurveyingTeamLead : IDisposable
    {
        List<SurveyingTeamLeadEntity> GetSurveyingTeamList(int UserMasterID);
        string AddCollector(SurveyingTeamLeadEntity surveyingTeamEntity);
        string AddCollectedBy(SurveyingTeamLeadEntity surveyingTeamEntity);
        SurveyingTeamLeadEntity GetSurveyingTeamLeadEntity(int SampleCollectionId);
    }
}
