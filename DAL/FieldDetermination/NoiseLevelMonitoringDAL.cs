using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.FieldDetermination;
using static LIMS_DEMO.Core.FieldDetermination.NoiseLevelMonitoringEntity;

namespace LIMS_DEMO.DAL.FieldDetermination
{
    public class NoiseLevelMonitoringDAL : IFieldDetermination
    {
        readonly LIMSEntities _dbContext;
        public NoiseLevelMonitoringDAL()
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

        public FoodAndAgriProductsEntity GetFoodDetails(int FieldFoodAndAgriCultureId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public MicrobiologicalMonitoringOfAirEntity GetAirMonitoringDetails(int MicrobiologicalID)
        {
            throw new NotImplementedException();
        }
        public long AddAmbientAir(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public FDAirInfo GetAirDetails(int FieldId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public long AddNoiseLevel(NoiseLevelMonitoringEntity noiseLevelMonitoringEntity)
        {
            try
            {
                if (noiseLevelMonitoringEntity.SampleTypeName == "AmbientWorkplaceNoise")
                {
                    if (noiseLevelMonitoringEntity.ParameterId == 0)
                    {
                        if (noiseLevelMonitoringEntity.FieldNoiseId == 0)
                        {
                            FieldNoiseLevelMonitoring fieldNoiseLevelMonitoring = new FieldNoiseLevelMonitoring();
                            fieldNoiseLevelMonitoring.FieldNoiseId = noiseLevelMonitoringEntity.FieldNoiseId;
                            fieldNoiseLevelMonitoring.SampleCollectionId = noiseLevelMonitoringEntity.SampleCollectionId;
                            fieldNoiseLevelMonitoring.EnquiryId = noiseLevelMonitoringEntity.EnquiryId;
                            fieldNoiseLevelMonitoring.StatusId = noiseLevelMonitoringEntity.StatusId;
                            fieldNoiseLevelMonitoring.Location = noiseLevelMonitoringEntity.Location;
                            fieldNoiseLevelMonitoring.LeqAvgDayTime = noiseLevelMonitoringEntity.LeqAvgDayTime;
                            fieldNoiseLevelMonitoring.LeqAvgNightTime = noiseLevelMonitoringEntity.LeqAvgNightTime;
                            fieldNoiseLevelMonitoring.L10AvgDayTime = noiseLevelMonitoringEntity.L10AvgDayTime;
                            fieldNoiseLevelMonitoring.L10AvgNightTime = noiseLevelMonitoringEntity.L10AvgNightTime;
                            fieldNoiseLevelMonitoring.L50AvgDayTime = noiseLevelMonitoringEntity.L50AvgDayTime;
                            fieldNoiseLevelMonitoring.L50AvgNightTime = noiseLevelMonitoringEntity.L50AvgNightTime;
                            fieldNoiseLevelMonitoring.L90AvgDayTime = noiseLevelMonitoringEntity.L90AvgDayTime;
                            fieldNoiseLevelMonitoring.L90AvgNightTime = noiseLevelMonitoringEntity.L90AvgNightTime;
                            fieldNoiseLevelMonitoring.LminAvgDayTime = noiseLevelMonitoringEntity.LminAvgDayTime;
                            fieldNoiseLevelMonitoring.LminAvgNightTime = noiseLevelMonitoringEntity.LminAvgNightTime;
                            fieldNoiseLevelMonitoring.LmaxAvgDayTime = noiseLevelMonitoringEntity.LmaxAvgDayTime;
                            fieldNoiseLevelMonitoring.LmaxAvgNightTime = noiseLevelMonitoringEntity.LmaxAvgNightTime;
                            fieldNoiseLevelMonitoring.MonitoringDate = noiseLevelMonitoringEntity.MonitoringDate;
                            fieldNoiseLevelMonitoring.MonitoringTime = noiseLevelMonitoringEntity.MonitoringTime;
                            fieldNoiseLevelMonitoring.LocationArea = noiseLevelMonitoringEntity.LocationZone;
                            fieldNoiseLevelMonitoring.LocationHeight = noiseLevelMonitoringEntity.LocationHeight;
                            fieldNoiseLevelMonitoring.InstrumentMake = noiseLevelMonitoringEntity.InstrumentMake;
                            fieldNoiseLevelMonitoring.InstrumentId = noiseLevelMonitoringEntity.InstrumentID;
                            fieldNoiseLevelMonitoring.InstrumentType = noiseLevelMonitoringEntity.InstrumentModel;
                            fieldNoiseLevelMonitoring.HeightOfMicroPhone = noiseLevelMonitoringEntity.MicrophoneHeight;
                            fieldNoiseLevelMonitoring.ObstacleIfAny = noiseLevelMonitoringEntity.Obstacles;
                            fieldNoiseLevelMonitoring.WindType = noiseLevelMonitoringEntity.WindType;
                            fieldNoiseLevelMonitoring.IsolationN = noiseLevelMonitoringEntity.Isolation;
                            fieldNoiseLevelMonitoring.DistancefrmSource = noiseLevelMonitoringEntity.DistancefrmSource;
                            fieldNoiseLevelMonitoring.ProminentNoiseSource = noiseLevelMonitoringEntity.ProminentNoiseSource;
                            fieldNoiseLevelMonitoring.BackgroundNoiselevel = noiseLevelMonitoringEntity.BackgroundNoiselevel;
                            fieldNoiseLevelMonitoring.IsActive = noiseLevelMonitoringEntity.IsActive;
                            fieldNoiseLevelMonitoring.EnteredBy = noiseLevelMonitoringEntity.EnteredBy;
                            fieldNoiseLevelMonitoring.EnteredDate = noiseLevelMonitoringEntity.EnteredDate;
                            _dbContext.FieldNoiseLevelMonitorings.Add(fieldNoiseLevelMonitoring);
                            _dbContext.SaveChanges();
                            noiseLevelMonitoringEntity.FieldNoiseId = fieldNoiseLevelMonitoring.FieldNoiseId;
                        }
                        _dbContext.FieldNoiseLevelMonitoringParameters.Add(new FieldNoiseLevelMonitoringParameter()
                        {
                            ParameterId = noiseLevelMonitoringEntity.ParameterId,
                            FieldNoiseId = noiseLevelMonitoringEntity.FieldNoiseId,
                            FileName = noiseLevelMonitoringEntity.FileName,
                            TimeDuration = noiseLevelMonitoringEntity.TimeDuration,
                            DayNight = noiseLevelMonitoringEntity.DayNight,
                            ShiftName = noiseLevelMonitoringEntity.ShiftName,
                            LocationGrid = noiseLevelMonitoringEntity.LocationGrid,
                            ParameterName = noiseLevelMonitoringEntity.ParameterName,
                            TestMethodName = noiseLevelMonitoringEntity.TestMethodName,
                            LeqFastResponse = noiseLevelMonitoringEntity.LeqFastResponse,
                            LeqSlowResponse = noiseLevelMonitoringEntity.LeqSlowResponse,
                            L10FastResponse = noiseLevelMonitoringEntity.L10FastResponse,
                            L10SlowResponse = noiseLevelMonitoringEntity.L10SlowResponse,
                            L50FastResponse = noiseLevelMonitoringEntity.L50FastResponse,
                            L50SlowResponse = noiseLevelMonitoringEntity.L50SlowResponse,
                            L90FastResponse = noiseLevelMonitoringEntity.L90FastResponse,
                            L90SlowResponse = noiseLevelMonitoringEntity.L90SlowResponse,
                            LminFastResponse = noiseLevelMonitoringEntity.LminFastResponse,
                            LminSlowResponse = noiseLevelMonitoringEntity.LminSlowResponse,
                            LmaxFastResponse = noiseLevelMonitoringEntity.LmaxFastResponse,
                            LmaxSlowResponse = noiseLevelMonitoringEntity.LmaxSlowResponse,
                            IsActive = noiseLevelMonitoringEntity.IsActive,
                            EnteredBy = noiseLevelMonitoringEntity.EnteredBy,
                            EnteredDate = noiseLevelMonitoringEntity.EnteredDate,
                            InField = noiseLevelMonitoringEntity.InField,
                            IsNABLAccredited = noiseLevelMonitoringEntity.IsNABLAccredited,
                        });
                        _dbContext.SaveChanges();

                        var sampleColl = _dbContext.SampleCollections.Find(noiseLevelMonitoringEntity.SampleCollectionId);
                        sampleColl.FieldStatusCode = "SampleColl";
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        var noise = _dbContext.FieldNoiseLevelMonitorings.Find(noiseLevelMonitoringEntity.FieldNoiseId);
                        noise.StatusId = noiseLevelMonitoringEntity.StatusId;
                        noise.Location = noiseLevelMonitoringEntity.Location;
                        noise.LeqAvgDayTime = noiseLevelMonitoringEntity.LeqAvgDayTime;
                        noise.LeqAvgNightTime = noiseLevelMonitoringEntity.LeqAvgNightTime;
                        noise.L10AvgDayTime = noiseLevelMonitoringEntity.L10AvgDayTime;
                        noise.L10AvgNightTime = noiseLevelMonitoringEntity.L10AvgNightTime;
                        noise.L50AvgDayTime = noiseLevelMonitoringEntity.L50AvgDayTime;
                        noise.L50AvgNightTime = noiseLevelMonitoringEntity.L50AvgNightTime;
                        noise.L90AvgDayTime = noiseLevelMonitoringEntity.L90AvgDayTime;
                        noise.L90AvgNightTime = noiseLevelMonitoringEntity.L90AvgNightTime;
                        noise.LminAvgDayTime = noiseLevelMonitoringEntity.LminAvgDayTime;
                        noise.LminAvgNightTime = noiseLevelMonitoringEntity.LminAvgNightTime;
                        noise.LmaxAvgDayTime = noiseLevelMonitoringEntity.LmaxAvgDayTime;
                        noise.LmaxAvgNightTime = noiseLevelMonitoringEntity.LmaxAvgNightTime;
                        noise.MonitoringDate = noiseLevelMonitoringEntity.MonitoringDate;
                        noise.MonitoringTime = noiseLevelMonitoringEntity.MonitoringTime;
                        noise.LocationArea = noiseLevelMonitoringEntity.LocationZone;
                        noise.LocationHeight = noiseLevelMonitoringEntity.LocationHeight;
                        noise.InstrumentId = noiseLevelMonitoringEntity.InstrumentID;
                        noise.InstrumentMake = noiseLevelMonitoringEntity.InstrumentMake;
                        noise.InstrumentType = noiseLevelMonitoringEntity.InstrumentModel;
                        noise.HeightOfMicroPhone = noiseLevelMonitoringEntity.MicrophoneHeight;
                        noise.ObstacleIfAny = noiseLevelMonitoringEntity.Obstacles;
                        noise.WindType = noiseLevelMonitoringEntity.WindType;
                        noise.IsolationN = noiseLevelMonitoringEntity.Isolation;
                        noise.DistancefrmSource = noiseLevelMonitoringEntity.DistancefrmSource;
                        noise.ProminentNoiseSource = noiseLevelMonitoringEntity.ProminentNoiseSource;
                        noise.BackgroundNoiselevel = noiseLevelMonitoringEntity.BackgroundNoiselevel;
                        _dbContext.SaveChanges();

                        var noiselevel = _dbContext.FieldNoiseLevelMonitoringParameters.Find(noiseLevelMonitoringEntity.ParameterId);
                        noiselevel.FileName = noiseLevelMonitoringEntity.FileName;
                        noiselevel.TimeDuration = noiseLevelMonitoringEntity.TimeDuration;
                        noiselevel.DayNight = noiseLevelMonitoringEntity.DayNight;
                        noiselevel.ShiftName = noiseLevelMonitoringEntity.ShiftName;
                        noiselevel.LocationGrid = noiseLevelMonitoringEntity.LocationGrid;
                        noiselevel.LeqFastResponse = noiseLevelMonitoringEntity.LeqFastResponse;
                        noiselevel.LeqSlowResponse = noiseLevelMonitoringEntity.LeqSlowResponse;
                        noiselevel.L10FastResponse = noiseLevelMonitoringEntity.L10FastResponse;
                        noiselevel.L10SlowResponse = noiseLevelMonitoringEntity.L10SlowResponse;
                        noiselevel.L50FastResponse = noiseLevelMonitoringEntity.L50FastResponse;
                        noiselevel.L50SlowResponse = noiseLevelMonitoringEntity.L50SlowResponse;
                        noiselevel.L90FastResponse = noiseLevelMonitoringEntity.L90FastResponse;
                        noiselevel.L90SlowResponse = noiseLevelMonitoringEntity.L90SlowResponse;
                        noiselevel.LminFastResponse = noiseLevelMonitoringEntity.LminFastResponse;
                        noiselevel.LminSlowResponse = noiseLevelMonitoringEntity.LminSlowResponse;
                        noiselevel.LmaxFastResponse = noiseLevelMonitoringEntity.LmaxFastResponse;
                        noiselevel.LmaxSlowResponse = noiseLevelMonitoringEntity.LmaxSlowResponse;
                        noiselevel.ParameterName = noiseLevelMonitoringEntity.ParameterName;
                        noiselevel.TestMethodName = noiseLevelMonitoringEntity.TestMethodName;
                        noiselevel.InField = noiseLevelMonitoringEntity.InField;
                        noiselevel.IsNABLAccredited = noiseLevelMonitoringEntity.IsNABLAccredited;
                        _dbContext.SaveChanges();
                    }
                }
                else
                {
                    if (noiseLevelMonitoringEntity.NoiseSourceParameterId == 0)
                    {
                        if (noiseLevelMonitoringEntity.FieldNoiseId == 0)
                        {
                            FieldNoiseLevelMonitoring fieldNoiseLevelMonitoring = new FieldNoiseLevelMonitoring();
                            fieldNoiseLevelMonitoring.FieldNoiseId = noiseLevelMonitoringEntity.FieldNoiseId;
                            fieldNoiseLevelMonitoring.SampleCollectionId = noiseLevelMonitoringEntity.SampleCollectionId;
                            fieldNoiseLevelMonitoring.EnquiryId = noiseLevelMonitoringEntity.EnquiryId;
                            fieldNoiseLevelMonitoring.StatusId = noiseLevelMonitoringEntity.StatusId;
                            fieldNoiseLevelMonitoring.MonitoringDate = noiseLevelMonitoringEntity.MonitoringDate;
                            fieldNoiseLevelMonitoring.MonitoringTime = noiseLevelMonitoringEntity.MonitoringTime;
                            fieldNoiseLevelMonitoring.Location = noiseLevelMonitoringEntity.Location;
                            fieldNoiseLevelMonitoring.LeqAvgDayTime = noiseLevelMonitoringEntity.LeqAvgDayTime;
                            fieldNoiseLevelMonitoring.LeqAvgNightTime = noiseLevelMonitoringEntity.LeqAvgNightTime;
                            fieldNoiseLevelMonitoring.L10AvgDayTime = noiseLevelMonitoringEntity.L10AvgDayTime;
                            fieldNoiseLevelMonitoring.L10AvgNightTime = noiseLevelMonitoringEntity.L10AvgNightTime;
                            fieldNoiseLevelMonitoring.L50AvgDayTime = noiseLevelMonitoringEntity.L50AvgDayTime;
                            fieldNoiseLevelMonitoring.L50AvgNightTime = noiseLevelMonitoringEntity.L50AvgNightTime;
                            fieldNoiseLevelMonitoring.L90AvgDayTime = noiseLevelMonitoringEntity.L90AvgDayTime;
                            fieldNoiseLevelMonitoring.L90AvgNightTime = noiseLevelMonitoringEntity.L90AvgNightTime;
                            fieldNoiseLevelMonitoring.LminAvgDayTime = noiseLevelMonitoringEntity.LminAvgDayTime;
                            fieldNoiseLevelMonitoring.LminAvgNightTime = noiseLevelMonitoringEntity.LminAvgNightTime;
                            fieldNoiseLevelMonitoring.LmaxAvgDayTime = noiseLevelMonitoringEntity.LmaxAvgDayTime;
                            fieldNoiseLevelMonitoring.LmaxAvgNightTime = noiseLevelMonitoringEntity.LmaxAvgNightTime;
                            fieldNoiseLevelMonitoring.LocationArea = noiseLevelMonitoringEntity.LocationZone;
                            fieldNoiseLevelMonitoring.LocationHeight = noiseLevelMonitoringEntity.LocationHeight;
                            fieldNoiseLevelMonitoring.InstrumentMake = noiseLevelMonitoringEntity.InstrumentMake;
                            fieldNoiseLevelMonitoring.InstrumentId = noiseLevelMonitoringEntity.InstrumentID;
                            fieldNoiseLevelMonitoring.InstrumentType = noiseLevelMonitoringEntity.InstrumentModel;
                            fieldNoiseLevelMonitoring.HeightOfMicroPhone = noiseLevelMonitoringEntity.MicrophoneHeight;
                            fieldNoiseLevelMonitoring.ObstacleIfAny = noiseLevelMonitoringEntity.Obstacles;
                            fieldNoiseLevelMonitoring.WindType = noiseLevelMonitoringEntity.WindType;
                            fieldNoiseLevelMonitoring.IsolationN = noiseLevelMonitoringEntity.Isolation;
                            fieldNoiseLevelMonitoring.DistancefrmSource = noiseLevelMonitoringEntity.DistancefrmSource;
                            fieldNoiseLevelMonitoring.ProminentNoiseSource = noiseLevelMonitoringEntity.ProminentNoiseSource;
                            fieldNoiseLevelMonitoring.BackgroundNoiselevel = noiseLevelMonitoringEntity.BackgroundNoiselevel;
                            fieldNoiseLevelMonitoring.IsActive = noiseLevelMonitoringEntity.IsActive;
                            fieldNoiseLevelMonitoring.EnteredBy = noiseLevelMonitoringEntity.EnteredBy;
                            fieldNoiseLevelMonitoring.EnteredDate = noiseLevelMonitoringEntity.EnteredDate;
                            _dbContext.FieldNoiseLevelMonitorings.Add(fieldNoiseLevelMonitoring);
                            _dbContext.SaveChanges();

                            noiseLevelMonitoringEntity.FieldNoiseId = fieldNoiseLevelMonitoring.FieldNoiseId;
                        }
                        _dbContext.FieldNoiseSourceParameters.Add(new FieldNoiseSourceParameter()
                        {
                            NoiseSourceParameterId = noiseLevelMonitoringEntity.NoiseSourceParameterId,
                            FieldNoiseId = noiseLevelMonitoringEntity.FieldNoiseId,
                            TimeSource = noiseLevelMonitoringEntity.TimeSource,
                            DayNight = noiseLevelMonitoringEntity.DayNight,
                            LocationGrid = noiseLevelMonitoringEntity.LocationGrid,
                            ParameterName = noiseLevelMonitoringEntity.ParameterName,
                            TestMethodName = noiseLevelMonitoringEntity.TestMethodName,
                            UnitUsed= noiseLevelMonitoringEntity.UnitName,
                            Inside = noiseLevelMonitoringEntity.Inside,
                            Outside = noiseLevelMonitoringEntity.Outside,
                            InsertionLoss = noiseLevelMonitoringEntity.InsertionLoss,
                            ShiftName = noiseLevelMonitoringEntity.ShiftName,
                            IsActive = noiseLevelMonitoringEntity.IsActive,
                            EnteredBy = noiseLevelMonitoringEntity.EnteredBy,
                            EnteredDate = noiseLevelMonitoringEntity.EnteredDate,
                            InField = noiseLevelMonitoringEntity.InField,
                            IsNABLAccredited = noiseLevelMonitoringEntity.IsNABLAccredited,
                        });
                        _dbContext.SaveChanges();

                        var sampleColl = _dbContext.SampleCollections.Find(noiseLevelMonitoringEntity.SampleCollectionId);
                        sampleColl.FieldStatusCode = "SampleColl";
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        var noise = _dbContext.FieldNoiseLevelMonitorings.Find(noiseLevelMonitoringEntity.FieldNoiseId);
                        noise.StatusId = noiseLevelMonitoringEntity.StatusId;
                        noise.Location = noiseLevelMonitoringEntity.Location;
                        noise.LeqAvgDayTime = noiseLevelMonitoringEntity.LeqAvgDayTime;
                        noise.LeqAvgNightTime = noiseLevelMonitoringEntity.LeqAvgNightTime;
                        noise.L10AvgDayTime = noiseLevelMonitoringEntity.L10AvgDayTime;
                        noise.L10AvgNightTime = noiseLevelMonitoringEntity.L10AvgNightTime;
                        noise.L50AvgDayTime = noiseLevelMonitoringEntity.L50AvgDayTime;
                        noise.L50AvgNightTime = noiseLevelMonitoringEntity.L50AvgNightTime;
                        noise.L90AvgDayTime = noiseLevelMonitoringEntity.L90AvgDayTime;
                        noise.L90AvgNightTime = noiseLevelMonitoringEntity.L90AvgNightTime;
                        noise.LminAvgDayTime = noiseLevelMonitoringEntity.LminAvgDayTime;
                        noise.LminAvgNightTime = noiseLevelMonitoringEntity.LminAvgNightTime;
                        noise.LmaxAvgDayTime = noiseLevelMonitoringEntity.LmaxAvgDayTime;
                        noise.LmaxAvgNightTime = noiseLevelMonitoringEntity.LmaxAvgNightTime;
                        noise.MonitoringDate = noiseLevelMonitoringEntity.MonitoringDate;
                        noise.MonitoringTime = noiseLevelMonitoringEntity.MonitoringTime;
                        noise.LocationArea = noiseLevelMonitoringEntity.LocationZone;
                        noise.LocationHeight = noiseLevelMonitoringEntity.LocationHeight;
                        noise.InstrumentId = noiseLevelMonitoringEntity.InstrumentID;
                        noise.InstrumentMake = noiseLevelMonitoringEntity.InstrumentMake;
                        noise.InstrumentType = noiseLevelMonitoringEntity.InstrumentModel;
                        noise.HeightOfMicroPhone = noiseLevelMonitoringEntity.MicrophoneHeight;
                        noise.ObstacleIfAny = noiseLevelMonitoringEntity.Obstacles;
                        noise.WindType = noiseLevelMonitoringEntity.WindType;
                        noise.IsolationN = noiseLevelMonitoringEntity.Isolation;
                        noise.DistancefrmSource = noiseLevelMonitoringEntity.DistancefrmSource;
                        noise.ProminentNoiseSource = noiseLevelMonitoringEntity.ProminentNoiseSource;
                        noise.BackgroundNoiselevel = noiseLevelMonitoringEntity.BackgroundNoiselevel;
                        _dbContext.SaveChanges();

                        var noiselevel = _dbContext.FieldNoiseSourceParameters.Find(noiseLevelMonitoringEntity.ParameterId);
                        noiselevel.TimeSource = noiseLevelMonitoringEntity.TimeSource;
                        noiselevel.DayNight = noiseLevelMonitoringEntity.DayNight;
                        noiselevel.LocationGrid = noiseLevelMonitoringEntity.LocationGrid;
                        noiselevel.ParameterName = noiseLevelMonitoringEntity.ParameterName;
                        noiselevel.TestMethodName = noiseLevelMonitoringEntity.TestMethodName;
                        noiselevel.UnitUsed = noiseLevelMonitoringEntity.UnitName;
                        noiselevel.InField = noiseLevelMonitoringEntity.InField;
                        noiselevel.IsNABLAccredited = noiseLevelMonitoringEntity.IsNABLAccredited;
                        noiselevel.Inside = noiseLevelMonitoringEntity.Inside;
                        noiselevel.Outside = noiseLevelMonitoringEntity.Outside;
                        noiselevel.InsertionLoss = noiseLevelMonitoringEntity.InsertionLoss;
                        noiselevel.ShiftName = noiseLevelMonitoringEntity.ShiftName;
                        _dbContext.SaveChanges();
                    }
                }
               
                return noiseLevelMonitoringEntity.FieldNoiseId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public FDNoiseInfo GetNoiseLevelDetails(int FieldNoiseId, int SampleCollectionId)
        {
            var result = (from fnm in _dbContext.FieldNoiseLevelMonitorings
                          where fnm.FieldNoiseId == FieldNoiseId
                          select new NoiseLevelMonitoringEntity()
                          {
                              FieldNoiseId = fnm.FieldNoiseId,
                              SampleCollectionId = (Int32)fnm.SampleCollectionId,
                              EnquiryId = (Int32)fnm.EnquiryId,
                              StatusId = fnm.StatusId,
                              CurrentStatus = fnm.StatusMaster.StatusCode,
                              LeqAvgDayTime = fnm.LeqAvgDayTime,
                              LeqAvgNightTime = fnm.LeqAvgNightTime,
                              L10AvgDayTime = fnm.L10AvgDayTime,
                              L10AvgNightTime = fnm.L10AvgNightTime,
                              L50AvgDayTime = fnm.L50AvgDayTime,
                              L50AvgNightTime = fnm.L50AvgNightTime,
                              L90AvgDayTime = fnm.L90AvgDayTime,
                              L90AvgNightTime = fnm.L90AvgNightTime,
                              LminAvgDayTime = fnm.LminAvgDayTime,
                              LminAvgNightTime = fnm.LminAvgNightTime,
                              LmaxAvgDayTime = fnm.LmaxAvgDayTime,
                              LmaxAvgNightTime = fnm.LmaxAvgNightTime,
                              DistancefrmSource = fnm.DistancefrmSource,
                              ProminentNoiseSource = fnm.ProminentNoiseSource,
                              BackgroundNoiselevel = fnm.BackgroundNoiselevel,
                              MonitoringDate =fnm.MonitoringDate,
                              MonitoringTime = fnm.MonitoringTime,
                              Location = fnm.Location,
                              LocationHeight = fnm.LocationHeight,
                              LocationZone = fnm.LocationArea,
                              InstrumentID = fnm.InstrumentId,
                              InstrumentMake = fnm.InstrumentMake,
                              InstrumentModel = fnm.InstrumentType,
                              MicrophoneHeight = fnm.HeightOfMicroPhone,
                              Obstacles = fnm.ObstacleIfAny,
                              WindType = fnm.WindType,
                              Isolation = fnm.IsolationN,
                        }).FirstOrDefault();

            FDNoiseInfo noiseInfo = new FDNoiseInfo();
            noiseInfo.NoiseDetails = result;

            var GridResult = (from fnm in _dbContext.FieldNoiseLevelMonitorings
                              join fnmp in _dbContext.FieldNoiseLevelMonitoringParameters on fnm.FieldNoiseId equals fnmp.FieldNoiseId
                              where fnmp.FieldNoiseId == FieldNoiseId
                              select new NoiseLevelMonitoringEntity
                              {
                                  FieldNoiseId = (Int32)fnmp.FieldNoiseId,
                                  ParameterId = fnmp.ParameterId,
                                  TimeDuration = fnmp.TimeDuration,
                                  FileName = fnmp.FileName,
                                  DayNight = fnmp.DayNight,
                                  InField = fnmp.InField,
                                  ShiftName = fnmp.ShiftName,
                                  LocationGrid = fnmp.LocationGrid,
                                  ParameterName = fnmp.ParameterName,
                                  TestMethodName = fnmp.TestMethodName,
                                  IsNABLAccredited = fnmp.IsNABLAccredited,
                                  LeqFastResponse = fnmp.LeqFastResponse,
                                  LeqSlowResponse = fnmp.LeqSlowResponse,
                                  L10FastResponse = fnmp.L10FastResponse,
                                  L10SlowResponse = fnmp.L10SlowResponse,
                                  L50FastResponse = fnmp.L50FastResponse,
                                  L50SlowResponse = fnmp.L50SlowResponse,
                                  L90FastResponse = fnmp.L90FastResponse,
                                  L90SlowResponse = fnmp.L90SlowResponse,
                                  LminFastResponse = fnmp.LminFastResponse,
                                  LminSlowResponse = fnmp.LminSlowResponse,
                                  LmaxFastResponse = fnmp.LmaxFastResponse,
                                  LmaxSlowResponse = fnmp.LmaxSlowResponse,
                              }).ToList();

            noiseInfo.NoiseInfos = GridResult;

            var GridSourceResult = (from fnm in _dbContext.FieldNoiseLevelMonitorings
                              join fnsp in _dbContext.FieldNoiseSourceParameters on fnm.FieldNoiseId equals fnsp.FieldNoiseId
                              where fnsp.FieldNoiseId == FieldNoiseId
                              select new NoiseLevelMonitoringEntity
                              {
                                  FieldNoiseId = (Int32)fnsp.FieldNoiseId,
                                  NoiseSourceParameterId = fnsp.NoiseSourceParameterId,
                                  TimeSource = fnsp.TimeSource,
                                  DayNight = fnsp.DayNight,
                                  ParameterName = fnsp.ParameterName,
                                  TestMethodName = fnsp.TestMethodName,
                                  IsNABLAccredited = fnsp.IsNABLAccredited,
                                  UnitName = fnsp.UnitUsed,
                                  InField = fnsp.InField,
                                  ShiftName = fnsp.ShiftName,
                                  LocationGrid = fnsp.LocationGrid,
                                  Inside = fnsp.Inside,
                                  Outside = fnsp.Outside,
                                  InsertionLoss = fnsp.InsertionLoss,
                              }).ToList();

            noiseInfo.SourceNoiseInfos = GridSourceResult;
            return noiseInfo;

        }
        public string DeleteNoiseLevelField(string SampleType,long FieldNoiseId, long ParameterId)
        {
            try
            {
                if (SampleType== "AmbientWorkplaceNoise")
                {
                    _dbContext.FieldNoiseLevelMonitoringParameters.Remove(_dbContext.FieldNoiseLevelMonitoringParameters.Find(ParameterId));//Complete Deleted from DB
                    _dbContext.SaveChanges();
                }
                if (SampleType== "SourceNoise")
                {
                    _dbContext.FieldNoiseSourceParameters.Remove(_dbContext.FieldNoiseSourceParameters.Find(ParameterId));//Complete Deleted from DB
                    _dbContext.SaveChanges();
                }
               
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

        //NoiseLevelMonitoringEntity IFieldDetermination.GetNoiseLevelDetails(int FieldNoiseId)
        //{
        //    throw new NotImplementedException();
        //}

        //public FDNoiseInfo GetNoiseInfo(int FieldNoiseId)
        //{
        //    throw new NotImplementedException();
        //}

        //FDNoiseInfo IFieldDetermination.GetNoiseLevelDetails(int FieldNoiseId)
        //{
        //    throw new NotImplementedException();
        //}

        public FDStackInfo GetStackDetails(int FDStackEmissionId)
        {
            throw new NotImplementedException();
        }

        string IFieldDetermination.Add(MicrobiologicalMonitoringOfAirEntity microbiologicalMonitoringOfAirEntity)
        {
            throw new NotImplementedException();
        }

        string IFieldDetermination.AddCoal(CoalCokeSolidFuelEntity coalCokeSolidFuelEntity)
        {
            throw new NotImplementedException();
        }

        string IFieldDetermination.AddWasteWater(WasteWaterEntity wasteWaterEntity)
        {
            throw new NotImplementedException();
        }

        List<WasteWaterEntity> IFieldDetermination.GetWaterList(int WasteWaterID)
        {
            throw new NotImplementedException();
        }

        FDStackInfo IFieldDetermination.GetStackDetails(int FDStackEmissionId)
        {
            throw new NotImplementedException();
        }



        //WasteWaterEntity IFieldDetermination.GetWasteWaterDetails(int WasteWaterID)
        //{
        //    throw new NotImplementedException();
        //}

        string IFieldDetermination.AddSolidWaste(SolidWasteSoilOilEntity solidWasteSoilOilEntity)
        {
            throw new NotImplementedException();
        }

        //CoalCokeSolidFuelEntity IFieldDetermination.GetCoalDetails(int FieldId)
        //{
        //    throw new NotImplementedException();
        //}

        string IFieldDetermination.AddBuildingMaterial(BuildingMaterialEntity buildingMaterialEntity)
        {
            throw new NotImplementedException();
        }

        string IFieldDetermination.AddFoodProducts(FoodAndAgriProductsEntity foodAndAgriProductsEntity)
        {
            throw new NotImplementedException();
        }

        long IFieldDetermination.AddWorkplace(WorkplaceAndFugitiveEmissionEntity workplaceAndFugitiveEmissionEntity)
        {
            throw new NotImplementedException();
        }

        long IFieldDetermination.AddAmbientAir(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            throw new NotImplementedException();
        }

        //long IFieldDetermination.AddNoiseLevel(NoiseLevelMonitoringEntity noiseLevelMonitoringEntity)
        //{
        //    throw new NotImplementedException();
        //}

        long IFieldDetermination.AddStackEmission(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
        FDWorkplaceInfo IFieldDetermination.GetWorkPlaceDetails(int WorkplaceID, int SampleCollectionId)
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

        public string  DeleteStackPara(long FDStackEmissionId, long FDStack_ParameterDataId)
        {
            throw new NotImplementedException();
        }

        //FDNoiseInfo IFieldDetermination.GetNoiseLevelDetails(int FieldNoiseId)
        //  {
        //      throw new NotImplementedException();
        //  }
    }
}
