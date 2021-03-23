using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.FieldDetermination;

namespace LIMS_DEMO.DAL.FieldDetermination
{
    public class CoalCokeSolidFuelDAL : IFieldDetermination
    {
        readonly LIMSEntities _dbContext;
        public CoalCokeSolidFuelDAL()
        {
            _dbContext = new LIMSEntities();
        }
        //FDCoalCokeInfo IFieldDetermination.GetCoalDetails(int FieldId)
        //{
        //    throw new NotImplementedException();
        //}
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
        public long AddAmbientShiftWiseData(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            throw new NotImplementedException();

        }
        FDMicrobiologicalInfo IFieldDetermination.GetAirMonitoringDetails(int MicrobiologicalID)
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
        public NoiseLevelMonitoringEntity GetNoiseLevelDetails(int FieldNoiseId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public FoodAndAgriProductsEntity GetFoodDetails(int FieldFoodAndAgriCultureId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public MicrobiologicalMonitoringOfAirEntity GetAirMonitoringDetails(int MicrobiologicalID)
        {
            throw new NotImplementedException("Not Implemented");
        }
        //public BuildingMaterialEntity GetBuildingMaterialDetails(int FieldBuildingMaterialId)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        public FDAirInfo GetAirDetails(int FieldId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public FDStackInfo GetStackDetails(int FDStackEmissionId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public long AddAmbientAir(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public string AddCoal(CoalCokeSolidFuelEntity coalCokeSolidFuelEntity)
        {
            try
            {
                if (coalCokeSolidFuelEntity.FieldId == 0)
                {
                    _dbContext.FieldCoalCokeSolidFuels.Add(new FieldCoalCokeSolidFuel()
                    {
                        FieldId = coalCokeSolidFuelEntity.FieldId,
                        SampleCollectionId = coalCokeSolidFuelEntity.SampleCollectionId,
                        EnquiryId = coalCokeSolidFuelEntity.EnquiryId,
                        StatusId = coalCokeSolidFuelEntity.StatusId,
                        Parameters = coalCokeSolidFuelEntity.Parameters,
                        TestMethodName =coalCokeSolidFuelEntity.TestMethodName,
                        InField = coalCokeSolidFuelEntity.InField,
                        IsNABLAccredited = coalCokeSolidFuelEntity.IsNABLAccredited,
                        TestResults = coalCokeSolidFuelEntity.TestResults,
                        AnyOtherFieldObservations = coalCokeSolidFuelEntity.AnyOtherObservations,
                        IsActive = coalCokeSolidFuelEntity.IsActive,
                        EnteredBy = coalCokeSolidFuelEntity.EnteredBy,
                        EnteredDate = coalCokeSolidFuelEntity.EnteredDate,
                    });
                    _dbContext.SaveChanges();

                    var sampleColl = _dbContext.SampleCollections.Find(coalCokeSolidFuelEntity.SampleCollectionId);
                    sampleColl.FieldStatusCode = "SampleColl";
                    _dbContext.SaveChanges();
                }
                else
                {
                    var coalcoke = _dbContext.FieldCoalCokeSolidFuels.Find(coalCokeSolidFuelEntity.FieldId);
                    coalcoke.StatusId = coalCokeSolidFuelEntity.StatusId;
                    coalcoke.Parameters = coalCokeSolidFuelEntity.Parameters;
                    coalcoke.TestMethodName = coalCokeSolidFuelEntity.TestMethodName;
                    coalcoke.InField = coalCokeSolidFuelEntity.InField;
                    coalcoke.IsNABLAccredited = coalCokeSolidFuelEntity.IsNABLAccredited;
                    coalcoke.TestResults = coalCokeSolidFuelEntity.TestResults;
                    coalcoke.AnyOtherFieldObservations = coalCokeSolidFuelEntity.AnyOtherObservations;
                    _dbContext.SaveChanges();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public FDCoalCokeInfo GetCoalDetails(int FieldId, int SampleCollectionId)
        {
            var result = (from fww in _dbContext.FieldCoalCokeSolidFuels
                          join sts in _dbContext.StatusMasters on fww.StatusId equals sts.StatusId
                          into field

                          // from f in field.DefaultIfEmpty()
                          where fww.FieldId == FieldId
                          select new CoalCokeSolidFuelEntity()
                          {
                              SampleCollectionId = fww.SampleCollectionId,
                              EnquiryId = fww.EnquiryId,
                              StatusId = fww.StatusMaster.StatusId,
                              CurrentStatus = fww.StatusMaster.StatusCode,
                              Parameters = fww.Parameters,
                              TestMethodName =fww.TestMethodName,
                              InField = fww.InField,
                              IsNABLAccredited = fww.IsNABLAccredited,
                              TestResults = fww.TestResults,
                              AnyOtherObservations = fww.AnyOtherFieldObservations,

                          }
                   ).FirstOrDefault();

            FDCoalCokeInfo coalcokeInfo = new FDCoalCokeInfo();
            coalcokeInfo.CoalCokeDetails = result;
            var GridResult = (from fcc in _dbContext.FieldCoalCokeSolidFuels

                              where fcc.SampleCollectionId == SampleCollectionId
                              select new CoalCokeSolidFuelEntity
                              {
                                  FieldId = (Int32)fcc.FieldId,
                                  Parameters = fcc.Parameters,
                                  TestMethodName=fcc.TestMethodName,
                                  InField = fcc.InField,
                                  IsNABLAccredited = fcc.IsNABLAccredited,
                                  TestResults = fcc.TestResults,

                              }).ToList();

            coalcokeInfo.CoalCokeInfos = GridResult;
            return coalcokeInfo;

        }
        
        public string DeleteCoalCokeField(long CoalCokeId)
        {
            var FieldId = CoalCokeId;
            try
            {
                _dbContext.FieldCoalCokeSolidFuels.Remove(_dbContext.FieldCoalCokeSolidFuels.Find(FieldId));//Complete Deleted from DB
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
            throw new NotImplementedException("Not Implemented");
        }
        public string Add(MicrobiologicalMonitoringOfAirEntity microbiologicalMonitoringOfAirEntity)
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
        public long AddStackEmission(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
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
        FDFoodInfo IFieldDetermination.GetFoodDetails(int FieldFoodAndAgriCultureId, int SampleCollectionId)
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

        //public FDCoalCokeInfo GetCoalDetails(int FieldId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
