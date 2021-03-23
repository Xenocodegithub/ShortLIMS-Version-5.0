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
    public class AmbientAirMonitoringBAL
    {
        public AmbientAirMonitoringBAL()
        {
            CoreFactory.fieldDetermination = new AmbientAirMonitoringDAL();
        }
        //public string AddAmbientAir(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        //{
        //    return CoreFactory.fieldDetermination.AddAmbientAir(ambientAirMonitoringEntity);
        //}
        public long AddAmbientAir(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            return CoreFactory.fieldDetermination.AddAmbientAir(ambientAirMonitoringEntity);
        }
        public long AddAmbientAir24Hr(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            return CoreFactory.fieldDetermination.AddAmbientAir24Hr(ambientAirMonitoringEntity);
        }
        public long AddAmbientShiftWiseData(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            return CoreFactory.fieldDetermination.AddAmbientShiftWiseData(ambientAirMonitoringEntity);
        }
        public FDAirInfo GetAirDetails(int FieldId)
        {
            return CoreFactory.fieldDetermination.GetAirDetails(FieldId);
        }
        public AmbientAirMonitoringEntity GetAirShift1Details(int FieldId)
        {
            return CoreFactory.fieldDetermination.GetAirShift1Details(FieldId);
        }
        public AmbientAirMonitoringEntity GetAirShift2Details(int FieldId)
        {
            return CoreFactory.fieldDetermination.GetAirShift2Details(FieldId);
        }
        public AmbientAirMonitoringEntity GetAirShift3Details(int FieldId)
        {
            return CoreFactory.fieldDetermination.GetAirShift3Details(FieldId);
        }
        public string DeleteAmbientAirField(long FieldId, long MatterSizeId, long GasesSampledId)
        {
            return CoreFactory.fieldDetermination.DeleteAmbientAirField(FieldId, MatterSizeId, GasesSampledId);
        }
    }
}
