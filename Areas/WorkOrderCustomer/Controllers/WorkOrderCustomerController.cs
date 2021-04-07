using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using LIMS_DEMO.Areas.WorkOrderCustomer.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.WorkOrderCustomer;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.WorkOrderCustomer;
using LIMS_DEMO.BAL.DropDown;
using System.Configuration;

namespace LIMS_DEMO.Areas.WorkOrderCustomer.Controllers
{
    [RouteArea("WorkOrderCustomer")]
    public class WorkOrderCustomerController : Controller
    {
        string strStatus = "";
        readonly decimal GSTRate = Convert.ToDecimal(ConfigurationManager.AppSettings["GSTRate"]);
        Common.ErrorLogging errorLogging = new Common.ErrorLogging();

        // GET: WorkOrderCustomer/WorkOrderCustomer
        public WorkOrderCustomerController()
        {
            BALFactory.dropdownsBAL = new DropDownBal();
            BALFactory.workordercustomerBAL = new WorkOrderCustomerBAL();
            BALFactory.sampleParameterBAL = new BAL.Enquiry.SampleParameterBAL();
            BALFactory.costingBAL = new BAL.Enquiry.CostingBAL();
            BALFactory.workOrderBAL = new BAL.Enquiry.WorkOrderBAL();
        }
        public ActionResult Index(DateTime? FromDate, DateTime? ToDate)
        {
            if (ToDate == null)
            {
                ToDate = FromDate;
            }
            else if (FromDate == null)
            {
                FromDate = ToDate;
            }
            CoreFactory.WorkOrderCustomerList = BALFactory.workordercustomerBAL.GetWorkOrderCustomerList(LIMS.User.LabId, FromDate, ToDate);
            List<WorkOrderCustomerModel> modelList = new List<WorkOrderCustomerModel>();

            foreach (var Item in CoreFactory.WorkOrderCustomerList)
            {
                modelList.Add(new WorkOrderCustomerModel()
                {
                    WorkOrderId = Item.WorkOrderId,
                    WorkOrderNo = Item.WorkOrderNo,
                    WorkOrderReceivedDate = Item.WORecieveDate,
                    WORecvDate = Convert.ToDateTime(Item.WORecieveDate).ToString("dd/MM/yyyy"),
                    WorkOrderEndDate = Item.WOEndDate,
                    WOEDate = Convert.ToDateTime(Item.WOEndDate).ToString("dd/MM/yyyy"),
                    CustomerMasterId = Item.CustomerMasterId,
                    CustomerName = Item.CustomerName,
                    CommunicationMode = Item.CommunicationMode,
                    AssignToId = Item.AssignToId,
                    Remarks = Item.Remark,
                    CurrentStatus = Item.CurrentStatus,
                    WOUpload = Item.WOUpload,
                    FileName = Item.FileName,
                    IsIGST = Item.IsIGST == null ? false : (bool)Item.IsIGST,
                    EnteredBy = Item.EnteredBy,
                });
            }

            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;
            string UserName = LIMS.User.UserName;
            var RoleNameVar = BALFactory.workOrderBAL.GetUserRoles(UserName);//For Role HOD/BDM
            if (RoleNameVar.RoleName != null)
            {
                ViewBag.RoleName = RoleNameVar.RoleName;
            }

            ViewBag.SurveyingLead = BALFactory.dropdownsBAL.GetUserList("Sampling Incharge", LIMS.User.LabId);//For STL
            return View(modelList);
        }
        public ActionResult HODWorkOrderDirectList(DateTime? FromDate, DateTime? ToDate)
        {
            if (ToDate == null)
            {
                ToDate = FromDate;
            }
            else if (FromDate == null)
            {
                FromDate = ToDate;
            }
            CoreFactory.WorkOrderCustomerList = BALFactory.workordercustomerBAL.GetWorkOrderCustomerList(LIMS.User.LabId, FromDate, ToDate);
            List<WorkOrderCustomerModel> modelList = new List<WorkOrderCustomerModel>();

            foreach (var Item in CoreFactory.WorkOrderCustomerList)
            {
                modelList.Add(new WorkOrderCustomerModel()
                {
                    WorkOrderId = Item.WorkOrderId,
                    WorkOrderNo = Item.WorkOrderNo,
                    WorkOrderReceivedDate = Item.WORecieveDate,
                    WORecvDate = Convert.ToDateTime(Item.WORecieveDate).ToString("dd/MM/yyyy"),
                    WorkOrderEndDate = Item.WOEndDate,
                    WOEDate = Convert.ToDateTime(Item.WOEndDate).ToString("dd/MM/yyyy"),
                    CustomerMasterId = Item.CustomerMasterId,
                    CustomerName = Item.CustomerName,
                    CommunicationMode = Item.CommunicationMode,
                    AssignToId = Item.AssignToId,
                    Remarks = Item.Remark,
                    CurrentStatus = Item.CurrentStatus,
                    WOUpload = Item.WOUpload,
                    FileName = Item.FileName,
                    IsIGST = Item.IsIGST == null ? false : (bool)Item.IsIGST,
                    EnteredBy = Item.EnteredBy,
                });
            }

            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;
            string UserName = LIMS.User.UserName;
            var RoleNameVar = BALFactory.workOrderBAL.GetUserRoles(UserName);//For Role HOD/BDM
            if (RoleNameVar.RoleName != null)
            {
                ViewBag.RoleName = RoleNameVar.RoleName;
            }

            ViewBag.SurveyingLead = BALFactory.dropdownsBAL.GetUserList("Sampling Incharge", LIMS.User.LabId);//For STL
            return View(modelList);
        }
        public ActionResult AddWorkOrderCustomer()
        {
            return View();
        }
        public PartialViewResult _AddCustomerDetails()
        {
            WorkOrderCustomerModel model = new WorkOrderCustomerModel();
            ViewBag.CommunicationMode = BALFactory.dropdownsBAL.GetCommunicationMode();
            ViewBag.Customers = BALFactory.dropdownsBAL.GetCustomers();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _AddCustomerDetails(HttpPostedFileBase file, WorkOrderCustomerModel model)
        {
            if (file != null)
            {
                string filename = Path.GetFileName(file.FileName);
                string guid = Convert.ToString(model.WorkOrderId) + "_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyy")) + "_";
                string storepath = "/Uploads/WorkOrders/" + guid + filename.Replace(" ", "") + "";
                if (!Directory.Exists(Server.MapPath("/Uploads/WorkOrders")))
                {
                    Directory.CreateDirectory(Server.MapPath("/Uploads/WorkOrders"));
                }
                string targetPath = Server.MapPath("~/Uploads/WorkOrders/") + guid + filename.Replace(" ", "") + "";
                file.SaveAs(targetPath);
                model.WorkOrderUpload = targetPath;
                model.FileName = filename;
            }
            CoreFactory.WorkOrderCustomerEntity = new WorkOrderCustomerEntity();
            CoreFactory.WorkOrderCustomerEntity.WorkOrderId = model.WorkOrderId;
            CoreFactory.WorkOrderCustomerEntity.ModeOfCommunicationId = model.ModeOfCommunicationId;
            CoreFactory.WorkOrderCustomerEntity.CustomerMasterId = (Int32)model.CustomerMasterId;
            CoreFactory.WorkOrderCustomerEntity.StatusId = (byte)BALFactory.dropdownsBAL.GetStatusIdByCode(Enum.GetName(typeof(ManageMessageId), ManageMessageId.WOGenerate));

            CoreFactory.WorkOrderCustomerEntity.WORecieveDate = model.WorkOrderReceivedDate;
            CoreFactory.WorkOrderCustomerEntity.WOEndDate = model.WorkOrderEndDate;
            CoreFactory.WorkOrderCustomerEntity.ExpectSampleCollDate = model.SampleCollectionDate;
            CoreFactory.WorkOrderCustomerEntity.Duration = model.Duration;
            if (model.SampleCollectionDate > model.WorkOrderEndDate || model.SampleCollectionDate < model.WorkOrderReceivedDate)
            {
                return Json(new { status = "Fail" }, JsonRequestBehavior.AllowGet);
            }
            CoreFactory.WorkOrderCustomerEntity.WorkOrderNo = model.WorkOrderNo;
            if (model.WorkOrderUpload == null)
            {
                CoreFactory.WorkOrderCustomerEntity.WOUpload = "";
                CoreFactory.WorkOrderCustomerEntity.FileName = "";
            }
            else
            {
                CoreFactory.WorkOrderCustomerEntity.WOUpload = model.WorkOrderUpload;
                CoreFactory.WorkOrderCustomerEntity.FileName = model.FileName;
            }

            CoreFactory.WorkOrderCustomerEntity.LabMasterId = LIMS.User.LabId;//doubt 
            CoreFactory.WorkOrderCustomerEntity.IsActive = true;
            CoreFactory.WorkOrderCustomerEntity.EnteredBy = LIMS.User.UserMasterID;
            if (model.WorkOrderId == 0)
            {
                long WorkOrderId = BALFactory.workordercustomerBAL.Add(CoreFactory.WorkOrderCustomerEntity);
                string Msg = "WorkOrder Received";
                long NotificationDetailId = BALFactory.workordercustomerBAL.AddNotification(Msg, "ADMIN", CoreFactory.WorkOrderCustomerEntity);
                long NotificationDetailId2 = BALFactory.workordercustomerBAL.AddNotification(Msg, "Sampling Incharge", CoreFactory.WorkOrderCustomerEntity);

                return Json(new { WorkOrderId = WorkOrderId, status = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                strStatus = BALFactory.workordercustomerBAL.Update(CoreFactory.WorkOrderCustomerEntity);
                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }


        }
        public PartialViewResult _AddSampleDetails(int? WorkOrderId = 0, int? EnquiryDetailId = 0, int? EnquirySampleID = 0, int? SampleTypeProductId = 0, string SubroupCode = "")
        {
            WorkOrderCustomerListModel model = new WorkOrderCustomerListModel();
            model.WorkOrderCustomer = new WorkOrderCustomerModel();
            model.WorkOrderCustomerList = new List<WorkOrderCustomerModel>();
            model.ParameterList = new List<ParameterModel>();
            model.ParaList = new List<WorkOrderCustomerModel>();
            ViewBag.WorkOrderId = WorkOrderId;
            ViewBag.EnquiryDetailId = EnquiryDetailId;

            if (WorkOrderId != 0)
            {
                CoreFactory.OrderSampleList = BALFactory.workordercustomerBAL.GetContractList((Int32)WorkOrderId);/////For Pg,Sg,Matrix List/////
                int iSrNo = 1;
                foreach (var item in CoreFactory.OrderSampleList)
                {
                    model.WorkOrderCustomerList.Add(new WorkOrderCustomerModel()
                    {
                        SerialNo = iSrNo,
                        WorkOrderId = (int)WorkOrderId,
                        EnquiryDetailId = item.EnquiryDetailId,
                        SampleTypeProductId = (Int32)item.SampleTypeProductId,
                        SampleTypeProductName = item.SampleTypeProductName,
                        SampleTypeProductCode = item.SampleTypeProductCode,
                        //ProductGroupId = item.ProductGroupId,
                        //ProductGroupName = item.ProductGroupName,
                        //SubGroupId = item.SubGroupId,
                        //SubGroupName = item.SubGroupName,
                        //SubGroupCode = item.SubGroupCode,
                        //MatrixId = item.MatrixId,
                        //MatrixName = item.MatrixName
                    });
                    iSrNo++;
                }
            }

            if (WorkOrderId != 0 && EnquiryDetailId != 0 && EnquirySampleID != 0)
            {
                CoreFactory.enquirySampleList = BALFactory.workordercustomerBAL.GetWorkOrderCustomerSampleList((Int32)WorkOrderId);
                int i = 1;
                foreach (var sample in CoreFactory.enquirySampleList)
                {
                    model.ParaList.Add(new WorkOrderCustomerModel()
                    {
                        SerialNo = i,
                        EnquirySampleID = sample.EnquirySampleID,
                        EnquiryDetailId = sample.EnquiryDetailId,
                        SampleName = sample.SampleName,
                        DisplaySampleName = sample.DisplaySampleName,
                        SampleTypeProductName = sample.SampleTypeProductName,
                        SampleTypeProductId = sample.SampleTypeProductId,
                        //ProductGroupName = sample.ProductGroupName,
                        //SubGroupName = sample.SubGroupName,
                        //MatrixName = sample.MatrixName,
                        //CurrentStatus = sample.CurrentStatus,
                        Parameters = BALFactory.sampleParameterBAL.GetSampleParameters((Int32)sample.EnquirySampleID), //sample.Parameters,
                        Cost = sample.Cost,
                        Remarks = BALFactory.sampleParameterBAL.GetParameterRemarks((Int32)sample.EnquirySampleID), //sample.Parameters,,
                    });
                    i++;
                }
            }
            ViewBag.SampleTypeProductList = BALFactory.dropdownsBAL.GetSampleTypeProduct();
            ViewBag.ProductGroupList = BALFactory.dropdownsBAL.GetProductGroup(0);//Passing Data to Viewbag for dropdown           
            ViewBag.SubGroupList = BALFactory.dropdownsBAL.GetSubGroup(0, model.ProductGroupId);//Passing Data to Viewbag for dropdown
            ViewBag.MatrixList = BALFactory.dropdownsBAL.GetMatrix(0, model.ProductGroupId, model.SubGroupId);
            ViewBag.TestMethod = BALFactory.dropdownsBAL.GetTestMethods();//to be removed later 
            ViewBag.Unit = BALFactory.dropdownsBAL.GetUnits();//to be removed later 
            ViewBag.Branch = BALFactory.dropdownsBAL.GetBranches(LIMS.User.LabId);//to be removed later 
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _AddSampleDetails(WorkOrderCustomerListModel model)
        {
            CoreFactory.enquiryParameterList = new List<EnquiryParameterEntity>();
            foreach (var par in model.ParameterList)
            {
                if (par.IsSelected)
                {
                    CoreFactory.enquiryParameterList.Add(new EnquiryParameterEntity()
                    {
                        EnquiryParameterDetailID = par.EnquiryParameterDetailID,
                        EnquirySampleID = model.WorkOrderCustomer.EnquirySampleID,
                        ParameterMasterId = (Int32)par.ParameterMasterId,
                        ParameterMappingId = par.ParameterMappingId,
                        DisciplineId = par.DisciplineId == null ? (byte)0 : (byte)par.DisciplineId,
                        UnitId = par.UnitId == 0 ? 56 : par.UnitId,
                        //Charges = par.Charges == Decimal.Zero ? Decimal.Zero : Decimal.Round((decimal)par.Charges, 1, MidpointRounding.AwayFromZero),
                        TestMethodId = par.TestMethodId,
                        LabMasterId = par.LabMasterId,
                        Remarks = par.Remarks,
                        IsActive = par.IsActive,
                        EnteredBy = LIMS.User.UserMasterID
                    });
                }
            }
            BALFactory.sampleParameterBAL.AddEnquiryParameterDetail(CoreFactory.enquiryParameterList);
            BALFactory.sampleParameterBAL.UpdateEnquirySampleCharges(model.WorkOrderCustomer.EnquirySampleID, model.WorkOrderCustomer.TotalCharges);
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _Parameters(int? EnquiryDetailId = 0, int? EnquirySampleID = 0, int? SampleTypeProductId = 0)
        {
            WorkOrderCustomerListModel model = new WorkOrderCustomerListModel();
            model.WorkOrderCustomer = new WorkOrderCustomerModel();
            model.ParameterList = new List<ParameterModel>();
            model.WorkOrderCustomer.EnquirySampleID = (long)EnquirySampleID;
            model.WorkOrderCustomer.EnquiryDetailId = (long)EnquiryDetailId;
            if (EnquiryDetailId != 0)
            {
                CoreFactory.enquiryParameterList = new List<EnquiryParameterEntity>();
                CoreFactory.enquiryParameterList = BALFactory.sampleParameterBAL.GetSampleParameterList((Int32)EnquiryDetailId, (Int32)EnquirySampleID, (Int32)SampleTypeProductId);

                foreach (var param in CoreFactory.enquiryParameterList)
                {
                    try
                    {
                        model.ParameterList.Add(new ParameterModel()
                        {
                            ParameterMappingId = param.ParameterMappingId,
                            EnquiryParameterDetailID = param.EnquiryParameterDetailID,
                            EnquirySampleID = param.EnquirySampleID,
                            ParameterMasterId = param.ParameterMasterId,
                            ParameterGroupId = param.ParameterGroupId,
                            ParameterName = param.ParameterName,
                            Discipline = param.Discipline,
                            TestMethodId = param.TestMethodId,
                            Remarks = param.Remarks,
                            //UnitId = param.UnitId,
                            LowerLimit = param.LowerLimit,
                            UpperLimit = param.UpperLimit,
                            Charges = param.Charges == Decimal.Zero ? Decimal.Zero : Decimal.Round((decimal)param.Charges, 1, MidpointRounding.AwayFromZero),
                            LabMasterId = param.LabMasterId == null || param.LabMasterId == 0 ? LIMS.User.LabId : param.LabMasterId,
                            IsSelected = param.EnquiryParameterDetailID == null || param.EnquiryParameterDetailID == 0 ? false : true,
                            TestMethods = BALFactory.dropdownsBAL.GetTestMethods((int)param.ParameterGroupId, (int)param.ParameterMasterId),
                            Units = BALFactory.dropdownsBAL.GetUnits((int)param.ParameterGroupId, (int)param.ParameterMasterId)
                        });
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return View(model);
        }
        public JsonResult GetFormula(int ParameterMasterId, int ParameterMappingId, int TestMethodId, int ParameterGroupId, int UnitId)
        {
            try
            {
                Core.Enquiry.EnquiryParameterEntity objParam = new Core.Enquiry.EnquiryParameterEntity();
                objParam.ParameterMasterId = ParameterMasterId;
                objParam.ParameterMappingId = ParameterMappingId;
                objParam.TestMethodId = TestMethodId;
                objParam.ParameterGroupId = ParameterGroupId;
                objParam.UnitId = UnitId;
                //CoreFactory.enquiryParameterList obj = new CoreFactory.enquiryParameterList();
                var valuecheck = BALFactory.sampleParameterBAL.GetFormula(objParam);
                return Json(valuecheck, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                errorLogging.Error(ex);
                return null;
            }
        }

        [HttpPost]
        public JsonResult InsertSampleDetail(WorkOrderCustomerListModel model)
        {
            CoreFactory.OrderSampleEntity = new WorkOrderSampleEntity();
            CoreFactory.OrderSampleEntity.WorkOrderID = model.WorkOrderId;
            CoreFactory.OrderSampleEntity.SampleTypeProductId = model.SampleTypeProductId;
            //CoreFactory.OrderSampleEntity.ProductGroupId = model.ProductGroupId;
            //CoreFactory.OrderSampleEntity.SubGroupId = model.SubGroupId;
            //CoreFactory.OrderSampleEntity.MatrixId = model.MatrixId;
            CoreFactory.OrderSampleEntity.EnteredBy = LIMS.User.UserMasterID;
            if (model.WorkOrderId != 0)
            {
                CoreFactory.OrderSampleEntity = BALFactory.workordercustomerBAL.AddContract(CoreFactory.OrderSampleEntity);
                model.WorkOrderId = (int)CoreFactory.OrderSampleEntity.WorkOrderID;
            }
            return Json(CoreFactory.OrderSampleEntity);
        }
        public JsonResult _DeleteContract(int? EnquiryDetailId = 0)
        {
            if (EnquiryDetailId != 0)
            {
                BALFactory.workordercustomerBAL.DeleteContract((long)EnquiryDetailId);
            }
            return Json(new { status = "success", message = "deleted" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult _DeleteSample(long? EnquirySampleId = 0)
        {
            if (EnquirySampleId != 0)
            {
                BALFactory.workordercustomerBAL.DeleteSample((long)EnquirySampleId, false);
            }
            return Json(new { status = "success", Message = "Sample Deleted" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertSampleNumber(WorkOrderCustomerListModel model)
        {
            CoreFactory.enqurySampleEntity = new EnquirySampleEntity();
            model.WorkOrderCustomer = new WorkOrderCustomerModel();
            CoreFactory.enqurySampleEntity.EnquiryDetailId = model.EnquiryDetailId;
            CoreFactory.enqurySampleEntity.SampleTypeProductId = model.SampleTypeProductId;
            int EnquirySMID = 0;
            var Samplename = BALFactory.sampleParameterBAL.GetWODSampleNumber((Int32)model.WorkOrderId, (Int32)model.EnquiryDetailId, (Int32)model.SampleTypeProductId);
            if (Samplename != null)
            {
                if (Samplename.SampleName != "" || Samplename.DisplaySampleName != "")
                {
                    CoreFactory.enqurySampleEntity.SampleName = Samplename.SampleName;
                    CoreFactory.enqurySampleEntity.DisplaySampleName = Samplename.DisplaySampleName;
                }
            }
            else
            {
                EnquirySMID = (Int32)BALFactory.sampleParameterBAL.GetEnquirySampleID();
                //CoreFactory.enqurySampleEntity.DisplaySampleName = BALFactory.sampleParameterBAL.GenerateDisplaySampleName();
                //CoreFactory.enqurySampleEntity.SampleName = model.SampleTypeProductCode.ToString() + "/" + CoreFactory.enqurySampleEntity.DisplaySampleName;
                var name = BALFactory.sampleParameterBAL.GenerateDisplaySampleName();
                CoreFactory.enqurySampleEntity.SampleName = model.SampleTypeProductCode.ToString() + "/" + name;
                CoreFactory.enqurySampleEntity.DisplaySampleName = model.SampleTypeProductCode.ToString() + BALFactory.sampleParameterBAL.GetSampleCount(Convert.ToInt32(DateTime.Now.Year), Convert.ToInt32(DateTime.Today.Month)) + "/" + name;

            }
            CoreFactory.enqurySampleEntity = BALFactory.sampleParameterBAL.AddEnquirySampleDetail(CoreFactory.enqurySampleEntity);
            CoreFactory.enqurySampleEntity = BALFactory.sampleParameterBAL.GetEnquirySampleDetail((Int32)CoreFactory.enqurySampleEntity.EnquirySampleID);
            //model.EnquiryDetailId = CoreFactory.enqurySampleEntity.EnquiryDetailId;
            model.WorkOrderCustomer.EnquirySampleID = CoreFactory.enqurySampleEntity.EnquirySampleID;
            model.WorkOrderCustomer.SampleName = CoreFactory.enqurySampleEntity.SampleName;
            model.WorkOrderCustomer.DisplaySampleName = CoreFactory.enqurySampleEntity.DisplaySampleName;
            model.WorkOrderCustomer.TotalCharges = CoreFactory.enqurySampleEntity.TotalCharges == null ? 0 : (decimal)CoreFactory.enqurySampleEntity.TotalCharges;
            return Json(new { status = "success", data = model }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult _AddCostingDetails(int? WorkOrderId = 0, int? EnquirySampleId = 0, int? CostingId = 0)
        {
            WorkOrderCustomerListModel model = new WorkOrderCustomerListModel();
            model.WorkOrderCustomer = new WorkOrderCustomerModel();
            model.WorkOrderCustomerList = new List<WorkOrderCustomerModel>();
            model.ParameterList = new List<ParameterModel>();
            model.CostList = new List<WorkOrderCustomerModel>();
            ViewBag.WorkOrderId = WorkOrderId;
            model.WorkOrderCustomer.WorkOrderId = (Int32)WorkOrderId;
            model.WorkOrderCustomer.CostingId = CostingId;

            CoreFactory.enquirySampleList = BALFactory.workordercustomerBAL.GetWorkOrderCustomerSampleList((Int32)WorkOrderId);
            int i = 1;
            foreach (var sample in CoreFactory.enquirySampleList)
            {
                model.WorkOrderCustomerList.Add(new WorkOrderCustomerModel()
                {
                    SerialNo = i,
                    EnquirySampleID = sample.EnquirySampleID,
                    EnquiryDetailId = sample.EnquiryDetailId,
                    SampleName = sample.SampleName,
                    SampleTypeProductName = sample.SampleTypeProductName,
                    DisplaySampleName = sample.DisplaySampleName,
                    //ProductGroupName = sample.ProductGroupName,
                    //SubGroupName = sample.SubGroupName,
                    //MatrixName = sample.MatrixName,
                    Parameters = BALFactory.sampleParameterBAL.GetSampleParameters((Int32)sample.EnquirySampleID), //sample.Parameters,
                    //Cost = sample.Cost
                });
                i++;
            }

            model.TermsList = new List<TermsAndConditionsModel>();
            var TermsCond = BALFactory.workordercustomerBAL.GetWorkOrderCustomerTermsAndCondition((Int32)WorkOrderId);
            foreach (var t in TermsCond)
            {
                model.TermsList.Add(new TermsAndConditionsModel()
                {
                    TermAndCondtId = t.TermAndCondtId,
                    TermAndCondt = t.TermAndCondt,
                    IsSelected = t.IsSelected
                });
            }

            /////Costing Calculations/////
            ViewBag.GSTRate = GSTRate;
            if (EnquirySampleId != 0)
            {
                CoreFactory.costingEntity = BALFactory.costingBAL.GetCosting((Int32)EnquirySampleId, (Int32)CostingId);
                if (CoreFactory.costingEntity != null)
                {
                    model.WorkOrderCustomer.CostingId = CoreFactory.costingEntity.CostingId;
                    model.WorkOrderCustomer.EnquirySampleID = CoreFactory.costingEntity.EnquirySampleID;
                    model.WorkOrderCustomer.TotalCharges = CoreFactory.costingEntity.TotalCharges == Decimal.Zero ? Decimal.Zero : Decimal.Round((decimal)CoreFactory.costingEntity.TotalCharges, 1, MidpointRounding.AwayFromZero);
                    model.WorkOrderCustomer.SampleAmount = CoreFactory.costingEntity.CollectionCharges == Decimal.Zero ? Decimal.Zero : Decimal.Round((decimal)CoreFactory.costingEntity.CollectionCharges, 1, MidpointRounding.AwayFromZero);
                    model.WorkOrderCustomer.TransportationAmount = CoreFactory.costingEntity.TransportCharges == Decimal.Zero ? Decimal.Zero : Decimal.Round((decimal)CoreFactory.costingEntity.TransportCharges, 1, MidpointRounding.AwayFromZero);
                    model.WorkOrderCustomer.TestingCharges = CoreFactory.costingEntity.TestingCharges == Decimal.Zero ? Decimal.Zero : Decimal.Round((decimal)CoreFactory.costingEntity.TestingCharges, 1, MidpointRounding.AwayFromZero);
                    model.WorkOrderCustomer.TotalAmount = CoreFactory.costingEntity.TotalCharges + CoreFactory.costingEntity.CollectionCharges + CoreFactory.costingEntity.TransportCharges - CoreFactory.costingEntity.Discount;

                    if (CoreFactory.costingEntity.CostingId == null || CoreFactory.costingEntity.CostingId == 0)
                    {
                        model.WorkOrderCustomer.GSTCharges = (model.WorkOrderCustomer.TotalAmount * GSTRate) / 100;
                        model.WorkOrderCustomer.NetAmount = model.WorkOrderCustomer.TotalAmount + model.WorkOrderCustomer.GSTCharges;
                    }
                    else
                    {
                        model.WorkOrderCustomer.GSTCharges = CoreFactory.costingEntity.GST;
                        model.WorkOrderCustomer.NetAmount = CoreFactory.costingEntity.UnitPrice;
                    }
                    model.WorkOrderCustomer.DiscountAmount = CoreFactory.costingEntity.Discount;
                }
            }

            CoreFactory.costingList = BALFactory.workordercustomerBAL.GetWorkOrderCustomerCostingList((Int32)WorkOrderId);
            int k = 1;
            foreach (var cost in CoreFactory.costingList)
            {
                model.CostList.Add(new WorkOrderCustomerModel()
                {
                    SerialNo = k,
                    CostingId = cost.CostingId,
                    EnquirySampleID = cost.EnquirySampleID,
                    NetAmount = cost.UnitPrice,
                    SampleName = cost.SampleName,
                    SampleTypeProductName = cost.SampleTypeProductName,
                    DisplaySampleName = cost.DisplaySampleName,
                    //ProductGroupName = cost.ProductGroupName,
                    //SubGroupName = cost.SubGroupName,
                    //MatrixName = cost.MatrixName
                });
                k++;
            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _AddCostingDetails(WorkOrderCustomerListModel model)
        {
            CoreFactory.quotationEntity = new Core.Enquiry.QuotationEntity();
            CoreFactory.quotTermList = new List<Core.Enquiry.QuotationTNCEntity>();
            foreach (var term in model.TermsList)
            {
                if (term.IsSelected)
                {
                    CoreFactory.quotTermList.Add(new Core.Enquiry.QuotationTNCEntity()
                    {
                        WorkOrderID = model.WorkOrderCustomer.WorkOrderId,
                        TermAndCondtId = term.TermAndCondtId,
                        EnteredBy = LIMS.User.UserMasterID
                    });
                }
            }
            BALFactory.costingBAL.AddQuotationTermsAndCondition(CoreFactory.quotTermList, 0, model.WorkOrderCustomer.Remarks);
            return Json(new { status = "success", WorkOrderId = model.WorkOrderCustomer.WorkOrderId }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _Costing(WorkOrderCustomerListModel model)
        {
            CoreFactory.workcostingEntity = new Core.WorkOrderCustomer.WorkOderCostingEntity();
            CoreFactory.workcostingEntity.EnquirySampleID = model.WorkOrderCustomer.getID;
            CoreFactory.workcostingEntity.TotalCharges = model.WorkOrderCustomer.TotalCharges;
            CoreFactory.workcostingEntity.CollectionCharges = model.WorkOrderCustomer.SampleAmount;
            CoreFactory.workcostingEntity.TransportCharges = model.WorkOrderCustomer.TransportationAmount;
            CoreFactory.workcostingEntity.TestingCharges = model.WorkOrderCustomer.TestingCharges;
            CoreFactory.workcostingEntity.GST = model.WorkOrderCustomer.GSTCharges;
            CoreFactory.workcostingEntity.Discount = model.WorkOrderCustomer.DiscountAmount;
            CoreFactory.workcostingEntity.UnitPrice = model.WorkOrderCustomer.NetAmount;
            CoreFactory.workcostingEntity.EnteredBy = LIMS.User.UserMasterID;
            if (model.WorkOrderCustomer.CostingId == null || model.WorkOrderCustomer.CostingId == 0)
            {
                BALFactory.workordercustomerBAL.AddWorkOrderCosting(CoreFactory.workcostingEntity);
                return Json(new { status = "added" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                CoreFactory.workcostingEntity.EnquirySampleID = model.WorkOrderCustomer.EnquirySampleID;
                CoreFactory.workcostingEntity.CostingId = (long)model.WorkOrderCustomer.CostingId;
                BALFactory.workordercustomerBAL.UpdateWorkOrderCosting(CoreFactory.workcostingEntity);
                return Json(new { status = "updated" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeleteCosting(int CostingId)
        {
            BALFactory.workordercustomerBAL.DeleteCosting(CostingId);
            return Json(new { status = "Deleted" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult WorkOrderApprove(int WorkOrderId)
        {
            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("WOApproved");
            BALFactory.workordercustomerBAL.UpdateWorkOrderCustomerStatus(WorkOrderId, (byte)iStatusId);
            BALFactory.workordercustomerBAL.WorkOrderApprove(WorkOrderId, iStatusId, LIMS.User.UserMasterID);
            if (LIMS.User.RoleName == "BDM")
            {
                return RedirectToAction("HODWorkOrderDirectList");
            }
            else if (LIMS.User.RoleName == "HOD")
            {
                return RedirectToAction("Index");
            }
            else if (LIMS.User.RoleName == "Admin")
            {
                return RedirectToAction("SampleCollectionCalendar", "ManageSampleCollection", new { area = "ManageSampleCollection"});

               
            }

            return RedirectToAction("Index");
        }
        public ActionResult AssignSTL(int WorkOrderId, int AssignToId)
        {
            BALFactory.workOrderBAL.AssignSTL(WorkOrderId, AssignToId, LIMS.User.UserMasterID);
            if (LIMS.User.RoleName == "Admin" || LIMS.User.RoleName == "BDM")
            {
                return RedirectToAction("HODWorkOrderDirectList");
            }
            else if (LIMS.User.RoleName == "HOD")
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult WorlOrderReject(int WorkOrderId, string Remark)
        {
            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("WORejected");
            BALFactory.workordercustomerBAL.UpdateWorkOrderCustomerStatus(WorkOrderId, (byte)iStatusId);
            BALFactory.workOrderBAL.WorkOrderReject(WorkOrderId, Remark, LIMS.User.UserMasterID);
            if ( LIMS.User.RoleName == "BDM")
            {
                return RedirectToAction("HODWorkOrderDirectList");
            }
            else if (LIMS.User.RoleName == "HOD")
            {
                return RedirectToAction("Index");
            }
            else if (LIMS.User.RoleName == "Admin")
            {
                return RedirectToAction("Index");
            }
                return RedirectToAction("Index");
        }
        public PartialViewResult _AddWorkOrderDetails(int? WorkOrderId = 0)
        {
            WorkOrderCustomerListModel model = new WorkOrderCustomerListModel();
            model.WorkOrderCustomer = new WorkOrderCustomerModel();
            model.FinalWorkOrderList = new List<FinalWorkOrderModel>();
            model.WorkOrderCustomer.WorkOrderId = (Int32)WorkOrderId;

            if (WorkOrderId != 0)//Changes by Nivedita for Major change may be removed later
            {
                CoreFactory.workOrderEntity = BALFactory.workordercustomerBAL.GetWorkOrderCustomerDetail((Int32)WorkOrderId);
                if (CoreFactory.workOrderEntity != null)
                {
                    model.WorkOrderCustomer.WorkOrderId = (Int32)CoreFactory.workOrderEntity.WorkOrderId;
                    model.WorkOrderCustomer.CustomerName = CoreFactory.workOrderEntity.RegistrationName;
                    model.WorkOrderCustomer.WorkOrderReceivedDate = CoreFactory.workOrderEntity.WORecieveDate;
                    model.WorkOrderCustomer.WORecvDate = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderReceivedDate).ToString("dd/MM/yyyy");
                    model.WorkOrderCustomer.WorkOrderEndDate = CoreFactory.workOrderEntity.WOEndDate;
                    model.WorkOrderCustomer.WOEDate = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderEndDate).ToString("dd/MM/yyyy");
                    model.WorkOrderCustomer.SampleCollectionDate = CoreFactory.workOrderEntity.ExpectSampleCollDate;
                    model.WorkOrderCustomer.expSampleDate = Convert.ToDateTime(model.WorkOrderCustomer.SampleCollectionDate).ToString("dd/MM/yyyy");
                    model.WorkOrderCustomer.Duration = CoreFactory.workOrderEntity.Duration;
                    model.WorkOrderCustomer.WorkOrderUpload = CoreFactory.workOrderEntity.WOUpload;
                    model.WorkOrderCustomer.FileName = CoreFactory.workOrderEntity.FileName;
                    model.WorkOrderCustomer.WorkOrderNo = CoreFactory.workOrderEntity.WorkOrderNo;
                    model.WorkOrderCustomer.ContractAmendment = CoreFactory.workOrderEntity.ContractAmendment;

                    if (CoreFactory.workOrderEntity.IsIGST == null)
                    {
                        model.WorkOrderCustomer.IsIGST = false;
                    }
                    else
                    {
                        model.WorkOrderCustomer.IsIGST = (bool)CoreFactory.workOrderEntity.IsIGST;
                    }
                }

                CoreFactory.costingList = BALFactory.workordercustomerBAL.GetWorkOrderCustomerCostingList((Int32)WorkOrderId);
                int i = 1;
                foreach (var cost in CoreFactory.costingList)
                {
                    model.FinalWorkOrderList.Add(new FinalWorkOrderModel()
                    {
                        SerialNo = i,
                        EnquirySampleID = cost.EnquirySampleID,
                        CostingId = cost.CostingId,
                        SampleName = cost.SampleName,
                        DisplaySampleName = cost.DisplaySampleName,
                        SampleTypeProductName = cost.SampleTypeProductName,
                        SampleTypeProductCode = cost.SampleTypeProductCode,
                        //ProductGroupName = cost.ProductGroupName,
                        //SubGroupName = cost.SubGroupName,
                        //MatrixName = cost.MatrixName,
                        ParameterName = BALFactory.sampleParameterBAL.GetSampleParameters((Int32)cost.EnquirySampleID), //sample.Parameters,
                        PCBLimit = BALFactory.sampleParameterBAL.GetSamplePCBLimit((Int32)cost.EnquirySampleID), //sample.PCBLimit,
                        UnitPrice = cost.UnitPrice,
                        Quantity = cost.NoOfSample == null ? 0 : (Int32)cost.NoOfSample,
                        Total = (cost.NoOfSample == null ? 0 : (Int32)cost.NoOfSample) * (cost.UnitPrice == null ? 0 : (decimal)cost.UnitPrice),
                        IsSetPCBLimit = cost.IsSetPCBLimit == null ? false : (bool)cost.IsSetPCBLimit,

                    }); ; ;
                    i++;
                }
            }

            ViewBag.Frequency = BALFactory.dropdownsBAL.GetFrequency();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _AddWorkOrderDetails(WorkOrderCustomerListModel model)
        {
            CoreFactory.WorkOrderCustomerEntity.WorkOrderId = model.WorkOrderCustomer.WorkOrderId;
            CoreFactory.WorkOrderCustomerEntity.ContractAmendment = model.WorkOrderCustomer.ContractAmendment;
            CoreFactory.WorkOrderCustomerEntity.IsIGST = model.WorkOrderCustomer.IsIGST;
            strStatus = BALFactory.workordercustomerBAL.AddCostPercentage(CoreFactory.WorkOrderCustomerEntity);
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            foreach (var sample in model.FinalWorkOrderList)
            {
                var getSampleLocationCount = BALFactory.workOrderBAL.GetSampleLocationCount((Int32)sample.EnquirySampleID);
                if (getSampleLocationCount.Count != sample.Quantity)
                {
                    return Json(new { status = "Fail NoOfLocation" }, JsonRequestBehavior.AllowGet);//For Locations less than number of Samples
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
          
            IList<EnquirySampleEntity> enquirySample = new List<EnquirySampleEntity>();
            foreach (var sample in model.FinalWorkOrderList)
            {
                if (sample.Quantity == 0 || sample.Quantity == null)
                {
                    return Json(new { status = "Fail" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    enquirySample.Add(new EnquirySampleEntity()
                    {
                        EnquirySampleID = sample.EnquirySampleID,
                        SampleTypeProductCode = sample.SampleTypeProductCode,
                        SampleTypeProductName = sample.SampleTypeProductName,
                        WorkOrderId = model.WorkOrderCustomer.WorkOrderId,
                        NoOfSample = sample.Quantity,
                        SampleLocation = sample.Location,
                        FrequencyMasterId = sample.FrequencyMasterId,
                        SampleName = sample.SampleName,
                        DisplaySampleName = sample.DisplaySampleName,
                        TotalCharges = sample.Total
                    });

                    BALFactory.workOrderBAL.UpdateEnquirySampleDetail(enquirySample);
                    var getloc = BALFactory.workOrderBAL.GetLocation((Int32)sample.EnquirySampleID);

                    CoreFactory.workOrderEntity.ExpectSampleCollDate = model.WorkOrderCustomer.SampleCollectionDate;
                    CoreFactory.workOrderEntity.Duration = model.WorkOrderCustomer.Duration;
                    if (sample.FrequencyMasterId != null)
                    {
                        var freq = BALFactory.workOrderBAL.GetFrequencyDetails((Int32)sample.FrequencyMasterId);
                        if (freq == 7)
                        {
                            DateTime dt;
                            DateTime dt1;
                            dt = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderReceivedDate);
                            dt1 = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderEndDate);
                            TimeSpan days = dt1 - dt;
                            int dateDiff = days.Days;
                            int weeks = (int)dateDiff / 7;
                            //Weekly

                            var noRecords = weeks;
                            for (int j = 0; j < noRecords; j++)
                            {
                                long WOMasterSampleCollectionDateId = BALFactory.workOrderBAL.AddWOMasterCollDate(true, sample.EnquirySampleID);
                                CoreFactory.workOrderEntity.WOMasterSampleCollectionDateId = (Int32)WOMasterSampleCollectionDateId;
                                for (int i = 0; i < sample.Quantity; i++)
                                {
                                    CoreFactory.workOrderEntity.LocationSampleCollectionID = getloc[i].LocationSampleCollectionID;
                                    CoreFactory.workOrderEntity.LocationSampleName = getloc[i].LocationSampleName;
                                    long WorkOrderSampleCollectionDateId = BALFactory.workOrderBAL.AddWOSampleCollectionDate(CoreFactory.workOrderEntity);
                                }
                                DateTime newDate = (DateTime)CoreFactory.workOrderEntity.ExpectSampleCollDate;
                                CoreFactory.workOrderEntity.ExpectSampleCollDate = newDate.AddDays((double)freq);
                            }
                        }
                        if (freq == 15)
                        {

                            DateTime dt;
                            DateTime dt1;
                            dt = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderReceivedDate);
                            dt1 = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderEndDate);
                            TimeSpan days = dt1 - dt;
                            int dateDiff = days.Days;

                            int fortnight = 0;
                            if (((int)dateDiff % 2) != 0)
                            {
                                fortnight = ((int)dateDiff + 1) / 15;
                            }
                            else
                            {
                                fortnight = ((int)dateDiff) / 15;
                            }
                            //Fortnightly

                            var noRecords = (fortnight) * sample.Quantity;
                            for (int j = 0; j < noRecords; j++)
                            {
                                long WOMasterSampleCollectionDateId = BALFactory.workOrderBAL.AddWOMasterCollDate(true, sample.EnquirySampleID);
                                CoreFactory.workOrderEntity.WOMasterSampleCollectionDateId = (Int32)WOMasterSampleCollectionDateId;
                                for (int i = 0; i < sample.Quantity; i++)
                                {
                                    CoreFactory.workOrderEntity.LocationSampleCollectionID = getloc[i].LocationSampleCollectionID;
                                    CoreFactory.workOrderEntity.LocationSampleName = getloc[i].LocationSampleName;
                                    long WorkOrderSampleCollectionDateId = BALFactory.workOrderBAL.AddWOSampleCollectionDate(CoreFactory.workOrderEntity);
                                }
                                DateTime newDate = (DateTime)CoreFactory.workOrderEntity.ExpectSampleCollDate;
                                CoreFactory.workOrderEntity.ExpectSampleCollDate = newDate.AddDays((double)freq);
                            }
                        }
                        if (freq == 30)
                        {
                            DateTime dt;
                            DateTime dt1;
                            dt = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderReceivedDate);
                            dt1 = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderEndDate);
                            TimeSpan days = dt1 - dt;
                            int dateDiff = days.Days;
                            int month = 0;
                            if (((int)dateDiff % 2) != 0)//Comparing with remainder value
                            {
                                month = ((int)dateDiff + 1) / 30;
                            }
                            else
                            {
                                month = ((int)dateDiff) / 30;
                            }

                            //Monthly
                            var noRecords = month;
                            for (int j = 0; j < noRecords; j++)
                            {
                                long WOMasterSampleCollectionDateId = BALFactory.workOrderBAL.AddWOMasterCollDate(true, sample.EnquirySampleID);
                                CoreFactory.workOrderEntity.WOMasterSampleCollectionDateId = (Int32)WOMasterSampleCollectionDateId;
                                for (int i = 0; i < sample.Quantity; i++)
                                {
                                    CoreFactory.workOrderEntity.LocationSampleCollectionID = getloc[i].LocationSampleCollectionID;
                                    CoreFactory.workOrderEntity.LocationSampleName = getloc[i].LocationSampleName;
                                    long WorkOrderSampleCollectionDateId = BALFactory.workOrderBAL.AddWOSampleCollectionDate(CoreFactory.workOrderEntity);
                                }
                                DateTime newDate = (DateTime)CoreFactory.workOrderEntity.ExpectSampleCollDate;
                                if ((j % 2) == 0)
                                {
                                    CoreFactory.workOrderEntity.ExpectSampleCollDate = newDate.AddDays((double)freq + 1);
                                }
                                else
                                {
                                    CoreFactory.workOrderEntity.ExpectSampleCollDate = newDate.AddDays((double)freq);
                                }
                            }
                        }
                        if (freq == 90)
                        {
                            DateTime dt;
                            DateTime dt1;
                            dt = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderReceivedDate);
                            dt1 = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderEndDate);
                            TimeSpan days = dt1 - dt;
                            int dateDiff = days.Days;
                            int quart = (int)dateDiff / 90;
                            //Quarterly
                            var noRecords = quart;
                            for (int j = 0; j < noRecords; j++)
                            {
                                long WOMasterSampleCollectionDateId = BALFactory.workOrderBAL.AddWOMasterCollDate(true, sample.EnquirySampleID);
                                CoreFactory.workOrderEntity.WOMasterSampleCollectionDateId = (Int32)WOMasterSampleCollectionDateId;
                                for (int i = 0; i < sample.Quantity; i++)
                                {
                                    CoreFactory.workOrderEntity.LocationSampleCollectionID = getloc[i].LocationSampleCollectionID;
                                    CoreFactory.workOrderEntity.LocationSampleName = getloc[i].LocationSampleName;
                                    long WorkOrderSampleCollectionDateId = BALFactory.workOrderBAL.AddWOSampleCollectionDate(CoreFactory.workOrderEntity);
                                }
                                DateTime newDate = (DateTime)CoreFactory.workOrderEntity.ExpectSampleCollDate;
                                CoreFactory.workOrderEntity.ExpectSampleCollDate = newDate.AddDays((double)freq + j);
                            }
                        }
                        if (freq == 180)
                        {
                            DateTime dt;
                            DateTime dt1;
                            dt = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderReceivedDate);
                            dt1 = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderEndDate);
                            TimeSpan days = dt1 - dt;
                            int dateDiff = days.Days;
                            int halfy = (int)dateDiff / 180;
                            //Half Yearly
                            var noRecords = halfy;
                            for (int j = 0; j < noRecords; j++)
                            {
                                long WOMasterSampleCollectionDateId = BALFactory.workOrderBAL.AddWOMasterCollDate(true, sample.EnquirySampleID);
                                CoreFactory.workOrderEntity.WOMasterSampleCollectionDateId = (Int32)WOMasterSampleCollectionDateId;
                                for (int i = 0; i < sample.Quantity; i++)
                                {
                                    CoreFactory.workOrderEntity.LocationSampleCollectionID = getloc[i].LocationSampleCollectionID;
                                    CoreFactory.workOrderEntity.LocationSampleName = getloc[i].LocationSampleName;
                                    long WorkOrderSampleCollectionDateId = BALFactory.workOrderBAL.AddWOSampleCollectionDate(CoreFactory.workOrderEntity);
                                }
                                DateTime newDate = (DateTime)CoreFactory.workOrderEntity.ExpectSampleCollDate;
                                CoreFactory.workOrderEntity.ExpectSampleCollDate = newDate.AddDays((double)freq);
                            }
                        }
                        if (freq == 365)
                        {
                            DateTime dt;
                            DateTime dt1;
                            dt = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderReceivedDate);
                            dt1 = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderEndDate);
                            TimeSpan days = dt1 - dt;
                            int dateDiff = days.Days;
                            int yearly = (int)dateDiff / 365;
                            //Yearly
                            var noRecords = yearly;
                            for (int j = 0; j < noRecords; j++)
                            {
                                long WOMasterSampleCollectionDateId = BALFactory.workOrderBAL.AddWOMasterCollDate(true, sample.EnquirySampleID);
                                CoreFactory.workOrderEntity.WOMasterSampleCollectionDateId = (Int32)WOMasterSampleCollectionDateId;
                                for (int i = 0; i < sample.Quantity; i++)
                                {
                                    CoreFactory.workOrderEntity.LocationSampleCollectionID = getloc[i].LocationSampleCollectionID;
                                    CoreFactory.workOrderEntity.LocationSampleName = getloc[i].LocationSampleName;
                                    long WorkOrderSampleCollectionDateId = BALFactory.workOrderBAL.AddWOSampleCollectionDate(CoreFactory.workOrderEntity);
                                }
                                DateTime newDate = (DateTime)CoreFactory.workOrderEntity.ExpectSampleCollDate;
                                CoreFactory.workOrderEntity.ExpectSampleCollDate = newDate.AddDays((double)freq);
                            }
                        }
                        if (freq == 1)
                        {
                            DateTime dt;
                            DateTime dt1;
                            dt = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderReceivedDate);
                            dt1 = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderEndDate);
                            TimeSpan days = dt1 - dt;
                            int dateDiff = days.Days;
                            int daily = (int)dateDiff / 1;
                            //Daily
                            var noRecords = daily;
                            for (int j = 0; j < noRecords; j++)
                            {
                                long WOMasterSampleCollectionDateId = BALFactory.workOrderBAL.AddWOMasterCollDate(true, sample.EnquirySampleID);
                                CoreFactory.workOrderEntity.WOMasterSampleCollectionDateId = (Int32)WOMasterSampleCollectionDateId;
                                for (int i = 0; i < sample.Quantity; i++)
                                {
                                    CoreFactory.workOrderEntity.LocationSampleCollectionID = getloc[i].LocationSampleCollectionID;
                                    CoreFactory.workOrderEntity.LocationSampleName = getloc[i].LocationSampleName;
                                    long WorkOrderSampleCollectionDateId = BALFactory.workOrderBAL.AddWOSampleCollectionDate(CoreFactory.workOrderEntity);
                                }
                                DateTime newDate = (DateTime)CoreFactory.workOrderEntity.ExpectSampleCollDate;
                                CoreFactory.workOrderEntity.ExpectSampleCollDate = newDate.AddDays((double)freq);
                            }
                        }
                        if (freq == 3)
                        {
                            DateTime dt;
                            DateTime dt1;
                            dt = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderReceivedDate);
                            dt1 = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderEndDate);
                            TimeSpan days = dt1 - dt;
                            int dateDiff = days.Days;
                            int tw = (int)dateDiff / 7;
                            tw = tw * 2;
                            //Twice in a Week
                            var noRecords = tw;
                            for (int j = 0; j < noRecords; j++)
                            {
                                long WOMasterSampleCollectionDateId = BALFactory.workOrderBAL.AddWOMasterCollDate(true, sample.EnquirySampleID);
                                CoreFactory.workOrderEntity.WOMasterSampleCollectionDateId = (Int32)WOMasterSampleCollectionDateId;
                                for (int i = 0; i < sample.Quantity; i++)
                                {
                                    CoreFactory.workOrderEntity.LocationSampleCollectionID = getloc[i].LocationSampleCollectionID;
                                    CoreFactory.workOrderEntity.LocationSampleName = getloc[i].LocationSampleName;
                                    long WorkOrderSampleCollectionDateId = BALFactory.workOrderBAL.AddWOSampleCollectionDate(CoreFactory.workOrderEntity);
                                }
                                DateTime newDate = (DateTime)CoreFactory.workOrderEntity.ExpectSampleCollDate;
                                if ((j % 2) == 0)
                                {
                                    CoreFactory.workOrderEntity.ExpectSampleCollDate = newDate.AddDays((double)freq + 1);
                                }
                                else
                                {
                                    CoreFactory.workOrderEntity.ExpectSampleCollDate = newDate.AddDays((double)freq);
                                }
                            }
                        }
                        if (freq == 2)
                        {
                            if (sample.FrequencyMasterId == 7)//Hourly
                            {
                                DateTime dt;
                                DateTime dt1;
                                dt = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderReceivedDate);
                                dt1 = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderEndDate);
                                TimeSpan days = dt1 - dt;
                                int dateDiff = days.Days;
                                int tw = (int)dateDiff / 1;

                                var noRecords = tw;//no of Records in Days
                                                   //for no of records ie for 24hrs basis
                                int noRecordinHrs = model.NoOfDays * 24;

                                for (int j = 0; j < noRecordinHrs; j++)
                                {
                                    long WOMasterSampleCollectionDateId = BALFactory.workOrderBAL.AddWOMasterCollDate(true, sample.EnquirySampleID);
                                    CoreFactory.workOrderEntity.WOMasterSampleCollectionDateId = (Int32)WOMasterSampleCollectionDateId;
                                    for (int i = 0; i < sample.Quantity; i++)
                                    {
                                        CoreFactory.workOrderEntity.LocationSampleCollectionID = getloc[i].LocationSampleCollectionID;
                                        CoreFactory.workOrderEntity.LocationSampleName = getloc[i].LocationSampleName;
                                        long WorkOrderSampleCollectionDateId = BALFactory.workOrderBAL.AddWOSampleCollectionDate(CoreFactory.workOrderEntity);
                                    }
                                    DateTime newDate = (DateTime)CoreFactory.workOrderEntity.ExpectSampleCollDate;
                                    CoreFactory.workOrderEntity.ExpectSampleCollDate = newDate.AddHours(1);
                                }
                            }

                            if (sample.FrequencyMasterId == 8)//Six Times a Day
                            {
                                DateTime dt;
                                DateTime dt1;
                                dt = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderReceivedDate);
                                dt1 = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderEndDate);
                                TimeSpan days = dt1 - dt;
                                int dateDiff = days.Days;
                                int tw = (int)dateDiff / 1;
                                var noRecords = tw;//no of Records in Days
                                //for no of records ie for 24hrs basis
                                int noRecordinHrs = model.NoOfDays * 6;

                                for (int j = 0; j < noRecordinHrs; j++)
                                {
                                    long WOMasterSampleCollectionDateId = BALFactory.workOrderBAL.AddWOMasterCollDate(true, sample.EnquirySampleID);
                                    CoreFactory.workOrderEntity.WOMasterSampleCollectionDateId = (Int32)WOMasterSampleCollectionDateId;
                                    for (int i = 0; i < sample.Quantity; i++)
                                    {
                                        CoreFactory.workOrderEntity.LocationSampleCollectionID = getloc[i].LocationSampleCollectionID;
                                        CoreFactory.workOrderEntity.LocationSampleName = getloc[i].LocationSampleName;
                                        long WorkOrderSampleCollectionDateId = BALFactory.workOrderBAL.AddWOSampleCollectionDate(CoreFactory.workOrderEntity);
                                    }
                                    DateTime newDate = (DateTime)CoreFactory.workOrderEntity.ExpectSampleCollDate;
                                    CoreFactory.workOrderEntity.ExpectSampleCollDate = newDate.AddHours(4);
                                }
                            }
                        }
                    }
                    else
                    {
                        //For one Time Sample Collection only
                        for (int i = 0; i < sample.Quantity; i++)
                        {
                            long WOMasterSampleCollectionDateId = BALFactory.workOrderBAL.AddWOMasterCollDate(true, sample.EnquirySampleID);
                            CoreFactory.workOrderEntity.WOMasterSampleCollectionDateId = (Int32)WOMasterSampleCollectionDateId;
                            CoreFactory.workOrderEntity.LocationSampleCollectionID = getloc[i].LocationSampleCollectionID;
                            CoreFactory.workOrderEntity.LocationSampleName = getloc[i].LocationSampleName;
                            long WorkOrderSampleCollectionDateId = BALFactory.workOrderBAL.AddWOSampleCollectionDate(CoreFactory.workOrderEntity);
                        }
                    }
                }
            }

            // BALFactory.workOrderBAL.UpdateEnquirySampleDetail(enquirySample);
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _AddLocation(long? EnquirySampleID = 0, int? WorkOrderId = 0, int? count = 0)
        {
            WorkOrderCustomerListModel model = new WorkOrderCustomerListModel();
            model.location = new SampleLocationModel();
            model.locationList = new List<SampleLocationModel>();
            model.location.EnquirySampleID = (long)EnquirySampleID;
            model.location.count = (Int32)count;

            CoreFactory.sampleLocationList = BALFactory.sampleParameterBAL.GetSampleLocationList((Int32)EnquirySampleID);
            int i = 1;
            foreach (var loc in CoreFactory.sampleLocationList)
            {
                model.locationList.Add(new SampleLocationModel()
                {
                    SrNo = i,
                    SampleLocationId = loc.SampleLocationId,
                    Location = loc.Location,
                    EnquirySampleID = (long)loc.EnquirySampleID,
                });
                i++;
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult _AddLocation(WorkOrderCustomerListModel model)
        {
            CoreFactory.enquiryParameterEntity = new EnquiryParameterEntity();
            CoreFactory.enquiryParameterEntity.Location = model.location.Location;
            CoreFactory.enquiryParameterEntity.EnquirySampleID = model.location.EnquirySampleID;
            CoreFactory.enquiryParameterEntity.IsActive = true;
            CoreFactory.enquiryParameterEntity.EnteredBy = LIMS.User.UserMasterID;

            if (CoreFactory.sampleLocationList.Count < model.location.count)
            {
                long SampleLocationId = BALFactory.workOrderBAL.AddLocation(CoreFactory.enquiryParameterEntity);
                CoreFactory.sampleLocationList = BALFactory.sampleParameterBAL.GetSampleLocationList((Int32)model.location.EnquirySampleID);
                return Json(new { SampleLocationId = SampleLocationId, status = "success", list = CoreFactory.sampleLocationList }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                CoreFactory.sampleLocationList = BALFactory.sampleParameterBAL.GetSampleLocationList((Int32)model.location.EnquirySampleID);
                return Json(new { status = "Fail", list = CoreFactory.sampleLocationList }, JsonRequestBehavior.AllowGet);
            }
        }
        public FileResult DownloadWorkOrder(string FilePath, string FileName)
        {
            string fname;
            if (FilePath == "")
            {
                return null;
            }

            // fname = Path.Combine(Server.MapPath("~/Files/Upload/HSSEDocument/"), FileName);
            fname = FilePath;
            FileInfo file = new FileInfo(fname);
            if (file.Exists)
            {

                byte[] fileBytes = System.IO.File.ReadAllBytes(fname);
                //string fileName = fname;
                //var _fileNameArray = fname.Split('_');
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);
            }
            else
            {
                return null;
            }
        }
        public JsonResult ProductGroups(int? SampleTypeProductId = 0)
        {
            var productgroupList = BALFactory.dropdownsBAL.GetProductGroup((Int32)SampleTypeProductId);
            return Json(new { result = productgroupList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubGroups(int? SampleTypeProductId = 0, int? ProductGroupId = 0)
        {
            var subgroupList = BALFactory.dropdownsBAL.GetSubGroup((Int32)SampleTypeProductId, (Int32)ProductGroupId);
            return Json(new { result = subgroupList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Matrix(int? SampleTypeProductId = 0, int? ProductGroupId = 0, int? SubGroupId = 0)
        {
            var MatrixList = BALFactory.dropdownsBAL.GetMatrix((Int32)SampleTypeProductId, (Int32)ProductGroupId, (Int32)SubGroupId);
            return Json(new { result = MatrixList }, JsonRequestBehavior.AllowGet);
        }
        public enum ManageMessageId
        {
            success,
            Error,
            ContSUBMIT,
            ParamAdded,
            WOGenerate,
            RevisingQt,
        }
        public ActionResult GetParameterForInvoice(int WOID, string MName, string YName)
        {
            string startDate;
            string endDate;
            int LastDate = 0;
            string MNDigit = "";

            var LastDay = BALFactory.workOrderBAL.GetLastDay(MName);
            MNDigit = LastDay.MonthTotalDaysId.ToString();
            if (Convert.ToInt32(MNDigit) < 10)
            {
                MNDigit = String.Concat("0", MNDigit);
            }
            var d = new DateTime();
            var x = "";
            startDate = (YName + "/" + MNDigit + "/" + "01");

            endDate = (YName + "/" + MNDigit + "/" + LastDay.LastDay);
            List<object> path = new List<object>();
            path.Add(WOID);
            path.Add(startDate);
            path.Add(endDate);
            var tempstr = string.Concat(WOID + "," + startDate + "," + endDate);
            return Json(tempstr);
        }

    }
}