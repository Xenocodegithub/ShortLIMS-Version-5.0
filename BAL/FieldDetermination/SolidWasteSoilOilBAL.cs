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
    public class SolidWasteSoilOilBAL
    {
        public SolidWasteSoilOilBAL()
        {
            CoreFactory.fieldDetermination = new SolidWasteSoilOilDAL();
        }
        public string AddSolidWaste(SolidWasteSoilOilEntity solidWasteSoilOilEntity)
        {
            return CoreFactory.fieldDetermination.AddSolidWaste(solidWasteSoilOilEntity);
        }
        public FDSolidWasteInfo GetSolidDetails(int SolidHazardousWasteSoilOilId, int SampleCollectionId)
        {
            return CoreFactory.fieldDetermination.GetSolidDetails(SolidHazardousWasteSoilOilId, SampleCollectionId);
        }
        public string DeleteSolidWasteField(long SolidHazardousWasteSoilOilId)
        {
            return CoreFactory.fieldDetermination.DeleteSolidWasteField(SolidHazardousWasteSoilOilId);

        }
    }
}
