using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Configuration;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core;
using LIMS_DEMO.Common;
using LIMS_DEMO.Areas.Configuration.Models;

namespace LIMS_DEMO.Areas.Configuration.Controllers
{
    public class UserMasterController : Controller
    {
        string strStatus = "";
        public UserMasterController()
        {
            BALFactory.userMasterBAL = new UserMasterBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }
        // GET: Configuration/UserMaster
        public ActionResult AddUser(int? UserMasterID =0)
        {
            UserMasterModel model = new UserMasterModel();
            if(UserMasterID != 0)
            {
                CoreFactory.userMasterEntity = BALFactory.userMasterBAL.GetDetails((Int32)UserMasterID);
                if(CoreFactory.userMasterEntity != null)
                {
                    model.UserMasterID = CoreFactory.userMasterEntity.UserMasterID;
                    model.UserName = CoreFactory.userMasterEntity.UserName;
                    model.DisciplineId = CoreFactory.userMasterEntity.DisciplineId;
                    model.IsActive = CoreFactory.userMasterEntity.IsActive;
                }
            }
            ViewBag.DisciplineList = BALFactory.dropdownsBAL.GetDiscipline();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(UserMasterModel model)
        {
            UserMasterEntity userMasterEntity = new UserMasterEntity();
            userMasterEntity.UserMasterID = model.UserMasterID;
            userMasterEntity.UserName = model.UserName;
            userMasterEntity.Password = Security.Encrypt(model.Password);
            userMasterEntity.DisciplineId = model.DisciplineId;
            userMasterEntity.IsActive = model.IsActive;
            //userMasterEntity.EnteredBy = LIMS.User.UserMasterID;
            userMasterEntity.EnteredDate = DateTime.Now;
            userMasterEntity.LabId = 1; 
            if (model.UserMasterID == 0)
            {
                strStatus = BALFactory.userMasterBAL.AddUser(userMasterEntity);
                return Json(new { Status = strStatus, message = "User created." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                strStatus = BALFactory.userMasterBAL.Update(userMasterEntity);
                return Json(new { Status = strStatus, message = "User updated." }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult UserMasterList()
        {
            CoreFactory.userMasterList = BALFactory.userMasterBAL.GetUserList();
            List<UserMasterModel> modelList = new List<UserMasterModel>();
            foreach (var Item in CoreFactory.userMasterList)
            {
                modelList.Add(new UserMasterModel()
                {
                    UserName = Item.UserName,
                    UserMasterID = Item.UserMasterID,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }
    }
}