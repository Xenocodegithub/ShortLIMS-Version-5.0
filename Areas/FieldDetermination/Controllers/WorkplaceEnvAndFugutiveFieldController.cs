using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.FieldDetermination.Models;
using LIMS_DEMO.Areas.Arrival.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.FieldDetermination;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.FieldDetermination;
using LIMS_DEMO.BAL.Arrival;
using LIMS_DEMO.BAL.DropDown;

namespace LIMS_DEMO.Areas.FieldDetermination.Controllers
{
    [RouteArea("FieldDetermination")]
    public class WorkplaceEnvAndFugutiveFieldController : Controller
    {
        string strStatus = "";
        public WorkplaceEnvAndFugutiveFieldController()
        {
            BALFactory.workplaceAndFugitiveEmissionBAL = new WorkplaceAndFugitiveEmissionBAL();
            BALFactory.enquiryBAL = new BAL.Enquiry.EnquiryBAL();
            BALFactory.samplecollectionBAL = new BAL.Collection.SampleCollectionBAL();
            BALFactory.samplearrivalBAL = new SampleArrivalBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
        }

        // GET: FieldDetermination/WorkplaceEnvAndFugutiveField

        [HttpGet]
        public ActionResult WorkplaceEnvAndFugitiveEmissionField(int? SampleCollectionId = 0, int? EnquiryId = 0, int? EnquirySampleID = 0, int? FieldId = 0)
        {
            FDWorkplaceEnvAndFugutiveEmissionModel model = new FDWorkplaceEnvAndFugutiveEmissionModel();
            model.GridModel = new List<FDWorkplaceEnvAndFugutiveEmissionModel>();
            model.SampleCollectionId = SampleCollectionId;
            model.EnquiryId = EnquiryId;
            model.EnquirySampleID = (long)EnquirySampleID;
            TempData.Remove("WorkplaceList");

            if (FieldId != 0)
            {
                FDWorkplaceInfo workplaceInfo = BALFactory.workplaceAndFugitiveEmissionBAL.GetWorkPlaceDetails((Int32)FieldId, (Int32)SampleCollectionId);
                if (workplaceInfo != null)
                {
                    //CoreFactory.workplaceAndFugitiveEmissionEntity = BALFactory.workplaceAndFugitiveEmissionBAL.GetWorkPlaceDetails((Int32)WorkplaceID);
                    model.WorkplaceID = (Int32)FieldId;
                    model.SampleCollectionId = SampleCollectionId;
                    model.EnquiryId = EnquiryId;
                    model.StatusId = workplaceInfo.WorkplaceDetails.StatusId;
                    model.CurrentStatus = workplaceInfo.WorkplaceDetails.CurrentStatus;
                    ViewBag.CurrentStatus = workplaceInfo.WorkplaceDetails.CurrentStatus;
                    model.SurveyDate = Convert.ToDateTime(workplaceInfo.WorkplaceDetails.SurveyDate).Date;
                    model.SurveyDateDisplay = Convert.ToDateTime(model.SurveyDate).ToString("dd/MM/yyyy");
                    model.InstrumentId = workplaceInfo.WorkplaceDetails.InstrumentId;
                    model.DurationOfSampling = (Int32)workplaceInfo.WorkplaceDetails.DurationOfSampling;
                    model.FilterPaperAnalyzed = workplaceInfo.WorkplaceDetails.FilterPaperAnalyzed;
                    model.FilterPaperNo = (Int32)workplaceInfo.WorkplaceDetails.FilterPaperNo;
                    model.FlowRate_Final = (Int32)workplaceInfo.WorkplaceDetails.FlowRate_Final;
                    model.FlowRate_Initial = (Int32)workplaceInfo.WorkplaceDetails.FlowRate_Initial;
                    model.FlowRate_Avg = (Int32)workplaceInfo.WorkplaceDetails.FlowRate_Avg;
                    //model.RelativeHumidity = CoreFactory.workplaceAndFugitiveEmissionEntity.RelativeHumidity;
                    model.RelativeHumidity = workplaceInfo.WorkplaceDetails.RelativeHumidity;
                    model.Temperature = workplaceInfo.WorkplaceDetails.Temperature;
                    model.AnyObservation = workplaceInfo.WorkplaceDetails.AnyObservation;

                    model.FilterPaperNo2 = workplaceInfo.WorkplaceDetails.FilterPaperNo2;
                    model.StartTime = workplaceInfo.WorkplaceDetails.StartTime;
                    model.EndTime = workplaceInfo.WorkplaceDetails.EndTime;
                    model.Sampling_Duration = workplaceInfo.WorkplaceDetails.Sampling_Duration;
                    model.AvgFlowRate = workplaceInfo.WorkplaceDetails.AvgFlowRate;
                    model.TotalVolAirPassed_L = workplaceInfo.WorkplaceDetails.TotalVolAirPassed_L;
                    model.TotalVolumeAirPassed_m3 = workplaceInfo.WorkplaceDetails.TotalVolumeAirPassed_m3;

                    int i = 1;
                    foreach (var grid1 in workplaceInfo.WorkplaceInfos)
                    {
                        try
                        {
                            model.GridModel.Add(new FDWorkplaceEnvAndFugutiveEmissionModel()
                            {
                                SrNo = i,
                                WorkplaceID = grid1.WorkplaceID,
                                WorkplaceGasID = grid1.WorkplaceGasID,
                                //GasesSampled = grid1.GasesSampled,
                                ParameterName = grid1.Parameters,
                                TestMethodName = grid1.TestMethodName,
                                InField = grid1.InField,
                                IsNABLAccredited = grid1.IsNABLAccredited,
                                RotameterFlow = grid1.RotameterFlow,
                                VolImpingingSol = grid1.VolImpingingSol,
                                BottleNo = grid1.BottleNo,
                                Duration = grid1.Duration,
                                Vs = grid1.Vs,
                                PreservationDone = grid1.PreservationDone,
                            });
                            i++;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    TempData["WorkplaceList"] = model.GridModel;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddWorkplaceEnvAndFugitiveEmissionField(FDWorkplaceEnvAndFugutiveEmissionModel model, string Save)
        {
            CoreFactory.workplaceAndFugitiveEmissionEntity = new WorkplaceAndFugitiveEmissionEntity();
            var workplaceList = (List<FDWorkplaceEnvAndFugutiveEmissionModel>)TempData.Peek("WorkplaceList");
            if (workplaceList != null)
            {
                if (workplaceList.Count > 0)
                {
                    foreach (var workplace in workplaceList)
                    {
                        //if (model.WorkplaceID == 0)
                        //{
                        CoreFactory.workplaceAndFugitiveEmissionEntity.WorkplaceID = Convert.ToByte(model.WorkplaceID);
                        CoreFactory.workplaceAndFugitiveEmissionEntity.SampleCollectionId = (long)model.SampleCollectionId;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.EnquiryId = model.EnquiryId;
                        //CoreFactory.workplaceAndFugitiveEmissionEntity.StatusId = (byte)iStatusId;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.SurveyDate = model.SurveyDate;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.InstrumentId = model.InstrumentId;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.DurationOfSampling = model.DurationOfSampling;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.FilterPaperAnalyzed = model.FilterPaperAnalyzed;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.FilterPaperNo = model.FilterPaperNo;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.FlowRate_Final = model.FlowRate_Final;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.FlowRate_Initial = model.FlowRate_Initial;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.FlowRate_Avg = model.FlowRate_Avg;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.RelativeHumidity = model.RelativeHumidity;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.Temperature = model.Temperature;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.AnyObservation = model.AnyObservation;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.FilterPaperNo2 = model.FilterPaperNo2;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.MatterSizeStartTime = model.MatterSizeStartTime;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.MatterSizeEndTime = model.MatterSizeEndTime;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.SamplingDurationDiff = model.SamplingDurationDiff;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.SamplingDurationFinal = model.SamplingDurationFinal;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.SamplingDurationInitial = model.SamplingDurationInitial;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.FlowRateDiffM = model.FlowRateDiffM;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.FlowRateInitialM = model.FlowRateInitialM;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.FlowRateFinalM = model.FlowRateFinalM;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.CycloneCupNo = model.CycloneCupNo;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.TimeTotalizerDiff = model.TimeTotalizerDiff;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.TimeTotalizerFinal = model.TimeTotalizerFinal;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.TimeTotalizerInitial = model.TimeTotalizerInitial;


                        CoreFactory.workplaceAndFugitiveEmissionEntity.FilterPaperNo2 = model.FilterPaperNo2;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.StartTime = model.StartTime;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.EndTime = model.EndTime;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.Sampling_Duration = model.Sampling_Duration;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.AvgFlowRate = model.AvgFlowRate;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.TotalVolAirPassed_L = model.TotalVolAirPassed_L;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.TotalVolumeAirPassed_m3 = model.TotalVolumeAirPassed_m3;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.IsActive = true;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.EnteredBy = LIMS.User.UserMasterID;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.EnteredDate = DateTime.UtcNow;
                        //}
                        CoreFactory.workplaceAndFugitiveEmissionEntity.WorkplaceID = Convert.ToByte(model.WorkplaceID);
                        CoreFactory.workplaceAndFugitiveEmissionEntity.WorkplaceGasID = workplace.WorkplaceGasID;
                        //CoreFactory.workplaceAndFugitiveEmissionEntity.GasesSampled = workplace.GasesSampled;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.Parameters = workplace.ParameterName;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.TestMethodName = workplace.TestMethodName;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.InField = workplace.InField;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.IsNABLAccredited = workplace.IsNABLAccredited;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.RotameterFlow = workplace.RotameterFlow;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.VolImpingingSol = workplace.VolImpingingSol;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.BottleNo = workplace.BottleNo;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.Duration = workplace.Duration;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.Vs = workplace.Vs;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.PreservationDone = workplace.PreservationDone;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.SampleCollectionId = model.SampleCollectionId.Value;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.IsActive = true;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.EnteredBy = LIMS.User.UserMasterID;
                        CoreFactory.workplaceAndFugitiveEmissionEntity.EnteredDate = DateTime.UtcNow;

                        if (Save == "FieldSave")
                        {
                            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("InProgress");
                            CoreFactory.workplaceAndFugitiveEmissionEntity.StatusId = (byte)iStatusId;
                            model.WorkplaceID = (Int32)BALFactory.workplaceAndFugitiveEmissionBAL.AddWorkplace(CoreFactory.workplaceAndFugitiveEmissionEntity);
                        }
                        else if (Save == "FieldSubmit")
                        {
                            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
                            CoreFactory.workplaceAndFugitiveEmissionEntity.StatusId = (byte)iStatusId;
                            model.WorkplaceID = (Int32)BALFactory.workplaceAndFugitiveEmissionBAL.AddWorkplace(CoreFactory.workplaceAndFugitiveEmissionEntity);
                        }
                        //model.WorkplaceID = (Int32)BALFactory.workplaceAndFugitiveEmissionBAL.AddWorkplace(CoreFactory.workplaceAndFugitiveEmissionEntity);
                    }
                }

            }
            else
            {
                CoreFactory.workplaceAndFugitiveEmissionEntity.SampleCollectionId = (long)model.SampleCollectionId;
                CoreFactory.workplaceAndFugitiveEmissionEntity.EnquiryId = model.EnquiryId;
                //CoreFactory.workplaceAndFugitiveEmissionEntity.StatusId = (byte)iStatusId;
                CoreFactory.workplaceAndFugitiveEmissionEntity.SurveyDate = model.SurveyDate;
                CoreFactory.workplaceAndFugitiveEmissionEntity.InstrumentId = model.InstrumentId;
                CoreFactory.workplaceAndFugitiveEmissionEntity.DurationOfSampling = model.DurationOfSampling;
                CoreFactory.workplaceAndFugitiveEmissionEntity.FilterPaperAnalyzed = model.FilterPaperAnalyzed;
                CoreFactory.workplaceAndFugitiveEmissionEntity.FilterPaperNo = model.FilterPaperNo;
                CoreFactory.workplaceAndFugitiveEmissionEntity.FlowRate_Final = model.FlowRate_Final;
                CoreFactory.workplaceAndFugitiveEmissionEntity.FlowRate_Initial = model.FlowRate_Initial;
                CoreFactory.workplaceAndFugitiveEmissionEntity.FlowRate_Avg = model.FlowRate_Avg;
                CoreFactory.workplaceAndFugitiveEmissionEntity.RelativeHumidity = model.RelativeHumidity;
                CoreFactory.workplaceAndFugitiveEmissionEntity.Temperature = model.Temperature;
                CoreFactory.workplaceAndFugitiveEmissionEntity.AnyObservation = model.AnyObservation;
                CoreFactory.workplaceAndFugitiveEmissionEntity.FilterPaperNo2 = model.FilterPaperNo2;
                CoreFactory.workplaceAndFugitiveEmissionEntity.MatterSizeStartTime = model.MatterSizeStartTime;
                CoreFactory.workplaceAndFugitiveEmissionEntity.MatterSizeEndTime = model.MatterSizeEndTime;
                CoreFactory.workplaceAndFugitiveEmissionEntity.SamplingDurationDiff = model.SamplingDurationDiff;
                CoreFactory.workplaceAndFugitiveEmissionEntity.SamplingDurationFinal = model.SamplingDurationFinal;
                CoreFactory.workplaceAndFugitiveEmissionEntity.SamplingDurationInitial = model.SamplingDurationInitial;
                CoreFactory.workplaceAndFugitiveEmissionEntity.FlowRateDiffM = model.FlowRateDiffM;
                CoreFactory.workplaceAndFugitiveEmissionEntity.FlowRateInitialM = model.FlowRateInitialM;
                CoreFactory.workplaceAndFugitiveEmissionEntity.FlowRateFinalM = model.FlowRateFinalM;
                CoreFactory.workplaceAndFugitiveEmissionEntity.CycloneCupNo = model.CycloneCupNo;
                CoreFactory.workplaceAndFugitiveEmissionEntity.TimeTotalizerDiff = model.TimeTotalizerDiff;
                CoreFactory.workplaceAndFugitiveEmissionEntity.TimeTotalizerFinal = model.TimeTotalizerFinal;
                CoreFactory.workplaceAndFugitiveEmissionEntity.TimeTotalizerInitial = model.TimeTotalizerInitial;


                CoreFactory.workplaceAndFugitiveEmissionEntity.FilterPaperNo2 = model.FilterPaperNo2;
                CoreFactory.workplaceAndFugitiveEmissionEntity.StartTime = model.StartTime;
                CoreFactory.workplaceAndFugitiveEmissionEntity.EndTime = model.EndTime;
                CoreFactory.workplaceAndFugitiveEmissionEntity.Sampling_Duration = model.Sampling_Duration;
                CoreFactory.workplaceAndFugitiveEmissionEntity.AvgFlowRate = model.AvgFlowRate;
                CoreFactory.workplaceAndFugitiveEmissionEntity.TotalVolAirPassed_L = model.TotalVolAirPassed_L;
                CoreFactory.workplaceAndFugitiveEmissionEntity.TotalVolumeAirPassed_m3 = model.TotalVolumeAirPassed_m3;
                CoreFactory.workplaceAndFugitiveEmissionEntity.IsActive = true;
                CoreFactory.workplaceAndFugitiveEmissionEntity.EnteredBy = LIMS.User.UserMasterID;
                CoreFactory.workplaceAndFugitiveEmissionEntity.EnteredDate = DateTime.UtcNow;
                if (Save == "FieldSave")
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("InProgress");
                    CoreFactory.workplaceAndFugitiveEmissionEntity.StatusId = (byte)iStatusId;
                    model.WorkplaceID = (Int32)BALFactory.workplaceAndFugitiveEmissionBAL.AddWorkplace(CoreFactory.workplaceAndFugitiveEmissionEntity);
                }
                else if (Save == "FieldSubmit")
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
                    CoreFactory.workplaceAndFugitiveEmissionEntity.StatusId = (byte)iStatusId;
                    model.WorkplaceID = (Int32)BALFactory.workplaceAndFugitiveEmissionBAL.AddWorkplace(CoreFactory.workplaceAndFugitiveEmissionEntity);
                }
            }
            return Json(new { WorkplaceID = model.WorkplaceID, message = "Values Entered successfully." }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult _FieldWorkplaceEnvAndFugitiveEmission(FDWorkplaceEnvAndFugutiveEmissionModel model)
        {
            return PartialView(model);
        }

        [HttpGet]
        public PartialViewResult _FieldWorkplaceEnvAndFugitiveEmissionList(int EnquirySampleID, int? Id = 0)
        {
            FDWorkplaceEnvAndFugutiveEmissionModel model = new FDWorkplaceEnvAndFugutiveEmissionModel();
            model.EnquirySampleID = EnquirySampleID;
            ViewBag.Parameter = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            ViewBag.TestMethod = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            if (Id != 0 && TempData["WorkplaceList"] != null)
            {
                var workplaceList = (List<FDWorkplaceEnvAndFugutiveEmissionModel>)TempData.Peek("WorkplaceList");
                TempData.Keep("WorkplaceList");
                model = workplaceList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _FieldWorkplaceEnvAndFugitiveEmissionList(FDWorkplaceEnvAndFugutiveEmissionModel model)
        {
            List<FDWorkplaceEnvAndFugutiveEmissionModel> workplaceList = new List<FDWorkplaceEnvAndFugutiveEmissionModel>();
            if (TempData["WorkplaceList"] != null)
            {
                workplaceList = (List<FDWorkplaceEnvAndFugutiveEmissionModel>)TempData["WorkplaceList"];
            }
            if (model.SrNo == 0)
            {
                model.SrNo = workplaceList.Count() + 1;
                var result = BALFactory.samplearrivalBAL.GetInFieldIsNabl((Int32)model.EnquirySampleID, model.ParameterMasterId, model.TestMethodId);
                if (result != null)
                {
                    model.InField = result.InField;
                    model.IsNABLAccredited = result.IsNABLAccredited;
                }
                workplaceList.Add(model);
            }
            else
            {
                var workplace = workplaceList.Where(c => c.SrNo == model.SrNo).FirstOrDefault();
                //workplace.GasesSampled = model.GasesSampled;
                workplace.Parameters = model.ParameterName;
                workplace.InField = model.InField;
                workplace.IsNABLAccredited = model.IsNABLAccredited;
                if (model.TestMethodId == 0 || model.TestMethodId == null)
                {
                    workplace.TestMethodName = "";
                }
                else
                {
                    workplace.TestMethodName = model.TestMethodName;
                }
                workplace.RotameterFlow = model.RotameterFlow;
                workplace.VolImpingingSol = model.VolImpingingSol;
                workplace.BottleNo = model.BottleNo;
                workplace.Duration = model.Duration;
                workplace.Vs = model.Vs;
                workplace.PreservationDone = model.PreservationDone;
                //FieldDetermination / FieldWorkplaceEnvAndFugitiveEmission ? SampleCollectionId = 22 & EnquiryId = 34 & EnquirySampleID = 89
            }

            TempData["WorkplaceList"] = workplaceList;
            return Json(new { status = "success", message = "Added" }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult _FieldWorkplaceEnvAndFugitiveEmissionGrid(FDWorkplaceEnvAndFugutiveEmissionModel model)
        {
            if (TempData["WorkplaceList"] != null)
            {
                model.GridModel = (List<FDWorkplaceEnvAndFugutiveEmissionModel>)TempData.Peek("WorkplaceList");
                TempData.Keep("WorkplaceList");
            }
            return PartialView(model);
        }
        public JsonResult _DeleteWorkplaceField(int? Id = 0)
        {
            var workplaceList = (List<FDWorkplaceEnvAndFugutiveEmissionModel>)TempData.Peek("WorkplaceList");
            var workplace = workplaceList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            if (workplace.WorkplaceID != 0)
            {
                BALFactory.workplaceAndFugitiveEmissionBAL.DeleteWorkplaceField(workplace.WorkplaceID, workplace.WorkplaceGasID);
            }
            workplaceList.Remove(workplace);
            int i = 1;
            foreach (var item in workplaceList)
            {
                item.SrNo = i;
                i++;
            }
            TempData["WorkplaceList"] = workplaceList;
            return Json(new { status = "success", message = "deleted" }, JsonRequestBehavior.AllowGet);
        }
    }
}