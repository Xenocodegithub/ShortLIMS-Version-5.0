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
    public class WorkplaceAndFugitiveEmissionBAL
    {
        public WorkplaceAndFugitiveEmissionBAL()
        {
            CoreFactory.fieldDetermination = new WorkplaceAndFugitiveEmissionDAL();
        }
        public string DeleteWorkplaceField(long WorkplaceID, long WorkplaceGasID)
        {
            return CoreFactory.fieldDetermination.DeleteWorkplaceField(WorkplaceID, WorkplaceGasID);
        }
        public long AddWorkplace(WorkplaceAndFugitiveEmissionEntity workplaceAndFugitiveEmissionEntity)
        {
            return CoreFactory.fieldDetermination.AddWorkplace(workplaceAndFugitiveEmissionEntity);
        }
        public FDWorkplaceInfo GetWorkPlaceDetails(int WorkplaceID, int SampleCollectionId)
        {
            return CoreFactory.fieldDetermination.GetWorkPlaceDetails(WorkplaceID, SampleCollectionId);
        }
    }
}
