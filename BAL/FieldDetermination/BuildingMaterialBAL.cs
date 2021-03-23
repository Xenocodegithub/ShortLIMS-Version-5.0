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
    public class BuildingMaterialBAL
    {
        public BuildingMaterialBAL()
        {
            CoreFactory.fieldDetermination = new BuildingMaterialDAL();
        }
        public FDBuildingInfo GetBuildingMaterialDetails(int FieldBuildingMaterialId, int SampleCollectionId)
        {
            return CoreFactory.fieldDetermination.GetBuildingMaterialDetails(FieldBuildingMaterialId, SampleCollectionId);
        }
        public string AddBuildingMaterial(BuildingMaterialEntity buildingMaterialEntity)
        {
            return CoreFactory.fieldDetermination.AddBuildingMaterial(buildingMaterialEntity);
        }
        public string DeleteBuildingMaterialField(long FieldBuildingMaterialId)
        {
            return CoreFactory.fieldDetermination.DeleteBuildingMaterialField(FieldBuildingMaterialId);
        }
    }
}
