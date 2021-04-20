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

namespace LIMS_DEMO.Areas.Configuration.Controllers
{
    public class MenuMasterController : Controller
    {
        string strStatus = "";
        public MenuMasterController()
        {
            BALFactory.menuMasterBAL = new MenuMasterBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }
        #region ParentMenu
        public ActionResult AddParentMenu(int? MenuMasterId = 0)
        {
            MenuMasterModel model = new MenuMasterModel();
            if (MenuMasterId != 0)
            {
                CoreFactory.menuMasterEntity = BALFactory.menuMasterBAL.GetDetailsParent((Int32)MenuMasterId);
                if (CoreFactory.menuMasterEntity != null)
                {
                    model.MenuMasterId = CoreFactory.menuMasterEntity.MenuMasterId;
                    model.Menu = CoreFactory.menuMasterEntity.MenuName;

                    model.IsActive = CoreFactory.menuMasterEntity.IsActive;
                }
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult AddParentMenu(MenuMasterModel model)
        {
            CoreFactory.menuMasterEntity = new MenuMasterEntity();

            CoreFactory.menuMasterEntity.MenuMasterId = model.MenuMasterId;
            CoreFactory.menuMasterEntity.MenuName = model.Menu;
            CoreFactory.menuMasterEntity.EnteredBy = 1; //LIMS.User.UserMasterID;
            CoreFactory.menuMasterEntity.EnteredDate = DateTime.Now;
            CoreFactory.menuMasterEntity.IsActive = model.IsActive;

            if (model.MenuMasterId == 0)
            {
                strStatus = BALFactory.menuMasterBAL.AddParentMenu(CoreFactory.menuMasterEntity);
                return Json(new { Status = strStatus, message = "Parent Menu has been registered successfully." }, JsonRequestBehavior.AllowGet);
               // return RedirectToAction("ParentMenuList");
            }
            //Use for update
            else
            {
                strStatus = BALFactory.menuMasterBAL.UpdateParentMenu(CoreFactory.menuMasterEntity);
                return Json(new { Status = strStatus, message = "Parent Menu has been updated successfully." }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("ParentMenuList");
            }
        }
        public ActionResult ParentMenuList()
        {
            CoreFactory.parentMenuList = BALFactory.menuMasterBAL.GetParentMenuList();
            List<MenuMasterModel> modelList = new List<MenuMasterModel>();
            foreach (var Item in CoreFactory.parentMenuList)
            {
                modelList.Add(new MenuMasterModel()
                {
                    Menu = Item.MenuName,
                    MenuMasterId = Item.MenuMasterId,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }

        public ActionResult DeleteParentMenu(int MenuMasterId)
        {
            strStatus = BALFactory.menuMasterBAL.DeleteParentMenu((Int32)MenuMasterId);
            if (strStatus == "success")
            {
                TempData["message"] = "Menu has been deleted successfully.";
            }
            return RedirectToAction("ParentMenuList");
        }

        #endregion





        #region SubMenu
        public ActionResult SubMenuList()
        {
            CoreFactory.subMenuList = BALFactory.menuMasterBAL.GetSubMenuList();
            List<MenuMasterModel> modelList = new List<MenuMasterModel>();
            foreach (var Item in CoreFactory.subMenuList)
            {
                modelList.Add(new MenuMasterModel()
                {
                    ParentMenu = Item.ParentMenuName,
                    SubMenuId = Item.SubMenuId,
                    MenuMasterId = Item.MenuMasterId,
                    SubMenu = Item.SubMenuName,
                    TargetUrl = Item.TargetURL,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }
        public ActionResult AddSubMenu(int? MenuMasterId = 0)
        {
            MenuMasterModel model = new MenuMasterModel();
            if (MenuMasterId != 0)
            {
                CoreFactory.menuMasterEntity = BALFactory.menuMasterBAL.GetDetailsSub((Int32)MenuMasterId);
                CoreFactory.menuMasterEntity1 = BALFactory.menuMasterBAL.GetDetailsSub((Int32)CoreFactory.menuMasterEntity.ParentMenuId);
                if (CoreFactory.menuMasterEntity != null)
                {
                    model.MenuMasterId = CoreFactory.menuMasterEntity.MenuMasterId;
                    model.Menu = CoreFactory.menuMasterEntity.MenuName;
                    model.SubMenu = CoreFactory.menuMasterEntity.SubMenuName;
                    model.ParentMenu = CoreFactory.menuMasterEntity1.SubMenuName;
                    model.ParentMenuId = (Int32)CoreFactory.menuMasterEntity.ParentMenuId;
                    model.TargetUrl = CoreFactory.menuMasterEntity.TargetURL;
                    model.IsActive = CoreFactory.menuMasterEntity.IsActive;
                }
            }
            ViewBag.ParentMenuList = BALFactory.dropdownsBAL.GetParentMenu();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddSubMenu(MenuMasterModel model)
        {
            CoreFactory.menuMasterEntity = new MenuMasterEntity();

            CoreFactory.menuMasterEntity.MenuMasterId = model.MenuMasterId;
            CoreFactory.menuMasterEntity.SubMenuName = model.SubMenu;
            CoreFactory.menuMasterEntity.ParentId = model.ParentMenuId;
            CoreFactory.menuMasterEntity.MenuName = model.SubMenu;
            CoreFactory.menuMasterEntity.TargetURL = model.TargetUrl;
            //CoreFactory.menuMasterEntity.EnteredBy = LIMS.User.UserMasterID;
            CoreFactory.menuMasterEntity.EnteredDate = DateTime.Now;
            CoreFactory.menuMasterEntity.IsActive = model.IsActive;

            if (model.MenuMasterId == 0)
            {
                strStatus = BALFactory.menuMasterBAL.AddSubMenu(CoreFactory.menuMasterEntity);
                return Json(new { Status = strStatus, message = "Sub Menu has been registered successfully." }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("SubMenuList");
            }

            //Use for update
            else
            {
                strStatus = BALFactory.menuMasterBAL.UpdateSubMenu(CoreFactory.menuMasterEntity);
                return Json(new { Status = strStatus, message = "Sub Menu has been updated successfully." }, JsonRequestBehavior.AllowGet);
               // return RedirectToAction("SubMenuList");
            }
        }
        public ActionResult DeleteSubMenu(int MenuMasterId)
        {
            strStatus = BALFactory.menuMasterBAL.DeleteSubMenu((Int32)MenuMasterId);
            if (strStatus == "success")
            {
                TempData["message"] = "SubMenu has been deleted successfully.";
            }
            return RedirectToAction("SubMenuList");
        }
        #endregion




        #region MenuMapping
        public ActionResult MenuMappingList()
        {
            CoreFactory.menuMappingist = BALFactory.menuMasterBAL.GetMenuMappingList();
            List<MenuMasterModel> modelList = new List<MenuMasterModel>();
            string parentMenu = null;
            string value = null;
            string roleName = null;
            string subMenu = null;
            int roleId = 0;
            SortedList<int, string> numToName = new SortedList<int, string>();
            foreach (var Item in CoreFactory.menuMappingist)
            {
                parentMenu = null;
                value = null;
                roleName = null;
                subMenu = null;
                roleId = 0;

                if (Item.TargetURL == null || Item.TargetURL == "")
                {

                    if (!numToName.ContainsKey(Convert.ToInt32(Item.MenuMasterId)))
                    {
                        numToName.Add(Convert.ToInt32(Item.MenuMasterId), (Item.MenuName));
                    }

                    roleName = Item.RoleName;
                    parentMenu = Item.MenuName;
                    roleId = Item.RoleId;
                }
                else
                {
                    if (numToName.ContainsKey(Convert.ToInt32(Item.ParentId)))
                    {
                        numToName.TryGetValue(Convert.ToInt32(Item.ParentId), out value);
                    }
                    if (value != null)
                        parentMenu = value;
                    roleName = Item.RoleName;
                    subMenu = Item.MenuName;
                }
                modelList.Add(new MenuMasterModel()
                {
                    RoleName = roleName,
                    ParentMenu = parentMenu,
                    SubMenu = subMenu,

                    IsActive = Item.IsActive
                });

            }
            return View(modelList);
        }

        public ActionResult AddMenuMapping()
        {
            MenuMasterModel model = new MenuMasterModel();

            ViewBag.RoleList = BALFactory.dropdownsBAL.GetRole();
            ViewBag.UserList = BALFactory.dropdownsBAL.GetUserMasterList();
            ViewBag.ParentMenuList = BALFactory.dropdownsBAL.GetParentMenu();
            ViewBag.SubMenuList = BALFactory.dropdownsBAL.GetSubMenu(model.ParentMenuId);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddMenuMapping(MenuMasterModel model)
        {
            CoreFactory.menuMasterEntity = new MenuMasterEntity();
            CoreFactory.menuMasterEntity.RoleId = model.RoleId;
            CoreFactory.menuMasterEntity.ParentMenuId = model.ParentMenuId;
            CoreFactory.menuMasterEntity.SubMenuId = model.SubMenuId;
            //CoreFactory.menuMasterEntity.EnteredBy = LIMS.User.UserMasterID;
            CoreFactory.menuMasterEntity.EnteredDate = DateTime.Now;
            CoreFactory.menuMasterEntity.IsActive = model.IsActive;

            if (model.MenuRoleBarnchMappingId == 0)
            {

                var checkv = BALFactory.menuMasterBAL.GetDataall(model.RoleId, model.ParentMenuId, model.SubMenuId);
                if (checkv != null)
                {
                    return Json(new { Status = checkv, message = "Menu Already Mapped" }, JsonRequestBehavior.AllowGet);


                }
                else
                {
                    var checkPMI = BALFactory.menuMasterBAL.GetDataill(model.RoleId, model.ParentMenuId);
                    if (checkPMI == null)
                    {
                        if (model.ParentMenuId != 0)
                            strStatus = BALFactory.menuMasterBAL.AddMappingMenu(CoreFactory.menuMasterEntity);
                    }

                    if (model.SubMenuId != 0)

                        strStatus = BALFactory.menuMasterBAL.AddMappingMenu2(CoreFactory.menuMasterEntity, 1);
                    return Json(new { Status = strStatus, message = "Menu Mapping has been registered successfully." }, JsonRequestBehavior.AllowGet);
                    //return RedirectToAction("MenuMappingList");
                }
            }

            //Use for update
            else
            {
                //strStatus = BALFactory.menuMasterBAL.UpdateMappingMenu(CoreFactory.menuMasterEntity);
                //return Json(new { Status = strStatus, message = "Parent Menu has been updated successfully." }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("MenuMappingList");
            }
        }
        #endregion
       
       
    }
}