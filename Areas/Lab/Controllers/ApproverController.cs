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
    public class ApproverController : Controller
    {
        // GET: Lab/Approver
        public ApproverController()
        {
            BALFactory.approverBAL = new ApproverBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }
        public ActionResult SampleList()
        {
            var DisciplineId = CoreFactory.userEntity.DisciplineId;
            int UserMasterId = CoreFactory.userEntity.UserMasterID;
            IList<Core.Lab.ApproverEntity> approveEntity = BALFactory.approverBAL.GetList(DisciplineId, UserMasterId);
            return View(approveEntity);
            
        }
        public ActionResult ParameterList(string SampleId)
        {
            int DisciplineId = CoreFactory.userEntity.DisciplineId;
            int UserMasterId = CoreFactory.userEntity.UserMasterID;

            SampleParameterInfoapprover sampleParameterInfo = BALFactory.approverBAL.GetParameterInfo(Convert.ToInt32(SampleId), DisciplineId, UserMasterId);
            return View(sampleParameterInfo);
            
        }
        public ActionResult Approver(long ParameterMasterId, long SampleId)
        {
            int DisciplineId = CoreFactory.userEntity.DisciplineId;
            int UserMasterId = CoreFactory.userEntity.UserMasterID;
            SampleParameterApprove sampleParameterApprove = BALFactory.approverBAL.GetSampleParameterApprove(Convert.ToInt32(SampleId), DisciplineId, UserMasterId, ParameterMasterId);
            string QRCodeText = "SampleNumber:" + sampleParameterApprove.SampleName + ",ParameterName:" + sampleParameterApprove.ParameterName + ",ParameterId:" + sampleParameterApprove.ParameterMasterId.ToString();

            string imgPath = "../../Content/images/" + "QRCode_" + SampleId + ParameterMasterId + ".jpg";
            sampleParameterApprove.QRCodePath = imgPath;

            return View(sampleParameterApprove);
           
        }

        public JsonResult GetSolutionPreparationData(int ParameterMasterId, int SampleCollectionId, int ParameterGroupId)
        {
            try
            {
                SolutionPreparationData solutionPreparationData = new SolutionPreparationData();
                solutionPreparationData.ParameterMasterId = ParameterMasterId;
                solutionPreparationData.SampleCollectionId = SampleCollectionId;
                solutionPreparationData.ParameterGroupId = ParameterGroupId;

                IList<SolutionPreparationData> objSolutionPreparationData = BALFactory.approverBAL.GetSolutionPreparationData(solutionPreparationData);
                return Json(objSolutionPreparationData, JsonRequestBehavior.AllowGet);

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
                Core.Configuration.ParameterFormulaList parameterFormulaList = BALFactory.approverBAL.GetFormula(objParam);
                return Json(parameterFormulaList, JsonRequestBehavior.AllowGet);
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

                IList<TestingHours> objTestingData = BALFactory.approverBAL.GetTestingHoursData(testingData);
                return Json(objTestingData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
              
                return null;
            }
        }

        public JsonResult GetTestProcessScheduler(string model)
        {
            try
            {
                TestProcessScheduleDetailApprove apsd = JsonConvert.DeserializeObject<TestProcessScheduleDetailApprove>(model);

                TestProcessScheduleDetailApprove testProcessScheduleDetail = BALFactory.approverBAL.GetTestProcessScheduler(apsd);
                return Json(testProcessScheduleDetail, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }

        public string UpdateApproverComment(string model)
        {
            try
            {
                SampleParameterApprove sampleParameterApprove = JsonConvert.DeserializeObject<SampleParameterApprove>(model);
                var status = BALFactory.approverBAL.UpdateApproverComment(sampleParameterApprove);
                if (sampleParameterApprove.ApproverApprovedStatus == 1)//for approveBtn
                {
                    BALFactory.approverBAL.UpdateApproveStatus(sampleParameterApprove.SampleCollectionId);//Updating Status for Approved Sample only to show Disposal Data

                    var IsNAbl = BALFactory.approverBAL.GetIsnabl((Int32)sampleParameterApprove.ParameterMasterId, (Int32)sampleParameterApprove.ParameterGroupId);
                    if (IsNAbl.IsNABLAccredited == true)
                    {
                        var MaxRange = BALFactory.approverBAL.GetMaxRange((Int32)sampleParameterApprove.ParameterMasterId, (Int32)sampleParameterApprove.ParameterGroupId);
                        if (MaxRange.MaxRange != null)
                        {
                            var FinalResult = BALFactory.approverBAL.GetFinalResult(sampleParameterApprove.SampleCollectionId, (Int32)sampleParameterApprove.ParameterMasterId, (Int32)sampleParameterApprove.ParameterGroupId);
                            decimal FinalResult2 = Convert.ToDecimal(FinalResult.FinalResult);
                            if (FinalResult2 > Convert.ToDecimal(MaxRange.MaxRange))
                            {
                                var ULR = BALFactory.approverBAL.GetULRNo(sampleParameterApprove.SampleCollectionId);
                                if (ULR != null)
                                {
                                    ULR.ULRNo = ULR.ULRNo;
                                    IsNAbl.IsNABLAccredited = false;

                                    var UPdateULR = BALFactory.approverBAL.UpdateULRNo(sampleParameterApprove.SampleCollectionId, ULR.ULRNo, (bool)IsNAbl.IsNABLAccredited, sampleParameterApprove.AnalysisProcessScheduleDetailId, sampleParameterApprove.WorkOrderId);

                                    var GetIsNABLSave = BALFactory.approverBAL.GetISNABLSave((Int32)sampleParameterApprove.ParameterGroupId, (Int32)sampleParameterApprove.ParameterMasterId, sampleParameterApprove.WorkOrderId);

                                    for (int i = 0; i < GetIsNABLSave.Count; i++)
                                    {
                                        if (GetIsNABLSave[i].IsNAblSave == false)
                                        {
                                            bool updateISNablSave = BALFactory.approverBAL.UpdateIsNablSave((Int32)sampleParameterApprove.ParameterGroupId, (Int32)sampleParameterApprove.ParameterMasterId, sampleParameterApprove.WorkOrderId, sampleParameterApprove.AnalysisProcessScheduleDetailId);
                                        }
                                    }
                                }


                            }
                            else
                            {
                                var ULR = BALFactory.approverBAL.GetULRNo(sampleParameterApprove.SampleCollectionId);
                                ULR.ULRNo = ULR.ULRNo;

                                var UPdateULR = BALFactory.approverBAL.UpdateULRNo(sampleParameterApprove.SampleCollectionId, ULR.ULRNo, (bool)IsNAbl.IsNABLAccredited, sampleParameterApprove.AnalysisProcessScheduleDetailId, sampleParameterApprove.WorkOrderId);
                                var GetIsNABLSave = BALFactory.approverBAL.GetISNABLSave((Int32)sampleParameterApprove.ParameterGroupId, (Int32)sampleParameterApprove.ParameterMasterId, sampleParameterApprove.WorkOrderId);

                                for (int i = 0; i < GetIsNABLSave.Count; i++)
                                {
                                    if (GetIsNABLSave[i].IsNAblSave == false)
                                    {
                                        bool updateISNablSave = BALFactory.approverBAL.UpdateIsNablSave((Int32)sampleParameterApprove.ParameterGroupId, (Int32)sampleParameterApprove.ParameterMasterId, sampleParameterApprove.WorkOrderId, sampleParameterApprove.AnalysisProcessScheduleDetailId);
                                    }
                                }

                            }
                        }
                        else
                        {
                            var ULR = BALFactory.approverBAL.GetULRNo(sampleParameterApprove.SampleCollectionId);
                            ULR.ULRNo = ULR.ULRNo;

                            var UPdateULR = BALFactory.approverBAL.UpdateULRNo(sampleParameterApprove.SampleCollectionId, ULR.ULRNo, (bool)IsNAbl.IsNABLAccredited, sampleParameterApprove.AnalysisProcessScheduleDetailId, sampleParameterApprove.WorkOrderId);
                            var GetIsNABLSave = BALFactory.approverBAL.GetISNABLSave((Int32)sampleParameterApprove.ParameterGroupId, (Int32)sampleParameterApprove.ParameterMasterId, sampleParameterApprove.WorkOrderId);

                            for (int i = 0; i < GetIsNABLSave.Count; i++)
                            {
                                if (GetIsNABLSave[i].IsNAblSave == false)
                                {
                                    bool updateISNablSave = BALFactory.approverBAL.UpdateIsNablSave((Int32)sampleParameterApprove.ParameterGroupId, (Int32)sampleParameterApprove.ParameterMasterId, sampleParameterApprove.WorkOrderId, sampleParameterApprove.AnalysisProcessScheduleDetailId);
                                }
                            }

                        }
                    }
                    else
                    {
                        var ULR = BALFactory.approverBAL.GetULRNo(sampleParameterApprove.SampleCollectionId);
                        ULR.ULRNo = ULR.ULRNo;
                        IsNAbl.IsNABLAccredited = false;
                        var UPdateULR = BALFactory.approverBAL.UpdateULRNo(sampleParameterApprove.SampleCollectionId, ULR.ULRNo, (bool)IsNAbl.IsNABLAccredited, sampleParameterApprove.AnalysisProcessScheduleDetailId, sampleParameterApprove.WorkOrderId);
                        var GetIsNABLSave = BALFactory.approverBAL.GetISNABLSave((Int32)sampleParameterApprove.ParameterGroupId, (Int32)sampleParameterApprove.ParameterMasterId, sampleParameterApprove.WorkOrderId);
                        for (int i = 0; i < GetIsNABLSave.Count; i++)
                        {
                            if (GetIsNABLSave[i].IsNAblSave == false)
                            {
                                bool updateISNablSave = BALFactory.approverBAL.UpdateIsNablSave((Int32)sampleParameterApprove.ParameterGroupId, (Int32)sampleParameterApprove.ParameterMasterId, sampleParameterApprove.WorkOrderId, sampleParameterApprove.AnalysisProcessScheduleDetailId);
                            }
                        }

                    }
                    if (sampleParameterApprove.ProbableDateOfReport >= DateTime.Now)
                    {
                        string Msg = "Report Submitted";//reports to be submitted 
                        long NotificationDetailId = BALFactory.approverBAL.AddNotification(Msg, "Sample Receipt and Report Incharge", sampleParameterApprove);
                        long NotificationDetailId1 = BALFactory.approverBAL.AddNotification(Msg, "Sample Receiver", sampleParameterApprove);
                    }
                    else
                    {
                        string Msg = "Report Generated after Probable Date Of Report";//on samples not reported after PDR for final report generation.
                        long NotificationDetailId = BALFactory.approverBAL.AddNotification(Msg, "ADMIN", sampleParameterApprove);
                        long NotificationDetailId1 = BALFactory.approverBAL.AddNotification(Msg, "BDM", sampleParameterApprove);
                    }
                }

                if (sampleParameterApprove.ApproverApprovedStatus == 0)//for rejectBtn
                {
                    string Msg = "Sample Received Back from Approver";//sample received back from approver
                    long NotificationDetailId = BALFactory.approverBAL.AddNotification(Msg, "Planner", sampleParameterApprove);
                    long NotificationDetailId1 = BALFactory.approverBAL.AddNotification(Msg, "Reviewer", sampleParameterApprove);
                }

                return "Approver Commented";
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }
        public JsonResult GetFinalResRow(int ParameterMasterId, int TestMethodId, int ParameterGroupId, int UnitId, int SampleCollectionId)
        {
            try
            {
                Core.Configuration.ParameterFormulaList objParam = new Core.Configuration.ParameterFormulaList();
                objParam.ParameterId = ParameterMasterId;
                objParam.TestMethodId = TestMethodId;
                objParam.ParameterGroupId = ParameterGroupId;
                objParam.UnitId = UnitId;
                objParam.SampleCollectionId = SampleCollectionId;
                // by nitin
                Core.Configuration.ParameterFormulaList finalResultRow = BALFactory.approverBAL.GetFinalResultRows(objParam);

                return Json(finalResultRow, JsonRequestBehavior.AllowGet);
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

                IList<SampleParameterFileInfo> sampleParameterFileInfos = BALFactory.approverBAL.GetParameterFileInfo(sampleParameterFileInfo);
                return Json(sampleParameterFileInfos, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                
                return null;
            }

        }
        [Route("FieldDeterminationApprover")]
        public ActionResult FieldDeterminationApprover(long SampleId, string Url = "", int? EnquirySampleID = 0)
        {
            int EnquiryID = 0;
            SampleParameterApprove sampleParameterApprover = BALFactory.approverBAL.GetFDData(Convert.ToInt32(SampleId));

            bool flag = true;
            if (Url == "Ambient Noise Level" || Url == "Workplace Noise Level")
            {
                flag = true;
            }
            if (Url == "Source Noise Level")
            {
                flag = false;
            }
            if (Url == "Ambient Noise Level" || Url == "Source Noise Level" || Url == "Workplace Noise Level")
            {
                return RedirectToAction("FieldNoiseLevelMonitoring", "FieldNoiseLevelMonitoring", new { area = "FieldDetermination", flag, SampleId, EnquiryID, EnquirySampleID, sampleParameterApprover.FieldId });
            }
            return RedirectToAction("TestResultParameter");
        }

    }
}