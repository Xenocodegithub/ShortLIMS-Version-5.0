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
    public class FieldCoalCokeSolidFuelController : Controller
    {
        string strStatus = "";
        public FieldCoalCokeSolidFuelController()
        {
            BALFactory.coalCokeSolidFuelsBAL = new CoalCokeSolidFuelsBAL();
            BALFactory.enquiryBAL = new BAL.Enquiry.EnquiryBAL();
            BALFactory.samplecollectionBAL = new BAL.Collection.SampleCollectionBAL();
            BALFactory.samplearrivalBAL = new SampleArrivalBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
        }
        // GET: FieldDetermination/FieldCoalCokeSolidFuel
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FieldCoalCokeSolidFuel(int? SampleCollectionId = 0, int? EnquiryId = 0, int? EnquirySampleID = 0, int? FieldId = 0, int? ParameterMappingId = 0)
        {
            FDCoalCokeSolidFuelModel model = new FDCoalCokeSolidFuelModel();
            model.GridModel = new List<FDCoalCokeSolidFuelModel>();
            model.SampleCollectionId = SampleCollectionId;
            model.EnquirySampleID = (long)EnquirySampleID;
            model.EnquiryId = EnquiryId;
            model.ParameterMappingId = (Int32)ParameterMappingId;
            model.CoalCokeId = (Int32)FieldId;
            TempData.Remove("CoalCokeList");
            if (FieldId != 0)
            {
                FDCoalCokeInfo coalcokeInfo = BALFactory.coalCokeSolidFuelsBAL.GetCoalDetails((Int32)FieldId, (Int32)SampleCollectionId);
                if (coalcokeInfo != null)
                {
                    model.CoalCokeId = (Int32)FieldId;
                    model.SampleCollectionId = SampleCollectionId;
                    model.EnquiryId = EnquiryId;
                    model.StatusId = coalcokeInfo.CoalCokeDetails.StatusId;
                    model.CurrentStatus = coalcokeInfo.CoalCokeDetails.CurrentStatus;
                    model.AnyOtherObservation = coalcokeInfo.CoalCokeDetails.AnyOtherObservations;
                    model.ParameterName = coalcokeInfo.CoalCokeDetails.Parameters;
                    model.TestResults = coalcokeInfo.CoalCokeDetails.TestResults;
                    model.TestMethodName = coalcokeInfo.CoalCokeDetails.TestMethodName;
                    model.InField = coalcokeInfo.CoalCokeDetails.InField;
                    model.IsNABLAccredited = coalcokeInfo.CoalCokeDetails.IsNABLAccredited;
                    int i = 1;
                    foreach (var grid1 in coalcokeInfo.CoalCokeInfos)
                    {
                        try
                        {
                            model.GridModel.Add(new FDCoalCokeSolidFuelModel()
                            {
                                SrNo = i,
                                CoalCokeId = grid1.FieldId,
                                //Parameters = grid1.Parameters,
                                ParameterName = grid1.Parameters,
                                TestMethodName = grid1.TestMethodName,
                                InField = grid1.InField,
                                IsNABLAccredited = grid1.IsNABLAccredited,
                                TestResults = grid1.TestResults,
                                AnyOtherObservation = grid1.AnyOtherObservations,
                            });
                            i++;
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    TempData["CoalCokeList"] = model.GridModel;
                }
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult FieldCoalCokeSolidFuel(FDCoalCokeSolidFuelModel model, string Save)
        {
            CoreFactory.coalCokeSolidFuelEntity = new CoalCokeSolidFuelEntity();
            var coalCokeList = (List<FDCoalCokeSolidFuelModel>)TempData.Peek("CoalCokeList");
            if (coalCokeList != null)
            {
                if (coalCokeList.Count > 0)
                {
                    foreach (var coalCoke in coalCokeList)
                    {
                        CoreFactory.coalCokeSolidFuelEntity.FieldId = Convert.ToByte(coalCoke.CoalCokeId);
                        CoreFactory.coalCokeSolidFuelEntity.SampleCollectionId = (long)model.SampleCollectionId;
                        CoreFactory.coalCokeSolidFuelEntity.EnquiryId = model.EnquiryId;
                        //CoreFactory.coalCokeSolidFuelEntity.StatusId = (byte)iStatusId;
                        CoreFactory.coalCokeSolidFuelEntity.AnyOtherObservations = model.AnyOtherObservation;
                        CoreFactory.coalCokeSolidFuelEntity.Parameters = coalCoke.ParameterName;
                        CoreFactory.coalCokeSolidFuelEntity.TestMethodName = coalCoke.TestMethodName;
                        CoreFactory.coalCokeSolidFuelEntity.InField = coalCoke.InField;
                        CoreFactory.coalCokeSolidFuelEntity.IsNABLAccredited = coalCoke.IsNABLAccredited;
                        CoreFactory.coalCokeSolidFuelEntity.TestResults = coalCoke.TestResults;
                        //CoreFactory.coalCokeSolidFuelEntity.ParameterMappingId = coalCoke.ParameterMappingId;
                        CoreFactory.coalCokeSolidFuelEntity.IsActive = true;
                        CoreFactory.coalCokeSolidFuelEntity.EnteredBy = LIMS.User.UserMasterID;
                        CoreFactory.coalCokeSolidFuelEntity.EnteredDate = DateTime.UtcNow;

                        if (Save == "FieldSave")
                        {
                            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("InProgress");
                            CoreFactory.coalCokeSolidFuelEntity.StatusId = (byte)iStatusId;
                            strStatus = BALFactory.coalCokeSolidFuelsBAL.AddCoal(CoreFactory.coalCokeSolidFuelEntity);
                        }
                        else if (Save == "FieldSubmit")
                        {
                            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
                            CoreFactory.coalCokeSolidFuelEntity.StatusId = (byte)iStatusId;
                            strStatus = BALFactory.coalCokeSolidFuelsBAL.AddCoal(CoreFactory.coalCokeSolidFuelEntity);
                        }
                        //strStatus = BALFactory.coalCokeSolidFuelsBAL.AddCoal(CoreFactory.coalCokeSolidFuelEntity);
                    }
                }
            }
            else
            {
                CoreFactory.coalCokeSolidFuelEntity.SampleCollectionId = (long)model.SampleCollectionId;
                CoreFactory.coalCokeSolidFuelEntity.EnquiryId = model.EnquiryId;
                //CoreFactory.coalCokeSolidFuelEntity.StatusId = (byte)iStatusId;
                CoreFactory.coalCokeSolidFuelEntity.AnyOtherObservations = model.AnyOtherObservation;
                CoreFactory.coalCokeSolidFuelEntity.IsActive = true;
                CoreFactory.coalCokeSolidFuelEntity.EnteredBy = LIMS.User.UserMasterID;
                CoreFactory.coalCokeSolidFuelEntity.EnteredDate = DateTime.UtcNow;

                if (Save == "FieldSave")
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("InProgress");
                    CoreFactory.coalCokeSolidFuelEntity.StatusId = (byte)iStatusId;
                    strStatus = BALFactory.coalCokeSolidFuelsBAL.AddCoal(CoreFactory.coalCokeSolidFuelEntity);
                }
                else if (Save == "FieldSubmit")
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
                    CoreFactory.coalCokeSolidFuelEntity.StatusId = (byte)iStatusId;
                    strStatus = BALFactory.coalCokeSolidFuelsBAL.AddCoal(CoreFactory.coalCokeSolidFuelEntity);
                }
            }
            return Json(new { Status = strStatus, message = "Values Entered successfully." }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _FieldCoalCokeSolidFuel(int EnquirySampleID, int? Id = 0)
        {
            FDCoalCokeSolidFuelModel model = new FDCoalCokeSolidFuelModel();
            model.EnquirySampleID = EnquirySampleID;
            ViewBag.Parameter = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            ViewBag.TestMethod = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            if (Id != 0 && TempData["coalCokeList"] != null)
            {
                var coalCokeList = (List<FDCoalCokeSolidFuelModel>)TempData.Peek("CoalCokeList");
                TempData.Keep("CoalCokeList");
                model = coalCokeList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            }
            return PartialView(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _FieldCoalCokeSolidFuel(FDCoalCokeSolidFuelModel model)
        {
            List<FDCoalCokeSolidFuelModel> coalCokeList = new List<FDCoalCokeSolidFuelModel>();
            if (TempData["CoalCokeList"] != null)
            {
                coalCokeList = (List<FDCoalCokeSolidFuelModel>)TempData["CoalCokeList"];
            }
            if (model.SrNo == 0)
            {
                model.SrNo = coalCokeList.Count() + 1;
                var result = BALFactory.samplearrivalBAL.GetInFieldIsNabl((Int32)model.EnquirySampleID, model.ParameterMasterId, model.TestMethodId);
                if (result != null)
                {
                    model.InField = result.InField;
                    model.IsNABLAccredited = result.IsNABLAccredited;
                }
                coalCokeList.Add(model);
            }
            else
            {
                var coalCoke = coalCokeList.Where(c => c.SrNo == model.SrNo).FirstOrDefault();
                coalCoke.ParameterName = model.ParameterName;
                coalCoke.InField = model.InField;
                coalCoke.IsNABLAccredited = model.IsNABLAccredited;
                if (model.TestMethodId == 0 || model.TestMethodId == null)
                {
                    coalCoke.TestMethodName = "";
                }
                else
                {
                    coalCoke.TestMethodName = model.TestMethodName;
                }
                coalCoke.TestResults = model.TestResults;
            }
            TempData["CoalCokeList"] = coalCokeList;
            return Json(new { status = "success", message = "Added" }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult _FieldCoalCokeSolidFuelList(FDCoalCokeSolidFuelModel model)
        {
            if (TempData["CoalCokeList"] != null)
            {
                model.GridModel = (List<FDCoalCokeSolidFuelModel>)TempData.Peek("CoalCokeList");
                TempData.Keep("CoalCokeList");
            }
            return PartialView(model);
        }
        public JsonResult _DeleteCoalCokeField(int? Id = 0)
        {
            var coalCokeList = (List<FDCoalCokeSolidFuelModel>)TempData.Peek("CoalCokeList");
            var coalCoke = coalCokeList.Where(c => c.SrNo == Id).FirstOrDefault();
            if (coalCoke.CoalCokeId != 0)
            {
                BALFactory.coalCokeSolidFuelsBAL.DeleteCoalCokeField(coalCoke.CoalCokeId);
            }
            coalCokeList.Remove(coalCoke);
            int i = 1;
            foreach (var item in coalCokeList)
            {
                item.SrNo = i;
                i++;
            }
            TempData["CoalCokeList"] = coalCokeList;
            return Json(new { status = "success", message = "Deleted" }, JsonRequestBehavior.AllowGet);
        }
    }
}