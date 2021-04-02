using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Lab;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Lab;
using LIMS_DEMO.Common;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using System.IO;
using LIMS_DEMO.Core.Configuration;



namespace LIMS_DEMO.Areas.Lab.Controllers
{
    public class TesterController : Controller
    {
        // GET: Lab/Tester
        public TesterController()
        {
            BALFactory.testerBAL = new TesterBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }
        public ActionResult SampleList()
        {
            var DisciplineId = CoreFactory.userEntity.DisciplineId;
            int UserMasterId = CoreFactory.userEntity.UserMasterID;

            IList<Core.Lab.TesterEntity> testerEntities = BALFactory.testerBAL.GetList(DisciplineId, UserMasterId);
            return View(testerEntities);
           
        }
        public ActionResult ParameterList(string SampleId)
        {
            int DisciplineId = CoreFactory.userEntity.DisciplineId;
            int UserMasterId = CoreFactory.userEntity.UserMasterID;

            SampleParameterInfo sampleParameterInfo = BALFactory.testerBAL.GetParameterInfo(Convert.ToInt32(SampleId), DisciplineId, UserMasterId);
            return View(sampleParameterInfo);

        }
        public ActionResult Tester(long ParameterMasterId, long SampleId)
        {
            int DisciplineId = CoreFactory.userEntity.DisciplineId;
            int UserMasterId = CoreFactory.userEntity.UserMasterID;
            SampleParameterAnalysis sampleParameterAnalysis = BALFactory.testerBAL.GetSampleParameterAnalysis(Convert.ToInt32(SampleId), DisciplineId, UserMasterId, ParameterMasterId);
            string QRCodeText = "SampleNumber:" + sampleParameterAnalysis.SampleNameOriginal + ",ParameterName:" + sampleParameterAnalysis.ParameterName + ",ParameterId:" + sampleParameterAnalysis.ParameterMasterId.ToString();

            string imgPath = "QRCode_" + SampleId + ParameterMasterId + ".jpg";
            sampleParameterAnalysis.QRCodePath = imgPath;
            GenerateCode(QRCodeText, imgPath);
            ViewBag.SampleCollectionId = SampleId;
            return View(sampleParameterAnalysis);
          
        }
        private void GenerateCode(string text, string FileName)
        {
            try
            {
                var writer = new BarcodeWriter();
                writer.Format = BarcodeFormat.QR_CODE;
                var result = writer.Write(text);
                string path = Server.MapPath("/Content/images/" + FileName);
                var barcodeBitmap = new Bitmap(result);
                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public long SaveSolutionPreparationData(string model)
        {
            try
            {
                SolutionPreparationData solutionPreparationData = JsonConvert.DeserializeObject<SolutionPreparationData>(model);
                solutionPreparationData.EnteredBy = CoreFactory.userEntity.UserMasterID;

                long Id = BALFactory.testerBAL.SaveSolutionPreparationData(solutionPreparationData);
                return Id;
            }
            catch (Exception ex)
            {
                
                return 0;
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

                IList<SolutionPreparationData> objSolutionPreparationData = BALFactory.testerBAL.GetSolutionPreparationData(solutionPreparationData);
                return Json(objSolutionPreparationData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }
        public JsonResult GetChemicalUsedData(int ParameterMasterId, int SampleCollectionId, int ParameterGroupId, string nameOfSolution)
        {
            try
            {
                SolutionPreparationData solutionPreparationData = new SolutionPreparationData();
                solutionPreparationData.ParameterMasterId = ParameterMasterId;
                solutionPreparationData.SampleCollectionId = SampleCollectionId;
                solutionPreparationData.ParameterGroupId = ParameterGroupId;
                solutionPreparationData.NameOfSolution = nameOfSolution;
                IList<SolutionPreparationData> objSolutionPreparationData = BALFactory.testerBAL.GetChemicalUsedData(solutionPreparationData);
                return Json(objSolutionPreparationData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }

        public JsonResult InsertAvgValue(int ParameterMasterId, int SampleCollectionId, int ParameterGroupId, string[] avgResult)
        {
            try
            {
                bool result = BALFactory.testerBAL.UpdateAvgValue(SampleCollectionId, ParameterMasterId, ParameterGroupId, avgResult);
                return Json("Calculated Values Updated.");
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }
        public JsonResult GetFormula(int ParameterMasterId, int TestMethodId, int ParameterGroupId, int UnitId, int SampleCollectionId)
        {
            try
            {
                Core.Configuration.ParameterFormulaList objParam = new Core.Configuration.ParameterFormulaList();
                objParam.ParameterId = ParameterMasterId;
                objParam.TestMethodId = TestMethodId;
                objParam.ParameterGroupId = ParameterGroupId;
                objParam.UnitId = UnitId;
                objParam.SampleCollectionId = SampleCollectionId;
                Core.Configuration.ParameterFormulaList parameterFormulaList = BALFactory.testerBAL.GetFormula(objParam);

                return Json(parameterFormulaList, JsonRequestBehavior.AllowGet);
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
                Core.Configuration.ParameterFormulaList finalResultRow = BALFactory.testerBAL.GetFinalResultRows(objParam);
                return Json(finalResultRow, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }

        public JsonResult UpdateCalculatedValue(string FormulaList, int SampleCollectionId, string TestingHours)
        {
            try
            {
                List<Core.Configuration.FormulaList> formulaList = JsonConvert.DeserializeObject<List<Core.Configuration.FormulaList>>(FormulaList);
                int UserMasterId = CoreFactory.userEntity.UserMasterID;
                bool result = BALFactory.testerBAL.UpdateCalculatedValue(formulaList, SampleCollectionId, UserMasterId, TestingHours);
                //bool result2 = BALFactory.analyseBAL.UpdateAvgValue(formulaList, SampleCollectionId, UserMasterId, TestingHours);
                return Json("Calculated Values Updated.");
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }
        public JsonResult UpdateFinalResult(string model, bool isSubmitted)
        {
            try
            {
                AnalysisProcessScheduleDetail analysisProcessScheduleDetail = JsonConvert.DeserializeObject<AnalysisProcessScheduleDetail>(model);

                analysisProcessScheduleDetail.EnteredBy = CoreFactory.userEntity.UserMasterID;
                analysisProcessScheduleDetail.EnteredDate = DateTime.UtcNow;
                var status = BALFactory.testerBAL.UpdateFinalResult(analysisProcessScheduleDetail, isSubmitted);

                string Msg = "Sample Received for Approve";//samples received for review from tester

                long NotificationDetailId = BALFactory.testerBAL.AddNotification(Msg, "Planner", analysisProcessScheduleDetail);
                long NotificationDetailId1 = BALFactory.testerBAL.AddNotification(Msg, "Reviewer", analysisProcessScheduleDetail);

                return Json(status);
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
                TestProcessScheduleDetail apsd = JsonConvert.DeserializeObject<TestProcessScheduleDetail>(model);

                TestProcessScheduleDetail testProcessScheduleDetail = BALFactory.testerBAL.GetTestProcessScheduler(apsd);
                return Json(testProcessScheduleDetail, JsonRequestBehavior.AllowGet);
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

                IList<SampleParameterFileInfo> sampleParameterFileInfos = BALFactory.testerBAL.GetParameterFileInfo(sampleParameterFileInfo);
                return Json(sampleParameterFileInfos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
              
                return null;
            }
        }
        public bool DeleteFile(string FileIdToDelete)
        {
            try
            {
                var status = BALFactory.testerBAL.DeleteFile(FileIdToDelete);
                return status;
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }
        public ActionResult UploadFiles()
        {
            try
            {
                List<SampleParameterFileInfo> sampleParameterFileInfos = new List<SampleParameterFileInfo>();
                HttpFileCollectionBase files = Request.Files;
                string ParameterMasterId = Request["ParameterMasterId"];
                string SampleCollectionId = Request["SampleCollectionId"];
                string ParameterGroupId = Request["ParameterGroupId"];

                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    string fname;
                    // Checking for Internet Explorer      
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }


                    string filename = Path.GetFileName(fname
                        );
                    string storepath = "/Uploads/Tester/" + filename.Replace(" ", "") + "";
                    if (!Directory.Exists(Server.MapPath("/Uploads/Tester")))
                    {
                        Directory.CreateDirectory(Server.MapPath("/Uploads/Tester"));
                    }
                    string targetPath = Server.MapPath("~/Uploads/Tester/") + filename.Replace(" ", "") + "";
                    file.SaveAs(targetPath);


                    // Get the complete folder path and store the file inside it.      
                    //fname = Path.Combine(Server.MapPath("~/Content/Uploads/"), fname);
                    //file.SaveAs(fname);
                    SampleParameterFileInfo sampleParameterFileInfo = new SampleParameterFileInfo();
                    sampleParameterFileInfo.FileName = filename;
                    sampleParameterFileInfo.ParameterMasterId = Convert.ToInt32(ParameterMasterId);
                    sampleParameterFileInfo.ParameterGroupId = Convert.ToInt32(ParameterGroupId);
                    sampleParameterFileInfo.SampleCollectionId = Convert.ToInt32(SampleCollectionId);
                    sampleParameterFileInfo.UserMasterID = CoreFactory.userEntity.UserMasterID;
                    sampleParameterFileInfo.IsActive = true;
                    sampleParameterFileInfos.Add(sampleParameterFileInfo);
                }
                bool status = BALFactory.testerBAL.SaveAnalysisProcessFileInfo(sampleParameterFileInfos);
                if (status)
                {
                    return Json(" Your files uploaded successfully", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(" Error in files upload!", JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {

                return null;
            }

        }
        [Route("FieldDeterminationTester")]
        public ActionResult FieldDeterminationTester(long ParameterMasterId, long SampleId, string Url = "", int? EnquiryId = 0, int? EnquirySampleID = 0, int? FDStackEmissionId = 0, int? FieldId = 0)
        {

            SampleParameterAnalysis sampleParameterAnalysis = BALFactory.testerBAL.GetFDData(Convert.ToInt32(SampleId));

            if (Url == "Ambient Air")
            {

                return RedirectToAction("FieldAmbientAirMonitoring", "FieldAmbientAirMonitoring", new { area = "FieldDetermination", SampleId, EnquiryId, EnquirySampleID, sampleParameterAnalysis.FieldId });


            }


            //if (Url == "Noise")
            //{
            //    return RedirectToAction("FieldNoiseLevelMonitoring", "FieldNoiseLevelMonitoring", new { area = "FieldDetermination", SampleId, EnquiryId, EnquirySampleID, FieldNoiseId });


            //}

            if (Url == "Stack Emission")
            {
                return RedirectToAction("FieldStackEmissionMonitoring", "FieldStackEmissionMonitoring", new { area = "FieldDetermination", SampleId, EnquiryId, EnquirySampleID, sampleParameterAnalysis.FDStackEmissionId });


            }

            return RedirectToAction("Analysis");
        }
    }

}