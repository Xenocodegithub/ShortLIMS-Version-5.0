using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.FieldDetermination;


namespace LIMS_DEMO.DAL.FieldDetermination
{
    public class AmbientAirMonitoringDAL : IFieldDetermination
    {
        readonly LIMSEntities _dbContext;
        public AmbientAirMonitoringDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public AmbientAirMonitoringEntity GetAirShift1Details(int FieldId)
        {
            var result = (from aam in _dbContext.FieldAmbientAirMatterSizes
                          where aam.FieldId == FieldId & aam.ShiftNo == 1
                          select new AmbientAirMonitoringEntity
                          {
                              FieldId = (Int32)aam.FieldId,
                              CycloneCupNo = aam.CycloneCupNo,
                              ShiftNo = (Int32)aam.ShiftNo,
                              TimeTotalizerDiff = aam.TimeTotalizerDiff,
                              TimeTotalizerFinal = aam.TimeTotalizerFinal,
                              TimeTotalizerInitial = aam.TimeTotalizerInitial,
                              SamplingDurationDiff = aam.SamplingDurationDiff,
                              SamplingDurationFinal = aam.SamplingDurationFinal,
                              SamplingDurationInitial = aam.SamplingDurationInitial,
                              FlowRateDiff = aam.FlowRateDiff,
                              FlowRateFinal = aam.FlowRateFinal,
                              FlowRateInitial = aam.FlowRateInitial,
                              MatterSizeId = aam.MatterSizeId,
                              FilterPaperNo = aam.FilterPaperNo,
                              IsActive = aam.IsActive,
                          }).FirstOrDefault();
            return result;
        }
        public AmbientAirMonitoringEntity GetAirShift2Details(int FieldId)
        {
            var result = (from aam in _dbContext.FieldAmbientAirMatterSizes
                          where aam.FieldId == FieldId & aam.ShiftNo == 2
                          select new AmbientAirMonitoringEntity
                          {
                              FieldId = (Int32)aam.FieldId,
                              CycloneCupNo = aam.CycloneCupNo,
                              ShiftNo = (Int32)aam.ShiftNo,
                              TimeTotalizerDiff = aam.TimeTotalizerDiff,
                              TimeTotalizerFinal = aam.TimeTotalizerFinal,
                              TimeTotalizerInitial = aam.TimeTotalizerInitial,
                              SamplingDurationDiff = aam.SamplingDurationDiff,
                              SamplingDurationFinal = aam.SamplingDurationFinal,
                              SamplingDurationInitial = aam.SamplingDurationInitial,
                              FlowRateDiff = aam.FlowRateDiff,
                              FlowRateFinal = aam.FlowRateFinal,
                              FlowRateInitial = aam.FlowRateInitial,
                              MatterSizeId = aam.MatterSizeId,
                              FilterPaperNo = aam.FilterPaperNo,
                              IsActive = aam.IsActive,
                          }).FirstOrDefault();
            return result;
        }
        public AmbientAirMonitoringEntity GetAirShift3Details(int FieldId)
        {
            var result = (from aam in _dbContext.FieldAmbientAirMatterSizes
                          where aam.FieldId == FieldId & aam.ShiftNo == 3
                          select new AmbientAirMonitoringEntity
                          {
                              FieldId = (Int32)aam.FieldId,
                              CycloneCupNo = aam.CycloneCupNo,
                              ShiftNo = (Int32)aam.ShiftNo,
                              TimeTotalizerDiff = aam.TimeTotalizerDiff,
                              TimeTotalizerFinal = aam.TimeTotalizerFinal,
                              TimeTotalizerInitial = aam.TimeTotalizerInitial,
                              SamplingDurationDiff = aam.SamplingDurationDiff,
                              SamplingDurationFinal = aam.SamplingDurationFinal,
                              SamplingDurationInitial = aam.SamplingDurationInitial,
                              FlowRateDiff = aam.FlowRateDiff,
                              FlowRateFinal = aam.FlowRateFinal,
                              FlowRateInitial = aam.FlowRateInitial,
                              MatterSizeId = aam.MatterSizeId,
                              FilterPaperNo = aam.FilterPaperNo,
                              IsActive = aam.IsActive,
                          }).FirstOrDefault();
            return result;
        }

        public FDAirInfo GetAirDetails(int FieldId)
        {
            var result = (from aam in _dbContext.FieldAmbientAirMonitorings
                          where aam.FieldId == FieldId
                          select new AmbientAirMonitoringEntity
                          {
                              FieldId = aam.FieldId,
                              StatusId = aam.StatusMaster.StatusId,
                              CurrentStatus = aam.StatusMaster.StatusCode,
                              InstrumentId = aam.InstrumentId,
                              WindVelocity = aam.WindVelocity,
                              SamplingDuration = aam.SamplingDuration,
                              WindDirection_ = aam.WindDirection_,
                              AverageWindVelocity = aam.AverageWindVelocity,
                              RelativeHumidity = aam.RelativeHumidity,
                              Temperature = aam.Temperature,
                              Temperature24Hr = aam.Temperature24Hr,
                              RelativeHumidity24Hr = aam.RelativeHumidity24Hr,
                              AvgFlowRate = aam.AvgFlowRate,
                              StartTime = aam.StartTime,
                              EndTime = aam.EndTime,
                              MatterSamplingDuration = aam.MatterSamplingDuration,
                              TotalVolAirPassed_L = aam.TotalVolAirPassed_L,
                              TotalVolumeAirPassed_m3 = aam.TotalVolumeAirPassed_m3,
                              IsActive = aam.IsActive,
                          }).FirstOrDefault();

            FDAirInfo airInfo = new FDAirInfo();
            airInfo.AirDetails = result;
            var GridResult = (from aam in _dbContext.FieldAmbientAirMonitorings
                              join aag in _dbContext.FieldAmbientAirGasesSamplings on aam.FieldId equals aag.FieldId
                              where aam.FieldId == FieldId & aag.Testing == false
                              select new AmbientAirMonitoringEntity
                              {
                                  FieldId = (Int32)aag.FieldId,
                                  GasesSampledId = aag.GasesSampledId,
                                  GasesSampled = aag.GasesSampled,
                                  VolImpingingSolution = aag.VolImpingingSolution,
                                  RotaMeterFlow = aag.RotaMeterFlow,
                                  Duration = aag.Duration,
                                  ShiftNoGases = (Int32)aag.ShiftNoGases,
                                  BottleNo = aag.BottleNo,
                                  PreservationDone = aag.PreservationDone,
                                  Vs = aag.Vs,

                              }).ToList();

            airInfo.AirInfos = GridResult;

            var GridResult24 = (from aam in _dbContext.FieldAmbientAirMonitorings
                              join aag in _dbContext.FieldAmbientAirGasesSamplings on aam.FieldId equals aag.FieldId
                              where aam.FieldId == FieldId & aag.Testing == true
                                select new AmbientAirMonitoringEntity
                              {
                                  FieldId = (Int32)aag.FieldId,
                                  GasesSampledId = aag.GasesSampledId,
                                  GasesSampled = aag.GasesSampled,
                                  VolImpingingSolution = aag.VolImpingingSolution,
                                  RotaMeterFlow = aag.RotaMeterFlow,
                                  ShiftNoGases = (Int32)aag.ShiftNoGases,
                                  Duration = aag.Duration,
                                  BottleNo = aag.BottleNo,
                                  PreservationDone = aag.PreservationDone,
                                  Vs = aag.Vs,

                              }).ToList();

            airInfo.AirInfos24Hr = GridResult24;
            return airInfo;

        }
        public long AddAmbientAir(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            try
            {
               
                    if (ambientAirMonitoringEntity.FieldId == 0)
                    {
                        FieldAmbientAirMonitoring fieldAmbientAir = new FieldAmbientAirMonitoring();
                        fieldAmbientAir.SampleCollectionId = ambientAirMonitoringEntity.SampleCollectionId;
                        fieldAmbientAir.EnquiryId = ambientAirMonitoringEntity.EnquiryId;
                        fieldAmbientAir.WindVelocity = ambientAirMonitoringEntity.WindVelocity;
                        fieldAmbientAir.InstrumentId = ambientAirMonitoringEntity.InstrumentId;
                        fieldAmbientAir.SamplingDuration = ambientAirMonitoringEntity.SamplingDuration;
                        fieldAmbientAir.WindDirection_ = ambientAirMonitoringEntity.WindDirection_;
                        fieldAmbientAir.AverageWindVelocity = ambientAirMonitoringEntity.AverageWindVelocity;
                        fieldAmbientAir.RelativeHumidity = ambientAirMonitoringEntity.RelativeHumidity;
                        fieldAmbientAir.Temperature = ambientAirMonitoringEntity.Temperature;
                        fieldAmbientAir.Temperature24Hr = ambientAirMonitoringEntity.Temperature24Hr;
                        fieldAmbientAir.RelativeHumidity24Hr = ambientAirMonitoringEntity.RelativeHumidity24Hr;
                        fieldAmbientAir.StatusId = ambientAirMonitoringEntity.StatusId;
                        fieldAmbientAir.IsActive = ambientAirMonitoringEntity.IsActive;
                        fieldAmbientAir.EnteredBy = ambientAirMonitoringEntity.EnteredBy;
                        fieldAmbientAir.MatterSamplingDuration = ambientAirMonitoringEntity.MatterSamplingDuration;
                        fieldAmbientAir.FilterPaperNo2 = ambientAirMonitoringEntity.FilterPaperNo2;
                        fieldAmbientAir.AvgFlowRate = ambientAirMonitoringEntity.AvgFlowRate;
                        fieldAmbientAir.StartTime = ambientAirMonitoringEntity.StartTime;
                        fieldAmbientAir.EndTime = ambientAirMonitoringEntity.EndTime;
                        fieldAmbientAir.TotalVolAirPassed_L = ambientAirMonitoringEntity.TotalVolAirPassed_L;
                        fieldAmbientAir.TotalVolumeAirPassed_m3 = ambientAirMonitoringEntity.TotalVolumeAirPassed_m3;
                        fieldAmbientAir.EnteredDate = DateTime.UtcNow;

                        _dbContext.FieldAmbientAirMonitorings.Add(fieldAmbientAir);
                        _dbContext.SaveChanges();
                        ambientAirMonitoringEntity.FieldId = fieldAmbientAir.FieldId;

                    }
                else
                {
                    var ambientAir = _dbContext.FieldAmbientAirMonitorings.Find(ambientAirMonitoringEntity.FieldId);
                    ambientAir.WindVelocity = ambientAirMonitoringEntity.WindVelocity;
                    ambientAir.InstrumentId = ambientAirMonitoringEntity.InstrumentId;
                    ambientAir.SamplingDuration = ambientAirMonitoringEntity.SamplingDuration;
                    ambientAir.WindDirection_ = ambientAirMonitoringEntity.WindDirection_;
                    ambientAir.AverageWindVelocity = ambientAirMonitoringEntity.AverageWindVelocity;
                    ambientAir.RelativeHumidity = ambientAirMonitoringEntity.RelativeHumidity;
                    ambientAir.Temperature = ambientAirMonitoringEntity.Temperature;
                    ambientAir.MatterSamplingDuration = ambientAirMonitoringEntity.MatterSamplingDuration;
                    ambientAir.Temperature24Hr = ambientAirMonitoringEntity.Temperature24Hr;
                    ambientAir.FilterPaperNo2 = ambientAirMonitoringEntity.FilterPaperNo2;
                    ambientAir.RelativeHumidity24Hr = ambientAirMonitoringEntity.RelativeHumidity24Hr;
                    ambientAir.AvgFlowRate = ambientAirMonitoringEntity.AvgFlowRate;
                    ambientAir.StartTime = ambientAirMonitoringEntity.StartTime;
                    ambientAir.EndTime = ambientAirMonitoringEntity.EndTime;
                    ambientAir.TotalVolAirPassed_L = ambientAirMonitoringEntity.TotalVolAirPassed_L;
                    ambientAir.TotalVolumeAirPassed_m3 = ambientAirMonitoringEntity.TotalVolumeAirPassed_m3;
                    ambientAir.StatusId = ambientAirMonitoringEntity.StatusId;
                    _dbContext.SaveChanges();

                  
                }
                var sampleColl = _dbContext.SampleCollections.Find(ambientAirMonitoringEntity.SampleCollectionId);
                sampleColl.FieldStatusCode = "SampleColl";
                _dbContext.SaveChanges();
                return ambientAirMonitoringEntity.FieldId;
            }
            catch (Exception ex)
            {
                //return ex.InnerException.Message;
                return 0;
            }
        }

        public long AddAmbientAir24Hr(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            try
            {

                _dbContext.FieldAmbientAirGasesSamplings.Add(new FieldAmbientAirGasesSampling()
                {
                    FieldId = ambientAirMonitoringEntity.FieldId,
                    ParameterMappingId = (Int32)ambientAirMonitoringEntity.ParameterMappingId,
                    GasesSampled = ambientAirMonitoringEntity.GasesSampled,
                    BottleNo = ambientAirMonitoringEntity.BottleNo,
                    RotaMeterFlow = ambientAirMonitoringEntity.RotaMeterFlow,
                    VolImpingingSolution = ambientAirMonitoringEntity.VolImpingingSolution,
                    ShiftNoGases = ambientAirMonitoringEntity.ShiftNoGases,
                    Vs = ambientAirMonitoringEntity.Vs,
                    Testing = ambientAirMonitoringEntity.Testing,
                    PreservationDone = ambientAirMonitoringEntity.PreservationDone,
                    Duration = ambientAirMonitoringEntity.Duration,
                    IsActive = ambientAirMonitoringEntity.IsActive,
                    EnteredBy = ambientAirMonitoringEntity.EnteredBy,
                    EnteredDate = ambientAirMonitoringEntity.EnteredDate,
                });
                _dbContext.SaveChanges();
                return ambientAirMonitoringEntity.GasesSampledId;
            }
            catch (Exception ex)
            {
                //return ex.InnerException.Message;
                return 0;
            }
        }

        public long AddAmbientShiftWiseData(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            try
            {
                var ambientAir = _dbContext.FieldAmbientAirMonitorings.Where(e => e.SampleCollectionId == ambientAirMonitoringEntity.SampleCollectionId).FirstOrDefault();
                //var ambientAir = _dbContext.FieldAmbientAirMonitorings.Find(ambientAirMonitoringEntity.SampleCollectionId);
                FieldAmbientAirMatterSize fieldAmbientAirMatterSize = new FieldAmbientAirMatterSize();
                fieldAmbientAirMatterSize.FieldId = ambientAir.FieldId;
                fieldAmbientAirMatterSize.MatterSize = ambientAirMonitoringEntity.MatterSize;
                fieldAmbientAirMatterSize.ShiftNo = ambientAirMonitoringEntity.ShiftNo;
                fieldAmbientAirMatterSize.MatterSizeStartTime = ambientAirMonitoringEntity.MatterSizeStartTime;
                fieldAmbientAirMatterSize.MatterSizeEndTime = ambientAirMonitoringEntity.MatterSizeEndTime;
                fieldAmbientAirMatterSize.FilterPaperNo = ambientAirMonitoringEntity.FilterPaperNo;
                fieldAmbientAirMatterSize.CycloneCupNo = ambientAirMonitoringEntity.CycloneCupNo;
                fieldAmbientAirMatterSize.FlowRateDiff = ambientAirMonitoringEntity.FlowRateDiff;
                fieldAmbientAirMatterSize.FlowRateFinal = ambientAirMonitoringEntity.FlowRateFinal;
                fieldAmbientAirMatterSize.FlowRateInitial = ambientAirMonitoringEntity.FlowRateInitial;
                fieldAmbientAirMatterSize.TimeTotalizerDiff = ambientAirMonitoringEntity.TimeTotalizerDiff;
                fieldAmbientAirMatterSize.TimeTotalizerFinal = ambientAirMonitoringEntity.TimeTotalizerFinal;
                fieldAmbientAirMatterSize.TimeTotalizerInitial = ambientAirMonitoringEntity.TimeTotalizerInitial;
                fieldAmbientAirMatterSize.SamplingDurationDiff = ambientAirMonitoringEntity.SamplingDurationDiff;
                fieldAmbientAirMatterSize.SamplingDurationFinal = ambientAirMonitoringEntity.SamplingDurationFinal;
                fieldAmbientAirMatterSize.SamplingDurationInitial = ambientAirMonitoringEntity.SamplingDurationInitial;
                fieldAmbientAirMatterSize.IsActive = ambientAirMonitoringEntity.IsActive;
                fieldAmbientAirMatterSize.EnteredBy = ambientAirMonitoringEntity.EnteredBy;
                fieldAmbientAirMatterSize.EnteredDate = DateTime.UtcNow; ;
                _dbContext.FieldAmbientAirMatterSizes.Add(fieldAmbientAirMatterSize);
                _dbContext.SaveChanges();
                      
                return ambientAirMonitoringEntity.FieldId;
            }
            catch (Exception ex)
            {
                //return ex.InnerException.Message;
                return 0;
            }
        }

        public string DeleteAmbientAirField(long FieldId, long MatterSizeId,long GasesSampledId)
        {
            try
            {
                _dbContext.FieldAmbientAirGasesSamplings.Remove(_dbContext.FieldAmbientAirGasesSamplings.Find(GasesSampledId));//Complete Deleted from DB
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public FDStackInfo GetStackDetails(int FDStackEmissionId)
        {
            throw new NotImplementedException("Not Implemented");
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
        public string AddFoodProducts(FoodAndAgriProductsEntity foodAndAgriProductsEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }

        public long AddWorkplace(WorkplaceAndFugitiveEmissionEntity workplaceAndFugitiveEmissionEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }

        public long AddNoiseLevel(NoiseLevelMonitoringEntity noiseLevelMonitoringEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public long AddStackEmission(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public SolidWasteSoilOilEntity GetSolidDetails(int SolidHazardousWasteSoilOilId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public FoodAndAgriProductsEntity GetFoodDetails(int FieldFoodAndAgriCultureId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        FDMicrobiologicalInfo IFieldDetermination.GetAirMonitoringDetails(int MicrobiologicalID)
        {
            throw new NotImplementedException();
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

        public NoiseLevelMonitoringEntity.FDNoiseInfo GetNoiseLevelDetails(int FieldNoiseId)
        {
            throw new NotImplementedException();
        }

        public FDSolidWasteInfo GetSolidDetails(int SolidHazardousWasteSoilOilId, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }

        public FDFoodInfo GetFoodDetails(int FieldFoodAndAgriCultureId, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }

        public FDWorkplaceInfo GetWorkPlaceDetails(int WorkplaceID, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }

        public NoiseLevelMonitoringEntity.FDNoiseInfo GetNoiseLevelDetails(int FieldNoiseId, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }

        public FDBuildingInfo GetBuildingMaterialDetails(int FieldBuildingMaterialId, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }

        public FDWaterInfo GetWasteWaterDetails(int WasteWaterID, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }

        public FDCoalCokeInfo GetCoalDetails(int FieldId, int SampleCollectionId)
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
