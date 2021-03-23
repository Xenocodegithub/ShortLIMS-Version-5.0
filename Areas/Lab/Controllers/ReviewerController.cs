using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Lab;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Lab;
using LIMS_DEMO.Core.Configuration;
using Newtonsoft.Json;


namespace LIMS_DEMO.Areas.Lab.Controllers
{
    public class ReviewerController : Controller
    {
        // GET: Lab/Reviewer
        public ReviewerController()
        {
            BALFactory.reviewerBAL = new ReviewerBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }
        public ActionResult SampleList()
        {
            var DisciplineId = CoreFactory.userEntity.DisciplineId;
            int UserMasterId = CoreFactory.userEntity.UserMasterID;

            IList<Core.Lab.ReviewerEntity> reviewEntity = BALFactory.reviewerBAL.GetList(DisciplineId, UserMasterId);
            return View(reviewEntity);
           
        }
        public ActionResult ParameterList(string SampleId)
        {
            int DisciplineId = CoreFactory.userEntity.DisciplineId;
            int UserMasterId = CoreFactory.userEntity.UserMasterID;

            SampleParameterInfoReview sampleParameterInfoReview = BALFactory.reviewerBAL.GetParameterInfo(Convert.ToInt32(SampleId), DisciplineId, UserMasterId);
            return View(sampleParameterInfoReview);
            
        }
        public ActionResult Reviewer(long ParameterMasterId, long SampleId)
        {
            int DisciplineId = CoreFactory.userEntity.DisciplineId;
            int UserMasterId = CoreFactory.userEntity.UserMasterID;
            SampleParameterReview sampleParameterReview = BALFactory.reviewerBAL.GetSampleParameterReview(Convert.ToInt32(SampleId), DisciplineId, UserMasterId, ParameterMasterId);
            string QRCodeText = "SampleNumber:" + sampleParameterReview.SampleName + ",ParameterName:" + sampleParameterReview.ParameterName + ",ParameterId:" + sampleParameterReview.ParameterMasterId.ToString();

            string imgPath = "../../Content/images/" + "QRCode_" + SampleId + ParameterMasterId + ".jpg";
            sampleParameterReview.QRCodePath = imgPath;
            return View(sampleParameterReview);
            
        }

        public JsonResult GetTestProcessScheduler(string model)
        {
            try
            {
                TestProcessScheduleDetail apsd = JsonConvert.DeserializeObject<TestProcessScheduleDetail>(model);

                TestProcessScheduleDetail testProcessScheduleDetail = BALFactory.reviewerBAL.GetTestProcessScheduler(apsd);
                return Json(testProcessScheduleDetail, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public JsonResult GetSolutionPreparationData(int ParameterMasterId, int SampleCollectionId, int ParameterGroupId)
        {
            try
            {
                SolutionPreparationData solutionPreparationData = new SolutionPreparationData();
                solutionPreparationData.ParameterMasterId = ParameterMasterId;
                solutionPreparationData.SampleCollectionId = SampleCollectionId;
                solutionPreparationData.ParameterGroupId = ParameterGroupId;

                IList<SolutionPreparationData> objSolutionPreparationData = BALFactory.reviewerBAL.GetSolutionPreparationData(solutionPreparationData);
                return Json(objSolutionPreparationData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                
                return null;
            }

        }

        public JsonResult GetTestingHoursData(int ParameterMasterId, int SampleCollectionId, int ParameterGroupId)
        {
            try
            {
                TestingHours testingData = new TestingHours();
                testingData.ParameterMasterId = ParameterMasterId;
                testingData.SampleCollectionId = SampleCollectionId;
                testingData.ParameterGroupId = ParameterGroupId;

                IList<TestingHours> objTestingData = BALFactory.reviewerBAL.GetTestingHoursData(testingData);
                return Json(objTestingData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }
        public JsonResult GetFormula(int ParameterMasterId, int TestMethodId, int ParameterGroupId, int UnitId, int SampleCollectionId, string TestingHour)
        {
            try
            {
                Core.Configuration.ParameterFormulaList objParam = new Core.Configuration.ParameterFormulaList();
                objParam.ParameterId = ParameterMasterId;
                objParam.TestMethodId = TestMethodId;
                objParam.ParameterGroupId = ParameterGroupId;
                objParam.UnitId = UnitId;
                objParam.SampleCollectionId = SampleCollectionId;
                objParam.TestingHour = TestingHour;
                Core.Configuration.ParameterFormulaList parameterFormulaList = BALFactory.reviewerBAL.GetFormula(objParam);
                return Json(parameterFormulaList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }
        public string UpdateReviewerComment(string model)
        {
            try
            {
                SampleParameterReview sampleParameterReview = JsonConvert.DeserializeObject<SampleParameterReview>(model);
                var status = BALFactory.reviewerBAL.UpdateReviewerComment(sampleParameterReview);
                if (sampleParameterReview.ReviewerApprovedStatus == 1)//for approveBtn
                {
                    string Msg = "Sample Submitted to Approver";//sample submission to approver
                    long NotificationDetailId = BALFactory.reviewerBAL.AddNotification(Msg, "Approver", sampleParameterReview);
                    long NotificationDetailId1 = BALFactory.reviewerBAL.AddNotification(Msg, "Planner", sampleParameterReview);
                }
                if (sampleParameterReview.ReviewerApprovedStatus == 0)//for rejectBtn
                {
                    string Msg = "Sample Received for Re-Test";//on sampler received back from reviewer
                    long NotificationDetailId = BALFactory.reviewerBAL.AddNotification(Msg, "Tester", sampleParameterReview);
                }
                return "Reviewer Commented";
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }
        public JsonResult GetParameterFileInfo(int ParameterMasterId, int SampleCollectionId, int ParameterGroupId)
        {
            try
            {
                SampleParameterFileInfo sampleParameterFileInfo = new SampleParameterFileInfo();
                sampleParameterFileInfo.ParameterMasterId = ParameterMasterId;
                sampleParameterFileInfo.SampleCollectionId = SampleCollectionId;
                sampleParameterFileInfo.ParameterGroupId = ParameterGroupId;

                IList<SampleParameterFileInfo> sampleParameterFileInfos = BALFactory.reviewerBAL.GetParameterFileInfo(sampleParameterFileInfo);
                return Json(sampleParameterFileInfos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }
    }
}