using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.FieldDetermination;

namespace LIMS_DEMO.DAL.FieldDetermination
{
    public class FoodAndAgriProductsDAL : IFieldDetermination
    {
        readonly LIMSEntities _dbContext;
        public FoodAndAgriProductsDAL()
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
        public long AddAmbientShiftWiseData(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            throw new NotImplementedException();

        }
        public AmbientAirMonitoringEntity GetAirShift1Details(int FieldId)
        {
            throw new NotImplementedException();
        }
        FDCoalCokeInfo IFieldDetermination.GetCoalDetails(int FieldId, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }
        public FDBuildingInfo GetBuildingMaterialDetails(int FieldBuildingMaterialId, int SampleCollectionId)
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
        //public BuildingMaterialEntity GetBuildingMaterialDetails(int FieldBuildingMaterialId)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        public NoiseLevelMonitoringEntity GetNoiseLevelDetails(int FieldNoiseId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public FDAirInfo GetAirDetails(int FieldId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public MicrobiologicalMonitoringOfAirEntity GetAirMonitoringDetails(int MicrobiologicalID)
        {
            throw new NotImplementedException();
        }
        public FDStackInfo GetStackDetails(int FDStackEmissionId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public long AddAmbientAir(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public string AddFoodProducts(FoodAndAgriProductsEntity foodAndAgriProductsEntity)
        {
            try
            {
                if (foodAndAgriProductsEntity.FieldFoodAndAgriCultureId == 0)
                {
                    _dbContext.FieldFoodAndAgriCultures.Add(new FieldFoodAndAgriCulture()
                    {
                        FieldFoodAndAgriCultureId = foodAndAgriProductsEntity.FieldFoodAndAgriCultureId,
                        SampleCollectionId = foodAndAgriProductsEntity.SampleCollectionId,
                        EnquiryId = foodAndAgriProductsEntity.EnquiryId,
                        Parameters = foodAndAgriProductsEntity.Parameters,
                        TestMethodName = foodAndAgriProductsEntity.TestMethodName,
                        InField = foodAndAgriProductsEntity.InField,
                        IsNABLAccredited = foodAndAgriProductsEntity.IsNABLAccredited,
                        TestResults = foodAndAgriProductsEntity.TestResults,
                        StatusId = foodAndAgriProductsEntity.StatusId,
                        IsActive = foodAndAgriProductsEntity.IsActive,
                        EnteredBy = foodAndAgriProductsEntity.EnteredBy,
                        EnteredDate = foodAndAgriProductsEntity.EnteredDate,
                        AnyOtherObservation = foodAndAgriProductsEntity.AnyOtherObservation,
                    });
                    _dbContext.SaveChanges();

                    var sampleColl = _dbContext.SampleCollections.Find(foodAndAgriProductsEntity.SampleCollectionId);
                    sampleColl.FieldStatusCode = "SampleColl";
                    _dbContext.SaveChanges();
                }
                else
                {
                    var foodagri = _dbContext.FieldFoodAndAgriCultures.Find(foodAndAgriProductsEntity.FieldFoodAndAgriCultureId);
                    foodagri.StatusId = foodAndAgriProductsEntity.StatusId;
                    foodagri.Parameters = foodAndAgriProductsEntity.Parameters;
                    foodagri.TestMethodName = foodAndAgriProductsEntity.TestMethodName;
                    foodagri.InField = foodAndAgriProductsEntity.InField;
                    foodagri.IsNABLAccredited = foodAndAgriProductsEntity.IsNABLAccredited;
                    foodagri.TestResults = foodAndAgriProductsEntity.TestResults;
                    foodagri.AnyOtherObservation = foodAndAgriProductsEntity.AnyOtherObservation;
                    _dbContext.SaveChanges();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public FDFoodInfo GetFoodDetails(int FieldFoodAndAgriCultureId, int SampleCollectionId)
        {
            var result = (from ffg in _dbContext.FieldFoodAndAgriCultures
                          join sts in _dbContext.StatusMasters on ffg.StatusId equals sts.StatusId
                          into field

                          from f in field.DefaultIfEmpty()
                          where ffg.FieldFoodAndAgriCultureId == FieldFoodAndAgriCultureId
                          select new FoodAndAgriProductsEntity()
                          {
                              SampleCollectionId = ffg.SampleCollectionId,
                              EnquiryId = ffg.EnquiryId,
                              StatusId = ffg.StatusMaster.StatusId,
                              CurrentStatus = ffg.StatusMaster.StatusCode,
                              Parameters = ffg.Parameters,
                              TestMethodName = ffg.TestMethodName,
                              InField = ffg.InField,
                              IsNABLAccredited = ffg.IsNABLAccredited,
                              TestResults = ffg.TestResults,
                              AnyOtherObservation = ffg.AnyOtherObservation,
                          }
                   ).FirstOrDefault();

            FDFoodInfo foodInfo = new FDFoodInfo();
            foodInfo.FoodDetails = result;
            var GridResult = (from ffc in _dbContext.FieldFoodAndAgriCultures

                              where ffc.SampleCollectionId == SampleCollectionId
                              select new FoodAndAgriProductsEntity
                              {
                                  FieldFoodAndAgriCultureId = (Int32)ffc.FieldFoodAndAgriCultureId,
                                  Parameters = ffc.Parameters,
                                  TestMethodName = ffc.TestMethodName,
                                  InField = ffc.InField,
                                  IsNABLAccredited = ffc.IsNABLAccredited,
                                  TestResults = ffc.TestResults,
                              }).ToList();

            foodInfo.FoodInfos = GridResult;
            return foodInfo;
        }
        
        public string DeleteFoodAgriField(long FieldFoodAndAgriCultureId)
        {
            try
            {
                _dbContext.FieldCoalCokeSolidFuels.Remove(_dbContext.FieldCoalCokeSolidFuels.Find(FieldFoodAndAgriCultureId));//Complete Deleted from DB
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public CoalCokeSolidFuelEntity GetCoalDetails(int FieldId)
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
        public string AddBuildingMaterial(BuildingMaterialEntity buildingMaterialEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public long AddStackEmission(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
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
        FDWorkplaceInfo IFieldDetermination.GetWorkPlaceDetails(int WorkplaceID, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }
        NoiseLevelMonitoringEntity.FDNoiseInfo IFieldDetermination.GetNoiseLevelDetails(int FieldNoiseId, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }

        public FDBuildingInfo GetBUildingMaterialDetails(int FieldBuildingMaterialId)
        {
            throw new NotImplementedException();
        }

        FDMicrobiologicalInfo IFieldDetermination.GetAirMonitoringDetails(int MicrobiologicalID)
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
    }
}
