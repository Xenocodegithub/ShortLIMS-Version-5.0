using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.DAL.Lab;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Lab;

namespace LIMS_DEMO.BAL.Lab
{
    public class PlanBAL
    {
        public PlanBAL()
        {
            CoreFactory.plan = new PlanDAL();
        }
        public IList<PlanEntity> GetList(int DisciplineId, int UserMasterID)
        {
            return CoreFactory.plan.GetList(DisciplineId, UserMasterID);
        }
        public SampleInfoEntity GetSampleInfoEntity(int SampleId, int DisciplineId)
        {
            return CoreFactory.plan.GetSampleInfoEntity(SampleId, DisciplineId);
        }
        public bool SaveParameterSelectedDetails(ParameterSelectedDetails parameterSelectedDetails)

        {
            return CoreFactory.plan.SaveParameterSelectedDetails(parameterSelectedDetails);
        }
        //public bool RejectSample(string SampleId, string Remarks)
        //{
        //    return CoreFactory.plan.RejectSample(SampleId, Remarks);
        //}

    }
}