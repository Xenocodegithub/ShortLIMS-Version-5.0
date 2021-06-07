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
using System.Configuration;

namespace LIMS_DEMO.Areas.Inventory.Controllers
{
    [RouteArea("Inventory")]
    public class InventoryItemMasterController : Controller
    {
        // GET: Inventory/InventoryItemMaster
        //private string _connectioString = Convert.ToString(ConfigurationManager.ConnectionStrings["Main.ConnectionString"]);
        public InventoryItemMasterController()
        {
            BALFactory.NewInventoryBAL = new NewInventoryBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }
       
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult List()
        {
            bool InventoryTypeMasterIsActive = true;
            bool InventoryTypeMasterIsDeleted = false;
            bool InventoryCategoryMasterIsActive = true;
            bool InventoryCategoryMasterIsDeleted = false;
            bool InventoryItemMasterIsActive = true;
            bool InventoryItemMasterIsDeleted = false;
            bool IsActive = true;
            int InventoryTypeID = 2;
            string Mode = "SELECT ALL ITEM";
            CoreFactory.InventoryItemList = BALFactory.NewInventoryBAL.GetInventoryItemList(InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, IsActive, Mode);

            List<InventoryItemMasterModel> modellist = new List<InventoryItemMasterModel>();
            foreach (var item in CoreFactory.InventoryItemList)
            {
                modellist.Add(new InventoryItemMasterModel()
                {
                    ItemMasterID = item.ItemMasterID,
                    CatagoryMasterID = item.CatagoryMasterID,
                    NameOfStock = item.NameOfStock,
                    Code = item.Code,
                    UnitID = item.UnitID,
                    MinimumStock = (decimal)item.MinimumStock,
                    IsActive = item.IsActive,
                    InventoryTypeID = (byte)item.InventoryTypeID,
                    AvailableStock = item.AvailableStock,
                    CategoryName = item.CategoryName,
                    InventoryTypeName = item.InventoryTypeName,
                    Capacity = item.Capacity,
                    ItemID = item.ItemID
                });
            }
            ViewBag.InvCategoryList = BALFactory.dropdownsBAL.GetFilteredInvCatogaryList(InventoryTypeID);
            ViewBag.InvInventoryTypeList = BALFactory.dropdownsBAL.GetInventoryCategoryType();
            return View(modellist);
        }

        [HttpGet]
        public ActionResult StockIssueToUser()
        {
            bool InventoryTypeMasterIsActive = true;
            bool InventoryTypeMasterIsDeleted = false;
            bool InventoryCategoryMasterIsActive = true;
            bool InventoryCategoryMasterIsDeleted = false;
            bool InventoryItemMasterIsActive = true;
            bool InventoryItemMasterIsDeleted = false;
            bool IsActive = true;
            string Mode = "SELECT ALL ITEM";
            CoreFactory.InventoryItemList = BALFactory.NewInventoryBAL.GetInventoryItemList(InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, IsActive, Mode);

            List<InventoryItemMasterModel> modellist = new List<InventoryItemMasterModel>();
            foreach (var item in CoreFactory.InventoryItemList)
            {
                modellist.Add(new InventoryItemMasterModel()
                {
                    ItemMasterID = item.ItemMasterID,
                    CatagoryMasterID = item.CatagoryMasterID,
                    NameOfStock = item.NameOfStock,
                    Code = item.Code,
                    UnitID = item.UnitID,
                    MinimumStock = (decimal)item.MinimumStock,
                    IsActive = item.IsActive,
                    InventoryTypeID = (byte)item.InventoryTypeID,
                    AvailableStock = item.AvailableStock,
                    CategoryName = item.CategoryName,
                    InventoryTypeName = item.InventoryTypeName,
                  
                    Capacity = item.Capacity,
                    InventoryCapacityMasterId = item.InventoryCapacityMasterId,
                    ItemID = item.ItemID,
                });
            }
            return View(modellist);
        }    
        [HttpGet]
      
        public ActionResult AddStockIssueToUser(int ItemMasterId = 0)
        {
            InventoryItemMasterModel model = new InventoryItemMasterModel();
            if (ItemMasterId != 0)
            {
                var result = BALFactory.NewInventoryBAL.GetItemDetailsWithID(ItemMasterId);
                if(result != null)
                {
                    model.NameOfStock = result.NameOfStock;
                    model.MinimumStock = result.MinimumStock;
                    model.AvailableStock = result.AvailableStock;
                    model.IsActive = result.IsActive;
                    model.ItemMasterID=result.ItemMasterID;
                }
            }
            bool InventoryTypeMasterIsActive = true;
            bool InventoryTypeMasterIsDeleted = false;
            bool InventoryCategoryMasterIsActive = true;
            bool InventoryCategoryMasterIsDeleted = false;
            bool InventoryItemMasterIsActive = true;
            bool InventoryItemMasterIsDeleted = false;
            bool IsActive = true;
            string Mode = "SELECT ALL ITEM";
            CoreFactory.InventoryItemList = BALFactory.NewInventoryBAL.GetInventoryItemList(InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, IsActive, Mode);
            if(CoreFactory.InventoryItemList != null)
            {
                ViewBag.InventoryItemList = CoreFactory.InventoryItemList;
            }

           // string Mode1 = "GetEmploye";
            CoreFactory.EmployeeList = BALFactory.NewInventoryBAL.GetEmployeeList();
            if(CoreFactory.EmployeeList != null)
            {
                ViewBag.EmployeeList = CoreFactory.EmployeeList;
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
   
        public ActionResult AddStockIssueToUser(InventoryItemMasterModel model)
        {
            if (model != null)
            {
                InventoryItemEntity inventoryItemEntity = new InventoryItemEntity();
                inventoryItemEntity.IssueToID = model.IssueToID;
                inventoryItemEntity.IssueToNameID = model.IssueToNameID;
                inventoryItemEntity.IssueDate = model.IssueDate;
                inventoryItemEntity.IssueQty = model.IssueQty;
                inventoryItemEntity.ItemMasterID = model.ItemMasterID;
                inventoryItemEntity.IsActive = true;
                inventoryItemEntity.AvailableStock = model.AvailableStock;
                inventoryItemEntity.InsertedBy = 0;
                inventoryItemEntity.InsertedDate = DateTime.Now.ToLocalTime();
                string status = BALFactory.NewInventoryBAL.InsertStockIssueData(inventoryItemEntity);
                if (status == "success")
                {
                    return RedirectToAction("StockLogDataList");
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Json(new { status = "fail", message = "Record created failed." });
                }
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Json(new { status = "fail", message = "Internal server error occured." });
            }
        }
        [HttpGet]
        
        public ActionResult Create()
        {
            // get list of Inventory Type
            ViewBag.InvTyp = BALFactory.dropdownsBAL.GetInventoryTypeList();

            // get list of Category Type
            ViewBag.CategoryType = BALFactory.dropdownsBAL.GetCategoryTypeList();

            // get list of Capacity Type
            ViewBag.GetUnitType = BALFactory.dropdownsBAL.GetUnitTypeList();

            // get list of Unit Type
            ViewBag.GetCapacityType = BALFactory.dropdownsBAL.GetCapacityList();

            return View();
        }
        public JsonResult GetInvCategory(int InventoryTypeId)
        {
            //AddInventoryItemModel model = new AddInventoryItemModel();
            var InvCategoryList = BALFactory.dropdownsBAL.GetFilteredInvCatogaryList(InventoryTypeId);
            return Json(new { result = InvCategoryList }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public ActionResult Create(InventoryItemMasterModel model)
        {
            InventoryItemEntity inventoryItemEntity = new InventoryItemEntity();
            inventoryItemEntity.InventoryTypeID = model.InventoryTypeID;
            inventoryItemEntity.CatagoryMasterID = model.CatagoryMasterID;
            inventoryItemEntity.NameOfStock = model.NameOfStock;
            inventoryItemEntity.UnitID = model.UnitID;
            inventoryItemEntity.Code = model.Code;
            inventoryItemEntity.Capacity = model.Capacity;
            if (model.MinimumStock == null)
            {
                inventoryItemEntity.MinimumStock = 0;
            }
            else
            {
                inventoryItemEntity.MinimumStock = model.MinimumStock;
            }
            if (model.AvailableStock == null || model.AvailableStock == "")
            {
                inventoryItemEntity.AvailableStock = "0";

            }
            else
            {
                inventoryItemEntity.AvailableStock = model.AvailableStock;
            }
            inventoryItemEntity.IsActive = true;
            inventoryItemEntity.InsertedBy = 0;
            inventoryItemEntity.InsertedDate = DateTime.Now.ToLocalTime();
            string result = BALFactory.NewInventoryBAL.AddInventoryItem(inventoryItemEntity);
            if (result == "success")
            {
                return RedirectToAction("List");
            }

            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Json(new { status = "success", message = "Record created Failed." });
            }
        }
        [HttpGet]
       
        public ActionResult Edit(int? ItemMasterId = 0, int? ID = 0)
        {
            InventoryItemMasterModel model = new InventoryItemMasterModel();
            if(ItemMasterId != 0)
            {
                bool InventoryTypeMasterIsActive = true;
                bool InventoryTypeMasterIsDeleted = true;
                bool InventoryCategoryMasterIsActive = true;
                bool InventoryCategoryMasterIsDeleted = true;
                bool InventoryItemMasterIsActive = true;
                bool InventoryItemMasterIsDeleted = true;
                bool IsActive = true;
                int ITID = (Int32)ItemMasterId;
                int InventoryCategoryMasterID = 0;
                int InventoryTypeMasterID = 0;
                string Mode= "SELECT ITEM BY ID";
                CoreFactory.InventoryItemList = BALFactory.NewInventoryBAL.SelectInventoryDetails(ITID, InventoryCategoryMasterID, InventoryTypeMasterID, InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, IsActive, Mode);
                if(CoreFactory.InventoryItemList != null)
                {
                    model.ItemMasterID = CoreFactory.InventoryItemList[0].ItemMasterID;
                    model.ItemID = CoreFactory.InventoryItemList[0].ItemID;
                    model.NameOfStock = CoreFactory.InventoryItemList[0].NameOfStock;
                    model.Code = CoreFactory.InventoryItemList[0].Code;
                    model.IsActive = CoreFactory.InventoryItemList[0].IsActive;
                    model.CatagoryMasterID = CoreFactory.InventoryItemList[0].CatagoryMasterID;
                    model.InventoryTypeID = CoreFactory.InventoryItemList[0].InventoryTypeID;
                    model.Capacity = CoreFactory.InventoryItemList[0].Capacity;
                    model.UnitID = CoreFactory.InventoryItemList[0].UnitID;
                    if (CoreFactory.InventoryItemList[0].MinimumStock == null || CoreFactory.InventoryItemList[0].MinimumStock == 0)
                    {
                        model.MinimumStock = 0;
                    }
                    else if(CoreFactory.InventoryItemList[0].MinimumStock != null || CoreFactory.InventoryItemList[0].MinimumStock != 0)
                    {
                        model.MinimumStock = CoreFactory.InventoryItemList[0].MinimumStock;
                    }
                    if (CoreFactory.InventoryItemList[0].AvailableStock == null || CoreFactory.InventoryItemList[0].AvailableStock == "")
                    {
                        model.AvailableStock = "0";
                    }
                    else if (CoreFactory.InventoryItemList[0].AvailableStock != null || CoreFactory.InventoryItemList[0].AvailableStock != "")
                    {
                        model.AvailableStock = Convert.ToString(CoreFactory.InventoryItemList[0].AvailableStock);
                    }
                }
            }
            else if (ID != 0)
            {
                bool InventoryTypeMasterIsActive = true;
                bool InventoryTypeMasterIsDeleted = false;
                bool InventoryCategoryMasterIsActive = true;
                bool InventoryCategoryMasterIsDeleted = false;
                bool InventoryItemMasterIsActive = true;
                bool InventoryItemMasterIsDeleted = false;
                bool IsActive = true;
                bool IsDeleted = false;
                int ITID = (Int32)ID;
                int InventoryCategoryMasterID = 0;
                int InventoryTypeMasterID = 0;
                string Mode = "SELECT ITEM BY ID";
                CoreFactory.InventoryItemList = BALFactory.NewInventoryBAL.SelectInventoryDetails(ITID, InventoryCategoryMasterID, InventoryTypeMasterID, InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, IsActive, Mode);
                if (CoreFactory.InventoryItemList != null)
                {
                    model.ItemMasterID = CoreFactory.InventoryItemList[0].ItemMasterID;
                    model.ItemID = CoreFactory.InventoryItemList[0].ItemID;
                    model.NameOfStock = CoreFactory.InventoryItemList[0].NameOfStock;
                    model.Code = CoreFactory.InventoryItemList[0].Code;           
                    model.IsActive = (bool)CoreFactory.InventoryItemList[0].IsActive;
                    model.CatagoryMasterID = (byte)CoreFactory.InventoryItemList[0].CatagoryMasterID;
                    model.InventoryTypeID = (byte)CoreFactory.InventoryItemList[0].InventoryTypeID;
                    model.Capacity = CoreFactory.InventoryItemList[0].Capacity;
                    model.UnitID = (byte)CoreFactory.InventoryItemList[0].UnitID;
                    if (CoreFactory.InventoryItemList[0].MinimumStock == null || CoreFactory.InventoryItemList[0].MinimumStock == 0)
                    {
                        model.MinimumStock = 0;
                    }
                    else if (CoreFactory.InventoryItemList[0].MinimumStock != null || CoreFactory.InventoryItemList[0].MinimumStock != 0)
                    {
                        model.MinimumStock =CoreFactory.InventoryItemList[0].MinimumStock;
                    }
                    if (CoreFactory.InventoryItemList[0].AvailableStock == null || CoreFactory.InventoryItemList[0].AvailableStock == "")
                    {
                        model.AvailableStock = "0";
                    }
                    else if (CoreFactory.InventoryItemList[0].AvailableStock != null || CoreFactory.InventoryItemList[0].AvailableStock != "")
                    {
                        model.AvailableStock = Convert.ToString(CoreFactory.InventoryItemList[0].AvailableStock);
                    }
                }
            }
            // get list of Inventory Type
            ViewBag.InvTyp = BALFactory.dropdownsBAL.GetInventoryTypeList();

            // get list of Category Type
            ViewBag.CategoryType = BALFactory.dropdownsBAL.GetCategoryTypeList();

            // get list of Capacity Type
            ViewBag.GetUnitType = BALFactory.dropdownsBAL.GetUnitTypeList();

            // get list of Unit Type
            ViewBag.GetCapacityType = BALFactory.dropdownsBAL.GetCapacityList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InventoryItemMasterModel model)
        {
            InventoryItemEntity inventoryItemEntity = new InventoryItemEntity();
            inventoryItemEntity.InventoryTypeID = model.InventoryTypeID;
            inventoryItemEntity.CatagoryMasterID = model.CatagoryMasterID;
            inventoryItemEntity.NameOfStock = model.NameOfStock;
            inventoryItemEntity.UnitID = model.UnitID;
            inventoryItemEntity.Code = model.Code;
            inventoryItemEntity.Capacity = model.Capacity;
            if (model.MinimumStock == null)
            {
                inventoryItemEntity.MinimumStock = 0;
            }
            else
            {
                inventoryItemEntity.MinimumStock = model.MinimumStock;
            }
            if (inventoryItemEntity.AvailableStock == null || model.AvailableStock == "")
            {
                inventoryItemEntity.AvailableStock = "0";
            }
            else if (inventoryItemEntity.AvailableStock != null || model.AvailableStock != "")
            {
                inventoryItemEntity.AvailableStock = model.AvailableStock;
            }
            inventoryItemEntity.IsActive = true;
            inventoryItemEntity.ItemMasterID = model.ItemMasterID;
            inventoryItemEntity.ItemID = model.ItemID;
            inventoryItemEntity.ModifiedBy = 0;
            inventoryItemEntity.ModifiedDate =DateTime.Now.ToLocalTime();
            string result = BALFactory.NewInventoryBAL.EditInventoryItem(inventoryItemEntity);
            if(result == "success")
            {
                return RedirectToAction("List");
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Json(new { status = "success", message = "Record created Failed." });
            }
        }
        
        
        [HttpGet]
        
        public ActionResult ConsumableListItems()
        {

            return View();
        }
        [HttpGet]
       
        public ActionResult NonConsumableListItems()
        {
            return View();
        }
        [HttpGet]
       
        public ActionResult StockLogDataList()
        {

            bool InventoryTypeMasterIsActive = true;
            bool InventoryTypeMasterIsDeleted = false;
            bool InventoryCategoryMasterIsActive = true;
            bool InventoryCategoryMasterIsDeleted = false;
            bool InventoryItemMasterIsActive = true;
            bool InventoryItemMasterIsDeleted = false;
            bool IsActive = true;
            string  Mode = "SELECT STOCK LOG";

            CoreFactory.InventoryItemList=BALFactory.NewInventoryBAL.GetStockItemDataList(InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, IsActive, Mode);

            List<InventoryItemMasterModel> modellist = new List<InventoryItemMasterModel>();
            foreach (var item in CoreFactory.InventoryItemList)
            {
                modellist.Add(new InventoryItemMasterModel()
                {
                    ItemMasterID = item.ItemMasterID,
                    EmpName =item.EmpName,
                    EmpId = item.EmpId,
                    IssueQty = item.IssueQty,
                    IssueDate = item.IssueDate,
                    CatagoryMasterID = item.CatagoryMasterID,
                    CategoryName = item.CategoryName,
                    InventoryTypeID = item.InventoryTypeID,
                    InventoryTypeName = item.InventoryTypeName,
                    IsActive = item.IsActive,
                    NameOfStock = item.NameOfStock
                });
            }
            return View(modellist);
        }
        [HttpGet]
      
        public ActionResult View(int? ItemMasterId = 0, int? ID = 0)
        {
            InventoryItemMasterModel model = new InventoryItemMasterModel();
            if (ItemMasterId != 0)
            {
                bool InventoryTypeMasterIsActive = true;
                bool InventoryTypeMasterIsDeleted = true;
                bool InventoryCategoryMasterIsActive = true;
                bool InventoryCategoryMasterIsDeleted = true;
                bool InventoryItemMasterIsActive = true;
                bool InventoryItemMasterIsDeleted = true;
                bool IsActive = true;
                int ITID = (Int32)ItemMasterId;
                int InventoryCategoryMasterID = 0;
                int InventoryTypeMasterID = 0;
                string Mode = "SELECT ITEM BY ID";
                CoreFactory.InventoryItemList = BALFactory.NewInventoryBAL.SelectInventoryDetails(ITID, InventoryCategoryMasterID, InventoryTypeMasterID, InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, IsActive, Mode);
                if (CoreFactory.InventoryItemList != null)
                {
                    model.ItemMasterID = CoreFactory.InventoryItemList[0].ItemMasterID;
                    model.ItemID = CoreFactory.InventoryItemList[0].ItemID;
                    model.NameOfStock = CoreFactory.InventoryItemList[0].NameOfStock;
                    model.Code = CoreFactory.InventoryItemList[0].Code;
                    if (CoreFactory.InventoryItemList[0].MinimumStock == null || CoreFactory.InventoryItemList[0].MinimumStock == 0)
                    {
                        model.MinimumStock = 0;
                    }
                    else
                    {
                        model.MinimumStock = CoreFactory.InventoryItemList[0].MinimumStock;
                    }
                    model.IsActive = CoreFactory.InventoryItemList[0].IsActive;
                    model.CatagoryMasterID = CoreFactory.InventoryItemList[0].CatagoryMasterID;
                    model.InventoryTypeID = CoreFactory.InventoryItemList[0].InventoryTypeID;
                    model.Capacity = CoreFactory.InventoryItemList[0].Capacity;
                    model.UnitID = CoreFactory.InventoryItemList[0].UnitID;
                    if (CoreFactory.InventoryItemList[0].AvailableStock == null || CoreFactory.InventoryItemList[0].AvailableStock == "")
                    {
                        model.AvailableStock = "0";
                    }
                    else if (CoreFactory.InventoryItemList[0].AvailableStock != null || CoreFactory.InventoryItemList[0].AvailableStock != "")
                    {
                        model.AvailableStock = Convert.ToString(CoreFactory.InventoryItemList[0].AvailableStock);
                    }

                }
            }
            else if (ID != 0)
            {
                bool InventoryTypeMasterIsActive = true;
                bool InventoryTypeMasterIsDeleted = false;
                bool InventoryCategoryMasterIsActive = true;
                bool InventoryCategoryMasterIsDeleted = false;
                bool InventoryItemMasterIsActive = true;
                bool InventoryItemMasterIsDeleted = false;
                bool IsActive = true;
                bool IsDeleted = false;
                int ITID = (Int32)ID;
                int InventoryCategoryMasterID = 0;
                int InventoryTypeMasterID = 0;
                string Mode = "SELECT ITEM BY ID";
                CoreFactory.InventoryItemList = BALFactory.NewInventoryBAL.SelectInventoryDetails(ITID, InventoryCategoryMasterID, InventoryTypeMasterID, InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, IsActive, Mode);
                if (CoreFactory.InventoryItemList != null)
                {
                    model.ItemMasterID = CoreFactory.InventoryItemList[0].ItemMasterID;
                    model.ItemID = CoreFactory.InventoryItemList[0].ItemID;
                    model.NameOfStock = CoreFactory.InventoryItemList[0].NameOfStock;
                    model.Code = CoreFactory.InventoryItemList[0].Code;
                    if (CoreFactory.InventoryItemList[0].MinimumStock == null || CoreFactory.InventoryItemList[0].MinimumStock == 0)
                    {
                        model.MinimumStock = 0;
                    }
                    else
                    {
                        model.MinimumStock = CoreFactory.InventoryItemList[0].MinimumStock;
                    }
                    model.IsActive = (bool)CoreFactory.InventoryItemList[0].IsActive;
                    model.CatagoryMasterID = (byte)CoreFactory.InventoryItemList[0].CatagoryMasterID;
                    model.InventoryTypeID = (byte)CoreFactory.InventoryItemList[0].InventoryTypeID;
                    model.Capacity =CoreFactory.InventoryItemList[0].Capacity;
                    model.UnitID = (byte)CoreFactory.InventoryItemList[0].UnitID;
                    if (CoreFactory.InventoryItemList[0].AvailableStock == null || CoreFactory.InventoryItemList[0].AvailableStock == "")
                    {
                        model.AvailableStock = "0";
                    }
                    else if (CoreFactory.InventoryItemList[0].AvailableStock != null || CoreFactory.InventoryItemList[0].AvailableStock != "")
                    {
                        model.AvailableStock = Convert.ToString(CoreFactory.InventoryItemList[0].AvailableStock);
                    }
                }
            }
            // get list of Inventory Type
            ViewBag.InvTyp = BALFactory.dropdownsBAL.GetInventoryTypeList();

            // get list of Category Type
            ViewBag.CategoryType = BALFactory.dropdownsBAL.GetCategoryTypeList();

            // get list of Capacity Type
            ViewBag.GetUnitType = BALFactory.dropdownsBAL.GetUnitTypeList();

            // get list of Unit Type
            ViewBag.GetCapacityType = BALFactory.dropdownsBAL.GetCapacityList();

            return View(model);
        }

        [HttpGet]
     
        public ActionResult Delete(int? ItemMasterId = 0, int? ID = 0)
        {
            InventoryItemMasterModel model = new InventoryItemMasterModel();
            if (ItemMasterId != 0)
            {
                bool InventoryTypeMasterIsActive = true;
                bool InventoryTypeMasterIsDeleted = true;
                bool InventoryCategoryMasterIsActive = true;
                bool InventoryCategoryMasterIsDeleted = true;
                bool InventoryItemMasterIsActive = true;
                bool InventoryItemMasterIsDeleted = true;
                bool IsActive = true;
                int ITID = (Int32)ItemMasterId;
                int InventoryCategoryMasterID = 0;
                int InventoryTypeMasterID = 0;
                string Mode = "SELECT ITEM BY ID";
                CoreFactory.InventoryItemList = BALFactory.NewInventoryBAL.SelectInventoryDetails(ITID, InventoryCategoryMasterID, InventoryTypeMasterID, InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, IsActive, Mode);
                if (CoreFactory.InventoryItemList != null)
                {
                    model.ItemMasterID = CoreFactory.InventoryItemList[0].ItemMasterID;
                    model.ItemID = CoreFactory.InventoryItemList[0].ItemID;
                    model.NameOfStock = CoreFactory.InventoryItemList[0].NameOfStock;
                    model.Code = CoreFactory.InventoryItemList[0].Code;
                    if (CoreFactory.InventoryItemList[0].MinimumStock == null || CoreFactory.InventoryItemList[0].MinimumStock == 0)
                    {
                        model.MinimumStock = 0;
                    }
                    else
                    {
                        model.MinimumStock = CoreFactory.InventoryItemList[0].MinimumStock;
                    }
                    model.IsActive = CoreFactory.InventoryItemList[0].IsActive;
                    model.CatagoryMasterID = CoreFactory.InventoryItemList[0].CatagoryMasterID;
                    model.InventoryTypeID = CoreFactory.InventoryItemList[0].InventoryTypeID;
                    model.Capacity = CoreFactory.InventoryItemList[0].Capacity;
                    model.UnitID = CoreFactory.InventoryItemList[0].UnitID;
                    if (CoreFactory.InventoryItemList[0].AvailableStock == null || CoreFactory.InventoryItemList[0].AvailableStock == "")
                    {
                        model.AvailableStock = "0";
                    }
                    else if (CoreFactory.InventoryItemList[0].AvailableStock != null || CoreFactory.InventoryItemList[0].AvailableStock != "")
                    {
                        model.AvailableStock = Convert.ToString(CoreFactory.InventoryItemList[0].AvailableStock);
                    }

                }
            }
            else if (ID != 0)
            {
                bool InventoryTypeMasterIsActive = true;
                bool InventoryTypeMasterIsDeleted = false;
                bool InventoryCategoryMasterIsActive = true;
                bool InventoryCategoryMasterIsDeleted = false;
                bool InventoryItemMasterIsActive = true;
                bool InventoryItemMasterIsDeleted = false;
                bool IsActive = true;
                bool IsDeleted = false;
                int ITID = (Int32)ID;
                int InventoryCategoryMasterID = 0;
                int InventoryTypeMasterID = 0;
                string Mode = "SELECT ITEM BY ID";
                CoreFactory.InventoryItemList = BALFactory.NewInventoryBAL.SelectInventoryDetails(ITID, InventoryCategoryMasterID, InventoryTypeMasterID, InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, IsActive, Mode);
                if (CoreFactory.InventoryItemList != null)
                {
                    model.ItemMasterID = CoreFactory.InventoryItemList[0].ItemMasterID;
                    model.ItemID = CoreFactory.InventoryItemList[0].ItemID;
                    model.NameOfStock = CoreFactory.InventoryItemList[0].NameOfStock;
                    model.Code = CoreFactory.InventoryItemList[0].Code;
                    if (CoreFactory.InventoryItemList[0].MinimumStock == null || CoreFactory.InventoryItemList[0].MinimumStock == 0)
                    {
                        model.MinimumStock = 0;
                    }
                    else
                    {
                        model.MinimumStock = CoreFactory.InventoryItemList[0].MinimumStock;
                    }
                    model.IsActive = (bool)CoreFactory.InventoryItemList[0].IsActive;
                    model.CatagoryMasterID = (byte)CoreFactory.InventoryItemList[0].CatagoryMasterID;
                    model.InventoryTypeID = (byte)CoreFactory.InventoryItemList[0].InventoryTypeID;
                    model.Capacity =CoreFactory.InventoryItemList[0].Capacity;
                    model.UnitID = (byte)CoreFactory.InventoryItemList[0].UnitID;
                    if (CoreFactory.InventoryItemList[0].AvailableStock == null || CoreFactory.InventoryItemList[0].AvailableStock == " ")
                    {
                        model.AvailableStock = "0";
                    }
                    else if (CoreFactory.InventoryItemList[0].AvailableStock != null || CoreFactory.InventoryItemList[0].AvailableStock != "")
                    {
                        model.AvailableStock = Convert.ToString(CoreFactory.InventoryItemList[0].AvailableStock);
                    }
                }
            }
            // get list of Inventory Type
            ViewBag.InvTyp = BALFactory.dropdownsBAL.GetInventoryTypeList();

            // get list of Category Type
            ViewBag.CategoryType = BALFactory.dropdownsBAL.GetCategoryTypeList();

            // get list of Capacity Type
            ViewBag.GetUnitType = BALFactory.dropdownsBAL.GetUnitTypeList();

            // get list of Unit Type
            ViewBag.GetCapacityType = BALFactory.dropdownsBAL.GetCapacityList();

            return View(model);
        }
        [HttpPost]
        
        public ActionResult Delete(InventoryItemMasterModel model)
        {
            Core.Inventory.InventoryItemEntity inventoryItemEntity  = new InventoryItemEntity();
            inventoryItemEntity.ItemMasterID = model.ItemMasterID;
            var result = BALFactory.NewInventoryBAL.DeleteItemCategory(inventoryItemEntity);
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
        [HttpPost]
       
        public ActionResult fathcItemDetails(int ItemID)
        {
            bool InventoryTypeMasterIsActive = true;
            bool InventoryTypeMasterIsDeleted = false;
            bool InventoryCategoryMasterIsActive = true;
            bool InventoryCategoryMasterIsDeleted = false;
            bool IsActive = true;
            bool IsDeleted = false;
            bool InventoryItemMasterIsActive = true;
            bool InventoryItemMasterIsDeleted = false;
            int ID = ItemID;
            int InventoryCategoryMasterID = 0;
            int InventoryTypeMasterID = 0;
            string Mode = "SELECT BY ITEM ID, CATEGORY ID AND INVENTORY ID AND ACTIVE AND DELETED PARAMETER";

            CoreFactory.InventoryItemList = BALFactory.NewInventoryBAL.GetInventoryItemMasterBySearch12(ID, InventoryCategoryMasterID, InventoryTypeMasterID, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, IsActive, Mode);

            if (CoreFactory.InventoryItemList.Count != 0)
            {
                if (CoreFactory.InventoryItemList != null)
                {
                    InventoryItemMasterModel model = new InventoryItemMasterModel();
                    List<InventoryItemMasterModel> ItemDataList = new List<InventoryItemMasterModel>();
                    foreach (var item in CoreFactory.InventoryItemList)
                    {

                        model.ItemID = item.ID;
                        model.CatagoryMasterID = item.CatagoryMasterID;
                        model.CategoryID = item.CatagoryMasterID;
                        model.CatagoryMasterID = item.CatagoryMasterID;
                        model.InventoryTypeID = item.InventoryTypeID;
                        model.TypeID = item.TypeID;
                        model.UnitID = item.UnitID;
                        model.CategoryName = item.CategoryName;
                        model.NameOfStock = item.Name;
                        model.Code = item.Code;
                        model.MinimumStock = item.MinimumStock;
                        model.AvailableStock = item.AvailableStock;
                        model.IsActive = item.IsActive;
                        model.Capacity = item.Capacity;
                        model.InventoryTypeName = item.InventoryTypeName;

                        model.ItemID = item.ID;
                        model.CatagoryMasterID = item.CatagoryMasterID;
                     //   model.CategoryID = item.CatagoryMasterID;
                        model.CatagoryMasterID = item.CatagoryMasterID;
                        model.InventoryTypeID = item.InventoryTypeID;
                        model.TypeID = item.TypeID;
                        model.UnitID = item.UnitID;
                        model.CategoryName = item.CategoryName;
                        model.NameOfStock = item.Name;
                        model.Code = item.Code;
                        model.MinimumStock = item.MinimumStock;
                        model.AvailableStock = item.AvailableStock;
                        model.IsActive = item.IsActive;
                        model.Capacity = item.Capacity;
                        model.InventoryTypeName = item.InventoryTypeName;
                    }
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Json(new { status = "success", message = "Record created successfully.", ItemDetails = model });
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Json(new { status = "info", message = "Internal Server Error Occured" });
                }
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Json(new { status = "fail", message = "Internal Server Error Occured" });
            }
            return View();
        }
    }
}