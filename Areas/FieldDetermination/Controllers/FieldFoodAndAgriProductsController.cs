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
    public class FieldFoodAndAgriProductsController : Controller
    {
        string strStatus = "";
        public FieldFoodAndAgriProductsController()
        {
            BALFactory.foodAndAgriProductsBAL = new FoodAndAgriProductsBAL();
            BALFactory.enquiryBAL = new BAL.Enquiry.EnquiryBAL();
            BALFactory.samplecollectionBAL = new BAL.Collection.SampleCollectionBAL();
            BALFactory.samplearrivalBAL = new SampleArrivalBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
        }
        // GET: FieldDetermination/FieldFoodAndAgriProducts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FieldFoodAndAgriProducts(int? SampleCollectionId = 0, int? EnquiryId = 0, int? EnquirySampleID = 0, int? FieldId = 0)
        {
            FDFoodAndAgriProductsModel model = new FDFoodAndAgriProductsModel();
            model.GridModel = new List<FDFoodAndAgriProductsModel>();
            model.SampleCollectionId = SampleCollectionId;
            model.EnquiryId = EnquiryId;
            model.EnquirySampleID = (long)EnquirySampleID;
            TempData.Remove("FoodProductsList");
            if (FieldId != 0)
            {
                FDFoodInfo foodInfo = BALFactory.foodAndAgriProductsBAL.GetFoodDetails((Int32)FieldId, (Int32)SampleCollectionId);
                if (foodInfo != null)
                {
                    model.FieldFoodAndAgriCultureId = (Int32)FieldId;
                    model.SampleCollectionId = SampleCollectionId;
                    model.EnquiryId = EnquiryId;
                    model.StatusId = foodInfo.FoodDetails.StatusId;
                    model.CurrentStatus = foodInfo.FoodDetails.CurrentStatus;
                    ViewBag.CurrentStatus = foodInfo.FoodDetails.CurrentStatus;
                    model.AnyOtherObservation = foodInfo.FoodDetails.AnyOtherObservation;
                    model.ParameterName = foodInfo.FoodDetails.Parameters;
                    model.TestMethodName = foodInfo.FoodDetails.TestMethodName;
                    model.InField = foodInfo.FoodDetails.InField;
                    model.IsNABLAccredited = foodInfo.FoodDetails.IsNABLAccredited;
                    model.TestResults = foodInfo.FoodDetails.TestResults;
                    int i = 1;
                    foreach (var grid1 in foodInfo.FoodInfos)
                    {
                        try
                        {
                            model.GridModel.Add(new FDFoodAndAgriProductsModel()
                            {
                                SrNo = i,
                                FieldFoodAndAgriCultureId = grid1.FieldFoodAndAgriCultureId,
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

                    TempData["FoodProductsList"] = model.GridModel;
                }
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult FieldFoodAndAgriProducts(FDFoodAndAgriProductsModel model, String Save)
        {
            CoreFactory.foodAndAgriProductsEntity = new FoodAndAgriProductsEntity();
            var foodProductsList = (List<FDFoodAndAgriProductsModel>)TempData.Peek("FoodProductsList");
            if (foodProductsList.Count > 0)
            {
                foreach (var foodAndAgriProducts in foodProductsList)
                {
                    CoreFactory.foodAndAgriProductsEntity.FieldFoodAndAgriCultureId = foodAndAgriProducts.FieldFoodAndAgriCultureId;
                    CoreFactory.foodAndAgriProductsEntity.SampleCollectionId = (long)model.SampleCollectionId;
                    CoreFactory.foodAndAgriProductsEntity.EnquiryId = model.EnquiryId;
                    CoreFactory.foodAndAgriProductsEntity.AnyOtherObservation = model.AnyOtherObservation;
                    CoreFactory.foodAndAgriProductsEntity.Parameters = foodAndAgriProducts.ParameterName;
                    CoreFactory.foodAndAgriProductsEntity.TestMethodName = foodAndAgriProducts.TestMethodName;
                    CoreFactory.foodAndAgriProductsEntity.InField = foodAndAgriProducts.InField;
                    CoreFactory.foodAndAgriProductsEntity.IsNABLAccredited = foodAndAgriProducts.IsNABLAccredited;
                    CoreFactory.foodAndAgriProductsEntity.TestResults = foodAndAgriProducts.TestResults;
                    CoreFactory.foodAndAgriProductsEntity.IsActive = true;
                    CoreFactory.foodAndAgriProductsEntity.EnteredBy = LIMS.User.UserMasterID;
                    CoreFactory.foodAndAgriProductsEntity.EnteredDate = DateTime.UtcNow;
                    if (Save == "FieldSave")
                    {
                        int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("InProgress");
                        CoreFactory.foodAndAgriProductsEntity.StatusId = (byte)iStatusId;
                        strStatus = BALFactory.foodAndAgriProductsBAL.AddFoodProducts(CoreFactory.foodAndAgriProductsEntity);
                    }
                    else if (Save == "FieldSubmit")
                    {
                        int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
                        CoreFactory.foodAndAgriProductsEntity.StatusId = (byte)iStatusId;
                        strStatus = BALFactory.foodAndAgriProductsBAL.AddFoodProducts(CoreFactory.foodAndAgriProductsEntity);
                    }
                }
            }
            return Json(new { Status = strStatus, message = "Values Entered successfully." }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _FieldFoodAndAgriProducts(int EnquirySampleID, int? Id = 0)
        {
            FDFoodAndAgriProductsModel model = new FDFoodAndAgriProductsModel();
            model.EnquirySampleID = EnquirySampleID;
            ViewBag.parameter = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            ViewBag.TestMethod = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            if (Id != 0 && TempData["FoodProductsList"] != null)
            {
                var foodProductsList = (List<FDFoodAndAgriProductsModel>)TempData.Peek("FoodProductsList");
                TempData.Keep("FoodProductsList");
                model = foodProductsList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            }
            return PartialView(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _FieldFoodAndAgriProducts(FDFoodAndAgriProductsModel model)
        {
            List<FDFoodAndAgriProductsModel> foodProductsList = new List<FDFoodAndAgriProductsModel>();
            if (TempData["FoodProductsList"] != null)
            {
                foodProductsList = (List<FDFoodAndAgriProductsModel>)TempData["FoodProductsList"];
            }
            if (model.SrNo == 0)
            {
                model.SrNo = foodProductsList.Count() + 1;
                var result = BALFactory.samplearrivalBAL.GetInFieldIsNabl((Int32)model.EnquirySampleID, model.ParameterMasterId, model.TestMethodId);
                if (result != null)
                {
                    model.InField = result.InField;
                    model.IsNABLAccredited = result.IsNABLAccredited;
                }
                foodProductsList.Add(model);
            }
            else
            {
                var foodProducts = foodProductsList.Where(c => c.SrNo == model.SrNo).FirstOrDefault();
                foodProducts.ParameterName = model.ParameterName;
                foodProducts.InField = model.InField;
                foodProducts.IsNABLAccredited = model.IsNABLAccredited;
                if (model.TestMethodId == 0 || model.TestMethodId != null)
                {
                    foodProducts.TestMethodName = "";
                }
                else
                {
                    foodProducts.TestMethodName = model.TestMethodName;
                }
                foodProducts.TestResults = model.TestResults;


            }
            TempData["FoodProductsList"] = foodProductsList;
            return Json(new { status = "success", message = "Added" }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult _FieldFoodAndAgriProductsList(FDFoodAndAgriProductsModel model)
        {
            if (TempData["FoodProductsList"] != null)
            {
                model.GridModel = (List<FDFoodAndAgriProductsModel>)TempData.Peek("FoodProductsList");
                TempData.Keep("FoodProductsList");
            }
            return PartialView(model);
        }
        public JsonResult _DeleteFoodAgriField(int? Id = 0)
        {
            var foodProductsList = (List<FDFoodAndAgriProductsModel>)TempData.Peek("FoodProductsList");
            var foodProducts = foodProductsList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            if (foodProducts.FieldFoodAndAgriCultureId != 0)
            {
                BALFactory.foodAndAgriProductsBAL.DeleteFoodAgriField(foodProducts.FieldFoodAndAgriCultureId);
            }
            foodProductsList.Remove(foodProducts);
            int i = 1;
            foreach (var item in foodProductsList)
            {
                item.SrNo = i;
                i++;
            }
            TempData["FoodProductsList"] = foodProductsList;
            return Json(new { status = "success", message = "deleted" }, JsonRequestBehavior.AllowGet);
        }
    }
}
