using LIMS_DEMO.Areas.Inventory.Models;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Inventory;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Inventory;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace LIMS_DEMO.Areas.Inventory.Controllers
{
    [RouteArea("Inventory")]
    public class InventoryMaintainanceAndCalibrationController : Controller
    {
        // GET: Inventory/InventoryMaintainanceAndCalibration
        public InventoryMaintainanceAndCalibrationController()
        {
            BALFactory.NewInventoryBAL = new NewInventoryBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MaintainanceList()
        {
            var sActionType = "Maintainance";
            CoreFactory.InventoryMaintenanceList = BALFactory.NewInventoryBAL.InventoryCalibrationAndMaintenanceList(sActionType);
            List<InventoryMaintainanceAndCalibrationModel> MaintenanceList = new List<InventoryMaintainanceAndCalibrationModel>();
            foreach (var Item in CoreFactory.InventoryMaintenanceList)
            {
                MaintenanceList.Add(new InventoryMaintainanceAndCalibrationModel()
                {
                    ScheduleID = Item.ScheduleID,
                    CompletionDateID = Item.CompletionDateID,
                    NextDateID = Item.NextDateID,
                    NextDate = Item.NextDate,
                    CompletionDate = Item.CompletionDate,
                    InventoryBasicItemDetailsID = Item.InventoryBasicItemDetailsID,
                    InventoryBasicItemDetailsNumber = Item.InventoryBasicItemDetailsNumber,
                    InventoryBasicDetailsID = Item.InventoryBasicDetailsID,
                    ItemID = (Int32)Item.ItemID,
                    ItemName = Item.ItemName,
                    CategoryID = Item.CategoryID,
                    CategoryName = Item.CategoryName

                });
            }
            ViewBag.InvCategoryList = BALFactory.dropdownsBAL.GetFilteredInvCatogaryList(2);
            return View(MaintenanceList);
        }
        [HttpPost]
        public ActionResult MaintainanceList(InventoryMaintainanceAndCalibrationModel model)
        {

            string ActionType = "Maintainance";
            string Mode = "SELECT RECORD BY ACTION TYPE";
            int ItemID = 0;
            string InventoryBasicItemDetailsNumber = model.InventoryBasicItemDetailsNumber;
            CoreFactory.imclist = BALFactory.NewInventoryBAL.GetInventoryCalibrationAndMaintenanceUpComingSchedulingDates(ActionType, InventoryBasicItemDetailsNumber, Mode, ItemID);
            List<ListItem> _itemsList = new List<ListItem>();
            bool InventoryTypeMasterIsActive = true;
            bool InventoryTypeMasterIsDeleted = false;
            bool InventoryCategoryMasterIsActive = true;
            bool InventoryCategoryMasterIsDeleted = false;
            bool IsActive = true;
            bool IsDeleted = false;
            int ID = 0;
            int InventoryCategoryMasterID = 0;
            int InventoryTypeMasterID = 2;
            string Mode1 = "SELECT BY ITEM ID, CATEGORY ID AND INVENTORY ID AND ACTIVE AND DELETED PARAMETER";


            CoreFactory.InventoryItemList = BALFactory.NewInventoryBAL.GetInventoryItemMasterBySearch(InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, IsActive, InventoryTypeMasterID, Mode1);

            if (CoreFactory.InventoryItemList  != null)
            {
                if (CoreFactory.InventoryItemList != null && CoreFactory.InventoryItemList.Count > 0)
                {
                    foreach (var item in CoreFactory.InventoryItemList)
                    {
                        _itemsList.Add(new ListItem { Text = item.Name, Value = item.ID.ToString() });

                    }
                }
            }
            ViewBag.InventoryItemListViewBag = new SelectList(_itemsList, "Value", "Text", model.ItemID);
            return View(model);
        }
        [HttpGet]
        public ActionResult CalibrationList()
        {
            // new code
            var sActionType = "Calibration";
            CoreFactory.InventoryMaintenanceList = BALFactory.NewInventoryBAL.InventoryCalibrationAndMaintenanceList(sActionType);
            List<InventoryMaintainanceAndCalibrationModel> CalibrationList = new List<InventoryMaintainanceAndCalibrationModel>();
            foreach (var Item in CoreFactory.InventoryMaintenanceList)
            {
                CalibrationList.Add(new InventoryMaintainanceAndCalibrationModel()
                {
                    ScheduleID = Item.ScheduleID,
                    CompletionDateID = Item.CompletionDateID,
                    NextDateID = Item.NextDateID,
                    NextDate = Item.NextDate,
                    CompletionDate = Item.CompletionDate,
                    InventoryBasicItemDetailsID = Item.InventoryBasicItemDetailsID,
                    InventoryBasicItemDetailsNumber = Item.InventoryBasicItemDetailsNumber,
                    InventoryBasicDetailsID = Item.InventoryBasicDetailsID,
                    ItemID = (Int32)Item.ItemID,
                    ItemName = Item.ItemName,
                    CategoryID = Item.CategoryID,
                    CategoryName = Item.CategoryName

                });
            }
            ViewBag.InvCategoryList = BALFactory.dropdownsBAL.GetFilteredInvCatogaryList(2);
            return View(CalibrationList);
        }
        [HttpPost]
        public ActionResult CalibrationList(InventoryMaintainanceAndCalibrationModel model)
        {
            string ActionType = "Calibration";
            string Mode = "SELECT RECORD BY ACTION TYPE";
            int ItemID = 0;
            string InventoryBasicItemDetailsNumber = model.InventoryBasicItemDetailsNumber;
            CoreFactory.imclist = BALFactory.NewInventoryBAL.GetInventoryCalibrationAndMaintenanceUpComingSchedulingDates(ActionType, InventoryBasicItemDetailsNumber, Mode, ItemID);
            List<ListItem> _itemsList = new List<ListItem>();
            bool InventoryTypeMasterIsActive = true;
            bool InventoryTypeMasterIsDeleted = false;
            bool InventoryCategoryMasterIsActive = true;
            bool InventoryCategoryMasterIsDeleted = false;
            bool IsActive = true;
            bool IsDeleted = false;
            int ID = 0;
            int InventoryCategoryMasterID = 0;
            int InventoryTypeMasterID = 2;
            string Mode1 = "SELECT BY ITEM ID, CATEGORY ID AND INVENTORY ID AND ACTIVE AND DELETED PARAMETER";
            CoreFactory.InventoryItemList = BALFactory.NewInventoryBAL.GetInventoryItemMasterBySearch(InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, IsActive, InventoryTypeMasterID, Mode1);

            if (CoreFactory.InventoryItemList != null)
            {
                if (CoreFactory.InventoryItemList != null && CoreFactory.InventoryItemList.Count > 0)
                {
                    foreach (var item in CoreFactory.InventoryItemList)
                    {
                        _itemsList.Add(new ListItem { Text = item.Name, Value = item.ID.ToString() });
                    }
                }
            }
            ViewBag.InventoryItemListViewBag = new SelectList(_itemsList, "Value", "Text", model.ItemID);
            return View(model);
        }
        [HttpPost]
        public ActionResult CheckMaintainance(string InventoryBasicItemDetailsNumber)
        {
            // new code
            string ActionType = "Maintainance";
            string InventoryBasicItemDetailsNumber1 = InventoryBasicItemDetailsNumber;
            string Mode = "SELECT RECORD BY ACTION TYPE";
            int ItemID = 0;

            CoreFactory.InventoryMaintenanceList = BALFactory.NewInventoryBAL.InventoryCalibrationAndMaintenanceList12(ActionType, InventoryBasicItemDetailsNumber1, Mode, ItemID);

            string ActionType1 = "Calibration";
            string InventoryBasicItemDetailsNumber12 = InventoryBasicItemDetailsNumber;
            string Mode1 = "SELECT RECORD BY ACTION TYPE";
            int ItemID1 = 0;

            //  CoreFactory.InventoryMaintenanceList = BALFactory.NewInventoryBAL.InventoryCalibrationAndMaintenanceList(ActionType1,searchRequest.InventoryBasicItemDetailsNumber,(Int32)searchRequest.ItemID);
            CoreFactory.InventoryMaintenanceList = BALFactory.NewInventoryBAL.InventoryCalibrationAndMaintenanceList12(ActionType1, InventoryBasicItemDetailsNumber12, Mode1, ItemID1);

            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return Json(new { status = "success", message = "Customer details updated successfully.", data = CoreFactory.InventoryMaintenanceList, dataCalibration = CoreFactory.InventoryMaintenanceList }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Maintainance(string InventoryBasicItemDetailsNumber, string ItemName, int ItemID = 0, long InventoryBasicItemDetailsID = 0, long InventoryMaintainanceAndCalibrationScheduleDatesID = 0, long BasicDetailsID = 0, int IMCID = 0)
        {

            List<ListItem> _inventoryItem = new List<ListItem>();
            List<ListItem> _inventorySerialNumbers = new List<ListItem>();
            List<ListItem> _inventoryFrequencyDate = new List<ListItem>();
            List<ListItem> _MaintainanceStatus = new List<ListItem>();
            _MaintainanceStatus.Add(new ListItem { Text = "In Progress", Value = "In Progress" });
            _MaintainanceStatus.Add(new ListItem { Text = "Completed", Value = "Completed" });
            ViewBag.MaintainanceListViewBag = new SelectList(_MaintainanceStatus, "Value", "Text");
            if (ItemID > 0)  // here code dropdown item list
            {
                bool InventoryTypeMasterIsActive = true;
                bool InventoryTypeMasterIsDeleted = false;
                bool InventoryCategoryMasterIsActive = true;
                bool InventoryCategoryMasterIsDeleted = false;
                bool IsActive = true;
                int InventoryTypeMasterID = 2;
                string Mode = "SELECT BY ITEM ID, CATEGORY ID AND INVENTORY ID AND ACTIVE AND DELETED PARAMETER";

                CoreFactory.InventoryItemList = BALFactory.NewInventoryBAL.GetInventoryItemMasterBySearch(InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, IsActive, InventoryTypeMasterID, Mode);
                if (CoreFactory.InventoryItemList != null)
                {
                    ViewBag.ItemIDViewBag = ItemID;
                    ViewBag.BasicItemDetailsIDViewBag = InventoryBasicItemDetailsID;
                    ViewBag.DateIDViewBag = InventoryMaintainanceAndCalibrationScheduleDatesID;
                    if (CoreFactory.InventoryItemList != null && CoreFactory.InventoryItemList.Count > 0)
                    {
                        foreach (var item in CoreFactory.InventoryItemList)
                        {
                            _inventoryItem.Add(new ListItem { Text = item.Name, Value = item.ID.ToString() });
                        }
                    }
                    if (ItemID > 0)
                    {
                        ViewBag.InventoryItemListViewBag = new SelectList(_inventoryItem, "Value", "Text", ItemID);
                    }
                    else
                    {
                        ViewBag.InventoryItemListViewBag = new SelectList(_inventoryItem, "Value", "Text");
                    }
                }
            }
            if (InventoryBasicItemDetailsID > 0) // code for dropdown basicitemlist
            {
                int ID = 0;
                long InventoryBasicDetailsID1 = BasicDetailsID;
                int InventoryCommercialDetailsID = 0;
                int ItemID1 = ItemID;
                bool InventoryBasicDetailsIsActive = true;
                int TypeID = 2;
                int CategoryId = 0;
                bool InventoryBasicItemDetailsIsActive = true;
                string mode = "SELECT RECORD BY InventoryBasicDetailsID";

                CoreFactory.InventoryBasicDetailsList = BALFactory.NewInventoryBAL.GetInventoryBasicItemDetailBySearch(ID, InventoryBasicDetailsID1, InventoryBasicDetailsIsActive, InventoryBasicItemDetailsIsActive, mode, ItemID1, TypeID, CategoryId);
                if (CoreFactory.InventoryBasicDetailsList != null && CoreFactory.InventoryBasicDetailsList.Count > 0)
                {
                    foreach (var item in CoreFactory.InventoryBasicDetailsList)
                    {
                        _inventorySerialNumbers.Add(new ListItem { Text = item.InventoryBasicItemDetailsNumber, Value = item.ID.ToString() });
                    }
                }
                if (InventoryBasicItemDetailsID > 0)
                {
                    ViewBag.InventorySerialNumberListViewBag = new SelectList(_inventorySerialNumbers, "Value", "Text", InventoryBasicItemDetailsID);
                }
                else
                {
                    ViewBag.InventorySerialNumberListViewBag = new SelectList(_inventorySerialNumbers, "Value", "Text");
                }
            }
            if (InventoryMaintainanceAndCalibrationScheduleDatesID > 0) // code for dropdown schedule 
            {

                int ID = 0;
                bool InventoryMaintainanceAndCalibrationScheduleIsActive = true;
                bool IsActive = true;
                string Type1 = "Maintainance";
                string AMCNumber = "";
                int InventoryBasicItemDetailsID1 = (int)InventoryBasicItemDetailsID;
                int InventoryMaintainanceAndCalibrationScheduleID = 0;
                string mode = "SELECT RECORD BY AMC NUMBER, BASIC ITEM DETAILS ID, TYPE, ID, SCHEDULED ID AND IS ACTIVE";
                CoreFactory.imcscheduledatelist = BALFactory.NewInventoryBAL.GetInventoryMaintainanceAndCalibrationScheduleDatesBySearch(ID, InventoryMaintainanceAndCalibrationScheduleIsActive, IsActive, Type1, AMCNumber, InventoryBasicItemDetailsID1, InventoryMaintainanceAndCalibrationScheduleID, mode);

                if (CoreFactory.imcscheduledatelist != null && CoreFactory.imcscheduledatelist.Count > 0)
                {
                    foreach (var item in CoreFactory.imcscheduledatelist)
                    {
                        _inventoryFrequencyDate.Add(new ListItem { Text = item.ScheduleDate.ToString(), Value = item.ID.ToString() });
                    }
                }
                if (InventoryMaintainanceAndCalibrationScheduleDatesID > 0)
                {
                    ViewBag.InventoryFrequencyDateListViewBag = new SelectList(_inventoryFrequencyDate, "Value", "Text", InventoryMaintainanceAndCalibrationScheduleDatesID);

                }
                else
                {
                    ViewBag.InventoryFrequencyDateListViewBag = new SelectList(_inventoryFrequencyDate, "Value", "Text");

                }
            }

            InventoryMaintainanceAndCalibrationModel model = new InventoryMaintainanceAndCalibrationModel();
            List<InventoryMaintainanceAndCalibrationModel> IMCLIST = new List<InventoryMaintainanceAndCalibrationModel>(); // for next date
            if (InventoryMaintainanceAndCalibrationScheduleDatesID > 0)
            {
                long ScheduleDateId = 0;
                ScheduleDateId = InventoryMaintainanceAndCalibrationScheduleDatesID;
                CoreFactory.inventoryMaintainanceAndCalibrationScheduleDatesEntity = BALFactory.NewInventoryBAL.GetScheduleNextDate(ScheduleDateId);
                DateTime date = (DateTime)CoreFactory.inventoryMaintainanceAndCalibrationScheduleDatesEntity.ScheduleDate;
                int id = (int)CoreFactory.inventoryMaintainanceAndCalibrationScheduleDatesEntity.InventoryMaintainanceAndCalibrationScheduleID;
                ScheduleDateId = id;
                CoreFactory.inventoryMaintainanceAndCalibrationScheduleDatesEntity = BALFactory.NewInventoryBAL.GetFrequnecyOfDates(ScheduleDateId, InventoryBasicItemDetailsID);
                int FrequencyOccurences = (int)CoreFactory.inventoryMaintainanceAndCalibrationScheduleDatesEntity.Frequency;
                if (FrequencyOccurences == 1)
                {

                    var nextdate = date.AddMonths(date.Month + 1);
                    var updatedate = nextdate.AddMonths(-(date.Month));
                    model.NextCalibrationDate = updatedate;
                    model.ndate = updatedate.ToString("dd/MM/yyyy").Replace("00:00:00", "");

                }
                else if (FrequencyOccurences == 2)
                {
                    var nextdate = date.AddMonths(date.Month + 3);
                    var updatedate = nextdate.AddMonths(-(date.Month));
                    model.NextCalibrationDate = updatedate;
                    model.ndate = updatedate.ToString("dd/MM/yyyy").Replace("00:00:00", "");

                }
                else if (FrequencyOccurences == 3)
                {
                    var nextdate = date.AddMonths(date.Month + 6);
                    var updatedate = nextdate.AddMonths(-(date.Month));
                    model.NextCalibrationDate = updatedate;
                    model.ndate = updatedate.ToString("dd/MM/yyyy").Replace("00:00:00", "");

                }
                else if (FrequencyOccurences == 4)
                {
                    var nextdate = date.AddMonths(date.Month + 12);
                    var updatedate = nextdate.AddMonths(-(date.Month));
                    model.NextCalibrationDate = updatedate;
                    model.ndate = updatedate.ToString("dd/MM/yyyy").Replace("00:00:00", "");

                }
            }



            // FatchListByBasicItemsDetailsAndItemsID // ofr down list code
            if (ItemID != 0 && BasicDetailsID != 0 && InventoryBasicItemDetailsID != 0)
            {
                long InventoryBasicDetailsID1 = BasicDetailsID;
                long InventoryBasicItemDetailsID1 = InventoryBasicItemDetailsID;
                string Type1 = "Maintainance";
                int ItemID1 = ItemID;
                int ID = 0;
                string Mode = "SELECT RECORD BY ItemID AND InventoryBasicDetailsID";
                CoreFactory.imclist = BALFactory.NewInventoryBAL.GetInventoryMaintainanceAndCalibrationBySearch(ID, InventoryBasicDetailsID1, InventoryBasicItemDetailsID1, ItemID1, Type1, Mode);

                foreach (var item in CoreFactory.imclist)
                {
                    IMCLIST.Add(new InventoryMaintainanceAndCalibrationModel()
                    {
                        InventoryBasicItemDetailsNumber = item.InventoryBasicItemDetailsNumber,
                        ScheduleDate = item.ScheduleDate,
                        AuditDate = item.AuditDate,
                        Auditor = item.Auditor,
                        CompletionStatus = item.CompletionStatus,
                        ID = item.ID,
                        InventoryMaintainanceAndCalibrationScheduleDatesID = item.InventoryMaintainanceAndCalibrationScheduleDatesID,
                        InventoryBasicDetailsID = item.InventoryBasicDetailsID,
                        InventoryBasicItemDetailsID = item.InventoryBasicItemDetailsID,
                        ItemID = item.ItemID,

                    });
                }
            }
            //FatchItemsByID
            if (InventoryMaintainanceAndCalibrationScheduleDatesID != 0) // for updating purpose // geting data from db of inventory maintaince and calibration table
            {
                long ScheduleDateId = InventoryMaintainanceAndCalibrationScheduleDatesID;

                CoreFactory.inventoryMaintainanceAndCalibrationEntity = BALFactory.NewInventoryBAL.GetInvetoryMaintainceCalibrationData(ScheduleDateId, InventoryBasicItemDetailsID);
                if (CoreFactory.inventoryMaintainanceAndCalibrationEntity != null)
                {
                    model.ID = CoreFactory.inventoryMaintainanceAndCalibrationEntity.ID;
                    model.ItemID = CoreFactory.inventoryMaintainanceAndCalibrationEntity.ItemID;
                    model.InventoryBasicDetailsID = CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicDetailsID;
                    model.InventoryBasicItemDetailsID = CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicDetailsID;
                    model.InventoryMaintainanceAndCalibrationScheduleDatesID = CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryMaintainanceAndCalibrationScheduleDatesID;
                    model.CalibrationStartDate = CoreFactory.inventoryMaintainanceAndCalibrationEntity.CalibrationStartDate;
                    model.CalibrationEndDate = CoreFactory.inventoryMaintainanceAndCalibrationEntity.CalibrationEndDate;
                    model.AuditDate = CoreFactory.inventoryMaintainanceAndCalibrationEntity.AuditDate;
                    model.AuditObservations = CoreFactory.inventoryMaintainanceAndCalibrationEntity.AuditObservations;
                    model.Auditor = CoreFactory.inventoryMaintainanceAndCalibrationEntity.Auditor;
                    model.CalibratorName = CoreFactory.inventoryMaintainanceAndCalibrationEntity.CalibratorName;
                    model.ActionTaken = CoreFactory.inventoryMaintainanceAndCalibrationEntity.ActionTaken;
                    model.CompletionStatus = CoreFactory.inventoryMaintainanceAndCalibrationEntity.CompletionStatus;
                }
            }
            // model.ID = 0;
            model.ItemID = ItemID;
            model.InventoryBasicDetailsID = Convert.ToInt32(BasicDetailsID);
            model.InventoryMaintainanceAndCalibrationScheduleDatesID = Convert.ToInt32(InventoryMaintainanceAndCalibrationScheduleDatesID);
            model.InventoryBasicItemDetailsNumber = InventoryBasicItemDetailsNumber;
            model.ItemName = ItemName;
            model.InventoryBasicItemDetailsID = InventoryBasicItemDetailsID;
            model.Type = "Maintainance";
            return View(model);
        }
        [HttpPost]
        public ActionResult Maintainance(InventoryMaintainanceAndCalibrationModel model)
        {
            string MID = "";
            if (model.CompletionStatus == "Completed")
            {
                //if(model.NextCalibrationDate == DateTime.Now)
                //{
                CoreFactory.purchaseEntity = new PurchaseEntity();
                CoreFactory.purchaseEntity.InventoryBasicItemDetailsNumber = model.InventoryBasicItemDetailsNumber;
                CoreFactory.purchaseEntity.ItemName = model.ItemName; //model.ItemName,model.InventoryBasicItemDetailsNumber;//model.NextCalibrationDate;
                CoreFactory.purchaseEntity.NextCalibrationDate = model.NextCalibrationDate;
                //CoreFactory.purchaseEntity.PurchaseRequestID = model.PurchaseRequestID;
                string Msg = " Maintenance/Calibration Details";//Raised &forwarded to TM
                long NotificationDetailId = BALFactory.NewInventoryBAL.AddNotification(Msg, "ADMIN", CoreFactory.purchaseEntity);
                long NotificationDetailId1 = BALFactory.NewInventoryBAL.AddNotification(Msg, "Purchase Incharge", CoreFactory.purchaseEntity);
                //}
            }
            if (model.ID > 0)
            {
                Core.Inventory.InventoryMaintainanceAndCalibrationEntity inventoryMaintainanceAndCalibrationEntity = new InventoryMaintainanceAndCalibrationEntity();
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.ID = model.ID;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.ItemID = (Int32)model.ItemID;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.Auditor = model.Auditor;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.Type = "Maintainance";
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicItemDetailsID = model.InventoryBasicItemDetailsID;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicDetailsID = model.InventoryBasicDetailsID;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryMaintainanceAndCalibrationScheduleDatesID = model.InventoryMaintainanceAndCalibrationScheduleDatesID;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.AuditDate = model.AuditDate;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.AuditObservations = model.AuditObservations;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.ActionTaken = model.ActionTaken;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.UpdatedBy = 0;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.UpdatedDate = DateTime.Now.ToLocalTime();
                if (model.CalibratorName == "" || model.CalibratorName == null)
                {
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.CalibratorName = null;
                }
                else
                {
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.CalibratorName = model.CalibratorName;
                }
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.NextDate = model.NextCalibrationDate;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.StartDate = model.CalibrationStartDate;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.EndDate = model.CalibrationEndDate;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.CompletionStatus = model.CompletionStatus;
                MID = BALFactory.NewInventoryBAL.UpdateInventoryMaintainanceAndCalibration(CoreFactory.inventoryMaintainanceAndCalibrationEntity);
                if (MID != null)
                {
                   // return RedirectToAction("MaintainanceList", "InventoryMaintainanceAndCalibration");
                  // return  RedirectToAction("MaintainanceList", "InventoryMaintainanceAndCalibration", new { Area = "" });
                  //  return Redirect("/InventoryMaintainanceAndCalibration/MaintainanceList"); // redirects to internal url
                    //  Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                     return Json(new { TypeCM = model.Type, status = "success", message = "Maintaince Details updated successfully.", ID = MID });
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Json(new { status = "info", message = "No record found." });
                }
            }
            else
            {
                CoreFactory.inventoryMaintainanceAndCalibrationEntity = new InventoryMaintainanceAndCalibrationEntity();
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.Auditor = model.Auditor;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.ItemID = (Int32)model.ItemID;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.Type = "Maintainance";
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicItemDetailsID = model.InventoryBasicItemDetailsID;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicDetailsID = model.InventoryBasicDetailsID;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryMaintainanceAndCalibrationScheduleDatesID = model.InventoryMaintainanceAndCalibrationScheduleDatesID;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.AuditDate = model.AuditDate;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.AuditObservations = model.AuditObservations;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.ActionTaken = model.ActionTaken;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.InsertedBy = 0;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.InsertedDate = DateTime.Now.ToLocalTime();
                if(model.CalibratorName == "" || model.CalibratorName == null)
                {
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.CalibratorName = null;
                }
                else
                {
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.CalibratorName = model.CalibratorName;
                }

                CoreFactory.inventoryMaintainanceAndCalibrationEntity.NextDate = model.NextCalibrationDate;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.StartDate = model.CalibrationStartDate;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.EndDate = model.CalibrationEndDate;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.CompletionStatus = model.CompletionStatus;
                MID = BALFactory.NewInventoryBAL.InsertInventoryMaintainanceAndCalibration(CoreFactory.inventoryMaintainanceAndCalibrationEntity);
                if (MID == "success")
                {
                          return Json(new { TypeCM = model.Type, status = "success", message = "Maintaince Details Saved successfully.", ID = MID });
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Json(new { status = "info", message = "No record found." });
                }
            }
        }
        [HttpGet]
        public ActionResult Calibration(string InventoryBasicItemDetailsNumber, string ItemName, int ItemID = 0, long InventoryBasicItemDetailsID = 0, long InventoryMaintainanceAndCalibrationScheduleDatesID = 0, long BasicDetailsID = 0, int IMCID = 0)
        {
            // new code
            List<ListItem> _inventoryItem = new List<ListItem>();
            List<ListItem> _inventorySerialNumbers = new List<ListItem>();
            List<ListItem> _inventoryFrequencyDate = new List<ListItem>();
            List<ListItem> _MaintainanceStatus = new List<ListItem>();

            // drop down completion status
            _MaintainanceStatus.Add(new ListItem { Text = "In Progress", Value = "In Progress" });
            _MaintainanceStatus.Add(new ListItem { Text = "Completed", Value = "Completed" });
            ViewBag.MaintainanceListViewBag = new SelectList(_MaintainanceStatus, "Value", "Text");

            // item drop down

            if (ItemID > 0)  // here code dropdown item list
            {
                bool InventoryTypeMasterIsActive = true;
                bool InventoryTypeMasterIsDeleted = false;
                bool InventoryCategoryMasterIsActive = true;
                bool InventoryCategoryMasterIsDeleted = false;
                bool IsActive = true;
                int InventoryTypeMasterID = 2;
                string Mode = "SELECT BY ITEM ID, CATEGORY ID AND INVENTORY ID AND ACTIVE AND DELETED PARAMETER";

                CoreFactory.InventoryItemList = BALFactory.NewInventoryBAL.GetInventoryItemMasterBySearch(InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, IsActive, InventoryTypeMasterID, Mode);
                if (CoreFactory.InventoryItemList != null)
                {
                    ViewBag.ItemIDViewBag = ItemID;
                    ViewBag.BasicItemDetailsIDViewBag = InventoryBasicItemDetailsID;
                    ViewBag.DateIDViewBag = InventoryMaintainanceAndCalibrationScheduleDatesID;
                    if (CoreFactory.InventoryItemList != null && CoreFactory.InventoryItemList.Count > 0)
                    {
                        foreach (var item in CoreFactory.InventoryItemList)
                        {
                            _inventoryItem.Add(new ListItem { Text = item.Name, Value = item.ID.ToString() });
                        }
                    }
                    if (ItemID > 0)
                    {
                        ViewBag.InventoryItemListViewBag = new SelectList(_inventoryItem, "Value", "Text", ItemID);
                    }
                    else
                    {
                        ViewBag.InventoryItemListViewBag = new SelectList(_inventoryItem, "Value", "Text");
                    }
                }
            }

            if (InventoryBasicItemDetailsID > 0) // code for dropdown basicitemlist
            {
                int ID = 0;
                long InventoryBasicDetailsID1 = BasicDetailsID;
                int InventoryCommercialDetailsID = 0;
                int ItemID1 = ItemID;
                bool InventoryBasicDetailsIsActive = true;
                int TypeID = 2;
                int CategoryId = 0;
                bool InventoryBasicItemDetailsIsActive = true;
                string mode = "SELECT RECORD BY InventoryBasicDetailsID";

                CoreFactory.InventoryBasicDetailsList = BALFactory.NewInventoryBAL.GetInventoryBasicItemDetailBySearch(ID, InventoryBasicDetailsID1, InventoryBasicDetailsIsActive, InventoryBasicItemDetailsIsActive, mode, ItemID1, TypeID, CategoryId);
                if (CoreFactory.InventoryBasicDetailsList != null && CoreFactory.InventoryBasicDetailsList.Count > 0)
                {
                    foreach (var item in CoreFactory.InventoryBasicDetailsList)
                    {
                        _inventorySerialNumbers.Add(new ListItem { Text = item.InventoryBasicItemDetailsNumber, Value = item.ID.ToString() });
                    }
                }
                if (InventoryBasicItemDetailsID > 0)
                {
                    ViewBag.InventorySerialNumberListViewBag = new SelectList(_inventorySerialNumbers, "Value", "Text", InventoryBasicItemDetailsID);
                }
                else
                {
                    ViewBag.InventorySerialNumberListViewBag = new SelectList(_inventorySerialNumbers, "Value", "Text");
                }
            }
            if (InventoryMaintainanceAndCalibrationScheduleDatesID > 0) // code for dropdown schedule 
            {

                int ID = 0;
                bool InventoryMaintainanceAndCalibrationScheduleIsActive = true;
                bool IsActive = true;
                string Type1 = "Calibration";
                string AMCNumber = "";
                int InventoryBasicItemDetailsID1 = (int)InventoryBasicItemDetailsID;
                int InventoryMaintainanceAndCalibrationScheduleID = 0;
                string mode = "SELECT RECORD BY AMC NUMBER, BASIC ITEM DETAILS ID, TYPE, ID, SCHEDULED ID AND IS ACTIVE";
                CoreFactory.imcscheduledatelist = BALFactory.NewInventoryBAL.GetInventoryMaintainanceAndCalibrationScheduleDatesBySearch(ID, InventoryMaintainanceAndCalibrationScheduleIsActive, IsActive, Type1, AMCNumber, InventoryBasicItemDetailsID1, InventoryMaintainanceAndCalibrationScheduleID, mode);

                if (CoreFactory.imcscheduledatelist != null && CoreFactory.imcscheduledatelist.Count > 0)
                {
                    foreach (var item in CoreFactory.imcscheduledatelist)
                    {
                        _inventoryFrequencyDate.Add(new ListItem { Text = item.ScheduleDate.ToString(), Value = item.ID.ToString() });
                    }
                }
                if (InventoryMaintainanceAndCalibrationScheduleDatesID > 0)
                {
                    ViewBag.InventoryFrequencyDateListViewBag = new SelectList(_inventoryFrequencyDate, "Value", "Text", InventoryMaintainanceAndCalibrationScheduleDatesID);

                }
                else
                {
                    ViewBag.InventoryFrequencyDateListViewBag = new SelectList(_inventoryFrequencyDate, "Value", "Text");

                }
            }

            InventoryMaintainanceAndCalibrationModel model = new InventoryMaintainanceAndCalibrationModel();
            List<InventoryMaintainanceAndCalibrationModel> IMCLIST = new List<InventoryMaintainanceAndCalibrationModel>(); // for next date
            if (InventoryMaintainanceAndCalibrationScheduleDatesID > 0)
            {
                long ScheduleDateId = 0;
                ScheduleDateId = InventoryMaintainanceAndCalibrationScheduleDatesID;
                CoreFactory.inventoryMaintainanceAndCalibrationScheduleDatesEntity = BALFactory.NewInventoryBAL.GetScheduleNextDate(ScheduleDateId);
                DateTime date = (DateTime)CoreFactory.inventoryMaintainanceAndCalibrationScheduleDatesEntity.ScheduleDate;
                int id = (int)CoreFactory.inventoryMaintainanceAndCalibrationScheduleDatesEntity.InventoryMaintainanceAndCalibrationScheduleID;
                ScheduleDateId = id;
                CoreFactory.inventoryMaintainanceAndCalibrationScheduleDatesEntity = BALFactory.NewInventoryBAL.GetFrequnecyOfDates(ScheduleDateId, InventoryBasicItemDetailsID);
                int FrequencyOccurences = (int)CoreFactory.inventoryMaintainanceAndCalibrationScheduleDatesEntity.Frequency;
                if (FrequencyOccurences == 1)
                {

                    var nextdate = date.AddMonths(date.Month + 1);
                    var updatedate = nextdate.AddMonths(-(date.Month));
                    model.NextCalibrationDate = updatedate;
                    model.ndate = updatedate.ToString("dd/MM/yyyy").Replace("00:00:00", "");

                }
                else if (FrequencyOccurences == 2)
                {
                    var nextdate = date.AddMonths(date.Month + 3);
                    var updatedate = nextdate.AddMonths(-(date.Month));
                    model.NextCalibrationDate = updatedate;
                    model.ndate = updatedate.ToString("dd/MM/yyyy").Replace("00:00:00", "");

                }
                else if (FrequencyOccurences == 3)
                {
                    var nextdate = date.AddMonths(date.Month + 6);
                    var updatedate = nextdate.AddMonths(-(date.Month));
                    model.NextCalibrationDate = updatedate;
                    model.ndate = updatedate.ToString("dd/MM/yyyy").Replace("00:00:00", "");

                }
                else if (FrequencyOccurences == 4)
                {
                    var nextdate = date.AddMonths(date.Month + 12);
                    var updatedate = nextdate.AddMonths(-(date.Month));
                    model.NextCalibrationDate = updatedate;
                    model.ndate = updatedate.ToString("dd/MM/yyyy").Replace("00:00:00", "");

                }
            }

            // FatchListByBasicItemsDetailsAndItemsID // ofr down list code
            if (ItemID != 0 && BasicDetailsID != 0 && InventoryBasicItemDetailsID != 0)
            {
                long InventoryBasicDetailsID1 = BasicDetailsID;
                long InventoryBasicItemDetailsID1 = InventoryBasicItemDetailsID;
                string Type1 = "Calibration";
                int ItemID1 = ItemID;
                int ID = 0;
                string Mode = "SELECT RECORD BY ItemID AND InventoryBasicDetailsID";
                CoreFactory.imclist = BALFactory.NewInventoryBAL.GetInventoryMaintainanceAndCalibrationBySearch(ID, InventoryBasicDetailsID1, InventoryBasicItemDetailsID1, ItemID1, Type1, Mode);

                foreach (var item in CoreFactory.imclist)
                {
                    IMCLIST.Add(new InventoryMaintainanceAndCalibrationModel()
                    {
                        InventoryBasicItemDetailsNumber = item.InventoryBasicItemDetailsNumber,
                        ScheduleDate = item.ScheduleDate,
                        AuditDate = item.AuditDate,
                        Auditor = item.Auditor,
                        CompletionStatus = item.CompletionStatus,
                        ID = item.ID,
                        InventoryMaintainanceAndCalibrationScheduleDatesID = item.InventoryMaintainanceAndCalibrationScheduleDatesID,
                        InventoryBasicDetailsID = item.InventoryBasicDetailsID,
                        InventoryBasicItemDetailsID = item.InventoryBasicItemDetailsID,
                        ItemID = item.ItemID,

                    });
                }
            }
            //FatchItemsByID
            if (InventoryMaintainanceAndCalibrationScheduleDatesID != 0) // for updating purpose // geting data from db of inventory maintaince and calibration table
            {
                long ScheduleDateId = InventoryMaintainanceAndCalibrationScheduleDatesID;

                CoreFactory.inventoryMaintainanceAndCalibrationEntity = BALFactory.NewInventoryBAL.GetInvetoryMaintainceCalibrationData(ScheduleDateId, InventoryBasicItemDetailsID);
                if (CoreFactory.inventoryMaintainanceAndCalibrationEntity != null)
                {
                    model.ID = CoreFactory.inventoryMaintainanceAndCalibrationEntity.ID;
                    model.ItemID = CoreFactory.inventoryMaintainanceAndCalibrationEntity.ItemID;
                    model.InventoryBasicDetailsID = CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicDetailsID;
                    model.InventoryBasicItemDetailsID = CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicDetailsID;
                    model.InventoryMaintainanceAndCalibrationScheduleDatesID = CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryMaintainanceAndCalibrationScheduleDatesID;
                    model.CalibrationStartDate = CoreFactory.inventoryMaintainanceAndCalibrationEntity.CalibrationStartDate;
                    model.CalibrationEndDate = CoreFactory.inventoryMaintainanceAndCalibrationEntity.CalibrationEndDate;
                    model.AuditDate = CoreFactory.inventoryMaintainanceAndCalibrationEntity.AuditDate;
                    model.AuditObservations = CoreFactory.inventoryMaintainanceAndCalibrationEntity.AuditObservations;
                    model.Auditor = CoreFactory.inventoryMaintainanceAndCalibrationEntity.Auditor;
                    model.CalibratorName = CoreFactory.inventoryMaintainanceAndCalibrationEntity.CalibratorName;
                    model.ActionTaken = CoreFactory.inventoryMaintainanceAndCalibrationEntity.ActionTaken;
                    model.CompletionStatus = CoreFactory.inventoryMaintainanceAndCalibrationEntity.CompletionStatus;
                }
            }
            model.ItemID = ItemID;
            model.InventoryBasicDetailsID = Convert.ToInt32(BasicDetailsID);
            model.InventoryMaintainanceAndCalibrationScheduleDatesID = Convert.ToInt32(InventoryMaintainanceAndCalibrationScheduleDatesID);
            model.InventoryBasicItemDetailsNumber = InventoryBasicItemDetailsNumber;
            model.ItemName = ItemName;
            model.InventoryBasicItemDetailsID = InventoryBasicItemDetailsID;
            model.Type = "Calibration";
            return View(model);
        }
        [HttpPost]
        public ActionResult Calibration(InventoryMaintainanceAndCalibrationModel model)
        {
            string MID = "";

            if (model.CompletionStatus == "Completed")
            {
                //if(model.NextCalibrationDate == DateTime.Now)
                //{
                CoreFactory.purchaseEntity = new PurchaseEntity();
                CoreFactory.purchaseEntity.InventoryBasicItemDetailsNumber = model.InventoryBasicItemDetailsNumber;
                CoreFactory.purchaseEntity.ItemName = model.ItemName; //model.ItemName,model.InventoryBasicItemDetailsNumber;//model.NextCalibrationDate;
                CoreFactory.purchaseEntity.NextCalibrationDate = model.NextCalibrationDate;
                //CoreFactory.purchaseEntity.PurchaseRequestID = model.PurchaseRequestID;
                string Msg = " Maintenance/Calibration Details";//Raised &forwarded to TM
                long NotificationDetailId = BALFactory.NewInventoryBAL.AddNotification(Msg, "ADMIN", CoreFactory.purchaseEntity);
                long NotificationDetailId1 = BALFactory.NewInventoryBAL.AddNotification(Msg, "Purchase Incharge", CoreFactory.purchaseEntity);
                //}
            }
            if (model.ID > 0)
            {
                if (model != null)
                {
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity = new InventoryMaintainanceAndCalibrationEntity();
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.ID = model.ID;
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.ItemID = (Int32)model.ItemID;
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.Auditor = model.Auditor;
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.Type = model.Type;
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicItemDetailsID = model.InventoryBasicItemDetailsID;
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicDetailsID = model.InventoryBasicDetailsID;
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryMaintainanceAndCalibrationScheduleDatesID = model.InventoryMaintainanceAndCalibrationScheduleDatesID;
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.AuditDate = model.AuditDate;
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.AuditObservations = model.AuditObservations;
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.ActionTaken = model.ActionTaken;
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.UpdatedBy = 0;
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.UpdatedDate = DateTime.Now.ToLocalTime();
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.CalibratorName = model.CalibratorName;
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.NextDate = model.NextCalibrationDate;
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.StartDate = model.CalibrationStartDate;
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.EndDate = model.CalibrationEndDate;
                    CoreFactory.inventoryMaintainanceAndCalibrationEntity.CompletionStatus = model.CompletionStatus;
                    MID = BALFactory.NewInventoryBAL.UpdateInventoryMaintainanceAndCalibration(CoreFactory.inventoryMaintainanceAndCalibrationEntity);               
                }
                if (MID != null)
                {
                    if (MID != null)
                    {
                       // return RedirectToAction("CalibrationList", "InventorytainanceAndCalibration");
                        //  Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                         return Json(new { TypeCM = model.Type, status = "success", message = "Calibration Details updated successfully.", ID = MID });
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
                    return Json(new { status = "fail", message = "Internal server error occured." });
                }
            }
            else
            {
                CoreFactory.inventoryMaintainanceAndCalibrationEntity = new InventoryMaintainanceAndCalibrationEntity();
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.Auditor = model.Auditor;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.ItemID = (Int32)model.ItemID;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.Type = model.Type;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicItemDetailsID = model.InventoryBasicItemDetailsID;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicDetailsID = model.InventoryBasicDetailsID;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryMaintainanceAndCalibrationScheduleDatesID = model.InventoryMaintainanceAndCalibrationScheduleDatesID;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.AuditDate = model.AuditDate;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.AuditObservations = model.AuditObservations;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.ActionTaken = model.ActionTaken;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.InsertedBy = 0; 
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.InsertedDate =DateTime.Now.ToLocalTime();
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.CalibratorName = model.CalibratorName;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.NextDate = model.NextCalibrationDate;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.StartDate = model.CalibrationStartDate;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.EndDate = model.CalibrationEndDate;
                CoreFactory.inventoryMaintainanceAndCalibrationEntity.CompletionStatus = model.CompletionStatus;
                MID = BALFactory.NewInventoryBAL.InsertInventoryMaintainanceAndCalibration(CoreFactory.inventoryMaintainanceAndCalibrationEntity);
                if (MID != null)
                {
                    if (MID != null)
                    {
                       // return RedirectToAction("CalibrationList", "InventoryMaintainanceAndCalibration");
                        //  Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                         return Json(new { TypeCM = model.Type, status = "success", message = "Calibration Details updated successfully.", ID = MID });
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
                    return Json(new { status = "fail", message = "Internal server error occured." });
                }
            }
        }

        [HttpPost]
        public ActionResult FatchSerialNumbers(int ItemID = 0, long InventoryBasicDetailsID = 0)
        {
            int ID = 0;
            long InventoryBasicDetailsID1 = InventoryBasicDetailsID;
            int InventoryCommercialDetailsID = 0;
            int ItemID1 = ItemID;
            bool InventoryBasicDetailsIsActive = true;
            int TypeID = 2;
            int CategoryId = 0;
            bool InventoryBasicItemDetailsIsActive = true;
            string mode = "SELECT RECORD BY InventoryBasicDetailsID";

            CoreFactory.InventoryBasicDetailsList = BALFactory.NewInventoryBAL.GetInventoryBasicItemDetailBySearch(ID, InventoryBasicDetailsID1, InventoryBasicDetailsIsActive, InventoryBasicItemDetailsIsActive, mode, ItemID1, TypeID, CategoryId);

            if (CoreFactory.InventoryBasicDetailsList != null)
            {

                if (CoreFactory.InventoryBasicDetailsList != null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Json(new { status = "success", message = "Customer details added successfully.", BasicItemsDetails = CoreFactory.InventoryBasicDetailsList });
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Json(new { status = "info", message = "No record found." }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Json(new { status = "fail", message = "Internal server error occured." });
            }
            return View("Maintainance");
        }
        [HttpPost]
        public ActionResult FatchFrequencyDates(int InventoryBasicItemDetailsID, string Type)
        {
            // new code

            int ID = 0;
            bool InventoryMaintainanceAndCalibrationScheduleIsActive = true;
            bool IsActive = true;
            string Type1 = Type;
            string AMCNumber = "";
            int InventoryBasicItemDetailsID1 = InventoryBasicItemDetailsID;
            int InventoryMaintainanceAndCalibrationScheduleID = 0;
            string mode = "SELECT RECORD BY AMC NUMBER, BASIC ITEM DETAILS ID, TYPE, ID, SCHEDULED ID AND IS ACTIVE";


            CoreFactory.imcscheduledatelist = BALFactory.NewInventoryBAL.GetInventoryMaintainanceAndCalibrationScheduleDatesBySearch(ID, InventoryMaintainanceAndCalibrationScheduleIsActive, IsActive, Type1, AMCNumber, InventoryBasicItemDetailsID1, InventoryMaintainanceAndCalibrationScheduleID, mode);

            ViewBag.InventoryFrequencyDateListViewBag = CoreFactory.imcscheduledatelist;
            //IBaseEntityCollectionResponse<DTO.EntityDTO.InventoryMaintainanceAndCalibrationScheduleDates> responseInventoryMaintainanceAndCalibrationScheduleDates = _inventoryMaintainanceAndCalibrationScheduleDatesDataProvider.GetInventoryMaintainanceAndCalibrationScheduleDatesBySearch(_searchRequest);

            if (ViewBag.InventoryFrequencyDateListViewBag != null)
            {

                if (ViewBag.InventoryFrequencyDateListViewBag != null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Json(new { status = "success", message = "Customer details added successfully.", FrequencyDate = ViewBag.InventoryFrequencyDateListViewBag });
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
                return Json(new { status = "fail", message = "Internal server error occured." });
            }
            return View();
        }
        [HttpPost]
        public ActionResult FatchSerialNumbersByItem(int ItemID)
        {

            int ID = 0;
            int InventoryBasicDetailsID1 = 0;
            int InventoryCommercialDetailsID = 0;
            int ItemID1 = ItemID;
            bool InventoryBasicDetailsIsActive = true;
            bool InventoryBasicItemDetailsIsActive = true;
            int TypeID = 2;
            int CategoryId = 0;
            string Mode = "SELECT RECORD BY ITEM ID";
            //USP_InventoryBasicItemDetails_Select

            // CoreFactory.InventoryBasicDetailsList = BALFactory.NewInventoryBAL.GetInventoryBasicItemDetailBySearch();

            CoreFactory.InventoryBasicDetailsList = BALFactory.NewInventoryBAL.GetInventoryBasicItemDetailBySearch(ID, InventoryBasicDetailsID1, InventoryBasicDetailsIsActive, InventoryBasicItemDetailsIsActive, Mode, ItemID1, TypeID, CategoryId);

            // IBaseEntityCollectionResponse<InventoryBasicItemDetail> responseInventoryBasicDetail = _inventoryBasicItemDetailDataProvider.GetInventoryBasicItemDetailBySearch(_inventoryBasicItemDetailSearchRequest);
            if (CoreFactory.InventoryBasicDetailsList != null)
            {
                if (CoreFactory.InventoryBasicDetailsList != null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Json(new { status = "success", message = "Customer details added successfully.", BasicItemsDetails = CoreFactory.InventoryBasicDetailsList });
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
                return Json(new { status = "fail", message = "Internal server error occured." });
            }
            return View();
        }
        [HttpPost]
        public ActionResult FatchListByBasicItemsDetailsAndItemsID(string Type, long InventoryBasicDetailsID = 0, int ItemID = 0, long InventoryBasicItemDetailsID = 0)
        {
           
            long InventoryBasicDetailsID1 = InventoryBasicDetailsID;
            long InventoryBasicItemDetailsID1 = InventoryBasicItemDetailsID;
            string Type1 = Type;
            int ItemID1 = ItemID;
            int ID = 0;
            string Mode = "SELECT RECORD BY ItemID AND InventoryBasicDetailsID";

            CoreFactory.imclist = BALFactory.NewInventoryBAL.GetInventoryMaintainanceAndCalibrationBySearch(ID, InventoryBasicDetailsID1, InventoryBasicItemDetailsID1, ItemID, Type1, Mode);
            if (CoreFactory.imclist != null)
            {

                if (CoreFactory.imclist != null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Json(new { status = "success", message = "Customer details added successfully.", List = CoreFactory.imclist });
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
                return Json(new { status = "fail", message = "Internal server error occured." });
            }
            return View();
        }
        [HttpPost]
        public ActionResult FatchItemsByID(int ID, string Type)
        {          // new code
            long InventoryBasicDetailsID1 = 0;
            long InventoryBasicItemDetailsID1 = 0;
            int ItemID = 0;
            int ID1 = ID;
            string Type1 = Type;
            string Mode = "SELECT RECORD BY ID";

            CoreFactory.imclist = BALFactory.NewInventoryBAL.GetInventoryMaintainanceAndCalibrationBySearch(ID1, InventoryBasicDetailsID1, InventoryBasicItemDetailsID1, ItemID, Type1, Mode);
            if (CoreFactory.imclist != null)
            {

                if (CoreFactory.imclist != null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return Json(new { status = "success", message = "Customer details added successfully.", Entity = CoreFactory.imclist });
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
                return Json(new { status = "fail", message = "Internal server error occured." });
            }
            return View();
        }
    }
}