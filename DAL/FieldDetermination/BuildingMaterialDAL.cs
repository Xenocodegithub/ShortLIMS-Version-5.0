using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.FieldDetermination;

namespace LIMS_DEMO.DAL.FieldDetermination
{
    public class BuildingMaterialDAL : IFieldDetermination
    {
        readonly LIMSEntities _dbContext;
        public BuildingMaterialDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public string DeleteStackPara(long FDStackEmissionId, long FDStack_ParameterDataId)
        {
            throw new NotImplementedException();
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
            FDMicrobiologicalInfo IFieldDetermination.GetAirMonitoringDetails(int MicrobiologicalID)
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
        public WorkplaceAndFugitiveEmissionEntity GetWorkPlaceDetails(int WorkplaceID)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public SolidWasteSoilOilEntity GetSolidDetails(int SolidHazardousWasteSoilOilId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public NoiseLevelMonitoringEntity GetNoiseLevelDetails(int FieldNoiseId)
        {
            throw new NotImplementedException("Not Implemented");
        }
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
        public string AddBuildingMaterial(BuildingMaterialEntity buildingMaterialEntity)
        {
            try
            {
                if(buildingMaterialEntity.FieldBuildingMaterialId == 0)
                {
                    _dbContext.FieldBuildingMaterials.Add(new FieldBuildingMaterial()
                    {
                        FieldBuildingMaterialId = buildingMaterialEntity.FieldBuildingMaterialId,
                        SampleCollectionId = buildingMaterialEntity.SampleCollectionId,
                        EnquiryId = buildingMaterialEntity.EnquiryId,
                        Parameters = buildingMaterialEntity.Parameters,
                        TestMethodName = buildingMaterialEntity.TestMethodName,
                        InField = buildingMaterialEntity.InField,
                        IsNABLAccredited = buildingMaterialEntity.IsNABLAccredited,
                        TestResults = buildingMaterialEntity.TestResults,
                        StatusId = buildingMaterialEntity.StatusId,
                        IsActive = buildingMaterialEntity.IsActive,
                        EnteredBy = buildingMaterialEntity.EnteredBy,
                        EnteredDate = buildingMaterialEntity.EnteredDate,
                        AnyOtherObservation = buildingMaterialEntity.AnyOtherObservation,
                    });
                    _dbContext.SaveChanges();

                    var sampleColl = _dbContext.SampleCollections.Find(buildingMaterialEntity.SampleCollectionId);
                    sampleColl.FieldStatusCode = "SampleColl";
                    _dbContext.SaveChanges();
                }
                else
                {
                    var buildingMaterial = _dbContext.FieldBuildingMaterials.Find(buildingMaterialEntity.FieldBuildingMaterialId);
                    buildingMaterial.StatusId = buildingMaterialEntity.StatusId;
                    buildingMaterial.Parameters = buildingMaterialEntity.Parameters;
                    buildingMaterial.TestMethodName = buildingMaterialEntity.TestMethodName;
                    buildingMaterial.InField = buildingMaterialEntity.InField;
                    buildingMaterial.IsNABLAccredited = buildingMaterialEntity.IsNABLAccredited;
                    buildingMaterial.TestResults = buildingMaterialEntity.TestResults;
                    buildingMaterial.AnyOtherObservation = buildingMaterialEntity.AnyOtherObservation;
                    _dbContext.SaveChanges();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public FDBuildingInfo GetBuildingMaterialDetails(int FieldBuildingMaterialId, int SampleCollectionId)
        {
            var result = (from fbm in _dbContext.FieldBuildingMaterials
                          join sts in _dbContext.StatusMasters on fbm.StatusId equals sts.StatusId
                          where fbm.FieldBuildingMaterialId == FieldBuildingMaterialId
                          select new BuildingMaterialEntity()
                          {
                              SampleCollectionId = fbm.SampleCollectionId,
                              EnquiryId = fbm.EnquiryId,
                              StatusId = fbm.StatusMaster.StatusId,
                              CurrentStatus = fbm.StatusMaster.StatusCode,
                              Parameters = fbm.Parameters,
                              TestMethodName = fbm.TestMethodName,
                              InField = fbm.InField,
                              IsNABLAccredited = fbm.IsNABLAccredited,
                              TestResults = fbm.TestResults,
                              AnyOtherObservation = fbm.AnyOtherObservation,
                          }
                   ).FirstOrDefault();

            FDBuildingInfo buildingInfo = new FDBuildingInfo();
            buildingInfo.BuildingeDetails = result;
            var GridResult = (from fbm in _dbContext.FieldBuildingMaterials
                              where fbm.SampleCollectionId == SampleCollectionId
                              select new BuildingMaterialEntity
                              {
                                  FieldBuildingMaterialId = (Int32)fbm.FieldBuildingMaterialId,
                                  Parameters = fbm.Parameters,
                                  TestMethodName=fbm.TestMethodName,
                                  InField = fbm.InField,
                                  IsNABLAccredited = fbm.IsNABLAccredited,
                                  TestResults = fbm.TestResults,
                              }).ToList();

            buildingInfo.BuildingInfos = GridResult;
            return buildingInfo;

        }
        public string DeleteBuildingMaterialField(long FieldBuildingMaterialId)
        {
            try
            {
                _dbContext.FieldBuildingMaterials.Remove(_dbContext.FieldBuildingMaterials.Find(FieldBuildingMaterialId));//Complete Deleted from DB
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public CoalCokeSolidFuelEntity GetCoalDetails(int FieldId, int SampleCollectionId)
        {
            throw new NotImplementedException("Not Implemented");

        }
        public List<WasteWaterEntity> GetWaterList(int WasteWaterID)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public string Add(MicrobiologicalMonitoringOfAirEntity microbiologicalMonitoringOfAirEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public string AddCoal(CoalCokeSolidFuelEntity coalCokeSolidFuelEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public long AddStackEmission(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public string AddWasteWater(WasteWaterEntity wasteWaterEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public WasteWaterEntity GetWasteWaterDetails(int WasteWaterID)
        {
            throw new NotImplementedException("Not Implemented");

        }
        public string AddSolidWaste(SolidWasteSoilOilEntity solidWasteSoilOilEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public string AddFoodProducts(FoodAndAgriProductsEntity foodAndAgriProductsEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public long AddWorkplace(WorkplaceAndFugitiveEmissionEntity workplaceAndFugitiveEmissionEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }

        //public string AddAmbientAir(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        public long AddAmbientAir(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }

        public long AddNoiseLevel(NoiseLevelMonitoringEntity noiseLevelMonitoringEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }
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
        public MicrobiologicalMonitoringOfAirEntity GetAirMonitoringDetails(int MicrobiologicalID)
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

        FDCoalCokeInfo IFieldDetermination.GetCoalDetails(int FieldId, int SampleCollectionId)
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

        public long AddStackParameter(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
        {
            throw new NotImplementedException();
        }

        //public FDBuildingInfo GetBuildingMaterialDetails(int FieldBuildingMaterialId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
