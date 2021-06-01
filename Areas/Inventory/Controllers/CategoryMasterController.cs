using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Core;
using LIMS_DEMO.Areas.Inventory.Models;
using LIMS_DEMO.Core.Inventory;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.DropDowns;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.DropDown;
using LIMS_DEMO.BAL.Inventory;
namespace LIMS_DEMO.Areas.Inventory.Controllers
{
    [RouteArea("Inventory")]
    public class CategoryMasterController : Controller
    {
        public CategoryMasterController()
        {
            BALFactory.NewInventoryBAL = new NewInventoryBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }
        // commented by ashwini
        // GET: Inventory/CategoryMaster
        // commented by ashwini thakre 20_2_2021 ...at 10.28 AM 
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        
       
        public ActionResult Create()
        {
            ViewBag.InvTyp = BALFactory.dropdownsBAL.GetInventoryTypeList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public ActionResult Create(InventoryCategoryMasterModel model)
        {
           
                Core.Inventory.CategoryEntity catagoryEntity = new Core.Inventory.CategoryEntity();
                catagoryEntity.CategoryName = model.CategoryName;
                catagoryEntity.InventoryTypeID = model.InventoryTypeID;
                catagoryEntity.IsActive = true;
                catagoryEntity.InsertedBy = 0;
                catagoryEntity.InsertedDate = DateTime.Now.ToLocalTime();
                string status = BAL.BALFactory.NewInventoryBAL.AddCategory(catagoryEntity);
                if (status == "success")
                {
                    return RedirectToAction("List");
                    //strStatus = BALFactory.addItemBAL.AddPurchaseSupplierDetails(CoreFactory.purchaseEntity);
                    //return Json(new { Status = strStatus, message = "Created successfully." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = "success", message = "Record created Failed." }, JsonRequestBehavior.AllowGet);
                }
       
        }

        [HttpGet]
      
        public ActionResult Edit(int? CatagoryMasterID = 0)
        {
            InventoryTypeMasterModel model = new InventoryTypeMasterModel();
            if (CatagoryMasterID != 0)
            {
                var result = BALFactory.NewInventoryBAL.Edit((int)CatagoryMasterID);
                if(result != null)
                {
                    model.CategoryName = result.CategoryName;
                    model.CatagoryMasterID = result.CatagoryMasterID;
                    model.InventoryTypeID = result.InventoryTypeID;
                    model.InventoryTypeName = result.InventoryTypeName;
                    model.IsActive = result.IsActive;
                }
            }
            ViewBag.InvTyp = BALFactory.dropdownsBAL.GetInventoryTypeList();
            return View(model);
        }
        [HttpPost]
       
        public ActionResult Edit(InventoryCategoryMasterModel model)
        {
            Core.Inventory.CategoryEntity categoryEntity = new Core.Inventory.CategoryEntity();
            categoryEntity.CategoryName = model.CategoryName;
            categoryEntity.CatagoryMasterID = model.CatagoryMasterID;
            categoryEntity.InventoryTypeID = model.InventoryTypeID;
            categoryEntity.InventoryTypeName = model.InventoryTypeName;
            categoryEntity.IsActive = model.IsActive;
            categoryEntity.InsertedBy = 0;
            categoryEntity.InsertedDate = DateTime.Now.ToLocalTime();
            var result = BAL.BALFactory.NewInventoryBAL.EditCatagory(categoryEntity);
            if (result == "success")
            {
                return RedirectToAction("List");

            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Json(new { status = "success", message = "Record Updated Failed." });
            }
        }
        [HttpGet]
       
        public ActionResult List()
        {
            CoreFactory.categoryList = BALFactory.NewInventoryBAL.GetInventoryCategoryList();
            List<InventoryCategoryMasterModel> CategoryDataList = new List<InventoryCategoryMasterModel>();
            foreach(var item in CoreFactory.categoryList)
            {
                CategoryDataList.Add(new InventoryCategoryMasterModel()
                { 
                   CategoryName=item.CategoryName,
                   CatagoryMasterID=item.CatagoryMasterID,
                   InventoryTypeID=item.InventoryTypeID,
                   InventoryTypeName=item.InventoryTypeName,
                   IsActive=item.IsActive
                });
            }
            return View(CategoryDataList);
        }
        [HttpGet]
       
        public ActionResult Delete(int? CatagoryMasterID = 0)
        {
            InventoryTypeMasterModel model = new InventoryTypeMasterModel();
            if (CatagoryMasterID != 0)
            {
                var result = BALFactory.NewInventoryBAL.Edit((int)CatagoryMasterID);
                if (result != null)
                {
                    model.CategoryName = result.CategoryName;
                    model.CatagoryMasterID = result.CatagoryMasterID;
                    model.InventoryTypeID = result.InventoryTypeID;
                    model.InventoryTypeName = result.InventoryTypeName;
                    model.IsActive = result.IsActive;
                }
            }
            ViewBag.InvTyp = BALFactory.dropdownsBAL.GetInventoryTypeList();
            return View(model);
        }
        [HttpPost]
        
        public ActionResult Delete(InventoryCategoryMasterModel model)
        {
            Core.Inventory.CategoryEntity categoryEntity = new CategoryEntity();
            categoryEntity.CatagoryMasterID = model.CatagoryMasterID;
            var result = BALFactory.NewInventoryBAL.DeleteCategory(categoryEntity);
            if (result == "success")
            {
                return RedirectToAction("List");
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Json(new { status = "success", message = "Record Delete Failed." });
            }
        }
        [HttpGet]
        
        public ActionResult View(int? CatagoryMasterID=0)
        {
            InventoryTypeMasterModel model = new InventoryTypeMasterModel();
            if (CatagoryMasterID != 0)
            {
                var result = BALFactory.NewInventoryBAL.Edit((int)CatagoryMasterID);
                if (result != null)
                {
                    model.CategoryName = result.CategoryName;
                    model.CatagoryMasterID = result.CatagoryMasterID;
                    model.InventoryTypeID = result.InventoryTypeID;
                    model.InventoryTypeName = result.InventoryTypeName;
                    model.IsActive = result.IsActive;
                }
            }
            ViewBag.InvTyp = BALFactory.dropdownsBAL.GetInventoryTypeList();
            return View(model);
        }
    }
}