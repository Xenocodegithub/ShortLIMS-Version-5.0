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
    public class WasteWaterBAL
    {
        public WasteWaterBAL()
        {
            CoreFactory.fieldDetermination = new WasteWaterDAL();
        }
        public string AddWasteWater(WasteWaterEntity wasteWaterEntity)
        {
            return CoreFactory.fieldDetermination.AddWasteWater(wasteWaterEntity);
        }

        public FDWaterInfo GetWasteWaterDetails(int WasteWaterID, int SampleCollectionId)
        {
            return CoreFactory.fieldDetermination.GetWasteWaterDetails(WasteWaterID, SampleCollectionId);
        }
        public string DeleteWaterField(long WasteWaterID)
        {
            return CoreFactory.fieldDetermination.DeleteWaterField(WasteWaterID);

        }
        public List<WasteWaterEntity> GetWaterList(int WasteWaterID)
        {
            return CoreFactory.fieldDetermination.GetWaterList(WasteWaterID);
        }
    }
}
