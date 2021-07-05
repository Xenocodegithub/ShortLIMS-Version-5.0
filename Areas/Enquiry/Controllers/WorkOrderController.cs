using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using LIMS_DEMO.Areas.Enquiry.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Enquiry;
using LIMS_DEMO.BAL.DropDown;
using LIMS_DEMO.BAL.WorkOrderCustomer;

namespace LIMS_DEMO.Areas.Enquiry.Controllers
{
    public class WorkOrderController : Controller
    {
        public WorkOrderController()
        {
            BALFactory.enquiryBAL = new EnquiryBAL();
            BALFactory.costingBAL = new CostingBAL();
            BALFactory.workOrderBAL = new WorkOrderBAL();
            BALFactory.sampleParameterBAL = new SampleParameterBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
            BALFactory.quotationBAL = new QuotationBAL();
            BALFactory.workordercustomerBAL = new WorkOrderCustomerBAL();
        }
        public PartialViewResult PreviewWorkOrder(int? WorkOrderId = 0, int? EnquiryId = 0)
        {
            ViewBag.WorkOrderId = WorkOrderId;
            WorkOrderListModel model = new WorkOrderListModel();
            // WorkOrderModel model = new WorkOrderModel();
            // model.WorkOrderCustomer = new SampleRegistrationModel();
            //model.FinalWorkOrderList = new List<WorkOrderCustomer.Models.FinalWorkOrderModel>();
            model.WorkOrderId = (Int32)WorkOrderId;
            model.EnquiryId = (Int32)EnquiryId;
            if (WorkOrderId != 0)//Changes by Nivedita for Major change may be removed later
            {
                CoreFactory.workOrderEntity = BALFactory.workordercustomerBAL.GetWorkOrderCustomerDetail((Int32)WorkOrderId);
                if (CoreFactory.workOrderEntity != null)
                {
                    model.WorkOrderId = (Int32)CoreFactory.workOrderEntity.WorkOrderId;
                    // model.EnquiryId = (Int32)CoreFactory.workOrderEntity.EnquirySampleId;
                    model.RegistrationName = CoreFactory.workOrderEntity.RegistrationName;
                    model.WorkOrderReceivedDate = (System.DateTime)CoreFactory.workOrderEntity.WORecieveDate;
                    model.WorkOrderNo = CoreFactory.workOrderEntity.WorkOrderNo;
                    //model.WorkOrderCustomer.CollectedBy = CoreFactory.workOrderEntity.CollectedBy;
                    if (CoreFactory.workOrderEntity.IsIGST == null)
                    {
                        model.IsIGST = false;
                    }
                    else
                    {
                        model.IsIGST = (bool)CoreFactory.workOrderEntity.IsIGST;
                    }
                }
                model.workorderList = new List<WorkOrderModel>();
                CoreFactory.costingList = BALFactory.costingBAL.GetCostingList((Int32)EnquiryId);
                int i = 1;
                foreach (var cost in CoreFactory.costingList)
                {
                    model.workorderList.Add(new WorkOrderModel()
                    {
                        SrNo = i,
                        EnquirySampleID = cost.EnquirySampleID,
                        CostingId = cost.CostingId,
                        SampleName = cost.SampleName,
                        DisplaySampleName = cost.DisplaySampleName,
                        SampleTypeProductName = cost.SampleTypeProductName,
                        SampleTypeProductCode = cost.SampleTypeProductCode,
                        ParameterName = BALFactory.sampleParameterBAL.GetSampleParameters((Int32)cost.EnquirySampleID), //sample.Parameters,
                        UnitPrice = cost.UnitPrice,
                        Total = (cost.NoOfSample == null ? 0 : (Int32)cost.NoOfSample) * (cost.UnitPrice == null ? 0 : (decimal)cost.UnitPrice),

                    });
                    i++;
                }
            }
            return PartialView(model);
        }

        //GET: Enquiry/WorkOrder
        public ActionResult WorkOrderList(DateTime? FromDate, DateTime? ToDate)
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
            var WorkOrderList = BALFactory.workOrderBAL.GetWorkOrderList(LIMS.User.LabId, FromDate, ToDate);
            foreach (var wo in WorkOrderList)
            {
                model.Add(new WorkOrderHODModel()
                {
                    RegistrationName = wo.RegistrationName,
                    WorkOrderNo = wo.WorkOrderNo,
                    EnquiryId = wo.EnquiryId,
                    WorkOrderId = wo.WorkOrderId,
                    CurrentStatus = wo.CurrentStatus,
                    AssignToId = wo.AssignToId,// Doubt for BDM
                    Remarks = wo.Remarks,
                    WORecieveDate = wo.WORecieveDate,
                    WORecvDate = Convert.ToDateTime(wo.WORecieveDate).ToString("dd/MM/yyyy"),
                    WOUpload = wo.WOUpload,
                    FileName = wo.FileName,
                    EnteredBy = wo.EnteredBy,
                    IsIGST = (bool)wo.IsIGST,
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
            return View(model);
        }
        public ActionResult HODWorkOrderList(DateTime? FromDate, DateTime? ToDate)
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
            var WorkOrderList = BALFactory.workOrderBAL.GetWorkOrderList(LIMS.User.LabId, FromDate, ToDate);
            foreach (var wo in WorkOrderList)
            {
                model.Add(new WorkOrderHODModel()
                {
                    RegistrationName = wo.RegistrationName,
                    WorkOrderNo = wo.WorkOrderNo,
                    EnquiryId = wo.EnquiryId,
                    WorkOrderId = wo.WorkOrderId,
                    CurrentStatus = wo.CurrentStatus,
                    AssignToId = wo.AssignToId,// Doubt for BDM
                    Remarks = wo.Remarks,
                    WORecieveDate = wo.WORecieveDate,
                    WORecvDate = Convert.ToDateTime(wo.WORecieveDate).ToString("dd/MM/yyyy"),
                    WOUpload = wo.WOUpload,
                    FileName = wo.FileName,
                    EnteredBy = wo.EnteredBy,
                    IsIGST = (bool)wo.IsIGST,
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
            return View(model);
        }
        public ActionResult AddWorkOrder(int EnquiryId, int? WorkOrderId = 0)
        {
            WorkOrderListModel model = new WorkOrderListModel();
            model.EnquiryId = EnquiryId;
            if (EnquiryId != 0)
            {
                CoreFactory.workOrderEntity = BALFactory.workOrderBAL.GetWorkOrderDetail(EnquiryId);
                if (CoreFactory.workOrderEntity != null)
                {
                    model.EnquiryId = EnquiryId;
                    model.WorkOrderId = CoreFactory.workOrderEntity.WorkOrderId;
                    model.QuotationId = CoreFactory.workOrderEntity.QuotationId;
                    model.RegistrationName = CoreFactory.workOrderEntity.RegistrationName;
                    model.CustomerMasterId = CoreFactory.workOrderEntity.CustomerMasterId;
                    model.SampleCollectionDate = CoreFactory.workOrderEntity.ExpectSampleCollDate;
                    model.Duration = CoreFactory.workOrderEntity.Duration;
                    model.WorkOrderReceivedDate = CoreFactory.workOrderEntity.WORecieveDate;
                    model.WorkOrderEndDate = CoreFactory.workOrderEntity.WOEndDate;
                    model.WorkOrderUpload = CoreFactory.workOrderEntity.WOUpload;
                    model.FileName = CoreFactory.workOrderEntity.FileName;
                    model.QuotationAmount = CoreFactory.workOrderEntity.QuotedAmount;
                    model.WorkOrderNo = CoreFactory.workOrderEntity.WorkOrderNo;
                    model.ContractAmendment = CoreFactory.workOrderEntity.ContractAmendment;
                    if (CoreFactory.workOrderEntity.IsIGST == null)
                    {
                        model.IsIGST = false;
                    }
                    else
                    {
                        model.IsIGST = (bool)CoreFactory.workOrderEntity.IsIGST;
                    }
                }
                model.workorderList = new List<WorkOrderModel>();
                CoreFactory.costingList = BALFactory.costingBAL.GetCostingList(EnquiryId);
                int i = 1;
                foreach (var cost in CoreFactory.costingList)
                {
                    model.workorderList.Add(new WorkOrderModel()
                    {
                        SrNo = i,
                        EnquirySampleID = cost.EnquirySampleID,
                        CostingId = cost.CostingId,
                        SampleName = cost.SampleName,
                        DisplaySampleName = cost.DisplaySampleName,
                        //ProductGroupName = cost.ProductGroupName,
                        //SubGroupName = cost.SubGroupName,
                        //MatrixName = cost.MatrixName,
                        SampleTypeProductName = cost.SampleTypeProductName,
                        SampleTypeProductCode = cost.SampleTypeProductCode,
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

            if (WorkOrderId != 0)//Changes by Nivedita for Major change may be removed later
            {
                model.WorkOrderId = WorkOrderId;
                CoreFactory.workOrderEntity = BALFactory.workordercustomerBAL.GetWorkOrderCustomerDetail((Int32)WorkOrderId);
                if (CoreFactory.workOrderEntity != null)
                {
                    //model.EnquiryId = EnquiryId;
                    model.WorkOrderId = CoreFactory.workOrderEntity.WorkOrderId;
                    //model.QuotationId = CoreFactory.workOrderEntity.QuotationId;
                    model.RegistrationName = CoreFactory.workOrderEntity.RegistrationName;
                    model.CustomerMasterId = CoreFactory.workOrderEntity.CustomerMasterId;
                    //model.SampleCollectionDate = CoreFactory.workOrderEntity.ExpectSampleCollDate;
                    model.WorkOrderReceivedDate = CoreFactory.workOrderEntity.WORecieveDate;
                    model.WorkOrderEndDate = CoreFactory.workOrderEntity.WOEndDate;
                    model.WorkOrderUpload = CoreFactory.workOrderEntity.WOUpload;
                    model.FileName = CoreFactory.workOrderEntity.FileName;
                    //model.QuotationAmount = CoreFactory.workOrderEntity.QuotedAmount;
                    model.WorkOrderNo = CoreFactory.workOrderEntity.WorkOrderNo;
                }
                model.workorderList = new List<WorkOrderModel>();
                CoreFactory.costingList = BALFactory.workordercustomerBAL.GetWorkOrderCustomerCostingList((Int32)WorkOrderId);
                int i = 1;
                foreach (var cost in CoreFactory.costingList)
                {
                    model.workorderList.Add(new WorkOrderModel()
                    {
                        SrNo = i,
                        EnquirySampleID = cost.EnquirySampleID,
                        CostingId = cost.CostingId,
                        SampleName = cost.SampleName,
                        DisplaySampleName = cost.DisplaySampleName,
                        ProductGroupName = cost.ProductGroupName,
                        SubGroupName = cost.SubGroupName,
                        MatrixName = cost.MatrixName,
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
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddWorkOrder(HttpPostedFileBase file, WorkOrderListModel model)
        {
            if (file != null)
            {
                string filename = Path.GetFileName(file.FileName);
                string guid = Convert.ToString(model.QuotationId) + "_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyy")) + "_";
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
            CoreFactory.workOrderEntity = new WorkOrderEntity();
            CoreFactory.workOrderEntity.WorkOrderId = model.WorkOrderId;
            CoreFactory.workOrderEntity.QuotationId = model.QuotationId;
            CoreFactory.workOrderEntity.CustomerMasterId = (Int32)model.CustomerMasterId;
            CoreFactory.workOrderEntity.ExpectSampleCollDate = model.SampleCollectionDate;
            CoreFactory.workOrderEntity.Duration = model.Duration;
            CoreFactory.workOrderEntity.WORecieveDate = model.WorkOrderReceivedDate;
            CoreFactory.workOrderEntity.WOEndDate = model.WorkOrderEndDate;
            CoreFactory.workOrderEntity.ContractAmendment = model.ContractAmendment;
            //false-for(CGST+SGST) & true- for(IGST)
            CoreFactory.workOrderEntity.IsIGST = model.IsIGST;

            if (model.SampleCollectionDate > model.WorkOrderEndDate || model.SampleCollectionDate < model.WorkOrderReceivedDate)
            {
                return Json(new { status = "Fail Date" }, JsonRequestBehavior.AllowGet);
            }

            if (model.WorkOrderUpload == null)
            {
                CoreFactory.workOrderEntity.WOUpload = "";
                CoreFactory.workOrderEntity.FileName = "";
            }
            else
            {
                CoreFactory.workOrderEntity.WOUpload = model.WorkOrderUpload;
                CoreFactory.workOrderEntity.FileName = model.FileName;
            }
            CoreFactory.workOrderEntity.WorkOrderNo = model.WorkOrderNo;
            CoreFactory.workOrderEntity.IsActive = true;
            CoreFactory.workOrderEntity.EnteredBy = LIMS.User.UserMasterID;

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            foreach (var sample in model.workorderList)
            {
                var getSampleLocationCount = BALFactory.workOrderBAL.GetSampleLocationCount((Int32)sample.EnquirySampleID);
                if (getSampleLocationCount.Count != sample.Quantity)
                {
                    return Json(new { status = "Fail NoOfLocation" }, JsonRequestBehavior.AllowGet);//For Locations less than number of Samples
                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            IList<EnquirySampleEntity> enquirySample = new List<EnquirySampleEntity>();

            if (model.WorkOrderId == null || model.WorkOrderId == 0)
            {
                long WorkOrderId = BALFactory.workOrderBAL.AddWorkOrder(CoreFactory.workOrderEntity);
                CoreFactory.workOrderEntity.WorkOrderId = (Int32)WorkOrderId;

                if (WorkOrderId != 0)
                {
                    CoreFactory.workOrderEntity = BALFactory.workOrderBAL.GetDetail((Int32)WorkOrderId);
                    string Msg = "WorkOrder Received";

                    long NotificationDetailId = BALFactory.workOrderBAL.AddNotification(Msg, "ADMIN", CoreFactory.workOrderEntity);
                    long NotificationDetailId1 = BALFactory.workOrderBAL.AddNotification(Msg, "BDM", CoreFactory.workOrderEntity);
                    long NotificationDetailId2 = BALFactory.workOrderBAL.AddNotification(Msg, "Sampling Incharge", CoreFactory.workOrderEntity);
                }

                foreach (var sample in model.workorderList)
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
                            WorkOrderId = (Int32)WorkOrderId,
                            NoOfSample = sample.Quantity,
                            SampleLocation = sample.Location,
                            FrequencyMasterId = sample.FrequencyMasterId,
                            TotalCharges = sample.Total,
                            EnteredBy = LIMS.User.UserMasterID,
                            SampleName = sample.SampleName,
                            DisplaySampleName = sample.DisplaySampleName
                        });

                        BALFactory.workOrderBAL.UpdateEnquirySampleDetail(enquirySample);
                        var getloc = BALFactory.workOrderBAL.GetLocation((Int32)sample.EnquirySampleID);
                        CoreFactory.workOrderEntity.ExpectSampleCollDate = model.SampleCollectionDate;
                        CoreFactory.workOrderEntity.Duration = model.Duration;
                        if (sample.FrequencyMasterId != null)
                        {
                            var freq = BALFactory.workOrderBAL.GetFrequencyDetails((Int32)sample.FrequencyMasterId);
                            if (freq == 7)
                            {
                                DateTime dt;
                                DateTime dt1;
                                dt = Convert.ToDateTime(model.WorkOrderReceivedDate);
                                dt1 = Convert.ToDateTime(model.WorkOrderEndDate);
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
                                dt = Convert.ToDateTime(model.WorkOrderReceivedDate);
                                dt1 = Convert.ToDateTime(model.WorkOrderEndDate);
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

                               
                                var noRecords = fortnight;
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
                                ///////////////////////////// New changes
                                DateTime dt;
                                DateTime dt1;
                                dt = Convert.ToDateTime(model.WorkOrderReceivedDate);
                                dt1 = Convert.ToDateTime(model.WorkOrderEndDate);
                                TimeSpan days = dt1 - dt;
                                int dateDiff = days.Days;
                              
                                int month = 0;
                                if (((int)dateDiff % 2) != 0)// Comparing with remainder value
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
                                dt = Convert.ToDateTime(model.WorkOrderReceivedDate);
                                dt1 = Convert.ToDateTime(model.WorkOrderEndDate);
                                TimeSpan days = dt1 - dt;
                                int dateDiff = days.Days;
                                int quart = (int)dateDiff / 90;
                                //Quarterly & Once in a three Month
                              
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
                                dt = Convert.ToDateTime(model.WorkOrderReceivedDate);
                                dt1 = Convert.ToDateTime(model.WorkOrderEndDate);
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
                                dt = Convert.ToDateTime(model.WorkOrderReceivedDate);
                                dt1 = Convert.ToDateTime(model.WorkOrderEndDate);
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
                                dt = Convert.ToDateTime(model.WorkOrderReceivedDate);
                                dt1 = Convert.ToDateTime(model.WorkOrderEndDate);
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
                                dt = Convert.ToDateTime(model.WorkOrderReceivedDate);
                                dt1 = Convert.ToDateTime(model.WorkOrderEndDate);
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
                                    dt = Convert.ToDateTime(model.WorkOrderReceivedDate);
                                    dt1 = Convert.ToDateTime(model.WorkOrderEndDate);
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
                                    dt = Convert.ToDateTime(model.WorkOrderReceivedDate);
                                    dt1 = Convert.ToDateTime(model.WorkOrderEndDate);
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
                //BALFactory.workOrderBAL.UpdateEnquirySampleDetail(enquirySample);
            }
            else
            {
                BALFactory.workOrderBAL.UpdateWorkOrder(CoreFactory.workOrderEntity);
            }

            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("WOGenerate");

            if (model.EnquiryId == 0)//Changes by Nivedita for Major change may be removed later
            {
                BALFactory.workordercustomerBAL.UpdateWorkOrderCustomerStatus((long)model.WorkOrderId, (byte)iStatusId); //to be done later
            }
            else
            {
                BALFactory.enquiryBAL.UpdateEnquiryStatus(model.EnquiryId, (byte)iStatusId);
             }

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult WorkOrderApprove(int WorkOrderId, int EnquiryId)
        {
            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("WOApproved");
            BALFactory.enquiryBAL.UpdateEnquiryStatus(EnquiryId, (byte)iStatusId);
            BALFactory.workOrderBAL.WorkOrderApprove(WorkOrderId, EnquiryId, iStatusId, LIMS.User.UserMasterID);
            if (LIMS.User.RoleName == "Admin" || LIMS.User.RoleName == "BDM")
            {
                return RedirectToAction("HODWorkOrderList");
            }
            else if (LIMS.User.RoleName == "HOD")
            {
                return RedirectToAction("WorkOrderList");
            }

            return RedirectToAction("WorkOrderList");
        }
        public ActionResult _AddLocation(long? EnquirySampleID = 0, int? EnquiryId = 0, int? count = 0, string state = null)
        {
            SampleAndParametersModel model = new SampleAndParametersModel();
            model.location = new SampleLocationModel();
            model.locationList = new List<SampleLocationModel>();
            model.location.EnquiryId = (long)EnquiryId;
            model.location.EnquirySampleID = (long)EnquirySampleID;
            model.location.count = (Int32)count;
            if (state == "view")
            {
                ViewBag.state = state;
            }
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
        public ActionResult _AddLocation(SampleAndParametersModel model)
        {
            CoreFactory.enquiryParameterEntity = new EnquiryParameterEntity();
            CoreFactory.enquiryParameterEntity.Location = model.location.Location;
            CoreFactory.enquiryParameterEntity.EnquirySampleID = model.location.EnquirySampleID;
            if (model.LocationId != 0)
            {
                CoreFactory.enquiryParameterEntity.SampleLocationId = model.LocationId;
            }
            CoreFactory.enquiryParameterEntity.IsActive = true;
            CoreFactory.enquiryParameterEntity.EnteredBy = LIMS.User.UserMasterID;
            CoreFactory.sampleLocationList = BALFactory.sampleParameterBAL.GetSampleLocationList((Int32)model.location.EnquirySampleID);

            if (CoreFactory.sampleLocationList.Count < model.location.count || model.LocationId != 0)
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
            //CoreFactory.enquiryParameterEntity = new EnquiryParameterEntity();
            //CoreFactory.enquiryParameterEntity.Location = model.location.Location;
            //CoreFactory.enquiryParameterEntity.EnquirySampleID = model.location.EnquirySampleID;
            // CoreFactory.enquiryParameterEntity.IsActive = true;
            //CoreFactory.enquiryParameterEntity.EnteredBy = LIMS.User.UserMasterID;
            //CoreFactory.sampleLocationList = BALFactory.sampleParameterBAL.GetSampleLocationList((Int32)model.location.EnquirySampleID);

            //if (CoreFactory.sampleLocationList.Count < model.location.count)
            //{
            //    long SampleLocationId = BALFactory.workOrderBAL.AddLocation(CoreFactory.enquiryParameterEntity);
            //    CoreFactory.sampleLocationList = BALFactory.sampleParameterBAL.GetSampleLocationList((Int32)model.location.EnquirySampleID);
            //    return Json(new { SampleLocationId = SampleLocationId, status = "success", list = CoreFactory.sampleLocationList }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    CoreFactory.sampleLocationList = BALFactory.sampleParameterBAL.GetSampleLocationList((Int32)model.location.EnquirySampleID);
            //    return Json(new { status = "Fail", list = CoreFactory.sampleLocationList }, JsonRequestBehavior.AllowGet);
            //}

        }
        public ActionResult AssignSTL(int WorkOrderId, int AssignToId)
        {
            BALFactory.workOrderBAL.AssignSTL(WorkOrderId, AssignToId, LIMS.User.UserMasterID);
            if (LIMS.User.RoleName == "Admin" || LIMS.User.RoleName == "BDM")
            {
                return RedirectToAction("HODWorkOrderList");
            }
            else if (LIMS.User.RoleName == "HOD")
            {
                return RedirectToAction("WorkOrderList");
            }
            return RedirectToAction("WorkOrderList");
        }
        public ActionResult WorkOrderReject(int WorkOrderId, int EnquiryId, string Remark)
        {
            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("WORejected");
            BALFactory.enquiryBAL.UpdateEnquiryStatus(EnquiryId, (byte)iStatusId);
            BALFactory.workOrderBAL.WorkOrderReject(WorkOrderId, Remark, LIMS.User.UserMasterID);
            if (LIMS.User.RoleName == "Admin" || LIMS.User.RoleName == "BDM")
            {
                return RedirectToAction("HODWorkOrderList");
            }
            else if (LIMS.User.RoleName == "HOD")
            {
                return RedirectToAction("WorkOrderList");
            }
            return RedirectToAction("WorkOrderList");
        }
        public PartialViewResult _Remarks(int EnquiryId)
        {
            WorkOrderListModel model = new WorkOrderListModel();
            model.EnquiryId = EnquiryId;
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Remarks(WorkOrderListModel model)
        {
            var quotLog = BALFactory.quotationBAL.GetQuotationLogDetails(model.EnquiryId);
            model.QuotationLogId = quotLog.QuotationLogId;
            if (model.Remarks == null || model.Remarks == "")
            {
                return Json(new { status = "Fail" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                BALFactory.workOrderBAL.UpdateRemark(model.QuotationLogId, model.Remarks);
            }
            return Json(new { EnquiryId = model.EnquiryId, status = "success", message = "Remark Success" }, JsonRequestBehavior.AllowGet);
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