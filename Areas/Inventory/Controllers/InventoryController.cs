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
using System.Web.UI.WebControls;
using System.Runtime.CompilerServices;
using System.IO;
using System.Net;
using System.Web.Configuration;
using System.Threading.Tasks;
namespace LIMS_DEMO.Areas.Inventory.Controllers
{
    [RouteArea("Inventory")]
    public class InventoryController : Controller
    {
        // GET: Inventory/Inventory
        string _sortOrder = string.Empty;
        string _sortBy = string.Empty;
        string _searchKeywords = string.Empty;
        int _startRow;
        int _rowLength;
        public InventoryController()
        {
            BALFactory.NewInventoryBAL = new NewInventoryBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }
        [HttpGet]
        public ActionResult ConsumableList()
        {
            int TypeId = 1;
            CoreFactory.InventoryBasicDetailsList = BALFactory.NewInventoryBAL.InventoryBasicDetailsList(TypeId);
            List<InventoryBasicDetailModel> ConsumableList = new List<InventoryBasicDetailModel>();
            foreach (var Item in CoreFactory.InventoryBasicDetailsList)
            {
                ConsumableList.Add(new InventoryBasicDetailModel()
                {
                    ID = Item.ID,
                    ItemID = Item.ItemID,
                    TypeID = Item.TypeID,
                    CategoryID = Item.CategoryID,
                    UnitID = Item.UnitID,
                    QuantityType = Item.QuantityType,
                    Quantity = Item.Quantity,
                    Warranty = Item.Warranty,
                    IsActive = Item.IsActive,
                    TotalQuantity = Item.TotalQuantity,
                    PurchaseRequestID = Item.PurchaseRequestID,
                    InventoryBasicDetailsNumber = Item.InventoryBasicDetailsNumber,
                    ItemName = Item.ItemName,
                    CategoryName = Item.CategoryName,
                    Unit = Item.Unit,
                    InventoryBasicDetailsID = Item.InventoryBasicDetailsID,
                    InventoryBasicItemDetailsID = Item.InventoryBasicItemDetailsID,

                });
                ViewBag.InvCategoryList = BALFactory.dropdownsBAL.GetFilteredInvCatogaryList(TypeId);
            }
            return View(ConsumableList);
        }
        [HttpGet]
        public ActionResult NonConsumableList()
        {
            int TypeId = 2;
            CoreFactory.InventoryBasicDetailsList = BALFactory.NewInventoryBAL.InventoryBasicDetailsList(TypeId);
            List<InventoryBasicDetailModel> ConsumableList = new List<InventoryBasicDetailModel>();
            foreach (var Item in CoreFactory.InventoryBasicDetailsList)
            {
                ConsumableList.Add(new InventoryBasicDetailModel()
                {
                    ID = Item.ID,
                    ItemID = Item.ItemID,
                    TypeID = Item.TypeID,
                    CategoryID = Item.CategoryID,
                    UnitID = Item.UnitID,
                    QuantityType = Item.QuantityType,
                    Quantity = Item.Quantity,
                    Warranty = Item.Warranty,
                    IsActive = Item.IsActive,
                    TotalQuantity = Item.TotalQuantity,
                    PurchaseRequestID = Item.PurchaseRequestID,
                    InventoryBasicDetailsNumber = Item.InventoryBasicDetailsNumber,
                    ItemName = Item.ItemName,
                    CategoryName = Item.CategoryName,
                    Unit = Item.Unit,
                    InventoryBasicDetailsID = Item.InventoryBasicDetailsID,
                    InventoryBasicItemDetailsID = Item.InventoryBasicItemDetailsID,

                });
            }
            ViewBag.InvCategoryList = BALFactory.dropdownsBAL.GetFilteredInvCatogaryList(TypeId);
            return View(ConsumableList);
        }

        [HttpGet]
        public ActionResult Index(int ID = 0, short TypeID = 0, int ItemID = 0, int PurchaseRequestID = 0, int InventoryBasicDetailsID = 0, decimal TotalQuantity = 0)
        {
            List<ListItem> _inventoryBasicItem = new List<ListItem>();
            List<ListItem> _FrequencyType = new List<ListItem>();
            List<ListItem> _inventoryItem = new List<ListItem>();

            ViewBag.InvInventoryTypeList = BALFactory.dropdownsBAL.GetInventoryTypeList();
            ViewBag.InvCategoryList = BALFactory.dropdownsBAL.GetCategoryTypeList();
            //ViewBag.InvItemList = BALFactory.dropdownsBAL.GetInventoryItemList(); // old code
            ViewBag.InvUnitList = BALFactory.dropdownsBAL.GetUnitTypeList();
            ViewBag.CapacityListViewBag = BALFactory.dropdownsBAL.GetCapacityList();

            bool InventoryTypeMasterIsActive = true;
            bool InventoryTypeMasterIsDeleted = false;
            bool InventoryCategoryMasterIsActive = true;
            bool InventoryCategoryMasterIsDeleted = false;
            bool InventoryItemMasterIsActive = true;
            bool InventoryItemMasterIsDeleted = false;
            bool IsActive = true;
            int InventoryTypeMasterID1 = TypeID;
            string Mode1 = "SELECT BY ITEM ID, CATEGORY ID AND INVENTORY ID AND ACTIVE AND DELETED PARAMETER";
            CoreFactory.InventoryItemList = BALFactory.NewInventoryBAL.GetInventoryItemList(InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, IsActive, InventoryTypeMasterID1, Mode1);
            if (CoreFactory.InventoryItemList != null)
            {
                foreach (InventoryItemEntity item in CoreFactory.InventoryItemList)
                {
                    _inventoryItem.Add(new ListItem { Text = item.Name, Value = item.ID.ToString() });

                }
            }
            ViewBag.InventoryItemListViewBag1 = new SelectList(_inventoryItem, "Value", "Text");
            ViewBag.ItemIdViewBag = ItemID;
            ViewBag.InventoryTypeMasterIDViewBag = TypeID;
            if (ID > 0)
            {
                ViewBag.BasicDetailsIdViewBag = ID;
                ViewBag.InventoryTypeMasterIDViewBag = TypeID;

            }
            if (PurchaseRequestID > 0)
            {
                ViewBag.PurchaseRequestId = PurchaseRequestID;
                ViewBag.InventoryItemListViewBag = new SelectList(_inventoryBasicItem, "Value", "Text");
                CoreFactory.inventoryBasicDetailEntity = BALFactory.NewInventoryBAL.GetInventoryItemCategoryDetail((Int32)PurchaseRequestID);
                var jsonInventoryItemMasterDTO = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(CoreFactory.inventoryBasicDetailEntity);
                ViewBag.InventoryRequestViewBag = jsonInventoryItemMasterDTO;
                _FrequencyType.Add(new ListItem { Text = "Monthly", Value = "1" });
                _FrequencyType.Add(new ListItem { Text = "Quarterly", Value = "2" });
                _FrequencyType.Add(new ListItem { Text = "Half Yearly", Value = "3" });
                _FrequencyType.Add(new ListItem { Text = "Yearly", Value = "4" });
                ViewBag.FrequencyTypeListViewBag = new SelectList(_FrequencyType, "Value", "Text");
            }
            if (PurchaseRequestID > 0 && TypeID == 1)
            {
                //    ViewBag.PurchaseRequestId = PurchaseRequestID;
                //    CoreFactory.inventoryBasicDetailEntity = BALFactory.NewInventoryBAL.GetInventoryItemCategoryDetail((Int32)PurchaseRequestID);
                //    var jsonInventoryItemMasterDTO = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(CoreFactory.inventoryBasicDetailEntity);
                //    ViewBag.InventoryRequestViewBag = jsonInventoryItemMasterDTO;
            }
            if (InventoryBasicDetailsID > 0)
            {
                ViewBag.InventoryBasicDetailsID = InventoryBasicDetailsID;
            }
            return View();
        }
        [HttpGet]
        public ActionResult _BasicDetails()
        {
            return View();
        }
        // Date: 24/02/2021
        [HttpPost]
        public ActionResult InventoryBasicDetails(InventoryBasicDetailModel model)
        {
            if (model.ID > 0)
            {
                //   model.UpdatedBy = Convert.ToInt32(System.Web.HttpContext.Current.Session["LoggedInEmployeeId"]);
                int pid = (int)model.PurchaseRequestID;
                CoreFactory.inventoryBasicDetailEntity = new InventoryBasicDetailEntity();
                CoreFactory.inventoryBasicDetailEntity.ID = model.ID; // InventoryBasicDetails- >ID 
                CoreFactory.inventoryBasicDetailEntity.TypeID = model.TypeID;
                CoreFactory.inventoryBasicDetailEntity.ItemID = model.ItemID;
                CoreFactory.inventoryBasicDetailEntity.CategoryID = model.CategoryID;
                CoreFactory.inventoryBasicDetailEntity.PurchaseRequestID = model.PurchaseRequestID;
                CoreFactory.inventoryBasicDetailEntity.UnitID = model.UnitID;
                CoreFactory.inventoryBasicDetailEntity.PackSize = model.PackSize;
                CoreFactory.inventoryBasicDetailEntity.QuantityType = model.QuantityType;
                CoreFactory.inventoryBasicDetailEntity.Quantity = model.Quantity;
                CoreFactory.inventoryBasicDetailEntity.QuantityLeft = model.QuantityLeft;
                CoreFactory.inventoryBasicDetailEntity.Brand = model.Brand;
                CoreFactory.inventoryBasicDetailEntity.BrandReceived = model.BrandReceived;
                CoreFactory.inventoryBasicDetailEntity.Grade = model.Grade;
                CoreFactory.inventoryBasicDetailEntity.GradeReceived = model.GradeReceived;
                CoreFactory.inventoryBasicDetailEntity.ConditionOfPackaging = model.ConditionOfPackaging;
                CoreFactory.inventoryBasicDetailEntity.IntegrityOfPackaging = model.IntegrityOfPackaging;
                CoreFactory.inventoryBasicDetailEntity.Warranty = model.Warranty;
                CoreFactory.inventoryBasicDetailEntity.Manufacturer = model.Manufacturer;
                CoreFactory.inventoryBasicDetailEntity.DOM = model.DOM;
                CoreFactory.inventoryBasicDetailEntity.CertifiedConcentration = model.CertifiedConcentration;
                CoreFactory.inventoryBasicDetailEntity.Praceability = model.Praceability;
                CoreFactory.inventoryBasicDetailEntity.DOE = model.DOE;
                CoreFactory.inventoryBasicDetailEntity.IsActive = true;
                CoreFactory.inventoryBasicDetailEntity.Remark = model.Remark;
                CoreFactory.inventoryBasicDetailEntity.StorageLocation = model.StorageLocation; // new field
                CoreFactory.inventoryBasicDetailEntity.InventoryBasicDetailsNumber = model.InventoryBasicDetailsNumber;
                CoreFactory.inventoryBasicDetailEntity.UpdatedBy = 0;
                CoreFactory.inventoryBasicDetailEntity.UpdatedDate = DateTime.Now.ToLocalTime();
                CoreFactory.inventoryBasicDetailEntity.TotalQuantity = model.Quantity;
                CoreFactory.inventoryBasicDetailEntity.DeliveryTime = null;
                // long BasicDetailsID = BALFactory.NewInventoryBAL.UpdateInventoryBasicData(CoreFactory.inventoryBasicDetailEntity);
                long BasicDetailsID = BALFactory.NewInventoryBAL.UpdateInventoryBasicData(CoreFactory.inventoryBasicDetailEntity);
                //  model.ID = (Int16)BasicDetailsID;
                int InvID = (int)model.ID;
                bool isactive = true;
                var result = BALFactory.NewInventoryBAL.GetInvBasicNumber(InvID, isactive);
                string InvBasicNumber1 = result.InventoryBasicDetailsNumber.ToString();
                if (InvBasicNumber1 != null)
                {
                    if (model.ListInventoryBasicItemDetail != null)
                    {
                        string InventoryBasicItemDetailsNumber1 = null;
                        int _counter = 1;
                        foreach (InventoryBasicDetailModel item in model.ListInventoryBasicItemDetail)
                        {
                            if (item.ID > 0)
                            {
                                _counter++;
                                if (item.IsDeleted == true)
                                {
                                    CoreFactory.inventoryBasicDetailEntity = new InventoryBasicDetailEntity();
                                    CoreFactory.inventoryBasicDetailEntity.ID = item.ID;
                                    CoreFactory.inventoryBasicDetailEntity.InventoryBasicDetailsID = item.InventoryBasicDetailsID;
                                    //   item.UpdatedBy = Convert.ToInt32(System.Web.HttpContext.Current.Session["LoggedInEmployeeId"]);
                                    string status = BALFactory.NewInventoryBAL.DeleteInventoryBasicItemDetail(CoreFactory.inventoryBasicDetailEntity);
                                    if (status == "success")
                                    {
                                        if (_counter < 10)
                                        {
                                            InventoryBasicItemDetailsNumber1 = InvBasicNumber1 + "/0" + _counter.ToString();
                                        }
                                        else
                                        {
                                            InventoryBasicItemDetailsNumber1 = InvBasicNumber1 + "/" + _counter.ToString();
                                        }
                                        CoreFactory.inventoryBasicDetailEntity.InventoryBasicDetailsID = model.ID;
                                        CoreFactory.inventoryBasicDetailEntity.BatchNumber = item.BatchNumber;
                                        CoreFactory.inventoryBasicDetailEntity.BarcodeNumber = item.BarcodeNumber;
                                        CoreFactory.inventoryBasicDetailEntity.ModelNumber = item.ModelNumber;
                                        CoreFactory.inventoryBasicDetailEntity.Quantity = item.Quantity;
                                        CoreFactory.inventoryBasicDetailEntity.Description = item.Description;
                                        CoreFactory.inventoryBasicDetailEntity.IsActive = true;
                                        CoreFactory.inventoryBasicDetailEntity.InventoryBasicItemDetailsNumber = (string)InventoryBasicItemDetailsNumber1;
                                        CoreFactory.inventoryBasicDetailEntity.Manufacturer = model.Manufacturer;
                                        CoreFactory.inventoryBasicDetailEntity.DOM = model.DOM;
                                        CoreFactory.inventoryBasicDetailEntity.DOE = model.DOE;
                                        CoreFactory.inventoryBasicDetailEntity.LOTNo = item.LOTNo;
                                        CoreFactory.inventoryBasicDetailEntity.SRNO = item.SRNO;
                                        var BasicItemDetailsID = BALFactory.NewInventoryBAL.AddInventoryBasicItemDetails(CoreFactory.inventoryBasicDetailEntity);
                                        model.InventoryBasicItemDetailsID = (Int32)BasicItemDetailsID;
                                        if (model.InventoryBasicItemDetailsID > 0)
                                        {
                                            _counter++;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (_counter < 10)
                                {
                                    InventoryBasicItemDetailsNumber1 = InvBasicNumber1 + "/0" + _counter.ToString();
                                }
                                else
                                {
                                    InventoryBasicItemDetailsNumber1 = InvBasicNumber1 + "/" + _counter.ToString();
                                }
                                CoreFactory.inventoryBasicDetailEntity.InventoryBasicDetailsID = model.ID;
                                CoreFactory.inventoryBasicDetailEntity.BatchNumber = item.BatchNumber;
                                CoreFactory.inventoryBasicDetailEntity.BarcodeNumber = item.BarcodeNumber;
                                CoreFactory.inventoryBasicDetailEntity.ModelNumber = item.ModelNumber;
                                CoreFactory.inventoryBasicDetailEntity.Quantity = item.Quantity;
                                CoreFactory.inventoryBasicDetailEntity.Description = item.Description;
                                CoreFactory.inventoryBasicDetailEntity.IsActive = true;
                                CoreFactory.inventoryBasicDetailEntity.InventoryBasicItemDetailsNumber = (string)InventoryBasicItemDetailsNumber1;
                                CoreFactory.inventoryBasicDetailEntity.Manufacturer = model.Manufacturer;
                                CoreFactory.inventoryBasicDetailEntity.DOM = model.DOM;
                                CoreFactory.inventoryBasicDetailEntity.DOE = model.DOE;
                                CoreFactory.inventoryBasicDetailEntity.LOTNo = item.LOTNo;
                                CoreFactory.inventoryBasicDetailEntity.SRNO = item.SRNO;
                                var BasicItemDetailsID = BALFactory.NewInventoryBAL.AddInventoryBasicItemDetails(CoreFactory.inventoryBasicDetailEntity);
                                model.InventoryBasicItemDetailsID = (Int32)BasicItemDetailsID;
                                if (model.InventoryBasicItemDetailsID > 0)
                                {
                                    _counter++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Json(new { status = "success", message = "Record created successfully.", ID = model.ID });

            }
            else // insert
            {
                if (model.ID == 0)
                {
                    int pid = (int)model.PurchaseRequestID;
                    string InventoryBasicDetailsNumber1 = BALFactory.NewInventoryBAL.GenerateInvBasicNumber(pid);
                    CoreFactory.inventoryBasicDetailEntity = new InventoryBasicDetailEntity();
                    CoreFactory.inventoryBasicDetailEntity.ID = model.ID;
                    CoreFactory.inventoryBasicDetailEntity.TypeID = model.TypeID;
                    CoreFactory.inventoryBasicDetailEntity.ItemID = model.ItemID;
                    CoreFactory.inventoryBasicDetailEntity.CategoryID = model.CategoryID;
                    CoreFactory.inventoryBasicDetailEntity.UnitID = model.UnitID;
                    CoreFactory.inventoryBasicDetailEntity.QuantityType = model.QuantityType;
                    CoreFactory.inventoryBasicDetailEntity.Quantity = model.Quantity;
                    CoreFactory.inventoryBasicDetailEntity.Warranty = model.Warranty;
                    CoreFactory.inventoryBasicDetailEntity.TotalQuantity = model.Quantity;
                    CoreFactory.inventoryBasicDetailEntity.InsertedBy = 0;
                    CoreFactory.inventoryBasicDetailEntity.InsertedDate = DateTime.Now.ToLocalTime();
                    CoreFactory.inventoryBasicDetailEntity.InventoryBasicDetailsNumber = InventoryBasicDetailsNumber1;
                    CoreFactory.inventoryBasicDetailEntity.PurchaseRequestID = model.PurchaseRequestID;
                    CoreFactory.inventoryBasicDetailEntity.PackSize = model.PackSize;
                    if (model.QuantityLeft == 0)
                    {
                        CoreFactory.inventoryBasicDetailEntity.QuantityLeft = 0;
                    }
                    else
                    {
                        CoreFactory.inventoryBasicDetailEntity.QuantityLeft = model.QuantityLeft;
                    }
                    CoreFactory.inventoryBasicDetailEntity.Brand = model.Brand;
                    CoreFactory.inventoryBasicDetailEntity.BrandReceived = model.BrandReceived;
                    CoreFactory.inventoryBasicDetailEntity.Grade = model.Grade;
                    CoreFactory.inventoryBasicDetailEntity.GradeReceived = model.GradeReceived;
                    CoreFactory.inventoryBasicDetailEntity.ConditionOfPackaging = model.ConditionOfPackaging;
                    CoreFactory.inventoryBasicDetailEntity.IntegrityOfPackaging = model.IntegrityOfPackaging;
                    CoreFactory.inventoryBasicDetailEntity.Manufacturer = model.Manufacturer;
                    CoreFactory.inventoryBasicDetailEntity.DOM = model.DOM;
                    CoreFactory.inventoryBasicDetailEntity.CertifiedConcentration = model.CertifiedConcentration;
                    CoreFactory.inventoryBasicDetailEntity.Praceability = model.Praceability;
                    CoreFactory.inventoryBasicDetailEntity.DOE = model.DOE;
                    CoreFactory.inventoryBasicDetailEntity.IsActive = true;
                    CoreFactory.inventoryBasicDetailEntity.Remark = model.Remark;
                    CoreFactory.inventoryBasicDetailEntity.StorageLocation = model.StorageLocation; // new field
                    long BasicDetailsID = BALFactory.NewInventoryBAL.InsertInventoryBasicData(CoreFactory.inventoryBasicDetailEntity);
                    model.ID = (Int16)BasicDetailsID;
                    int InvID = (int)model.ID;
                    bool isactive = true;
                    var result = BALFactory.NewInventoryBAL.GetInvBasicNumber(InvID, isactive);
                    string InvBasicNumber1 = result.InventoryBasicDetailsNumber.ToString();
                    //   var BasicDetailsList = (List<InventoryBasicDetailEntity>)TempData.Peek("BasicDetailsList");
                    if (InvBasicNumber1 != null)
                    {
                        if (model.ListInventoryBasicItemDetail != null)
                        {
                            string InventoryBasicItemDetailsNumber1 = null;
                            int _counter = 1;
                            foreach (InventoryBasicDetailModel item in model.ListInventoryBasicItemDetail)
                            {
                                if (_counter < 10)
                                {
                                    InventoryBasicItemDetailsNumber1 = InvBasicNumber1 + "/0" + _counter.ToString();
                                }
                                else
                                {
                                    InventoryBasicItemDetailsNumber1 = InvBasicNumber1 + "/" + _counter.ToString();
                                }
                                CoreFactory.inventoryBasicDetailEntity.InventoryBasicDetailsID = model.ID;
                                CoreFactory.inventoryBasicDetailEntity.BatchNumber = item.BatchNumber;
                                CoreFactory.inventoryBasicDetailEntity.BarcodeNumber = item.BarcodeNumber;
                                CoreFactory.inventoryBasicDetailEntity.ModelNumber = item.ModelNumber;
                                CoreFactory.inventoryBasicDetailEntity.Quantity = item.Quantity;
                                CoreFactory.inventoryBasicDetailEntity.Description = item.Description;
                                CoreFactory.inventoryBasicDetailEntity.IsActive = true;
                                CoreFactory.inventoryBasicDetailEntity.InventoryBasicItemDetailsNumber = (string)InventoryBasicItemDetailsNumber1;
                                CoreFactory.inventoryBasicDetailEntity.Manufacturer = model.Manufacturer;
                                CoreFactory.inventoryBasicDetailEntity.DOM = model.DOM;
                                CoreFactory.inventoryBasicDetailEntity.DOE = model.DOExpiry;
                                CoreFactory.inventoryBasicDetailEntity.LOTNo = item.LOTNo;
                                CoreFactory.inventoryBasicDetailEntity.SRNO = item.SRNO;
                                var BasicItemDetailsID = BALFactory.NewInventoryBAL.AddInventoryBasicItemDetails(CoreFactory.inventoryBasicDetailEntity);
                                model.InventoryBasicItemDetailsID = (Int32)BasicItemDetailsID;
                                if (model.InventoryBasicItemDetailsID > 0)
                                {
                                    _counter++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        int ItemId = (Int32)model.ItemID;
                        decimal AvailableStock = BALFactory.NewInventoryBAL.GetInvAvailableStockbyItemId(ItemId); // here get stock from InventoryItemMaster ->Table
                        decimal ReceivedQuantity = (decimal)model.Quantity;
                        decimal UpdatedAvailableStock = AvailableStock + ReceivedQuantity;
                        string success = BALFactory.NewInventoryBAL.UpdateInvAvailableStock(ItemId, UpdatedAvailableStock);
                    }
                }
                ViewBag.BasicDetailsIdViewBag = model.ID;

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Json(new { status = "success", message = "Record created successfully.", ID = model.ID });
            }
            return View();
        }
        [HttpGet]
        public ActionResult _CommercialDetails()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult InventoryCommercialDetails(InventoryCommercialDetailModel model)
        {

            if (model != null)
            {
                if (model.ID > 0)
                {  // Update Here
                   // UPDATE HERE
                   //  model.UpdatedBy = Convert.ToInt32(System.Web.HttpContext.Current.Session["LoggedInEmployeeId"]);
                    CoreFactory.inventoryCommercialDetailEntity = new InventoryCommercialDetailEntity();
                    CoreFactory.inventoryCommercialDetailEntity.VendorName = model.VendorName;
                    CoreFactory.inventoryCommercialDetailEntity.InventoryBasicDetailsID = model.InventoryBasicDetailsID;
                    CoreFactory.inventoryCommercialDetailEntity.PurchaseOrderNumber = model.PurchaseOrderNumber;
                    // CoreFactory.inventoryCommercialDetailEntity.PurchaseOrderDate = model.PurchaseOrderDate;
                    CoreFactory.inventoryCommercialDetailEntity.PurchaseOrderValue = model.PurchaseOrderValue;
                    CoreFactory.inventoryCommercialDetailEntity.PurchaseDate = model.PurchaseDate;
                    CoreFactory.inventoryCommercialDetailEntity.DeliveryTime = model.DeliveryTime;
                    CoreFactory.inventoryCommercialDetailEntity.DeliveryChallanNo = model.DeliveryChallanNo;
                    CoreFactory.inventoryCommercialDetailEntity.DeliveryChallanDate = model.DeliveryChallanDate;
                    CoreFactory.inventoryCommercialDetailEntity.InvoiceNumber = model.InvoiceNumber;
                    CoreFactory.inventoryCommercialDetailEntity.BillDate = model.BillDate;
                    CoreFactory.inventoryCommercialDetailEntity.IsActive = true;
                    CoreFactory.inventoryCommercialDetailEntity.UpdatedBy = 0;
                    CoreFactory.inventoryCommercialDetailEntity.ID = model.ID;
                    CoreFactory.inventoryCommercialDetailEntity.UpdatedDate = DateTime.Now.ToLocalTime();
                    long InvCommId = BALFactory.NewInventoryBAL.UpdateInventoryCommercialData(CoreFactory.inventoryCommercialDetailEntity);
                    ViewBag.CommercialDetailsIdViewBag = InvCommId;
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Json(new { status = "success", message = "Record created successfully.", ID = model.ID });

                }
                else
                {
                    if (model.ID == 0)    // insert code here
                    {
                        CoreFactory.inventoryCommercialDetailEntity = new InventoryCommercialDetailEntity();
                        CoreFactory.inventoryCommercialDetailEntity.VendorName = model.VendorName;
                        CoreFactory.inventoryCommercialDetailEntity.InventoryBasicDetailsID = model.InventoryBasicDetailsID;
                        CoreFactory.inventoryCommercialDetailEntity.PurchaseOrderNumber = model.PurchaseOrderNumber;
                        //CoreFactory.inventoryCommercialDetailEntity.PurchaseOrderDate = model.PurchaseOrderDate;
                        CoreFactory.inventoryCommercialDetailEntity.PurchaseOrderValue = model.PurchaseOrderValue;
                        CoreFactory.inventoryCommercialDetailEntity.PurchaseDate = model.PurchaseDate;
                        CoreFactory.inventoryCommercialDetailEntity.DeliveryTime = model.DeliveryTime;
                        CoreFactory.inventoryCommercialDetailEntity.DeliveryChallanNo = model.DeliveryChallanNo;
                        CoreFactory.inventoryCommercialDetailEntity.DeliveryChallanDate = model.DeliveryChallanDate;
                        CoreFactory.inventoryCommercialDetailEntity.InvoiceNumber = model.InvoiceNumber;
                        CoreFactory.inventoryCommercialDetailEntity.BillDate = model.BillDate;
                        CoreFactory.inventoryCommercialDetailEntity.IsActive = true;
                        CoreFactory.inventoryCommercialDetailEntity.InsertedBy = 0;
                        CoreFactory.inventoryCommercialDetailEntity.InsertedDate = DateTime.Now.ToLocalTime();
                        long InvCommId = BALFactory.NewInventoryBAL.InsertInventoryCommercialData(CoreFactory.inventoryCommercialDetailEntity);
                        if (InvCommId > 0)
                        {

                            ViewBag.CommercialDetailsIdViewBag = InvCommId;
                            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                            return Json(new { status = "success", message = "Record created successfully.", ID = model.ID });
                        }
                        else
                        {
                            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                            return Json(new { status = "failed", message = "Failed", ID = model.ID });
                        }
                    }
                }
            }
            return PartialView();
        }

        [HttpGet]
        public ActionResult _CalibrationDetails()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult _MaintainanceDetails()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InventoryMaintainanceAndCalibrationDetails(InventoryMaintainanceAndCalibrationModel model)
        {



            //   model.InsertedBy = Convert.ToInt32(System.Web.HttpContext.Current.Session["LoggedInEmployeeId"]);
            CoreFactory.inventoryMaintainanceAndCalibrationEntity = new InventoryMaintainanceAndCalibrationEntity();
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.AMCVendorName = model.AMCVendorName;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.AMCNumber = model.AMCNumber;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.AMCDate = model.AMCDate;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.AMCValue = model.AMCValue;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.AMCPeriod = model.AMCPeriod;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.Frequency = model.Frequency;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.Type = "Maintainance";
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.InsertedBy = 0;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.StartDate = model.AMCStartDate;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicItemDetailsID = model.InventoryBasicItemDetailsID;
            long MID = BALFactory.NewInventoryBAL.InsertInventoryMaintainanceDetail(CoreFactory.inventoryMaintainanceAndCalibrationEntity);

            if (MID > 0)
            {
                // Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                //  return Json(new { status = "success", message = "Record created successfully.", ID = MID });
                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                //return Json(new { status = "success", message = "Failed!!!!", ID = MID });
                return Json(new { status = "fail" }, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult InventoryCalibrationDetails(InventoryMaintainanceAndCalibrationModel model)
        {

            //   model.InsertedBy = Convert.ToInt32(System.Web.HttpContext.Current.Session["LoggedInEmployeeId"]);
            CoreFactory.inventoryMaintainanceAndCalibrationEntity = new InventoryMaintainanceAndCalibrationEntity();
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.AMCVendorName = model.CalibrationAMCVendorName;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.AMCNumber = model.CalibrationAMCNumber;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.AMCDate = model.CalibrationAMCDate;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.AMCValue = model.CalibrationAMCValue;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.AMCPeriod = model.CalibrationAMCPeriod;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.Frequency = model.CalibrationFrequency;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.Type = "Calibration";
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.InsertedBy = 0;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.StartDate = model.CalibrationAMCStartDate;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicItemDetailsID = model.CalibrationInventoryBasicItemDetailsID;
            long MID = BALFactory.NewInventoryBAL.InsertInventoryMaintainanceDetail(CoreFactory.inventoryMaintainanceAndCalibrationEntity);

            if (MID > 0)
            {
                // Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                //  return Json(new { status = "success", message = "Record created successfully.", ID = MID });
                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                //return Json(new { status = "success", message = "Failed!!!!", ID = MID });
                return Json(new { status = "fail" }, JsonRequestBehavior.AllowGet);

            }
        }


        [HttpPost]
        public ActionResult FatchCommercialDetailsByID(int ID)
        {
            int ID1 = ID;
            string Mode = "SELECT RECORD BY ID";
            string SortBy = "";
            string Search = "";
            string SortOrder = "";
            int StartRow = 0;
            int EndRow = 0;
            int TypeID = 0;
            CoreFactory.InventoryBasicDetailsList = BALFactory.NewInventoryBAL.GetInventoryBasicDetailBySearch(ID1, Mode, SortBy, Search, SortOrder, StartRow, EndRow, TypeID);
            if (CoreFactory.InventoryBasicDetailsList != null)
            {
                if (CoreFactory.InventoryBasicDetailsList.Count > 0)
                {
                    int ID12 = 0;
                    long InventoryBasicDetailsID = ID;
                    bool InventoryBasicDetailsIsActive = true;
                    bool InventoryBasicItemDetailsIsActive = true;
                    string Mode1 = "SELECT RECORD BY COMMERCIAL DETAILS, BASIC DETAILS AND BASIC ITEMS DETAILS ID FIELD";
                    int TypeID1 = 0;
                    int CategoryID = 0;
                    int ItemID = 0;
                    CoreFactory.InventoryBasicItemDetailList = BALFactory.NewInventoryBAL.GetInventoryBasicItemDetailBySearch1(ID12, InventoryBasicDetailsID, InventoryBasicDetailsIsActive, InventoryBasicItemDetailsIsActive, Mode1, ItemID, TypeID1, CategoryID);
                    if (CoreFactory.InventoryBasicItemDetailList.Count == 0)
                    {
                    }
                    int InventoryBasicDetailsID1 = ID;
                    string Mode2 = "SELECT RECORD BY ID";
                    CoreFactory.InventoryCommercialList = BALFactory.NewInventoryBAL.GetInventoryCommercialDetailsBySearch(InventoryBasicDetailsID1, Mode2);
                    if (CoreFactory.InventoryCommercialList.Count == 0)
                    {
                    }
                    string Mode3 = "SELECT RECORD BY InventoryBasicDetailID";
                    long InventoryBasicDetailID2 = ID;
                    CoreFactory.InventoryCommercialFileDetailList = BALFactory.NewInventoryBAL.GetInventoryCommercialFileDetailBySearch(InventoryBasicDetailID2, Mode3);
                    if (CoreFactory.InventoryCommercialList.Count == 0)
                    {
                    }
                    string _commercialImagePhysicalPath = WebConfigurationManager.AppSettings["CommercialDetailsFilePath"];
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Json(new { status = "success", message = "data fatch successfully.", CommercialDetails = CoreFactory.InventoryCommercialList, InventoryBasicDetail = CoreFactory.InventoryBasicDetailsList, InventoryBasicItemDetail = CoreFactory.InventoryBasicItemDetailList, InventoryCommercialFileDetail = CoreFactory.InventoryCommercialFileDetailList, ImagePath = _commercialImagePhysicalPath });
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult FatchBasicItemsDetails(int ID)
        {
            int ID2 = ID;
            ViewBag.InventoryItemListViewBag = BALFactory.NewInventoryBAL.GetInventoryBasicItemDetails(ID2);

            if (ViewBag.InventoryItemListViewBag != null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Json(new { status = "success", message = "Customer details added successfully.", BasicItemsDetails = ViewBag.InventoryItemListViewBag });
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Json(new { status = "info", message = "No record found." });
            }
        }
        public List<InventoryBasicDetailEntity> GetBasicDetails(int TotalRecords, int TypeID)
        {
            List<InventoryBasicDetailEntity> InvBasicDetailsList = new List<InventoryBasicDetailEntity>();
            int ID1 = 0;
            int TypeID1 = TypeID;
            string SortBy1 = _sortBy;
            string SortOrder1 = _sortOrder;
            int StartRow1 = _startRow + 1;
            int EndRow1 = _startRow + (_rowLength * 3);
            string SearchKeywords1 = _searchKeywords;
            string Mode1 = "SELECT RECORD BY INVENTORY TYPE";
            CoreFactory.InventoryBasicDetailsList = BALFactory.NewInventoryBAL.GetInventoryBasicDetailBySearch2(ID1, TypeID1, SortBy1, SortOrder1, StartRow1, EndRow1, SearchKeywords1, Mode1);
            InvBasicDetailsList = CoreFactory.InventoryBasicDetailsList;
            TotalRecords = CoreFactory.InventoryBasicDetailsList.Count;
            return InvBasicDetailsList;
        }
        [HttpPost]
        public ActionResult GetMaintainanceAndCalibrationScheduleByBasicItemDetailsID(string Type, int InventoryBasicItemDetailsID = 0)
        {
            string Mode1 = "SELECT RECORD BY InventoryBasicDetailsID";
            string Type1 = Type;
            int InventoryBasicItemDetailsID1 = InventoryBasicItemDetailsID;
            CoreFactory.inventoryMaintainanceAndCalibrationScheduleList = BALFactory.NewInventoryBAL.GetMaintainanceAndCalibrationScheduleByBasicItemDetailsID(InventoryBasicItemDetailsID1, Type1, Mode1);
            if (CoreFactory.inventoryMaintainanceAndCalibrationScheduleList.Count != 0)
            {
                if (CoreFactory.inventoryMaintainanceAndCalibrationScheduleList.Count > 0 && CoreFactory.inventoryMaintainanceAndCalibrationScheduleList[0].ID > 0)
                {
                    Session["ID"] = CoreFactory.inventoryMaintainanceAndCalibrationScheduleList[0].ID;
                    Session["AMCNumber"] = CoreFactory.inventoryMaintainanceAndCalibrationScheduleList[0].AMCNumber;
                    Session["AMCDate"] = CoreFactory.inventoryMaintainanceAndCalibrationScheduleList[0].AMCDate;
                    Session["AMCPeriod"] = CoreFactory.inventoryMaintainanceAndCalibrationScheduleList[0].AMCPeriod;
                    Session["frequency"] = CoreFactory.inventoryMaintainanceAndCalibrationScheduleList[0].Frequency;
                    Session["AMCStartDate"] = CoreFactory.inventoryMaintainanceAndCalibrationScheduleList[0].AMCStartDate;
                    Session["AMCVendorName"] = CoreFactory.inventoryMaintainanceAndCalibrationScheduleList[0].AMCVendorName;
                    Session["Type"] = CoreFactory.inventoryMaintainanceAndCalibrationScheduleList[0].Type;
                    Session["IsActive"] = CoreFactory.inventoryMaintainanceAndCalibrationScheduleList[0].IsActive;
                    Session["InventoryBasicItemDetailsID"] = CoreFactory.inventoryMaintainanceAndCalibrationScheduleList[0].InventoryBasicItemDetailsID;

                    // here get the data from ScheduleDate table

                    int ScheduleDateID = CoreFactory.inventoryMaintainanceAndCalibrationScheduleList[0].ID;

                    CoreFactory.InventoryMaintainanceAndCalibrationScheduleDatesList = BALFactory.NewInventoryBAL.GetInventoryMaintanceScheduleDate(ScheduleDateID);

                    Session["ScheduleDateList"] = CoreFactory.InventoryMaintainanceAndCalibrationScheduleDatesList;

                    string _mainTenanceImagePhysicalPath = WebConfigurationManager.AppSettings["MaintainanceAndCalibrationFilePath"];
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Json(new { status = "success", message = "Customer details added successfully.", InventoryMaintainanceAndCalibrationScheduleDetails = CoreFactory.inventoryMaintainanceAndCalibrationScheduleList, ImagePath = _mainTenanceImagePhysicalPath });
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Json(new { status = "info", message = "No record found." });
                }
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Json(new { status = "info", message = "No record found." });
            }
            return View();
        }

        [HttpGet]
        public ActionResult SetCookies(string previousURL)
        {
            //ViewData["previousURL"] = "Welcome to ASP.NET MVC!";
            //string cookie = string.Empty;
            this.ControllerContext.HttpContext.Response.Cookies["previousURL"].Value = previousURL;
            return RedirectToAction("Create", "InventoryItemMaster");
            //return View("Create", "InventoryItemMaster");
            //ViewData["Cookie"] = previousURL;
            //return Json(true);
        }
        public ActionResult AjaxHandler(JQueryDataTableParamModel param, int ID = 0)
        {
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            string sortDirection = Convert.ToString(Request["sSortDir_0"]); // asc or desc
            int TotalRecords = 0;
            //IEnumerable<InventoryBasicDetail> _inventoryBasicDetail;
            IEnumerable<InventoryBasicDetailEntity> _inventoryBasicDetail;
            switch (Convert.ToInt32(sortColumnIndex))
            {
                case 0:
                    _sortBy = "TypeID";
                    break;
                case 1:
                    _sortBy = "CategoryID";
                    break;

            }
            _sortOrder = sortDirection;
            _rowLength = param.iDisplayLength;
            _startRow = param.iDisplayStart;
            _searchKeywords = param.sSearch;
            _inventoryBasicDetail = GetBasicDetails(TotalRecords, ID);
            var displayedPosts = _inventoryBasicDetail.Skip(0).Take(param.iDisplayLength);
            var result = from c in displayedPosts
                         select new[]
                {
                    Convert.ToString(c.RowNumber),
                    c.InventoryBasicDetailsNumber,
                    c.ItemName,
                    c.CategoryName,
                    Convert.ToString(c.TotalQuantity),
                    c.Unit,
                    Convert.ToString(c.ID),
                    Convert.ToString(c.ItemID),
                     Convert.ToString(c.PurchaseRequestID)
                };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = TotalRecords,
                iTotalDisplayRecords = TotalRecords,
                aaData = result
            },
                        JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> UploadCommercialFile(string id)
        {
            string _image = string.Empty;
            string _path = string.Empty;
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        int status = 0;
                        Guid _Guid = Guid.NewGuid();
                        string UniqueDigits = _Guid.ToString();
                        string[] split = UniqueDigits.Split('-');
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        var fileName = Path.GetFileName(file);
                        var stringFileName = id + "_Commercial_" + split[0] + "_" + fileName;
                        var fileExtension = Path.GetExtension(file);

                        string _commercialDetailsImagePhysicalPath = WebConfigurationManager.AppSettings["CommercialDetailsFilePhysicalPath"];
                        string _isFullFilePath = WebConfigurationManager.AppSettings["IsCommercialDetailsFullFilePath"];
                        string path = string.Empty;
                        if (_isFullFilePath == "1")
                        {
                            string _rootPath = Server.MapPath("~/");
                            path = Path.Combine(_rootPath + _commercialDetailsImagePhysicalPath, stringFileName);
                            _image = stringFileName;
                            _path = path;
                        }
                        else
                        {
                            //string _rootPath = Server.MapPath("~/");
                            path = Path.Combine(_commercialDetailsImagePhysicalPath, stringFileName);
                        }



                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);

                            InventoryCommercialFileDetailModel _inventoryCommercialFileDetail = new InventoryCommercialFileDetailModel();
                            int InventoryBasicDetailID = Convert.ToInt32(id);
                            string FileName = stringFileName;
                            string Strstatus = BALFactory.NewInventoryBAL.InsertInventoryCommercialFileDetail(InventoryBasicDetailID, FileName);
                            // IBaseEntityResponse<InventoryCommercialFileDetail> responseInventoryCommercialFileDetail = _inventoryCommercialFileDetailDataProvider.InsertInventoryCommercialFileDetail(_inventoryCommercialFileDetail);
                            if (Strstatus == "success")
                            {

                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { status = "fail", message = ex.Message, imageName = _image, imagePath = _path, });
            }
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { status = "success", message = "File uploaded successfully.", imageName = _image, imagePath = _path });
            //return Json("File uploaded successfully");
        }


        [HttpPost]
        public async Task<JsonResult> UploadMaintainanceAndCalibrationFile(string id, string Type)
        {
            string _fileNameString = string.Empty;
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        int status = 0;
                        Guid _Guid = Guid.NewGuid();
                        string UniqueDigits = _Guid.ToString();
                        string[] split = UniqueDigits.Split('-');
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        var fileName = Path.GetFileName(file);
                        var stringFileName = id + "_" + Type + "_" + split[0] + "_" + fileName;
                        var fileExtension = Path.GetExtension(file);

                        string _maintainanceAndCalibrationDetailsImagePhysicalPath = WebConfigurationManager.AppSettings["MaintainanceAndCalibrationFilePhysicalPath"];
                        string _isFullFilePath = WebConfigurationManager.AppSettings["IsMaintainanceAndCalibrationFullFilePath"];
                        string path = string.Empty;
                        if (_isFullFilePath == "1")
                        {
                            string _rootPath = Server.MapPath("~/");
                            path = Path.Combine(_rootPath + _maintainanceAndCalibrationDetailsImagePhysicalPath, stringFileName);
                        }
                        else
                        {
                            path = Path.Combine(_maintainanceAndCalibrationDetailsImagePhysicalPath, stringFileName);
                        }



                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                            if (_fileNameString == string.Empty)
                            {
                                _fileNameString = stringFileName;
                            }
                            else
                            {
                                _fileNameString = _fileNameString + "," + stringFileName;
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json(_fileNameString);
        }




    }
}