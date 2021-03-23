using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.FieldDetermination;
using System.Security.Cryptography;

namespace LIMS_DEMO.DAL.FieldDetermination
{
    public class StackEmissionMonitoringDAL : IFieldDetermination
    {
        readonly LIMSEntities _dbContext;
        public StackEmissionMonitoringDAL()
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
        public FDAirInfo GetAirDetails(int FieldId)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public long AddAmbientAir(AmbientAirMonitoringEntity ambientAirMonitoringEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }

        public FDStackInfo GetStackDetails(int FDStackEmissionId)
        {
            var result = (from fss in _dbContext.FDStackEmissions
                          join sei in _dbContext.FDStackEmission_IsoKinetic on fss.FDStackEmissionId equals sei.FDStackEmissionId
                          where fss.FDStackEmissionId == FDStackEmissionId
                          select new StackEmissionMonitoringEntity
                          {
                              FDStackEmissionId = fss.FDStackEmissionId,
                              FDStackEmission_IsoKineticId = sei.FDStackEmission_IsoKineticId,
                              StatusId = fss.StatusId,
                              CurrentStatus =fss.StatusMaster.StatusCode,
                              InstrumentId = fss.InstrumentId,
                              BarometricPressure = fss.BarometricPressure,
                              FlueGasComposition_CO = fss.FlueGasComposition_CO,
                              FlueGasComposition_CO2 = fss.FlueGasComposition_CO2,
                              FlueGasComposition_N2 = fss.FlueGasComposition_N2,
                              FlueGasComposition_O2 = fss.FlueGasComposition_O2,
                              MoistureContent = fss.MoistureContent,
                              DurationOfBoiler = fss.DurationOfBoiler,
                              AmbientTemperature = fss.AmbientTemperature,
                              StackTemperature = fss.StackTemperature,
                              VelocityofStackGas = fss.VelocityofStackGas,
                              PitotTubefactor = fss.PitotTubefactor,
                              StaticPressure = fss.StaticPressure,
                              AbsoluteStackPressure = fss.AbsoluteStackPressure,
                              FlueGasQuantity = fss.FlueGasQuantity,
                              ThimbleNO = fss.ThimbleNO,
                              TotalGasPassed = fss.TotalGasPassed,
                              SamplingGasTemp_Average = fss.SamplingGasTemp_Average,
                              SamplingGasTemp_Final = fss.SamplingGasTemp_Final,
                              SamplingGasTemp_Initial = fss.SamplingGasTemp_Initial,
                              Vsc = fss.Vsc,
                              Bwo = fss.Bwo,
                              AmbientTempC = fss.AmbientTempC,
                              StackTempC = fss.StackTempC,
                              C = fss.C,
                              StackIdentity = fss.StackIdentity,
                              StackAttachedTo = fss.StackAttachedTo,
                              StackArea = fss.StackArea,
                              MaterialOfConstruction = fss.MaterialOfConstruction,
                              StackHeight = fss.StackHeight,
                              StackDiameter = fss.StackDiameter,
                              HeightOfPorthole = fss.HeightOfPorthole,
                              StackShapeTop = fss.StackShapeTop,
                              Fuelused_Type = fss.Fuelused_Type,
                              Fuelused_Consumption = fss.Fuelused_Consumption,
                              Is8DAnd2D = fss.Is8DAnd2D,
                              Samplingportandplatformexists = fss.Samplingportandplatformexists,
                              Airpollutioncontrolequipmentexists = fss.Airpollutioncontrolequipmentexists,
                              NozzleConstantArea = sei.NozzleConstantArea,
                              SamplingDuration = sei.SamplingDuration,
                              SamplingFlowRate = sei.SamplingFlowRate,
                              VacuumPv_Average = sei.VacuumPv_Average,
                              VacuumPv_Final = sei.VacuumPv_Final,
                              VacuumPv_Initial = sei.VacuumPv_Initial,
                              DryGasDiff = sei.DryGasDiff,
                              DryGasFinal = sei.DryGasFinal,
                              DryGasInitial = sei.DryGasInitial,
                              Velocity_V = fss.Velocity_V,
                              Vf = (Int32)fss.Vf,
                              Va = (Int32)fss.Va,
                              Pf = (Int32)fss.Pf,
                              Pi = (Int32)fss.Pi,
                              Tf = (Int32)fss.Tf,
                              Ti = (Int32)fss.Ti,
                              LoadAtMonitoring = fss.LoadAtMonitoring,
                              Tstd = (Int32)fss.Tstd,
                              Pstd = (Int32)fss.Pstd,
                              IsActive = fss.IsActive,
                          }).FirstOrDefault();

            FDStackInfo stackInfo = new FDStackInfo();
            stackInfo.StackDetails = result;
            var GridResult = (from fss in _dbContext.FDStackEmissions
                              join sik in _dbContext.FDStackEmission_IsoKinetic on fss.FDStackEmissionId equals sik.FDStackEmissionId
                              join fsd in _dbContext.FDStackEmission_GaseousData on fss.FDStackEmissionId equals fsd.FDStackEmissionId
                              where fsd.FDStackEmissionId == FDStackEmissionId
                              select new StackEmissionMonitoringEntity
                              {
                                  FDStackEmissionId = fsd.FDStackEmissionId,
                                  FDStackEmission_IsoKineticId = sik.FDStackEmission_IsoKineticId,
                                  FDStackEmission_GaseousDataId = fsd.FDStackEmission_GaseousDataId,
                                  FlowRate = fsd.FlowRate,
                                  Parameters = fsd.Parameters,
                                  TestMethodName = fsd.TestMethodName,
                                  InField = fsd.InField,
                                  IsNABLAccredited = fsd.IsNABLAccredited,
                                  SamplingTime = fsd.SamplingTime,
                                  GasTemp = fsd.GasTemp,
                                  BarometricPressure = fsd.BarometricPressure,
                                  DryGasMeterReading_Initial = fsd.DryGasMeterReading_Initial,
                                  DryGasMeterReading_Final = fsd.DryGasMeterReading_Final,
                                  DryGasMeterReading_Total = fsd.DryGasMeterReading_Total,
                                  BottleNo = fsd.BottleNo,
                                  AbsorbingSolutionUsed_Conc = fsd.AbsorbingSolutionUsed_Conc,
                                  AbsorbingSolutionUsed_solution = fsd.AbsorbingSolutionUsed_solution,
                                  AbsorbingSolutionUsed_Vol = fsd.AbsorbingSolutionUsed_Vol,
                                  PreservationDone = fsd.PreservationDone,
                                  Vs = fsd.Vs,
                              }).ToList();

            stackInfo.StackInfos = GridResult;

            var GridParaResult = (from fss in _dbContext.FDStackEmissions
                                  join fpd in _dbContext.FDStack_ParameterData on fss.FDStackEmissionId equals fpd.FDStackEmissionId
                                  where fpd.FDStackEmissionId == FDStackEmissionId
                                  select new StackEmissionMonitoringEntity
                                  {
                                      FDStackEmissionId = fpd.FDStackEmissionId,
                                      FDStack_ParameterDataId = fpd.FDStack_ParameterDataId,
                                      Parameters = fpd.Parameters,
                                      TestMethodName = fpd.TestMethodName,
                                      TestResults = fpd.TestResults,
                                      InField = fpd.InField,
                                      IsNABLAccredited = fpd.IsNABLAccredited,
                                  }).ToList();

            stackInfo.StackParaInfo = GridParaResult;
            return stackInfo;
        }
        public long AddStackParameter(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
        {
            try
            {
                _dbContext.FDStack_ParameterData.Add(new FDStack_ParameterData()
                {
                    FDStackEmissionId = stackEmissionMonitoringEntity.FDStackEmissionId,
                    Parameters = stackEmissionMonitoringEntity.Parameters,
                    TestMethodName = stackEmissionMonitoringEntity.TestMethodName,
                    InField = stackEmissionMonitoringEntity.InField,
                    IsNABLAccredited = stackEmissionMonitoringEntity.IsNABLAccredited,
                    TestResults = stackEmissionMonitoringEntity.TestResults,
                    IsActive = stackEmissionMonitoringEntity.IsActive,
                    EnteredBy = stackEmissionMonitoringEntity.EnteredBy,
                    EnteredDate = stackEmissionMonitoringEntity.EnteredDate,
                });
                _dbContext.SaveChanges();
                return stackEmissionMonitoringEntity.FDStack_ParameterDataId;
            }
            catch (Exception ex)
            {
                //return ex.InnerException.Message;
                return 0;
            }
        }
        public long AddStackEmission(StackEmissionMonitoringEntity stackEmissionMonitoringEntity)
        {
            try
            {
                if (stackEmissionMonitoringEntity.FDStackEmission_GaseousDataId == 0)
                {
                    if (stackEmissionMonitoringEntity.FDStackEmissionId == 0)
                    {
                        FDStackEmission fieldstack = new FDStackEmission();
                        fieldstack.FDStackEmissionId = stackEmissionMonitoringEntity.FDStackEmissionId;
                        fieldstack.SampleCollectionId = stackEmissionMonitoringEntity.SampleCollectionId;
                        fieldstack.EnquiryId = stackEmissionMonitoringEntity.EnquiryId;
                        fieldstack.InstrumentId = stackEmissionMonitoringEntity.InstrumentId;
                        fieldstack.StatusId = stackEmissionMonitoringEntity.StatusId;
                        fieldstack.Vsc = stackEmissionMonitoringEntity.Vsc;
                        fieldstack.StackAttachedTo = stackEmissionMonitoringEntity.StackAttachedTo;
                        fieldstack.StackArea = stackEmissionMonitoringEntity.StackArea;
                        fieldstack.StackIdentity = stackEmissionMonitoringEntity.StackIdentity;
                        fieldstack.StackHeight = stackEmissionMonitoringEntity.StackHeight;
                        fieldstack.DurationOfBoiler = stackEmissionMonitoringEntity.DurationOfBoiler;
                        fieldstack.StackDiameter = stackEmissionMonitoringEntity.StackDiameter;
                        fieldstack.HeightOfPorthole = stackEmissionMonitoringEntity.HeightOfPorthole;
                        fieldstack.Is8DAnd2D = stackEmissionMonitoringEntity.Is8DAnd2D;
                        fieldstack.Samplingportandplatformexists = stackEmissionMonitoringEntity.Samplingportandplatformexists;
                        fieldstack.Airpollutioncontrolequipmentexists = stackEmissionMonitoringEntity.Airpollutioncontrolequipmentexists;
                        fieldstack.MaterialOfConstruction = stackEmissionMonitoringEntity.MaterialOfConstruction;
                        fieldstack.StackShapeTop = stackEmissionMonitoringEntity.StackShapeTop;
                        fieldstack.Fuelused_Type = stackEmissionMonitoringEntity.Fuelused_Type;
                        fieldstack.Fuelused_Consumption = stackEmissionMonitoringEntity.Fuelused_Consumption;
                        fieldstack.BarometricPressure = stackEmissionMonitoringEntity.BarometricPressure;
                        fieldstack.FlueGasComposition_CO = stackEmissionMonitoringEntity.FlueGasComposition_CO;
                        fieldstack.FlueGasComposition_CO2 = stackEmissionMonitoringEntity.FlueGasComposition_CO2;
                        fieldstack.FlueGasComposition_N2 = stackEmissionMonitoringEntity.FlueGasComposition_N2;
                        fieldstack.FlueGasComposition_O2 = stackEmissionMonitoringEntity.FlueGasComposition_O2;
                        fieldstack.FlueGasQuantity = stackEmissionMonitoringEntity.FlueGasQuantity;
                        fieldstack.MoistureContent = stackEmissionMonitoringEntity.MoistureContent;
                        fieldstack.AmbientTemperature = stackEmissionMonitoringEntity.AmbientTemperature;
                        fieldstack.StackTemperature = stackEmissionMonitoringEntity.StackTemperature;
                        fieldstack.AmbientTempC = stackEmissionMonitoringEntity.AmbientTempC;
                        fieldstack.StackTempC = stackEmissionMonitoringEntity.StackTempC;
                        fieldstack.C = stackEmissionMonitoringEntity.C;
                        fieldstack.Bwo = stackEmissionMonitoringEntity.Bwo;
                        fieldstack.Velocity_V = stackEmissionMonitoringEntity.Velocity_V;
                        fieldstack.VelocityofStackGas = stackEmissionMonitoringEntity.VelocityofStackGas;
                        fieldstack.PitotTubefactor = stackEmissionMonitoringEntity.PitotTubefactor;
                        fieldstack.StaticPressure = stackEmissionMonitoringEntity.StaticPressure;
                        fieldstack.AbsoluteStackPressure = stackEmissionMonitoringEntity.AbsoluteStackPressure;
                        fieldstack.ThimbleNO = stackEmissionMonitoringEntity.ThimbleNO;
                        fieldstack.TotalGasPassed = stackEmissionMonitoringEntity.TotalGasPassed;
                        fieldstack.SamplingGasTemp_Average = stackEmissionMonitoringEntity.SamplingGasTemp_Average;
                        fieldstack.SamplingGasTemp_Final = stackEmissionMonitoringEntity.SamplingGasTemp_Final;
                        fieldstack.SamplingGasTemp_Initial = stackEmissionMonitoringEntity.SamplingGasTemp_Initial;
                        fieldstack.Velocity_V = stackEmissionMonitoringEntity.Velocity_V;
                        fieldstack.Vf = stackEmissionMonitoringEntity.Vf;
                        fieldstack.Va = stackEmissionMonitoringEntity.Va;
                        fieldstack.Pf = stackEmissionMonitoringEntity.Pf;
                        fieldstack.Pi = stackEmissionMonitoringEntity.Pi;
                        fieldstack.Ti = stackEmissionMonitoringEntity.Ti;
                        fieldstack.Tf = stackEmissionMonitoringEntity.Tf;
                        fieldstack.Pstd = stackEmissionMonitoringEntity.Pstd;
                        fieldstack.Tstd = stackEmissionMonitoringEntity.Tstd;
                        fieldstack.LoadAtMonitoring = stackEmissionMonitoringEntity.LoadAtMonitoring;
                        fieldstack.IsActive = stackEmissionMonitoringEntity.IsActive;
                        fieldstack.EnteredBy = stackEmissionMonitoringEntity.EnteredBy;
                        fieldstack.EnteredDate = stackEmissionMonitoringEntity.EnteredDate;
                        _dbContext.FDStackEmissions.Add(fieldstack);
                        _dbContext.SaveChanges();
                        stackEmissionMonitoringEntity.FDStackEmissionId = fieldstack.FDStackEmissionId;

                        _dbContext.FDStackEmission_IsoKinetic.Add(new FDStackEmission_IsoKinetic()
                        {
                            FDStackEmissionId = stackEmissionMonitoringEntity.FDStackEmissionId,
                           //FDStackEmission_IsoKineticId = stackEmissionMonitoringEntity.FDStackEmission_IsoKineticId,
                            SamplingDuration = stackEmissionMonitoringEntity.SamplingDuration,
                            SamplingFlowRate = stackEmissionMonitoringEntity.SamplingFlowRate,
                            VacuumPv_Average = stackEmissionMonitoringEntity.VacuumPv_Average,
                            VacuumPv_Final = stackEmissionMonitoringEntity.VacuumPv_Final,
                            VacuumPv_Initial = stackEmissionMonitoringEntity.VacuumPv_Initial,
                            DryGasFinal = stackEmissionMonitoringEntity.DryGasFinal,
                            DryGasDiff = stackEmissionMonitoringEntity.DryGasDiff,
                            DryGasInitial = stackEmissionMonitoringEntity.DryGasInitial,
                            NozzleConstantArea = stackEmissionMonitoringEntity.NozzleConstantArea,
                            IsActive = stackEmissionMonitoringEntity.IsActive,
                            EnteredBy = stackEmissionMonitoringEntity.EnteredBy,
                            EnteredDate = stackEmissionMonitoringEntity.EnteredDate,
                        });

                        _dbContext.SaveChanges();
                    }

                    _dbContext.FDStackEmission_GaseousData.Add(new FDStackEmission_GaseousData()
                    {
                        FDStackEmissionId = stackEmissionMonitoringEntity.FDStackEmissionId,
                       //FDStackEmission_GaseousDataId = stackEmissionMonitoringEntity.FDStackEmission_GaseousDataId,
                        FlowRate = stackEmissionMonitoringEntity.FlowRate,
                        BarometricPressure = stackEmissionMonitoringEntity.BarometricPressure,
                        SamplingTime = stackEmissionMonitoringEntity.SamplingTime,
                        GasTemp = stackEmissionMonitoringEntity.GasTemp,
                        DryGasMeterReading_Final = stackEmissionMonitoringEntity.DryGasMeterReading_Final,
                        DryGasMeterReading_Initial = stackEmissionMonitoringEntity.DryGasMeterReading_Initial,
                        DryGasMeterReading_Total = stackEmissionMonitoringEntity.DryGasMeterReading_Total,
                        BottleNo = stackEmissionMonitoringEntity.BottleNo,
                        AbsorbingSolutionUsed_Conc = stackEmissionMonitoringEntity.AbsorbingSolutionUsed_Conc,
                        AbsorbingSolutionUsed_solution = stackEmissionMonitoringEntity.AbsorbingSolutionUsed_solution,
                        AbsorbingSolutionUsed_Vol = stackEmissionMonitoringEntity.AbsorbingSolutionUsed_Vol,
                        PreservationDone = stackEmissionMonitoringEntity.PreservationDone,
                        Parameters = stackEmissionMonitoringEntity.Parameters,
                        TestMethodName = stackEmissionMonitoringEntity.TestMethodName,
                        InField = stackEmissionMonitoringEntity.InField,
                        IsNABLAccredited = stackEmissionMonitoringEntity.IsNABLAccredited,
                        Vs = stackEmissionMonitoringEntity.Vs,
                        IsActive = stackEmissionMonitoringEntity.IsActive,
                        EnteredBy = stackEmissionMonitoringEntity.EnteredBy,
                        EnteredDate = stackEmissionMonitoringEntity.EnteredDate,
                    });
                    _dbContext.SaveChanges();

                    var sampleColl = _dbContext.SampleCollections.Find(stackEmissionMonitoringEntity.SampleCollectionId);
                    sampleColl.FieldStatusCode = "SampleColl";
                    _dbContext.SaveChanges();
                }
                else
                {
                    var stackEmission = _dbContext.FDStackEmissions.Find(stackEmissionMonitoringEntity.FDStackEmissionId);
                    stackEmission.InstrumentId = stackEmissionMonitoringEntity.InstrumentId;
                    stackEmission.StatusId = stackEmissionMonitoringEntity.StatusId;
                    stackEmission.Vsc = stackEmissionMonitoringEntity.Vsc;
                    stackEmission.StackAttachedTo = stackEmissionMonitoringEntity.StackAttachedTo;
                    stackEmission.StackArea = stackEmissionMonitoringEntity.StackArea;
                    stackEmission.StackIdentity = stackEmissionMonitoringEntity.StackIdentity;
                    stackEmission.StackHeight = stackEmissionMonitoringEntity.StackHeight;
                    stackEmission.DurationOfBoiler = stackEmissionMonitoringEntity.DurationOfBoiler;
                    stackEmission.StackDiameter = stackEmissionMonitoringEntity.StackDiameter;
                    stackEmission.HeightOfPorthole = stackEmissionMonitoringEntity.HeightOfPorthole;
                    stackEmission.Is8DAnd2D = stackEmissionMonitoringEntity.Is8DAnd2D;
                    stackEmission.Samplingportandplatformexists = stackEmissionMonitoringEntity.Samplingportandplatformexists;
                    stackEmission.Airpollutioncontrolequipmentexists = stackEmissionMonitoringEntity.Airpollutioncontrolequipmentexists;
                    stackEmission.MaterialOfConstruction = stackEmissionMonitoringEntity.MaterialOfConstruction;
                    stackEmission.StackShapeTop = stackEmissionMonitoringEntity.StackShapeTop;
                    stackEmission.Fuelused_Type = stackEmissionMonitoringEntity.Fuelused_Type;
                    stackEmission.Fuelused_Consumption = stackEmissionMonitoringEntity.Fuelused_Consumption;
                    stackEmission.BarometricPressure = stackEmissionMonitoringEntity.BarometricPressure;
                    stackEmission.FlueGasComposition_CO = stackEmissionMonitoringEntity.FlueGasComposition_CO;
                    stackEmission.FlueGasComposition_CO2 = stackEmissionMonitoringEntity.FlueGasComposition_CO2;
                    stackEmission.FlueGasComposition_N2 = stackEmissionMonitoringEntity.FlueGasComposition_N2;
                    stackEmission.FlueGasComposition_O2 = stackEmissionMonitoringEntity.FlueGasComposition_O2;
                    stackEmission.FlueGasQuantity = stackEmissionMonitoringEntity.FlueGasQuantity;
                    stackEmission.MoistureContent = stackEmissionMonitoringEntity.MoistureContent;
                    stackEmission.AmbientTemperature = stackEmissionMonitoringEntity.AmbientTemperature;
                    stackEmission.StackTemperature = stackEmissionMonitoringEntity.StackTemperature;
                    stackEmission.AmbientTempC = stackEmissionMonitoringEntity.AmbientTempC;
                    stackEmission.StackTempC = stackEmissionMonitoringEntity.StackTempC;
                    stackEmission.C = stackEmissionMonitoringEntity.C;
                    stackEmission.Bwo = stackEmissionMonitoringEntity.Bwo;
                    stackEmission.Velocity_V = stackEmissionMonitoringEntity.Velocity_V;
                    stackEmission.VelocityofStackGas = stackEmissionMonitoringEntity.VelocityofStackGas;
                    stackEmission.PitotTubefactor = stackEmissionMonitoringEntity.PitotTubefactor;
                    stackEmission.StaticPressure = stackEmissionMonitoringEntity.StaticPressure;
                    stackEmission.AbsoluteStackPressure = stackEmissionMonitoringEntity.AbsoluteStackPressure;
                    stackEmission.ThimbleNO = stackEmissionMonitoringEntity.ThimbleNO;
                    stackEmission.TotalGasPassed = stackEmissionMonitoringEntity.TotalGasPassed;
                    stackEmission.SamplingGasTemp_Average = stackEmissionMonitoringEntity.SamplingGasTemp_Average;
                    stackEmission.SamplingGasTemp_Final = stackEmissionMonitoringEntity.SamplingGasTemp_Final;
                    stackEmission.SamplingGasTemp_Initial = stackEmissionMonitoringEntity.SamplingGasTemp_Initial;
                    stackEmission.Velocity_V = stackEmissionMonitoringEntity.Velocity_V;
                    stackEmission.Vf = stackEmissionMonitoringEntity.Vf;
                    stackEmission.Va = stackEmissionMonitoringEntity.Va;
                    stackEmission.Pf = stackEmissionMonitoringEntity.Pf;
                    stackEmission.Pi = stackEmissionMonitoringEntity.Pi;
                    stackEmission.Ti = stackEmissionMonitoringEntity.Ti;
                    stackEmission.Tf = stackEmissionMonitoringEntity.Tf;
                    stackEmission.Pstd = stackEmissionMonitoringEntity.Pstd;
                    stackEmission.Tstd = stackEmissionMonitoringEntity.Tstd;
                    stackEmission.LoadAtMonitoring = stackEmissionMonitoringEntity.LoadAtMonitoring;
                    _dbContext.SaveChanges();

                    var stackIso = _dbContext.FDStackEmission_IsoKinetic.Find(stackEmissionMonitoringEntity.FDStackEmission_IsoKineticId);
                    stackIso.SamplingDuration = stackEmissionMonitoringEntity.SamplingDuration;
                    stackIso.SamplingFlowRate = stackEmissionMonitoringEntity.SamplingFlowRate;
                    stackIso.VacuumPv_Average = stackEmissionMonitoringEntity.VacuumPv_Average;
                    stackIso.VacuumPv_Final = stackEmissionMonitoringEntity.VacuumPv_Final;
                    stackIso.VacuumPv_Initial = stackEmissionMonitoringEntity.VacuumPv_Initial;
                    stackIso.DryGasFinal = stackEmissionMonitoringEntity.DryGasFinal;
                    stackIso.DryGasDiff = stackEmissionMonitoringEntity.DryGasDiff;
                    stackIso.DryGasInitial = stackEmissionMonitoringEntity.DryGasInitial;
                    stackIso.NozzleConstantArea = stackEmissionMonitoringEntity.NozzleConstantArea;
                     _dbContext.SaveChanges();

                    var stackGas = _dbContext.FDStackEmission_GaseousData.Find(stackEmissionMonitoringEntity.FDStackEmission_GaseousDataId);
                    stackGas.FlowRate = stackEmissionMonitoringEntity.FlowRate;
                    stackGas.BarometricPressure = stackEmissionMonitoringEntity.BarometricPressure;
                    stackGas.SamplingTime = stackEmissionMonitoringEntity.SamplingTime;
                    stackGas.GasTemp = stackEmissionMonitoringEntity.GasTemp;
                    stackGas.DryGasMeterReading_Final = stackEmissionMonitoringEntity.DryGasMeterReading_Final;
                    stackGas.DryGasMeterReading_Initial = stackEmissionMonitoringEntity.DryGasMeterReading_Initial;
                    stackGas.DryGasMeterReading_Total = stackEmissionMonitoringEntity.DryGasMeterReading_Total;
                    stackGas.BottleNo = stackEmissionMonitoringEntity.BottleNo;
                    stackGas.AbsorbingSolutionUsed_Conc = stackEmissionMonitoringEntity.AbsorbingSolutionUsed_Conc;
                    stackGas.AbsorbingSolutionUsed_solution = stackEmissionMonitoringEntity.AbsorbingSolutionUsed_solution;
                    stackGas.AbsorbingSolutionUsed_Vol = stackEmissionMonitoringEntity.AbsorbingSolutionUsed_Vol;
                    stackGas.PreservationDone = stackEmissionMonitoringEntity.PreservationDone;
                    stackGas.Parameters = stackEmissionMonitoringEntity.Parameters;
                    stackGas.TestMethodName = stackEmissionMonitoringEntity.TestMethodName;
                    stackGas.InField = stackEmissionMonitoringEntity.InField;
                    stackGas.IsNABLAccredited = stackEmissionMonitoringEntity.IsNABLAccredited;
                    stackGas.Vs = stackEmissionMonitoringEntity.Vs;
                    _dbContext.SaveChanges();

                    var stackPara = _dbContext.FDStack_ParameterData.Find(stackEmissionMonitoringEntity.FDStack_ParameterDataId);
                    stackPara.Parameters = stackEmissionMonitoringEntity.Parameters;
                    stackPara.TestMethodName = stackEmissionMonitoringEntity.TestMethodName;
                    stackPara.InField = stackEmissionMonitoringEntity.InField;
                    stackPara.IsNABLAccredited = stackEmissionMonitoringEntity.IsNABLAccredited;
                   _dbContext.SaveChanges();
                }

                return stackEmissionMonitoringEntity.FDStackEmissionId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string DeleteStackEmissionField(long FDStackEmissionId, long FDStackEmission_IsoKineticId, long FDStackEmission_GaseousDataId)
        {
            try
            {
                _dbContext.FDStackEmission_GaseousData.Remove(_dbContext.FDStackEmission_GaseousData.Find(FDStackEmission_GaseousDataId));//Complete Deleted from DB
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string DeleteStackPara(long FDStackEmissionId, long FDStack_ParameterDataId)
        {
            try
            {
                _dbContext.FDStack_ParameterData.Remove(_dbContext.FDStack_ParameterData.Find(FDStack_ParameterDataId));//Complete Deleted from DB
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
        public long AddWorkplace(WorkplaceAndFugitiveEmissionEntity workplaceAndFugitiveEmissionEntity)
        {
            throw new NotImplementedException("Not Implemented");
        }
        public long AddNoiseLevel(NoiseLevelMonitoringEntity noiseLevelMonitoringEntity)
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
        public FDMicrobiologicalInfo GetAirMonitoringDetails(int MicrobiologicalID)
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

        public string DeleteAmbientAirField(long FieldId, long MatterSizeId, long GasesSampledId)
        {
            throw new NotImplementedException();
        }
    }
}
