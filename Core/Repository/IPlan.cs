using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Lab;

namespace LIMS_DEMO.Core.Repository
{
   public interface IPlan
    {
        IList<PlanEntity> GetList(int DisciplineId, int UserMasterID);
        SampleInfoEntity GetSampleInfoEntity(int SampleId, int DisciplineId);
        bool SaveParameterSelectedDetails(ParameterSelectedDetails parameterSelectedDetails);
        //bool RejectSample(string SampleId, string Remarks);

    }
}
