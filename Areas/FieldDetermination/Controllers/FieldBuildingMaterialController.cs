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
    public class FieldBuildingMaterialController : Controller
    {
        string strStatus = "";
        public FieldBuildingMaterialController()
        {
            BALFactory.buildingMaterialBAL = new BuildingMaterialBAL();
            BALFactory.enquiryBAL = new BAL.Enquiry.EnquiryBAL();
            BALFactory.samplecollectionBAL = new BAL.Collection.SampleCollectionBAL();
            BALFactory.samplearrivalBAL = new SampleArrivalBAL();
            BALFactory.dropdownsBAL = new DropDownBal();

        }
        // GET: FieldDetermination/FieldBuildingMaterial
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FieldBuildingMaterial(int? SampleCollectionId = 0, int? EnquiryId = 0, int? EnquirySampleID = 0, int? FieldId = 0)
        {
            FDBuildingMaterialModel model = new FDBuildingMaterialModel();
            model.GridModel = new List<FDBuildingMaterialModel>();
            model.SampleCollectionId = SampleCollectionId;
            model.EnquiryId = EnquiryId;
            model.EnquirySampleID = (long)EnquirySampleID;
            TempData.Remove("BuildingMaterialList");
            if (FieldId != 0)
            {
                FDBuildingInfo buildingInfo = BALFactory.buildingMaterialBAL.GetBuildingMaterialDetails((Int32)FieldId, (Int32)SampleCollectionId);
                if (buildingInfo != null)
                {
                    model.FieldBuildingMaterialId = (Int32)FieldId;
                    model.SampleCollectionId = SampleCollectionId;
                    model.EnquiryId = EnquiryId;
                    model.AnyOtherObservation = buildingInfo.BuildingeDetails.AnyOtherObservation;
                    model.CurrentStatus = buildingInfo.BuildingeDetails.CurrentStatus;
                    model.ParameterName = buildingInfo.BuildingeDetails.Parameters;
                    model.TestMethodName = buildingInfo.BuildingeDetails.TestMethodName;
                    model.InField = buildingInfo.BuildingeDetails.InField;
                    model.IsNABLAccredited = buildingInfo.BuildingeDetails.IsNABLAccredited;
                    ViewBag.CurrentStatus = buildingInfo.BuildingeDetails.CurrentStatus;
                    int i = 1;
                    foreach (var grid1 in buildingInfo.BuildingInfos)
                    {
                        try
                        {
                            model.GridModel.Add(new FDBuildingMaterialModel()
                            {
                                SrNo = i,
                                FieldBuildingMaterialId = grid1.FieldBuildingMaterialId,
                                ParameterName = grid1.Parameters,
                                TestMethodName = grid1.TestMethodName,
                                InField = grid1.InField,
                                IsNABLAccredited = grid1.IsNABLAccredited,
                                TestResults = grid1.TestResults,
                                AnyOtherObservation = grid1.AnyOtherObservation,
                            });
                            i++;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    TempData["BuildingMaterialList"] = model.GridModel;
                }
            }

            return View(model);
        }
        [HttpPost]
        public JsonResult FieldBuildingMaterial(FDBuildingMaterialModel model, string Save)
        {
            CoreFactory.buildingMaterialEntity = new BuildingMaterialEntity();
            var buildingMaterialList = (List<FDBuildingMaterialModel>)TempData.Peek("BuildingMaterialList");
            if (buildingMaterialList != null)
            {
                if (buildingMaterialList.Count > 0)
                {
                    foreach (var buildingMaterial in buildingMaterialList)
                    {
                        // CoreFactory.buildingMaterialEntity.FieldBuildingMaterialId = Convert.ToByte(model.FieldBuildingMaterialId);
                        CoreFactory.buildingMaterialEntity.FieldBuildingMaterialId = Convert.ToByte(buildingMaterial.FieldBuildingMaterialId);
                        CoreFactory.buildingMaterialEntity.SampleCollectionId = (long)model.SampleCollectionId;
                        CoreFactory.buildingMaterialEntity.EnquiryId = model.EnquiryId;
                        //CoreFactory.buildingMaterialEntity.StatusId = (byte)iStatusId;
                        CoreFactory.buildingMaterialEntity.AnyOtherObservation = model.AnyOtherObservation;
                        CoreFactory.buildingMaterialEntity.Parameters = buildingMaterial.ParameterName;
                        CoreFactory.buildingMaterialEntity.TestMethodName = buildingMaterial.TestMethodName;
                        CoreFactory.buildingMaterialEntity.InField = buildingMaterial.InField;
                        CoreFactory.buildingMaterialEntity.IsNABLAccredited = buildingMaterial.IsNABLAccredited;
                        CoreFactory.buildingMaterialEntity.TestResults = buildingMaterial.TestResults;
                        CoreFactory.buildingMaterialEntity.IsActive = true;
                        CoreFactory.buildingMaterialEntity.EnteredBy = LIMS.User.UserMasterID;
                        CoreFactory.buildingMaterialEntity.EnteredDate = DateTime.UtcNow;
                        CoreFactory.buildingMaterialEntity.EnteredDate = DateTime.UtcNow;
                        if (Save == "FieldSave")
                        {
                            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("InProgress");
                            CoreFactory.buildingMaterialEntity.StatusId = (byte)iStatusId;
                            strStatus = BALFactory.buildingMaterialBAL.AddBuildingMaterial(CoreFactory.buildingMaterialEntity);
                        }
                        else if (Save == "FieldSubmit")
                        {
                            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
                            CoreFactory.buildingMaterialEntity.StatusId = (byte)iStatusId;
                            strStatus = BALFactory.buildingMaterialBAL.AddBuildingMaterial(CoreFactory.buildingMaterialEntity);
                        }
                        //strStatus = BALFactory.buildingMaterialBAL.AddBuildingMaterial(CoreFactory.buildingMaterialEntity);
                    }
                }
            }
            else
            {
                CoreFactory.buildingMaterialEntity.SampleCollectionId = (long)model.SampleCollectionId;
                CoreFactory.buildingMaterialEntity.EnquiryId = model.EnquiryId;
                CoreFactory.buildingMaterialEntity.AnyOtherObservation = model.AnyOtherObservation;
                CoreFactory.buildingMaterialEntity.IsActive = true;
                CoreFactory.buildingMaterialEntity.EnteredBy = LIMS.User.UserMasterID;
                CoreFactory.buildingMaterialEntity.EnteredDate = DateTime.UtcNow;
                CoreFactory.buildingMaterialEntity.EnteredDate = DateTime.UtcNow;
                if (Save == "FieldSave")
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("InProgress");
                    CoreFactory.buildingMaterialEntity.StatusId = (byte)iStatusId;
                    strStatus = BALFactory.buildingMaterialBAL.AddBuildingMaterial(CoreFactory.buildingMaterialEntity);
                }
                else if (Save == "FieldSubmit")
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
                    CoreFactory.buildingMaterialEntity.StatusId = (byte)iStatusId;
                    strStatus = BALFactory.buildingMaterialBAL.AddBuildingMaterial(CoreFactory.buildingMaterialEntity);
                }
            }
            return Json(new { Status = strStatus, message = "Values Entered successfully." }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult _FieldBuildingMaterial(int EnquirySampleID, int? Id = 0)
        {
            FDBuildingMaterialModel model = new FDBuildingMaterialModel();
            model.EnquirySampleID = EnquirySampleID;
            ViewBag.Parameter = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            ViewBag.TestMethod = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            if (Id != 0 && TempData["BuildingMaterialList"] != null)
            {
                var buildingMaterialList = (List<FDBuildingMaterialModel>)TempData.Peek("BuildingMaterialList");
                TempData.Keep("BuildingMaterialList");
                model = buildingMaterialList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            }
            return PartialView(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _FieldBuildingMaterial(FDBuildingMaterialModel model)
        {
            List<FDBuildingMaterialModel> buildingMaterialList = new List<FDBuildingMaterialModel>();
            if (TempData["BuildingMaterialList"] != null)
            {
                buildingMaterialList = (List<FDBuildingMaterialModel>)TempData["BuildingMaterialList"];
            }
            if (model.SrNo == 0)
            {
                model.SrNo = buildingMaterialList.Count() + 1;
                var result = BALFactory.samplearrivalBAL.GetInFieldIsNabl((Int32)model.EnquirySampleID, model.ParameterMasterId, model.TestMethodId);
                if (result != null)
                {
                    model.InField = result.InField;
                    model.IsNABLAccredited = result.IsNABLAccredited;
                }
                buildingMaterialList.Add(model);
            }
            else
            {
                var solidwaste = buildingMaterialList.Where(c => c.SrNo == model.SrNo).FirstOrDefault();
                solidwaste.ParameterName = model.ParameterName;
                solidwaste.InField = model.InField;
                solidwaste.IsNABLAccredited = model.IsNABLAccredited;
                if (model.TestMethodId == 0 || model.TestMethodId != null)
                {
                    solidwaste.TestMethodName = "";
                }
                else
                {
                    solidwaste.TestMethodName = model.TestMethodName;
                }
                solidwaste.TestResults = model.TestResults;


            }
            TempData["BuildingMaterialList"] = buildingMaterialList;
            return Json(new { status = "success", message = "Added" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public PartialViewResult _FieldBuildingMaterialList(FDBuildingMaterialModel model)
        {
            if (TempData["BuildingMaterialList"] != null)
            {
                model.GridModel = (List<FDBuildingMaterialModel>)TempData.Peek("BuildingMaterialList");
                TempData.Keep("BuildingMaterialList");
            }

            return PartialView(model);
        }
        public JsonResult _DeleteBuildingMaterialField(int? Id = 0)
        {
            var buildingMaterialList = (List<FDBuildingMaterialModel>)TempData.Peek("BuildingMaterialList");
            var buildingMaterial = buildingMaterialList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            if (buildingMaterial.FieldBuildingMaterialId != 0)
            {
                BALFactory.buildingMaterialBAL.DeleteBuildingMaterialField(buildingMaterial.FieldBuildingMaterialId);
            }
            buildingMaterialList.Remove(buildingMaterial);
            int i = 1;
            foreach (var item in buildingMaterialList)
            {
                item.SrNo = i;
                i++;
            }
            TempData["BuildingMaterialList"] = buildingMaterialList;
            return Json(new { status = "success", message = "deleted" }, JsonRequestBehavior.AllowGet);
        }

    }
}