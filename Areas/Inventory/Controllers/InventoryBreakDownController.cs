using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LIMS_DEMO.Areas.Inventory.Models;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Inventory;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Inventory;

namespace LIMS_DEMO.Areas.Inventory.Controllers
{
    
    [RouteArea("Inventory")]
    [Authorize]
    public class InventoryBreakDownController : Controller
    {
        // GET: Inventory/InventoryMaintainanceAndCalibrationBreakDown
        
        public InventoryBreakDownController()
        {
            BALFactory.InventoryBreakDownBAL = new InventoryBreakDownBAL();
            BALFactory.NewInventoryBAL = new NewInventoryBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }

        public ActionResult Index()
        {
            return View();
        }

        #region BREAKDOWN
        
        public ActionResult AddBreakDown()
        {
            InventoryBreakDownModel model = new InventoryBreakDownModel();
            int InventoryTypeID = 2;
                int ItemId = 0;
                int InventoryBasicDetailsId = 0;
                ViewBag.InventoryItemList = CoreFactory.dropdowns.GetItemMaster(InventoryTypeID);
                ViewBag.ItemNumberList = BALFactory.InventoryBreakDownBAL.GetSerialNumbersByID(ItemId, InventoryBasicDetailsId);
               
             return View(model);
        }
        public ActionResult EditBreakDown(int ID = 0)
        {
            InventoryBreakDownModel model = new InventoryBreakDownModel();  
            CoreFactory.breakDownEntity = BALFactory.InventoryBreakDownBAL.GetBreakDowmDataEdit(ID);

            model.InventoryMaintAndCalibBreakDownId = CoreFactory.breakDownEntity.InventoryMaintAndCalibBreakDownId;
            model.ItemMasterId = CoreFactory.breakDownEntity.ItemMasterId;
            model.IDNO = CoreFactory.breakDownEntity.IDNO;
            model.NameOfInstrument = CoreFactory.breakDownEntity.NameOfInstrument;
            model.InventoryBasicItemDetailsId = CoreFactory.breakDownEntity.InventoryBasicItemDetailsId;
            model.InventoryBasicDetailId = CoreFactory.breakDownEntity.InventoryBasicDetailId;
            model.ProblemReportedBy = CoreFactory.breakDownEntity.ProblemReportedBy;
            model.ProblemReportedDate = CoreFactory.breakDownEntity.ProblemReportedDate;
            model.ProblemDescription = CoreFactory.breakDownEntity.ProblemDescription;
            model.DateOfStartRepair = CoreFactory.breakDownEntity.DateOfStartRepair;
            model.DateOfCompletionRepair = CoreFactory.breakDownEntity.DateOfCompletionRepair;
            model.MaintenanceInspectedBy = CoreFactory.breakDownEntity.MaintenanceInspectedBy;
            model.NameOfAgency = CoreFactory.breakDownEntity.NameOfAgency;
            model.Expenses = CoreFactory.breakDownEntity.Expenses;
            model.Remark = CoreFactory.breakDownEntity.Remark;
            model.IsRepair = CoreFactory.breakDownEntity.IsRepair;
            model.ID = ID;
            int InventoryTypeID = 2;
            int ItemId = (int)model.InventoryBasicItemDetailsId;
            int InventoryBasicDetailsId = (int)model.InventoryBasicDetailId;
            ViewBag.InventoryItemList = CoreFactory.dropdowns.GetItemMaster(InventoryTypeID);
            ViewBag.ItemNumberList = BALFactory.InventoryBreakDownBAL.GetSerialNumbersByID_1(ItemId, InventoryBasicDetailsId);

            return View(model);
        }
        [HttpPost]
       
        public ActionResult AddBreakDown(int ItemMasterId, int InventoryBasicDetailID, int InventoryBasicItemDetailsID, string ProblemDescription, string ProblemReportedBy, DateTime? ProblemReportedDate, DateTime? DateOfStartRepair, DateTime? DateOfCompletionRepair, string MaintenanceInspectedBy, long? Expenses, string NameOfAgency, string Remark, int? Id, bool IsRepair)
        {
            CoreFactory.breakDownEntity = new BreakDownEntity();
            CoreFactory.breakDownEntity.ID = Id;
            CoreFactory.breakDownEntity.ItemMasterId = ItemMasterId;
            CoreFactory.breakDownEntity.InventoryBasicItemDetailsId = InventoryBasicItemDetailsID;
            CoreFactory.breakDownEntity.InventoryBasicDetailId = InventoryBasicDetailID;
            CoreFactory.breakDownEntity.ProblemReportedBy = ProblemReportedBy;
            CoreFactory.breakDownEntity.ProblemReportedDate = ProblemReportedDate;
            CoreFactory.breakDownEntity.ProblemDescription = ProblemDescription;
            CoreFactory.breakDownEntity.DateOfStartRepair = DateOfStartRepair;
            CoreFactory.breakDownEntity.DateOfCompletionRepair = DateOfCompletionRepair;
            CoreFactory.breakDownEntity.MaintenanceInspectedBy = MaintenanceInspectedBy;
            CoreFactory.breakDownEntity.Expenses = Expenses;
            CoreFactory.breakDownEntity.Remark = Remark;
            CoreFactory.breakDownEntity.NameOfAgency = NameOfAgency;
            CoreFactory.breakDownEntity.IsRepair = IsRepair;
            bool result = BALFactory.InventoryBreakDownBAL.AddBreakdown(CoreFactory.breakDownEntity);
            return Json(new { status = result, message = "Added Breakdown" }, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("BreackDownMaintainanceList");
        }

      
        public ActionResult BreackDownList(string search)
        {
            CoreFactory.breakDownEntityList = BALFactory.InventoryBreakDownBAL.GetBreakDowmDataList();
            List<InventoryBreakDownModel> List = new List<InventoryBreakDownModel>();
            if (CoreFactory.breakDownEntityList != null)
            {
                foreach (var Item in CoreFactory.breakDownEntityList)
                {
                    List.Add(new InventoryBreakDownModel()
                    {
                        InventoryMaintAndCalibBreakDownId = Item.InventoryMaintAndCalibBreakDownId,
                        ItemMasterId = Item.ItemMasterId,
                        IDNO = Item.IDNO,
                        NameOfInstrument = Item.NameOfInstrument,
                        InventoryBasicItemDetailsId = Item.InventoryBasicItemDetailsId,
                        InventoryBasicDetailId = Item.InventoryBasicDetailId,
                        ProblemReportedBy = Item.ProblemReportedBy,
                        ProblemReportedDate = Item.ProblemReportedDate,
                        ProblemDescription = Item.ProblemDescription,
                        DateOfStartRepair = Item.DateOfStartRepair,
                        DateOfCompletionRepair = Item.DateOfCompletionRepair,
                        MaintenanceInspectedBy = Item.MaintenanceInspectedBy,
                        NameOfAgency = Item.NameOfAgency,
                        Expenses = Item.Expenses,
                        Remark = Item.Remark,
                        IsRepair = Item.IsRepair,
                    });
                }
                return View(List);
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public JsonResult GetSerialNumbers(int ItemId = 0, int InventoryBasicDetailsId = 0)
        {
            CoreFactory.ItemSerialNumber = BALFactory.InventoryBreakDownBAL.GetSerialNumbersByID(ItemId, InventoryBasicDetailsId);
            //return Json(new { status = "success", message = "", SerialNumber = CoreFactory.ItemSerialNumber});
            return Json(CoreFactory.ItemSerialNumber, JsonRequestBehavior.AllowGet);
        }
       
        [HttpGet]
        public JsonResult GetDataBySrNo(int InventoryBasicItemDetailsID = 0, int InventorybasicdetailsID = 0, int InventoryBasicDetailID = 0)
        {
            CoreFactory.breakDownEntityList = BALFactory.InventoryBreakDownBAL.GetBreakDowmDataBySrNo(InventoryBasicItemDetailsID, InventorybasicdetailsID, InventoryBasicDetailID);
            //return Json(new { status = "success", message = "", SerialNumber = CoreFactory.ItemSerialNumber});
            return Json(CoreFactory.breakDownEntityList, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region Performance Check
        
        [HttpGet]
        public ActionResult PlanePerformance()
        {
            InventoryMaintainanceAndCalibrationModel model = new InventoryMaintainanceAndCalibrationModel();
            int InventoryTypeID = 2;
            int ItemId = 0;
            int InventoryBasicDetailsId = 0;
            ViewBag.InventoryItemList = CoreFactory.dropdowns.GetItemMaster(InventoryTypeID);
            ViewBag.ItemNumberList = BALFactory.InventoryBreakDownBAL.GetSerialNumbersByID(ItemId, InventoryBasicDetailsId);
            List<ListItem> _FrequencyType = new List<ListItem>();
            _FrequencyType.Add(new ListItem { Text = "Monthly", Value = "1" });
            _FrequencyType.Add(new ListItem { Text = "Quarterly", Value = "2" });
            _FrequencyType.Add(new ListItem { Text = "Half Yearly", Value = "3" });
            _FrequencyType.Add(new ListItem { Text = "Yearly", Value = "4" });
            ViewBag.FrequencyTypeListViewBag = new SelectList(_FrequencyType, "Value", "Text");
            return View(model);
        }
        [HttpPost]
       
        public ActionResult PlanePerformance(InventoryMaintainanceAndCalibrationModel model)
        {
           
            CoreFactory.inventoryMaintainanceAndCalibrationEntity = new InventoryMaintainanceAndCalibrationEntity();
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.ItemID = (int)model.ItemID;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicItemDetailsID = model.InventoryBasicItemDetailsID;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.AMCPeriod = model.AMCPeriod;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.Frequency = model.Frequency;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.StartDate = model.StartDate;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.AMCDate = DateTime.UtcNow;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.AMCNumber = "----";
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.Type = "PerFormance-Check";
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.AMCPeriod = model.AMCPeriod;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicDetailsID = model.InventoryBasicDetailsID;
            int ID = BALFactory.InventoryBreakDownBAL._FindPerformanceCheck((int)model.InventoryBasicItemDetailsID);
            if(ID == 0)
            {
                var result = BALFactory.NewInventoryBAL.InsertInventoryMaintainanceDetail(CoreFactory.inventoryMaintainanceAndCalibrationEntity);
            }
            else
            {
                //var result = BALFactory.NewInventoryBAL.InsertInventoryMaintainanceDetailUpdate(CoreFactory.inventoryMaintainanceAndCalibrationEntity,ID);
                return Json(new { status = "False" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = "Success" }, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("PerformanceList");

        }

        [HttpGet]
        
        public ActionResult PerformanceList()
        {
            var sActionType = "PerFormance-Check";
            CoreFactory.InventoryMaintenanceList = BALFactory.NewInventoryBAL.InventoryCalibrationAndMaintenanceList(sActionType);
            List<InventoryMaintenanceListModel> MaintenanceList = new List<InventoryMaintenanceListModel>();
            foreach (var Item in CoreFactory.InventoryMaintenanceList)
            {
                MaintenanceList.Add(new InventoryMaintenanceListModel()
                {
                    ScheduleID = Item.ScheduleID,
                    CompletionDateID = Item.CompletionDateID,
                    NextDateID = Item.NextDateID,
                    NextDate = Item.NextDate,
                    CompletionDate = Item.CompletionDate,
                    InventoryBasicItemDetailsID = Item.InventoryBasicItemDetailsID,
                    InventoryBasicItemDetailsNumber = Item.InventoryBasicItemDetailsNumber,
                    InventoryBasicDetailsID = Item.InventoryBasicDetailsID,
                    ItemID = Item.ItemID,
                    ItemName = Item.ItemName,
                    CategoryID = Item.CategoryID,
                    CategoryName = Item.CategoryName,
                    Frequency = Item.Frequency
                });
            }
            return View(MaintenanceList);
        }
        [HttpGet]
       
        public ActionResult PerformanceCheak(string InventoryBasicItemDetailsNumber, string ItemName, int ItemID = 0, long BasicItemDetailsID = 0, long DateID = 0, long BasicDetailsID = 0)
        {
            List<ListItem> _MaintainanceStatus = new List<ListItem>();
            _MaintainanceStatus.Add(new ListItem { Text = "In Progress", Value = "In Progress" });
            _MaintainanceStatus.Add(new ListItem { Text = "Completed", Value = "Completed" });
            ViewBag.MaintainanceListViewBag = new SelectList(_MaintainanceStatus, "Value", "Text");
            InventoryMaintainanceAndCalibrationModel model = new InventoryMaintainanceAndCalibrationModel();
            var Data = BALFactory.InventoryBreakDownBAL.GetDatabyDateID(ItemID, DateID, BasicDetailsID);
            
            model.ID = 0;
            model.ItemID = ItemID;
            model.InventoryBasicDetailsID = Convert.ToInt32(BasicDetailsID);
            model.InventoryMaintainanceAndCalibrationScheduleDatesID = Convert.ToInt32(DateID);
            model.InventoryBasicItemDetailsNumber = InventoryBasicItemDetailsNumber;
            model.ItemName = ItemName;
            model.CalibrationInventoryBasicItemDetailsID = (int)BasicItemDetailsID;
            model.InventoryBasicItemDetailsID = BasicItemDetailsID;
            model.Type = "PerFormance-Check";
            if(Data==null)
            {
                model.Auditor =  "";
                model.AuditDate = null; 
                model.AuditObservations = ""; 
                model.ActionTaken = "";
            }
            else
            {
                model.Auditor = Data.Auditor != null ? Data.Auditor : "";
                model.AuditDate = Data.AuditDate;
                model.AuditObservations = Data.AuditObservations;
                model.ActionTaken = Data.ActionTaken;
            }
               
            int InventoryTypeMasterId = 2;
                ViewBag.InventoryItemList = BALFactory.dropdownsBAL.GetItemMaster(InventoryTypeMasterId);

                ViewBag.ItemNumberList = BALFactory.InventoryBreakDownBAL.GetSerialNumbersByIBID(ItemID, (int)BasicDetailsID);
                ViewBag.ItemScheduleDateList = BALFactory.InventoryBreakDownBAL.GetscheduleDateByItemNumber((int)BasicItemDetailsID);
            

            return View(model);
        }

        [HttpPost]
        public ActionResult PerformanceCheak(InventoryMaintainanceAndCalibrationModel model)
        {
            CoreFactory.inventoryMaintainanceAndCalibrationEntity = new InventoryMaintainanceAndCalibrationEntity();
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.Type = "PerFormance-Check";
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicDetailsID = model.InventoryBasicDetailsID;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.Auditor = model.Auditor;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.AuditDate = model.AuditDate;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.AuditObservations = model.AuditObservations;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.ActionTaken = model.ActionTaken;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.ItemID = (int)model.ItemID;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryMaintainanceAndCalibrationScheduleDatesID = model.InventoryMaintainanceAndCalibrationScheduleDatesID;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.UpdatedDate = DateTime.Now;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.CalibratorName = model.CalibratorName;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.NextDate = model.NextDate;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.InventoryBasicItemDetailsID = model.InventoryBasicItemDetailsID;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.StartDate = model.StartDate;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.EndDate = model.EndDate;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.DateID = model.DateID;
            CoreFactory.inventoryMaintainanceAndCalibrationEntity.CompletionStatus = model.CompletionStatus;
            bool result = BALFactory.InventoryBreakDownBAL.savePerformanceCheck(CoreFactory.inventoryMaintainanceAndCalibrationEntity);
            // return Json("success", JsonRequestBehavior.AllowGet);
            return RedirectToAction("PerformanceList");
        }
        public JsonResult FindPerformanceCheck(int InventoryBasicItemDetailsID = 0)
        {
            var Result = BALFactory.InventoryBreakDownBAL._FindPerformanceCheck(InventoryBasicItemDetailsID);
            //return Json(new { status = "success", message = "", SerialNumber = CoreFactory.ItemSerialNumber});
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
       
        public JsonResult FatchListByBasicItemsDetailsAndItemsID(string Type, long InventoryBasicDetailsID = 0, int ItemID = 0, long InventoryBasicItemDetailsID = 0)
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
                return Json(CoreFactory.imclist, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No Record Found", JsonRequestBehavior.AllowGet);
            }
        }
       
        #endregion
    }
}
