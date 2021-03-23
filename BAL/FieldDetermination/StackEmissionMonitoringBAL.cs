using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.FieldDetermination;
using LIMS_DEMO.DAL.FieldDetermination;


namespace LIMS_DEMO.BAL.FieldDetermination
{
    public class StackEmissionMonitoringBAL
    {
        public StackEmissionMonitoringBAL()
        {
            CoreFactory.fieldDetermination = new StackEmissionMonitoringDAL();
        }
        public string DeleteStackPara(long FDStackEmissionId, long FDStack_ParameterDataId)
        {
            return CoreFactory.fieldDetermination.DeleteStackPara(FDStackEmissionId, FDStack_ParameterDataId);
        }
        public long AddStackParameter(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
        {
            return CoreFactory.fieldDetermination.AddStackParameter(stackEmissionMonitoringEntity);
        }
        public long AddStackEmission(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
        {
            return CoreFactory.fieldDetermination.AddStackEmission(stackEmissionMonitoringEntity);
        }
        public FDStackInfo GetStackDetails(int FDStackEmissionId)
        {
            return CoreFactory.fieldDetermination.GetStackDetails(FDStackEmissionId);
        }
        public string DeleteStackEmissionField(long FDStackEmissionId, long FDStackEmission_IsoKineticId, long FDStackEmission_GaseousDataId)
        {
            return CoreFactory.fieldDetermination.DeleteStackEmissionField(FDStackEmissionId, FDStackEmission_IsoKineticId, FDStackEmission_GaseousDataId);
        }
    }
}
