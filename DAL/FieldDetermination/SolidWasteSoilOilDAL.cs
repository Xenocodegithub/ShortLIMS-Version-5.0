using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.FieldDetermination;

namespace LIMS_DEMO.DAL.FieldDetermination
{
    public class SolidWasteSoilOilDAL : IFieldDetermination
    {
        readonly LIMSEntities _dbContext;
        public SolidWasteSoilOilDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public long AddAmbientAir24Hr(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            throw new NotImplementedException();
        }
        public AmbientAirMonitoringEntity GetAirShift1Details(int FieldId)
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
        FDMicrobiologicalInfo IFieldDetermination.GetAirMonitoringDetails(int MicrobiologicalID)
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
        public FDBuildingInfo GetBuildingMaterialDetails(int FieldBuildingMaterialId, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }
        //public BuildingMaterialEntity GetBuildingMaterialDetails(int FieldBuildingMaterialId)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        public WorkplaceAndFugitiveEmissionEntity GetWorkPlaceDetails(int WorkplaceID)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public FoodAndAgriProductsEntity GetFoodDetails(int FieldFoodAndAgriCultureId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public NoiseLevelMonitoringEntity GetNoiseLevelDetails(int FieldNoiseId, int SampleCollectionId)
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
        public FDAirInfo GetAirDetails(int FieldId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public long AddAmbientAir(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public string AddSolidWaste(SolidWasteSoilOilEntity solidWasteSoilOilEntity)
        {
            try
            {
                if (solidWasteSoilOilEntity.SolidHazardousWasteSoilOilId == 0)
                {
                    _dbContext.FieldSolidHazardousWasteSoilOils.Add(new FieldSolidHazardousWasteSoilOil()
                    {

                        SolidHazardousWasteSoilOilId = solidWasteSoilOilEntity.SolidHazardousWasteSoilOilId,
                        SampleCollectionId = solidWasteSoilOilEntity.SampleCollectionId,
                        EnquiryId = solidWasteSoilOilEntity.EnquiryId,
                        Parameters = solidWasteSoilOilEntity.Parameters,
                        TestMethodName = solidWasteSoilOilEntity.TestMethodName,
                        InField = solidWasteSoilOilEntity.InField,
                        IsNABLAccredited = solidWasteSoilOilEntity.IsNABLAccredited,
                        TestResults = solidWasteSoilOilEntity.TestResults,
                        StatusId = solidWasteSoilOilEntity.StatusId,
                        IsActive = solidWasteSoilOilEntity.IsActive,
                        EnteredBy = solidWasteSoilOilEntity.EnteredBy,
                        EnteredDate = solidWasteSoilOilEntity.EnteredDate,
                        AnyOtherObservation = solidWasteSoilOilEntity.AnyOtherObservation,
                    });
                    _dbContext.SaveChanges();

                    var sampleColl = _dbContext.SampleCollections.Find(solidWasteSoilOilEntity.SampleCollectionId);
                    sampleColl.FieldStatusCode = "SampleColl";
                    _dbContext.SaveChanges();
                }
                else
                {
                    var solid = _dbContext.FieldSolidHazardousWasteSoilOils.Find(solidWasteSoilOilEntity.SolidHazardousWasteSoilOilId);
                    solid.StatusId = solidWasteSoilOilEntity.StatusId;
                    solid.Parameters = solidWasteSoilOilEntity.Parameters;
                    solid.TestMethodName = solidWasteSoilOilEntity.TestMethodName;
                    solid.InField = solidWasteSoilOilEntity.InField;
                    solid.IsNABLAccredited = solidWasteSoilOilEntity.IsNABLAccredited;
                    solid.TestResults = solidWasteSoilOilEntity.TestResults;
                    solid.AnyOtherObservation = solidWasteSoilOilEntity.AnyOtherObservation;
                    _dbContext.SaveChanges();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public FDSolidWasteInfo GetSolidDetails(int SolidHazardousWasteSoilOilId, int SampleCollectionId)
        {
            var result = (from fhw in _dbContext.FieldSolidHazardousWasteSoilOils
                          join sts in _dbContext.StatusMasters on fhw.StatusId equals sts.StatusId
                          into field

                          from f in field.DefaultIfEmpty()
                          where fhw.SolidHazardousWasteSoilOilId == SolidHazardousWasteSoilOilId
                          select new SolidWasteSoilOilEntity()
                          {
                              SampleCollectionId = fhw.SampleCollectionId,
                              EnquiryId = fhw.EnquiryId,
                              StatusId = fhw.StatusMaster.StatusId,
                              CurrentStatus = fhw.StatusMaster.StatusCode,
                              Parameters = fhw.Parameters,
                              TestMethodName =fhw.TestMethodName,
                              InField = fhw.InField,
                              IsNABLAccredited = fhw.IsNABLAccredited,
                              TestResults = fhw.TestResults,
                              AnyOtherObservation = fhw.AnyOtherObservation,

                          }
                   ).FirstOrDefault();

            FDSolidWasteInfo solidwasteInfo = new FDSolidWasteInfo();
            solidwasteInfo.SolidWasteDetails = result;
            var GridResult = (from fhw in _dbContext.FieldSolidHazardousWasteSoilOils

                              where fhw.SampleCollectionId == SampleCollectionId
                              select new SolidWasteSoilOilEntity
                              {
                                  SolidHazardousWasteSoilOilId = (Int32)fhw.SolidHazardousWasteSoilOilId,
                                  Parameters = fhw.Parameters,
                                  TestMethodName = fhw.TestMethodName,
                                  InField = fhw.InField,
                                  IsNABLAccredited = fhw.IsNABLAccredited,
                                  TestResults = fhw.TestResults,
                              }).ToList();

            solidwasteInfo.SolidWasteInfos = GridResult;
            return solidwasteInfo;
        }
        
         public string DeleteSolidWasteField(long SolidHazardousWasteSoilOilId)
        {
            try
            {
                _dbContext.FieldSolidHazardousWasteSoilOils.Remove(_dbContext.FieldSolidHazardousWasteSoilOils.Find(SolidHazardousWasteSoilOilId));//Complete Deleted from DB
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

        public string Add(MicrobiologicalMonitoringOfAirEntity microbiologicalMonitoringOfAirEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public string AddCoal(CoalCokeSolidFuelEntity coalCokeSolidFuelEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }

        public List<WasteWaterEntity> GetWaterList(int WasteWaterID)
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
        public string AddBuildingMaterial(BuildingMaterialEntity buildingMaterialEntity)
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
        public long AddStackEmission(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
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
