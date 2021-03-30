using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Arrival.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Collection;
using LIMS_DEMO.Core.Arrival;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Collection;
using LIMS_DEMO.BAL.Arrival;
using LIMS_DEMO.BAL.DropDown;
using System.Runtime.CompilerServices;
using System.ComponentModel;


namespace LIMS_DEMO.Areas.Arrival.Controllers
{
    [RouteArea("Arrival")]
    public class SampleArrivalController : Controller
    {
        int a = 0;
        public SampleArrivalController()
        {
            BALFactory.samplearrivalBAL = new SampleArrivalBAL();
            BALFactory.samplecollectionBAL = new SampleCollectionBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
        }

        // GET: Arrival/SampleArrival
        public ActionResult SampleArrivalList()
        {
            CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetArrivalList();
            List<SampleArrivalModel> modelList = new List<SampleArrivalModel>();

            int iSrNo = 1;
            foreach (var Item in CoreFactory.samplearrivalList)
            {
                if (Item.StatusCode == "SampleSub" || Item.StatusCode == "SampleRecv" || Item.StatusCode == "ReqCreated" || Item.StatusCode == "APPROVED")
                {
                    modelList.Add(new SampleArrivalModel()
                    {
                        LocationSampleCollectionID = Item.LocationSampleCollectionID,
                        SampleTypeProductId = Item.SampleTypeProductId,
                        Url = Item.SampleTypeProductName,
                        WorkOrderID = Item.WorkOrderID,
                        SerialNo = iSrNo,
                        ARCId = Item.ARCId,
                        SampleCollectionId = Item.SampleCollectionId,
                        EnquirySampleID = Item.EnquirySampleID,
                        EnquiryDetailId = Item.EnquiryDetailId,
                        SampleNameOriginal = Item.SampleNameOriginal,
                        SampleTypeProductName = Item.SampleTypeProductName,
                        SampleLocation = Item.SampleLocation,
                        EnquiryId = Item.EnquiryId,
                        EmployeeId = Item.EmployeeId, //RoleId,UserName,CollectorName
                        SampleNo = Item.SampleNo,//to be Changed to SampleName wrt Iteration Number,
                        //ULRNo = Item.ULRNo,//doubt for backend storage only
                        SampleName = Item.SampleName,// FieldDeterminationId = Item.FieldDeterminationId,//Doubt
                        RequestNo = Item.RequestNo,
                        ContactNO = Item.ContactNO,
                        CustomerName = Item.CustomerName,
                        StatusId = Item.StatusId,
                        CurrentStatus = Item.CurrentStatus,//SampleReceived
                        CollectionDate = Item.CollectionDate,
                        Date = Convert.ToDateTime(Item.CollectionDate).ToString("dd/MM/yyyy"),
                        MatrixName = Item.MatrixName,
                        //Url = Item.MatrixName,//Url = "Air",//Url=ProductGrp+SubGrp+Matrix
                    }); ;
                    iSrNo++;
                }
            }
            return View(modelList);
        }
        public ActionResult AddSampleArrival(int? SampleCollectionId = 0, int? ARCId = 0, string SampleNo = "")
        {
            SampleArrivalModel model = new SampleArrivalModel();
            if (ARCId != 0)
            {
                CoreFactory.samplearrivalEntity = BALFactory.samplearrivalBAL.GetARCDetails((Int32)ARCId);
                model.ARCId = CoreFactory.samplearrivalEntity.ARCId;
                model.SampleCollectionId = CoreFactory.samplearrivalEntity.SampleCollectionId;
                model.UserRoleId = CoreFactory.samplearrivalEntity.UserRoleId;
                model.ActionDate = CoreFactory.samplearrivalEntity.ActionDate;
                model.Date2 = Convert.ToDateTime(CoreFactory.samplearrivalEntity.ActionDate).Date;
                model.dt2 = Convert.ToDateTime(model.Date2).ToString("dd/MM/yyyy");
                model.Time2 = Convert.ToDateTime(CoreFactory.samplearrivalEntity.ActionDate).TimeOfDay;
            }
            if (SampleCollectionId != 0)
            {
                CoreFactory.samplearrivalEntity = BALFactory.samplearrivalBAL.GetSampleArrivalDetails((Int32)SampleCollectionId);
                model.WorkOrderID = CoreFactory.samplearrivalEntity.WorkOrderID;
                model.LocationSampleCollectionID = CoreFactory.samplearrivalEntity.LocationSampleCollectionID;
                if (model.SampleTypeProductName == "Ambient Noise Level" || model.SampleTypeProductName == "Source Noise Level" || model.SampleTypeProductName == "Workplace Noise Level")
                {
                    model.flag = true; //For Noise SampleType Only
                }
                else
                {
                    model.flag = false;
                }
                model.SampleTypeProductId = CoreFactory.samplearrivalEntity.SampleTypeProductId;
                model.SampleTypeProductName = CoreFactory.samplearrivalEntity.SampleTypeProductName;
                model.Url = model.SampleTypeProductName;//for FieldDetermination
                model.SampleCollectionId = CoreFactory.samplearrivalEntity.SampleCollectionId;
                model.EnquirySampleID = CoreFactory.samplearrivalEntity.EnquirySampleID;
                model.EnquiryDetailId = CoreFactory.samplearrivalEntity.EnquiryDetailId;
                model.EnquiryId = CoreFactory.samplearrivalEntity.EnquiryId;
                model.StatusCode = CoreFactory.samplearrivalEntity.StatusCode;//FoLabStatus
                ViewBag.StatusCode = model.StatusCode;
                //model.SampleNo = CoreFactory.samplearrivalEntity.SampleNo;
                model.SampleNo = SampleNo;
                model.SampleName = CoreFactory.samplearrivalEntity.SampleName;
                model.SampleNameOriginal = CoreFactory.samplearrivalEntity.SampleNameOriginal;
                //model.CollectedBy = CoreFactory.samplearrivalEntity.CollectedBy;
                model.CustomerName = CoreFactory.samplearrivalEntity.CustomerName;
                model.CityName = CoreFactory.samplearrivalEntity.CityName;
                model.PlannerId = CoreFactory.samplearrivalEntity.PlannerId;
                model.SampleCollectedBy = CoreFactory.samplearrivalEntity.SampleCollectedBy;
                a = (Int32)model.SampleCollectedBy;
                if (a == 1)
                {
                    model.CollectedBy = "LAB";
                }
                if (a == 2)
                {
                    model.CollectedBy = "Customer";
                }
                model.ProductGroupName = CoreFactory.samplearrivalEntity.ProductGroupName;
                model.SubGroupName = CoreFactory.samplearrivalEntity.SubGroupName;
                model.MatrixName = CoreFactory.samplearrivalEntity.MatrixName;
                model.Url = model.MatrixName;//for FieldDetermination
                model.CollectionDate = CoreFactory.samplearrivalEntity.CollectionDate;
                model.Date = Convert.ToDateTime(model.CollectionDate).ToString("dd/MM/yyyy");
                model.SampleCollectionTime = CoreFactory.samplearrivalEntity.SampleCollectionTime;
                model.Duration = CoreFactory.samplearrivalEntity.Duration;
                model.SampleLocation = CoreFactory.samplearrivalEntity.SampleLocation;
                model.SampleType = CoreFactory.samplearrivalEntity.SampleType;
                model.EnvCondts = CoreFactory.samplearrivalEntity.EnvCondts;
                model.EmployeeId = CoreFactory.samplearrivalEntity.EmployeeId;
                model.WitnessName = CoreFactory.samplearrivalEntity.WitnessName;
                model.ProbableDateOfReport = Convert.ToDateTime(CoreFactory.samplearrivalEntity.ProbableDateOfReport).Date;
                model.podr = Convert.ToDateTime(model.ProbableDateOfReport).ToString("dd/MM/yyyy");
                model.IsSampleIntact = (bool)CoreFactory.samplearrivalEntity.IsSampleIntact;
                model.ULRNo = CoreFactory.samplearrivalEntity.ULRNo;//To be genrated here
                model.RequestNo = CoreFactory.samplearrivalEntity.RequestNo;//To be genrated here
                model.IsReturnedOrIsRetained = CoreFactory.samplearrivalEntity.IsReturnedOrIsRetained;
                model.StatutoryLimits = CoreFactory.samplearrivalEntity.StatutoryLimits;
                model.SubContractedParameters = CoreFactory.samplearrivalEntity.SubContractedParameters;
                model.AckRemarks = CoreFactory.samplearrivalEntity.AckRemarks;

            }
            if (model.flag == true)
            {
                ///////For Approver Selection only For Noise Level SapmleType///////////////
                CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetDisciplineList();
                model.ParaDisciplineList = new List<ParameterDisciplineModel>();
                int iSrNo = 1;
                foreach (var item in CoreFactory.samplearrivalList)
                {
                    model.ParaDisciplineList.Add(new ParameterDisciplineModel()
                    {
                        SerialNo = iSrNo,
                        //EnquirySampleID = item.EnquirySampleID,
                        //EnquiryParameterDetailID = item.EnquiryParameterDetailID,
                        //ParameterMappingId = item.ParameterMappingId,
                        //UnitId = item.UnitId,
                        //Unit = item.Unit,
                        //ParameterMasterId = item.ParameterMasterId,
                        //ParameterName = item.ParameterName,
                        //ParameterGroupId = item.ParameterGroupId,
                        DisciplineId = item.DisciplineId,
                        Discipline = item.Discipline,
                        ApproverId = item.ApproverId,
                        Parameters = BALFactory.samplearrivalBAL.GetParameterByDiscipline((Int32)model.EnquirySampleID, (int)item.DisciplineId), //sample.Parameters,
                        Approver = BALFactory.dropdownsBAL.GetPlannerList("Approver", LIMS.User.LabId, (int)item.DisciplineId),//For selection of Planner,
                    });
                    iSrNo++;
                }

                ViewBag.ApproverList = BALFactory.dropdownsBAL.GetApproverList("Approver", LIMS.User.LabId);//For selection of Approver
                model.Parameters = BALFactory.samplearrivalBAL.GetParameterByDiscipline((Int32)model.EnquirySampleID, 0);
            }
            else
            {
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
                        //EnquirySampleID = item.EnquirySampleID,
                        //EnquiryParameterDetailID = item.EnquiryParameterDetailID,
                        //ParameterMappingId = item.ParameterMappingId,
                        //UnitId = item.UnitId,
                        //Unit = item.Unit,
                        //ParameterMasterId = item.ParameterMasterId,
                        //ParameterName = item.ParameterName,
                        //ParameterGroupId = item.ParameterGroupId,
                        DisciplineId = item.DisciplineId,
                        Discipline = item.Discipline,
                        PlannerId = item.PlannerId,
                        Parameters = BALFactory.samplearrivalBAL.GetParameterByDiscipline((Int32)model.EnquirySampleID, (int)item.DisciplineId), //sample.Parameters,
                        Planner = BALFactory.dropdownsBAL.GetPlannerList("Planner", LIMS.User.LabId, (int)item.DisciplineId),//For selection of Planner,
                    });
                    iSrNo++;
                }
            }
            ViewBag.SampleReceviedLabBy = BALFactory.dropdownsBAL.GetReceiver(LIMS.User.LabId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddSampleArrival(SampleArrivalModel model, string Save)
        {
            CoreFactory.samplearrivalEntity = new SampleArrivalEntity();
            CoreFactory.samplearrivalEntity.ARCId = model.ARCId;
            CoreFactory.samplearrivalEntity.SampleCollectionId = model.SampleCollectionId;
            CoreFactory.samplearrivalEntity.LocationSampleCollectionID = model.LocationSampleCollectionID;
            CoreFactory.samplearrivalEntity.EnquirySampleID = model.EnquirySampleID;
            CoreFactory.samplearrivalEntity.WorkOrderID = model.WorkOrderID;
            CoreFactory.samplearrivalEntity.UserRoleId = model.UserRoleId;
            CoreFactory.samplearrivalEntity.SubContractedParameters = model.SubContractedParameters;
            CoreFactory.samplearrivalEntity.StatutoryLimits = model.StatutoryLimits;
            CoreFactory.samplearrivalEntity.IsReturnedOrIsRetained = model.IsReturnedOrIsRetained;
            if (model.IsReturnedOrIsRetained == "Returned")
            {
                CoreFactory.samplearrivalEntity.ReturnedDate = model.ReturnedDate;
                CoreFactory.samplearrivalEntity.ReturnedRemark = model.ReturnedRemarks;
            }
            CoreFactory.samplearrivalEntity.AckRemarks = model.AckRemarks;
            //CoreFactory.samplearrivalEntity.SampleNo = model.SampleNo;
            CoreFactory.samplearrivalEntity.Date2 = model.Date2;
            CoreFactory.samplearrivalEntity.Time2 = model.Time2;/* DateTime(int year, int month, int day);*/
            if (model.Date2 == null)
            {
                CoreFactory.samplearrivalEntity.ActionDate = null; //For Noise SampleType Only
            }
            else
            {
                CoreFactory.samplearrivalEntity.ActionDate = Convert.ToDateTime(model.Date2 + model.Time2);
            }
            CoreFactory.samplearrivalEntity.IsSampleIntact = model.IsSampleIntact;
            CoreFactory.samplearrivalEntity.ProbableDateOfReport = model.ProbableDateOfReport;
            CoreFactory.samplearrivalEntity.PlannerId = model.PlannerId;//For selection of Planner

            CoreFactory.samplearrivalEntity.CollectedBy = model.CollectedBy;
            CoreFactory.samplearrivalEntity.CustomerName = model.CustomerName;
            CoreFactory.samplearrivalEntity.CityName = model.CityName;
            CoreFactory.samplearrivalEntity.IsActive = true;
            CoreFactory.samplearrivalEntity.EnteredBy = LIMS.User.UserMasterID;

            if (Save == "ArrivalSubmit")
            {

                //////////Generate SampleNo. with new Format(Notation\Yymm\SerialNo)/////////////////////// 
                //CoreFactory.samplearrivalEntity.SampleNo = model.SampleNo;
                //CoreFactory.samplearrivalEntity.SampleNo = BALFactory.samplearrivalBAL.GenerateSampleNo((Int32)model.SampleCollectionId, model.SampleNo);// may be removed later

                //if (model.ULRNo == " " || model.ULRNo == null)
                //{
                //    CoreFactory.samplearrivalEntity.ULRNo = BALFactory.samplearrivalBAL.GenerateULRNo((Int32)model.SampleCollectionId);

                //}
                //if (model.RequestNo == " " || model.RequestNo == null)
                //{
                //    CoreFactory.samplearrivalEntity.RequestNo = BALFactory.samplearrivalBAL.GenerateReportNo((Int32)model.SampleCollectionId, model.SampleName, model.CollectedBy, model.CustomerName, model.CityName);
                //}

                int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleRecv");
                BALFactory.samplecollectionBAL.UpdateCollectionStatus(model.SampleCollectionId, iStatusId);

                string Msg = "Sample Received";
                CoreFactory.samplearrivalEntity.SampleNameOriginal = model.SampleNameOriginal;
                long NotificationDetailId = BALFactory.samplearrivalBAL.AddNotification(Msg, "ADMIN", CoreFactory.samplearrivalEntity);
                long NotificationDetailId7 = BALFactory.samplearrivalBAL.AddNotification(Msg, "BDM", CoreFactory.samplearrivalEntity);
                long NotificationDetailId1 = BALFactory.samplearrivalBAL.AddNotification(Msg, "Sample Receipt and Report Incharge", CoreFactory.samplearrivalEntity);
                long NotificationDetailId2 = BALFactory.samplearrivalBAL.AddNotification(Msg, "Sample Receiver", CoreFactory.samplearrivalEntity);
                long NotificationDetailId3 = BALFactory.samplearrivalBAL.AddNotification(Msg, "Planner", CoreFactory.samplearrivalEntity);
                long NotificationDetailId4 = BALFactory.samplearrivalBAL.AddNotification(Msg, "Tester", CoreFactory.samplearrivalEntity);
                long NotificationDetailId5 = BALFactory.samplearrivalBAL.AddNotification(Msg, "Reviewer", CoreFactory.samplearrivalEntity);
                long NotificationDetailId6 = BALFactory.samplearrivalBAL.AddNotification(Msg, "Approver", CoreFactory.samplearrivalEntity);


                var Reportno_ULRno = BALFactory.samplearrivalBAL.GetReportULRNumber((Int32)model.EnquiryId, model.SampleTypeProductId, (Int32)model.EnquirySampleID, model.WorkOrderID);
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
                if (model.flag == true)//For Noise Level sampleType Only
                {
                    CoreFactory.plannerbydisciplineList = new List<PlannerByDisciplineEntity>();
                    foreach (var item in model.ParaDisciplineList)
                    {
                        if (item.ApproverId != null && item.Parameters != null)
                        {
                            CoreFactory.plannerbydisciplineList.Add(new PlannerByDisciplineEntity()
                            {
                                ApproverId = item.ApproverId,
                                //ParameterName = item.ParameterName,
                                Parameters = item.Parameters,
                                DisciplineId = item.DisciplineId,
                                SampleCollectionId = model.SampleCollectionId,
                                IsActive = true,
                                EnteredBy = LIMS.User.UserMasterID,
                                EnteredDate = DateTime.Now,
                            });

                            BALFactory.samplearrivalBAL.AddApprover(CoreFactory.plannerbydisciplineList, model.EnquirySampleID, model.SampleCollectionId);
                        }
                        else if (item.ApproverId == null && item.Parameters != null)
                        {
                            return Json(new { status = "Fail" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
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

                            BALFactory.samplearrivalBAL.AddPlannerByDiscipline(CoreFactory.plannerbydisciplineList);
                        }
                        else if (item.PlannerId == null && item.Parameters != null)
                        {
                            return Json(new { status = "Fail" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                int iStatusId2 = BALFactory.dropdownsBAL.GetStatusIdByCode("ReqCreated");
                BALFactory.samplecollectionBAL.UpdateCollectionStatus(model.SampleCollectionId, (byte)iStatusId2);

                string Msg2 = "Reports to be Submitted";//reports to be submitted 
                CoreFactory.samplearrivalEntity.SampleName = model.SampleName;
                long NotificationDetailId22 = BALFactory.samplearrivalBAL.AddNotification(Msg2, "Sample Receipt and Report Incharge", CoreFactory.samplearrivalEntity);
                long NotificationDetailId23 = BALFactory.samplearrivalBAL.AddNotification(Msg2, "Sample Receiver", CoreFactory.samplearrivalEntity);
                long NotificationDetailId24 = BALFactory.samplearrivalBAL.AddNotification(Msg2, "Planner", CoreFactory.samplearrivalEntity);
                long NotificationDetailId25 = BALFactory.samplearrivalBAL.AddNotification(Msg2, "Reviewer", CoreFactory.samplearrivalEntity);
            }
            var status = BALFactory.samplearrivalBAL.UpdateSampleArrival(CoreFactory.samplearrivalEntity);
            return Json(new { status = status, message = "Sample Arrival updated." }, JsonRequestBehavior.AllowGet);

            //if (model.ARCId == 0)
            //{

            //    BALFactory.samplearrivalBAL.AddSampleArrival(CoreFactory.samplearrivalEntity);
            //    return Json(new { status = "success", message = "Sample Arrival Added" }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{

            //    BALFactory.samplearrivalBAL.UpdateSampleArrival(CoreFactory.samplearrivalEntity);
            //    return Json(new { status = "success", message = "Sample Arrival updated." }, JsonRequestBehavior.AllowGet);
            //}

        }
        public PartialViewResult _QuantityPreservativeList(int SampleCollectionId)
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
        public PartialViewResult _SampleDevicesList(int SampleCollectionId)
        {
            CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetCollectionDevicesList(SampleCollectionId);
            List<SampleArrivalModel> modelList = new List<SampleArrivalModel>();
            int iSrNo = 1;
            foreach (var item in CoreFactory.samplearrivalList)
            {
                modelList.Add(new SampleArrivalModel()
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
        public ActionResult FieldDeterminationArrival(int? SampleCollectionId = 0, string Url = "", int? EnquiryId = 0, int? EnquirySampleID = 0)
        {

            if (Url == "Ambient Air")
            {
                return RedirectToAction("FieldAmbientAirMonitoring", "FieldAmbientAirMonitoring", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID });
            }
            if (Url == "Admixture" || Url == "Cement" || Url == "Fly Ash")
            {
                return RedirectToAction("FieldBuildingMaterial", "FieldBuildingMaterial", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID });
            }
            if (Url == "Coal" || Url == "Coke" || Url == "Charcoal" || Url == "Briquettes" || Url == "Other Solid fuel")
            {
                int FieldId = BALFactory.samplecollectionBAL.GetFieldIdByMatrixName(Url, (Int32)SampleCollectionId);
                return RedirectToAction("FieldCoalCokeSolidFuel", "FieldCoalCokeSolidFuel", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID });

            }
            if (Url == "Ambient Noise Level" || Url == "Source Noise Level")
            {
                return RedirectToAction("FieldNoiseLevelMonitoring", "FieldNoiseLevelMonitoring", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID });

            }
            if (Url == "Soil" || Url == "Sediment" || Url == "Solid Waste" || Url == "Hazardous Waste" || Url == "Used Oil" || Url == "Waste Oil")
            {
                return RedirectToAction("FieldSolidSoilOilHazardousWaste", "FieldSolidSoilOilHazardousWaste", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID });

            }
            if (Url == "Drinking Water/Potable Water /Domestic Water" || Url == "Ground Water" || Url == "Construction Water" || Url == "Industrial Water" || Url == "Irrigation Water" || Url == "Packaged Drinking Water"
                  || Url == "Surface Water" || Url == "Swimming Pool Water" || Url == "Water from Purifiers" || Url == "Water for Medicinal Purpose" || Url == "Disilled/Deminarilized Water" || Url == "Other Water" || Url == "Domestic Effluent" || Url == "Industrial Effluent")
            {
                int WasteWaterID = BALFactory.samplecollectionBAL.GetFieldIdByMatrixName(Url, (Int32)SampleCollectionId);

                return RedirectToAction("FieldWasteWater", "FieldWasteWater", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID, WasteWaterID });
            }
            if (Url == "Food ")
            {
                return RedirectToAction("FieldFoodAndAgriProducts", "FieldFoodAndAgriProducts", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID });
            }
            if (Url == "Workplace Air" || Url == "Fugitive Emission" || Url == "Workplace Air/Indoor Air Quality")
            {
                //return RedirectToAction("FieldWorkplaceEnvAndFugitiveEmission", "FieldWorkplaceEnvAndFugitiveEmission", new { area = "FieldDetermination", SampleCollectionId, EnquiryId });

                return Redirect("/FieldDetermination/FieldWorkplaceEnvAndFugitiveEmission?SampleCollectionId=" + SampleCollectionId + "&EnquiryId=" + EnquiryId + "&EnquirySampleID=" + EnquirySampleID);
            }
            if (Url == "Stack Emission")
            {
                return RedirectToAction("FieldStackEmissionMonitoring", "FieldStackEmissionMonitoring", new { area = "FieldDetermination", SampleCollectionId, EnquiryId, EnquirySampleID });

            }

            return RedirectToAction("SampleArrivalList");
        }
        public enum ManageMessageId
        {
            SampleRecv,
            ReqCreated,
            SampleSub,

        }
        public ActionResult FinalReportList()
        {
            CoreFactory.finalReportList = BALFactory.samplearrivalBAL.GetFinalReportsList();
            List<FinalReportModel> modelList = new List<FinalReportModel>();

            int iSrNo = 1;
            foreach (var Item in CoreFactory.finalReportList)
            {
                modelList.Add(new FinalReportModel()
                {
                    WOMasterSampleCollectionDateId = Item.WOMasterSampleCollectionDateId,
                    LocationSampleCollectionID = Item.LocationSampleCollectionID,
                    SampleTypeProductId = Item.SampleTypeProductId,
                    Url = Item.SampleTypeProductName,
                    SampleTypeProductName = Item.SampleTypeProductName,
                    SampleLocation = Item.SampleLocation,
                    SerialNo = iSrNo,
                    SampleCollectionId = Item.SampleCollectionId,
                    EnquirySampleID = Item.EnquirySampleID,
                    EnquiryDetailId = Item.EnquiryDetailId,
                    EnquiryId = Item.EnquiryId,
                    SampleNo = Item.SampleNo,
                    SampleName = Item.SampleName,
                    ContactNO = Item.ContactNO,
                    CustomerName = Item.CustomerName,
                    StatusId = Item.StatusId,
                    CurrentStatus = Item.CurrentStatus,//Approved 
                    WorkOrderNo = Item.WorkOrderNo,
                    MatrixName = Item.MatrixName,
                    // Url = Item.MatrixName,//Url = "Air",//Url=ProductGrp+SubGrp+Matrix
                }); ;
                iSrNo++;
            }
            return View(modelList);
        }
        public ActionResult SampleReturnedList()
        {
            CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetSampleReturnedList();
            List<SampleArrivalModel> modelList = new List<SampleArrivalModel>();

            int iSrNo = 1;
            foreach (var Item in CoreFactory.samplearrivalList)
            {
                if (Item.IsReturnedOrIsRetained == "Returned")
                {
                    modelList.Add(new SampleArrivalModel()
                    {
                        SerialNo = iSrNo,
                        SampleTypeProductName = Item.SampleTypeProductName,
                        SampleNameOriginal = Item.SampleNameOriginal,
                        CustomerName = Item.CustomerName,
                        ContactNO = Item.ContactNO,
                        ARCId = Item.ARCId,
                        DateofRecieptofSample = Convert.ToDateTime(Item.ActionDate).ToString("dd/MM/yyyy"),
                        ReturnedDate = Item.ReturnedDate,
                        returnDate = Convert.ToDateTime(Item.ReturnedDate).ToString("dd/MM/yyyy"),
                        ProbableDateOfReport = Item.ProbableDateOfReport,
                        PDR = Convert.ToDateTime(Item.ProbableDateOfReport).ToString("dd/MM/yyyy"),
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