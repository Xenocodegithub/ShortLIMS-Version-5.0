using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Collection.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Collection;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.DropDown;
using LIMS_DEMO.BAL.Collection;
namespace LIMS_DEMO.Areas.Collection.Controllers
{
    [RouteArea("Collection")]
    public class SurveyingTeamController : Controller
    {
        public SurveyingTeamController()
        {
            BALFactory.surveyingTeamLeadBAL = new SurveyingTeamLeadBAL();
            BALFactory.samplecollectionBAL = new SampleCollectionBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
        }
        // GET: Collection/SurveyingTeam
       
        public ActionResult SurveyingTeamLeadList()
        {
            CoreFactory.surveyingTeamList = BALFactory.surveyingTeamLeadBAL.GetSurveyingTeamList(LIMS.User.UserMasterID);
            List<SurveyingTeamLeadModel> modelList = new List<SurveyingTeamLeadModel>();

            foreach (var Item in CoreFactory.surveyingTeamList)
            {
                if (Item.StatusCode == "WOApproved" || Item.StatusCode == "CollByCust" || Item.StatusCode == "CollAssign" || Item.StatusCode == "InProgress")
                {
                    if (Item.Iteration == 1)
                    {
                        modelList.Add(new SurveyingTeamLeadModel()
                        {
                            LocationSampleCollectionID = Item.LocationSampleCollectionID,
                            SampleCollectionId = Item.SampleCollectionId,
                            EnquirySampleID = Item.EnquirySampleID,
                            EnquiryDetailId = Item.EnquiryDetailId,
                            SampleName = Item.SampleName,//Doubt for Sample No.
                            EnquiryId = Item.EnquiryId,
                            DisplaySampleName = Item.DisplaySampleName,
                            Location = Item.Location,
                            SampleTypeProductName = Item.SampleTypeProductName,
                            //ProductGroupName = Item.ProductGroupName,
                            //SubGroupName = Item.SubGroupName,
                            CustomerName = Item.CustomerName,
                            CurrentStatus = Item.CurrentStatus,
                            WorkOrderID = Item.WorkOrderID,
                            WorkOrderNo = Item.WorkOrderNo,
                            // ExpectSampleCollDate = (DateTime)Item.ExpectSampleCollDate,//Sample to be Collected On
                            //CollectedOn = Item.ExpectSampleCollDate.ToString("dd/MM/yyyy"),
                            // CollectedOn = Item.ExpectSampleCollDate.ToString(),
                            FrequencyMasterId = Item.FrequencyMasterId,
                            Frequency = Item.Frequency,
                            Iteration = Item.Iteration,
                            EmployeeId = Item.EmployeeId, //RoleId,UserName,CollectorName
                                                          // UserName = Item.UserName,
                        }); ;
                    }
                    else
                    {
                        modelList.Add(new SurveyingTeamLeadModel()
                        {
                            LocationSampleCollectionID = Item.LocationSampleCollectionID,
                            SampleCollectionId = Item.SampleCollectionId,
                            EnquirySampleID = Item.EnquirySampleID,
                            EnquiryDetailId = Item.EnquiryDetailId,
                            SampleName = Item.SampleName,//Doubt for Sample No.
                            EnquiryId = Item.EnquiryId,
                            DisplaySampleName = Item.DisplaySampleName,
                            //ProductGroupName = Item.ProductGroupName,
                            //SubGroupName = Item.SubGroupName,
                            SampleTypeProductName = Item.SampleTypeProductName,
                            CustomerName = Item.CustomerName,
                            CurrentStatus = Item.CurrentStatus,
                            WorkOrderID = Item.WorkOrderID,
                            WorkOrderNo = Item.WorkOrderNo,
                            //CollectionDate = Item.CollectionDate,//Sample to be Collected On
                            //CollectedOn = Convert.ToDateTime(Item.CollectionDate).ToString("dd/MM/yyyy"),
                            FrequencyMasterId = Item.FrequencyMasterId,
                            Frequency = Item.Frequency,
                            Iteration = Item.Iteration,
                            EmployeeId = Item.EmployeeId, //RoleId,UserName,CollectorName
                                                          // UserName = Item.UserName,
                        }); ;

                    }
                }
            }
            //ViewBag.CollectorName = BALFactory.dropdownsBAL.GetCollector();
            ViewBag.CollectorName = BALFactory.dropdownsBAL.GetUserList("Sampler", LIMS.User.LabId);
            ViewBag.SurveyTeamData = modelList;
            return View(modelList);
        }

        public PartialViewResult _AssignSampler(int SampleCollectionId)
        {
            SurveyingTeamLeadEntity FirstRecord = new SurveyingTeamLeadEntity();
            SurveyingTeamLeadModel model = new SurveyingTeamLeadModel();

            //ViewBag.CollectorName = BALFactory.dropdownsBAL.GetCollector();
            ViewBag.CollectorName = BALFactory.dropdownsBAL.GetUserList("Sampler", LIMS.User.LabId);
            FirstRecord = BALFactory.surveyingTeamLeadBAL.GetSurveyingTeamLeadEntity(SampleCollectionId);

            if (FirstRecord != null)
            {
                model.EnquiryId = FirstRecord.EnquiryId;
                model.EnquirySampleID = FirstRecord.EnquirySampleID;
                model.EmployeeId = FirstRecord.EmployeeId;//
                model.SampleName = FirstRecord.SampleName;
                model.DisplaySampleName = FirstRecord.DisplaySampleName;
                model.SampleCollectionId = FirstRecord.SampleCollectionId;
                model.SampleCollectedBy = FirstRecord.SampleCollectedBy;
                model.LocationSampleCollectionID = FirstRecord.LocationSampleCollectionID;

            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _AssignSampler(SurveyingTeamLeadModel model)
        {
            CoreFactory.surveyingTeamEntity = new SurveyingTeamLeadEntity();
            CoreFactory.surveyingTeamEntity.EnquirySampleID = model.EnquirySampleID;
            CoreFactory.surveyingTeamEntity.LocationSampleCollectionID = model.LocationSampleCollectionID;
            CoreFactory.surveyingTeamEntity.SampleCollectionId = model.SampleCollectionId;
            CoreFactory.surveyingTeamEntity.SampleCollectedBy = model.SampleCollectedBy;

            if (model.SampleCollectedBy == 1)
            {
                //model.SampleCollectedBy= "LAB";
                CoreFactory.surveyingTeamEntity.SampleCollectedBy = model.SampleCollectedBy;
                CoreFactory.surveyingTeamEntity.EmployeeId = model.EmployeeId;
                int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("CollAssign");
                BALFactory.surveyingTeamLeadBAL.AddCollectedBy(CoreFactory.surveyingTeamEntity);
                BALFactory.samplecollectionBAL.UpdateCollectionStatus(model.SampleCollectionId, (byte)iStatusId);
                BALFactory.surveyingTeamLeadBAL.AddCollector(CoreFactory.surveyingTeamEntity);
            }
            if (model.SampleCollectedBy == 2)
            {
                //model.SampleCollectedBy = "Customer";
                CoreFactory.surveyingTeamEntity.SampleCollectedBy = model.SampleCollectedBy;
                //CoreFactory.surveyingTeamEntity.EmployeeId = model.EmployeeId;
                CoreFactory.surveyingTeamEntity.EmployeeId = "36";//For Mahabal Logins only
                //CoreFactory.surveyingTeamEntity.EmployeeId = "17";
                int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("CollByCust");
                BALFactory.surveyingTeamLeadBAL.AddCollectedBy(CoreFactory.surveyingTeamEntity);
                BALFactory.samplecollectionBAL.UpdateCollectionStatus(model.SampleCollectionId, (byte)iStatusId);
                BALFactory.surveyingTeamLeadBAL.AddCollector(CoreFactory.surveyingTeamEntity);
            }

            //TempData["message"] = " Assigned Successfully";

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }
        public enum ManageMessageId
        {
            CollAssign,
            WOApproved,
            InProgress,

        }
    }
}