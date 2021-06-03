using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Configuration.Models;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Configuration;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Common;
//using InfoLIMS.Common;



namespace LIMS_DEMO.Areas.Configuration.Controllers
{
    public class ParameterMasterController : Controller
    {

        string strStatus = "";
        public ParameterMasterController()
        {
            BALFactory.parameterMasterBAL = new ParameterMasterBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }
        // GET: Configuration/ParameterMaster
        public ActionResult AddParameterMaster(int? ParameterMappingId= 0)
        {
            ParameterMasterModel model = new ParameterMasterModel();
            if (ParameterMappingId != 0)
            {
                CoreFactory.parameterMasterEntity = CoreFactory.parameterMaster.GetDetails((Int32)ParameterMappingId);
                CoreFactory.parameterMasterEntity1 = CoreFactory.parameterMaster.GetParameterGroupDetails(CoreFactory.parameterMasterEntity.ParameterGroupId);
                model.ParameterMappingId = CoreFactory.parameterMasterEntity.ParameterMappingId;
                model.ParameterGroupId = CoreFactory.parameterMasterEntity.ParameterGroupId;
                model.ProductGroupId = CoreFactory.parameterMasterEntity1.ProductGroupId;
                model.SubGroupId = CoreFactory.parameterMasterEntity1.SubGroupId;
                model.SampleTypeProductId = (Int32)CoreFactory.parameterMasterEntity1.SampleTypeProductId;
                model.MatrixId = (Int32)CoreFactory.parameterMasterEntity1.MatrixId;
                model.DisciplineId = (Byte)CoreFactory.parameterMasterEntity1.DisciplineId;
                if (CoreFactory.parameterMasterEntity1.IsIndustrySpecified != null)
                    model.IsIndustrySpecified = CoreFactory.parameterMasterEntity1.IsIndustrySpecified;
                else
                    model.IsIndustrySpecified = false;
                model.ParameterMasterId = CoreFactory.parameterMasterEntity.ParameterMasterId;
                model.UnitId = CoreFactory.parameterMasterEntity.UnitId;
                model.TestMethodId = (Int32)CoreFactory.parameterMasterEntity.TestMethodId;
                if (CoreFactory.parameterMasterEntity.IsNABLAccredited != null)
                    model.IsNABLAccredited = CoreFactory.parameterMasterEntity.IsNABLAccredited;
                else
                    model.IsNABLAccredited = false;
                model.PermissibleMin = CoreFactory.parameterMasterEntity.PermissibleMin;
                model.PermissibleMax = CoreFactory.parameterMasterEntity.PermissibleMax;
                model.RegulatoryMin = CoreFactory.parameterMasterEntity.RegulatoryMin;
                model.RegulatoryMax = CoreFactory.parameterMasterEntity.RegulatoryMax;
                model.MaxRange = CoreFactory.parameterMasterEntity.MaxRange;
                model.LOD = CoreFactory.parameterMasterEntity.LOD;
                if (CoreFactory.parameterMasterEntity.IsField != null)
                    model.IsField = CoreFactory.parameterMasterEntity.IsField;
                else
                    model.IsField = false;
                model.IsActive = CoreFactory.parameterMasterEntity.IsActive;
            }
            ViewBag.SampleTypeProductList = BALFactory.dropdownsBAL.GetSampleTypeProduct();
            ViewBag.ProductGroupList = BALFactory.dropdownsBAL.GetProductGroup(model.SampleTypeProductId);//Passing Data to Viewbag for dropdown           
            ViewBag.SubGroupList = BALFactory.dropdownsBAL.GetSubGroup(model.SampleTypeProductId, model.ProductGroupId);//Passing Data to Viewbag for dropdown
            ViewBag.MatrixList = BALFactory.dropdownsBAL.GetMatrix(model.SampleTypeProductId, model.ProductGroupId, model.SubGroupId);
            ViewBag.UnitList = BALFactory.dropdownsBAL.GetUnits();
            ViewBag.DisciplineList = BALFactory.dropdownsBAL.GetDiscipline();
            ViewBag.TestMethodList = BALFactory.dropdownsBAL.GetTestMethods();
            ViewBag.ParameterList = BALFactory.dropdownsBAL.GetParameter();
            return View(model);
        }
        [HttpPost]  
        public ActionResult AddParameterMaster(ParameterMasterModel model)
        {
            CoreFactory.parameterMasterEntity = new ParameterMasterEntity();

            if (model.ParameterMappingId == 0)
            {
                CoreFactory.parameterMasterEntity.SampleTypeProductId = model.SampleTypeProductId;
                CoreFactory.parameterMasterEntity.ProductGroupId = model.ProductGroupId;
                CoreFactory.parameterMasterEntity.SubGroupId = model.SubGroupId;
                CoreFactory.parameterMasterEntity.MatrixId = model.MatrixId;
                CoreFactory.parameterMasterEntity.DisciplineId = model.DisciplineId;
                CoreFactory.parameterMasterEntity.IsIndustrySpecified = model.IsIndustrySpecified;
            }
            else
                CoreFactory.parameterMasterEntity.ParameterMappingId = model.ParameterMappingId;
            CoreFactory.parameterMasterEntity.ParameterMasterId = model.ParameterMasterId;
            CoreFactory.parameterMasterEntity.ParameterGroupId = model.ParameterGroupId;
            CoreFactory.parameterMasterEntity.EnteredBy = LIMS.User.UserMasterID;
            CoreFactory.parameterMasterEntity.EnteredDate = DateTime.Now;
            CoreFactory.parameterMasterEntity.IsActive = model.IsActive;
            var prodgrp1 = BALFactory.parameterMasterBAL.GetParameterDetails(model.SampleTypeProductId, model.ProductGroupId, model.SubGroupId, model.MatrixId);
            if (prodgrp1 == 0)
            {
                //var prodgrp1 = BALFactory.parameterMasterBAL.GetParameterGroupDetails(model.ParameterGroupId);
                CoreFactory.parameterMasterEntity.ParameterGroupId = BALFactory.parameterMasterBAL.AddParameterGroup(CoreFactory.parameterMasterEntity);

            }
            else
            {
                model.ParameterGroupId = prodgrp1;
                CoreFactory.parameterMasterEntity.ParameterGroupId = model.ParameterGroupId;
                var prodgrp = BALFactory.parameterMasterBAL.GetParameterGroupDetails(model.ParameterGroupId);
                CoreFactory.parameterMasterEntity.ProductGroupId = prodgrp.ProductGroupId;
                CoreFactory.parameterMasterEntity.SubGroupId = prodgrp.SubGroupId;
                CoreFactory.parameterMasterEntity.MatrixId = prodgrp.MatrixId;
                CoreFactory.parameterMasterEntity.SampleTypeProductId = prodgrp.SampleTypeProductId;
                CoreFactory.parameterMasterEntity.DisciplineId = prodgrp.DisciplineId;
                CoreFactory.parameterMasterEntity.IsIndustrySpecified = prodgrp.IsIndustrySpecified;
            }
            CoreFactory.parameterMasterEntity.TestMethodId = model.TestMethodId;
            if (model.UnitId == 0)
                CoreFactory.parameterMasterEntity.UnitId = 56;
            else
                CoreFactory.parameterMasterEntity.UnitId = model.UnitId;
            CoreFactory.parameterMasterEntity.LOD = model.LOD;
            CoreFactory.parameterMasterEntity.MaxRange = model.MaxRange;
            CoreFactory.parameterMasterEntity.IsField = model.IsField;
            CoreFactory.parameterMasterEntity.IsNABLAccredited = model.IsNABLAccredited;
            CoreFactory.parameterMasterEntity.PermissibleMin = model.PermissibleMin;
            CoreFactory.parameterMasterEntity.PermissibleMax = model.PermissibleMax;
            CoreFactory.parameterMasterEntity.RegulatoryMin = model.RegulatoryMin;
            CoreFactory.parameterMasterEntity.RegulatoryMax = model.RegulatoryMax;
            if (model.ParameterMappingId == 0)
            {
                var getPMI = BALFactory.parameterMasterBAL.GetParameterMasterMappingDetail(CoreFactory.parameterMasterEntity);
                if (getPMI == 0)
                {
                    try
                    {
                        strStatus = BALFactory.parameterMasterBAL.AddParameterMaster(CoreFactory.parameterMasterEntity);
                        return Json(new { Status = strStatus, message = "ParameterMaster has been registered successfully." }, JsonRequestBehavior.AllowGet);

                    }

                    catch (Exception ex)
                    {
                        return null;

                    }
                }
                //Use for update
                else
                {
                    return Json(new { Status = false, message = "Parameter already Mapped" }, JsonRequestBehavior.AllowGet);

                }

            }
            else
            {
                strStatus = BALFactory.parameterMasterBAL.Update(CoreFactory.parameterMasterEntity);
                return Json(new { Status = strStatus, message = "ParameterMaster has been updated successfully." }, JsonRequestBehavior.AllowGet);

            }
            //return RedirectToAction("ParameterMasterList");
        }
        public ActionResult ParameterMasterList()
        {
            CoreFactory.parameterMasterList = BALFactory.parameterMasterBAL.GetParameterMasterList();
            List<ParameterMasterModel> modelList = new List<ParameterMasterModel>();
            foreach (var Item in CoreFactory.parameterMasterList)
            {
                modelList.Add(new ParameterMasterModel()
                {
                    ParameterMappingId = Item.ParameterMappingId,
                    Parameter = Item.Parameter,
                    DisciplineName = Item.DisciplineName,
                    SampleTypeProductName = Item.SampleTypeProductName,
                    ProductGroupName = Item.ProductGroupName,
                    SubGroupName = Item.SubGroupName,
                    MatrixName = Item.MatrixName,
                    Unit = Item.UnitName,
                    MaxRange = Item.MaxRange,
                    TestMethod = Item.TestMethodName,
                    Charges = Item.Charges,
                    LOD = Item.LOD,
                    PermissibleMin = Item.PermissibleMin,
                    PermissibleMax = Item.PermissibleMax,
                    RegulatoryMin = Item.RegulatoryMin,
                    RegulatoryMax = Item.RegulatoryMax,
                    IsIndustrySpecified = Item.IsIndustrySpecified,
                    IsField = Item.IsField,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }
        [HttpPost]
        public JsonResult Add(ParameterMasterModel model)
        {
            CoreFactory.parameterMasterEntity = new ParameterMasterEntity();

            return Json(new { Status = strStatus, message = "ParameterMaster has been registered successfully." }, JsonRequestBehavior.AllowGet);  
        }
        public ActionResult AddParameter(int? ParameterMasterId = 0)
        {
            ParameterMasterModel model = new ParameterMasterModel();
            if (ParameterMasterId != 0)
            {
                CoreFactory.parameterMasterEntity = BALFactory.parameterMasterBAL.GetDetailsParameter((Int32)ParameterMasterId);
                if (CoreFactory.parameterMasterEntity != null)
                {
                    model.ParameterMasterId = CoreFactory.parameterMasterEntity.ParameterMasterId;
                    model.Parameter = CoreFactory.parameterMasterEntity.Parameter;

                    model.IsActive = CoreFactory.parameterMasterEntity.IsActive;
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddParameter(ParameterMasterModel model)
        {
            CoreFactory.parameterMasterEntity = new ParameterMasterEntity();
           // UnicodeConverter _conUniCode = new UnicodeConverter();
        //    string strParameter = _conUniCode.HtmlTagConverter(model.Parameter);
          //  strParameter = RemoveHtmlTags.RemoveUnwantedTags(strParameter);
          //  strParameter = System.Web.HttpUtility.HtmlDecode(strParameter);
          //  model.Parameter = strParameter;
            CoreFactory.parameterMasterEntity.ParameterMasterId = model.ParameterMasterId;
            CoreFactory.parameterMasterEntity.Parameter = model.Parameter;
            //CoreFactory.testMethodMasterEntity.EnteredBy = LIMS.User.UserMasterID;
            CoreFactory.parameterMasterEntity.EnteredDate = DateTime.Now;
            CoreFactory.parameterMasterEntity.IsActive = model.IsActive;

            if (model.ParameterMasterId == 0)
            {
                strStatus = BALFactory.parameterMasterBAL.AddParameter(CoreFactory.parameterMasterEntity);
                //return Json(new { Status = strStatus, message = "TestMethod has been registered successfully." }, JsonRequestBehavior.AllowGet);
            }

            //Use for update
            else
            {
                strStatus = BALFactory.parameterMasterBAL.UpdateParameter(CoreFactory.parameterMasterEntity);
                //return Json(new { Status = strStatus, message = "TestMethod has been updated successfully." }, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("ParameterList");
            //return Json(new { Status = strStatus, message = "Parameter has been registered successfully." }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ParameterList()
        {
            CoreFactory.parameterMasterList = BALFactory.parameterMasterBAL.GetParameterList();
            List<ParameterMasterModel> modelList = new List<ParameterMasterModel>();
            foreach (var Item in CoreFactory.parameterMasterList)
            {
                modelList.Add(new ParameterMasterModel()
                {
                    ParameterMasterId = Item.ParameterMasterId,
                    Parameter = Item.Parameter,
                    //DisposedDay = Item.DisposedDay,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }

        public JsonResult ProductGroups(int? SampleTypeProductId = 0)
        {
            var productgroupList = BALFactory.dropdownsBAL.GetProductGroup((Int32)SampleTypeProductId);
            return Json(new { result = productgroupList }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult SubGroups(int? SampleTypeProductId = 0, int? ProductGroupId = 0)
        {
            var subgroupList = BALFactory.dropdownsBAL.GetSubGroup((Int32)SampleTypeProductId, (Int32)ProductGroupId);
            return Json(new { result = subgroupList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Matrix(int? SampleTypeProductId = 0, int? ProductGroupId = 0, int? SubGroupId = 0)
        {
            var MatrixList = BALFactory.dropdownsBAL.GetMatrix((Int32)SampleTypeProductId, (Int32)ProductGroupId, (Int32)SubGroupId);
            return Json(new { result = MatrixList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteParameterMaster(int ParameterMappingId)
        {
            strStatus = BALFactory.parameterMasterBAL.DeleteParameterMaster((Int32)ParameterMappingId);
            return Json(new { result = strStatus }, JsonRequestBehavior.AllowGet);
                
        }
        public JsonResult DeleteParameter(int ParameterMasterId)
        {
            strStatus = BALFactory.parameterMasterBAL.DeleteParameter((Int32)ParameterMasterId);
            return Json(new { result = strStatus }, JsonRequestBehavior.AllowGet);
        }
    }
}
