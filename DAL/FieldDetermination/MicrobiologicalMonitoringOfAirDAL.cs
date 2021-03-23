using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.FieldDetermination;

namespace LIMS_DEMO.DAL.FieldDetermination
{
    public class MicrobiologicalMonitoringOfAirDAL : IFieldDetermination
    {
        readonly LIMSEntities _dbContext;
        public MicrobiologicalMonitoringOfAirDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public long AddAmbientAir24Hr(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            throw new NotImplementedException();
        }
        public AmbientAirMonitoringEntity GetAirShift2Details(int FieldId)
        {
            throw new NotImplementedException();
        }
        public AmbientAirMonitoringEntity GetAirShift3Details(int FieldId)
        {
            throw new NotImplementedException();
        }
        public AmbientAirMonitoringEntity GetAirShift1Details(int FieldId)
        {
            throw new NotImplementedException();
        }
        public long AddAmbientShiftWiseData(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            throw new NotImplementedException();

        }
        FDFoodInfo IFieldDetermination.GetFoodDetails(int FieldFoodAndAgriCultureId, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }
        FDCoalCokeInfo IFieldDetermination.GetCoalDetails(int FieldId, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }
        public WorkplaceAndFugitiveEmissionEntity GetWorkPlaceDetails(int WorkplaceID)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public SolidWasteSoilOilEntity GetSolidDetails(int SolidHazardousWasteSoilOilId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public FDBuildingInfo GetBuildingMaterialDetails(int FieldBuildingMaterialId, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }
        //public BuildingMaterialEntity GetBuildingMaterialDetails(int FieldBuildingMaterialId)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        public NoiseLevelMonitoringEntity GetNoiseLevelDetails(int FieldNoiseId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        //public MicrobiologicalMonitoringOfAirEntity GetAirMonitoringDetails(int MicrobiologicalID)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        public FoodAndAgriProductsEntity GetFoodDetails(int FieldFoodAndAgriCultureId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public FDAirInfo GetAirDetails(int FieldId)
        {
            throw new NotImplementedException("Not Implemented");
        }

        public FDStackInfo GetStackDetails(int FDStackEmissionId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public string Add(MicrobiologicalMonitoringOfAirEntity microbiologicalMonitoringOfAirEntity)
        {
            try
            {
                if (microbiologicalMonitoringOfAirEntity.MicrobiologicalID == 0)
                {
                    _dbContext.FieldMicrobiologicalMonitoringOfAirs.Add(new FieldMicrobiologicalMonitoringOfAir()
                    {
                        MicrobiologicalID = microbiologicalMonitoringOfAirEntity.MicrobiologicalID,
                        SampleCollectionId = microbiologicalMonitoringOfAirEntity.SampleCollectionId,
                        EnquiryId = microbiologicalMonitoringOfAirEntity.EnquiryId,
                        StatusId = microbiologicalMonitoringOfAirEntity.StatusId,
                        AreaSwabbed = microbiologicalMonitoringOfAirEntity.AreaSwabbed,
                        FlowRate = microbiologicalMonitoringOfAirEntity.FlowRate,
                        MediaUsed = microbiologicalMonitoringOfAirEntity.MediaUsed,
                        IndustryType = microbiologicalMonitoringOfAirEntity.IndustryType,
                        EquipmentId = microbiologicalMonitoringOfAirEntity.EquipmentId,
                        RelativeHumidity = microbiologicalMonitoringOfAirEntity.RelativeHumidity,
                        AnyOtherObservation = microbiologicalMonitoringOfAirEntity.AnyOtherObservation,
                        IsActive = microbiologicalMonitoringOfAirEntity.IsActive,
                        EnteredBy = microbiologicalMonitoringOfAirEntity.EnteredBy,
                        EnteredDate = microbiologicalMonitoringOfAirEntity.EnteredDate,
                    });
                    _dbContext.SaveChanges();

                    var sampleColl = _dbContext.SampleCollections.Find(microbiologicalMonitoringOfAirEntity.SampleCollectionId);
                    sampleColl.FieldStatusCode = "SampleColl";
                    _dbContext.SaveChanges();
                }
                else
                {
                    var micro = _dbContext.FieldMicrobiologicalMonitoringOfAirs.Find(microbiologicalMonitoringOfAirEntity.MicrobiologicalID);
                    micro.StatusId = microbiologicalMonitoringOfAirEntity.StatusId;
                    micro.AreaSwabbed = microbiologicalMonitoringOfAirEntity.AreaSwabbed;
                    micro.FlowRate = microbiologicalMonitoringOfAirEntity.FlowRate;
                    micro.MediaUsed = microbiologicalMonitoringOfAirEntity.MediaUsed;
                    micro.IndustryType = microbiologicalMonitoringOfAirEntity.IndustryType;
                    micro.EquipmentId = microbiologicalMonitoringOfAirEntity.EquipmentId;
                    micro.RelativeHumidity = microbiologicalMonitoringOfAirEntity.RelativeHumidity;
                    micro.AnyOtherObservation = microbiologicalMonitoringOfAirEntity.AnyOtherObservation;
                    _dbContext.SaveChanges();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public FDMicrobiologicalInfo GetAirMonitoringDetails(int MicrobiologicalID)
        {
            var result = (from fmm in _dbContext.FieldMicrobiologicalMonitoringOfAirs
                          join sts in _dbContext.StatusMasters on fmm.StatusId equals sts.StatusId
                          into field

                          from f in field.DefaultIfEmpty()
                          where fmm.MicrobiologicalID == MicrobiologicalID
                          select new MicrobiologicalMonitoringOfAirEntity()
                          {
                              SampleCollectionId = fmm.SampleCollectionId,
                              EnquiryId = fmm.EnquiryId,
                              StatusId = fmm.StatusMaster.StatusId,
                              CurrentStatus = fmm.StatusMaster.StatusCode,
                              AreaSwabbed = fmm.AreaSwabbed,
                              FlowRate = fmm.FlowRate,
                              MediaUsed = fmm.MediaUsed,
                              IndustryType = fmm.IndustryType,
                              EquipmentId = fmm.EquipmentId,
                              RelativeHumidity = fmm.RelativeHumidity,
                              AnyOtherObservation = fmm.AnyOtherObservation,
                          }
                   ).FirstOrDefault();
            FDMicrobiologicalInfo microbiologicalInfo = new FDMicrobiologicalInfo();
            microbiologicalInfo.MicrobiologicalDetails = result;
            var GridResult = (from fmm in _dbContext.FieldMicrobiologicalMonitoringOfAirs

                              where fmm.MicrobiologicalID == MicrobiologicalID
                              select new MicrobiologicalMonitoringOfAirEntity
                              {
                                  MicrobiologicalID = (Int32)fmm.MicrobiologicalID,
                                  AreaSwabbed = fmm.AreaSwabbed,
                                  FlowRate = fmm.FlowRate,
                                  MediaUsed = fmm.MediaUsed,
                                  IndustryType = fmm.IndustryType,
                                  EquipmentId = fmm.EquipmentId,
                                  RelativeHumidity = fmm.RelativeHumidity,
                              }).ToList();

            microbiologicalInfo.MicrobiologicalInfos = GridResult;
            return microbiologicalInfo;
        }

        //    public CoalCokeSolidFuelEntity GetCoalDetails(int FieldId)
        //   {
        //    throw new NotImplementedException("Not Implemented");

        //    }
        //public List<WasteWaterEntity> GetWaterList(int WasteWaterID)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        //public string AddCoal(CoalCokeSolidFuelEntity coalCokeSolidFuelEntity)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        //public string AddWasteWater(WasteWaterEntity wasteWaterEntity)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        //public WasteWaterEntity GetWasteWaterDetails(int WasteWaterID)
        //{
        //    throw new NotImplementedException("Not Implemented");

        //}
        //public string AddSolidWaste(SolidWasteSoilOilEntity solidWasteSoilOilEntity)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        //public string AddBuildingMaterial(BuildingMaterialEntity buildingMaterialEntity)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        //public string AddFoodProducts(FoodAndAgriProductsEntity foodAndAgriProductsEntity)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        //public long AddWorkplace(WorkplaceAndFugitiveEmissionEntity workplaceAndFugitiveEmissionEntity)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}

        //public string AddAmbientAir(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        //public long AddAmbientAir(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        //public long AddNoiseLevel(NoiseLevelMonitoringEntity noiseLevelMonitoringEntity)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        //public long AddStackEmission(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            { if (disposing) { _dbContext.Dispose(); } }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public NoiseLevelMonitoringEntity.FDNoiseInfo GetNoiseLevelDetails1(int FieldNoiseId)
        {
            throw new NotImplementedException();
        }

        public string AddCoal(CoalCokeSolidFuelEntity coalCokeSolidFuelEntity)
        {
            throw new NotImplementedException();
        }

        public string AddWasteWater(WasteWaterEntity wasteWaterEntity)
        {
            throw new NotImplementedException();
        }

        public List<WasteWaterEntity> GetWaterList(int WasteWaterID)
        {
            throw new NotImplementedException();
        }

        public WasteWaterEntity GetWasteWaterDetails(int WasteWaterID)
        {
            throw new NotImplementedException();
        }

        public string AddSolidWaste(SolidWasteSoilOilEntity solidWasteSoilOilEntity)
        {
            throw new NotImplementedException();
        }

        public CoalCokeSolidFuelEntity GetCoalDetails(int FieldId)
        {
            throw new NotImplementedException();
        }

        public string AddBuildingMaterial(BuildingMaterialEntity buildingMaterialEntity)
        {
            throw new NotImplementedException();
        }

        public string AddFoodProducts(FoodAndAgriProductsEntity foodAndAgriProductsEntity)
        {
            throw new NotImplementedException();
        }

        public long AddWorkplace(WorkplaceAndFugitiveEmissionEntity workplaceAndFugitiveEmissionEntity)
        {
            throw new NotImplementedException();
        }

        public long AddAmbientAir(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            throw new NotImplementedException();
        }

        public long AddNoiseLevel(NoiseLevelMonitoringEntity noiseLevelMonitoringEntity)
        {
            throw new NotImplementedException();
        }

        public long AddStackEmission(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
        {
            throw new NotImplementedException();
        }
        FDWorkplaceInfo IFieldDetermination.GetWorkPlaceDetails(int WorkplaceID, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }
        NoiseLevelMonitoringEntity.FDNoiseInfo IFieldDetermination.GetNoiseLevelDetails(int FieldNoiseId, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }

        FDSolidWasteInfo IFieldDetermination.GetSolidDetails(int SolidHazardousWasteSoilOilId, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }

        FDWaterInfo IFieldDetermination.GetWasteWaterDetails(int WasteWaterID, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }

        public string DeleteWaterField(long WasteWaterID)
        {
            throw new NotImplementedException();
        }

        public string DeleteWorkplaceField(long WorkplaceID, long WorkplaceGasID)
        {
            throw new NotImplementedException();
        }

        public string DeleteBuildingMaterialField(long FieldBuildingMaterialId)
        {
            throw new NotImplementedException();
        }

        public string DeleteCoalCokeField(long CoalCokeId)
        {
            throw new NotImplementedException();
        }

        public string DeleteFoodAgriField(long FieldFoodAndAgriCultureId)
        {
            throw new NotImplementedException();
        }

        public string DeleteSolidWasteField(long SolidHazardousWasteSoilOilId)
        {
            throw new NotImplementedException();
        }

        public string DeleteNoiseLevelField(string SampleType, long FieldNoiseId, long ParameterId)
        {
            throw new NotImplementedException();
        }

        public string DeleteAmbientAirField(long FieldId, long MatterSizeId, long GasesSampledId)
        {
            throw new NotImplementedException();
        }

        public string DeleteStackEmissionField(long FDStackEmissionId, long FDStackEmission_IsoKineticId, long FDStackEmission_GaseousDataId)
        {
            throw new NotImplementedException();
        }

        public string DeleteStackPara(long FDStackEmissionId, long FDStack_ParameterDataId)
        {
            throw new NotImplementedException();
        }

        public long AddStackParameter(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
        {
            throw new NotImplementedException();
        }
    }
}
