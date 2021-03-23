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
    public class NoiseLevelMonitoringBAL
    {
        public NoiseLevelMonitoringBAL()
        {
            CoreFactory.fieldDetermination = new NoiseLevelMonitoringDAL();
        }
        public long AddNoiseLevel(NoiseLevelMonitoringEntity noiseLevelMonitoringEntity)
        {
            return CoreFactory.fieldDetermination.AddNoiseLevel(noiseLevelMonitoringEntity);
        }
        public NoiseLevelMonitoringEntity.FDNoiseInfo GetNoiseLevelDetails(int FieldNoiseId, int SampleCollectionId)
        {
            return CoreFactory.fieldDetermination.GetNoiseLevelDetails(FieldNoiseId, SampleCollectionId);
        }
        public string DeleteNoiseLevelField(string SampleType,long FieldNoiseId, long ParameterId)
        {
            return CoreFactory.fieldDetermination.DeleteNoiseLevelField(SampleType,FieldNoiseId, ParameterId);

        }
    }
}
