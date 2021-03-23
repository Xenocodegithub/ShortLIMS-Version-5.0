using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.FieldDetermination;

namespace LIMS_DEMO.DAL.FieldDetermination
{
    public class WasteWaterDAL : IFieldDetermination
    {
        readonly LIMSEntities _dbContext;
        public WasteWaterDAL()
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
        public SolidWasteSoilOilEntity GetSolidDetails(int SolidHazardousWasteSoilOilId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public WorkplaceAndFugitiveEmissionEntity GetWorkPlaceDetails(int WorkplaceID)
        {
            throw new NotImplementedException("Not Implemented");
        }
        //public BuildingMaterialEntity GetBuildingMaterialDetails(int FieldBuildingMaterialId)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        public MicrobiologicalMonitoringOfAirEntity GetAirMonitoringDetails(int MicrobiologicalID)
        {
            throw new NotImplementedException();
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
        public string AddWasteWater(WasteWaterEntity wasteWaterEntity)
        {
            try
            {
                if (wasteWaterEntity.WasteWaterID == 0)
                {
                    _dbContext.FieldWasteWaters.Add(new FieldWasteWater()
                    {

                        WasteWaterID = wasteWaterEntity.WasteWaterID,
                        SampleCollectionId = wasteWaterEntity.SampleCollectionId,
                        EnquiryId = wasteWaterEntity.EnquiryId,
                        StatusId = wasteWaterEntity.StatusId,
                        ParameterMappingId = wasteWaterEntity.ParameterMappingId,
                        Parameters = wasteWaterEntity.Parameters,
                        TestMethodName = wasteWaterEntity.TestMethodName,
                        InField = wasteWaterEntity.InField,
                        IsNABLAccredited = wasteWaterEntity.IsNABLAccredited,
                        TestResults = wasteWaterEntity.TestResults,
                        IsActive = wasteWaterEntity.IsActive,
                        EnteredBy = wasteWaterEntity.EnteredBy,
                        EnteredDate = wasteWaterEntity.EnteredDate,
                        AnyOtherObservation = wasteWaterEntity.AnyOtherObservation,
                        WaterUse = wasteWaterEntity.WaterUse,
                    });
                    _dbContext.SaveChanges();

                    var sampleColl = _dbContext.SampleCollections.Find(wasteWaterEntity.SampleCollectionId);
                    sampleColl.FieldStatusCode = "SampleColl";
                    //sampleColl.FieldDeterminationId = wasteWaterEntity.WasteWaterID;
                    _dbContext.SaveChanges();
                }
                else
                {
                    var water = _dbContext.FieldWasteWaters.Find(wasteWaterEntity.WasteWaterID);
                    //water.SampleCollectionId = wasteWaterEntity.SampleCollectionId;
                    //water.EnquiryId = wasteWaterEntity.EnquiryId;
                    water.StatusId = wasteWaterEntity.StatusId;
                    water.Parameters = wasteWaterEntity.Parameters;
                    water.TestMethodName = wasteWaterEntity.TestMethodName;
                    water.InField = wasteWaterEntity.InField;
                    water.IsNABLAccredited = wasteWaterEntity.IsNABLAccredited;
                    water.TestResults = wasteWaterEntity.TestResults;
                    water.WaterUse = wasteWaterEntity.WaterUse;
                    water.AnyOtherObservation = wasteWaterEntity.AnyOtherObservation;
                    _dbContext.SaveChanges();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public FDWaterInfo GetWasteWaterDetails(int WasteWaterID, int SampleCollectionId)
        {
            var result = (from fww in _dbContext.FieldWasteWaters
                          join sts in _dbContext.StatusMasters on fww.StatusId equals sts.StatusId
                          into field

                          from f in field.DefaultIfEmpty()
                          where fww.WasteWaterID == WasteWaterID
                          select new WasteWaterEntity()
                          {
                              SampleCollectionId = fww.SampleCollectionId,
                              EnquiryId = fww.EnquiryId,
                              StatusId = fww.StatusMaster.StatusId,
                              CurrentStatus = fww.StatusMaster.StatusCode,
                              Parameters = fww.Parameters,
                              TestMethodName = fww.TestMethodName,
                              InField = fww.InField,
                              IsNABLAccredited = fww.IsNABLAccredited,
                              TestResults = fww.TestResults,
                              AnyOtherObservation = fww.AnyOtherObservation,
                              WaterUse = fww.WaterUse,
                          }
                   ).FirstOrDefault();

            FDWaterInfo waterInfo = new FDWaterInfo();
            waterInfo.WaterDetails = result;
            var GridResult = (from fww in _dbContext.FieldWasteWaters

                              where fww.SampleCollectionId == SampleCollectionId
                              select new WasteWaterEntity
                              {
                                  WasteWaterID = (Int32)fww.WasteWaterID,
                                  Parameters = fww.Parameters,
                                  TestMethodName = fww.TestMethodName,
                                  InField = fww.InField,
                                  IsNABLAccredited = fww.IsNABLAccredited,
                                  TestResults = fww.TestResults,
                              }).ToList();

            waterInfo.WaterInfos = GridResult;
            return waterInfo;

        }
        public string DeleteWaterField(long WasteWaterID)
        {
            try
            {
                _dbContext.FieldWasteWaters.Remove(_dbContext.FieldWasteWaters.Find(WasteWaterID));//Complete Deleted from DB
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public List<WasteWaterEntity> GetWaterList(int WasteWaterID)
        {
            return (from fww in _dbContext.FieldWasteWaters
                    join sts in _dbContext.StatusMasters on fww.StatusId equals sts.StatusId
                    into field

                    from f in field.DefaultIfEmpty()
                    where fww.WasteWaterID == WasteWaterID
                    select new WasteWaterEntity()
                    {
                        SampleCollectionId = fww.SampleCollectionId,
                        EnquiryId = fww.EnquiryId,
                        StatusId = fww.StatusMaster.StatusId,
                        CurrentStatus = fww.StatusMaster.StatusName,
                        Parameters = fww.Parameters,
                        TestResults = fww.TestResults,
                    }).ToList();
        }

        public long AddStackEmission(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
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
        public string AddSolidWaste(SolidWasteSoilOilEntity solidWasteSoilOilEntity)
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
        public CoalCokeSolidFuelEntity GetCoalDetails(int FieldId)
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

        FDSolidWasteInfo IFieldDetermination.GetSolidDetails(int SolidHazardousWasteSoilOilId, int SampleCollectionId)
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

        public long AddStackParameter(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
        {
            throw new NotImplementedException();
        }

        public string DeleteStackPara(long FDStackEmissionId, long FDStack_ParameterDataId)
        {
            throw new NotImplementedException();
        }
    }
}
