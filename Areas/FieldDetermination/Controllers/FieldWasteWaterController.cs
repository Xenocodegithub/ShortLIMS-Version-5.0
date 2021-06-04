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
    public class FieldWasteWaterController : Controller
    {
        string strStatus = "";
        public FieldWasteWaterController()
        {
            BALFactory.wasteWaterBAL = new WasteWaterBAL();
            BALFactory.enquiryBAL = new BAL.Enquiry.EnquiryBAL();
            BALFactory.samplecollectionBAL = new BAL.Collection.SampleCollectionBAL();
            BALFactory.samplearrivalBAL = new SampleArrivalBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
        }

        // GET: FieldDetermination/FieldWasteWater
        public ActionResult FieldWasteWater(int? SampleCollectionId = 0, int? EnquiryId = 0, int? EnquirySampleID = 0, int? FieldId = 0, int? ParameterMappingId = 0)
        {
            FDWasteWaterModel model = new FDWasteWaterModel();
            model.GridModel = new List<FDWasteWaterModel>();
            model.SampleCollectionId = SampleCollectionId;
            model.EnquiryId = EnquiryId;
            model.EnquirySampleID = (long)EnquirySampleID;
            model.ParameterMappingId = ParameterMappingId;
            //model.WasteWaterID = (Int32)FieldId;
            ViewBag.SampleCollectionId = SampleCollectionId;
            TempData.Remove("WasteWaterList");
            //TempData.Clear();
            if (FieldId != 0)
            {
                {
                    FDWaterInfo waterInfo = BALFactory.wasteWaterBAL.GetWasteWaterDetails((Int32)FieldId, (Int32)SampleCollectionId);
                    if (waterInfo != null)
                    {
                        //CoreFactory.wasteWaterEntity = BALFactory.wasteWaterBAL.GetWasteWaterDetails((Int32)WasteWaterID);
                        model.WasteWaterID = (Int32)FieldId;
                        model.SampleCollectionId = SampleCollectionId;
                        model.EnquiryId = EnquiryId;
                        model.StatusId = waterInfo.WaterDetails.StatusId;
                        //model.FDWater.ParameterMappingId = ParameterMappingId;
                        model.CurrentStatus = waterInfo.WaterDetails.CurrentStatus;
                        ViewBag.CurrentStatus = waterInfo.WaterDetails.CurrentStatus;
                        model.AnyOtherObservation = waterInfo.WaterDetails.AnyOtherObservation;
                        model.WaterUse = waterInfo.WaterDetails.WaterUse;
                        //model.Parameters = waterInfo.WaterDetails.Parameters;
                        model.ParameterName = waterInfo.WaterDetails.Parameters;
                        model.TestMethodName = waterInfo.WaterDetails.TestMethodName;
                        model.InField = waterInfo.WaterDetails.InField;
                        model.IsNABLAccredited = waterInfo.WaterDetails.IsNABLAccredited;
                        model.TestResults = waterInfo.WaterDetails.TestResults;
                        int i = 1;

                        foreach (var grid1 in waterInfo.WaterInfos)
                        {
                            try
                            {
                                model.GridModel.Add(new FDWasteWaterModel()
                                {
                                    SrNo = i,
                                    ParameterName = grid1.Parameters,
                                    TestMethodName = grid1.TestMethodName,
                                    InField = grid1.InField,
                                    IsNABLAccredited = grid1.IsNABLAccredited,
                                    //Parameters = grid1.Parameters,
                                    TestResults = grid1.TestResults,
                                    AnyOtherObservation = grid1.AnyOtherObservation,
                                    WaterUse = grid1.WaterUse,
                                    WasteWaterID = grid1.WasteWaterID,
                                });
                                i++;
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        TempData["WasteWaterList"] = model.GridModel;
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult FieldWasteWater(FDWasteWaterModel model, string Save)
        {
            CoreFactory.wasteWaterEntity = new WasteWaterEntity();
            var wasteWaterList = (List<FDWasteWaterModel>)TempData.Peek("WasteWaterList");

            //////For Save and Submit with Status Update///////
            /* if (Save == "FieldSave")//May be Modified Later
             {
                 int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("InProgress");
                CoreFactory.wasteWaterEntity.StatusId = (byte)iStatusId;
             }*/

            //if (Save == "FieldSubmit")
            //{
            // int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
            if (wasteWaterList != null)
            {
                if (wasteWaterList.Count > 0)
                {
                    foreach (var wastewater in wasteWaterList)
                    {
                        //CoreFactory.wasteWaterEntity.WasteWaterID = Convert.ToInt32(model.WasteWaterID);
                        CoreFactory.wasteWaterEntity.WasteWaterID = Convert.ToInt32(wastewater.WasteWaterID);
                        CoreFactory.wasteWaterEntity.SampleCollectionId = (long)model.SampleCollectionId;
                        CoreFactory.wasteWaterEntity.EnquiryId = model.EnquiryId;
                        //CoreFactory.wasteWaterEntity.StatusId = (byte)iStatusId;
                        CoreFactory.wasteWaterEntity.AnyOtherObservation = model.AnyOtherObservation;
                        CoreFactory.wasteWaterEntity.WaterUse = model.WaterUse;
                        //CoreFactory.wasteWaterEntity.Parameters = wastewater.Parameters;
                        CoreFactory.wasteWaterEntity.Parameters = wastewater.ParameterName;
                        CoreFactory.wasteWaterEntity.TestMethodName = wastewater.TestMethodName;
                        CoreFactory.wasteWaterEntity.InField = wastewater.InField;
                        CoreFactory.wasteWaterEntity.IsNABLAccredited = wastewater.IsNABLAccredited;
                        CoreFactory.wasteWaterEntity.TestResults = wastewater.TestResults;
                        CoreFactory.wasteWaterEntity.ParameterMappingId = wastewater.ParameterMappingId;
                        CoreFactory.wasteWaterEntity.IsActive = true;
                        CoreFactory.wasteWaterEntity.EnteredBy = LIMS.User.UserMasterID;
                        CoreFactory.wasteWaterEntity.EnteredDate = DateTime.UtcNow;
                        if (Save == "FieldSave")
                        {
                            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("InProgress");
                            CoreFactory.wasteWaterEntity.StatusId = (byte)iStatusId;
                            strStatus = BALFactory.wasteWaterBAL.AddWasteWater(CoreFactory.wasteWaterEntity);
                        }
                        else if (Save == "FieldSubmit")
                        {
                            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
                            CoreFactory.wasteWaterEntity.StatusId = (byte)iStatusId;
                            strStatus = BALFactory.wasteWaterBAL.AddWasteWater(CoreFactory.wasteWaterEntity);
                        }
                        //strStatus = BALFactory.wasteWaterBAL.AddWasteWater(CoreFactory.wasteWaterEntity);
                    }
                }
            }
            else
            {
                CoreFactory.wasteWaterEntity.SampleCollectionId = (long)model.SampleCollectionId;
                CoreFactory.wasteWaterEntity.EnquiryId = model.EnquiryId;
                //CoreFactory.wasteWaterEntity.StatusId = (byte)iStatusId;
                CoreFactory.wasteWaterEntity.AnyOtherObservation = model.AnyOtherObservation;
                CoreFactory.wasteWaterEntity.WaterUse = model.WaterUse;
                CoreFactory.wasteWaterEntity.IsActive = true;
                CoreFactory.wasteWaterEntity.EnteredBy = LIMS.User.UserMasterID;
                CoreFactory.wasteWaterEntity.EnteredDate = DateTime.UtcNow;
                if (Save == "FieldSave")
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("InProgress");
                    CoreFactory.wasteWaterEntity.StatusId = (byte)iStatusId;
                    strStatus = BALFactory.wasteWaterBAL.AddWasteWater(CoreFactory.wasteWaterEntity);
                }
                else if (Save == "FieldSubmit")
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
                    CoreFactory.wasteWaterEntity.StatusId = (byte)iStatusId;
                    strStatus = BALFactory.wasteWaterBAL.AddWasteWater(CoreFactory.wasteWaterEntity);
                }
            }            //}
            //strStatus = BALFactory.wasteWaterBAL.AddWasteWater(CoreFactory.wasteWaterEntity);
            return Json(new { Status = strStatus, message = "Values Entered successfully." }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _FieldWaterUse(int? WasteWaterID = 0, int? SampleCollectionId = 0)
        {
            FDWasteWaterModel model = new FDWasteWaterModel();
            model.WasteWaterID = (Int32)WasteWaterID;
            if (WasteWaterID != 0)
            {
                FDWaterInfo waterInfo = BALFactory.wasteWaterBAL.GetWasteWaterDetails((Int32)WasteWaterID, (Int32)SampleCollectionId);
                //CoreFactory.wasteWaterEntity = BALFactory.wasteWaterBAL.GetWasteWaterDetails((Int32)WasteWaterID);
                model.WasteWaterID = (Int32)WasteWaterID;
                model.AnyOtherObservation = waterInfo.WaterDetails.AnyOtherObservation;
                model.WaterUse = waterInfo.WaterDetails.WaterUse;
            }
            return PartialView(model);
        }

        public PartialViewResult _FieldWasteWater(int EnquirySampleID, int? Id = 0)
        {
            FDWasteWaterModel model = new FDWasteWaterModel();
            model.EnquirySampleID = EnquirySampleID;
            ViewBag.Parameter = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            ViewBag.TestMethod = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            if (Id != 0 && TempData["WasteWaterList"] != null)
            {
                var wastewaterList = (List<FDWasteWaterModel>)TempData.Peek("WasteWaterList");
                TempData.Keep("WasteWaterList");
                model = wastewaterList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _FieldWasteWater(FDWasteWaterModel model)
        {
            List<FDWasteWaterModel> wastewaterList = new List<FDWasteWaterModel>();
            if (TempData["WasteWaterList"] != null)
            {
                wastewaterList = (List<FDWasteWaterModel>)TempData["WasteWaterList"];
            }
            if (model.SrNo == 0)
            {
                model.SrNo = wastewaterList.Count() + 1;
                var result = BALFactory.samplearrivalBAL.GetInFieldIsNabl((Int32)model.EnquirySampleID, model.ParameterMasterId, model.TestMethodId);
                if (result != null)
                {
                    model.InField = result.InField;
                    model.IsNABLAccredited = result.IsNABLAccredited;
                }

                wastewaterList.Add(model);
            }
            else
            {
                var wastewater = wastewaterList.Where(c => c.SrNo == model.SrNo).FirstOrDefault();
                //wastewater.Parameters = model.Parameters;
                wastewater.ParameterName = model.ParameterName;
                wastewater.InField = model.InField;
                wastewater.IsNABLAccredited = model.IsNABLAccredited;
                if (model.TestMethodId == 0 || model.TestMethodId == null)
                {
                    wastewater.TestMethodName = "";
                }
                else
                {
                    wastewater.TestMethodName = model.TestMethodName;
                }
                wastewater.TestResults = model.TestResults;
            }
            TempData["WasteWaterList"] = wastewaterList;
            return Json(new { status = "success", message = "Added" }, JsonRequestBehavior.AllowGet);
        }

       
        [HttpGet]
        public PartialViewResult _FDWasteWaterList(FDWasteWaterModel model, int? WasteWaterID = 0)
        {
           
            if (TempData["WasteWaterList"] != null)
            {
                model.GridModel = (List<FDWasteWaterModel>)TempData.Peek("WasteWaterList");
                TempData.Keep("WasteWaterList");
            }
           
            return PartialView(model);
        }

    
        public JsonResult _DeleteField(int? Id = 0)
        {
            var wastewaterList = (List<FDWasteWaterModel>)TempData.Peek("WasteWaterList");
            var wastewater = wastewaterList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            if (wastewater.WasteWaterID != 0)
            {
                BALFactory.wasteWaterBAL.DeleteWaterField(wastewater.WasteWaterID);
            }
            wastewaterList.Remove(wastewater);
            int i = 1;
            foreach (var item in wastewaterList)
            {
                item.SrNo = i;
                i++;
            }
            TempData["WasteWaterList"] = wastewaterList;
            return Json(new { status = "success", message = "deleted" }, JsonRequestBehavior.AllowGet);
        }

        
        public PartialViewResult _FieldParameterUnit(int EnquirySampleID)
        {
            CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            List<SampleArrivalModel> modelList = new List<SampleArrivalModel>();
            int iSrNo = 1;
            foreach (var item in CoreFactory.samplearrivalList)
            {
                modelList.Add(new SampleArrivalModel()
                {
                    SerialNo = iSrNo,
                    EnquirySampleID = item.EnquirySampleID,
                    EnquiryParameterDetailID = item.EnquiryParameterDetailID,
                    ParameterMappingId = item.ParameterMappingId,
                    UnitId = item.UnitId,
                    Unit = item.Unit,
                    ParameterMasterId = item.ParameterMasterId,
                    ParameterName = item.ParameterName,
                    InField = item.InField,
                    Field = item.InField == false ? "NO" : "YES",

                });
                iSrNo++;
            }
            return PartialView(modelList);
        }

       
        public JsonResult GetTestMethod(int EnquirySampleID, int ParameterMasterId)
        {
            var tmList = BALFactory.samplearrivalBAL.GetTestMethod(EnquirySampleID, ParameterMasterId);
            return Json(new { result = tmList }, JsonRequestBehavior.AllowGet);
        }
    }
}