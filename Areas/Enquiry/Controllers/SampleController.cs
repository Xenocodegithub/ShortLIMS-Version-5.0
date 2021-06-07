using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using LIMS_DEMO.Areas.Enquiry.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.WorkOrderCustomer;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Arrival;
using LIMS_DEMO.Core.Arrival;
using LIMS_DEMO.BAL.WorkOrderCustomer;
using LIMS_DEMO.BAL.DropDown;
using System.Configuration;
using LIMS_DEMO.Core.Collection;
using LIMS_DEMO.Areas.Collection.Models;
using LIMS_DEMO.BAL.Collection;
using LIMS_DEMO.Areas.Arrival.Models;

namespace LIMS_DEMO.Areas.Enquiry.Controllers
{
    [RouteArea("Sample")]
    public class SampleController : Controller
    {
        string strStatus = "";
        readonly decimal GSTRate = Convert.ToDecimal(ConfigurationManager.AppSettings["GSTRate"]);
        Common.ErrorLogging errorLogging = new Common.ErrorLogging();
        public SampleController()
        {
            BALFactory.dropdownsBAL = new DropDownBal();
            BALFactory.samplecollectionBAL = new SampleCollectionBAL();
            BALFactory.workordercustomerBAL = new WorkOrderCustomerBAL();
            BALFactory.sampleParameterBAL = new BAL.Enquiry.SampleParameterBAL();
            BALFactory.costingBAL = new BAL.Enquiry.CostingBAL();
            BALFactory.workOrderBAL = new BAL.Enquiry.WorkOrderBAL();
            BALFactory.samplearrivalBAL = new BAL.Arrival.SampleArrivalBAL();
        }
        //public PartialViewResult _QuantityList(int SampleCollectionId)
        //{
        //    CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetArrivalQtyPreservativeList(SampleCollectionId);
        //    List<SampleArrivalModel> modelList = new List<SampleArrivalModel>();
        //    int iSrNo = 1;
        //    int Count = CoreFactory.samplearrivalList.Count();
        //    foreach (var item in CoreFactory.samplearrivalList)
        //    {

        //        if (item.ISGivenPreservative == true)
        //        {
        //            modelList.Add(new SampleArrivalModel()
        //            {
        //                SerialNo = iSrNo,
        //                SampleCollectionId = item.SampleCollectionId,
        //                QtyPreservativeId = item.QtyPreservativeId,
        //                ISGivenPreservative = item.ISGivenPreservative,
        //                GivenPreservative = "Yes",
        //                SampleQtyId = (Int32)item.SampleQtyId,
        //                SampleQty = item.SampleQty,
        //                Preservation = item.Preservation,
        //                No = (long)item.No,

        //            });
        //            iSrNo++;
        //        }
        //        if (item.ISGivenPreservative == false)
        //        {
        //            modelList.Add(new SampleArrivalModel()
        //            {
        //                SerialNo = iSrNo,
        //                SampleCollectionId = item.SampleCollectionId,
        //                QtyPreservativeId = item.QtyPreservativeId,
        //                ISGivenPreservative = item.ISGivenPreservative,
        //                GivenPreservative = "NO",
        //                SampleQtyId = (Int32)item.SampleQtyId,
        //                SampleQty = item.SampleQty,
        //                Preservation = item.Preservation,
        //                No = (long)item.No,
        //            });
        //            iSrNo++;
        //        }
        //    }
        //    return PartialView(modelList);
        //}


        // GET: Enquiry/Sample
        public ActionResult SampleRegistration(DateTime? FromDate, DateTime? ToDate)
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
            List<SampleRegistrationModel> modelList = new List<SampleRegistrationModel>();

            foreach (var Item in CoreFactory.WorkOrderCustomerList)
            {
                modelList.Add(new SampleRegistrationModel()
                {
                    WorkOrderID = Item.WorkOrderId,
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
            List<SampleRegistrationModel> modelList = new List<SampleRegistrationModel>();

            foreach (var Item in CoreFactory.WorkOrderCustomerList)
            {
                modelList.Add(new SampleRegistrationModel()
                {
                    WorkOrderID = Item.WorkOrderId,
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
        public JsonResult Getuserdetails(int UserMasterID)
        {
            var UserDetails = BALFactory.dropdownsBAL.GetUserDetails(UserMasterID);
            return Json(new { Name = UserDetails.UserName, No = UserDetails.PhoneNo }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult _AddCustomerDetails(int EnquiryId=0)
        {
            SampleRegistrationModel model = new SampleRegistrationModel();
            ViewBag.CommunicationMode = BALFactory.dropdownsBAL.GetCommunicationMode();
            ViewBag.Customers = BALFactory.dropdownsBAL.GetCustomers();
            ViewBag.Delivers = BALFactory.dropdownsBAL.GetDeliver();
            ViewBag.CollectorName = BALFactory.dropdownsBAL.GetUser();
            //CoreFactory.enquiryEntity = BALFactory.enquiryBAL.GetDetails((Int32)EnquiryId);
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _AddCustomerDetails(HttpPostedFileBase file, SampleRegistrationModel model)
        {
            if (file != null)
            {
                string filename = Path.GetFileName(file.FileName);
                string guid = Convert.ToString(model.WorkOrderID) + "_" + Convert.ToString(DateTime.Now.ToString("ddMMyyyy")) + "_";
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
            CoreFactory.WorkOrderCustomerEntity.WorkOrderId = model.WorkOrderID;
            CoreFactory.WorkOrderCustomerEntity.QuotationId = model.QuotationId;

            //CoreFactory.WorkOrderCustomerEntity.ModeOfCommunicationId = model.ModeOfCommunicationId;
            CoreFactory.WorkOrderCustomerEntity.CustomerMasterId = (Int32)model.CustomerMasterId;
            CoreFactory.WorkOrderCustomerEntity.DeliverId = (Int32)model.DeliverId;
            CoreFactory.WorkOrderCustomerEntity.StatusId = (byte)BALFactory.dropdownsBAL.GetStatusIdByCode(Enum.GetName(typeof(ManageMessageId), ManageMessageId.WOGenerate));
            CoreFactory.WorkOrderCustomerEntity.OVC = model.OVC;
            CoreFactory.WorkOrderCustomerEntity.WORecieveDate = model.WorkOrderReceivedDate;
            CoreFactory.WorkOrderCustomerEntity.WOEndDate = model.WorkOrderEndDate;
            CoreFactory.WorkOrderCustomerEntity.ExpectSampleCollDate = model.WorkOrderReceivedDate;
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
            
            if (model.WorkOrderID == 0)
            {
                long WorkOrderId = BALFactory.workordercustomerBAL.Add(CoreFactory.WorkOrderCustomerEntity);
                string Msg = "WorkOrder Received";
                long NotificationDetailId = BALFactory.workordercustomerBAL.AddNotification(Msg, "ADMIN", CoreFactory.WorkOrderCustomerEntity);
                long NotificationDetailId2 = BALFactory.workordercustomerBAL.AddNotification(Msg, "Sampling Incharge", CoreFactory.WorkOrderCustomerEntity);
                //long EnquiryId = BALFactory.enquiryBAL.Add(CoreFactory.enquiryEntity);
                return Json(new { WorkOrderId = WorkOrderId, status = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                strStatus = BALFactory.workordercustomerBAL.Update(CoreFactory.WorkOrderCustomerEntity);

                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }

            //if (model.EnquiryId == 0)
            //{
            //    long EnquiryId = BALFactory.enquiryBAL.Add(CoreFactory.enquiryEntity);
            //    CoreFactory.enquiryEntity.EnquiryId = EnquiryId;
            //    return Json(new { EnquiryId = EnquiryId, message = "Enquiry created." }, JsonRequestBehavior.AllowGet);
            //}

        }
        public PartialViewResult _AddSampleDetails(int? SampleCollectionId =0,int? WorkOrderId = 0, int? EnquiryDetailId = 0, int? EnquirySampleID = 0, int? SampleTypeProductId = 0, string SubroupCode = "")
        
        {
            WorkOrderCustomerListModel model = new WorkOrderCustomerListModel();

            model.WorkOrderCustomer = new SampleRegistrationModel();
            model.WorkOrderCustomerList = new List<SampleRegistrationModel>();
            model.ParameterList = new List<ParameterModel>();
            model.ParaList = new List<SampleRegistrationModel>();
            model.DeviceList = new List<SampleRegistrationModel>();
            model.ProcedureList = new List<SampleRegistrationModel>();
            
            ViewBag.EnquiryDetailId = EnquiryDetailId;

            if (WorkOrderId != 0)
            {
                CoreFactory.OrderSampleList = BALFactory.workordercustomerBAL.GetContractList((Int32)WorkOrderId);/////For Pg,Sg,Matrix List/////
                int iSrNo = 1;
                foreach (var item in CoreFactory.OrderSampleList)
                {
                    model.WorkOrderCustomerList.Add(new SampleRegistrationModel()
                    {
                        SerialNo = iSrNo,
                        WorkOrderID = (int)WorkOrderId,
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
                //CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetCollectionDevicesList((Int32)model.Sample.SampleCollectionId); //for sampledevice
               
                //int iSrNo2 = 1;
                //foreach (var item in CoreFactory.samplearrivalList)
                //{
                //    model.DeviceList.Add(new SampleRegistrationModel()
                //    {
                //        SerialNo2 = iSrNo2,
                //        SampleCollectionId = item.SampleCollectionId,
                //        SampleCollectionDevicesId = item.SampleCollectionDevicesId,
                //        SampleDeviceId2 = (Int32)item.SampleDeviceId,
                //        SampleDevice = item.SampleDevice,
                //    });
                //    iSrNo2++;
                //}
                //CoreFactory.procedureList = BALFactory.samplecollectionBAL.GetCollectionProcedureList((Int32)model.Sample.SampleCollectionId); //for sampledevice
                //int iSrNo3 = 1;
                //foreach (var item in CoreFactory.procedureList)
                //{
                //    model.ProcedureList.Add(new SampleRegistrationModel()
                //    {
                //        SerialNo2 = iSrNo3,
                //        SampleCollectionId = item.SampleCollectionId,
                //        SampleCollectionProcedureId = item.SampleCollectionProcedureId,
                //        ProcedureId2 = (Int32)item.ProcedureId,
                //        ProcedureName = item.Procedure,
                //    });
                //    iSrNo3++;
                //}
            }

            if (WorkOrderId != 0 && EnquiryDetailId != 0 && EnquirySampleID != 0)
            {
                CoreFactory.enquirySampleList = BALFactory.workordercustomerBAL.GetWorkOrderCustomerSampleList((Int32)WorkOrderId);
                int i = 1;
                foreach (var sample in CoreFactory.enquirySampleList)
                {
                    model.ParaList.Add(new SampleRegistrationModel()
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
                //CoreFactory.quantityPreservativeList = BALFactory.samplecollectionBAL.GetSampleQty(model.Sample.SampleTypeProductId);/////For QuantityPreservativeGrid
                CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetArrivalQtyPreservativeList((Int32)SampleCollectionId);

                //var dtoJson = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(CoreFactory.samplearrivalList);
                //ViewBag.ListViewBag = dtoJson;

                //int iSrNo = 1;
                //foreach (var item in CoreFactory.quantityPreservativeList)
                //{
                //    model.CollectionList.Add(new SampleCollectionModel()
                //    {
                //        SerialNo = iSrNo,
                //        SampleQtyId = item.SampleQtyId,
                //        SampleTypeProductId = item.SampleTypeProductId,
                //        SampleQty = item.SampleQty,
                //        Preservation = item.Preservation,
                //        No = item.No,
                //    });
                //    iSrNo++;
                //}

            }
            ViewBag.SampleTypeProductList = BALFactory.dropdownsBAL.GetSampleTypeProduct();
                ViewBag.ProductGroupList = BALFactory.dropdownsBAL.GetProductGroup(0);//Passing Data to Viewbag for dropdown           
                ViewBag.SubGroupList = BALFactory.dropdownsBAL.GetSubGroup(0, model.ProductGroupId);//Passing Data to Viewbag for dropdown
                ViewBag.MatrixList = BALFactory.dropdownsBAL.GetMatrix(0, model.ProductGroupId, model.SubGroupId);
                ViewBag.TestMethod = BALFactory.dropdownsBAL.GetTestMethods();//to be removed later 
                ViewBag.Unit = BALFactory.dropdownsBAL.GetUnits();//to be removed later 
                ViewBag.Branch = BALFactory.dropdownsBAL.GetBranches(LIMS.User.LabId);//to be removed later 
                //ViewBag.Unit = BALFactory.dropdownsBAL.GetUnits();

                ViewBag.SampleDescriptionList = BALFactory.dropdownsBAL.GetSampleDescription(model.ProductGroupId, model.SubGroupId);//Passing Data to Viewbag for dropdown
                                                                                                                                     //Doubt need to replace ProductGroupId with SampleTypeProductId
                                                                                                                                     // ViewBag.SampleDeviceList = BALFactory.dropdownsBAL.GetSampleDevice(0);//Passing Data to Viewbag for dropdown
            ViewBag.SampleDeviceList = BALFactory.dropdownsBAL.GetSampleDevice();;//Passing Data to Viewbag for dropdown
            ViewBag.ProcedureList = BALFactory.dropdownsBAL.GetProcedure();
                //ViewBag.SampleTypeList = BALFactory.dropdownsBAL.GetSampleType();
                ViewBag.EnvCondtsList = BALFactory.dropdownsBAL.GetEnvironmentalCondition();
            ViewBag.workid = WorkOrderId;
            ViewBag.WorkOrderId = WorkOrderId;
            return PartialView(model);
        }
        [HttpPost]
       
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _AddSampleDetails1(WorkOrderCustomerListModel model)
        {
            CoreFactory.enquiryParameterList = new List<EnquiryParameterEntity>();
            //CoreFactory.samplecollectionEntity.SampleCollectionId = model.Sample.SampleCollectionId;
            CoreFactory.samplecollectionEntity.EnquirySampleID = model.Sample.EnquirySampleID;
            CoreFactory.samplecollectionEntity.EnquiryDetailId = model.Sample.EnquiryDetailId;
            //CoreFactory.samplecollectionEntity.EnquiryId = model.Sample.Enqui;
            CoreFactory.samplecollectionEntity.WorkOrderID = model.Sample.WorkOrderID;

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

                List<int> DeviceCount = model.Sample.SampleDeviceId;
                if (DeviceCount != null)
                {
                    CoreFactory.sampleDeviceList = new List<SampleDeviceEntity>();////For SampleDevice Multiselect List
                    for (int i = 0; i < model.Sample.SampleDeviceId.Count; i++)
                    {
                        CoreFactory.sampleDeviceList.Add(new SampleDeviceEntity()
                        {
                            SampleCollectionDevicesId = model.Sample.SampleCollectionDevicesId,
                            SampleCollectionId = model.Sample.SampleCollectionId,
                            SampleDeviceId = model.Sample.SampleDeviceId[i],
                            IsActive = true,
                            EnteredBy = LIMS.User.UserMasterID,
                            EnteredDate = DateTime.Now,
                        });
                    }
                    BALFactory.samplecollectionBAL.AddSampleDevice(CoreFactory.sampleDeviceList);
                }
                List<int> ProcedureCount = model.Sample.ProcedureId;
                if (ProcedureCount != null)
                {
                    CoreFactory.procedureList = new List<ProcedureEntity>();////For Procedure Multiselect List
                    for (int i = 0; i < model.Sample.ProcedureId.Count; i++)
                    {
                        CoreFactory.procedureList.Add(new ProcedureEntity()
                        {
                            SampleCollectionProcedureId = model.Sample.SampleCollectionProcedureId,
                            SampleCollectionId = model.Sample.SampleCollectionId,
                            ProcedureId = model.Sample.ProcedureId[i],
                            IsActive = true,
                            EnteredBy = LIMS.User.UserMasterID,
                            EnteredDate = DateTime.Now,
                        });
                    }

                    BALFactory.samplecollectionBAL.AddSampleProcedure(CoreFactory.procedureList);
                }
                CoreFactory.samplecollectionEntity.SampleTypeId = model.SampleTypeId;
                CoreFactory.samplecollectionEntity.SampleType = model.SampleType;
                CoreFactory.samplecollectionEntity.EnvCondtId = model.EnvCondtId;
                CoreFactory.samplecollectionEntity.EnvCondts = model.EnvCondts;
                CoreFactory.samplecollectionEntity.ProcedureId = model.ProcedureId;
                CoreFactory.samplecollectionEntity.ProcedureName = model.Sample.ProcedureName;
                CoreFactory.samplecollectionEntity.IsActive = true;
                CoreFactory.samplecollectionEntity.EnteredBy = LIMS.User.UserMasterID;
                CoreFactory.samplecollectionEntity.SampleCollectionId = model.SampleCollectionId;
                CoreFactory.samplecollectionEntity.EnquiryDetailId = model.EnquiryDetailId;
                CoreFactory.samplecollectionEntity.WorkOrderID = model.WorkOrderId;

                //CoreFactory.quantityPreservativeList = new List<QuantityPreservativeEntity>();////For  QuantityPreservative List
                //    foreach (var item in model.CollectionList)
                //    {
                //        if (item.IsSelected)
                //        {
                //            CoreFactory.quantityPreservativeList.Add(new QuantityPreservativeEntity()
                //            {
                //                QtyPreservativeId = item.QtyPreservativeId,
                //                SampleCollectionId = model.SampleCollectionId,
                //                SampleQtyId = (Int32)item.SampleQtyId,
                //                ISGivenPreservative = item.ISGivenPreservative,
                //                No = item.No,
                //                IsActive = true,
                //                EnteredBy = LIMS.User.UserMasterID,
                //                EnteredDate = DateTime.Now,
                //            });
                //        }
                //    }
                //    BALFactory.samplecollectionBAL.AddQuantityPreservative(CoreFactory.quantityPreservativeList, model.EnquirySampleID, model.WorkOrderID);
                if (model.Sample.SampleCollectionId == 0)
                {
                    BALFactory.samplecollectionBAL.AddSampleCollection(CoreFactory.samplecollectionEntity);
                    return Json(new { status = "success", message = "Sample Collection Added" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    BALFactory.samplecollectionBAL.Update(CoreFactory.samplecollectionEntity);
                    return Json(new { status = "success", message = "Sample Collection updated." }, JsonRequestBehavior.AllowGet);
                }


            }
            BALFactory.sampleParameterBAL.AddEnquiryParameterDetail(CoreFactory.enquiryParameterList);
            BALFactory.sampleParameterBAL.UpdateEnquirySampleCharges(model.WorkOrderCustomer.EnquirySampleID, model.WorkOrderCustomer.TotalCharges);
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _Parameters(int? EnquiryDetailId = 0, int? EnquirySampleID = 0, int? SampleTypeProductId = 0)
        {
            WorkOrderCustomerListModel model = new WorkOrderCustomerListModel();
            model.WorkOrderCustomer = new SampleRegistrationModel();
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
            model.WorkOrderCustomer = new SampleRegistrationModel();
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
            model.WorkOrderCustomer = new SampleRegistrationModel();
            model.WorkOrderCustomerList = new List<SampleRegistrationModel>();
            model.ParameterList = new List<ParameterModel>();
            model.CostList = new List<SampleRegistrationModel>();
            ViewBag.WorkOrderId = WorkOrderId;
            model.WorkOrderCustomer.WorkOrderID = (Int32)WorkOrderId;
            model.WorkOrderCustomer.CostingId = CostingId;
            
           
                CoreFactory.enquirySampleList = BALFactory.workordercustomerBAL.GetWorkOrderCustomerSampleList((Int32)WorkOrderId);


                int i = 1;
                foreach (var sample in CoreFactory.enquirySampleList)
                {
                    model.WorkOrderCustomerList.Add(new SampleRegistrationModel()
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
                    model.CostList.Add(new SampleRegistrationModel()
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
                        WorkOrderID = model.WorkOrderCustomer.WorkOrderID,
                        TermAndCondtId = term.TermAndCondtId,
                        EnteredBy = LIMS.User.UserMasterID
                    });
                }
            }
            BALFactory.costingBAL.AddQuotationTermsAndCondition(CoreFactory.quotTermList, 0, model.WorkOrderCustomer.Remarks);
            return Json(new { status = "success", WorkOrderId = model.WorkOrderCustomer.WorkOrderID }, JsonRequestBehavior.AllowGet);
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
                //-----------------------------------------------------------------------
                CoreFactory.quotationEntity = new Core.Enquiry.QuotationEntity();
                
                CoreFactory.quotationEntity.EnquiryId = model.EnquiryId;
                CoreFactory.quotationEntity.QuotedAmount = BALFactory.costingBAL.GetQuotedAmount(model.EnquiryId);
                if (CoreFactory.quotationEntity.QuotedAmount == 0 || CoreFactory.quotationEntity.QuotedAmount == null)
                {
                    return Json(new { status = "Fail" }, JsonRequestBehavior.AllowGet);
                }
                CoreFactory.quotationEntity.EnteredBy = LIMS.User.UserMasterID;
                long QuotationId = BALFactory.costingBAL.AddQuotation(CoreFactory.quotationEntity);
                //-----------------------------------------------------------------------------------
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
        public JsonResult AddCosting(CostingListModel model)
        {
            CoreFactory.quotationEntity = new Core.Enquiry.QuotationEntity();

            CoreFactory.quotationEntity.EnquiryId = model.EnquiryId;
            CoreFactory.quotationEntity.QuotedAmount = BALFactory.costingBAL.GetQuotedAmount(model.EnquiryId);
            if (CoreFactory.quotationEntity.QuotedAmount == 0 || CoreFactory.quotationEntity.QuotedAmount == null)
            {
                return Json(new { status = "Fail" }, JsonRequestBehavior.AllowGet);
            }
            CoreFactory.quotationEntity.EnteredBy = LIMS.User.UserMasterID;
            long QuotationId = BALFactory.costingBAL.AddQuotation(CoreFactory.quotationEntity);

            CoreFactory.quotTermList = new List<Core.Enquiry.QuotationTNCEntity>();
            foreach (var term in model.TermsList)
            {
                if (term.IsSelected)
                {
                    CoreFactory.quotTermList.Add(new Core.Enquiry.QuotationTNCEntity()
                    {
                        QuotationId = QuotationId,
                        TermAndCondtId = term.TermAndCondtId,
                        EnteredBy = LIMS.User.UserMasterID
                    });
                }
            }

            BALFactory.costingBAL.AddQuotationTermsAndCondition(CoreFactory.quotTermList, QuotationId, model.Remarks);

            ///////////Added by Nivedita Suggested By Rajesh Sir////////////////////
            ///Note:check QuotationNo First and then Generate it//////////////////
            var QuotsDetail = BALFactory.quotationBAL.GetQuotationDetails(model.EnquiryId);
            if (QuotsDetail.QuotationNo == null || QuotsDetail.QuotationNo == "")
            {
                BALFactory.enquiryBAL.GenerateQuotationNo(QuotsDetail.QuotationId);
                int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("QuotCreate");
                BALFactory.enquiryBAL.UpdateEnquiryStatus(model.EnquiryId, (byte)iStatusId);
            }


            return Json(new { status = "success", EnquiryId = model.EnquiryId }, JsonRequestBehavior.AllowGet);
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
                return RedirectToAction("SampleCollectionCalendar", "ManageSampleCollection", new { area = "ManageSampleCollection" });


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
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public PartialViewResult _AddWorkOrderDetails(int? WorkOrderId = 0)
        {
            WorkOrderCustomerListModel model = new WorkOrderCustomerListModel();
            model.WorkOrderCustomer = new SampleRegistrationModel();
            model.FinalWorkOrderList = new List<FinalWorkOrderModel>();
            model.WorkOrderCustomer.WorkOrderID = (Int32)WorkOrderId;

            if (WorkOrderId != 0)//Changes by Nivedita for Major change may be removed later
            {
                CoreFactory.workOrderEntity = BALFactory.workordercustomerBAL.GetWorkOrderCustomerDetail((Int32)WorkOrderId);
                if (CoreFactory.workOrderEntity != null)
                {
                    model.WorkOrderCustomer.WorkOrderID = (Int32)CoreFactory.workOrderEntity.WorkOrderId;
                    model.WorkOrderCustomer.CustomerName = CoreFactory.workOrderEntity.RegistrationName;
                    model.WorkOrderCustomer.WorkOrderReceivedDate = CoreFactory.workOrderEntity.WORecieveDate;
                    model.WorkOrderCustomer.WORecvDate = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderReceivedDate).ToString("dd/MM/yyyy");
                    model.WorkOrderCustomer.WorkOrderEndDate = CoreFactory.workOrderEntity.WOEndDate;
                    model.WorkOrderCustomer.WOEDate = Convert.ToDateTime(model.WorkOrderCustomer.WorkOrderEndDate).ToString("dd/MM/yyyy");
                    model.WorkOrderCustomer.SampleCollectionDate = CoreFactory.workOrderEntity.ExpectSampleCollDate;
                    model.WorkOrderCustomer.expSampleDate = Convert.ToDateTime(model.WorkOrderCustomer.SampleCollectionDate);
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
            CoreFactory.WorkOrderCustomerEntity.WorkOrderId = model.WorkOrderCustomer.WorkOrderID;
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
                        WorkOrderId = model.WorkOrderCustomer.WorkOrderID,
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
             BALFactory.workOrderBAL.WorkOrderApprove(model.WorkOrderCustomer.WorkOrderID, model.WorkOrderCustomer.EnquiryId,36, LIMS.User.UserMasterID);
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
        public PartialViewResult _AddSampleArrival(int WorkOrderID=0)
        {
            ViewBag.WorkOrderId = WorkOrderID;
            var EnteredBy = CoreFactory.userEntity.UserMasterID;
            CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetERFArrivalList(WorkOrderID);
            List<SampleRegistrationModel> modelList = new List<SampleRegistrationModel>();

            int iSrNo = 1;
            foreach (var Item in CoreFactory.samplearrivalList)
            {
                modelList.Add(new SampleRegistrationModel()
                {
                    LocationSampleCollectionID = Item.LocationSampleCollectionID,
                    SampleTypeProductId = Item.SampleTypeProductId,
                    Url = Item.SampleTypeProductName,
                    WorkOrderID = (int)Item.WorkOrderID,
                    SerialNo = iSrNo,
                    //ARCId = Item.ARCId,
                    SampleCollectionId = Item.SampleCollectionId,
                    EnquirySampleID = (long)Item.EnquirySampleID,
                    EnquiryDetailId = Item.EnquiryDetailId,
                    SampleNameOriginal = Item.SampleNameOriginal,
                    SampleTypeProductName = Item.SampleTypeProductName,
                    SampleLocation = Item.SampleLocation,
                    //EnquiryId = Item.EnquiryId,
                    //EmployeeId = Item.EmployeeId, //RoleId,UserName,CollectorName
                    SampleNo = Item.SampleNo,//to be Changed to SampleName wrt Iteration Number,
                                             //ULRNo = Item.ULRNo,//doubt for backend storage only
                    SampleName = Item.SampleName,// FieldDeterminationId = Item.FieldDeterminationId,//Doubt
                    RequestNo = Item.RequestNo,
                    //ContactNO = Item.ContactNO,
                    CustomerName = Item.CustomerName,
                    //StatusId = Item.StatusId,
                    CurrentStatus = Item.CurrentStatus,//SampleReceived
                    CollectionDate = Item.CollectionDate,
                    Date = Convert.ToDateTime(Item.CollectionDate),
                   
                    MatrixName = Item.MatrixName,
                    //Url = Item.MatrixName,//Url = "Air",//Url=ProductGrp+SubGrp+Matrix
                }); 
                iSrNo++;

            }
            return PartialView(modelList);

        }
        public ActionResult TRFSampleArrivalList(int SampleCollectionId,int SampleTypeProductId,int EnquirySampleID,int WorkOrderId,string SampleLocation)
        {
            CollectionListModel model = new CollectionListModel();
            ViewBag.SampleCollectionId = SampleCollectionId;
            ViewBag.EnquirySampleID = (long)EnquirySampleID;
            ViewBag.SampleLocation = SampleLocation;
            ViewBag.WorkOrderId = WorkOrderId;
            ViewBag.SampleDeviceList = BALFactory.dropdownsBAL.GetSampleDevice(SampleTypeProductId); //Passing Data to Viewbag for dropdown
            ViewBag.ProcedureList = BALFactory.dropdownsBAL.GetProcedure(SampleTypeProductId);
            ViewBag.SampleTypeList = BALFactory.dropdownsBAL.GetSampleType();
            ViewBag.EnvCondtsList = BALFactory.dropdownsBAL.GetEnvironmentalCondition();

            //CoreFactory.quantityPreservativeList = new List<QuantityPreservativeEntity>();////For  QuantityPreservative List
            //foreach (var item in model.CollectionList)
            //{
            //    if (item.IsSelected)
            //    {
            //        CoreFactory.quantityPreservativeList.Add(new QuantityPreservativeEntity()
            //        {
            //            QtyPreservativeId = item.QtyPreservativeId,
            //            SampleCollectionId = model.Collection.SampleCollectionId,
            //            SampleQtyId = (Int32)item.SampleQtyId,
            //            ISGivenPreservative = item.ISGivenPreservative,
            //            No = item.No,
            //            IsActive = true,
            //            EnteredBy = LIMS.User.UserMasterID,
            //            EnteredDate = DateTime.Now,
            //        });
            //    }
            //}
            model.CollectionList = new List<SampleCollectionModel>();
            CoreFactory.quantityPreservativeList = BALFactory.samplecollectionBAL.GetSampleQty(SampleTypeProductId);/////For QuantityPreservativeGrid
            CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetArrivalQtyPreservativeList((Int32)SampleCollectionId);

            var dtoJson = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(CoreFactory.samplearrivalList);
            ViewBag.ListViewBag = dtoJson;

            int iSrNo = 1;
            foreach (var item in CoreFactory.quantityPreservativeList)
            {
                model.CollectionList.Add(new SampleCollectionModel()
                {
                    SerialNo = iSrNo,
                    SampleQtyId = item.SampleQtyId,
                    SampleTypeProductId = item.SampleTypeProductId,
                    SampleQty = item.SampleQty,
                    Preservation = item.Preservation,
                    WorkOrderID =item.WorkOrderID,
                    No = item.No,
                });
                iSrNo++;
            }

            return PartialView(model);
        }
        
        [HttpPost]
        public JsonResult AddSampleArrival(CollectionListModel model, string Save)
        {
            CoreFactory.samplearrivalEntity = new SampleArrivalEntity();
            CoreFactory.samplearrivalEntity.SampleCollectionId = model.Collection.SampleCollectionId;
            CoreFactory.samplearrivalEntity.WorkOrderID = model.Collection.WorkOrderID;
            CoreFactory.samplearrivalEntity.EnvCondtId = model.Collection.EnvCondtId;
            CoreFactory.samplearrivalEntity.SubContractedParameters = model.Collection.SubContractedParameters;
            CoreFactory.samplearrivalEntity.AckRemarks = model.Collection.AckRemarks;
            CoreFactory.samplearrivalEntity.ReturnedRemark = model.Collection.ReturnedRemarks;
            CoreFactory.samplearrivalEntity.IsReturnedOrIsRetained = model.Collection.IsReturnedOrIsRetained;
            CoreFactory.samplearrivalEntity.IsSampleIntact = model.Collection.IsSampleIntact;
            CoreFactory.samplearrivalEntity.CustomerName = model.Collection.CustomerName;
            CoreFactory.samplearrivalEntity.Date = model.Collection.CollectionDate;
            CoreFactory.samplearrivalEntity.EmployeeId = (LIMS.User.UserMasterID).ToString();
            CoreFactory.samplearrivalEntity.SampleTypeId = model.Collection.SampleTypeId;
            CoreFactory.samplearrivalEntity.SampleLocation = model.Collection.SampleLocation;
            CoreFactory.samplearrivalEntity.EnteredBy = LIMS.User.UserMasterID;
            List<int> DeviceCount = model.Collection.SampleDeviceId;
            if (DeviceCount != null)
            {
                CoreFactory.sampleDeviceList = new List<SampleDeviceEntity>();////For SampleDevice Multiselect List
                for (int i = 0; i < model.Collection.SampleDeviceId.Count; i++)
                {
                    CoreFactory.sampleDeviceList.Add(new SampleDeviceEntity()
                    {
                        SampleCollectionDevicesId = model.Collection.SampleCollectionDevicesId,
                        SampleCollectionId = model.Collection.SampleCollectionId,
                        SampleDeviceId = model.Collection.SampleDeviceId[i],
                        IsActive = true,
                        EnteredBy = LIMS.User.UserMasterID,
                        EnteredDate = DateTime.Now,
                    });
                }
                BALFactory.samplecollectionBAL.AddSampleDevice(CoreFactory.sampleDeviceList);
            }
            List<int> ProcedureCount = model.Collection.ProcedureId;
            if (ProcedureCount != null)
            {
                CoreFactory.procedureList = new List<ProcedureEntity>();////For Procedure Multiselect List
                for (int i = 0; i < model.Collection.ProcedureId.Count; i++)
                {
                    CoreFactory.procedureList.Add(new ProcedureEntity()
                    {
                        SampleCollectionProcedureId = model.Collection.SampleCollectionProcedureId,
                        SampleCollectionId = model.Collection.SampleCollectionId,
                        ProcedureId = model.Collection.ProcedureId[i],
                        IsActive = true,
                        EnteredBy = LIMS.User.UserMasterID,
                        EnteredDate = DateTime.Now,
                    });
                }

                BALFactory.samplecollectionBAL.AddSampleProcedure(CoreFactory.procedureList);
            }
            CoreFactory.quantityPreservativeList = new List<QuantityPreservativeEntity>();////For  QuantityPreservative List
            foreach (var item in model.CollectionList)
            {
                if (item.IsSelected)
                {
                    CoreFactory.quantityPreservativeList.Add(new QuantityPreservativeEntity()
                    {
                        QtyPreservativeId = item.QtyPreservativeId,
                        SampleCollectionId = model.Collection.SampleCollectionId,
                        SampleQtyId = (Int32)item.SampleQtyId,
                        ISGivenPreservative = item.ISGivenPreservative,
                        No = item.No,
                        IsActive = true,
                        EnteredBy = LIMS.User.UserMasterID,
                        EnteredDate = DateTime.Now,
                    });
                }
            }
            BALFactory.samplecollectionBAL.AddQuantityPreservative(CoreFactory.quantityPreservativeList, model.Collection.EnquirySampleID, model.Collection.WorkOrderID);
            BALFactory.samplecollectionBAL.AddARC((Int32)model.Collection.SampleCollectionId, LIMS.User.UserMasterID);
            CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetArrivalQtyPreservativeList((Int32)model.Collection.SampleCollectionId);

            BALFactory.samplecollectionBAL.TRFEnv_Update(CoreFactory.samplearrivalEntity);
            return Json(new { status = "success", message = "Sample Collection updated." }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult _SampleDevicesList(int SampleCollectionId)
        {
            CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetCollectionDevicesList(SampleCollectionId);
            List<SampleRegistrationModel> modelList = new List<SampleRegistrationModel>();
            int iSrNo = 1;
            foreach (var item in CoreFactory.samplearrivalList)
            {
                modelList.Add(new SampleRegistrationModel()
                {
                    SerialNo = iSrNo,
                    SampleCollectionId = item.SampleCollectionId,
                    SampleCollectionDevicesId = item.SampleCollectionDevicesId,
                    SampleDeviceId = item.SampleDeviceId,
                    SampleDevice = item.SampleDevice,

                });
                iSrNo++;
            }
            return PartialView(modelList);
        }
        public ActionResult SampleReturnedList()
        {
            CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetSampleReturnedList();
            List<SampleRegistrationModel> modelList = new List<SampleRegistrationModel>();

            int iSrNo = 1;
            foreach (var Item in CoreFactory.samplearrivalList)
            {
                if (Item.IsReturnedOrIsRetained == "Returned")
                {
                    modelList.Add(new SampleRegistrationModel()
                    {
                        SerialNo = iSrNo,
                        SampleTypeProductName = Item.SampleTypeProductName,
                        SampleNameOriginal = Item.SampleNameOriginal,
                        CustomerName = Item.CustomerName,
                        //ContactNO = Item.ContactNO,
                        ARCId = Item.ARCId,
                        //DateofRecieptofSample = Convert.ToDateTime(Item.ActionDate).ToString("dd/MM/yyyy"),
                        ReturnedDate = Item.ReturnedDate,
                        returnDate = Convert.ToDateTime(Item.ReturnedDate),
                        ProbableDateOfReport = Item.ProbableDateOfReport,
                        PDR = Convert.ToDateTime(Item.ProbableDateOfReport),
                        ReturnedRemarks = Item.ReturnedRemark,
                        StatusId = Item.StatusId,
                        CurrentStatus = Item.CurrentStatus,//SampleReceived
                    }); ;

                    iSrNo++;
                }
            }
            return View(modelList);
        }
    }
}