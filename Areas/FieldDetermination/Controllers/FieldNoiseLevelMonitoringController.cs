using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.FieldDetermination.Models;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.FieldDetermination;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.FieldDetermination;
namespace LIMS_DEMO.Areas.FieldDetermination.Controllers
{
    [RouteArea("FieldDetermination")]
    public class FieldNoiseLevelMonitoringController : Controller
    {
        string strStatus = "";
        public FieldNoiseLevelMonitoringController()
        {
            BALFactory.noiseLevelMonitoringBAL = new NoiseLevelMonitoringBAL();
            BALFactory.enquiryBAL = new BAL.Enquiry.EnquiryBAL();
            BALFactory.samplecollectionBAL = new BAL.Collection.SampleCollectionBAL();
        }

        // GET: FieldDetermination/FieldNoiseLevelMonitoring
        public ActionResult FieldNoiseLevelMonitoring(bool flag, int? SampleCollectionId = 0, int? EnquiryId = 0, int? EnquirySampleID = 0, int? FieldId = 0)
        {
            FDNoiseLevelMonitoringModel model = new FDNoiseLevelMonitoringModel();
            model.GridModel = new List<FDNoiseLevelMonitoringModel>();
            model.GridSourceModel = new List<FDNoiseLevelMonitoringModel>();
            model.SampleCollectionId = SampleCollectionId;
            model.EnquiryId = EnquiryId;
            model.EnquirySampleID = (long)EnquirySampleID;
            if (flag == true)
            {
                //For "Ambient Noise Level" & "Workplace Noise Level
                model.flag = true;
            }
            if (flag == false)
            {
                //For "Source Noise Level"
                model.flag = false;
            }

            TempData.Remove("NoiseLevelList");
            TempData.Remove("SourceNoiseLevelList");
            if (FieldId != 0)
            {
                NoiseLevelMonitoringEntity.FDNoiseInfo noiseInfo = BALFactory.noiseLevelMonitoringBAL.GetNoiseLevelDetails((Int32)FieldId, (Int32)SampleCollectionId);
                if (noiseInfo != null)
                {
                    model.FieldNoiseId = (Int32)FieldId;
                    model.SampleCollectionId = SampleCollectionId;
                    model.EnquiryId = EnquiryId;
                    model.StatusId = noiseInfo.NoiseDetails.StatusId;
                    model.CurrentStatus = noiseInfo.NoiseDetails.CurrentStatus;
                    ViewBag.CurrentStatus = noiseInfo.NoiseDetails.CurrentStatus;
                    model.Location = noiseInfo.NoiseDetails.Location;
                    model.LocationHeight = noiseInfo.NoiseDetails.LocationHeight;
                    model.LocationZone = noiseInfo.NoiseDetails.LocationZone;
                    model.LeqAvgDayTime = noiseInfo.NoiseDetails.LeqAvgDayTime;
                    model.LeqAvgNightTime = noiseInfo.NoiseDetails.LeqAvgNightTime;
                    model.L10AvgDayTime = noiseInfo.NoiseDetails.L10AvgDayTime;
                    model.L10AvgNightTime = noiseInfo.NoiseDetails.L10AvgNightTime;
                    model.L50AvgDayTime = noiseInfo.NoiseDetails.L50AvgDayTime;
                    model.L50AvgNightTime = noiseInfo.NoiseDetails.L50AvgNightTime;
                    model.L90AvgDayTime = noiseInfo.NoiseDetails.L90AvgDayTime;
                    model.L90AvgNightTime = noiseInfo.NoiseDetails.L90AvgNightTime;
                    model.LminAvgDayTime = noiseInfo.NoiseDetails.LminAvgDayTime;
                    model.LminAvgNightTime = noiseInfo.NoiseDetails.LminAvgNightTime;
                    model.LmaxAvgDayTime = noiseInfo.NoiseDetails.LmaxAvgDayTime;
                    model.LmaxAvgNightTime = noiseInfo.NoiseDetails.LmaxAvgNightTime;
                    model.InstrumentID = noiseInfo.NoiseDetails.InstrumentID;
                    model.InstrumentMake = noiseInfo.NoiseDetails.InstrumentMake;
                    model.InstrumentModel = noiseInfo.NoiseDetails.InstrumentModel;
                    model.MicrophoneHeight = noiseInfo.NoiseDetails.MicrophoneHeight;
                    model.Obstacles = noiseInfo.NoiseDetails.Obstacles;
                    model.WindType = noiseInfo.NoiseDetails.WindType;
                    model.Isolation = noiseInfo.NoiseDetails.Isolation;
                    model.Weather = noiseInfo.NoiseDetails.Weather;
                    model.MonitoringDate = noiseInfo.NoiseDetails.MonitoringDate;
                    model.MonitoringTime = noiseInfo.NoiseDetails.MonitoringTime;
                    model.DistancefrmSource = noiseInfo.NoiseDetails.DistancefrmSource;
                    model.ProminentNoiseSource = noiseInfo.NoiseDetails.ProminentNoiseSource;
                    model.BackgroundNoiselevel = noiseInfo.NoiseDetails.BackgroundNoiselevel;
                    int i = 1;
                    foreach (var grid1 in noiseInfo.NoiseInfos)
                    {
                        try
                        {
                            model.GridModel.Add(new FDNoiseLevelMonitoringModel()
                            {
                                SrNo = i,
                                FieldNoiseId = grid1.FieldNoiseId,
                                ParameterId = grid1.ParameterId,
                                TimeDuration = grid1.TimeDuration,
                                FileName = grid1.FileName,
                                LocationGrid = grid1.LocationGrid,
                                DayNight = grid1.DayNight,
                                ShiftName = grid1.ShiftName,
                                LeqFastResponse = grid1.LeqFastResponse,
                                LeqSlowResponse = grid1.LeqSlowResponse,
                                L10FastResponse = grid1.L10FastResponse,
                                L10SlowResponse = grid1.L10SlowResponse,
                                L50FastResponse = grid1.L50FastResponse,
                                L50SlowResponse = grid1.L50SlowResponse,
                                L90FastResponse = grid1.L90FastResponse,
                                L90SlowResponse = grid1.L90SlowResponse,
                                LminFastResponse = grid1.LminFastResponse,
                                LminSlowResponse = grid1.LminSlowResponse,
                                LmaxFastResponse = grid1.LmaxFastResponse,
                                LmaxSlowResponse = grid1.LmaxSlowResponse,
                                ParameterName = grid1.ParameterName,
                                TestMethodName = grid1.TestMethodName,
                                InField = grid1.InField,
                                IsNABLAccredited = grid1.IsNABLAccredited,
                            });

                            i++;
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    int j = 1;
                    foreach (var gridSource in noiseInfo.SourceNoiseInfos)
                    {
                        try
                        {
                            model.GridSourceModel.Add(new FDNoiseLevelMonitoringModel()
                            {
                                SrNo = j,
                                FieldNoiseId = gridSource.FieldNoiseId,
                                NoiseSourceParameterId = gridSource.NoiseSourceParameterId,
                                TimeSource = gridSource.TimeSource,
                                LocationGrid = gridSource.LocationGrid,
                                DayNight = gridSource.DayNight,
                                ShiftName = gridSource.ShiftName,
                                Inside = gridSource.Inside,
                                Outside = gridSource.Outside,
                                InsertionLoss = gridSource.InsertionLoss,
                                ParameterName = gridSource.ParameterName,
                                TestMethodName = gridSource.TestMethodName,
                                UnitName = gridSource.UnitName,
                                InField = gridSource.InField,
                                IsNABLAccredited = gridSource.IsNABLAccredited,
                            });
                            j++;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    TempData["NoiseLevelList"] = model.GridModel;
                    TempData["SourceNoiseLevelList"] = model.GridSourceModel;
                }
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult FieldNoiseLevelMonitoring(FDNoiseLevelMonitoringModel model, string Save)
        {

            CoreFactory.noiseLevelMonitoringEntity = new NoiseLevelMonitoringEntity();
            var noiseLevelList = (List<FDNoiseLevelMonitoringModel>)TempData.Peek("NoiseLevelList");
            var sourcenoiseLevelList = (List<FDNoiseLevelMonitoringModel>)TempData.Peek("SourceNoiseLevelList");

            CoreFactory.noiseLevelMonitoringEntity.FieldNoiseId = Convert.ToByte(model.FieldNoiseId);
            CoreFactory.noiseLevelMonitoringEntity.SampleCollectionId = model.SampleCollectionId;
            CoreFactory.noiseLevelMonitoringEntity.EnquiryId = model.EnquiryId;
            CoreFactory.noiseLevelMonitoringEntity.MonitoringDate = model.MonitoringDate;
            CoreFactory.noiseLevelMonitoringEntity.MonitoringTime = model.MonitoringTime;
            CoreFactory.noiseLevelMonitoringEntity.Location = model.Location;
            CoreFactory.noiseLevelMonitoringEntity.LeqAvgDayTime = model.LeqAvgDayTime;
            CoreFactory.noiseLevelMonitoringEntity.LeqAvgNightTime = model.LeqAvgNightTime;
            CoreFactory.noiseLevelMonitoringEntity.L10AvgDayTime = model.L10AvgDayTime;
            CoreFactory.noiseLevelMonitoringEntity.L10AvgNightTime = model.L10AvgNightTime;
            CoreFactory.noiseLevelMonitoringEntity.L50AvgDayTime = model.L50AvgDayTime;
            CoreFactory.noiseLevelMonitoringEntity.L50AvgNightTime = model.L50AvgNightTime;
            CoreFactory.noiseLevelMonitoringEntity.L90AvgDayTime = model.L90AvgDayTime;
            CoreFactory.noiseLevelMonitoringEntity.L90AvgNightTime = model.L90AvgNightTime;
            CoreFactory.noiseLevelMonitoringEntity.LminAvgDayTime = model.LminAvgDayTime;
            CoreFactory.noiseLevelMonitoringEntity.LminAvgNightTime = model.LminAvgNightTime;
            CoreFactory.noiseLevelMonitoringEntity.LmaxAvgDayTime = model.LmaxAvgDayTime;
            CoreFactory.noiseLevelMonitoringEntity.LmaxAvgNightTime = model.LmaxAvgNightTime;
            CoreFactory.noiseLevelMonitoringEntity.LocationHeight = model.LocationHeight;
            CoreFactory.noiseLevelMonitoringEntity.LocationZone = model.LocationZone;
            CoreFactory.noiseLevelMonitoringEntity.InstrumentMake = model.InstrumentMake;
            CoreFactory.noiseLevelMonitoringEntity.InstrumentID = model.InstrumentID;
            CoreFactory.noiseLevelMonitoringEntity.InstrumentModel = model.InstrumentModel;
            CoreFactory.noiseLevelMonitoringEntity.MicrophoneHeight = model.MicrophoneHeight;
            CoreFactory.noiseLevelMonitoringEntity.Obstacles = model.Obstacles;
            CoreFactory.noiseLevelMonitoringEntity.WindType = model.WindType;
            CoreFactory.noiseLevelMonitoringEntity.Isolation = model.Isolation;
            CoreFactory.noiseLevelMonitoringEntity.Weather = model.Weather;
            CoreFactory.noiseLevelMonitoringEntity.DistancefrmSource = model.DistancefrmSource;
            CoreFactory.noiseLevelMonitoringEntity.ProminentNoiseSource = model.ProminentNoiseSource;
            CoreFactory.noiseLevelMonitoringEntity.BackgroundNoiselevel = model.BackgroundNoiselevel;
            CoreFactory.noiseLevelMonitoringEntity.IsActive = true;
            CoreFactory.noiseLevelMonitoringEntity.EnteredBy = LIMS.User.UserMasterID;
            CoreFactory.noiseLevelMonitoringEntity.EnteredDate = DateTime.UtcNow;
            /////For Ambient/Workplace Noise Level Sample Type/////////////////
            if (noiseLevelList != null)
            {
                if (noiseLevelList.Count > 0)
                {
                    foreach (var noiseLevel in noiseLevelList)
                    {
                        CoreFactory.noiseLevelMonitoringEntity.SampleTypeName = "AmbientWorkplaceNoise";
                        CoreFactory.noiseLevelMonitoringEntity.FieldNoiseId = Convert.ToInt32(model.FieldNoiseId);
                        CoreFactory.noiseLevelMonitoringEntity.ParameterId = noiseLevel.ParameterId;
                        CoreFactory.noiseLevelMonitoringEntity.ParameterName = noiseLevel.ParameterName;
                        CoreFactory.noiseLevelMonitoringEntity.TimeDuration = noiseLevel.TimeDuration;
                        CoreFactory.noiseLevelMonitoringEntity.FileName = noiseLevel.FileName;
                        CoreFactory.noiseLevelMonitoringEntity.ShiftName = noiseLevel.ShiftName;
                        CoreFactory.noiseLevelMonitoringEntity.LocationGrid = noiseLevel.LocationGrid;
                        CoreFactory.noiseLevelMonitoringEntity.DayNight = noiseLevel.DayNight;
                        CoreFactory.noiseLevelMonitoringEntity.LeqFastResponse = noiseLevel.LeqFastResponse;
                        CoreFactory.noiseLevelMonitoringEntity.LeqSlowResponse = noiseLevel.LeqSlowResponse;
                        CoreFactory.noiseLevelMonitoringEntity.L10FastResponse = noiseLevel.L10FastResponse;
                        CoreFactory.noiseLevelMonitoringEntity.L10SlowResponse = noiseLevel.L10SlowResponse;
                        CoreFactory.noiseLevelMonitoringEntity.L50FastResponse = noiseLevel.L50FastResponse;
                        CoreFactory.noiseLevelMonitoringEntity.L50SlowResponse = noiseLevel.L50SlowResponse;
                        CoreFactory.noiseLevelMonitoringEntity.L90FastResponse = noiseLevel.L90FastResponse;
                        CoreFactory.noiseLevelMonitoringEntity.L90SlowResponse = noiseLevel.L90SlowResponse;
                        CoreFactory.noiseLevelMonitoringEntity.LminFastResponse = noiseLevel.LminFastResponse;
                        CoreFactory.noiseLevelMonitoringEntity.LminSlowResponse = noiseLevel.LminSlowResponse;
                        CoreFactory.noiseLevelMonitoringEntity.LmaxFastResponse = noiseLevel.LmaxFastResponse;
                        CoreFactory.noiseLevelMonitoringEntity.LmaxSlowResponse = noiseLevel.LmaxSlowResponse;
                        CoreFactory.noiseLevelMonitoringEntity.ParameterName = noiseLevel.ParameterName;
                        CoreFactory.noiseLevelMonitoringEntity.TestMethodName = noiseLevel.TestMethodName;
                        CoreFactory.noiseLevelMonitoringEntity.InField = noiseLevel.InField;
                        CoreFactory.noiseLevelMonitoringEntity.IsNABLAccredited = noiseLevel.IsNABLAccredited;

                        if (Save == "FieldSave")
                        {
                            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("InProgress");
                            CoreFactory.noiseLevelMonitoringEntity.StatusId = (byte)iStatusId;
                            model.FieldNoiseId = (Int32)BALFactory.noiseLevelMonitoringBAL.AddNoiseLevel(CoreFactory.noiseLevelMonitoringEntity);
                        }
                        else if (Save == "FieldSubmit")
                        {
                            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
                            CoreFactory.noiseLevelMonitoringEntity.StatusId = (byte)iStatusId;
                            model.FieldNoiseId = (Int32)BALFactory.noiseLevelMonitoringBAL.AddNoiseLevel(CoreFactory.noiseLevelMonitoringEntity);
                        }
                    }
                }
            }

            /////For Source Noise Level Sample Type/////////////////
            if (sourcenoiseLevelList != null)
            {
                if (sourcenoiseLevelList.Count > 0)
                {
                    foreach (var sourcenoiseLevel in sourcenoiseLevelList)
                    {
                        CoreFactory.noiseLevelMonitoringEntity.SampleTypeName = "SourceNoise";
                        CoreFactory.noiseLevelMonitoringEntity.FieldNoiseId = Convert.ToInt32(model.FieldNoiseId);
                        CoreFactory.noiseLevelMonitoringEntity.NoiseSourceParameterId = sourcenoiseLevel.NoiseSourceParameterId;
                        CoreFactory.noiseLevelMonitoringEntity.TimeSource = sourcenoiseLevel.TimeSource;
                        CoreFactory.noiseLevelMonitoringEntity.ShiftName = sourcenoiseLevel.ShiftName;
                        CoreFactory.noiseLevelMonitoringEntity.LocationGrid = sourcenoiseLevel.LocationGrid;
                        CoreFactory.noiseLevelMonitoringEntity.DayNight = sourcenoiseLevel.DayNight;
                        CoreFactory.noiseLevelMonitoringEntity.Inside = sourcenoiseLevel.Inside;
                        CoreFactory.noiseLevelMonitoringEntity.Outside = sourcenoiseLevel.Outside;
                        CoreFactory.noiseLevelMonitoringEntity.InsertionLoss = sourcenoiseLevel.InsertionLoss;
                        CoreFactory.noiseLevelMonitoringEntity.ParameterName = sourcenoiseLevel.ParameterName;
                        CoreFactory.noiseLevelMonitoringEntity.TestMethodName = sourcenoiseLevel.TestMethodName;
                        CoreFactory.noiseLevelMonitoringEntity.UnitName = sourcenoiseLevel.UnitName;
                        CoreFactory.noiseLevelMonitoringEntity.InField = sourcenoiseLevel.InField;
                        CoreFactory.noiseLevelMonitoringEntity.IsNABLAccredited = sourcenoiseLevel.IsNABLAccredited;

                        if (Save == "FieldSave")
                        {
                            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("InProgress");
                            CoreFactory.noiseLevelMonitoringEntity.StatusId = (byte)iStatusId;
                            model.FieldNoiseId = (Int32)BALFactory.noiseLevelMonitoringBAL.AddNoiseLevel(CoreFactory.noiseLevelMonitoringEntity);
                        }
                        else if (Save == "FieldSubmit")
                        {
                            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
                            CoreFactory.noiseLevelMonitoringEntity.StatusId = (byte)iStatusId;
                            model.FieldNoiseId = (Int32)BALFactory.noiseLevelMonitoringBAL.AddNoiseLevel(CoreFactory.noiseLevelMonitoringEntity);
                        }
                    }
                }
            }

            return Json(new { FieldNoiseId = model.FieldNoiseId, message = "Values Entered successfully." }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult _FieldNoiseLevelMonitoring(FDNoiseLevelMonitoringModel model)
        {
            return PartialView(model);
        }

        ////////////////////////////For Ambient / Workplace Noise Level Data/////////////////////////////////////////////////////////////////////

        [HttpGet]
        public PartialViewResult _FieldNoiseLevelMonitoringList(int EnquirySampleID, int? Id = 0)
        {

            FDNoiseLevelMonitoringModel model = new FDNoiseLevelMonitoringModel();
            model.EnquirySampleID = EnquirySampleID;
            ViewBag.Parameter = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            ViewBag.TestMethod = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            if (Id != 0 && TempData["NoiseLevelList"] != null)
            {
                var noiseLevelList = (List<FDNoiseLevelMonitoringModel>)TempData.Peek("NoiseLevelList");
                TempData.Keep("NoiseLevelList");
                model = noiseLevelList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _FieldNoiseLevelMonitoringList(FDNoiseLevelMonitoringModel model)
        {

            List<FDNoiseLevelMonitoringModel> noiseLevelList = new List<FDNoiseLevelMonitoringModel>();
            if (TempData["NoiseLevelList"] != null)
            {
                noiseLevelList = (List<FDNoiseLevelMonitoringModel>)TempData["NoiseLevelList"];
            }
            if (model.SrNo == 0)
            {
                model.SrNo = noiseLevelList.Count() + 1;
                var result = BALFactory.samplearrivalBAL.GetInFieldIsNabl((Int32)model.EnquirySampleID, model.ParameterMasterId, model.TestMethodId);
                if (result != null)
                {
                    model.InField = result.InField;
                    model.IsNABLAccredited = result.IsNABLAccredited;
                }
                if (model.LeqFastResponse != null || model.LeqSlowResponse != null)
                {
                    model.ParameterName = "Leq";
                }
                else if (model.L10FastResponse != null || model.L10SlowResponse != null)
                {
                    model.ParameterName = "L10";
                }
                else if (model.L50FastResponse != null || model.L50SlowResponse != null)
                {
                    model.ParameterName = "L50";
                }
                else if (model.L90FastResponse != null || model.L90SlowResponse != null)
                {
                    model.ParameterName = "L90";
                }
                else if (model.LminFastResponse != null || model.LminSlowResponse != null)
                {
                    model.ParameterName = "Lmin";
                }
                else if (model.LmaxFastResponse != null || model.LmaxSlowResponse != null)
                {
                    model.ParameterName = "Lmax";
                }
                noiseLevelList.Add(model);
            }
            else
            {
                var noiseLevel = noiseLevelList.Where(c => c.SrNo == model.SrNo).FirstOrDefault();
                noiseLevel.ParameterName = model.ParameterName;
                noiseLevel.InField = model.InField;
                noiseLevel.IsNABLAccredited = model.IsNABLAccredited;
                if (model.TestMethodId == 0 || model.TestMethodId != null)
                {
                    noiseLevel.TestMethodName = "";
                }
                else
                {
                    noiseLevel.TestMethodName = model.TestMethodName;
                }
                noiseLevel.TimeDuration = model.TimeDuration;
                noiseLevel.ShiftName = model.ShiftName;
                noiseLevel.FileName = model.FileName;
                noiseLevel.DayNight = model.DayNight;
                noiseLevel.LocationGrid = model.LocationGrid;
                noiseLevel.LeqFastResponse = model.LeqFastResponse;
                noiseLevel.LeqSlowResponse = model.LeqSlowResponse;
                noiseLevel.L10FastResponse = model.L10FastResponse;
                noiseLevel.L10SlowResponse = model.L10SlowResponse;
                noiseLevel.L50FastResponse = model.L50FastResponse;
                noiseLevel.L50SlowResponse = model.L50SlowResponse;
                noiseLevel.L90FastResponse = model.L90FastResponse;
                noiseLevel.L90SlowResponse = model.L90SlowResponse;
                noiseLevel.LminFastResponse = model.LminFastResponse;
                noiseLevel.LminSlowResponse = model.LminSlowResponse;
                noiseLevel.LmaxFastResponse = model.LmaxFastResponse;
                noiseLevel.LmaxSlowResponse = model.LmaxSlowResponse;
            }
            TempData["NoiseLevelList"] = noiseLevelList;
            return Json(new { status = "success", message = "Added" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult _FDNoiseLevelGridList(FDNoiseLevelMonitoringModel model)
        {
            if (TempData["NoiseLevelList"] != null)
            {
                model.GridModel = (List<FDNoiseLevelMonitoringModel>)TempData.Peek("NoiseLevelList");
                TempData.Keep("NoiseLevelList");
            }
            return PartialView(model);
        }
        public JsonResult _DeleteNoiseLevelField(int? Id = 0)
        {
            var noiseLevelList = (List<FDNoiseLevelMonitoringModel>)TempData.Peek("NoiseLevelList");
            var noiseLevel = noiseLevelList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            if (noiseLevel.FieldNoiseId != 0)
            {
                BALFactory.noiseLevelMonitoringBAL.DeleteNoiseLevelField("AmbientWorkplaceNoise", noiseLevel.FieldNoiseId, noiseLevel.ParameterId);
            }
            noiseLevelList.Remove(noiseLevel);
            int i = 1;
            foreach (var item in noiseLevelList)
            {
                item.SrNo = i;
                i++;
            }
            TempData["NoiseLevelList"] = noiseLevelList;
            return Json(new { status = "success", message = "deleted" }, JsonRequestBehavior.AllowGet);
        }

        ////////////////////////////For Source Noise Level Data/////////////////////////////////////////////////////////////////////
       
        [HttpGet]
        public PartialViewResult _FieldSourceNoiseLevelList(int EnquirySampleID, int? Id = 0)
        {

            FDNoiseLevelMonitoringModel model = new FDNoiseLevelMonitoringModel();
            model.EnquirySampleID = EnquirySampleID;
            ViewBag.Parameter = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            ViewBag.TestMethod = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            ViewBag.UnitName = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);

            if (Id != 0 && TempData["SourceNoiseLevelList"] != null)
            {
                var sourcenoiseLevelList = (List<FDNoiseLevelMonitoringModel>)TempData.Peek("SourceNoiseLevelList");
                TempData.Keep("SourceNoiseLevelList");
                model = sourcenoiseLevelList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _FieldSourceNoiseLevelList(FDNoiseLevelMonitoringModel model)
        {

            List<FDNoiseLevelMonitoringModel> sourcenoiseLevelList = new List<FDNoiseLevelMonitoringModel>();
            if (TempData["SourceNoiseLevelList"] != null)
            {
                sourcenoiseLevelList = (List<FDNoiseLevelMonitoringModel>)TempData["SourceNoiseLevelList"];
            }
            if (model.SrNo == 0)
            {
                model.SrNo = sourcenoiseLevelList.Count() + 1;
                var result = BALFactory.samplearrivalBAL.GetInFieldIsNabl((Int32)model.EnquirySampleID, model.ParameterMasterId, model.TestMethodId);
                if (result != null)
                {
                    model.InField = result.InField;
                    model.IsNABLAccredited = result.IsNABLAccredited;
                }

                sourcenoiseLevelList.Add(model);
            }
            else
            {
                var sourcenoiseLevel = sourcenoiseLevelList.Where(c => c.SrNo == model.SrNo).FirstOrDefault();
                sourcenoiseLevel.ParameterName = model.ParameterName;
                sourcenoiseLevel.InField = model.InField;
                sourcenoiseLevel.IsNABLAccredited = model.IsNABLAccredited;
                if (model.TestMethodId == 0 || model.TestMethodId != null)
                {
                    sourcenoiseLevel.TestMethodName = "";
                }
                else
                {
                    sourcenoiseLevel.TestMethodName = model.TestMethodName;
                }
                sourcenoiseLevel.UnitName = model.UnitName;
                sourcenoiseLevel.TimeSource = model.TimeSource;
                sourcenoiseLevel.ShiftName = model.ShiftName;
                sourcenoiseLevel.LocationGrid = model.LocationGrid;
                sourcenoiseLevel.DayNight = model.DayNight;
                sourcenoiseLevel.Inside = model.Inside;
                sourcenoiseLevel.Outside = model.Outside;
                sourcenoiseLevel.InsertionLoss = model.InsertionLoss;
            }
            TempData["SourceNoiseLevelList"] = sourcenoiseLevelList;
            return Json(new { status = "success", message = "Added" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult _FDSourceLevelGridList(FDNoiseLevelMonitoringModel model)
        {

            if (TempData["SourceNoiseLevelList"] != null)
            {
                model.GridSourceModel = (List<FDNoiseLevelMonitoringModel>)TempData.Peek("SourceNoiseLevelList");
                TempData.Keep("SourceNoiseLevelList");
            }
            return PartialView(model);
        }

        public JsonResult _DeleteSorceLevelField(int? Id = 0)
        {
            var sourcenoiseLevelList = (List<FDNoiseLevelMonitoringModel>)TempData.Peek("SourceNoiseLevelList");
            var sourcenoiseLevel = sourcenoiseLevelList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            if (sourcenoiseLevel.FieldNoiseId != 0)
            {
                BALFactory.noiseLevelMonitoringBAL.DeleteNoiseLevelField("SourceNoise", sourcenoiseLevel.FieldNoiseId, sourcenoiseLevel.ParameterId);
            }
            sourcenoiseLevelList.Remove(sourcenoiseLevel);
            int i = 1;
            foreach (var item in sourcenoiseLevelList)
            {
                item.SrNo = i;
                i++;
            }
            TempData["SourceNoiseLevelList"] = sourcenoiseLevelList;
            return Json(new { status = "success", message = "deleted" }, JsonRequestBehavior.AllowGet);
        }
    }
}