using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Configuration;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core;
using LIMS_DEMO.Areas.Configuration.Models;

namespace LIMS_DEMO.Areas.Configuration.Controllers
{
    public class RoleMasterController : Controller
    {
        string strStatus = "";
        public RoleMasterController()
        {
            BALFactory.roleMasterBAL = new RoleMasterBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }
        // GET: Configuration/RoleMaster
        public ActionResult AddUserRole(int? UserRoleID = 0)
        {
            RoleMasterModel model = new RoleMasterModel();
            ViewBag.RoleList = BALFactory.dropdownsBAL.GetRole();
            ViewBag.UserList = BALFactory.dropdownsBAL.GetUserMasterList();
            if (UserRoleID != 0)
            {
                CoreFactory.roleMasterEntity = BALFactory.roleMasterBAL.GetDetails((Int32)UserRoleID);
                if (CoreFactory.roleMasterEntity != null)
                {
                    model.UserRoleId = CoreFactory.roleMasterEntity.UserRoleId;
                    model.UserMasterId = CoreFactory.roleMasterEntity.UserMasterId;
                    model.RoleId = CoreFactory.roleMasterEntity.RoleId;
                    model.IsActive = CoreFactory.roleMasterEntity.IsActive;
                }

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddUserRole(RoleMasterModel model)
        {
            RoleMasterEntity roleMasterEntity = new RoleMasterEntity();
            roleMasterEntity.RoleId = model.RoleId;
            roleMasterEntity.UserMasterId = model.UserMasterId;
            //roleMasterEntity.EnteredBy = LIMS.User.UserMasterID;
            roleMasterEntity.LabId = 1;
            roleMasterEntity.UserRoleId = model.UserRoleId;
            roleMasterEntity.EnteredDate = DateTime.Now;
            roleMasterEntity.IsActive = model.IsActive;

            if (model.UserRoleId == 0)
            {
                var check = BALFactory.roleMasterBAL.GetData(model.RoleId, model.UserMasterId);
                if (check != null)
                {
                    //TempData["message"] = "Role Already Mapped";
                    return Json(new { Status = check, message = "UserRole Already registered " }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    strStatus = BALFactory.roleMasterBAL.AddUserRole(roleMasterEntity);
                    return Json(new { Status = strStatus, message = "UserRole has been registered successfully." }, JsonRequestBehavior.AllowGet);
                }
            }

            //Use for update
            else
            {
                strStatus = BALFactory.roleMasterBAL.UpdateUserRole(roleMasterEntity);
                return Json(new { Status = strStatus, message = "UserRole has been updated successfully." }, JsonRequestBehavior.AllowGet);
            }
            //return RedirectToAction("UserRoleList");

        }
        public ActionResult UserRoleList()
        {
            CoreFactory.userRoleList = BALFactory.roleMasterBAL.GetUserRoleList();
            List<RoleMasterModel> modelList = new List<RoleMasterModel>();
            foreach (var Item in CoreFactory.userRoleList)
            {
                modelList.Add(new RoleMasterModel()
                {
                    RoleName = Item.RoleName,
                    UserName = Item.UserName,
                    UserRoleId = Item.UserRoleId,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }
        public ActionResult DeleteUserRole(int UserRoleId)
        {
            strStatus = BALFactory.roleMasterBAL.DeleteUserRole((Int32)UserRoleId);
            if (strStatus == "success")
            {
                TempData["message"] = "Role has been deleted successfully.";
            }
            return RedirectToAction("UserRoleList");
        }
        public ActionResult DeleteRoleMaster(int RoleId)
        {
            strStatus = BALFactory.roleMasterBAL.DeleteRoleMaster((Int32)RoleId);
            if (strStatus == "success")
            {
                TempData["message"] = "Role has been deleted successfully.";
            }
            return RedirectToAction("RoleMasterList");
        }


        public ActionResult AddRoleMaster(int? RoleID = 0)
        {
            RoleMasterModel model = new RoleMasterModel();
            if (RoleID != 0)
            {
                CoreFactory.roleMasterEntity = BALFactory.roleMasterBAL.GetDetailsRole((Int32)RoleID);
                if (CoreFactory.roleMasterEntity != null)
                {
                    model.RoleId = CoreFactory.roleMasterEntity.RoleId;
                    model.RoleName = CoreFactory.roleMasterEntity.RoleName;
                    model.IsActive = CoreFactory.roleMasterEntity.IsActive;
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddRoleMaster(RoleMasterModel model)
        {
            RoleMasterEntity roleMasterEntity = new RoleMasterEntity();

            roleMasterEntity.RoleId = model.RoleId;
            roleMasterEntity.RoleName = model.RoleName;
            //roleMasterEntity.EnteredBy = LIMS.User.UserMasterID;
            roleMasterEntity.EnteredDate = DateTime.Now;
            roleMasterEntity.IsActive = model.IsActive;

            if (model.RoleId == 0)
            {
                strStatus = BALFactory.roleMasterBAL.AddRoleMaster(roleMasterEntity);
                //return Json(new { Status = strStatus, message = "Role has been registered successfully." }, JsonRequestBehavior.AllowGet);
            }
            //Use for update
            else
            {
                strStatus = BALFactory.roleMasterBAL.UpdateRoleMaster(roleMasterEntity);
                //return Json(new { Status = strStatus, message = "Role has been updated successfully." }, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("RoleMasterList");
        }

        public ActionResult RoleMasterList()
        {
            CoreFactory.userRoleList = BALFactory.roleMasterBAL.GetRoleMasterList();
            List<RoleMasterModel> modelList = new List<RoleMasterModel>();
            foreach (var Item in CoreFactory.userRoleList)
            {
                modelList.Add(new RoleMasterModel()
                {
                    RoleId = Item.RoleId,
                    RoleName = Item.RoleName,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }
    }
}