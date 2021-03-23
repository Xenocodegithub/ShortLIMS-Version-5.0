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
    public class MicrobiologicalMonitoringOfAirBAL
    {
        public MicrobiologicalMonitoringOfAirBAL()
        {
            CoreFactory.fieldDetermination = new MicrobiologicalMonitoringOfAirDAL();
        }
        public string Add(MicrobiologicalMonitoringOfAirEntity microbiologicalMonitoringOfAirEntity)
        {
            return CoreFactory.fieldDetermination.Add(microbiologicalMonitoringOfAirEntity);
        }
        public FDMicrobiologicalInfo GetAirMonitoringDetails(int MicrobiologicalID)
        {
            return CoreFactory.fieldDetermination.GetAirMonitoringDetails(MicrobiologicalID);
        }


    }
}
