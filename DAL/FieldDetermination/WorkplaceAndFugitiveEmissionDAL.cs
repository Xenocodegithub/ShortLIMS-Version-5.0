using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.FieldDetermination;

namespace LIMS_DEMO.DAL.FieldDetermination
{
    public class WorkplaceAndFugitiveEmissionDAL : IFieldDetermination
    {
        readonly LIMSEntities _dbContext;
        public WorkplaceAndFugitiveEmissionDAL()
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
        FDCoalCokeInfo IFieldDetermination.GetCoalDetails(int FieldId, int SampleCollectionId)
        {
            throw new NotImplementedException();
        }
        FDFoodInfo IFieldDetermination.GetFoodDetails(int FieldFoodAndAgriCultureId, int SampleCollectionId)
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
        //public BuildingMaterialEntity GetBUildingMaterialDetails(int FieldBuildingMaterialId)
        //{
        //    throw new NotImplementedException("Not Implemented");
        //}
        public NoiseLevelMonitoringEntity GetNoiseLevelDetails(int FieldNoiseId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public MicrobiologicalMonitoringOfAirEntity GetAirMonitoringDetails(int MicrobiologicalID)
        {
            throw new NotImplementedException();
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
        public long AddWorkplace(WorkplaceAndFugitiveEmissionEntity workplaceAndFugitiveEmissionEntity)
        {
            try
            {
                if(workplaceAndFugitiveEmissionEntity.WorkplaceGasID == 0)
                {
                    if(workplaceAndFugitiveEmissionEntity.WorkplaceID == 0)
                    {
                        FieldWorkplaceEnvironmentAndFugitiveEmission fieldworkplace = new FieldWorkplaceEnvironmentAndFugitiveEmission();
                        fieldworkplace.WorkplaceID = workplaceAndFugitiveEmissionEntity.WorkplaceID;
                        fieldworkplace.SampleCollectionId = workplaceAndFugitiveEmissionEntity.SampleCollectionId;
                        fieldworkplace.EnquiryId = workplaceAndFugitiveEmissionEntity.EnquiryId;
                        fieldworkplace.InstrumentId = workplaceAndFugitiveEmissionEntity.InstrumentId;
                        fieldworkplace.SurveyDate = workplaceAndFugitiveEmissionEntity.SurveyDate;
                        fieldworkplace.DurationOfSampling = workplaceAndFugitiveEmissionEntity.DurationOfSampling;
                        fieldworkplace.FilterPaperAnalyzed = workplaceAndFugitiveEmissionEntity.FilterPaperAnalyzed;
                        fieldworkplace.FilterPaperNo = workplaceAndFugitiveEmissionEntity.FilterPaperNo;
                        fieldworkplace.FlowRate_Avg = workplaceAndFugitiveEmissionEntity.FlowRate_Avg;
                        fieldworkplace.FlowRate_Final = workplaceAndFugitiveEmissionEntity.FlowRate_Final;
                        fieldworkplace.FlowRate_Initial = workplaceAndFugitiveEmissionEntity.FlowRate_Initial;
                        fieldworkplace.Sampling_Duration = workplaceAndFugitiveEmissionEntity.Sampling_Duration;
                        fieldworkplace.RelativeHumidity = workplaceAndFugitiveEmissionEntity.RelativeHumidity;
                        fieldworkplace.Temperature = workplaceAndFugitiveEmissionEntity.Temperature;
                        fieldworkplace.StatusId = workplaceAndFugitiveEmissionEntity.StatusId;
                        fieldworkplace.AnyObservation = workplaceAndFugitiveEmissionEntity.AnyObservation;
                        fieldworkplace.FilterPaperNo2 = workplaceAndFugitiveEmissionEntity.FilterPaperNo2;
                        fieldworkplace.StartTime = workplaceAndFugitiveEmissionEntity.StartTime;
                        fieldworkplace.EndTime = workplaceAndFugitiveEmissionEntity.EndTime;
                        fieldworkplace.CycloneCupNo = workplaceAndFugitiveEmissionEntity.CycloneCupNo;
                        fieldworkplace.SamplingDurationDiff = workplaceAndFugitiveEmissionEntity.SamplingDurationDiff;
                        fieldworkplace.SamplingDurationFinal = workplaceAndFugitiveEmissionEntity.SamplingDurationFinal;
                        fieldworkplace.SamplingDurationInitial = workplaceAndFugitiveEmissionEntity.SamplingDurationInitial;
                        fieldworkplace.FlowRateDiffM = workplaceAndFugitiveEmissionEntity.FlowRateDiffM;
                        fieldworkplace.FlowRateFinalM = workplaceAndFugitiveEmissionEntity.FlowRateFinalM;
                        fieldworkplace.FlowRateInitialM = workplaceAndFugitiveEmissionEntity.FlowRateInitialM;
                        fieldworkplace.TimeTotalizerFinal = workplaceAndFugitiveEmissionEntity.TimeTotalizerFinal;
                        fieldworkplace.TimeTotalizerInitial = workplaceAndFugitiveEmissionEntity.TimeTotalizerInitial;
                        fieldworkplace.TimeTotalizerDiff = workplaceAndFugitiveEmissionEntity.TimeTotalizerDiff;
                        fieldworkplace.MatterSizeEndTime = workplaceAndFugitiveEmissionEntity.MatterSizeEndTime;
                        fieldworkplace.MatterSizeStartTime = workplaceAndFugitiveEmissionEntity.MatterSizeStartTime;

                        fieldworkplace.Sampling_Duration = workplaceAndFugitiveEmissionEntity.Sampling_Duration;
                        fieldworkplace.AvgFlowRate = workplaceAndFugitiveEmissionEntity.AvgFlowRate;
                        fieldworkplace.TotalVolAirPassed_L = workplaceAndFugitiveEmissionEntity.TotalVolAirPassed_L;
                        fieldworkplace.TotalVolumeAirPassed_m3 = workplaceAndFugitiveEmissionEntity.TotalVolumeAirPassed_m3;
                        fieldworkplace.IsActive = true;
                        fieldworkplace.EnteredBy = workplaceAndFugitiveEmissionEntity.EnteredBy;
                        fieldworkplace.EnteredDate = DateTime.UtcNow;

                        _dbContext.FieldWorkplaceEnvironmentAndFugitiveEmissions.Add(fieldworkplace);
                        _dbContext.SaveChanges();
                        workplaceAndFugitiveEmissionEntity.WorkplaceID = fieldworkplace.WorkplaceID;
                    }
                    _dbContext.FieldWorkplaceGasSamplings.Add(new FieldWorkplaceGasSampling()
                    {
                        WorkplaceID = workplaceAndFugitiveEmissionEntity.WorkplaceID,
                        WorkplaceGasID = workplaceAndFugitiveEmissionEntity.WorkplaceGasID,
                       //GasesSampled = workplaceAndFugitiveEmissionEntity.GasesSampled,
                        Parameters = workplaceAndFugitiveEmissionEntity.Parameters,
                        TestMethodName = workplaceAndFugitiveEmissionEntity.TestMethodName,
                        InField = workplaceAndFugitiveEmissionEntity.InField,
                        IsNABLAccredited = workplaceAndFugitiveEmissionEntity.IsNABLAccredited,
                        BottleNo = workplaceAndFugitiveEmissionEntity.BottleNo,
                        VolImpingingSol = workplaceAndFugitiveEmissionEntity.VolImpingingSol,
                        RotameterFlow = workplaceAndFugitiveEmissionEntity.RotameterFlow,
                        Duration = workplaceAndFugitiveEmissionEntity.Duration,
                        Vs = workplaceAndFugitiveEmissionEntity.Vs,
                        PreservationDone = workplaceAndFugitiveEmissionEntity.PreservationDone,
                        IsActive = workplaceAndFugitiveEmissionEntity.IsActive,
                        EnteredBy = workplaceAndFugitiveEmissionEntity.EnteredBy,
                        EnteredDate = workplaceAndFugitiveEmissionEntity.EnteredDate,
                    });
                    _dbContext.SaveChanges();

                    var sampleColl = _dbContext.SampleCollections.Find(workplaceAndFugitiveEmissionEntity.SampleCollectionId);
                    sampleColl.FieldStatusCode = "SampleColl";
                    //sampleColl.FieldDeterminationId = wasteWaterEntity.WasteWaterID;
                    _dbContext.SaveChanges();
                }
                else
                {
                    var workplace = _dbContext.FieldWorkplaceEnvironmentAndFugitiveEmissions.Find(workplaceAndFugitiveEmissionEntity.WorkplaceID);
                    workplace.InstrumentId = workplaceAndFugitiveEmissionEntity.InstrumentId;
                    workplace.SurveyDate = workplaceAndFugitiveEmissionEntity.SurveyDate;
                    workplace.DurationOfSampling = workplaceAndFugitiveEmissionEntity.DurationOfSampling;
                    workplace.FilterPaperAnalyzed = workplaceAndFugitiveEmissionEntity.FilterPaperAnalyzed;
                    workplace.FilterPaperNo = workplaceAndFugitiveEmissionEntity.FilterPaperNo;
                    workplace.FlowRate_Avg = workplaceAndFugitiveEmissionEntity.FlowRate_Avg;
                    workplace.FlowRate_Final = workplaceAndFugitiveEmissionEntity.FlowRate_Final;
                    workplace.FlowRate_Initial = workplaceAndFugitiveEmissionEntity.FlowRate_Initial;
                    workplace.Sampling_Duration = workplaceAndFugitiveEmissionEntity.Sampling_Duration;
                    workplace.RelativeHumidity = workplaceAndFugitiveEmissionEntity.RelativeHumidity;
                    workplace.Temperature = workplaceAndFugitiveEmissionEntity.Temperature;
                    workplace.StatusId = workplaceAndFugitiveEmissionEntity.StatusId;
                    workplace.AnyObservation = workplaceAndFugitiveEmissionEntity.AnyObservation;
                    workplace.FilterPaperNo2 = workplaceAndFugitiveEmissionEntity.FilterPaperNo2;
                    workplace.StartTime = workplaceAndFugitiveEmissionEntity.StartTime;
                    workplace.EndTime = workplaceAndFugitiveEmissionEntity.EndTime;
                    workplace.CycloneCupNo = workplaceAndFugitiveEmissionEntity.CycloneCupNo;
                    workplace.SamplingDurationDiff = workplaceAndFugitiveEmissionEntity.SamplingDurationDiff;
                    workplace.SamplingDurationFinal = workplaceAndFugitiveEmissionEntity.SamplingDurationFinal;
                    workplace.SamplingDurationInitial = workplaceAndFugitiveEmissionEntity.SamplingDurationInitial;
                    workplace.FlowRateDiffM = workplaceAndFugitiveEmissionEntity.FlowRateDiffM;
                    workplace.FlowRateFinalM = workplaceAndFugitiveEmissionEntity.FlowRateFinalM;
                    workplace.FlowRateInitialM = workplaceAndFugitiveEmissionEntity.FlowRateInitialM;
                    workplace.TimeTotalizerFinal = workplaceAndFugitiveEmissionEntity.TimeTotalizerFinal;
                    workplace.TimeTotalizerInitial = workplaceAndFugitiveEmissionEntity.TimeTotalizerInitial;
                    workplace.TimeTotalizerDiff = workplaceAndFugitiveEmissionEntity.TimeTotalizerDiff;
                    workplace.MatterSizeEndTime = workplaceAndFugitiveEmissionEntity.MatterSizeEndTime;
                    workplace.MatterSizeStartTime = workplaceAndFugitiveEmissionEntity.MatterSizeStartTime;

                    workplace.Sampling_Duration = workplaceAndFugitiveEmissionEntity.Sampling_Duration;
                    workplace.AvgFlowRate = workplaceAndFugitiveEmissionEntity.AvgFlowRate;
                    workplace.TotalVolAirPassed_L = workplaceAndFugitiveEmissionEntity.TotalVolAirPassed_L;
                    workplace.TotalVolumeAirPassed_m3 = workplaceAndFugitiveEmissionEntity.TotalVolumeAirPassed_m3;
                    _dbContext.SaveChanges();

                    var workplaceGas = _dbContext.FieldWorkplaceGasSamplings.Find(workplaceAndFugitiveEmissionEntity.WorkplaceGasID);
                    //workplaceGas.GasesSampled = workplaceAndFugitiveEmissionEntity.GasesSampled;
                    workplaceGas.Parameters = workplaceAndFugitiveEmissionEntity.Parameters;
                    workplaceGas.TestMethodName = workplaceAndFugitiveEmissionEntity.TestMethodName;
                    workplaceGas.InField = workplaceAndFugitiveEmissionEntity.InField;
                    workplaceGas.IsNABLAccredited = workplaceAndFugitiveEmissionEntity.IsNABLAccredited;
                    workplaceGas.BottleNo = workplaceAndFugitiveEmissionEntity.BottleNo;
                    workplaceGas.VolImpingingSol = workplaceAndFugitiveEmissionEntity.VolImpingingSol;
                    workplaceGas.RotameterFlow = workplaceAndFugitiveEmissionEntity.RotameterFlow;
                    workplaceGas.Duration = workplaceAndFugitiveEmissionEntity.Duration;
                    workplaceGas.Vs = workplaceAndFugitiveEmissionEntity.Vs;
                    workplaceGas.PreservationDone = workplaceAndFugitiveEmissionEntity.PreservationDone;
                    _dbContext.SaveChanges();
                }
                return workplaceAndFugitiveEmissionEntity.WorkplaceID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //public WorkplaceAndFugitiveEmissionEntity GetWorkPlaceDetails(int WorkplaceID)
        //{
        //    return (from fwf in _dbContext.FieldWorkplaceEnvironmentAndFugitiveEmissions
        //          //  join fwp in _dbContext.FieldWorkplaceGasSamplings on fwf.WorkplaceID equals fwp.WorkplaceID
        //           // into field
        //           // from f in field.DefaultIfEmpty()
        //            where fwf.WorkplaceID == WorkplaceID
        //            select new WorkplaceAndFugitiveEmissionEntity()
        //            {
        //                SampleCollectionId = fwf.SampleCollectionId,
        //                EnquiryId = fwf.EnquiryId,
        //                StatusId =fwf.StatusMaster.StatusId,
        //                InstrumentId = fwf.InstrumentId,
        //                SurveyDate = fwf.SurveyDate,
        //                DurationOfSampling = fwf.DurationOfSampling,
        //                FilterPaperAnalyzed = fwf.FilterPaperAnalyzed,
        //                FilterPaperNo = fwf.FilterPaperNo,
        //                FlowRate_Avg = fwf.FlowRate_Avg,
        //                FlowRate_Final = fwf.FlowRate_Final,
        //                FlowRate_Initial = fwf.FlowRate_Initial,
        //                Sampling_Duration = fwf.Sampling_Duration,
        //                RelativeHumidity = fwf.RelativeHumidity,
        //              Temperature = fwf.Temperature,
        //              AnyObservation = fwf.AnyObservation,
        //            }
        //           ).FirstOrDefault();
        //}

        public FDWorkplaceInfo GetWorkPlaceDetails(int WorkplaceID, int SampleCollectionId)
        {
            var result = (from fwf in _dbContext.FieldWorkplaceEnvironmentAndFugitiveEmissions
                          join sts in _dbContext.StatusMasters on fwf.StatusId equals sts.StatusId
                          //into field
                          //from f in field.DefaultIfEmpty()
                          where fwf.WorkplaceID == WorkplaceID
                          select new WorkplaceAndFugitiveEmissionEntity()
                          {
                              SampleCollectionId = fwf.SampleCollectionId,
                              EnquiryId = fwf.EnquiryId,
                              StatusId = fwf.StatusMaster.StatusId,
                              CurrentStatus = fwf.StatusMaster.StatusCode,
                              InstrumentId = fwf.InstrumentId,
                              SurveyDate = fwf.SurveyDate,
                              DurationOfSampling = fwf.DurationOfSampling,
                              FilterPaperAnalyzed = fwf.FilterPaperAnalyzed,
                              FilterPaperNo = fwf.FilterPaperNo,
                              FlowRate_Avg = fwf.FlowRate_Avg,
                              FlowRate_Final = fwf.FlowRate_Final,
                              FlowRate_Initial = fwf.FlowRate_Initial,
                              RelativeHumidity = fwf.RelativeHumidity,
                              Temperature = fwf.Temperature,
                              AnyObservation = fwf.AnyObservation,
                              FilterPaperNo2 = fwf.FilterPaperNo2,
                              StartTime = fwf.StartTime,
                              EndTime = fwf.EndTime,
                              Sampling_Duration = fwf.Sampling_Duration,
                              AvgFlowRate = fwf.AvgFlowRate,
                              TotalVolAirPassed_L = fwf.TotalVolAirPassed_L,
                              TotalVolumeAirPassed_m3 = fwf.TotalVolumeAirPassed_m3,
                          }
                   ).FirstOrDefault();

            FDWorkplaceInfo workplaceInfo = new FDWorkplaceInfo();
            workplaceInfo.WorkplaceDetails = result;
            var GridResult = (from fwf in _dbContext.FieldWorkplaceEnvironmentAndFugitiveEmissions
                              join fwp in _dbContext.FieldWorkplaceGasSamplings on fwf.WorkplaceID equals fwp.WorkplaceID
                              where fwp.WorkplaceID == WorkplaceID
                              select new WorkplaceAndFugitiveEmissionEntity
                              {
                                  WorkplaceID = (Int32)fwp.WorkplaceID,
                                  WorkplaceGasID = fwp.WorkplaceGasID,
                                  //GasesSampled = fwp.GasesSampled,
                                  Parameters = fwp.Parameters,
                                  TestMethodName = fwp.TestMethodName,
                                  InField = fwp.InField,
                                  IsNABLAccredited = fwp.IsNABLAccredited,
                                  VolImpingingSol = fwp.VolImpingingSol,
                                  Duration = fwp.Duration,
                                  BottleNo = fwp.BottleNo,
                                  RotameterFlow = fwp.RotameterFlow,
                                  PreservationDone = fwp.PreservationDone,
                                  Vs =fwp.Vs,
                              }).ToList();

            workplaceInfo.WorkplaceInfos = GridResult;
            return workplaceInfo;
        }
        
        public string DeleteWorkplaceField(long WorkplaceID,long WorkplaceGasID)
        {
            try
            {
                //_dbContext.FieldWorkplaceEnvironmentAndFugitiveEmissions.Remove(_dbContext.FieldWorkplaceEnvironmentAndFugitiveEmissions.Find(WorkplaceID));//Complete Deleted from DB
                //_dbContext.SaveChanges();
                _dbContext.FieldWorkplaceGasSamplings.Remove(_dbContext.FieldWorkplaceGasSamplings.Find(WorkplaceGasID));//Complete Deleted from DB
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
        public string AddFoodProducts(FoodAndAgriProductsEntity foodAndAgriProductsEntity)
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
        public long AddStackEmission(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
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
