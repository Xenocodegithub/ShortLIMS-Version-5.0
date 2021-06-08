using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Collection.Models;
using LIMS_DEMO.Areas.Arrival.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Collection;
using LIMS_DEMO.Core.Arrival;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Collection;
using LIMS_DEMO.BAL.Arrival;
using LIMS_DEMO.Common;
using LIMS_DEMO.BAL.Customer;
using LIMS_DEMO.BAL.Enquiry;
using LIMS_DEMO.BAL.DropDown;

namespace LIMS_DEMO.Areas.Collection.Controllers
{
    [RouteArea("Collection")]
    public class SampleCollectionController : Controller
    {
        int a = 0;
        public SampleCollectionController()
        {
            BALFactory.samplearrivalBAL = new SampleArrivalBAL();
            BALFactory.samplecollectionBAL = new SampleCollectionBAL();
            BALFactory.enquiryBAL = new EnquiryBAL();
            BALFactory.customerBAL = new CustomerBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
        }

        // GET: Collection/SampleCollection
        public ActionResult CollectorList(int CollectedBy = 0)
        {
            CoreFactory.SamplecollectionList = BALFactory.samplecollectionBAL.GetCollectionList(LIMS.User.UserMasterID, CollectedBy);
            List<SampleCollectionModel> modelList = new List<SampleCollectionModel>();

            foreach (var Item in CoreFactory.SamplecollectionList)
            {
                if (Item.StatusCodeLab == "CollByCust" || Item.StatusCodeLab == "CollAssign" || Item.StatusCodeLab == "SampleColl" || Item.StatusCodeLab == "SampleSub" || Item.StatusCodeLab == "InProgress")
                {
                    if (Item.Iteration == 1)
                    {
                        modelList.Add(new SampleCollectionModel()
                        {
                            CollBy = CollectedBy,
                            SampleCollectionId = Item.SampleCollectionId,
                            EnquirySampleID = Item.EnquirySampleID,
                            EnquiryDetailId = Item.EnquiryDetailId,
                            Location = Item.Location,
                            SampleName = Item.SampleName,
                            DisplaySampleName = Item.DisplaySampleName,
                            EnquiryId = Item.EnquiryId,
                            CustomerName = Item.CustomerName,
                            ContactNO = Item.ContactNO,
                            CurrentStatus = Item.CurrentStatus,
                            //ExpectSampleCollDate = Item.ExpectSampleCollDate,//Sample to be Collected On
                            Iteration = Item.Iteration,
                            //CollectedOn = Item.ExpectSampleCollDate.ToString("dd/MM/yyyy"),
                            CollectedOn = Item.CollectedOn,
                            ULRNo = Item.ULRNo,//Doubt
                            FieldDeterminationId = Item.FieldDeterminationId,//Doubt
                            StatusCodeLab = Item.StatusCodeLab,
                            StatusCodeField = Item.StatusCodeField,
                            LocationSampleCollectionID = Item.LocationSampleCollectionID,
                            //MatrixName = Item.MatrixName,
                            SampleTypeProductName = Item.SampleTypeProductName,
                            Url = Item.SampleTypeProductName,//Url = "Air",//Url=ProductGrp+SubGrp+Matrix
                        }); ;
                        ViewBag.StatusCodeLab = Item.StatusCodeLab;
                        ViewBag.StatusCodeField = Item.StatusCodeField;
                        ViewBag.MatrixName = Item.MatrixName;
                    }
                    else
                    {
                        modelList.Add(new SampleCollectionModel()
                        {
                            CollBy = CollectedBy,
                            SampleCollectionId = Item.SampleCollectionId,
                            EnquirySampleID = Item.EnquirySampleID,
                            EnquiryDetailId = Item.EnquiryDetailId,
                            Location = Item.Location,
                            SampleName = Item.SampleName,
                            DisplaySampleName = Item.DisplaySampleName,
                            EnquiryId = Item.EnquiryId,
                            CustomerName = Item.CustomerName,
                            ContactNO = Item.ContactNO,
                            CurrentStatus = Item.CurrentStatus,
                            //ExpectSampleCollDate = Item.ExpectSampleCollDate,//Sample to be Collected On
                            Iteration = Item.Iteration,
                            //CollectedOn = Item.ExpectSampleCollDate.ToString("dd/MM/yyyy"),
                            CollectedOn = Item.CollectedOn,
                            ULRNo = Item.ULRNo,//Doubt
                            FieldDeterminationId = Item.FieldDeterminationId,//Doubt
                            StatusCodeLab = Item.StatusCodeLab,
                            StatusCodeField = Item.StatusCodeField,
                            LocationSampleCollectionID = Item.LocationSampleCollectionID,
                            //MatrixName = Item.MatrixName,
                            SampleTypeProductName = Item.SampleTypeProductName,
                            Url = Item.SampleTypeProductName,//Url = "Air",//Url=ProductGrp+SubGrp+Matrix
                        }); ;
                        ViewBag.StatusCodeLab = Item.StatusCodeLab;
                        ViewBag.StatusCodeField = Item.StatusCodeField;
                        ViewBag.MatrixName = Item.MatrixName;
                    }
                }
            }

            return View(modelList);
        }

        public ActionResult CollectorListCustomer(int CollectedBy = 0)
        {
            CoreFactory.SamplecollectionList = BALFactory.samplecollectionBAL.GetCollectionList(LIMS.User.UserMasterID, CollectedBy);
            List<SampleCollectionModel> modelList = new List<SampleCollectionModel>();
            foreach (var Item in CoreFactory.SamplecollectionList)
            {
                if (Item.StatusCodeLab == "CollByCust" || Item.StatusCodeLab == "CollAssign" || Item.StatusCodeLab == "SampleColl" || Item.StatusCodeLab == "SampleSub" || Item.StatusCodeLab == "InProgress")
                {
                    if (Item.Iteration == 1)
                    {
                        modelList.Add(new SampleCollectionModel()
                        {
                            CollBy = CollectedBy,
                            SampleCollectionId = Item.SampleCollectionId,
                            EnquirySampleID = Item.EnquirySampleID,
                            EnquiryDetailId = Item.EnquiryDetailId,
                            Location = Item.Location,
                            SampleName = Item.SampleName,
                            DisplaySampleName = Item.DisplaySampleName,
                            EnquiryId = Item.EnquiryId,
                            CustomerName = Item.CustomerName,
                            ContactNO = Item.ContactNO,
                            CurrentStatus = Item.CurrentStatus,
                            //ExpectSampleCollDate = Item.ExpectSampleCollDate,//Sample to be Collected On
                            Iteration = Item.Iteration,
                            //CollectedOn = Item.ExpectSampleCollDate.ToString("dd/MM/yyyy"),
                            CollectedOn = Item.CollectedOn,
                            ULRNo = Item.ULRNo,//Doubt
                            FieldDeterminationId = Item.FieldDeterminationId,//Doubt
                            StatusCodeLab = Item.StatusCodeLab,
                            StatusCodeField = Item.StatusCodeField,
                            LocationSampleCollectionID = Item.LocationSampleCollectionID,
                            //MatrixName = Item.MatrixName,
                            SampleTypeProductName = Item.SampleTypeProductName,
                            Url = Item.SampleTypeProductName,//Url = "Air",//Url=ProductGrp+SubGrp+Matrix
                        }); ;
                        ViewBag.StatusCodeLab = Item.StatusCodeLab;
                        ViewBag.StatusCodeField = Item.StatusCodeField;
                        ViewBag.MatrixName = Item.MatrixName;
                    }
                    else
                    {
                        modelList.Add(new SampleCollectionModel()
                        {
                            CollBy = CollectedBy,
                            SampleCollectionId = Item.SampleCollectionId,
                            EnquirySampleID = Item.EnquirySampleID,
                            EnquiryDetailId = Item.EnquiryDetailId,
                            Location = Item.Location,
                            SampleName = Item.SampleName,
                            DisplaySampleName = Item.DisplaySampleName,
                            EnquiryId = Item.EnquiryId,
                            CustomerName = Item.CustomerName,
                            ContactNO = Item.ContactNO,
                            CurrentStatus = Item.CurrentStatus,
                            //ExpectSampleCollDate = Item.ExpectSampleCollDate,//Sample to be Collected On
                            Iteration = Item.Iteration,
                            //CollectedOn = Item.ExpectSampleCollDate.ToString("dd/MM/yyyy"),
                            CollectedOn = Item.CollectedOn,
                            ULRNo = Item.ULRNo,//Doubt
                            FieldDeterminationId = Item.FieldDeterminationId,//Doubt
                            StatusCodeLab = Item.StatusCodeLab,
                            StatusCodeField = Item.StatusCodeField,
                            LocationSampleCollectionID = Item.LocationSampleCollectionID,
                            //MatrixName = Item.MatrixName,
                            SampleTypeProductName = Item.SampleTypeProductName,
                            Url = Item.SampleTypeProductName,//Url = "Air",//Url=ProductGrp+SubGrp+Matrix
                        }); ;
                        ViewBag.StatusCodeLab = Item.StatusCodeLab;
                        ViewBag.StatusCodeField = Item.StatusCodeField;
                        ViewBag.MatrixName = Item.MatrixName;
                    }
                }
            }
            return View(modelList);
        }

        public ActionResult AddSampleCollection(int? SampleCollectionId = 0, int? EnquirySampleID = 0)
        {
            CollectionListModel model = new CollectionListModel();
            model.Collection = new SampleCollectionModel();
            model.CollectionList = new List<SampleCollectionModel>();
            model.DeviceList = new List<SampleCollectionModel>();
            model.ProcedureList = new List<SampleCollectionModel>();
            ViewBag.SampleCollectionId = SampleCollectionId;
            model.Collection.EnquirySampleID = (long)EnquirySampleID;

            if (SampleCollectionId != 0)
            {
                CoreFactory.samplecollectionEntity = BALFactory.samplecollectionBAL.GetSampleCollectionDetails((Int32)SampleCollectionId);
                model.Collection.LocationSampleCollectionID = CoreFactory.samplecollectionEntity.LocationSampleCollectionID;
                model.Collection.WorkOrderSampleCollectionDateId = CoreFactory.samplecollectionEntity.WorkOrderSampleCollectionDateId;
                model.Collection.SampleTypeProductId = CoreFactory.samplecollectionEntity.SampleTypeProductId;
                model.Collection.SampleTypeProductName = CoreFactory.samplecollectionEntity.SampleTypeProductName;
                model.Collection.SampleCollectionId = CoreFactory.samplecollectionEntity.SampleCollectionId;
                model.Collection.EnquirySampleID = CoreFactory.samplecollectionEntity.EnquirySampleID;
                model.Collection.EnquiryDetailId = CoreFactory.samplecollectionEntity.EnquiryDetailId;
                model.Collection.StatusCodeLab = CoreFactory.samplecollectionEntity.StatusCodeLab;//FoLabStatus
                ViewBag.StatusCodeLab = model.Collection.StatusCodeLab;
                model.Collection.StatusCodeField = CoreFactory.samplecollectionEntity.StatusCodeField;//ForFieldStatus
                //model.Collection.ProductGroupId = CoreFactory.samplecollectionEntity.ProductGroupId;
                //model.Collection.SubGroupId = CoreFactory.samplecollectionEntity.SubGroupId;
                //model.Collection.MatrixName = CoreFactory.samplecollectionEntity.MatrixName;
                //model.Collection.SampleDescriptionId = CoreFactory.samplecollectionEntity.SampleDescriptionId;
                model.Collection.SampleDescription = CoreFactory.samplecollectionEntity.SampleDescription;
                model.Collection.SampleTypeId = CoreFactory.samplecollectionEntity.SampleTypeId;
                model.Collection.EnvCondtId = CoreFactory.samplecollectionEntity.EnvCondtId;
                //model.Collection.ProcedureId = CoreFactory.samplecollectionEntity.ProcedureId;
                model.Collection.SampleNo = CoreFactory.samplecollectionEntity.SampleNo;
                model.CollectionDate = CoreFactory.samplecollectionEntity.CollectionDate;
                model.CollDate = CoreFactory.samplecollectionEntity.CollectionDate;

                if (CoreFactory.samplecollectionEntity.SampleLocation != null)
                {
                    model.Collection.SampleLocation = CoreFactory.samplecollectionEntity.SampleLocation;
                    model.Collection.Location = CoreFactory.samplecollectionEntity.Location;
                }
                else
                {
                    model.Collection.Location = CoreFactory.samplecollectionEntity.Location;
                }

                model.Collection.Source = CoreFactory.samplecollectionEntity.Source;

                if (CoreFactory.samplecollectionEntity.IsWitness == null)
                {
                    model.Collection.IsWitness = false;
                }
                else
                {
                    model.Collection.IsWitness = (bool)CoreFactory.samplecollectionEntity.IsWitness;
                }

                model.Collection.WitnessName = CoreFactory.samplecollectionEntity.WitnessName;
                model.Collection.IndustryType = CoreFactory.samplecollectionEntity.IndustryType;
                model.Collection.Iteration = CoreFactory.samplecollectionEntity.Iteration;

                if (model.Collection.Iteration == 1)
                {
                    model.Collection.CollectedOn = BALFactory.samplecollectionBAL.GetExpectedCollDate((Int32)CoreFactory.samplecollectionEntity.WorkOrderSampleCollectionDateId);
                    var x = model.Collection.CollectedOn;
                    string d = x.Substring(x.Length - 2, 2);
                    string m = x.Substring(x.Length - 5, 2);
                    string y = x.Substring(0, 4);
                    model.Collection.CollectedOn = string.Concat(d, "/", m, "/", y);
                }
                //else
                //{
                //model.Collection.CollectionDate = CoreFactory.samplecollectionEntity.CollectionDate;
                //}

                model.Collection.SampleCollectionTime = CoreFactory.samplecollectionEntity.SampleCollectionTime;
                model.Collection.Duration = CoreFactory.samplecollectionEntity.Duration;
                model.Collection.StartTime = CoreFactory.samplecollectionEntity.StartTime;
                model.Collection.EndTime = CoreFactory.samplecollectionEntity.EndTime;
                model.Collection.Remark = CoreFactory.samplecollectionEntity.Remark;
                model.Collection.SampleCollectedBy = CoreFactory.samplecollectionEntity.SampleCollectedBy;
                a = (Int32)model.Collection.SampleCollectedBy;
                if (a == 1)
                {
                    model.Collection.CollectedBy = "LAB";
                    model.Collection.EmployeeId = LIMS.User.UserName;
                }
                if (a == 2)
                {
                    model.Collection.CollectedBy = "Customer";
                    model.Collection.EmployeeId = "N/A";
                }
                //model.Collection.EmployeeId = CoreFactory.samplecollectionEntity.EmployeeId;
                model.Collection.EnquiryId = CoreFactory.samplecollectionEntity.EnquiryId;
                model.Collection.WorkOrderID = CoreFactory.samplecollectionEntity.WorkOrderID;
                model.Collection.WorkOrderNo = CoreFactory.samplecollectionEntity.WorkOrderNo;
                model.Collection.CustomerName = CoreFactory.samplecollectionEntity.CustomerName;
                model.Collection.CustomerMasterId = CoreFactory.samplecollectionEntity.CustomerMasterId;
                model.Collection.Address = CoreFactory.samplecollectionEntity.Address;
                model.Collection.ContactNO = CoreFactory.samplecollectionEntity.ContactNO;

                CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetCollectionDevicesList((Int32)model.Collection.SampleCollectionId); //for sampledevice
                int iSrNo2 = 1;
                foreach (var item in CoreFactory.samplearrivalList)
                {
                    model.DeviceList.Add(new SampleCollectionModel()
                    {
                        SerialNo2 = iSrNo2,
                        SampleCollectionId = item.SampleCollectionId,
                        SampleCollectionDevicesId = item.SampleCollectionDevicesId,
                        SampleDeviceId2 = (Int32)item.SampleDeviceId1,
                        SampleDevice = item.SampleDevice,
                    });
                    iSrNo2++;
                }

                CoreFactory.procedureList = BALFactory.samplecollectionBAL.GetCollectionProcedureList((Int32)model.Collection.SampleCollectionId); //for sampledevice
                int iSrNo3 = 1;
                foreach (var item in CoreFactory.procedureList)
                {
                    model.ProcedureList.Add(new SampleCollectionModel()
                    {
                        SerialNo2 = iSrNo3,
                        SampleCollectionId = item.SampleCollectionId,
                        SampleCollectionProcedureId = item.SampleCollectionProcedureId,
                        ProcedureId2 = (Int32)item.ProcedureId,
                        ProcedureName = item.Procedure,
                    });
                    iSrNo3++;
                }
                //CoreFactory.enquiryEntity = BALFactory.enquiryBAL.GetDetails((Int32)model.Collection.EnquiryId);
                //model.Collection.CustomerName = CoreFactory.enquiryEntity.CustomerName;
                //model.Collection.CustomerMasterId = CoreFactory.enquiryEntity.CustomerMasterId;
                //CoreFactory.customerEntity = BALFactory.customerBAL.GetDetails((Int32)model.Collection.CustomerMasterId);
                //model.Collection.Address = CoreFactory.customerEntity.Address1;
                //model.Collection.ContactNO = CoreFactory.customerEntity.ContactMobileNo;
            }
            //Doubt need to replace ProductGroupId with SampleTypeProductId
            CoreFactory.quantityPreservativeList = BALFactory.samplecollectionBAL.GetSampleQty(model.Collection.SampleTypeProductId);/////For QuantityPreservativeGrid
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
                    No = item.No,
                });
                iSrNo++;
            }

            ViewBag.SampleDescriptionList = BALFactory.dropdownsBAL.GetSampleDescription(model.Collection.ProductGroupId, model.Collection.SubGroupId);//Passing Data to Viewbag for dropdown
            //Doubt need to replace ProductGroupId with SampleTypeProductId
             ViewBag.SampleDeviceList = BALFactory.dropdownsBAL.GetSampleDevice(model.Collection.SampleTypeProductId);//Passing Data to Viewbag for dropdown
            ViewBag.ProcedureList = BALFactory.dropdownsBAL.GetProcedure(model.Collection.SampleTypeProductId);
            ViewBag.SampleTypeList = BALFactory.dropdownsBAL.GetSampleType();
            ViewBag.EnvCondtsList = BALFactory.dropdownsBAL.GetEnvironmentalCondition();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddSampleCollection(CollectionListModel model, string Save)
        {
            CoreFactory.samplecollectionEntity = new SampleCollectionEntity();
            CoreFactory.samplecollectionEntity.SampleCollectionId = model.Collection.SampleCollectionId;
            CoreFactory.samplecollectionEntity.EnquirySampleID = model.Collection.EnquirySampleID;
            CoreFactory.samplecollectionEntity.EnquiryDetailId = model.Collection.EnquiryDetailId;
            CoreFactory.samplecollectionEntity.EnquiryId = model.Collection.EnquiryId;
            CoreFactory.samplecollectionEntity.WorkOrderID = model.Collection.WorkOrderID;
            //CoreFactory.samplecollectionEntity.StatusCodeLab = model.Collection.StatusCodeLab; //FoLabStatus
            //CoreFactory.samplecollectionEntity.StatusCodeField = model.Collection.StatusCodeField; //ForFieldStatus
            List<int> DeviceCount = model.Collection.SampleDeviceId;
            if (model.Collection.CollectedBy == "LAB" && (DeviceCount == null || model.Collection.SampleTypeId == 0 || model.Collection.EnvCondtId == 0))
            {
                return Json(new { status = "Fail" }, JsonRequestBehavior.AllowGet);
            }
            if (model.Collection.Iteration == 1)
            {
                //Update ExpectedSampleCollectionDate of Work Order tbl for iteration 1
                //CoreFactory.samplecollectionEntity.ExpectSampleCollDate=(DateTime)model.Collection.CollectionDate;
                //String SampleCollectionDate = BALFactory.samplecollectionBAL.UpdateExpectSampleCollDate(model.Collection.WorkOrderID, (DateTime)model.CollectionDate);
                if (model.CollectionDate != null)
                {
                    CoreFactory.samplecollectionEntity.CollectionDate = model.CollectionDate;
                }
                else
                {
                    CoreFactory.samplecollectionEntity.CollectionDate = model.CollDate;
                }

            }
            else
            {
                CoreFactory.samplecollectionEntity.CollectionDate = model.CollectionDate;
            }
            CoreFactory.samplecollectionEntity.SampleCollectionTime = model.Collection.SampleCollectionTime;
            CoreFactory.samplecollectionEntity.Duration = model.Collection.Duration;
            CoreFactory.samplecollectionEntity.StartTime = model.Collection.StartTime;
            CoreFactory.samplecollectionEntity.EndTime = model.Collection.EndTime;
            CoreFactory.samplecollectionEntity.SampleLocation = model.Collection.SampleLocation;
            CoreFactory.samplecollectionEntity.Remark = model.Collection.Remark;
            //CoreFactory.samplecollectionEntity.SampleDescriptionId = model.Collection.SampleDescriptionId;
            CoreFactory.samplecollectionEntity.SampleDescription = model.Collection.SampleDescription;
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
            CoreFactory.samplecollectionEntity.SampleTypeId = model.Collection.SampleTypeId;
            CoreFactory.samplecollectionEntity.SampleType = model.Collection.SampleType;
            CoreFactory.samplecollectionEntity.EnvCondtId = model.Collection.EnvCondtId;
            CoreFactory.samplecollectionEntity.EnvCondts = model.Collection.EnvCondts;
            //CoreFactory.samplecollectionEntity.ProcedureId = model.Collection.ProcedureId;
            //CoreFactory.samplecollectionEntity.ProcedureName = model.Collection.ProcedureName;
            CoreFactory.samplecollectionEntity.IsWitness = model.Collection.IsWitness;
            CoreFactory.samplecollectionEntity.WitnessName = model.Collection.WitnessName;
            CoreFactory.samplecollectionEntity.IndustryType = model.Collection.IndustryType;
            CoreFactory.samplecollectionEntity.Source = model.Collection.Source;
            //CoreFactory.samplecollectionEntity.SampleNo = model.Collection.SampleNo; //to be Changed to SampleName,
            CoreFactory.samplecollectionEntity.IsActive = true;
            CoreFactory.samplecollectionEntity.EnteredBy = LIMS.User.UserMasterID;

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

            //////For Save and Submit with Status Update///////
            if (Save == "SampleSave")
            {
                CoreFactory.samplecollectionEntity.mode = "SampleSave";
                int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("InProgress");
                BALFactory.samplecollectionBAL.UpdateCollectionStatus(model.Collection.SampleCollectionId, (byte)iStatusId);
                BALFactory.samplecollectionBAL.UpdateWorkOrderSampleCollectionDate((Int32)model.Collection.WorkOrderSampleCollectionDateId, (DateTime)CoreFactory.samplecollectionEntity.CollectionDate);
            }
            else if (Save == "SampleSubmit")
            {
                CoreFactory.samplecollectionEntity.mode = "SampleSubmit";
                int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
                BALFactory.samplecollectionBAL.UpdateCollectionStatus(model.Collection.SampleCollectionId, (byte)iStatusId);
                BALFactory.samplecollectionBAL.UpdateWorkOrderSampleCollectionDate((Int32)model.Collection.WorkOrderSampleCollectionDateId, (DateTime)CoreFactory.samplecollectionEntity.CollectionDate);
                string Msg = "Sample Collected";
                CoreFactory.samplecollectionEntity.SampleNo = model.Collection.SampleNo;
                long NotificationDetailId = BALFactory.samplecollectionBAL.AddNotification(Msg, "ADMIN", CoreFactory.samplecollectionEntity);
                long NotificationDetailId1 = BALFactory.samplecollectionBAL.AddNotification(Msg, "BDM", CoreFactory.samplecollectionEntity);
                long NotificationDetailId2 = BALFactory.samplecollectionBAL.AddNotification(Msg, "Sampling Incharge", CoreFactory.samplecollectionEntity);
                long NotificationDetailId3 = BALFactory.samplecollectionBAL.AddNotification(Msg, "Sampler", CoreFactory.samplecollectionEntity);
                long NotificationDetailId4 = BALFactory.samplecollectionBAL.AddNotification(Msg, "Sampler Customer", CoreFactory.samplecollectionEntity);
            }

            //////For Add and Update SampleCollection///////
            if (model.Collection.SampleCollectionId == 0)
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

        public PartialViewResult _QuantityList(int SampleCollectionId)
        {
            CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetArrivalQtyPreservativeList(SampleCollectionId);
            List<SampleArrivalModel> modelList = new List<SampleArrivalModel>();
            int iSrNo = 1;
            int Count = CoreFactory.samplearrivalList.Count();
            foreach (var item in CoreFactory.samplearrivalList)
            {

                if (item.ISGivenPreservative == true)
                {
                    modelList.Add(new SampleArrivalModel()
                    {
                        SerialNo = iSrNo,
                        SampleCollectionId = item.SampleCollectionId,
                        QtyPreservativeId = item.QtyPreservativeId,
                        ISGivenPreservative = item.ISGivenPreservative,
                        GivenPreservative = "Yes",
                        SampleQtyId = (Int32)item.SampleQtyId,
                        SampleQty = item.SampleQty,
                        Preservation = item.Preservation,
                        No = (long)item.No,

                    });
                    iSrNo++;
                }
                if (item.ISGivenPreservative == false)
                {
                    modelList.Add(new SampleArrivalModel()
                    {
                        SerialNo = iSrNo,
                        SampleCollectionId = item.SampleCollectionId,
                        QtyPreservativeId = item.QtyPreservativeId,
                        ISGivenPreservative = item.ISGivenPreservative,
                        GivenPreservative = "NO",
                        SampleQtyId = (Int32)item.SampleQtyId,
                        SampleQty = item.SampleQty,
                        Preservation = item.Preservation,
                        No = (long)item.No,
                    });
                    iSrNo++;
                }
            }
            return PartialView(modelList);
        }

        public PartialViewResult _ParametersUnitList(int EnquirySampleID)
        {
            CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetArrivalParameterUnitList(EnquirySampleID);
            List<SampleArrivalModel> modelList = new List<SampleArrivalModel>();

            int iSrNo = 1;
            foreach (var item in CoreFactory.samplearrivalList)
            {
                modelList.Add(new SampleArrivalModel()
                {
                    SerialNo = iSrNo,
                    EnquirySampleID = item.EnquirySampleID,
                    EnquiryParameterDetailID = item.EnquiryParameterDetailID,
                    ParameterMappingId = item.ParameterMappingId,
                    UnitId = item.UnitId,
                    Unit = item.Unit,
                    ParameterMasterId = item.ParameterMasterId,
                    ParameterName = item.ParameterName,
                    ParameterGroupId = item.ParameterGroupId,
                    DisciplineId = item.DisciplineId,
                    Discipline = item.Discipline,
                });

                iSrNo++;
            }
            return PartialView(modelList);
        }

        public ActionResult SampleSubmit(int? CollBy = 0, int? SampleCollectionId = 0)
        {
            CollectionListModel model = new CollectionListModel();
            model.Collection = new SampleCollectionModel();
            int CollectedBy = (Int32)CollBy;
            CoreFactory.samplecollectionEntity = BALFactory.samplecollectionBAL.GetSampleCollectionDetails((Int32)SampleCollectionId);
            model.Collection.SampleCollectionId = CoreFactory.samplecollectionEntity.SampleCollectionId;
            model.Collection.StatusId = CoreFactory.samplecollectionEntity.StatusId;
            model.Collection.MatrixName = CoreFactory.samplecollectionEntity.MatrixName;
            string MatrixName = CoreFactory.samplecollectionEntity.MatrixName;
            string StatusCodeLab = CoreFactory.samplecollectionEntity.StatusCodeLab;//FoLabStatus
            string StatusCodeField = CoreFactory.samplecollectionEntity.StatusCodeField;//ForFieldStatus
            if (MatrixName == "Ambient Air" || MatrixName == "NoiseLevel")//Only For Field
            {
                if (StatusCodeField == "SampleColl" && StatusCodeLab == "SampleColl")//Only for Field
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleSub");
                    BALFactory.samplecollectionBAL.UpdateCollectionStatus((Int32)SampleCollectionId, (byte)iStatusId);
                    string Msg = "Sample Submitted";
                    model.Collection.SampleNo = CoreFactory.samplecollectionEntity.SampleNo;
                    long NotificationDetailId2 = BALFactory.samplecollectionBAL.AddNotification(Msg, "Sampling Incharge", CoreFactory.samplecollectionEntity);

                    TempData.Keep("message");
                    TempData["message"] = "Collection  Submitted.";

                }
                else if(StatusCodeField == null && StatusCodeLab == "CollAssign")
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleSub");
                    BALFactory.samplecollectionBAL.UpdateCollectionStatus((Int32)SampleCollectionId, (byte)iStatusId);
                    BALFactory.samplecollectionBAL.AddARC((Int32)SampleCollectionId, LIMS.User.UserMasterID);
                    string Msg = "Sample Submitted";
                    model.Collection.SampleNo = CoreFactory.samplecollectionEntity.SampleNo;
                    long NotificationDetailId2 = BALFactory.samplecollectionBAL.AddNotification(Msg, "Sampling Incharge", CoreFactory.samplecollectionEntity);

                    TempData.Keep("message");
                    TempData["message"] = "Collection  Submitted.";
                    //TempData.Keep("message");
                    //TempData["message"] = "Collection can not be Submitted.";
                }
                else if (StatusCodeField == null && StatusCodeLab == "SampleColl")
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleSub");
                    BALFactory.samplecollectionBAL.UpdateCollectionStatus((Int32)SampleCollectionId, (byte)iStatusId);
                    //BALFactory.samplecollectionBAL.AddARC((Int32)SampleCollectionId, LIMS.User.UserMasterID);
                    string Msg = "Sample Submitted";
                    model.Collection.SampleNo = CoreFactory.samplecollectionEntity.SampleNo;
                    long NotificationDetailId2 = BALFactory.samplecollectionBAL.AddNotification(Msg, "Sampling Incharge", CoreFactory.samplecollectionEntity);

                    TempData.Keep("message");
                    TempData["message"] = "Collection  Submitted.";
                    //TempData.Keep("message");
                    //TempData["message"] = "Collection can not be Submitted.";
                }


            }
            else
            {
                //For Both Field And Lab And Only For Lab(Collected By Customer Case)
                if (StatusCodeLab == "SampleColl" && StatusCodeField == "SampleColl")//And Status for Field Also to be checked 
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleSub");
                    BALFactory.samplecollectionBAL.UpdateCollectionStatus((Int32)SampleCollectionId, (byte)iStatusId);
                    string Msg = "Sample Submitted";
                    model.Collection.SampleNo = CoreFactory.samplecollectionEntity.SampleNo;
                    long NotificationDetailId2 = BALFactory.samplecollectionBAL.AddNotification(Msg, "Sampling Incharge", CoreFactory.samplecollectionEntity);

                    TempData.Keep("message");
                    TempData["message"] = "Collection Submitted.";

                }
                else if (StatusCodeLab == "CollAssign" && StatusCodeField == null)//Only for Lab
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleSub");
                    BALFactory.samplecollectionBAL.UpdateCollectionStatus((Int32)SampleCollectionId, (byte)iStatusId);
                    BALFactory.samplecollectionBAL.AddARC((Int32)SampleCollectionId, LIMS.User.UserMasterID);
                    string Msg = "Sample Submitted";
                    model.Collection.SampleNo = CoreFactory.samplecollectionEntity.SampleNo;
                    long NotificationDetailId2 = BALFactory.samplecollectionBAL.AddNotification(Msg, "Sampling Incharge", CoreFactory.samplecollectionEntity);

                    TempData.Keep("message");
                    TempData["message"] = "Collection Submitted.";

                }
                else if (StatusCodeLab == "InProgress" && StatusCodeField == null)
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleSub");
                    BALFactory.samplecollectionBAL.UpdateCollectionStatus((Int32)SampleCollectionId, (byte)iStatusId);
                    //BALFactory.samplecollectionBAL.AddARC((Int32)SampleCollectionId, LIMS.User.UserMasterID);
                    string Msg = "Sample Submitted";
                    model.Collection.SampleNo = CoreFactory.samplecollectionEntity.SampleNo;
                    long NotificationDetailId2 = BALFactory.samplecollectionBAL.AddNotification(Msg, "Sampling Incharge", CoreFactory.samplecollectionEntity);

                    TempData.Keep("message");
                    TempData["message"] = "Collection Submitted.";
                    //TempData.Keep("message");
                    //TempData["message"] = "Collection is not Submitted Properly";
                }
                else if (StatusCodeLab == "SampleColl" && StatusCodeField == null)
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleSub");
                    BALFactory.samplecollectionBAL.UpdateCollectionStatus((Int32)SampleCollectionId, (byte)iStatusId);
                    //BALFactory.samplecollectionBAL.AddARC((Int32)SampleCollectionId, LIMS.User.UserMasterID);
                    string Msg = "Sample Submitted";
                    model.Collection.SampleNo = CoreFactory.samplecollectionEntity.SampleNo;
                    long NotificationDetailId2 = BALFactory.samplecollectionBAL.AddNotification(Msg, "Sampling Incharge", CoreFactory.samplecollectionEntity);

                    TempData.Keep("message");
                    TempData["message"] = "Collection Submitted.";
                    //TempData.Keep("message");
                    //TempData["message"] = "Collection is not Submitted Properly";
                }
                //else
                //{
                //    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleSub");
                //    BALFactory.samplecollectionBAL.UpdateCollectionStatus((Int32)SampleCollectionId, (byte)iStatusId);
                //    BALFactory.samplecollectionBAL.AddARC((Int32)SampleCollectionId, LIMS.User.UserMasterID);
                //    string Msg = "Sample Submitted";
                //    model.Collection.SampleNo = CoreFactory.samplecollectionEntity.SampleNo;
                //    long NotificationDetailId2 = BALFactory.samplecollectionBAL.AddNotification(Msg, "Sampling Incharge", CoreFactory.samplecollectionEntity);

                //    TempData.Keep("message");
                //    TempData["message"] = "Collection Submitted.";
                //    //TempData.Keep("message");
                //    //TempData["message"] = "Collection can not be Submitted.";
                //}
            }

            if (CollBy == 2)
            {
                //return RedirectToAction("CollectorListCustomer", "Collection", new { CollectedBy });
                return Redirect("/Collection/SampleCollection/CollectorListCustomer?CollectedBy=" + CollectedBy);

            }

            //return RedirectToAction("CollectorList", "Collection", new { CollectedBy });
            return Redirect("/Collection/SampleCollection/CollectorList?CollectedBy=" + CollectedBy);

        }

        public ActionResult FieldDetermination(int? SampleCollectionId = 0, string Url = "", int? EnquiryId = 0, int? EnquirySampleID = 0)
        {
            long FieldId = BALFactory.samplecollectionBAL.GetFDDetails((Int32)SampleCollectionId);

            if (Url == "Ambient Air")
            {
                return RedirectToAction("FieldAmbientAirMonitoring", "FieldAmbientAirMonitoring", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID, FieldId });
            }
            if (Url == "Admixture" || Url == "Cement" || Url == "Fly Ash")
            {
                //int FieldBuildingMaterialId = (Int32)FieldId;
                return RedirectToAction("FieldBuildingMaterial", "FieldBuildingMaterial", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID, FieldId });
            }
            if (Url == "Coal" || Url == "Coke" || Url == "Charcoal" || Url == "Briquettes" || Url == "Other Solid fuel")
            {
                return RedirectToAction("FieldCoalCokeSolidFuel", "FieldCoalCokeSolidFuel", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID, FieldId });
            }
            if (Url == "Ambient Noise Level" || Url == "Source Noise Level" || Url == "Workplace Noise Level")
            {
                //int FieldNoiseId = (Int32)FieldId;    
                bool flag = true;
                if (Url == "Ambient Noise Level" || Url == "Workplace Noise Level")
                {
                    flag = true;
                }
                if (Url == "Source Noise Level")
                {
                    flag = false;
                }

                return RedirectToAction("FieldNoiseLevelMonitoring", "FieldNoiseLevelMonitoring", new { area = "FieldDetermination", flag, SampleCollectionId, EnquiryId, EnquirySampleID, FieldId });
            }
            if (Url == "Soil" || Url == "Sediment" || Url == "Solid Waste" || Url == "Hazardous Waste" || Url == "Used Oil" || Url == "Waste Oil")
            {
                //int SolidHazardousWasteSoilOilId = (Int32)FieldId;
                return RedirectToAction("FieldSolidSoilOilHazardousWaste", "FieldSolidSoilOilHazardousWaste", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID, FieldId });
            }
            if (Url == "Drinking Water/Potable Water /Domestic Water" || Url == "Ground Water" || Url == "Construction Water" || Url == "Industrial Water" || Url == "Irrigation Water" || Url == "Packaged Drinking Water" || Url == "Wastewater"
                     || Url == "Surface Water" || Url == "Swimming Pool Water" || Url == "Water from Purifiers" || Url == "Water for Medicinal Purpose" || Url == "Disilled/Deminarilized Water" || Url == "Other Water" || Url == "Domestic Effluent" || Url == "Industrial Effluent")
            {
                //int WasteWaterID = BALFactory.samplecollectionBAL.GetFieldIdByMatrixName(Url, (Int32)SampleCollectionId);
                //int WasteWaterID = (Int32)FieldId;
                return RedirectToAction("FieldWasteWater", "FieldWasteWater", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID, FieldId });
            }
            if (Url == "Food ")
            {
                // int FieldFoodAndAgriCultureId = (Int32)FieldId;
                return RedirectToAction("FieldFoodAndAgriProducts", "FieldFoodAndAgriProducts", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID, FieldId });
            }
            if (Url == "Workplace Air" || Url == "Fugitive Emission" || Url == "Workplace Air/Indoor Air Quality")
            {
                // int WorkplaceID = (Int32)FieldId;
                return RedirectToAction("WorkplaceEnvAndFugitiveEmissionField", "WorkplaceEnvAndFugutiveField", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID, FieldId });
               // return Redirect("/FieldDetermination/FieldWorkplaceEnvAndFugitiveEmission/FieldWorkplaceEnvAndFugitiveEmission?SampleCollectionId=" + SampleCollectionId + "&EnquiryId=" + EnquiryId + "&EnquirySampleID=" + EnquirySampleID + "&FieldId=" + FieldId);
            }
            if (Url == "Stack Emission")
            {
                int FDStackEmissionId = (Int32)FieldId;
                return RedirectToAction("FieldStackEmissionMonitoring", "FieldStackEmissionMonitoring", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID, FDStackEmissionId });
            }
            if (Url == "Surface Swabs" || Url == "Air Monitoring")
            {
                //int MicrobiologicalID = (Int32)FieldId;
                return RedirectToAction("FieldMicrobiologicalMonitoringOfAir", "FieldMicrobiologicalMonitoringOfAir", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID, Url, FieldId });
            }

            return RedirectToAction("CollectorList");
        }

        public enum ManageMessageId
        {
            CollAssign,
            WOApproved,
            InProgress,
            SampleColl,
            SampleSub,
        }
    }
}