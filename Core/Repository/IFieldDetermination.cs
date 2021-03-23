using LIMS_DEMO.Core.FieldDetermination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LIMS_DEMO.Core.FieldDetermination.NoiseLevelMonitoringEntity;

namespace LIMS_DEMO.Core.Repository
{
    public interface IFieldDetermination : IDisposable
    {
        string DeleteStackPara(long FDStackEmissionId, long FDStack_ParameterDataId);
        long AddStackParameter(StackEmissionMonitoringEntity stackEmissionMonitoringEntity);
        string DeleteStackEmissionField(long FDStackEmissionId, long FDStackEmission_IsoKineticId, long FDStackEmission_GaseousDataId);
        string DeleteAmbientAirField(long FieldId, long MatterSizeId, long GasesSampledId);
        string DeleteNoiseLevelField(string SampleType,long FieldNoiseId, long ParameterId);
        string DeleteSolidWasteField(long SolidHazardousWasteSoilOilId);
        string DeleteFoodAgriField(long FieldFoodAndAgriCultureId);
        string DeleteCoalCokeField(long CoalCokeId);
        string DeleteBuildingMaterialField(long FieldBuildingMaterialId);
        string DeleteWorkplaceField(long WorkplaceID, long WorkplaceGasID);
        string DeleteWaterField(long WasteWaterID);

        //WorkplaceAndFugitiveEmissionEntity GetWorkPlaceDetails(int WorkplaceID);
        FDSolidWasteInfo GetSolidDetails(int SolidHazardousWasteSoilOilId, int SampleCollectionId);

        //NoiseLevelMonitoringEntity GetNoiseLevelDetails(int FieldNoiseId);
        FDFoodInfo GetFoodDetails(int FieldFoodAndAgriCultureId, int SampleCollectionId);
        FDMicrobiologicalInfo GetAirMonitoringDetails(int MicrobiologicalID);
        string Add(MicrobiologicalMonitoringOfAirEntity microbiologicalMonitoringOfAirEntity);
        string AddCoal(CoalCokeSolidFuelEntity coalCokeSolidFuelEntity);
        string AddWasteWater(WasteWaterEntity wasteWaterEntity);
        List<WasteWaterEntity> GetWaterList(int WasteWaterID);
        FDWorkplaceInfo GetWorkPlaceDetails(int WorkplaceID, int SampleCollectionId);
        FDNoiseInfo GetNoiseLevelDetails(int FieldNoiseId, int SampleCollectionId);
        FDStackInfo GetStackDetails(int FDStackEmissionId);
        FDAirInfo GetAirDetails(int FieldId);
        AmbientAirMonitoringEntity GetAirShift1Details(int FieldId);
        AmbientAirMonitoringEntity GetAirShift2Details(int FieldId);
        AmbientAirMonitoringEntity GetAirShift3Details(int FieldId);
        FDBuildingInfo GetBuildingMaterialDetails(int FieldBuildingMaterialId, int SampleCollectionId);
        FDWaterInfo GetWasteWaterDetails(int WasteWaterID, int SampleCollectionId);
        string AddSolidWaste(SolidWasteSoilOilEntity solidWasteSoilOilEntity);

        //CoalCokeSolidFuelEntity GetCoalDetails(int FieldId);
        FDCoalCokeInfo GetCoalDetails(int FieldId, int SampleCollectionId);
        string AddBuildingMaterial(BuildingMaterialEntity buildingMaterialEntity);
        string AddFoodProducts(FoodAndAgriProductsEntity foodAndAgriProductsEntity);
        long AddWorkplace(WorkplaceAndFugitiveEmissionEntity workplaceAndFugitiveEmissionEntity);

        //string AddAmbientAir(AmbientAirMonitoringEntity ambientAirMonitoringEntity);
        long AddAmbientAir(AmbientAirMonitoringEntity ambientAirMonitoringEntity);
        long AddAmbientAir24Hr(AmbientAirMonitoringEntity ambientAirMonitoringEntity);
        long AddAmbientShiftWiseData(AmbientAirMonitoringEntity ambientAirMonitoringEntity);
        long AddNoiseLevel(NoiseLevelMonitoringEntity noiseLevelMonitoringEntity);
        long AddStackEmission(StackEmissionMonitoringEntity stackEmissionMonitoringEntity);
    }
}
