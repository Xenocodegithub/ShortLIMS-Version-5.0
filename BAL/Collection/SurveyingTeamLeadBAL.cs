using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Collection;
using LIMS_DEMO.DAL.Collection;
namespace LIMS_DEMO.BAL.Collection
{
    public class SurveyingTeamLeadBAL
    {
        public SurveyingTeamLeadBAL()
        {
            CoreFactory.surveyingTeamLead = new SurveyingTeamLeadDAL();

        }

        public List<SurveyingTeamLeadEntity> GetSurveyingTeamList(int UserMasterID)
        {
            return CoreFactory.surveyingTeamLead.GetSurveyingTeamList(UserMasterID);
        }

        public string AddCollector(SurveyingTeamLeadEntity surveyingTeamEntity)
        {
            return CoreFactory.surveyingTeamLead.AddCollector(surveyingTeamEntity);
        }
        public string AddCollectedBy(SurveyingTeamLeadEntity surveyingTeamEntity)
        {
            return CoreFactory.surveyingTeamLead.AddCollectedBy(surveyingTeamEntity);
        }
        public SurveyingTeamLeadEntity GetSurveyingTeamLeadEntity(int SampleCollectionId)
        {
            return CoreFactory.surveyingTeamLead.GetSurveyingTeamLeadEntity(SampleCollectionId);
        }


    }
}
