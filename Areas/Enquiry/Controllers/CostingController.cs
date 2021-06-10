using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Enquiry.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Enquiry;
using System.Configuration;

namespace LIMS_DEMO.Areas.Enquiry.Controllers
{
    public class CostingController : Controller
    {
        readonly decimal GSTRate = Convert.ToDecimal(ConfigurationManager.AppSettings["GSTRate"]);
        public CostingController()
        {
            BALFactory.quotationBAL = new QuotationBAL();
            BALFactory.sampleParameterBAL = new BAL.Enquiry.SampleParameterBAL();
            BALFactory.enquiryBAL = new BAL.Enquiry.EnquiryBAL();
            BALFactory.costingBAL = new BAL.Enquiry.CostingBAL();
        }

        // GET: Enquiry/Costing
        public ActionResult AddCosting(int EnquiryId)
        {
            ViewBag.EnquiryId = EnquiryId;
            CostingListModel model = new CostingListModel();
            CoreFactory.enquiryEntity = BALFactory.enquiryBAL.GetDetails(EnquiryId);
            if (CoreFactory.enquiryEntity != null)
            {
                model.EnquiryId = CoreFactory.enquiryEntity.EnquiryId;
                model.CustomerTypeName = CoreFactory.enquiryEntity.CustomerName;
                model.EnquiryOn = CoreFactory.enquiryEntity.EnquiryOn;
            }
            model.SampleList = new List<SampleAndParametersModel>();
            CoreFactory.enquirySampleList = BALFactory.sampleParameterBAL.GetEnquirySampleList((Int32)EnquiryId);
            int i = 1;
            foreach (var sample in CoreFactory.enquirySampleList)
            {
                model.SampleList.Add(new SampleAndParametersModel()
                {
                    SrNo = i,
                    EnquirySampleID = sample.EnquirySampleID,
                    EnquiryDetailId = sample.EnquiryDetailId,
                    SampleName = sample.SampleName,
                    DisplaySampleName = sample.DisplaySampleName,
                    ProductGroupName = sample.ProductGroupName,
                    SubGroupName = sample.SubGroupName,
                    MatrixName = sample.MatrixName,
                    Parameters = BALFactory.sampleParameterBAL.GetSampleParameters((Int32)sample.EnquirySampleID), //sample.Parameters,
                    Cost = sample.Cost
                });
                i++;
            }
            model.TermsList = new List<TermsAndConditionsModel>();
            var TermsCond = BALFactory.costingBAL.GetTermsAndCondition(EnquiryId);
            foreach (var t in TermsCond)
            {
                model.TermsList.Add(new TermsAndConditionsModel()
                {
                    TermAndCondtId = t.TermAndCondtId,
                    TermAndCondt = t.TermAndCondt,
                    IsSelected = t.IsSelected
                });
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public PartialViewResult _Costing(int? EnquirySampleID = 0, int? CostingId = 0)
        {
            ViewBag.GSTRate = GSTRate;
            CostingModel model = new CostingModel();
            model.EnquirySampleID = (Int32)EnquirySampleID;
            model.CostingId = (Int32)CostingId;
            CoreFactory.costingEntity = BALFactory.costingBAL.GetCosting((Int32)EnquirySampleID, (Int32)CostingId);
            if (CoreFactory.costingEntity != null)
            {
                model.CostingId = CoreFactory.costingEntity.CostingId;
                model.EnquirySampleID = CoreFactory.costingEntity.EnquirySampleID;
                model.TotalCharges = CoreFactory.costingEntity.TotalCharges == Decimal.Zero ? Decimal.Zero : Decimal.Round((decimal)CoreFactory.costingEntity.TotalCharges, 1, MidpointRounding.AwayFromZero);
                model.SampleAmount = CoreFactory.costingEntity.CollectionCharges == Decimal.Zero ? Decimal.Zero : Decimal.Round((decimal)CoreFactory.costingEntity.CollectionCharges, 1, MidpointRounding.AwayFromZero);
                model.TransportationAmount = CoreFactory.costingEntity.TransportCharges == Decimal.Zero ? Decimal.Zero : Decimal.Round((decimal)CoreFactory.costingEntity.TransportCharges, 1, MidpointRounding.AwayFromZero);
                model.TestingCharges = CoreFactory.costingEntity.TestingCharges == Decimal.Zero ? Decimal.Zero : Decimal.Round((decimal)CoreFactory.costingEntity.TestingCharges, 1, MidpointRounding.AwayFromZero);
                model.TotalAmount = CoreFactory.costingEntity.TotalCharges + CoreFactory.costingEntity.CollectionCharges + CoreFactory.costingEntity.TransportCharges - CoreFactory.costingEntity.Discount;

                if (CoreFactory.costingEntity.CostingId == null || CoreFactory.costingEntity.CostingId == 0)
                {
                    model.GSTCharges = (model.TotalAmount * GSTRate) / 100;
                    model.NetAmount = model.TotalAmount + model.GSTCharges;
                }
                else
                {
                    model.GSTCharges = CoreFactory.costingEntity.GST;
                    model.NetAmount = CoreFactory.costingEntity.UnitPrice;
                }
                model.DiscountAmount = CoreFactory.costingEntity.Discount;
            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _Costing(CostingModel model)
        {
            CoreFactory.costingEntity = new Core.Enquiry.CostingEntity();
            CoreFactory.costingEntity.CostingId = model.CostingId;
            CoreFactory.costingEntity.EnquirySampleID = model.EnquirySampleID;
            //CoreFactory.costingEntity.EnquiryMasterSampleTypeId = model.EnquiryMasterSampleTypeId;
            CoreFactory.costingEntity.TotalCharges = model.TotalCharges;
            CoreFactory.costingEntity.CollectionCharges = model.SampleAmount;
            CoreFactory.costingEntity.TransportCharges = model.TransportationAmount;
            CoreFactory.costingEntity.TestingCharges = model.TestingCharges;
            CoreFactory.costingEntity.GST = model.GSTCharges;
            CoreFactory.costingEntity.Discount = model.DiscountAmount;
            CoreFactory.costingEntity.UnitPrice = model.NetAmount;
            CoreFactory.costingEntity.EnteredBy = LIMS.User.UserMasterID;
            if (model.CostingId == null || model.CostingId == 0)
            {
                BALFactory.costingBAL.AddCosting(CoreFactory.costingEntity);
                return Json(new { status = "added" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                BALFactory.costingBAL.UpdateCosting(CoreFactory.costingEntity);
                return Json(new { status = "updated" }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult _CostingList(int EnquiryId)
        {
            List<CostingModel> model = new List<CostingModel>();
            CoreFactory.costingList = BALFactory.costingBAL.GetCostingList(EnquiryId);
            //CoreFactory.costingList = BALFactory.costingBAL.GetSampleTypeCostingList(EnquiryId);
            int i = 1;
            foreach (var cost in CoreFactory.costingList)
            {
                model.Add(new CostingModel()
                {
                    SrNo = i,
                    CostingId = cost.CostingId,
                    EnquirySampleID = cost.EnquirySampleID,
                    NetAmount = cost.UnitPrice,
                    SampleName = cost.SampleName,
                    DisplaySampleName = cost.DisplaySampleName,
                    //ProductGroupName = cost.ProductGroupName,
                    //SubGroupName = cost.SubGroupName,
                    SampleTypeProductName = cost.SampleTypeProductName
                });
                i++;
            }
            ViewBag.EnquiryId = EnquiryId;
            return PartialView(model);
        }

        public ActionResult TermsAndCondition()
        {
            return View();
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
                    EnquiryMasterSampleTypeId = sample.EnquiryMasterSampleTypeId,
                    DisplaySampleName = sample.DisplaySampleName,
                    SampleTypeProductName = sample.SampleTypeProductName,
                    //ProductGroupName = sample.ProductGroupName,
                    //SubGroupName = sample.SubGroupName,
                    //MatrixName = sample.MatrixName,
                    Parameters = BALFactory.sampleParameterBAL.GetSampleParameters((Int32)sample.EnquirySampleID), //sample.Parameters,
                    Cost = sample.Cost
                });
                i++;
           }
            return PartialView(model);
        }
        public JsonResult DeleteCosting(int CostingId)
        {
            BALFactory.costingBAL.DeleteCosting(CostingId);
            return Json(new { status = "Deleted" }, JsonRequestBehavior.AllowGet);
        }
    }
}