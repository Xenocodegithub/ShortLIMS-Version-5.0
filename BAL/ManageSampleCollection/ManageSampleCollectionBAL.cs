using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.ManageSampleCollection;
using LIMS_DEMO.DAL.ManageSampleCollection;
namespace LIMS_DEMO.BAL.ManageSampleCollection
{
    public class ManageSampleCollectionBAL
    {
        public ManageSampleCollectionBAL()
        {
            CoreFactory.manageSample = new ManageSampleCollectionDAL();
        }
        public List<ManageSampleEntity> GetCalendarList()/*int UserMasterID, int CollectedBy*/
        {
            return CoreFactory.manageSample.GetCalendarList();
        }
        public string UpdateCalendarStatus(int WorkOrderSampleCollectionDateId, int Status)
        {
            return CoreFactory.manageSample.UpdateCalendarStatus(WorkOrderSampleCollectionDateId,Status);

        }
    }
}
