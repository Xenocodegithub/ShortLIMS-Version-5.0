using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Inventory.Models;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Inventory;
using LIMS_DEMO.BAL.DropDown;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Inventory;
using System.IO;
using Newtonsoft.Json;

namespace LIMS_DEMO.Areas.Inventory.Controllers
{
    public class PurchaseController : Controller
    {
        string strStatus = "";
        public PurchaseController()
        {
            BALFactory.PurchaseBAL = new PurchaseBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
        }

        // GET: Inventory/Purchase

        public ActionResult Index()
        {
            return View();
        }
        //--------------------------------------Pages------------------------------------------------
        public ActionResult PurchaseRequestForm(int? PurchaseRequestID = 0, int? PurchaseMasterID = 0)
        {
            PurchaseModel model = new PurchaseModel();
            model.PurchaseRequestID = (byte)PurchaseRequestID;
            model.PurchaseMasterID = (byte)PurchaseMasterID;
            model.TickIf = false;
            if (PurchaseRequestID != 0)
            {
                CoreFactory.purchaseEntity = BALFactory.PurchaseBAL.GetPurchaseRequestDetails((Int32)PurchaseRequestID);
                if (CoreFactory.purchaseEntity != null)
                {
                    model.PurchaseRequestID = CoreFactory.purchaseEntity.PurchaseRequestID;
                    model.PurchaseSupplierID = CoreFactory.purchaseEntity.PurchaseSupplierID;
                    model.PurchaseMasterID = CoreFactory.purchaseEntity.PurchaseMasterID;
                    model.ItemMasterId = CoreFactory.purchaseEntity.ItemMasterId;
                    model.Brand = CoreFactory.purchaseEntity.Brand;
                    model.Quantity = (Int32)CoreFactory.purchaseEntity.Quantity;
                    model.PackSize = CoreFactory.purchaseEntity.PackSize;
                    model.CatagoryName = CoreFactory.purchaseEntity.CategoryName;
                    model.Grade = CoreFactory.purchaseEntity.Grade;
                    model.Priority = CoreFactory.purchaseEntity.Priority;
                    model.Purpose = CoreFactory.purchaseEntity.Purpose;
                    model.Specification = CoreFactory.purchaseEntity.Specification;
                    model.Remark = CoreFactory.purchaseEntity.Remark;
                    model.Rate = CoreFactory.purchaseEntity.Rate;
                    model.EstimatedLagTime = CoreFactory.purchaseEntity.EstimatedLagTime;
                    model.TickIf = CoreFactory.purchaseEntity.TickIf;
                    model.COA = CoreFactory.purchaseEntity.COA;
                    model.IsActive = CoreFactory.purchaseEntity.IsActive;
                }

            }

            ViewBag.InventoryName = BALFactory.dropdownsBAL.GetInventoryType();
            ViewBag.Item = BALFactory.dropdownsBAL.GetItemMaster(model.InventoryTypeID);
            ViewBag.Supplier = BALFactory.dropdownsBAL.GetSupplierName();

            return View(model);
        }
        [HttpGet]
        public PartialViewResult _PurchaseRequestFormList(int? Id = 0, int? PurchaseRequestID = 0)
        {
            PurchaseModel model = new PurchaseModel();
            if (Id != 0 && TempData["PurchaseList"] != null)
            {
                var purchaseList = (List<PurchaseModel>)TempData.Peek("PurchaseList");
                TempData.Keep("PurchaseList");
                model = purchaseList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            }
            if (PurchaseRequestID != 0)
            {
                CoreFactory.purchaseEntity = BALFactory.PurchaseBAL.GetPurchaseRequestDetails((Int32)PurchaseRequestID);
                if (CoreFactory.purchaseEntity != null)
                {
                    model.PurchaseRequestID = CoreFactory.purchaseEntity.PurchaseRequestID;
                    model.PurchaseSupplierID = CoreFactory.purchaseEntity.PurchaseSupplierID;
                    model.PurchaseMasterID = CoreFactory.purchaseEntity.PurchaseMasterID;
                    model.ItemMasterId = CoreFactory.purchaseEntity.ItemMasterId;
                    model.InventoryTypeID = CoreFactory.purchaseEntity.InventoryTypeID;
                    model.Brand = CoreFactory.purchaseEntity.Brand;
                    model.FileName = CoreFactory.purchaseEntity.FileName;
                    model.CatagoryName = CoreFactory.purchaseEntity.CategoryName;
                    model.PurchaseUpload = CoreFactory.purchaseEntity.PurchaseUpload;
                    model.Quantity = (Int32)CoreFactory.purchaseEntity.Quantity;
                    //model.Amount = CoreFactory.purchaseEntity.Amount;
                    //model.GSTAmount = CoreFactory.purchaseEntity.GSTAmount;
                    //model.DiscAmount = CoreFactory.purchaseEntity.DiscAmount;
                    //model.NetAmount = (Int32)CoreFactory.purchaseEntity.NetAmount;
                    model.PackSize = CoreFactory.purchaseEntity.PackSize;
                    model.Grade = CoreFactory.purchaseEntity.Grade;
                    model.Priority = CoreFactory.purchaseEntity.Priority;
                    model.Purpose = CoreFactory.purchaseEntity.Purpose;
                    model.Specification = CoreFactory.purchaseEntity.Specification;
                    model.Remark = CoreFactory.purchaseEntity.Remark;
                    //model.Rate = CoreFactory.purchaseEntity.Rate;
                    //model.EstimatedLagTime = CoreFactory.purchaseEntity.EstimatedLagTime;
                    //model.TickIf = CoreFactory.purchaseEntity.TickIf;
                    model.COA = CoreFactory.purchaseEntity.COA;

                    model.IsActive = CoreFactory.purchaseEntity.IsActive;
                }
            }

            ViewBag.InventoryName = BALFactory.dropdownsBAL.GetInventoryType();
            ViewBag.Item = BALFactory.dropdownsBAL.GetItemMaster(model.InventoryTypeID);
            return PartialView(model);
        }
        public PartialViewResult _PurchaseList()
         {
            List<PurchaseModel> model = new List<PurchaseModel>();
            if (TempData["PurchaseList"] != null)
            {
                model = (List<PurchaseModel>)TempData.Peek("PurchaseList");
                TempData.Keep("PurchaseList");
            }
            return PartialView(model);
        }

        public ActionResult PurchaseForm(int PurchaseRequestID, string Category)
        {
            PurchaseModel model = new PurchaseModel();
            CoreFactory.purchaseEntity = BALFactory.PurchaseBAL.GetPurchaseRequestDetails((Int32)PurchaseRequestID);
            if (CoreFactory.purchaseEntity != null)
            {
                model.PurchaseRequestID = CoreFactory.purchaseEntity.PurchaseRequestID;
                model.PurchaseSupplierID = CoreFactory.purchaseEntity.PurchaseSupplierID;
                model.PurchaseMasterID = CoreFactory.purchaseEntity.PurchaseMasterID;
                model.ItemMasterId = CoreFactory.purchaseEntity.ItemMasterId;
                model.InventoryTypeID = CoreFactory.purchaseEntity.InventoryTypeID;
                model.Brand = CoreFactory.purchaseEntity.Brand;
                model.Quantity = (Int32)CoreFactory.purchaseEntity.Quantity;
                model.PackSize = CoreFactory.purchaseEntity.PackSize;
                model.Grade = CoreFactory.purchaseEntity.Grade;
                model.Amount = CoreFactory.purchaseEntity.Amount;
                model.DiscAmount = CoreFactory.purchaseEntity.DiscAmount;
                model.GSTAmount = CoreFactory.purchaseEntity.GSTAmount;
                model.NetAmount = CoreFactory.purchaseEntity.NetAmount;
                model.Priority = CoreFactory.purchaseEntity.Priority;
                model.Purpose = CoreFactory.purchaseEntity.Purpose;
                model.Specification = CoreFactory.purchaseEntity.Specification;
                model.Remark = CoreFactory.purchaseEntity.Remark;
                model.Rate = CoreFactory.purchaseEntity.Rate;
                model.EstimatedLagTime = CoreFactory.purchaseEntity.EstimatedLagTime;
                model.TickIf = CoreFactory.purchaseEntity.TickIf;
                model.COA = CoreFactory.purchaseEntity.COA;
                model.CatagoryName = Category;
                model.IsActive = CoreFactory.purchaseEntity.IsActive;
            }

            ViewBag.InventoryName = BALFactory.dropdownsBAL.GetInventoryType();
            ViewBag.Item = BALFactory.dropdownsBAL.GetItemMaster(model.InventoryTypeID);
            ViewBag.Supplier = BALFactory.dropdownsBAL.GetSupplierName();
            return View(model);
        }

        public ActionResult PurchaseFormHO(int PurchaseRequestID)
        {
            PurchaseModel model = new PurchaseModel();


            CoreFactory.purchaseEntity = BALFactory.PurchaseBAL.GetPurchaseRequestDetails((Int32)PurchaseRequestID);
            if (CoreFactory.purchaseEntity != null)
            {
                model.PurchaseRequestID = CoreFactory.purchaseEntity.PurchaseRequestID;
                model.PurchaseSupplierID = CoreFactory.purchaseEntity.PurchaseSupplierID;
                model.PurchaseMasterID = CoreFactory.purchaseEntity.PurchaseMasterID;
                model.ItemMasterId = CoreFactory.purchaseEntity.ItemMasterId;
                model.InventoryTypeID = CoreFactory.purchaseEntity.InventoryTypeID;
                model.Brand = CoreFactory.purchaseEntity.Brand;
                model.Quantity = (Int32)CoreFactory.purchaseEntity.Quantity;
                model.PackSize = CoreFactory.purchaseEntity.PackSize;
                model.Grade = CoreFactory.purchaseEntity.Grade;
                model.Amount = CoreFactory.purchaseEntity.Amount;
                model.DiscAmount = CoreFactory.purchaseEntity.DiscAmount;
                model.GSTAmount = CoreFactory.purchaseEntity.GSTAmount;
                model.NetAmount = CoreFactory.purchaseEntity.NetAmount;
                model.Priority = CoreFactory.purchaseEntity.Priority;
                model.Purpose = CoreFactory.purchaseEntity.Purpose;
                model.Specification = CoreFactory.purchaseEntity.Specification;
                model.Remark = CoreFactory.purchaseEntity.Remark;
                model.Rate = CoreFactory.purchaseEntity.Rate;
                model.EstimatedLagTime = CoreFactory.purchaseEntity.EstimatedLagTime;
                model.TickIf = CoreFactory.purchaseEntity.TickIf;
                model.COA = CoreFactory.purchaseEntity.COA;

                model.IsActive = CoreFactory.purchaseEntity.IsActive;
            }

            ViewBag.InventoryName = BALFactory.dropdownsBAL.GetInventoryType();
            ViewBag.Item = BALFactory.dropdownsBAL.GetItemMaster(model.InventoryTypeID);
            ViewBag.Supplier = BALFactory.dropdownsBAL.GetSupplierName();
            return View(model);
        }

        public ActionResult PurchaseFormPI(int PurchaseRequestID)
        {
            PurchaseModel model = new PurchaseModel();


            CoreFactory.purchaseEntity = BALFactory.PurchaseBAL.GetPurchaseRequestDetailsPI((Int32)PurchaseRequestID);
            if (CoreFactory.purchaseEntity != null)
            {
                model.PurchaseRequestID = CoreFactory.purchaseEntity.PurchaseRequestID;
                model.PurchaseSupplierID = CoreFactory.purchaseEntity.PurchaseSupplierID;
                model.PurchaseMasterID = CoreFactory.purchaseEntity.PurchaseMasterID;
                model.ItemMasterId = CoreFactory.purchaseEntity.ItemMasterId;
                model.InventoryTypeID = CoreFactory.purchaseEntity.InventoryTypeID;
                model.Brand = CoreFactory.purchaseEntity.Brand;
                model.Quantity = (Int32)CoreFactory.purchaseEntity.Quantity;
                model.PackSize = CoreFactory.purchaseEntity.PackSize;
                model.Grade = CoreFactory.purchaseEntity.Grade;
                model.Amount = CoreFactory.purchaseEntity.Amount;
                model.AmountAfterDisc = CoreFactory.purchaseEntity.DiscAmount;
                model.GSTAmount = CoreFactory.purchaseEntity.GSTAmount;
                model.NetAmount = CoreFactory.purchaseEntity.NetAmount;
                model.Priority = CoreFactory.purchaseEntity.Priority;
                model.Purpose = CoreFactory.purchaseEntity.Purpose;
                model.Specification = CoreFactory.purchaseEntity.Specification;
                model.Remark = CoreFactory.purchaseEntity.Remark;
                model.Rate = CoreFactory.purchaseEntity.Rate;
                model.EstimatedLagTime = CoreFactory.purchaseEntity.EstimatedLagTime;
                model.TickIf = CoreFactory.purchaseEntity.TickIf;
                model.COA = CoreFactory.purchaseEntity.COA;

                model.IsActive = CoreFactory.purchaseEntity.IsActive;
            }

            ViewBag.InventoryName = BALFactory.dropdownsBAL.GetInventoryType();
            ViewBag.Item = BALFactory.dropdownsBAL.GetItemMaster(model.InventoryTypeID);
            ViewBag.Supplier = BALFactory.dropdownsBAL.GetSupplierName();
            return View(model);
        }

        public ActionResult PurchaseRecord(int? PurchaseRecordID = 0, int? PurchaseRequestID = 0)
        {
            PurchaseModel model = new PurchaseModel();

            model.PurchaseRequestID = (byte)PurchaseRequestID;
            // model.TickIf = false;
            if (PurchaseRequestID != 0)
            {
                if (PurchaseRecordID != 0)
                {
                    CoreFactory.purchaseEntity = BALFactory.PurchaseBAL.GetPurchaseRecordDetails((Int32)PurchaseRecordID);
                    if (CoreFactory.purchaseEntity != null)
                    {
                        model.PurchaseRecordID = CoreFactory.purchaseEntity.PurchaseRecordID;
                        model.BrandReceived = CoreFactory.purchaseEntity.BrandReceived;
                        model.QtyReceived = CoreFactory.purchaseEntity.QtyReceived;
                        model.ReceivedDate = CoreFactory.purchaseEntity.ReceivedDate;
                        model.RemarkRecord = CoreFactory.purchaseEntity.RemarkRecord;
                        model.GradeReceived = CoreFactory.purchaseEntity.GradeReceived;
                        model.DeliveryTime = CoreFactory.purchaseEntity.DeliveryTime;
                        model.DeliveryChallanNo = CoreFactory.purchaseEntity.DeliveryChallanNo;
                        model.DeliveryChallanDate = CoreFactory.purchaseEntity.DeliveryChallanDate;
                        model.BillNo = CoreFactory.purchaseEntity.BillNo;
                        model.BillDate = CoreFactory.purchaseEntity.BillDate;
                        model.ConditionOfPackaging = CoreFactory.purchaseEntity.ConditionOfPackaging;
                        model.IntegrityOfPackaging = CoreFactory.purchaseEntity.IntegrityOfPackaging;
                        model.CurrentStatus = CoreFactory.purchaseEntity.CurrentStatus;
                        model.IsActive = CoreFactory.purchaseEntity.IsActive;
                    }
                }

                CoreFactory.purchaseEntity = BALFactory.PurchaseBAL.GetPurchaseRequestDetails2((Int32)PurchaseRequestID);
                if (CoreFactory.purchaseEntity != null)
                {
                    model.PurchaseRequestID = CoreFactory.purchaseEntity.PurchaseRequestID;
                    model.PurchaseOrderNo = CoreFactory.purchaseEntity.PurchaseOrderNo;
                    model.Quantity = CoreFactory.purchaseEntity.Quantity;
                    model.Grade = CoreFactory.purchaseEntity.Grade;
                    model.Brand = CoreFactory.purchaseEntity.Brand;
                    model.Specification = CoreFactory.purchaseEntity.Specification;
                    model.SupplierName = CoreFactory.purchaseEntity.SupplierName;
                    model.IsActive = CoreFactory.purchaseEntity.IsActive;
                }
            }
            return View(model);

        }

        public ActionResult PurchasSupplierForm(int? PurchaseSupplierID = 0)
        {
            PurchaseModel model = new PurchaseModel();
            if (PurchaseSupplierID != 0)
            {
                CoreFactory.purchaseEntity = BALFactory.PurchaseBAL.GetPurchaseSupplierDetails((Int32)PurchaseSupplierID);
                if (CoreFactory.purchaseEntity != null)
                {
                    model.PurchaseSupplierID = CoreFactory.purchaseEntity.PurchaseSupplierID;
                    model.SupplierName = CoreFactory.purchaseEntity.SupplierName;
                    model.SupplierContactPerson = CoreFactory.purchaseEntity.SupplierContactPerson;
                    model.SupplierContactNo = CoreFactory.purchaseEntity.SupplierContactNo;
                    model.SupplierAddress = CoreFactory.purchaseEntity.SupplierAddress;
                    model.Product = CoreFactory.purchaseEntity.Product;
                    model.ApprovedBy = CoreFactory.purchaseEntity.ApprovedBy;
                    model.Approved = (bool)CoreFactory.purchaseEntity.IsApproved;

                    model.DateOfApproval = CoreFactory.purchaseEntity.DateOfApproval;
                    model.Remark = CoreFactory.purchaseEntity.Remark;
                    model.IsActive = CoreFactory.purchaseEntity.IsActive;
                }
            }
            return View(model);
        }

        //--------------------------------------Lists------------------------------------------------
        public ActionResult PurchaseRequestList()
        {
            CoreFactory.purchaseRequestList = BALFactory.PurchaseBAL.GetPurchaseRequestList();
            List<PurchaseModel> modelList = new List<PurchaseModel>();
            foreach (var Item in CoreFactory.purchaseRequestList)
            {
                modelList.Add(new PurchaseModel()
                {
                    PurchaseRequestID = Item.PurchaseRequestID,
                    PurchaseMasterID = Item.PurchaseMasterID,
                    ItemMasterId = Item.ItemMasterId,
                    Item = Item.Item,
                    SupplierName = Item.SupplierName,
                    Brand = Item.Brand,
                    Grade = Item.Grade,
                    Rate = Item.Rate,
                    RemarkIfRejectedByTM = Item.TMStatus,
                    RemarkIfRejectedByHO = Item.HOStatus,
                    PackSize = Item.PackSize,
                    Quantity = Item.Quantity,
                    NetAmount = Item.NetAmount,
                    Priority = Item.Priority,
                    Purpose = Item.Purpose,
                    Specification = Item.Specification,
                    Remark = Item.Remark,
                    TickIf = Item.TickIf,
                    EstimatedLagTime = Item.EstimatedLagTime,
                    COA = Item.COA,
                    //DisposedDay = Item.DisposedDay,
                    //SampleTypeProductCode = Item.SampleTypeProductCode,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }
        public ActionResult TechnicalManagerList()
        {
            IList<Core.Inventory.PurchaseEntity> purchaseEntities = BALFactory.PurchaseBAL.GetTML();
            return View(purchaseEntities);
        }
        public ActionResult PurchaseOrderList()
        {
            IList<Core.Inventory.PurchaseEntity> purchaseEntities = BALFactory.PurchaseBAL.GetPOList();
            return View(purchaseEntities);
        }
        public ActionResult PurchaseRecordFirstList()
        {
            IList<Core.Inventory.PurchaseEntity> purchaseEntities = BALFactory.PurchaseBAL.GetPurchaseRecordFirstList();

            return View(purchaseEntities);
        }
        public ActionResult PurchaseInchargeFillList()
        {
            IList<Core.Inventory.PurchaseEntity> purchaseEntities = BALFactory.PurchaseBAL.GetPIL();
            return View(purchaseEntities);
        }
        public ActionResult PurchaseInchargeList()
        {
            IList<Core.Inventory.PurchaseEntity> purchaseEntities = BALFactory.PurchaseBAL.GetInchargeList();
            return View(purchaseEntities);
        }
        public ActionResult PurchaseRecordList()
        {
            CoreFactory.purchaseRequestList = BALFactory.PurchaseBAL.GetPurchaseRecordList();
            List<PurchaseModel> modelList = new List<PurchaseModel>();
            foreach (var Item in CoreFactory.purchaseRequestList)
            {
                modelList.Add(new PurchaseModel()
                {
                    PurchaseRecordID = Item.PurchaseRecordID,
                    PurchaseRequestID = Item.PurchaseRequestID,
                    SupplierName = Item.SupplierName,
                    PurchaseOrderNo = Item.PurchaseOrderNo,
                    Specification = Item.Specification,
                    Quantity = Item.Quantity,
                    QtyReceived = Item.QtyReceived,
                    Brand = Item.Brand,
                    Item = Item.Item,
                    BrandReceived = Item.BrandReceived,
                    Grade = Item.Grade,
                    GradeReceived = Item.GradeReceived,
                    DeliveryTime = Item.DeliveryTime,
                    DeliveryChallanDate = Item.DeliveryChallanDate,
                    DeliveryChallanNo = Item.DeliveryChallanNo,
                    ConditionOfPackaging = Item.ConditionOfPackaging,
                    IntegrityOfPackaging = Item.IntegrityOfPackaging,
                    BillDate = Item.BillDate,
                    BillNo = Item.BillNo,
                    RemarkRecord = Item.RemarkRecord,
                    ReceivedDate = Item.ReceivedDate,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }
        public ActionResult PurchaseRecordSecondList(int PurchaseMasterID, int InventoryTypeID, string PurchaseOrderNo)
        {
            IList<Core.Inventory.PurchaseEntity> purchaseEntities = BALFactory.PurchaseBAL.GetPurchaseRecordSecondList(PurchaseMasterID, InventoryTypeID, PurchaseOrderNo);
            return View(purchaseEntities);
        }
        public ActionResult PurchaseSupplierList()
        {
            CoreFactory.purchaseRequestList = BALFactory.PurchaseBAL.GetPurchaseSupplierList();
            List<PurchaseModel> modelList = new List<PurchaseModel>();
            foreach (var Item in CoreFactory.purchaseRequestList)
            {
                modelList.Add(new PurchaseModel()
                {
                    PurchaseSupplierID = Item.PurchaseSupplierID,
                    SupplierName = Item.SupplierName,
                    SupplierContactPerson = Item.SupplierContactPerson,
                    SupplierContactNo = Item.SupplierContactNo,
                    SupplierAddress = Item.SupplierAddress,
                    Product = Item.Product,
                    ApprovedBy = Item.ApprovedBy,
                    DateOfApproval = Item.DateOfApproval,
                    Approved = Item.IsApproved,
                    Remark = Item.Remark,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }


        //--------------------------------------post Functions------------------------------------------------
        [HttpPost]
        public JsonResult PurchaseRequestForm(PurchaseModel model, string Save)
        {
            CoreFactory.purchaseEntity = new PurchaseEntity();
            if (model.PurchaseMasterID == 0)
            {
                CoreFactory.purchaseEntity.PurchaseMasterID = model.PurchaseMasterID;
                //CoreFactory.purchaseEntity.EnteredBy = LIMS.User.UserMasterID;
                CoreFactory.purchaseEntity.EnteredDate = DateTime.Now;
                CoreFactory.purchaseEntity.IsActive = true;
                long PurchaseMasterID = BALFactory.PurchaseBAL.AddPurchaseMasterDetails(CoreFactory.purchaseEntity);
                model.PurchaseMasterID = (Int16)PurchaseMasterID;
            }
            var purchaseList = (List<PurchaseModel>)TempData.Peek("PurchaseList");
            if (Save == "PurchaseSubmit")
            {

                //int iCount=1;
                foreach (var purchase in purchaseList)
                {

                    CoreFactory.purchaseEntity.PurchaseRequestID = model.PurchaseRequestID;
                    CoreFactory.purchaseEntity.PurchaseMasterID = model.PurchaseMasterID;
                    CoreFactory.purchaseEntity.ItemMasterId = purchase.ItemMasterId;
                    CoreFactory.purchaseEntity.InventoryTypeID = purchase.InventoryTypeID;
                    CoreFactory.purchaseEntity.Priority = purchase.Priority;
                    CoreFactory.purchaseEntity.Specification = purchase.Specification;
                    CoreFactory.purchaseEntity.Brand = purchase.Brand;
                    CoreFactory.purchaseEntity.FileName = purchase.FileName;
                    CoreFactory.purchaseEntity.PurchaseUpload = purchase.PurchaseUpload;
                    CoreFactory.purchaseEntity.Grade = purchase.Grade;
                    CoreFactory.purchaseEntity.Remark = purchase.Remark;
                    CoreFactory.purchaseEntity.CategoryName = purchase.CatagoryName;
                    CoreFactory.purchaseEntity.Purpose = purchase.Purpose;

                    if (purchase.PurchaseUpload == null)
                    {
                        CoreFactory.purchaseEntity.PurchaseUpload = "";
                        CoreFactory.purchaseEntity.FileName = "";
                    }
                    else
                    {
                        CoreFactory.purchaseEntity.PurchaseUpload = purchase.PurchaseUpload;
                        CoreFactory.purchaseEntity.FileName = purchase.FileName;
                    }

                    CoreFactory.purchaseEntity.COA = purchase.COA;
                    CoreFactory.purchaseEntity.PackSize = purchase.PackSize;
                    CoreFactory.purchaseEntity.Quantity = purchase.Quantity;
                    //CoreFactory.purchaseEntity.EnteredBy = LIMS.User.UserMasterID;
                    CoreFactory.purchaseEntity.EnteredDate = DateTime.Now;
                    CoreFactory.purchaseEntity.IsActive = true;

                    var PurchaseRequestID = BALFactory.PurchaseBAL.AddPurchaseDetails(CoreFactory.purchaseEntity);
                    model.PurchaseRequestID = (Int32)PurchaseRequestID;

                    int InventoryTypeID = 0;
                    InventoryTypeID = (Int32)BALFactory.PurchaseBAL.GetInventoryTypeID((Int32)PurchaseRequestID);
                    var purchaseTermsList = (List<TermsAndConditionPurchaseModel>)TempData.Peek("PurchaseTermsList");
                    var purchaseListTemp = purchaseTermsList.Where(a => a.Count == purchase.SrNo);
                    foreach (var terms in purchaseListTemp)
                    {
                        CoreFactory.purchaseEntity.PurchaseRequestID = model.PurchaseRequestID;
                        CoreFactory.purchaseEntity.PurchaseMasterID = model.PurchaseMasterID;
                        CoreFactory.purchaseEntity.TermAndCondt = terms.TermAndCondt;
                        CoreFactory.purchaseEntity.TermAndCondtId = terms.TermAndCondtId;
                        CoreFactory.purchaseEntity.InventoryTypeID = terms.InventoryTypeID;
                        BALFactory.PurchaseBAL.AddPurchaseTermsAndCondition(model.PurchaseRequestID, model.PurchaseMasterID, InventoryTypeID, terms.TermAndCondtId);

                    }

                }
            }
            else
            {
                foreach (var purchase in purchaseList)
                {

                    CoreFactory.purchaseEntity.PurchaseRequestID = model.PurchaseRequestID;
                    CoreFactory.purchaseEntity.PurchaseMasterID = model.PurchaseMasterID;
                    CoreFactory.purchaseEntity.ItemMasterId = purchase.ItemMasterId;
                    CoreFactory.purchaseEntity.InventoryTypeID = purchase.InventoryTypeID;
                    CoreFactory.purchaseEntity.Priority = purchase.Priority;
                    CoreFactory.purchaseEntity.Specification = purchase.Specification;
                    CoreFactory.purchaseEntity.Brand = purchase.Brand;
                    CoreFactory.purchaseEntity.FileName = purchase.FileName;
                    CoreFactory.purchaseEntity.PurchaseUpload = purchase.PurchaseUpload;
                    CoreFactory.purchaseEntity.Grade = purchase.Grade;
                    CoreFactory.purchaseEntity.Remark = purchase.Remark;
                    CoreFactory.purchaseEntity.CategoryName = purchase.CatagoryName;
                    CoreFactory.purchaseEntity.Purpose = purchase.Purpose;



                    if (purchase.PurchaseUpload == null)
                    {
                        CoreFactory.purchaseEntity.PurchaseUpload = "";
                        CoreFactory.purchaseEntity.FileName = "";
                    }
                    else
                    {
                        CoreFactory.purchaseEntity.PurchaseUpload = purchase.PurchaseUpload;
                        CoreFactory.purchaseEntity.FileName = purchase.FileName;
                    }

                    CoreFactory.purchaseEntity.COA = purchase.COA;
                    CoreFactory.purchaseEntity.PackSize = purchase.PackSize;
                    CoreFactory.purchaseEntity.Quantity = purchase.Quantity;
                    //CoreFactory.purchaseEntity.EnteredBy = LIMS.User.UserMasterID;
                    CoreFactory.purchaseEntity.EnteredDate = DateTime.Now;
                    CoreFactory.purchaseEntity.IsActive = true;

                    CoreFactory.purchaseEntity.Approvests = null;
                    CoreFactory.purchaseEntity.PurchaseSupplierID2 = null;
                    long PurchaseRequestID = BALFactory.PurchaseBAL.UpdatePurchaseDetails(CoreFactory.purchaseEntity);
                    model.PurchaseRequestID = (Int32)PurchaseRequestID;

                    int InventoryTypeID = 0;
                    InventoryTypeID = (Int32)BALFactory.PurchaseBAL.GetInventoryTypeID((Int32)PurchaseRequestID);
                    var purchaseTermsList = (List<TermsAndConditionPurchaseModel>)TempData.Peek("PurchaseTermsList");
                    var purchaseListTemp = purchaseTermsList.Where(a => a.Count == purchase.SrNo);
                    foreach (var terms in purchaseListTemp)
                    {
                        CoreFactory.purchaseEntity.PurchaseRequestID = model.PurchaseRequestID;
                        CoreFactory.purchaseEntity.PurchaseMasterID = model.PurchaseMasterID;
                        CoreFactory.purchaseEntity.TermAndCondt = terms.TermAndCondt;
                        CoreFactory.purchaseEntity.TermAndCondtId = terms.TermAndCondtId;
                        CoreFactory.purchaseEntity.InventoryTypeID = terms.InventoryTypeID;
                        BALFactory.PurchaseBAL.UpdatePurchaseTermsAndCondition(model.PurchaseRequestID, model.PurchaseMasterID, InventoryTypeID, terms.TermAndCondtId);

                    }
                }
            }
            TempData["PurchaseTermsList"] = null;
            TempData["PurchaseList"] = null;
            return Json(new { Status = "success", message = "Created successfully." }, JsonRequestBehavior.AllowGet);





        }

        [HttpPost]
        public JsonResult _PurchaseRequestFormList1(HttpPostedFileBase file, PurchaseModel model, TermsAndConditionPurchaseModel model1)
        {
            List<PurchaseModel> purchaseList = new List<PurchaseModel>();
            List<TermsAndConditionPurchaseModel> purchaseTermsList = new List<TermsAndConditionPurchaseModel>();

            if (file != null)
            {
                string filename = Path.GetFileName(file.FileName);
                string guid = Convert.ToString(model.PurchaseRequestID) + "_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyy")) + "_";
                string storepath = "/Uploads/PurchaseOrders/" + guid + filename.Replace(" ", "") + "";
                if (!Directory.Exists(Server.MapPath("/Uploads/PurchaseOrders")))
                {
                    Directory.CreateDirectory(Server.MapPath("/Uploads/PurchaseOrders"));
                }
                string targetPath = Server.MapPath("~/Uploads/PurchaseOrders/") + guid + filename.Replace(" ", "") + "";
                file.SaveAs(targetPath);
                model.PurchaseUpload = targetPath;
                model.FileName = filename;
            }
            if (TempData["PurchaseList"] != null)
            {
                purchaseList = (List<PurchaseModel>)TempData["PurchaseList"];
            }
            if (model.SrNo == 0)
            {
                model.SrNo = purchaseList.Count() + 1;
                purchaseList.Add(model);
            }
            else
            {
                var purchase = purchaseList.Where(c => c.SrNo == model.SrNo).FirstOrDefault();
                purchase.PurchaseRequestID = model.PurchaseRequestID;
                purchase.PurchaseSupplierID = model.PurchaseSupplierID;
                purchase.Item = model.Item;
                purchase.PurchaseUpload = model.PurchaseUpload;
                purchase.FileName = model.FileName;
                purchase.SupplierName = model.SupplierName;
                purchase.ItemMasterId = model.ItemMasterId;
                purchase.CatagoryName = model.CatagoryName;
                purchase.InventoryTypeID = model.InventoryTypeID;
                purchase.Priority = model.Priority;
                purchase.Specification = model.Specification;
                purchase.Brand = model.Brand;
                purchase.Grade = model.Grade;
                purchase.Remark = model.Remark;
                purchase.Purpose = model.Purpose;
                purchase.COA = model.COA;
                purchase.PackSize = model.PackSize;
                purchase.Quantity = model.Quantity;
            }

            if (TempData["PurchaseTermsList"] != null)
            {
                purchaseTermsList = (List<TermsAndConditionPurchaseModel>)TempData["PurchaseTermsList"];
                purchaseTermsList.RemoveAll(r => r.Count == model.SrNo);
            }


            foreach (var term in model.TermsList)
            {

                if (term.IsSelected == true)
                {
                    purchaseTermsList.Add(new TermsAndConditionPurchaseModel()
                    {
                        PurchaseMasterID = model.PurchaseMasterID,
                        PurchaseRequestID = term.PurchaseRequestID,
                        InventoryTypeID = term.InventoryTypeID,
                        TermAndCondtId = term.TermAndCondtId,
                        Count = model.SrNo

                    });
                }
            }
            TempData["PurchaseList"] = purchaseList;
            TempData["PurchaseTermsList"] = purchaseTermsList;
            return Json(new { status = "success", message = "Added" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PurchaseRecord(PurchaseModel model)
        {
            CoreFactory.purchaseEntity = new PurchaseEntity();
            CoreFactory.purchaseEntity.PurchaseRecordID = model.PurchaseRecordID;
            CoreFactory.purchaseEntity.PurchaseRequestID = model.PurchaseRequestID;
            CoreFactory.purchaseEntity.QtyReceived = model.QtyReceived;
            CoreFactory.purchaseEntity.BrandReceived = model.BrandReceived;
            CoreFactory.purchaseEntity.GradeReceived = model.GradeReceived;
            CoreFactory.purchaseEntity.DeliveryChallanDate = model.DeliveryChallanDate;
            CoreFactory.purchaseEntity.DeliveryChallanNo = model.DeliveryChallanNo;
            CoreFactory.purchaseEntity.DeliveryTime = model.DeliveryTime;
            CoreFactory.purchaseEntity.BillDate = model.BillDate;
            CoreFactory.purchaseEntity.BillNo = model.BillNo;
            CoreFactory.purchaseEntity.RemarkRecord = model.RemarkRecord;
            CoreFactory.purchaseEntity.ReceivedDate = model.ReceivedDate;
            CoreFactory.purchaseEntity.ConditionOfPackaging = model.ConditionOfPackaging;
            CoreFactory.purchaseEntity.IntegrityOfPackaging = model.IntegrityOfPackaging;
            CoreFactory.purchaseEntity.CurrentStatus = model.CurrentStatus;
            CoreFactory.purchaseEntity.EnteredBy = LIMS.User.UserMasterID;
            CoreFactory.purchaseEntity.EnteredDate = DateTime.Now;
            CoreFactory.purchaseEntity.IsActive = true;

            if (model.PurchaseRecordID == 0)
            {
                strStatus = BALFactory.PurchaseBAL.AddPurchaseRecordDetails(CoreFactory.purchaseEntity);
                return Json(new { Status = strStatus, message = "Created successfully." }, JsonRequestBehavior.AllowGet);
            }

            //Use for update
            else
            {
                strStatus = BALFactory.PurchaseBAL.UpdatePurchaseRecord(CoreFactory.purchaseEntity);
                return Json(new { Status = strStatus, message = "Updated successfully." }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult PurchasSupplierForm(PurchaseModel model)
        {
            CoreFactory.purchaseEntity = new PurchaseEntity();
            CoreFactory.purchaseEntity.PurchaseSupplierID = model.PurchaseSupplierID;
            CoreFactory.purchaseEntity.SupplierName = model.SupplierName;
            CoreFactory.purchaseEntity.SupplierContactPerson = model.SupplierContactPerson;
            CoreFactory.purchaseEntity.SupplierContactNo = model.SupplierContactNo;
            CoreFactory.purchaseEntity.IsApproved = model.Approved;
            CoreFactory.purchaseEntity.SupplierAddress = model.SupplierAddress;
            CoreFactory.purchaseEntity.Product = model.Product;
            CoreFactory.purchaseEntity.Remark = model.Remark;
            CoreFactory.purchaseEntity.ApprovedBy = model.ApprovedBy;
            CoreFactory.purchaseEntity.DateOfApproval = model.DateOfApproval;
            CoreFactory.purchaseEntity.EnteredBy = LIMS.User.UserMasterID;
            CoreFactory.purchaseEntity.EnteredDate = DateTime.Now;
            CoreFactory.purchaseEntity.IsActive = true;

            if (model.PurchaseSupplierID == 0)
            {
                strStatus = BALFactory.PurchaseBAL.AddPurchaseSupplierDetails(CoreFactory.purchaseEntity);
                return Json(new { Status = strStatus, message = "Created successfully." }, JsonRequestBehavior.AllowGet);
            }

            //Use for update
            else
            {
                strStatus = BALFactory.PurchaseBAL.UpdatePurchaseSupplier(CoreFactory.purchaseEntity);
                return Json(new { Status = strStatus, message = "Updated successfully." }, JsonRequestBehavior.AllowGet);
            }




        }
        //--------------------------------------Other Functions------------------------------------------------
        public JsonResult _DeletePurchaseRequest(int? Id = 0)
        {
            var purchaseList = (List<PurchaseModel>)TempData.Peek("PurchaseList");
            var purchase = purchaseList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            if (purchase.PurchaseRequestID != 0)
            {
                BALFactory.PurchaseBAL.DeleteContract(purchase.PurchaseRequestID);
            }
            purchaseList.Remove(purchase);
            int i = 1;
            foreach (var item in purchaseList)
            {
                item.SrNo = i;
                i++;
            }
            TempData["PurchaseList"] = purchaseList;
            return Json(new { status = "success", message = "deleted" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ItemMaster(int? InventoryTypeID = 0)
        {
            PurchaseModel model = new PurchaseModel();
            var ItemMasterList = BALFactory.dropdownsBAL.GetItemMaster((Int32)InventoryTypeID);
            //model.TermsList = new List<TermsAndConditionPurchaseModel>();
            //var TermsCond = BALFactory.addItemBAL.GetTermsAndConditionPurchase((Int32)InventoryTypeID);
            //foreach (var t in TermsCond)
            //{
            //    model.TermsList.Add(new TermsAndConditionPurchaseModel()
            //    {
            //        TermAndCondtId = t.TermAndCondtId,
            //        TermAndCondt = t.TermAndCondt,
            //        IsSelected = t.IsSelected
            //    });
            //}
            return Json(new { result = ItemMasterList }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult TermsMaster(int? InventoryTypeID = 0, int? PurchaseRequestID = 0)
        {
            PurchaseModel model = new PurchaseModel();
            model.TermsList = new List<TermsAndConditionPurchaseModel>();
            if (PurchaseRequestID == 0)
            {
                var TermsCond = BALFactory.PurchaseBAL.GetTermsAndConditionPurchase((Int32)InventoryTypeID);

                List<TermsAndConditionPurchaseModel> purchaseTermsList = new List<TermsAndConditionPurchaseModel>();
                if (TempData["PurchaseTermsList"] != null)
                {
                    purchaseTermsList = (List<TermsAndConditionPurchaseModel>)TempData["PurchaseTermsList"];
                }

                TempData.Keep("PurchaseTermsList");
                foreach (var t in TermsCond)
                {
                    //bool isSelected = false;
                    //var TempTermAndCondtId = purchaseTermsList.Where(a => a.TermAndCondtId == t.TermAndCondtId).SingleOrDefault();
                    //if (TempTermAndCondtId != null)
                    //{
                    //    if (TempTermAndCondtId.TermAndCondtId > 0)
                    //    {
                    //        isSelected = true;
                    //    }
                    //}
                    model.TermsList.Add(new TermsAndConditionPurchaseModel()
                    {
                        TermAndCondtId = t.TermAndCondtId,
                        TermAndCondt = t.TermAndCondt,
                        IsSelected = t.IsSelected
                    });
                }
            }
            else
            {
                var countTNC = BALFactory.PurchaseBAL.GetPurchaseTNCDetails((Int32)PurchaseRequestID);

                for (int i = 0; i < countTNC.Count; i++)
                {

                    model.TermsList.Add(new TermsAndConditionPurchaseModel()
                    {
                        TermAndCondtId = countTNC[i].TermAndCondtId,
                        TermAndCondt = countTNC[i].TermAndCondt,

                    });

                }
            }
            return PartialView(model);



        }
        public string UpdateStatus(string model)
        {
            PurchaseEntity purchaseEntity = JsonConvert.DeserializeObject<PurchaseEntity>(model);
            var status = BALFactory.PurchaseBAL.UpdateStatus(purchaseEntity);
            string Msg = "Purchase Requisitions Received";//Notification on purchase requisitions received for approval
            //long NotificationDetailId = BALFactory.PurchaseBAL.AddNotification(Msg, "HOD", purchaseEntity);

            return "Commented";
        }
        public string UpdatePRD(string model)
        {

            PurchaseEntity purchaseEntity = JsonConvert.DeserializeObject<PurchaseEntity>(model);
            //int PurchaseId = 0;
            if (purchaseEntity.TickIf != null || purchaseEntity.TickIf == true)
            {
                CoreFactory.purchaseEntity.TickIf = purchaseEntity.TickIf;
            }
            else
            {
                CoreFactory.purchaseEntity.TickIf = false;
            }
            var status = BALFactory.PurchaseBAL.UpdatePRD(purchaseEntity);

            return "Commented";
        }
        public string UpdateComment(string model)
        {

            PurchaseEntity purchaseEntity = JsonConvert.DeserializeObject<PurchaseEntity>(model);
            int PurchaseId = 0;


            if (purchaseEntity.CategoryName.Trim() == "Glassware")
            {

                var ponumber = BALFactory.PurchaseBAL.GetPONumberGlass(purchaseEntity.PurchaseMasterID, purchaseEntity.InventoryTypeID);
                if (ponumber != string.Empty)
                {
                    if (ponumber != "" || ponumber != null)
                    {
                        purchaseEntity.PurchaseOrderNo = ponumber;
                    }
                }
                else
                {
                    purchaseEntity.PurchaseOrderNo = BALFactory.PurchaseBAL.GeneratePONumber(purchaseEntity.PurchaseRequestID);

                }
                var status = BALFactory.PurchaseBAL.UpdateComment(purchaseEntity);
            }
            else
            {
                var ponumber = BALFactory.PurchaseBAL.GetPONumber(purchaseEntity.PurchaseMasterID, purchaseEntity.InventoryTypeID);
                if (ponumber != string.Empty)
                {
                    if (ponumber != "" || ponumber != null)
                    {
                        purchaseEntity.PurchaseOrderNo = ponumber;
                    }
                }
                else
                {

                    //PurchaseId = (Int32)BALFactory.addItemBAL.GetPurchaseRequestID();
                    purchaseEntity.PurchaseOrderNo = BALFactory.PurchaseBAL.GeneratePONumber(purchaseEntity.PurchaseRequestID);
                    //"MEEPL/LAB" + DateTime.Now.ToString("yyyyMM") + "/" + (PurchaseId + 1).ToString();
                }
                var status = BALFactory.PurchaseBAL.UpdateComment(purchaseEntity);
            }


            string Msg = "Purchase Requisitions Processed";//Notification on purchase requisitions Processed
            //long NotificationDetailId = BALFactory.PurchaseBAL.AddNotification(Msg, "Purchase Incharge", purchaseEntity);
            return "Commented";
        }
    }
}