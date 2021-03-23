using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.FieldDetermination.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.FieldDetermination;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.FieldDetermination;
using LIMS_DEMO.BAL.DropDown;
using Newtonsoft.Json;
namespace LIMS_DEMO.Areas.FieldDetermination.Controllers
{
    [RouteArea("FieldDetermination")]
    public class FieldAmbientAirMonitoringController : Controller
    {
        string strStatus = "";
        public FieldAmbientAirMonitoringController()
        {
            BALFactory.ambientAirMonitoringBAL = new AmbientAirMonitoringBAL();
            BALFactory.sampleParameterBAL = new BAL.Enquiry.SampleParameterBAL();
            BALFactory.enquiryBAL = new BAL.Enquiry.EnquiryBAL();
            BALFactory.samplecollectionBAL = new BAL.Collection.SampleCollectionBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
        }

        //GET: FieldDetermination/FieldAmbientAirMonitoring
        public ActionResult FieldAmbientAirMonitoring(long? SampleId = 0, int? SampleCollectionId = 0, int? EnquiryId = 0, int? EnquirySampleID = 0, int? ParameterMappingId = 0, int? FieldId = 0)
        {
            FDAmbientAirMonitoringModel model = new FDAmbientAirMonitoringModel();
            model.GridModel = new List<FDAmbientAirMonitoringModel>();
            model.Grid24HrModel = new List<FDAmbientAirMonitoringModel>();
            model.SampleCollectionId = SampleCollectionId;
            model.SampleCollectionId = (Int32)SampleId;
            model.EnquiryId = EnquiryId;
            model.ParameterMappingId = ParameterMappingId;
            model.EnquirySampleID = (long)EnquirySampleID;
            TempData.Remove("AmbientAirList");
            TempData.Remove("AmbientAirList24Hr");

            if (FieldId != 0)
            {
                FDAirInfo airInfo = BALFactory.ambientAirMonitoringBAL.GetAirDetails((Int32)FieldId);
                if (airInfo != null)
                {

                    model.FieldId = (Int32)FieldId;
                    model.MatterSizeId = airInfo.AirDetails.MatterSizeId;
                    model.StatusId = airInfo.AirDetails.StatusId;
                    model.CurrentStatus = airInfo.AirDetails.CurrentStatus;
                    ViewBag.CurrentStatus = airInfo.AirDetails.CurrentStatus;
                    model.InstrumentId = airInfo.AirDetails.InstrumentId;
                    model.WindVelocity = airInfo.AirDetails.WindVelocity;
                    model.SamplingDuration = airInfo.AirDetails.SamplingDuration;
                    model.WindDirection_ = airInfo.AirDetails.WindDirection_;
                    model.AverageWindVelocity = airInfo.AirDetails.AverageWindVelocity;
                    model.RelativeHumidity = airInfo.AirDetails.RelativeHumidity;
                    model.Temperature = airInfo.AirDetails.Temperature;
                    model.Temperature24Hr = airInfo.AirDetails.Temperature24Hr;
                    model.RelativeHumidity24Hr = airInfo.AirDetails.RelativeHumidity24Hr;
                    model.MatterSize = airInfo.AirDetails.MatterSize;
                    model.AvgFlowRate = airInfo.AirDetails.AvgFlowRate;
                    model.StartTime = airInfo.AirDetails.StartTime;
                    model.EndTime = airInfo.AirDetails.EndTime;
                    model.MatterSamplingDuration = airInfo.AirDetails.MatterSamplingDuration;
                    model.TotalVolAirPassed_L = airInfo.AirDetails.TotalVolAirPassed_L;
                    model.FilterPaperNo2 = airInfo.AirDetails.FilterPaperNo2;
                    model.TotalVolumeAirPassed_m3 = airInfo.AirDetails.TotalVolumeAirPassed_m3;
                    model.IsActive = airInfo.AirDetails.IsActive;

                    var shift1 = BALFactory.ambientAirMonitoringBAL.GetAirShift1Details((Int32)FieldId);
                    if (shift1 != null)
                    {
                        model.FilterPaperNo = shift1.FilterPaperNo;
                        model.CycloneCupNo = shift1.CycloneCupNo;
                        model.ShiftNo = 1;
                        model.TimeTotalizerDiff = shift1.TimeTotalizerDiff;
                        model.TimeTotalizerFinal = shift1.TimeTotalizerFinal;
                        model.TimeTotalizerInitial = shift1.TimeTotalizerInitial;
                        model.SamplingDurationDiff = shift1.SamplingDurationDiff;
                        model.SamplingDurationFinal = shift1.SamplingDurationFinal;
                        model.SamplingDurationInitial = shift1.SamplingDurationInitial;
                        model.FlowRateFinal = shift1.FlowRateFinal;
                        model.FlowRateInitial = shift1.FlowRateInitial;
                        model.FlowRateDiff = shift1.FlowRateDiff;
                    }

                    var shift2 = BALFactory.ambientAirMonitoringBAL.GetAirShift2Details((Int32)FieldId);
                    if (shift2 != null)
                    {
                        model.FilterPaperNos2 = shift2.FilterPaperNo;
                        model.CycloneCupNos2 = shift2.CycloneCupNo;
                        model.ShiftNos2 = 2;
                        model.TimeTotalizerDiffs2 = shift2.TimeTotalizerDiff;
                        model.TimeTotalizerFinals2 = shift2.TimeTotalizerFinal;
                        model.TimeTotalizerInitials2 = shift2.TimeTotalizerInitial;
                        model.SamplingDurationDiffs2 = shift2.SamplingDurationDiff;
                        model.SamplingDurationFinals2 = shift2.SamplingDurationFinal;
                        model.SamplingDurationInitials2 = shift2.SamplingDurationInitial;
                        model.FlowRateFinals2 = shift2.FlowRateFinal;
                        model.FlowRateInitials2 = shift2.FlowRateInitial;
                        model.FlowRateDiffs2 = shift2.FlowRateDiff;
                    }


                    var shift3 = BALFactory.ambientAirMonitoringBAL.GetAirShift3Details((Int32)FieldId);
                    if (shift3 != null)
                    {

                        model.FilterPaperNos3 = shift3.FilterPaperNo;
                        model.CycloneCupNos3 = shift3.CycloneCupNo;
                        model.ShiftNos3 = 3;
                        model.TimeTotalizerDiffs3 = shift3.TimeTotalizerDiff;
                        model.TimeTotalizerFinals3 = shift3.TimeTotalizerFinal;
                        model.TimeTotalizerInitials3 = shift3.TimeTotalizerInitial;
                        model.SamplingDurationDiffs3 = shift3.SamplingDurationDiff;
                        model.SamplingDurationFinals3 = shift3.SamplingDurationFinal;
                        model.SamplingDurationInitials3 = shift3.SamplingDurationInitial;
                        model.FlowRateFinals3 = shift3.FlowRateFinal;
                        model.FlowRateInitials3 = shift3.FlowRateInitial;
                        model.FlowRateDiffs3 = shift3.FlowRateDiff;
                    }

                    //List<StackEmissionMonitoringEntity> StackInfos = new List<StackEmissionMonitoringEntity>();
                    int i = 1;
                    foreach (var grid in airInfo.AirInfos)
                    {
                        try
                        {
                            model.GridModel.Add(new FDAmbientAirMonitoringModel()
                            {
                                SrNo = i,
                                FieldId = grid.FieldId,
                                MatterSizeId = grid.MatterSizeId,
                                GasesSampledId = grid.GasesSampledId,
                                GasesSampled = grid.GasesSampled,
                                ParameterName = grid.GasesSampled,
                                ShiftNoAir = Convert.ToString(grid.ShiftNoGases),
                                VolImpingingSolution = grid.VolImpingingSolution,
                                BottleNo = grid.BottleNo,
                                RotaMeterFlow = grid.RotaMeterFlow,
                                Duration = grid.Duration,
                                Vs = grid.Vs,
                                PreservationDone = grid.PreservationDone,
                                EnquirySampleID = model.EnquirySampleID,
                            });
                            i++;
                        }
                        catch (Exception ex)
                        {

                        }
                    }


                    int j = 1;
                    foreach (var grid24 in airInfo.AirInfos24Hr)
                    {
                        try
                        {
                            model.Grid24HrModel.Add(new FDAmbientAirMonitoringModel()
                            {
                                SrNo = j,
                                FieldId = grid24.FieldId,
                                MatterSizeId = grid24.MatterSizeId,
                                GasesSampledId = grid24.GasesSampledId,
                                GasesSampled = grid24.GasesSampled,
                                ParameterName = grid24.GasesSampled,
                                VolImpingingSolution = grid24.VolImpingingSolution,
                                BottleNo = grid24.BottleNo,
                                RotaMeterFlow = grid24.RotaMeterFlow,
                                Duration = grid24.Duration,
                                Vs = grid24.Vs,
                                PreservationDone = grid24.PreservationDone,
                                ShiftNoAir24Hr = Convert.ToString(grid24.ShiftNoGases),
                                EnquirySampleID = model.EnquirySampleID,
                            });
                            j++;
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    TempData["AmbientAirList"] = model.GridModel;
                    TempData["AmbientAirList24Hr"] = model.Grid24HrModel;
                }
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult FieldAmbientAirMonitoring(FDAmbientAirMonitoringModel model, string Save)
        {
            CoreFactory.ambientAirEntity = new AmbientAirMonitoringEntity();
            var ambientAirList = (List<FDAmbientAirMonitoringModel>)TempData.Peek("AmbientAirList");
            var ambientAirList24Hr = (List<FDAmbientAirMonitoringModel>)TempData.Peek("AmbientAirList24Hr");

            CoreFactory.ambientAirEntity.FieldId = Convert.ToInt32(model.FieldId);
            CoreFactory.ambientAirEntity.MatterSizeId = model.MatterSizeId;
            CoreFactory.ambientAirEntity.SampleCollectionId = (long)model.SampleCollectionId;
            CoreFactory.ambientAirEntity.EnquiryId = (long)model.EnquiryId;
            CoreFactory.ambientAirEntity.WindVelocity = model.WindVelocity;
            CoreFactory.ambientAirEntity.InstrumentId = model.InstrumentId;
            CoreFactory.ambientAirEntity.SamplingDuration = model.SamplingDuration;
            CoreFactory.ambientAirEntity.WindDirection_ = model.WindDirection_;
            CoreFactory.ambientAirEntity.AverageWindVelocity = model.AverageWindVelocity;
            CoreFactory.ambientAirEntity.RelativeHumidity = model.RelativeHumidity;
            CoreFactory.ambientAirEntity.Temperature = model.Temperature;
            CoreFactory.ambientAirEntity.Temperature24Hr = model.Temperature24Hr;
            CoreFactory.ambientAirEntity.RelativeHumidity24Hr = model.RelativeHumidity24Hr;
            CoreFactory.ambientAirEntity.AvgFlowRate = model.AvgFlowRate;
            CoreFactory.ambientAirEntity.StartTime = model.StartTime;
            CoreFactory.ambientAirEntity.EndTime = model.EndTime;
            CoreFactory.ambientAirEntity.MatterSamplingDuration = model.MatterSamplingDuration;
            CoreFactory.ambientAirEntity.TotalVolAirPassed_L = model.TotalVolAirPassed_L;
            CoreFactory.ambientAirEntity.FilterPaperNo2 = model.FilterPaperNo2;
            CoreFactory.ambientAirEntity.TotalVolumeAirPassed_m3 = model.TotalVolumeAirPassed_m3;
            CoreFactory.ambientAirEntity.IsActive = true;
            if (Save == "FieldSave")
            {
                int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("InProgress");
                CoreFactory.ambientAirEntity.StatusId = (byte)iStatusId;
                model.FieldId = (Int32)BALFactory.ambientAirMonitoringBAL.AddAmbientAir(CoreFactory.ambientAirEntity);
            }
            else if (Save == "FieldSubmit")
            {
                int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
                CoreFactory.ambientAirEntity.StatusId = (byte)iStatusId;
                model.FieldId = (Int32)BALFactory.ambientAirMonitoringBAL.AddAmbientAir(CoreFactory.ambientAirEntity);
            }

            if (ambientAirList24Hr != null)
            {
                foreach (var ambientAir24Hr in ambientAirList24Hr)
                {
                    CoreFactory.ambientAirEntity.FieldId = Convert.ToInt32(model.FieldId);
                    CoreFactory.ambientAirEntity.ParameterMappingId = 1;
                    CoreFactory.ambientAirEntity.GasesSampledId = Convert.ToInt32(ambientAir24Hr.GasesSampledId);
                    CoreFactory.ambientAirEntity.GasesSampled = ambientAir24Hr.ParameterName;//note:Save ParameterName as GasesSampled
                    CoreFactory.ambientAirEntity.VolImpingingSolution = ambientAir24Hr.VolImpingingSolution;
                    CoreFactory.ambientAirEntity.BottleNo = ambientAir24Hr.BottleNo;
                    CoreFactory.ambientAirEntity.RotaMeterFlow = ambientAir24Hr.RotaMeterFlow;
                    CoreFactory.ambientAirEntity.Duration = ambientAir24Hr.Duration;
                    CoreFactory.ambientAirEntity.Vs = ambientAir24Hr.Vs;
                    CoreFactory.ambientAirEntity.PreservationDone = ambientAir24Hr.PreservationDone;
                    if (ambientAir24Hr.ShiftNoAir24Hr == "Shift 1")
                    {
                        CoreFactory.ambientAirEntity.ShiftNoGases = 1;
                    }
                    else if (ambientAir24Hr.ShiftNoAir24Hr == "Shift 2")
                    {
                        CoreFactory.ambientAirEntity.ShiftNoGases = 2;
                    }
                    else if (ambientAir24Hr.ShiftNoAir24Hr == "Shift 3")
                    {
                        CoreFactory.ambientAirEntity.ShiftNoGases = 3;
                    }
                    CoreFactory.ambientAirEntity.Testing = true;
                    CoreFactory.ambientAirEntity.IsActive = true;
                    CoreFactory.ambientAirEntity.EnteredBy = LIMS.User.UserMasterID;
                    CoreFactory.ambientAirEntity.EnteredDate = DateTime.UtcNow;
                    model.GasesSampledId = (Int32)BALFactory.ambientAirMonitoringBAL.AddAmbientAir24Hr(CoreFactory.ambientAirEntity);
                }

            }
            if (ambientAirList != null)
            {
                foreach (var ambientAir in ambientAirList)
                {
                    CoreFactory.ambientAirEntity.FieldId = Convert.ToInt32(model.FieldId);
                    CoreFactory.ambientAirEntity.ParameterMappingId = 1;
                    CoreFactory.ambientAirEntity.GasesSampled = ambientAir.ParameterName;//note:Save ParameterName as GasesSampled
                    CoreFactory.ambientAirEntity.VolImpingingSolution = ambientAir.VolImpingingSolution;
                    CoreFactory.ambientAirEntity.BottleNo = ambientAir.BottleNo;
                    CoreFactory.ambientAirEntity.RotaMeterFlow = ambientAir.RotaMeterFlow;
                    CoreFactory.ambientAirEntity.Duration = ambientAir.Duration;
                    CoreFactory.ambientAirEntity.Vs = ambientAir.Vs;
                    CoreFactory.ambientAirEntity.PreservationDone = ambientAir.PreservationDone;
                    if (ambientAir.ShiftNoAir == "Shift 1")
                    {
                        CoreFactory.ambientAirEntity.ShiftNoGases = 1;
                    }
                    else if (ambientAir.ShiftNoAir == "Shift 2")
                    {
                        CoreFactory.ambientAirEntity.ShiftNoGases = 2;
                    }
                    else if (ambientAir.ShiftNoAir == "Shift 3")
                    {
                        CoreFactory.ambientAirEntity.ShiftNoGases = 3;
                    }
                    CoreFactory.ambientAirEntity.IsActive = true;
                    CoreFactory.ambientAirEntity.Testing = false;
                    CoreFactory.ambientAirEntity.EnteredBy = LIMS.User.UserMasterID;
                    CoreFactory.ambientAirEntity.EnteredDate = DateTime.UtcNow;
                    model.GasesSampledId = (Int32)BALFactory.ambientAirMonitoringBAL.AddAmbientAir24Hr(CoreFactory.ambientAirEntity);
                }
            }
            return Json(new { FieldId = model.FieldId, message = "Values Entered successfully." }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult _FieldAmbientAirMonitoring(FDAmbientAirMonitoringModel model)
        {
            return PartialView(model);
        }

        [HttpGet]
        public PartialViewResult _FDAmbientAirGasesList(int EnquirySampleID, int? Id = 0)
        {
            FDAmbientAirMonitoringModel model = new FDAmbientAirMonitoringModel();
            ViewBag.Parameter = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            model.EnquirySampleID = EnquirySampleID;
            if (Id != 0 && TempData["AmbientAirList"] != null)
            {
                var ambientAirList = (List<FDAmbientAirMonitoringModel>)TempData.Peek("AmbientAirList");
                TempData.Keep("AmbientAirList");
                model = ambientAirList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
                //model.ParameterName = BALFactory.sampleParameterBAL.GetFDParameters((Int32)model.ParameterMappingId); //sample.Parameters,
            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _FDAmbientAirGasesList(FDAmbientAirMonitoringModel model)
        {
            List<FDAmbientAirMonitoringModel> ambientAirList = new List<FDAmbientAirMonitoringModel>();

            if (TempData["AmbientAirList"] != null)
            {
                ambientAirList = (List<FDAmbientAirMonitoringModel>)TempData["AmbientAirList"];
            }
            if (model.SrNo == 0)
            {
                model.SrNo = ambientAirList.Count() + 1;
                //model.ParameterName = BALFactory.sampleParameterBAL.GetFDParameters((Int32)model.ParameterMappingId); //sample.Parameters,
                ambientAirList.Add(model);
            }
            else
            {
                var ambientAir = ambientAirList.Where(c => c.SrNo == model.SrNo).FirstOrDefault();
                ambientAir.RotaMeterFlow = model.RotaMeterFlow;
                ambientAir.VolImpingingSolution = model.VolImpingingSolution;
                ambientAir.BottleNo = model.BottleNo;
                ambientAir.Duration = model.Duration;
                //ambientAir.GasesSampled = model.GasesSampled;
                ambientAir.Vs = model.Vs;
                ambientAir.PreservationDone = model.PreservationDone;
                ambientAir.ParameterName = model.ParameterName;
                ambientAir.ShiftNoAir = model.ShiftNoAir;
                //ambientAir.ParameterMasterId = model.ParameterMasterId;
                //ambientAir.ParameterName = BALFactory.sampleParameterBAL.GetFDParameters((Int32)model.ParameterMappingId);//sample.Parameters,
                //ambientAir.ParameterName = model.ParameterName;
                //ambientAir.ParameterMappingId = model.ParameterMappingId;
            }

            TempData["AmbientAirList"] = ambientAirList;
            return Json(new { status = "success", message = "Added" }, JsonRequestBehavior.AllowGet);
            //RedirectToAction("FieldAmbientAirMonitoring");
        }

        [HttpGet]
        public PartialViewResult _FDAmbientAirList(FDAmbientAirMonitoringModel model)
        {
            //List<FDAmbientAirMonitoringModel> model = new List<FDAmbientAirMonitoringModel>();
            if (TempData["AmbientAirList"] != null)
            {
                model.GridModel = (List<FDAmbientAirMonitoringModel>)TempData.Peek("AmbientAirList");
                //model.Add(new FDAmbientAirMonitoringModel()
                //{
                //    ParameterName = BALFactory.sampleParameterBAL.GetSampleParameters(EnquirySampleID), //sample.Parameters,
                // });
                TempData.Keep("AmbientAirList");
            }
            return PartialView(model);
        }

        public JsonResult _DeleteAmbientAirField(int? Id = 0)
        {
            var ambientAirList = (List<FDAmbientAirMonitoringModel>)TempData.Peek("AmbientAirList");
            var ambientAir = ambientAirList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            //if (ambientAir.FieldId != 0)
            //{
            //    BALFactory.ambientAirMonitoringBAL.DeleteAmbientAirField(ambientAir.FieldId, ambientAir.MatterSizeId, ambientAir.GasesSampledId);
            //}
            ambientAirList.Remove(ambientAir);
            int i = 1;
            foreach (var item in ambientAirList)
            {
                item.SrNo = i;
                i++;
            }
            TempData["AmbientAirList"] = ambientAirList;
            return Json(new { status = "success", message = "deleted" }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _FieldAmbientAirMonitoringShift(FDAmbientAirMonitoringModel model)
        {

            return PartialView(model);
        }
        public string FieldAmbientAirMonitoringShift(string model)
        {
            try
            {
                Core.FieldDetermination.AmbientAirMonitoringEntity sampleParameterReview = JsonConvert.DeserializeObject<Core.FieldDetermination.AmbientAirMonitoringEntity>(model);
                var status = BALFactory.ambientAirMonitoringBAL.AddAmbientShiftWiseData(sampleParameterReview);
                return "Data Submitted";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        public PartialViewResult _FDAmbientAirGasesList24Hr(int EnquirySampleID, int? Id = 0)
        {
            FDAmbientAirMonitoringModel model = new FDAmbientAirMonitoringModel();
            ViewBag.Parameter = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            model.EnquirySampleID = EnquirySampleID;
            if (Id != 0 && TempData["AmbientAirList24Hr"] != null)
            {
                var ambientAirList24Hr = (List<FDAmbientAirMonitoringModel>)TempData.Peek("AmbientAirList24Hr");
                TempData.Keep("AmbientAirList24Hr");
                model = ambientAirList24Hr.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
                //model.ParameterName = BALFactory.sampleParameterBAL.GetFDParameters((Int32)model.ParameterMappingId); //sample.Parameters,
            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _FDAmbientAirGasesList24Hr(FDAmbientAirMonitoringModel model)
        {
            List<FDAmbientAirMonitoringModel> ambientAirList24Hr = new List<FDAmbientAirMonitoringModel>();

            if (TempData["AmbientAirList24Hr"] != null)
            {
                ambientAirList24Hr = (List<FDAmbientAirMonitoringModel>)TempData["AmbientAirList24Hr"];
            }
            if (model.SrNo == 0)
            {
                model.SrNo = ambientAirList24Hr.Count() + 1;
                //model.ParameterName = BALFactory.sampleParameterBAL.GetFDParameters((Int32)model.ParameterMappingId); //sample.Parameters,
                ambientAirList24Hr.Add(model);
            }
            else
            {
                var ambientAir = ambientAirList24Hr.Where(c => c.SrNo == model.SrNo).FirstOrDefault();
                ambientAir.RotaMeterFlow = model.RotaMeterFlow;
                ambientAir.VolImpingingSolution = model.VolImpingingSolution;
                ambientAir.BottleNo = model.BottleNo;
                ambientAir.Duration = model.Duration;
                ambientAir.ShiftNoAir24Hr = model.ShiftNoAir24Hr;
                //ambientAir.GasesSampled = model.GasesSampled;
                ambientAir.Vs = model.Vs;
                ambientAir.PreservationDone = model.PreservationDone;
                ambientAir.ParameterName = model.ParameterName;
                //ambientAir.ParameterMasterId = model.ParameterMasterId;
                //ambientAir.ParameterName = BALFactory.sampleParameterBAL.GetFDParameters((Int32)model.ParameterMappingId);//sample.Parameters,
                //ambientAir.ParameterName = model.ParameterName;
                //ambientAir.ParameterMappingId = model.ParameterMappingId;
            }

            TempData["AmbientAirList24Hr"] = ambientAirList24Hr;
            return Json(new { status = "success", message = "Added" }, JsonRequestBehavior.AllowGet);
            //RedirectToAction("FieldAmbientAirMonitoring");
        }

        [HttpGet]
        public PartialViewResult _FDAmbientAirList24Hr(FDAmbientAirMonitoringModel model)
        {
            //List<FDAmbientAirMonitoringModel> model = new List<FDAmbientAirMonitoringModel>();
            if (TempData["AmbientAirList24Hr"] != null)
            {
                model.Grid24HrModel = (List<FDAmbientAirMonitoringModel>)TempData.Peek("AmbientAirList24Hr");
                //model.Add(new FDAmbientAirMonitoringModel()
                //{
                //    ParameterName = BALFactory.sampleParameterBAL.GetSampleParameters(EnquirySampleID), //sample.Parameters,
                // });
                TempData.Keep("AmbientAirList24Hr");
            }
            return PartialView(model);
        }
        public JsonResult _DeleteAmbientAirField24Hr(int? Id = 0)
        {
            var ambientAirList24Hr = (List<FDAmbientAirMonitoringModel>)TempData.Peek("AmbientAirList24Hr");
            var ambientAir24Hr = ambientAirList24Hr.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            //if (ambientAir24Hr.FieldId != 0)
            //{
            //    BALFactory.ambientAirMonitoringBAL.DeleteAmbientAirField(ambientAir24Hr.FieldId, ambientAir24Hr.MatterSizeId, ambientAir24Hr.GasesSampledId);
            //}
            ambientAirList24Hr.Remove(ambientAir24Hr);
            int i = 1;
            foreach (var item in ambientAirList24Hr)
            {
                item.SrNo = i;
                i++;
            }
            TempData["AmbientAirList24Hr"] = ambientAirList24Hr;
            return Json(new { status = "success", message = "deleted" }, JsonRequestBehavior.AllowGet);
        }
    }
}