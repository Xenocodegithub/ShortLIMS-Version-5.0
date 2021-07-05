using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Enquiry.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.DropDown;

namespace LIMS_DEMO.Areas.Enquiry.Controllers
{
    [RouteArea("Enquiry")]
    public class SampleAndParameterController : Controller
    {
        public SampleAndParameterController()
        {
            BALFactory.contractBAL = new BAL.Enquiry.ContractBAL();
            BALFactory.sampleParameterBAL = new BAL.Enquiry.SampleParameterBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
        }
        //GET: Enquiry/SampleAndParameter
        public ActionResult SampleAndParameter(int EnquiryId)
        {
            ViewBag.EnquiryId = EnquiryId;
            return View();
        }
        public PartialViewResult _SampleContract(int EnquiryId)
        {
            var contractList = BALFactory.contractBAL.GetContractDetails(EnquiryId);
            IList<ContractModel> model = new List<ContractModel>();
            int iSrNo = 1;
            foreach (var item in contractList)
            {
                model.Add(new ContractModel()
                {
                    SerialNo = iSrNo,
                    EnquiryDetailId = item.EnquiryDetailId,
                    EnquiryId = EnquiryId,
                    //ProductGroupId = item.ProductGroupId,
                    //ProductGroupName = item.ProductGroupName,
                    //SubGroupId = item.SubGroupId,
                    SampleTypeProductId = (Int32)item.SampleTypeProductId,
                    SampleTypeProductCode = item.SampleTypeProductCode,
                    SampleTypeProductName = item.SampleTypeProductName,
                    //SubGroupName = item.SubGroupName,
                    //SubGroupCode = item.SubGroupCode,
                });
                iSrNo++;
            }
            return PartialView(model);
        }
        public ActionResult _Parameters(int EnquiryDetailId, int? EnquirySampleID = 0, int? EnquiryId = 0, string SubroupCode = "", string SampleTypeProductCode = "", int? SampleTypeProductId = 0,int? EnquiryParameterDetailID = 0)
        {
            SampleAndParametersModel model = new SampleAndParametersModel();
            model.ParameterList = new List<EnquiryParameterModel>();
            int EnquirySMID = 0;

            if (EnquiryDetailId == 0)
            {
                return View(model);
            }
            CoreFactory.enqurySampleEntity = new EnquirySampleEntity();
            CoreFactory.enqurySampleEntity.EnquiryDetailId = EnquiryDetailId;

            var Samplename = BALFactory.sampleParameterBAL.GetSampleNumber((Int32)EnquiryId, (Int32)EnquiryDetailId, (Int32)SampleTypeProductId);
            if (Samplename != null)
            {
                if (Samplename.DisplaySampleName != "")
                {
                    CoreFactory.enqurySampleEntity.SampleName = Samplename.SampleName;
                    CoreFactory.enqurySampleEntity.DisplaySampleName = Samplename.DisplaySampleName;
                }

            }
            else
            {
                EnquirySMID = (Int32)BALFactory.sampleParameterBAL.GetEnquirySampleID();
                //CoreFactory.enqurySampleEntity.DisplaySampleName = BALFactory.sampleParameterBAL.GenerateDisplaySampleName();
                //CoreFactory.enqurySampleEntity.SampleName = SampleTypeProductCode.ToString() + "/" + CoreFactory.enqurySampleEntity.DisplaySampleName;
                var name = BALFactory.sampleParameterBAL.GenerateDisplaySampleName();
                CoreFactory.enqurySampleEntity.SampleName = SampleTypeProductCode.ToString() + "/" + name;
                CoreFactory.enqurySampleEntity.DisplaySampleName = SampleTypeProductCode.ToString() + BALFactory.sampleParameterBAL.GetSampleCount(Convert.ToInt32(DateTime.Now.Year), Convert.ToInt32(DateTime.Today.Month)) + "/" + name;

            }
            if (EnquirySampleID == 0)
            {
                CoreFactory.enqurySampleEntity = BALFactory.sampleParameterBAL.AddEnquirySampleDetail(CoreFactory.enqurySampleEntity);
            }
            else
            {
                CoreFactory.enqurySampleEntity = BALFactory.sampleParameterBAL.GetEnquirySampleDetail((Int32)EnquirySampleID);
            }

            CoreFactory.enquiryParameterList = BALFactory.sampleParameterBAL.GetSampleParameterList(EnquiryDetailId, (Int32)EnquirySampleID, (Int32)SampleTypeProductId);

            model.EnquiryDetailId = CoreFactory.enqurySampleEntity.EnquiryDetailId;
            model.EnquirySampleID = CoreFactory.enqurySampleEntity.EnquirySampleID;
            model.SampleName = CoreFactory.enqurySampleEntity.SampleName;
            model.DisplaySampleName = CoreFactory.enqurySampleEntity.DisplaySampleName;

            foreach (var param in CoreFactory.enquiryParameterList)
            {
                try
                {
                    model.ParameterList.Add(new EnquiryParameterModel()
                    {
                        ParameterMappingId = param.ParameterMappingId,
                        EnquiryParameterDetailID = param.EnquiryParameterDetailID,
                        EnquirySampleID = param.EnquirySampleID,
                        ParameterMasterId = param.ParameterMasterId,
                        ParameterGroupId = param.ParameterGroupId,
                        ParameterName = param.ParameterName,
                        Discipline = param.Discipline,
                        TestMethodId = param.TestMethodId,
                        ProductGroupId = param.ProductGroupId,
                        SubGroupId = param.SubGroupId,
                        MatrixId = param.MatrixId,
                        Remarks = param.Remarks,
                        Infield = param.Infield,
                        //UnitId = param.UnitId,
                        LowerLimit = param.LowerLimit,
                        UpperLimit = param.UpperLimit,
                        //Charges = param.Charges == Decimal.Zero ? Decimal.Zero : Decimal.Round((decimal)param.Charges, 1, MidpointRounding.AwayFromZero),
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
            ViewBag.TestMethod = BALFactory.dropdownsBAL.GetTestMethods();
            ViewBag.Unit = BALFactory.dropdownsBAL.GetUnits();
            ViewBag.Branch = BALFactory.dropdownsBAL.GetBranches(LIMS.User.LabId);
            return View(model);
        }
           
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _Parameters(SampleAndParametersModel model)
        {
            CoreFactory.enquiryParameterList = new List<EnquiryParameterEntity>();
           
            foreach (var par in model.ParameterList)
            {
                if (par.IsSelected)
                {
                    CoreFactory.enquiryParameterList.Add(new EnquiryParameterEntity()
                    {
                        EnquiryParameterDetailID = par.EnquiryParameterDetailID,
                        EnquirySampleID = model.EnquirySampleID,
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
            BALFactory.sampleParameterBAL.UpdateEnquirySampleCharges(model.EnquirySampleID, model.TotalCharges);
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFormula(int ParameterMasterId, int ParameterMappingId, int TestMethodId, int ParameterGroupId, int UnitId, bool Infield)
        {
            try
            {
                Core.Enquiry.EnquiryParameterEntity objParam = new Core.Enquiry.EnquiryParameterEntity();
                objParam.ParameterMasterId = ParameterMasterId;
                objParam.ParameterMappingId = ParameterMappingId;
                objParam.TestMethodId = TestMethodId;
                objParam.ParameterGroupId = ParameterGroupId;
                objParam.UnitId = UnitId;
                objParam.Infield = Infield;
                var valuecheck = BALFactory.sampleParameterBAL.GetFormula(objParam);
                return Json(valuecheck, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PartialViewResult _SampleList(int? EnquiryId = 0)
        {
            List<SampleAndParametersModel> model = new List<SampleAndParametersModel>();
            CoreFactory.enquirySampleList = BALFactory.sampleParameterBAL.GetEnquirySampleList((Int32)EnquiryId);
            int i = 1;

            foreach (var sample in CoreFactory.enquirySampleList)
            {
                                model.Add(new SampleAndParametersModel()
                {
                    SrNo = i,

                    EnquirySampleID = sample.EnquirySampleID,
                    EnquiryDetailId = sample.EnquiryDetailId,
                    SampleName = sample.SampleName,
                    DisplaySampleName = sample.DisplaySampleName,
                    //ProductGroupName = sample.ProductGroupName,
                    //SubGroupName = sample.SubGroupName,
                    SampleTypeProductName = sample.SampleTypeProductName,
                    CurrentStatus = sample.CurrentStatus,
                    Parameters = BALFactory.sampleParameterBAL.GetSampleParameters((Int32)sample.EnquirySampleID), //sample.Parameters,
                    //Cost = sample.Cost,
                    Remarks = BALFactory.sampleParameterBAL.GetParameterRemarks((Int32)sample.EnquirySampleID), //sample.Parameters,,
                });
                i++;
                //}

            }
            ViewBag.EnquiryId = EnquiryId;
            return PartialView(model);
        }
        public ActionResult _ParameterPCBLimit(long? EnquirySampleID = 0, int? EnquiryId = 0)
        {
            SampleAndParametersModel model = new SampleAndParametersModel();
            model.ParameterList = new List<EnquiryParameterModel>();
            model.EnquiryId = (long)EnquiryId;
            CoreFactory.enqurySampleEntity = new EnquirySampleEntity();

            if (EnquirySampleID != 0)
            {
                CoreFactory.enqurySampleEntity = BALFactory.sampleParameterBAL.GetEnquirySampleDetail((Int32)EnquirySampleID);
            }
            else
            {
                return View(model);
            }
            CoreFactory.enquiryParameterList = BALFactory.sampleParameterBAL.GetSampleParameterList((Int32)EnquirySampleID);

            model.EnquiryDetailId = CoreFactory.enqurySampleEntity.EnquiryDetailId;
            model.EnquirySampleID = CoreFactory.enqurySampleEntity.EnquirySampleID;
            model.SampleName = CoreFactory.enqurySampleEntity.SampleName;
            model.DisplaySampleName = CoreFactory.enqurySampleEntity.DisplaySampleName;
            foreach (var param in CoreFactory.enquiryParameterList)
            {
                model.ParameterList.Add(new EnquiryParameterModel()
                {
                    EnquiryParameterDetailID = param.EnquiryParameterDetailID,
                    EnquirySampleID = model.EnquirySampleID,
                    ParameterMasterId = (Int32)param.ParameterMasterId,
                    ParameterMappingId = param.ParameterMappingId,
                    ParameterName = param.ParameterName,
                    PCBLimit = param.PCBLimit
                });

            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _ParameterPCBLimit(SampleAndParametersModel model)
        {
            CoreFactory.enquiryParameterList = new List<EnquiryParameterEntity>();
            foreach (var par in model.ParameterList)
            {
                CoreFactory.enquiryParameterList.Add(new EnquiryParameterEntity()
                {
                    EnquiryParameterDetailID = par.EnquiryParameterDetailID,
                    EnquirySampleID = model.EnquirySampleID,
                    ParameterMasterId = (Int32)par.ParameterMasterId,
                    ParameterMappingId = par.ParameterMappingId,
                    ParameterName = par.ParameterName,
                    PCBLimit = par.PCBLimit,
                    EnteredBy = LIMS.User.UserMasterID
                });
            }

            BALFactory.sampleParameterBAL.UpdateEnquiryParameterPCB(CoreFactory.enquiryParameterList);
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult _DeleteSample(long? EnquirySampleId = 0, string CurrentStatus = " ")
        {
            List<SampleAndParametersModel> model = new List<SampleAndParametersModel>();
            if (EnquirySampleId != 0)
            {
                BALFactory.sampleParameterBAL.DeleteSample((long)EnquirySampleId, false, CurrentStatus);
            }
            return Json(new { status = "success", Message = "Sample Deleted" }, JsonRequestBehavior.AllowGet);
        }
    }
}