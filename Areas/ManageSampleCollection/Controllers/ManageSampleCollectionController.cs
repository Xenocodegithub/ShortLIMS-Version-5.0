using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.ManageSampleCollection.Models;
using LIMS_DEMO.Areas.Collection.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.ManageSampleCollection;
using LIMS_DEMO.Core.Collection;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.ManageSampleCollection;
using LIMS_DEMO.BAL.Collection;
using LIMS_DEMO.BAL.DropDown;

namespace LIMS_DEMO.Areas.ManageSampleCollection.Controllers
{
    [RouteArea("ManageSampleCollection")]
    public class ManageSampleCollectionController : Controller
    {
        public ManageSampleCollectionController()
        {
            BALFactory.manageSampleBAL = new ManageSampleCollectionBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
            BALFactory.samplecollectionBAL = new SampleCollectionBAL();
            BALFactory.surveyingTeamLeadBAL = new SurveyingTeamLeadBAL();
        }

        // GET: ManageSampleCollection/ManageSampleCollection
       
        public ActionResult SampleCollectionCalendar()
        {
            return View();
        }

      
        public ActionResult GetCalendarData()
        {
            // Initialization.  
            JsonResult result = new JsonResult();
            try
            {
                // Loading.  
                CoreFactory.managesampleList = BALFactory.manageSampleBAL.GetCalendarList();
                // Processing.  
                result = this.Json(CoreFactory.managesampleList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }
            // Return info.  
            return result;
        }


      
        public PartialViewResult _AssignSampler(int SampleCollectionId, int WorkOrderSampleCollectionDateId)
        {
            SurveyingTeamLeadEntity FirstRecord = new SurveyingTeamLeadEntity();
            SurveyingTeamLeadModel model = new SurveyingTeamLeadModel();
            model.WorkOrderSampleCollectionDateId = WorkOrderSampleCollectionDateId;
            //ViewBag.CollectorName = BALFactory.dropdownsBAL.GetCollector();
            ViewBag.CollectorName = BALFactory.dropdownsBAL.GetUserList("Sampler", LIMS.User.LabId);
            FirstRecord = BALFactory.surveyingTeamLeadBAL.GetSurveyingTeamLeadEntity(SampleCollectionId);

            if (FirstRecord != null)
            {
                model.EnquiryId = FirstRecord.EnquiryId;
                model.EnquirySampleID = FirstRecord.EnquirySampleID;
                model.EmployeeId = FirstRecord.EmployeeId;
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
                BALFactory.manageSampleBAL.UpdateCalendarStatus(model.WorkOrderSampleCollectionDateId, 1);

            }
            if (model.SampleCollectedBy == 2)
            {
                //model.SampleCollectedBy = "Customer";
                CoreFactory.surveyingTeamEntity.SampleCollectedBy = model.SampleCollectedBy;
                //CoreFactory.surveyingTeamEntity.EmployeeId = model.EmployeeId;
                //CoreFactory.surveyingTeamEntity.EmployeeId = "36";//For Mahabal Logins only
                CoreFactory.surveyingTeamEntity.EmployeeId = "17";
                int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("CollByCust");
                BALFactory.surveyingTeamLeadBAL.AddCollectedBy(CoreFactory.surveyingTeamEntity);
                BALFactory.samplecollectionBAL.UpdateCollectionStatus(model.SampleCollectionId, (byte)iStatusId);
                BALFactory.surveyingTeamLeadBAL.AddCollector(CoreFactory.surveyingTeamEntity);
                BALFactory.manageSampleBAL.UpdateCalendarStatus(model.WorkOrderSampleCollectionDateId, 1);
            }
            //TempData["message"] = " Assigned Successfully";
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

    }
}