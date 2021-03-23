using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Login;
using LIMS_DEMO.BAL.DropDown;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.User;
using LIMS_DEMO.Common;
using LIMS_DEMO.Models;
using Newtonsoft.Json;

namespace LIMS_DEMO.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            BALFactory.loginBAL = new LoginBAL();
            BALFactory.menuBAL = new BAL.AppSettings.MenuBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
        }
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult _Login()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Login(LoginModel model)
        {
            BALFactory.loginBAL.Initialize();
            CoreFactory.userEntity = BALFactory.loginBAL.GetUserDetails(model.UserName);
            if (CoreFactory.userEntity != null && Security.Verify(model.Password, CoreFactory.userEntity.Password))
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    CoreFactory.userEntity.FirstName + " " + CoreFactory.userEntity.LastName,
                    DateTime.Now,
                    DateTime.Now.AddMonths(1),
                    true,
                    JsonConvert.SerializeObject(CoreFactory.userEntity),
                    FormsAuthentication.FormsCookiePath);

                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)));
                LIMS.User.LabId = BALFactory.loginBAL.GetLabId(CoreFactory.userEntity.UserMasterID);//Passing LabMasterId to it
                LIMS.User.UserMasterID = CoreFactory.userEntity.UserMasterID;//Passing UserMasterID to it
                LIMS.User.UserName = CoreFactory.userEntity.UserName;//Passing UserName to it for HOD & BDM Grid
                LIMS.User.RoleName = CoreFactory.userEntity.RoleName;
                LIMS.User.RoleId = CoreFactory.userEntity.RoleId;
                var noti = BALFactory.loginBAL.GetNotificationDetailList(LIMS.User.RoleName);
                if (noti.Count != 0)
                {
                      var No = noti.Count();
                        var count = 0;
                        while (No != 0)
                        {
                            No--;
                            if (noti[No].EnteredDate.Month == System.DateTime.UtcNow.Month)
                            {
                                count++;
                                LIMS.User.NotifCount = count;
                            }
                        }
                  }
                
                var forgotpass = BALFactory.dropdownsBAL.GetPassChaReqUserList();
                if (forgotpass.Count != 0 || forgotpass.Count!= null)
                { 
                    LIMS.User.ForgotpassCount = forgotpass.Count();
                }
                  

                if (LIMS.User.RoleName == "Admin")
                {
                   // return Redirect("/Dashboard/Dashboard/Index");
                  return Json(new { status = "success", returnUrl = "/Dashboard/Dashboard/Admin" }, JsonRequestBehavior.AllowGet);
                }
                if (LIMS.User.RoleName == "BDM")
                {
                    // return Redirect("/Dashboard/Dashboard/Index");
                    return Json(new { status = "success", returnUrl = "/Dashboard/Dashboard/BDM" }, JsonRequestBehavior.AllowGet);
                }
                if (LIMS.User.RoleName == "HOD")
                {
                    // return Redirect("/Dashboard/Dashboard/Index");
                    return Json(new { status = "success", returnUrl = "/Dashboard/Dashboard/HOD" }, JsonRequestBehavior.AllowGet);
                }
                if (LIMS.User.RoleName == "Sampling Incharge")
                {
                    // return Redirect("/Dashboard/Dashboard/Index");
                    return Json(new { status = "success", returnUrl = "/Dashboard/Dashboard/STL" }, JsonRequestBehavior.AllowGet);
                }
                if (LIMS.User.RoleName == "Technical Manager")
                {
                    // return Redirect("/Dashboard/Dashboard/Index");
                    return Json(new { status = "success", returnUrl = "/Dashboard/Dashboard/TM" }, JsonRequestBehavior.AllowGet);
                }
                if (LIMS.User.RoleName == "Sampler" || LIMS.User.RoleName == "Sampler Customer")
                {
                    return Json(new { status = "success", returnUrl = "/Dashboard/Dashboard/Sampler" }, JsonRequestBehavior.AllowGet);
                }
                if(LIMS.User.RoleName == "Sample Receipt and Report Incharge")
                {
                    return Json(new { status = "success", returnUrl = "/Dashboard/Dashboard/ReceiptIncharge" }, JsonRequestBehavior.AllowGet);
                }
                if (LIMS.User.RoleName == "Sample Receiver")
                {
                    //int DisciplineId = CoreFactory.userEntity.DisciplineId;
                    return Json(new { status = "success", returnUrl = "/Dashboard/Dashboard/SampleReceiver" }, JsonRequestBehavior.AllowGet);

                }
                if (LIMS.User.RoleName == "Purchase Incharge")
                {
                    // return Redirect("/Dashboard/Dashboard/Index");
                    return Json(new { status = "success", returnUrl = "/Dashboard/Dashboard/PurchaseIncharge" }, JsonRequestBehavior.AllowGet);
                }
                if (LIMS.User.RoleName == "Tester" || LIMS.User.RoleName == "Planner" || LIMS.User.RoleName == "Reviewer")
                {
                    // return Redirect("/Dashboard/Dashboard/Index");
                    return Json(new { status = "success", returnUrl = "/Dashboard/Dashboard/Lab" }, JsonRequestBehavior.AllowGet);
                }
                if (LIMS.User.RoleName == "Approver")
                {
                    // return Redirect("/Dashboard/Dashboard/Index");
                    return Json(new { status = "success", returnUrl = "/Dashboard/Dashboard/Approver" }, JsonRequestBehavior.AllowGet);
                }
               
                
                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = "failed" }, JsonRequestBehavior.AllowGet);
            }
        }
        public RedirectToRouteResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            LIMS.User.UserMasterID = 0;
            return RedirectToAction("Index");
        }
        public PartialViewResult _Menu()
        {
            IList<MenuModel> model = new List<MenuModel>();

            var Menus = BALFactory.menuBAL.Get(LIMS.User.UserMasterID);
            int i = 0;
            foreach (var menu in Menus)
            {
                model.Add(new MenuModel()
                {
                    MenuMasterId = menu.MenuMasterId,
                    ParentId = menu.ParentId,
                    Menu = menu.Menu,
                    IconValues = menu.Logo,
                    TargetUrl = menu.TargetUrl
                });

                model[i].SubMenu = new List<MenuModel>();
                foreach (var subMenu in menu.SubMenu)
                {
                    model[i].SubMenu.Add(new MenuModel()
                    {
                        MenuMasterId = subMenu.MenuMasterId,
                        ParentId = subMenu.ParentId,
                        Menu = subMenu.Menu,
                        IconValues = subMenu.Logo,
                        TargetUrl = subMenu.TargetUrl
                    });
                }
                i++;
            }
            return PartialView(model);
        }
        public PartialViewResult _ForgotPassword(bool? Send)
        {
            if (Send.HasValue)
            {
                ViewBag.msg = "Request has Been Send";
            }
            LoginModel model = new LoginModel();
            CoreFactory.userEntity = new UserEntity();
            ViewBag.UserList = BALFactory.dropdownsBAL.GetUserMasterList();
            return PartialView(model);
        }
    
        [HttpPost]
        public ActionResult _ForgotPassword(LoginModel model)
        {
            CoreFactory.userEntity = new UserEntity();
            CoreFactory.userEntity.ResetActive = true;
            CoreFactory.userEntity.UserMasterID = model.UserMasterID;

            bool send = BALFactory.loginBAL.ChangeRequest(CoreFactory.userEntity.UserMasterID, CoreFactory.userEntity.ResetActive);
            return RedirectToAction("_ForgotPassword", "Home", new { @Send = true });
        }
        public ActionResult passchange()
        {
            var Result = BALFactory.dropdownsBAL.GetPassChaReqUserList();
            return new JsonResult { Data = Result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult GetNotificationDetails(string RoleName)
        {
            List<NotificationEntity> notifentity = new List<NotificationEntity>();
            var Result = BALFactory.loginBAL.GetNotificationDetailList(RoleName);
            if (Result.Count() != 0)
            {
                var No = Result.Count();
                int a = No;
                while (No != 0)
                {
                    No--;
                    notifentity.Add(new NotificationEntity() { DisplayDate = Result[No].DisplayDate, NotificationName = Result[No].NotificationName, Message = Result[No].Message + "[ " + a + " ]", LastUpdated = DateTime.Now.ToString("ss") + " seconds ago..." });
                    a--;
                }
            }
            return new JsonResult { Data = notifentity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}