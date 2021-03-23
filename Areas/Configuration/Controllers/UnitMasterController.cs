using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.BAL;
using LIMS_DEMO.Common;
using LIMS_DEMO.BAL.Configuration;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core;

using LIMS_DEMO.Areas.Configuration.Models;


namespace LIMS_DEMO.Areas.Configuration.Controllers
{
    public class UnitMasterController : Controller
    {
        string strStatus = "";
        public UnitMasterController()
        {
            BALFactory.unitMasterBAL = new UnitMasterBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();

        }
        // GET: Configuration/UnitMaster
        public ActionResult AddUnit(int? UnitId =0)
        {
            UnitMasterModel model = new UnitMasterModel();
            if (UnitId != 0)
            {
                CoreFactory.unitMasterEntity = BALFactory.unitMasterBAL.GetDetails((Int32)UnitId);
                if (CoreFactory.unitMasterEntity != null)
                {
                    model.UnitId = CoreFactory.unitMasterEntity.UnitId;
                    model.Unit = CoreFactory.unitMasterEntity.Unit;
                    model.IsActive = CoreFactory.unitMasterEntity.IsActive;
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddUnit(UnitMasterModel model)
        {
            UnitMasterEntity unitMasterEntity = new UnitMasterEntity();
            unitMasterEntity.UnitId = model.UnitId;
            //CoreFactory.unitMasterEntity.UnitId = model.UnitId;
            UnicodeConverter _conUniCode = new UnicodeConverter();
            string strUnit = _conUniCode.HtmlTagConverter(model.Unit);
            strUnit = RemoveHtmlTags.RemoveUnwantedTags(strUnit);
            strUnit = System.Web.HttpUtility.HtmlDecode(strUnit);
            model.Unit = strUnit;

            unitMasterEntity.Unit = model.Unit;
            //unitMasterEntity.EnteredBy = LIMS.User.UserMasterID;
            unitMasterEntity.EnteredDate = DateTime.Now;
            unitMasterEntity.IsActive = model.IsActive;
            if (model.UnitId == 0)
            {
                strStatus = BALFactory.unitMasterBAL.AddUnitMaster(unitMasterEntity);
                return RedirectToAction("UnitList");
                //return Json(new { Status = strStatus, message = "Unit has been registered successfully." }, JsonRequestBehavior.AllowGet);
            }
            //Use for update
            else
            {
                strStatus = BALFactory.unitMasterBAL.UpdateUnitMaster(unitMasterEntity);
                return RedirectToAction("UnitList");
                //return Json(new { Status = strStatus, message = "Unit has been updated successfully." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteUnitMaster(int UnitId)
        {
            strStatus = BALFactory.unitMasterBAL.DeleteUnitMaster((Int32)UnitId);
            if (strStatus == "success")
            {
                TempData["message"] = "Unit has been deleted successfully.";
            }
            return RedirectToAction("UnitList");
        }
        public ActionResult UnitList()
        {
            CoreFactory.unitMasterList = BALFactory.unitMasterBAL.GetUnitList();
            List<UnitMasterModel> modelList = new List<UnitMasterModel>();
            foreach (var Item in CoreFactory.unitMasterList)
            {

                modelList.Add(new UnitMasterModel()
                {
                    UnitId = Item.UnitId,
                    Unit = Item.Unit,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }
    }
}