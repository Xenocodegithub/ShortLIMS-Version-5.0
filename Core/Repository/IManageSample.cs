using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.ManageSampleCollection;

namespace LIMS_DEMO.Core.Repository
{
    public interface IManageSample:IDisposable
    {
        List<ManageSampleEntity> GetCalendarList();/*int UserMasterID, int CollectedBy*/
        string UpdateCalendarStatus(int WorkOrderSampleCollectionDateId, int Status);
    }
}
