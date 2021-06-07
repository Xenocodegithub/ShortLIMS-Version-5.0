using LIMS_DEMO.Areas.Arrival.Models;
using LIMS_DEMO.Areas.Enquiry.Models;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Arrival;
using LIMS_DEMO.BAL.Collection;
using LIMS_DEMO.BAL.DropDown;
using LIMS_DEMO.BAL.Enquiry;
using LIMS_DEMO.BAL.Lab;
using LIMS_DEMO.BAL.WorkOrderCustomer;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Arrival;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace LIMS_DEMO.Areas.Enquiry.Controllers
{
    public class TRFController : Controller
    {
        public TRFController()
        {
            BALFactory.enquiryBAL = new EnquiryBAL();
            BALFactory.costingBAL = new CostingBAL();
            BALFactory.workOrderBAL = new WorkOrderBAL();
            BALFactory.sampleParameterBAL = new SampleParameterBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
            BALFactory.quotationBAL = new QuotationBAL();
            BALFactory.workordercustomerBAL = new WorkOrderCustomerBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
            BALFactory.samplearrivalBAL = new SampleArrivalBAL();
            BALFactory.samplecollectionBAL = new SampleCollectionBAL();
        }
       
        // GET: Enquiry/TRF
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TRFList(DateTime? FromDate, DateTime? ToDate)
        {
            if (ToDate == null)
            {
                ToDate = FromDate;
            }
            else if (FromDate == null)
            {
                FromDate = ToDate;
            }
            List<WorkOrderHODModel> model = new List<WorkOrderHODModel>();
            var WorkOrderList = BALFactory.workOrderBAL.GetTRF_WOList(LIMS.User.LabId, FromDate, ToDate);
            foreach (var wo in WorkOrderList)
            {
                model.Add(new WorkOrderHODModel()
                {
                    RegistrationName = wo.RegistrationName,
                    WorkOrderNo = wo.WorkOrderNo,
                    //EnquiryId = wo.EnquiryId,
                    WorkOrderId = wo.WorkOrderId,
                    CurrentStatus = wo.CurrentStatus,
                    
                    //AssignToId = wo.AssignToId,// Doubt for BDM
                    Remarks = wo.Remarks,
                    WORecieveDate = wo.WORecieveDate,
                    WORecvDate = Convert.ToDateTime(wo.WORecieveDate).ToString("dd/MM/yyyy"),
                    WOUpload = wo.WOUpload,
                    FileName = wo.FileName,
                    EnteredBy = wo.EnteredBy,
                    //IsIGST = (bool)wo.IsIGST,
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

            ViewBag.Planner = BALFactory.dropdownsBAL.GetUserList("Planner", LIMS.User.LabId);//For STL 
            return View(model);
        }

        public ActionResult AssignPlanner(int WorkOrderId, int AssignToId)
        {
            BALFactory.workOrderBAL.AssignSTL(WorkOrderId, AssignToId, LIMS.User.UserMasterID);
            if (LIMS.User.RoleName == "Admin" || LIMS.User.RoleName == "BDM")
            {
                return RedirectToAction("TRFList");
            }
            else if (LIMS.User.RoleName == "HOD")
            {
                return RedirectToAction("WorkOrderList");
            }
            return RedirectToAction("WorkOrderList");
        }
        public ActionResult TRF_WoReject(int WorkOrderId, int EnquiryId, string Remark)
        {
            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("WORejected");
            BALFactory.enquiryBAL.UpdateEnquiryStatus(EnquiryId, (byte)iStatusId);
            BALFactory.workOrderBAL.WorkOrderReject(WorkOrderId, Remark, LIMS.User.UserMasterID);
            if (LIMS.User.RoleName == "Admin" || LIMS.User.RoleName == "BDM")
            {
                return RedirectToAction("TRFList");
            }
            else if (LIMS.User.RoleName == "HOD")
            {
                return RedirectToAction("WorkOrderList");
            }
            return RedirectToAction("WorkOrderList");
        }
        public ActionResult TRF_WOApprove(int WorkOrderId, int EnquiryId)
        {
            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("WOApproved");
            BALFactory.enquiryBAL.UpdateTRFStatus(WorkOrderId, (byte)iStatusId);
            //BALFactory.workOrderBAL.WorkOrderApprove(WorkOrderId, EnquiryId, iStatusId, LIMS.User.UserMasterID);
            if (LIMS.User.RoleName == "Admin" || LIMS.User.RoleName == "BDM")
            {
                return RedirectToAction("TRFList");
            }
            else if (LIMS.User.RoleName == "HOD")
            {
                return RedirectToAction("WorkOrderList");
            }

            return RedirectToAction("WorkOrderList");
        }
        public ActionResult TRFPlannerAssignList()
         {
            
            CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetTRFReceiverList(LIMS.User.UserMasterID); //LIMS.User.UserMasterID
            List<SampleArrivalModel> modelList = new List<SampleArrivalModel>();
            int iSrNo = 1;
            foreach (var Item in CoreFactory.samplearrivalList)
            {
                if (iSrNo!=0)
                {
                    modelList.Add(new SampleArrivalModel()
                    {
                        WorkOrderID = (Int32)Item.WorkOrderID,
                        LocationSampleCollectionID = Item.LocationSampleCollectionID,
                        SampleTypeProductId = Item.SampleTypeProductId,
                        Url = Item.SampleTypeProductName,
                        SerialNo = iSrNo,
                        //ARCId = Item.ARCId,
                        SampleCollectionId = Item.SampleCollectionId,
                        SampleTypeProductName = Item.SampleTypeProductName,
                        SampleLocation = Item.SampleLocation,
                        EnquirySampleID = Item.EnquirySampleID,
                        EnquiryDetailId = Item.EnquiryDetailId,
                        //EnquiryId = Item.EnquiryId,
                        //EmployeeId = Item.EmployeeId, //RoleId,UserName,CollectorName
                        SampleNo = Item.SampleNo,//to be Changed to SampleName wrt Iteration Number,
                                                 //ULRNo = Item.ULRNo,//doubt for backend storage only
                        SampleName = Item.SampleName,// FieldDeterminationId = Item.FieldDeterminationId,//Doubt
                        RequestNo = Item.RequestNo,
                        ULRNo = Item.ULRNo,
                        SampleNameOriginal = Item.SampleNameOriginal,
                        //ContactNO = Item.ContactNO,
                        CustomerName = Item.CustomerName,
                        CityName =Item.CityName,
                        StatusId = Item.StatusId,
                        CurrentStatus = Item.CurrentStatus,//SampleReceived
                        CollectionDate = Item.CollectionDate,
                        //Date = Convert.ToDateTime(Item.CollectionDate).ToString("dd/MM/yyyy"),
                        //MatrixName = Item.MatrixName,
                        //Url = Item.MatrixName,//Url = "Air",//Url=ProductGrp+SubGrp+Matrix
                    }); ;
                    iSrNo++;
                }
            }
            return View(modelList);
        }
        public ActionResult _PlannerSelect(int? SampleCollectionId = 0, int? ARCId = 0, string SampleNo = "", int SampleTypeProductId=0, int EnquirySampleID=0, int WorkOrderID=0)
        {
            SampleArrivalModel model = new SampleArrivalModel();
            if (SampleCollectionId!= null)
            {
                CoreFactory.samplearrivalEntity = BALFactory.samplearrivalBAL.GetTRFArrivalDetails((Int32)SampleCollectionId);
                model.EnquirySampleID = CoreFactory.samplearrivalEntity.EnquirySampleID;
                model.SampleCollectionId = CoreFactory.samplearrivalEntity.SampleCollectionId;
                model.CustomerMasterId = CoreFactory.samplearrivalEntity.CustomerMasterId;
                model.SampleLocation = CoreFactory.samplearrivalEntity.SampleLocation;
                model.CityName = CoreFactory.samplearrivalEntity.CityName;
                model.CollectedBy = CoreFactory.samplearrivalEntity.CollectedBy;
                model.CustomerName = CoreFactory.samplearrivalEntity.CustomerName;
                model.SampleLocation = CoreFactory.samplearrivalEntity.SampleLocation;
                model.EnvCondtId = CoreFactory.samplearrivalEntity.EnvCondtId;
                model.SampleTypeId = CoreFactory.samplearrivalEntity.SampleTypeId;
                /////////////////Planner by Chemical OR Biological///////////////////////////////////////////////////////////////////////////////////
                CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetDisciplineList();
                //CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList((Int32)model.EnquirySampleID);
                model.ParaDisciplineList = new List<ParameterDisciplineModel>();
                int iSrNo = 1;
                foreach (var item in CoreFactory.samplearrivalList)
                {
                    model.ParaDisciplineList.Add(new ParameterDisciplineModel()
                    {
                        SerialNo = iSrNo,
                        DisciplineId = item.DisciplineId,
                        Discipline = item.Discipline,
                        PlannerId = item.PlannerId,
                        Parameters = BALFactory.samplearrivalBAL.GetParameterByDiscipline((Int32)model.EnquirySampleID, (int)item.DisciplineId), //sample.Parameters,
                        Planner = BALFactory.dropdownsBAL.GetPlannerList("Planner", LIMS.User.LabId, (int)item.DisciplineId),//For selection of Planner,
                    });
                    iSrNo++;
                }
            }
            ViewBag.SampleTypeProductId = SampleTypeProductId;
            ViewBag.EnquirySampleID = EnquirySampleID;
            ViewBag.WorkOrderID = WorkOrderID;
            return View(model);
        }
        [HttpPost]
        public JsonResult _PlannerSelect(SampleArrivalModel model)
        {
            CoreFactory.samplearrivalEntity = new SampleArrivalEntity();
            CoreFactory.samplearrivalEntity.SampleCollectionId = model.SampleCollectionId;
            CoreFactory.samplearrivalEntity.LocationSampleCollectionID = model.LocationSampleCollectionID;
            CoreFactory.samplearrivalEntity.EnquirySampleID = model.EnquirySampleID;
            CoreFactory.samplearrivalEntity.WorkOrderID = model.WorkOrderID;
            CoreFactory.samplearrivalEntity.SampleTypeProductId = model.SampleTypeProductId;
            //CoreFactory.samplearrivalEntity.RequestNo = model.RequestNo;
            CoreFactory.samplearrivalEntity.SampleLocation = model.SampleLocation;
            CoreFactory.samplearrivalEntity.CollectedBy = model.CollectedBy;
            CoreFactory.samplearrivalEntity.CustomerName = model.CustomerName;
            CoreFactory.samplearrivalEntity.CityName = model.CityName;
            CoreFactory.samplearrivalEntity.SampleLocation = model.SampleLocation;
            CoreFactory.samplearrivalEntity.SampleTypeId = model.SampleTypeId;
            var Reportno_ULRno = BALFactory.samplearrivalBAL.GetTRFReportULRNumber(model.SampleTypeProductId, (Int32)model.EnquirySampleID, model.WorkOrderID);
            if (Reportno_ULRno != string.Empty)
            {
                //if (Reportno_ULRno!= "" || Reportno_ULRno != "")
                {
                    CoreFactory.samplearrivalEntity.ULRNo = Reportno_ULRno;
                    CoreFactory.samplearrivalEntity.RequestNo = BALFactory.samplearrivalBAL.GenerateReportNo((Int32)model.SampleCollectionId, model.SampleName, model.CollectedBy, model.CustomerName, model.CityName);
                    //CoreFactory.samplearrivalEntity.RequestNo = Reportno_ULRno;
                }
            }

            else
            {
                CoreFactory.samplearrivalEntity.ULRNo = BALFactory.samplearrivalBAL.GenerateULRNo((Int32)model.SampleCollectionId, (Int32)model.EnquiryDetailId);
                CoreFactory.samplearrivalEntity.RequestNo = BALFactory.samplearrivalBAL.GenerateReportNo((Int32)model.SampleCollectionId, model.SampleName, model.CollectedBy, model.CustomerName, model.CityName);
            }
            CoreFactory.plannerbydisciplineList = new List<PlannerByDisciplineEntity>();
            foreach (var item in model.ParaDisciplineList)
            {
                if (item.PlannerId != null && item.Parameters != null)
                {
                    CoreFactory.plannerbydisciplineList.Add(new PlannerByDisciplineEntity()
                    {
                        PlannerId = item.PlannerId,
                        //ParameterName = item.ParameterName,
                        Parameters = item.Parameters,
                        DisciplineId = item.DisciplineId,
                        SampleCollectionId = model.SampleCollectionId,
                        IsActive = true,
                        EnteredBy = LIMS.User.UserMasterID,
                        EnteredDate = DateTime.Now,
                    });

                    BALFactory.samplearrivalBAL.AddTRFPlannerByDiscipline(CoreFactory.plannerbydisciplineList,(Int32)model.SampleCollectionId);
                }
                else if (item.PlannerId == null && item.Parameters != null)
                {
                    return Json(new { status = "Fail" }, JsonRequestBehavior.AllowGet);
                }

            }
            //var status = BALFactory.samplearrivalBAL.AddARC();
            var status = BALFactory.samplearrivalBAL.UpdateTRFSampleArrival(CoreFactory.samplearrivalEntity);
            return Json(new { status = "succes" }, JsonRequestBehavior.AllowGet);
        }
    }
}