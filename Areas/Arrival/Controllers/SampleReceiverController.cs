using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Arrival.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Arrival;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Collection;
using LIMS_DEMO.BAL.Arrival;
using LIMS_DEMO.BAL.DropDown;

namespace LIMS_DEMO.Areas.Arrival.Controllers
{
    [RouteArea("Arrival")]
    public class SampleReceiverController : Controller
    {
        int a = 0;
        public SampleReceiverController()
        {
            BALFactory.samplearrivalBAL = new SampleArrivalBAL();
            BALFactory.samplecollectionBAL = new SampleCollectionBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
        }

        // GET: Arrival/SampleReceiver
        public ActionResult SampleReceiverList()
        {

            CoreFactory.samplearrivalList = BALFactory.samplearrivalBAL.GetReceiverList(LIMS.User.UserMasterID); //LIMS.User.UserMasterID
            List<SampleArrivalModel> modelList = new List<SampleArrivalModel>();

            int iSrNo = 1;
            foreach (var Item in CoreFactory.samplearrivalList)
            {
                if (Item.StatusCode == "SampleRecv" || Item.StatusCode == "ReqCreated")
                {
                    modelList.Add(new SampleArrivalModel()
                    {
                        LocationSampleCollectionID = Item.LocationSampleCollectionID,
                        SampleTypeProductId = Item.SampleTypeProductId,
                        Url = Item.SampleTypeProductName,
                        SerialNo = iSrNo,
                        ARCId = Item.ARCId,
                        SampleCollectionId = Item.SampleCollectionId,
                        SampleTypeProductName = Item.SampleTypeProductName,
                        SampleLocation = Item.SampleLocation,
                        EnquirySampleID = Item.EnquirySampleID,
                        EnquiryDetailId = Item.EnquiryDetailId,
                        EnquiryId = Item.EnquiryId,
                        EmployeeId = Item.EmployeeId, //RoleId,UserName,CollectorName
                        SampleNo = Item.SampleNo,//to be Changed to SampleName wrt Iteration Number,
                                                 //ULRNo = Item.ULRNo,//doubt for backend storage only
                        SampleName = Item.SampleName,// FieldDeterminationId = Item.FieldDeterminationId,//Doubt
                        RequestNo = Item.RequestNo,
                        SampleNameOriginal = Item.SampleNameOriginal,
                        //ContactNO = Item.ContactNO,
                        CustomerName = Item.CustomerName,
                        StatusId = Item.StatusId,
                        CurrentStatus = Item.CurrentStatus,//SampleReceived
                        //CollectionDate = Item.CollectionDate,
                        //Date = Convert.ToDateTime(Item.CollectionDate).ToString("dd/MM/yyyy"),
                        //MatrixName = Item.MatrixName,
                        //Url = Item.MatrixName,//Url = "Air",//Url=ProductGrp+SubGrp+Matrix
                    }); ;
                    iSrNo++;
                }
            }
            return View(modelList);
        }
        public ActionResult AddSampleReceiver(int? SampleCollectionId = 0, int? ARCId = 0, string SampleNo = "")
        {
            SampleArrivalModel model = new SampleArrivalModel();
            if (ARCId != 0)
            {
                CoreFactory.samplearrivalEntity = BALFactory.samplearrivalBAL.GetARCDetails((Int32)ARCId);
                model.ARCId = CoreFactory.samplearrivalEntity.ARCId;
                model.SampleCollectionId = CoreFactory.samplearrivalEntity.SampleCollectionId;
                model.ActionDate = CoreFactory.samplearrivalEntity.ActionDate;
                model.Date2 = Convert.ToDateTime(CoreFactory.samplearrivalEntity.ActionDate).Date;
                model.dt2 = Convert.ToDateTime(model.Date2).ToString("dd/MM/yyyy");
                model.Time2 = Convert.ToDateTime(CoreFactory.samplearrivalEntity.ActionDate).TimeOfDay;
            }
            if (SampleCollectionId != 0)
            {
                CoreFactory.samplearrivalEntity = BALFactory.samplearrivalBAL.GetSampleArrivalDetails((Int32)SampleCollectionId);
                model.LocationSampleCollectionID = CoreFactory.samplearrivalEntity.LocationSampleCollectionID;
                model.SampleTypeProductName = CoreFactory.samplearrivalEntity.SampleTypeProductName;
                if (model.SampleTypeProductName == "Ambient Noise Level" || model.SampleTypeProductName == "Source Noise Level" || model.SampleTypeProductName == "Workplace Noise Level")
                {
                    model.flag = true; //For Noise SampleType Only
                }
                else
                {
                    model.flag = false;
                }
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
                //model.CollectedBy = CoreFactory.samplearrivalEntity.CollectedBy;
                model.CustomerName = CoreFactory.samplearrivalEntity.CustomerName;
                model.CityName = CoreFactory.samplearrivalEntity.CityName;
                model.SampleCollectedBy = CoreFactory.samplearrivalEntity.SampleCollectedBy;
                model.PlannerId = CoreFactory.samplearrivalEntity.PlannerId;
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
                model.SampleLocation = CoreFactory.samplearrivalEntity.SampleLocation;
                model.Duration = CoreFactory.samplearrivalEntity.Duration;
                model.SampleType = CoreFactory.samplearrivalEntity.SampleType;
                //model.SampleDevice = CoreFactory.samplearrivalEntity.SampleDevice;
                model.EnvCondts = CoreFactory.samplearrivalEntity.EnvCondts;
                model.EmployeeId = CoreFactory.samplearrivalEntity.EmployeeId;
                model.WitnessName = CoreFactory.samplearrivalEntity.WitnessName;
                model.ProbableDateOfReport = Convert.ToDateTime(CoreFactory.samplearrivalEntity.ProbableDateOfReport).Date;
                model.podr = Convert.ToDateTime(model.ProbableDateOfReport).ToString("dd/MM/yyyy");
                model.IsSampleIntact = (bool)CoreFactory.samplearrivalEntity.IsSampleIntact;
                model.ULRNo = CoreFactory.samplearrivalEntity.ULRNo;//To be genrated here
                model.RequestNo = CoreFactory.samplearrivalEntity.RequestNo;//To be genrated here
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

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddSampleReceiver(SampleArrivalModel model, string Save)
        {
            CoreFactory.samplearrivalEntity = new SampleArrivalEntity();
            CoreFactory.samplearrivalEntity.ARCId = model.ARCId;
            CoreFactory.samplearrivalEntity.SampleCollectionId = model.SampleCollectionId;
            CoreFactory.samplearrivalEntity.LocationSampleCollectionID = model.LocationSampleCollectionID;
            CoreFactory.samplearrivalEntity.EnquirySampleID = model.EnquirySampleID;
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
            if (model.ARCId != 0 && model.SampleCollectionId != 0 && Save == "ReceiverSubmit")
            {
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

                BALFactory.samplearrivalBAL.UpdateSampleReceived(CoreFactory.samplearrivalEntity);

                int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("ReqCreated");
                BALFactory.samplecollectionBAL.UpdateCollectionStatus(model.SampleCollectionId, (byte)iStatusId);

                string Msg = "Reports to be Submitted";//reports to be submitted 
                CoreFactory.samplearrivalEntity.SampleName = model.SampleName;
                long NotificationDetailId = BALFactory.samplearrivalBAL.AddNotification(Msg, "Sample Receipt and Report Incharge", CoreFactory.samplearrivalEntity);
                long NotificationDetailId1 = BALFactory.samplearrivalBAL.AddNotification(Msg, "Sample Receiver", CoreFactory.samplearrivalEntity);
                long NotificationDetailId2 = BALFactory.samplearrivalBAL.AddNotification(Msg, "Planner", CoreFactory.samplearrivalEntity);
                long NotificationDetailId3 = BALFactory.samplearrivalBAL.AddNotification(Msg, "Reviewer", CoreFactory.samplearrivalEntity);

                return Json(new { status = "success", message = "Sample Arrival updated." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //To be removed not in use
                BALFactory.samplearrivalBAL.AddSampleArrival(CoreFactory.samplearrivalEntity);
                return Json(new { status = "success", message = "Sample Arrival Added" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}