using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.FieldDetermination;
using LIMS_DEMO.BAL.Arrival;
using LIMS_DEMO.BAL.DropDown;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.FieldDetermination;
using LIMS_DEMO.Areas.FieldDetermination.Models;


namespace LIMS_DEMO.Areas.FieldDetermination.Controllers
{
    [RouteArea("FieldDetermination")]
    public class FieldStackEmissionMonitoringController : Controller
    {
        string strStatus = "";
        public FieldStackEmissionMonitoringController()
        {
            BALFactory.stackEmissionMonitoringBAL = new StackEmissionMonitoringBAL();
            BALFactory.enquiryBAL = new BAL.Enquiry.EnquiryBAL();
            BALFactory.samplecollectionBAL = new BAL.Collection.SampleCollectionBAL();
            BALFactory.samplearrivalBAL = new SampleArrivalBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
        }
        // GET: FieldDetermination/FieldStackEmissionMonitoring
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FieldStackEmissionMonitoring(long? SampleId = 0, int? SampleCollectionId = 0, int? FDStackEmissionId = 0, int? EnquiryId = 0, int? EnquirySampleID = 0)
        {
            FDStackEmissionModel model = new FDStackEmissionModel();
            model.GridModel = new List<FDStackEmissionModel>();
            model.GridParaModel = new List<FDStackEmissionModel>();
            model.SampleCollectionId = (Int32)SampleId;
            model.SampleCollectionId = (Int32)SampleCollectionId;
            model.EnquiryId = EnquiryId;
            model.EnquirySampleID = (int)EnquirySampleID;
            TempData.Remove("StackList");
            TempData.Remove("StackParaList");

            if (FDStackEmissionId != 0)
            {
                FDStackInfo stackInfo = BALFactory.stackEmissionMonitoringBAL.GetStackDetails((Int32)FDStackEmissionId);
                if (stackInfo != null)
                {
                    model.FDStackEmissionId = (Int32)FDStackEmissionId;
                    model.FDStackEmission_IsoKineticId = stackInfo.StackDetails.FDStackEmission_IsoKineticId;
                    model.StatusId = stackInfo.StackDetails.StatusId;
                    model.CurrentStatus = stackInfo.StackDetails.CurrentStatus;
                    ViewBag.CurrentStatus = stackInfo.StackDetails.CurrentStatus;
                    model.InstrumentId = stackInfo.StackDetails.InstrumentId;
                    model.BarometricPressure = stackInfo.StackDetails.BarometricPressure;
                    model.FlueGasComposition_CO = stackInfo.StackDetails.FlueGasComposition_CO;
                    model.FlueGasComposition_N2 = stackInfo.StackDetails.FlueGasComposition_N2;
                    model.FlueGasComposition_CO2 = stackInfo.StackDetails.FlueGasComposition_CO2;
                    model.FlueGasComposition_O2 = stackInfo.StackDetails.FlueGasComposition_O2;
                    model.MoistureContent = stackInfo.StackDetails.MoistureContent;
                    model.DurationOfBoiler = stackInfo.StackDetails.DurationOfBoiler;
                    model.AmbientTemperature = stackInfo.StackDetails.AmbientTemperature;
                    model.StackTemperature = stackInfo.StackDetails.StackTemperature;
                    model.VelocityofStackGas = stackInfo.StackDetails.VelocityofStackGas;
                    model.PitotTubefactor = stackInfo.StackDetails.PitotTubefactor;
                    model.StaticPressure = stackInfo.StackDetails.StaticPressure;
                    model.AbsoluteStackPressure = stackInfo.StackDetails.AbsoluteStackPressure;
                    model.FlueGasQuantity = stackInfo.StackDetails.FlueGasQuantity;
                    model.ThimbleNO = stackInfo.StackDetails.ThimbleNO;
                    model.TotalGasPassed = stackInfo.StackDetails.TotalGasPassed;
                    model.SamplingGasTemp_Initial = stackInfo.StackDetails.SamplingGasTemp_Initial;
                    model.SamplingGasTemp_Final = stackInfo.StackDetails.SamplingGasTemp_Final;
                    model.SamplingGasTemp_Average = stackInfo.StackDetails.SamplingGasTemp_Average;
                    model.Vsc = stackInfo.StackDetails.Vsc;
                    model.Bwo = stackInfo.StackDetails.Bwo;
                    model.AmbientTempC = stackInfo.StackDetails.AmbientTempC;
                    model.StackTempC = stackInfo.StackDetails.StackTempC;
                    model.C = stackInfo.StackDetails.C;
                    model.StackIdentity = stackInfo.StackDetails.StackIdentity;
                    model.StackAttachedTo = stackInfo.StackDetails.StackAttachedTo;
                    model.MaterialOfConstruction = stackInfo.StackDetails.MaterialOfConstruction;
                    model.StackHeight = stackInfo.StackDetails.StackHeight;
                    model.StackDiameter = stackInfo.StackDetails.StackDiameter;
                    model.StackArea = stackInfo.StackDetails.StackArea;
                    model.HeightOfPorthole = stackInfo.StackDetails.HeightOfPorthole;
                    model.StackShapeTop = stackInfo.StackDetails.StackShapeTop;
                    model.Fuelused_Type = stackInfo.StackDetails.Fuelused_Type;
                    model.Fuelused_Consumption = stackInfo.StackDetails.Fuelused_Consumption;
                    model.Is8DAnd2D = stackInfo.StackDetails.Is8DAnd2D;
                    model.Samplingportandplatformexists = stackInfo.StackDetails.Samplingportandplatformexists;
                    model.Airpollutioncontrolequipmentexists = stackInfo.StackDetails.Airpollutioncontrolequipmentexists;
                    model.NozzleConstantArea = stackInfo.StackDetails.NozzleConstantArea;
                    model.SamplingFlowRate = stackInfo.StackDetails.SamplingFlowRate;
                    model.SamplingDuration = stackInfo.StackDetails.SamplingDuration;
                    model.VacuumPv_Average = stackInfo.StackDetails.VacuumPv_Average;
                    model.VacuumPv_Final = stackInfo.StackDetails.VacuumPv_Final;
                    model.VacuumPv_Initial = stackInfo.StackDetails.VacuumPv_Initial;
                    model.DryGasMeterReading_Final1 = stackInfo.StackDetails.DryGasFinal;
                    model.DryGasMeterReading_Initial1 = stackInfo.StackDetails.DryGasInitial;
                    model.DryGasMeterReading_Total1 = stackInfo.StackDetails.DryGasDiff;
                    model.Velocity_V = stackInfo.StackDetails.Velocity_V;
                    model.Vf = stackInfo.StackDetails.Vf;
                    model.Va = stackInfo.StackDetails.Va;
                    model.Pi = stackInfo.StackDetails.Pi;
                    model.Pf = stackInfo.StackDetails.Pf;
                    model.Ti = stackInfo.StackDetails.Ti;
                    model.Tf = stackInfo.StackDetails.Tf;
                    model.Pstd = stackInfo.StackDetails.Pstd;
                    model.Tstd = stackInfo.StackDetails.Tstd;
                    model.LoadAtMonitoring = stackInfo.StackDetails.LoadAtMonitoring;
                    model.IsActive = stackInfo.StackDetails.IsActive;
                    //List<StackEmissionMonitoringEntity> StackInfos = new List<StackEmissionMonitoringEntity>();
                    int i = 1;
                    foreach (var grid1 in stackInfo.StackInfos)
                    {
                        try
                        {
                            model.GridModel.Add(new FDStackEmissionModel()
                            {
                                SrNo = i,
                                FDStackEmissionId = grid1.FDStackEmissionId,
                                FDStackEmission_IsoKineticId = grid1.FDStackEmission_IsoKineticId,
                                FDStackEmission_GaseousDataId = grid1.FDStackEmission_GaseousDataId,
                                FlowRate = grid1.FlowRate,
                                SamplingTime = grid1.SamplingTime,
                                GasTemp = grid1.GasTemp,
                                //Parameters = grid1.Parameters,
                                //ParameterName = grid1.Parameters,
                                ParameterName = grid1.Parameters,
                                TestMethodName = grid1.TestMethodName,
                                InField = grid1.InField,
                                IsNABLAccredited = grid1.IsNABLAccredited,
                                BarometricPressure = grid1.BarometricPressure,
                                DryGasMeterReading_Final = grid1.DryGasMeterReading_Final,
                                DryGasMeterReading_Initial = grid1.DryGasMeterReading_Initial,
                                DryGasMeterReading_Total = grid1.DryGasMeterReading_Total,
                                BottleNo = grid1.BottleNo,
                                AbsorbingSolutionUsed_Conc = grid1.AbsorbingSolutionUsed_Conc,
                                AbsorbingSolutionUsed_solution = grid1.AbsorbingSolutionUsed_solution,
                                AbsorbingSolutionUsed_Vol = grid1.AbsorbingSolutionUsed_Vol,
                                PreservationDone = grid1.PreservationDone,
                                Vs = grid1.Vs,
                                EnquirySampleID = model.EnquirySampleID,
                                CurrentStatus = model.CurrentStatus,
                            });
                            i++;
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    int j = 1;
                    foreach (var grid2 in stackInfo.StackParaInfo)
                    {
                        try
                        {
                            model.GridParaModel.Add(new FDStackEmissionModel()
                            {
                                SrNo = j,
                                FDStackEmissionId = grid2.FDStackEmissionId,
                                FDStack_ParameterDataId = grid2.FDStack_ParameterDataId,
                                ParameterName = grid2.Parameters,
                                TestMethodName = grid2.TestMethodName,
                                TestResults = grid2.TestResults,
                                InField = grid2.InField,
                                IsNABLAccredited = grid2.IsNABLAccredited,
                                EnquirySampleID = model.EnquirySampleID,
                            });
                            j++;
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    TempData["StackList"] = model.GridModel;
                    TempData["StackParaList"] = model.GridParaModel;
                }
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult FieldStackEmissionMonitoring(FDStackEmissionModel model, string Save)
        {
            CoreFactory.stackEmissionMonitoringEntity = new StackEmissionMonitoringEntity();
            var stackList = (List<FDStackEmissionModel>)TempData.Peek("StackList");
            var stackParaList = (List<FDStackEmissionModel>)TempData.Peek("StackParaList");
            //if (Save == "FieldSubmit")
            // {
            // int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
            foreach (var Stack in stackList)
            {
                // if (model.FDStackEmissionId == 0)
                // {
                CoreFactory.stackEmissionMonitoringEntity.FDStackEmissionId = Convert.ToInt32(model.FDStackEmissionId);
                CoreFactory.stackEmissionMonitoringEntity.FDStackEmission_IsoKineticId = model.FDStackEmission_IsoKineticId;
                CoreFactory.stackEmissionMonitoringEntity.SampleCollectionId = model.SampleCollectionId;
                CoreFactory.stackEmissionMonitoringEntity.EnquiryId = model.EnquiryId;
                //CoreFactory.stackEmissionMonitoringEntity.StatusId = (byte)iStatusId;
                CoreFactory.stackEmissionMonitoringEntity.InstrumentId = model.InstrumentId;
                CoreFactory.stackEmissionMonitoringEntity.StackIdentity = model.StackIdentity;
                CoreFactory.stackEmissionMonitoringEntity.StackAttachedTo = model.StackAttachedTo;
                CoreFactory.stackEmissionMonitoringEntity.StackArea = model.StackArea;
                CoreFactory.stackEmissionMonitoringEntity.StackHeight = model.StackHeight;
                CoreFactory.stackEmissionMonitoringEntity.StackDiameter = model.StackDiameter;
                CoreFactory.stackEmissionMonitoringEntity.DurationOfBoiler = model.DurationOfBoiler;
                CoreFactory.stackEmissionMonitoringEntity.HeightOfPorthole = model.HeightOfPorthole;
                CoreFactory.stackEmissionMonitoringEntity.Is8DAnd2D = model.Is8DAnd2D;
                CoreFactory.stackEmissionMonitoringEntity.Samplingportandplatformexists = model.Samplingportandplatformexists;
                CoreFactory.stackEmissionMonitoringEntity.Airpollutioncontrolequipmentexists = model.Airpollutioncontrolequipmentexists;
                CoreFactory.stackEmissionMonitoringEntity.MaterialOfConstruction = model.MaterialOfConstruction;
                CoreFactory.stackEmissionMonitoringEntity.StackShapeTop = model.StackShapeTop;
                CoreFactory.stackEmissionMonitoringEntity.Fuelused_Type = model.Fuelused_Type;
                CoreFactory.stackEmissionMonitoringEntity.Fuelused_Consumption = model.Fuelused_Consumption;
                CoreFactory.stackEmissionMonitoringEntity.FlueGasComposition_CO = model.FlueGasComposition_CO;
                CoreFactory.stackEmissionMonitoringEntity.FlueGasComposition_N2 = model.FlueGasComposition_N2;
                CoreFactory.stackEmissionMonitoringEntity.FlueGasComposition_O2 = model.FlueGasComposition_O2;
                CoreFactory.stackEmissionMonitoringEntity.FlueGasComposition_CO2 = model.FlueGasComposition_CO2;
                CoreFactory.stackEmissionMonitoringEntity.BarometricPressure = model.BarometricPressure;
                CoreFactory.stackEmissionMonitoringEntity.MoistureContent = model.MoistureContent;
                CoreFactory.stackEmissionMonitoringEntity.AmbientTemperature = model.AmbientTemperature;
                CoreFactory.stackEmissionMonitoringEntity.StackTemperature = model.StackTemperature;
                CoreFactory.stackEmissionMonitoringEntity.VelocityofStackGas = model.VelocityofStackGas;
                CoreFactory.stackEmissionMonitoringEntity.PitotTubefactor = model.PitotTubefactor;
                CoreFactory.stackEmissionMonitoringEntity.StaticPressure = model.StaticPressure;
                CoreFactory.stackEmissionMonitoringEntity.AbsoluteStackPressure = model.AbsoluteStackPressure;
                CoreFactory.stackEmissionMonitoringEntity.FlueGasQuantity = model.FlueGasQuantity;
                CoreFactory.stackEmissionMonitoringEntity.NozzleConstantArea = model.NozzleConstantArea;
                CoreFactory.stackEmissionMonitoringEntity.ThimbleNO = model.ThimbleNO;
                CoreFactory.stackEmissionMonitoringEntity.TotalGasPassed = model.TotalGasPassed;
                CoreFactory.stackEmissionMonitoringEntity.SamplingGasTemp_Initial = model.SamplingGasTemp_Initial;
                CoreFactory.stackEmissionMonitoringEntity.SamplingGasTemp_Final = model.SamplingGasTemp_Final;
                CoreFactory.stackEmissionMonitoringEntity.SamplingGasTemp_Average = model.SamplingGasTemp_Average;
                CoreFactory.stackEmissionMonitoringEntity.Bwo = model.Bwo;
                CoreFactory.stackEmissionMonitoringEntity.C = model.C;
                CoreFactory.stackEmissionMonitoringEntity.Vsc = model.Vsc;
                CoreFactory.stackEmissionMonitoringEntity.DryGasFinal = model.DryGasMeterReading_Final1;
                CoreFactory.stackEmissionMonitoringEntity.DryGasInitial = model.DryGasMeterReading_Initial1;
                CoreFactory.stackEmissionMonitoringEntity.DryGasDiff = model.DryGasMeterReading_Total1;
                CoreFactory.stackEmissionMonitoringEntity.StackTempC = model.StackTempC;
                CoreFactory.stackEmissionMonitoringEntity.AmbientTempC = model.AmbientTempC;
                CoreFactory.stackEmissionMonitoringEntity.SamplingFlowRate = model.SamplingFlowRate;
                CoreFactory.stackEmissionMonitoringEntity.SamplingDuration = model.SamplingDuration;
                CoreFactory.stackEmissionMonitoringEntity.VacuumPv_Average = model.VacuumPv_Average;
                CoreFactory.stackEmissionMonitoringEntity.VacuumPv_Final = model.VacuumPv_Final;
                CoreFactory.stackEmissionMonitoringEntity.VacuumPv_Initial = model.VacuumPv_Initial;
                CoreFactory.stackEmissionMonitoringEntity.NozzleConstantArea = model.NozzleConstantArea;
                CoreFactory.stackEmissionMonitoringEntity.Velocity_V = model.Velocity_V;
                CoreFactory.stackEmissionMonitoringEntity.Vf = model.Vf;
                CoreFactory.stackEmissionMonitoringEntity.Va = model.Va;
                CoreFactory.stackEmissionMonitoringEntity.Pf = model.Pf;
                CoreFactory.stackEmissionMonitoringEntity.Pi = model.Pi;
                CoreFactory.stackEmissionMonitoringEntity.Ti = model.Ti;
                CoreFactory.stackEmissionMonitoringEntity.Tf = model.Tf;
                CoreFactory.stackEmissionMonitoringEntity.Pstd = model.Pstd;
                CoreFactory.stackEmissionMonitoringEntity.Tstd = model.Tstd;
                CoreFactory.stackEmissionMonitoringEntity.LoadAtMonitoring = model.LoadAtMonitoring;
                // }
                CoreFactory.stackEmissionMonitoringEntity.FDStackEmissionId = Convert.ToInt32(model.FDStackEmissionId);
                CoreFactory.stackEmissionMonitoringEntity.FlowRate = Stack.FlowRate;
                //CoreFactory.stackEmissionMonitoringEntity.Parameters = Stack.Parameters;
                CoreFactory.stackEmissionMonitoringEntity.FDStackEmission_GaseousDataId = Convert.ToInt32(Stack.FDStackEmission_GaseousDataId);
                CoreFactory.stackEmissionMonitoringEntity.Parameters = Stack.ParameterName;
                CoreFactory.stackEmissionMonitoringEntity.TestMethodName = Stack.TestMethodName;
                CoreFactory.stackEmissionMonitoringEntity.InField = Stack.InField;
                CoreFactory.stackEmissionMonitoringEntity.IsNABLAccredited = Stack.IsNABLAccredited;
                CoreFactory.stackEmissionMonitoringEntity.SamplingTime = Stack.SamplingTime;
                CoreFactory.stackEmissionMonitoringEntity.GasTemp = Stack.GasTemp;
                CoreFactory.stackEmissionMonitoringEntity.BarometricPressure = Stack.BarometricPressure;
                CoreFactory.stackEmissionMonitoringEntity.DryGasMeterReading_Final = Stack.DryGasMeterReading_Final;
                CoreFactory.stackEmissionMonitoringEntity.DryGasMeterReading_Initial = Stack.DryGasMeterReading_Initial;
                CoreFactory.stackEmissionMonitoringEntity.DryGasMeterReading_Total = Stack.DryGasMeterReading_Total;
                CoreFactory.stackEmissionMonitoringEntity.BottleNo = Stack.BottleNo;
                CoreFactory.stackEmissionMonitoringEntity.AbsorbingSolutionUsed_Conc = Stack.AbsorbingSolutionUsed_Conc;
                CoreFactory.stackEmissionMonitoringEntity.AbsorbingSolutionUsed_solution = Stack.AbsorbingSolutionUsed_solution;
                CoreFactory.stackEmissionMonitoringEntity.AbsorbingSolutionUsed_Vol = Stack.AbsorbingSolutionUsed_Vol;
                CoreFactory.stackEmissionMonitoringEntity.PreservationDone = Stack.PreservationDone;
                CoreFactory.stackEmissionMonitoringEntity.Vs = Stack.Vs;
                CoreFactory.stackEmissionMonitoringEntity.IsActive = true;
                CoreFactory.stackEmissionMonitoringEntity.EnteredBy = LIMS.User.UserMasterID;
                CoreFactory.stackEmissionMonitoringEntity.EnteredDate = DateTime.UtcNow;

                if (Save == "FieldSave")
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("InProgress");
                    CoreFactory.stackEmissionMonitoringEntity.StatusId = (byte)iStatusId;
                    model.FDStackEmissionId = (Int32)BALFactory.stackEmissionMonitoringBAL.AddStackEmission(CoreFactory.stackEmissionMonitoringEntity);
                }
                else if (Save == "FieldSubmit")
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
                    CoreFactory.stackEmissionMonitoringEntity.StatusId = (byte)iStatusId;
                    model.FDStackEmissionId = (Int32)BALFactory.stackEmissionMonitoringBAL.AddStackEmission(CoreFactory.stackEmissionMonitoringEntity);
                }
                //model.FDStackEmissionId = (Int32)BALFactory.stackEmissionMonitoringBAL.AddStackEmission(CoreFactory.stackEmissionMonitoringEntity);
            }
            //}
            if (stackParaList != null)
            {
                foreach (var stpList in stackParaList)
                {
                    CoreFactory.stackEmissionMonitoringEntity.FDStackEmissionId = Convert.ToInt32(model.FDStackEmissionId);
                    CoreFactory.stackEmissionMonitoringEntity.FDStack_ParameterDataId = Convert.ToInt32(stpList.FDStack_ParameterDataId);
                    CoreFactory.stackEmissionMonitoringEntity.Parameters = stpList.ParameterName;
                    CoreFactory.stackEmissionMonitoringEntity.TestMethodName = stpList.TestMethodName;
                    CoreFactory.stackEmissionMonitoringEntity.TestResults = stpList.TestResults;
                    CoreFactory.stackEmissionMonitoringEntity.InField = stpList.InField;
                    CoreFactory.stackEmissionMonitoringEntity.IsNABLAccredited = stpList.IsNABLAccredited;
                    CoreFactory.stackEmissionMonitoringEntity.IsActive = true;
                    CoreFactory.stackEmissionMonitoringEntity.EnteredBy = LIMS.User.UserMasterID;
                    CoreFactory.stackEmissionMonitoringEntity.EnteredDate = DateTime.UtcNow;
                    model.FDStack_ParameterDataId = (Int32)BALFactory.stackEmissionMonitoringBAL.AddStackParameter(CoreFactory.stackEmissionMonitoringEntity);
                }
            }
            return Json(new { FDStackEmissionId = model.FDStackEmissionId, message = "Values Entered successfully." }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _FieldStackSingle(FDStackEmissionModel model)
        {
            return PartialView(model);
        }

        public PartialViewResult _FDStackSingle(FDStackEmissionModel model)
        {
            return PartialView(model);
        }

        [HttpGet]
        public PartialViewResult _FieldStackEmission(int EnquirySampleID, int? Id = 0)
        {
            FDStackEmissionModel model = new FDStackEmissionModel();
            model.EnquirySampleID = EnquirySampleID;
            ViewBag.Parameter = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            ViewBag.TestMethod = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            if (Id != 0 && TempData["StackList"] != null)
            {
                var stackList = (List<FDStackEmissionModel>)TempData.Peek("StackList");
                TempData.Keep("StackList");
                model = stackList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _FieldStackEmission(FDStackEmissionModel model)
        {
            List<FDStackEmissionModel> stackList = new List<FDStackEmissionModel>();
            if (TempData["StackList"] != null)
            {
                stackList = (List<FDStackEmissionModel>)TempData["StackList"];
            }
            if (model.SrNo == 0)
            {
                model.SrNo = stackList.Count() + 1;
                var result = BALFactory.samplearrivalBAL.GetInFieldIsNabl((Int32)model.EnquirySampleID, model.ParameterMasterId, model.TestMethodId);
                if (result != null)
                {
                    model.InField = result.InField;
                    model.IsNABLAccredited = result.IsNABLAccredited;
                }
                stackList.Add(model);
            }
            else
            {
                var stack = stackList.Where(c => c.SrNo == model.SrNo).FirstOrDefault();
                //stack.Parameters = model.Parameters;
                stack.Parameters = model.ParameterName;
                stack.InField = model.InField;
                stack.IsNABLAccredited = model.IsNABLAccredited;
                if (model.TestMethodId == 0 || model.TestMethodId == null)
                {
                    stack.TestMethodName = "";
                }
                else
                {
                    stack.TestMethodName = model.TestMethodName;
                }
                stack.FlowRate = model.FlowRate;
                stack.SamplingTime = model.SamplingTime;
                stack.GasTemp = model.GasTemp;
                stack.BarometricPressure = model.BarometricPressure;
                stack.AbsorbingSolutionUsed_Vol = model.AbsorbingSolutionUsed_Vol;
                stack.AbsorbingSolutionUsed_solution = model.AbsorbingSolutionUsed_solution;
                stack.AbsorbingSolutionUsed_Conc = model.AbsorbingSolutionUsed_Conc;
                stack.DryGasMeterReading_Total = model.DryGasMeterReading_Total;
                stack.DryGasMeterReading_Initial = model.DryGasMeterReading_Initial;
                stack.DryGasMeterReading_Final = model.DryGasMeterReading_Final;
                stack.BottleNo = model.BottleNo;
                stack.PreservationDone = model.PreservationDone;
                stack.Vs = model.Vs;
            }

            TempData["StackList"] = stackList;
            return Json(new { status = "success", message = "Added" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult _FStackList(FDStackEmissionModel model)
        {
            //var wasteWaterList = (List<FDWasteWaterModel>)TempData.Peek("WasteWaterList");
            if (TempData["StackList"] != null)
            {
                model.GridModel = (List<FDStackEmissionModel>)TempData.Peek("StackList");
                TempData.Keep("StackList");
            }
            return PartialView(model);
        }
        public JsonResult _DeleteStackEmissionField(int? Id = 0)
        {
            var stackList = (List<FDStackEmissionModel>)TempData.Peek("StackList");
            var stack = stackList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            if (stack.FDStackEmissionId != 0)
            {
                BALFactory.stackEmissionMonitoringBAL.DeleteStackEmissionField(stack.FDStackEmissionId, stack.FDStackEmission_IsoKineticId, stack.FDStackEmission_GaseousDataId);
            }
            stackList.Remove(stack);
            int i = 1;
            foreach (var item in stackList)
            {
                item.SrNo = i;
                i++;
            }
            TempData["StackList"] = stackList;
            return Json(new { status = "success", message = "deleted" }, JsonRequestBehavior.AllowGet);
        }
    
        [HttpGet]
        public PartialViewResult _FDStackParameter(int EnquirySampleID, int? Id = 0)
        {
            FDStackEmissionModel model = new FDStackEmissionModel();
            model.EnquirySampleID = EnquirySampleID;
            ViewBag.Parameter = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            ViewBag.TestMethod = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            if (Id != 0 && TempData["StackParaList"] != null)
            {
                var stackListPara = (List<FDStackEmissionModel>)TempData.Peek("StackParaList");
                TempData.Keep("StackParaList");
                model = stackListPara.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _FDStackParameter(FDStackEmissionModel model)
        {
            List<FDStackEmissionModel> stackListPara = new List<FDStackEmissionModel>();
            if (TempData["StackParaList"] != null)
            {
                stackListPara = (List<FDStackEmissionModel>)TempData["StackParaList"];
            }
            if (model.SrNo == 0)
            {
                model.SrNo = stackListPara.Count() + 1;
                var result = BALFactory.samplearrivalBAL.GetInFieldIsNabl((Int32)model.EnquirySampleID, model.ParameterMasterId, model.TestMethodId);
                if (result != null)
                {
                    model.InField = result.InField;
                    model.IsNABLAccredited = result.IsNABLAccredited;
                    model.ParameterName = result.ParameterName;
                    model.TestMethodName = result.TestMethodName;
                }
                stackListPara.Add(model);
            }
            else
            {
                var stackPara = stackListPara.Where(c => c.SrNo == model.SrNo).FirstOrDefault();
                stackPara.Parameters = model.ParameterName;
                stackPara.InField = model.InField;
                stackPara.IsNABLAccredited = model.IsNABLAccredited;
                if (model.TestMethodId == 0 || model.TestMethodId == null)
                {
                    stackPara.TestMethodName = "";
                }
                else
                {
                    stackPara.TestMethodName = model.TestMethodName;
                }
                stackPara.TestResults = model.TestResults;
            }

            TempData["StackParaList"] = stackListPara;
            return Json(new { status = "success", message = "Added" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult _FDStackParameterList(FDStackEmissionModel model)
        {
            if (TempData["StackParaList"] != null)
            {
                model.GridParaModel = (List<FDStackEmissionModel>)TempData.Peek("StackParaList");
                TempData.Keep("StackParaList");
            }
            return PartialView(model);
        }
        public JsonResult _DeleteStackPara(int? Id = 0)
        {
            var stackParaList = (List<FDStackEmissionModel>)TempData.Peek("StackParaList");
            var stack = stackParaList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            if (stack.FDStackEmissionId != 0)
            {
                BALFactory.stackEmissionMonitoringBAL.DeleteStackPara(stack.FDStackEmissionId, stack.FDStack_ParameterDataId);
            }
            stackParaList.Remove(stack);
            int i = 1;
            foreach (var item in stackParaList)
            {
                item.SrNo = i;
                i++;
            }
            TempData["StackParaList"] = stackParaList;
            return Json(new { status = "success", message = "deleted" }, JsonRequestBehavior.AllowGet);
        }
    }
}