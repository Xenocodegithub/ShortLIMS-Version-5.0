using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Enquiry.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Enquiry;
using LIMS_DEMO.BAL.DropDown;
using System.IO;
namespace LIMS_DEMO.Areas.Enquiry.Controllers
{
    [RouteArea("Enquiry")]
    public class QuotationController : Controller
    {
        public QuotationController()
        {
            BALFactory.quotationBAL = new QuotationBAL();
            BALFactory.costingBAL = new CostingBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
            BALFactory.enquiryBAL = new EnquiryBAL();
        }

        // GET: Enquiry/Quotation
        public ActionResult QuotationList(DateTime? FromDate, DateTime? ToDate)
        {
            if (ToDate == null)
            {
                ToDate = FromDate;
            }
            else if (FromDate == null)
            {
                FromDate = ToDate;
            }
            var Quotations = BALFactory.quotationBAL.GetQuotationList(LIMS.User.LabId, FromDate, ToDate);
            List<QuotationModel> model = new List<QuotationModel>();
            foreach (var quot in Quotations)
            {
                model.Add(new QuotationModel()
                {
                    EnquiryId = quot.EnquiryId,
                    EnquiryNo = quot.EnquiryId.ToString(),
                    QuotationId = quot.QuotationId,
                    QuotationNo = quot.QuotationNo,
                    CustomerName = quot.CustomerName,
                    Status = quot.Status,
                    StatusCode = quot.StatusCode
                });
            }
            return View(model);
        }
        public ActionResult Quotation(int EnquiryId)
        {
            var QuotationDetail = BALFactory.quotationBAL.GetQuotationPreview(EnquiryId);
            QuotationPreviewModel model = new QuotationPreviewModel();
            model.EnquiryId = EnquiryId;
            if (QuotationDetail != null)
            {
                model.EnquiryId = QuotationDetail.EnquiryId;
                model.LabName = QuotationDetail.LabName;
                model.RefName = QuotationDetail.RefName;
                model.CompanyName = QuotationDetail.CompanyName;
                model.ContactPerson = QuotationDetail.ContactPerson;
                model.Designation = QuotationDetail.Designation;
                model.ClientMobileNo = QuotationDetail.ClientMobileNo;
                model.ClientEmail = QuotationDetail.ContactEmail;
                model.PAN = QuotationDetail.PAN;
                model.GSTNO = QuotationDetail.GSTNO;
                model.SACNO = QuotationDetail.SACNO;
                model.BANKERS = QuotationDetail.BANKERS;
                model.PFNO = QuotationDetail.PFNO;
                model.ESICNO = QuotationDetail.ESICNO;
                model.PTNO = QuotationDetail.PTNO;
                model.TDSNO = QuotationDetail.TDSNO;
                model.MSMEDA = QuotationDetail.MSMEDA;
                model.IECNO = QuotationDetail.IECNO;
                model.LABAddress = QuotationDetail.LABAddress;
                model.LABPhone = QuotationDetail.LABPhone;
                model.LABMobileFax = QuotationDetail.LABMobileFax;
                model.LABEmail = QuotationDetail.LABEmail;
                model.RevisedDates = BALFactory.quotationBAL.GetQuotationRevisedDates(EnquiryId);
                model.ContactMobile = QuotationDetail.ContactMobile;
                model.ClientFax = QuotationDetail.ClientFax;
                model.LABCity = QuotationDetail.LABCity;
            }
            model.costing = new List<CostingModel>();
            model.TermsList = new List<TermsAndConditionsModel>();
            CoreFactory.costingList = BALFactory.costingBAL.GetCostingList(EnquiryId);
            int i = 1;
            foreach (var cost in CoreFactory.costingList)
            {
                model.costing.Add(new CostingModel()
                {
                    SrNo = i,
                    CostingId = cost.CostingId,
                    EnquirySampleID = cost.EnquirySampleID,
                    NetAmount = cost.UnitPrice,
                    SampleName = cost.SampleName,
                    DisplaySampleName = cost.DisplaySampleName,
                    ProductGroupName = cost.ProductGroupName,
                    SubGroupName = cost.SubGroupName,
                    MatrixName = cost.MatrixName,
                    Particulars = cost.SampleTypeProductName,
                    Parameters = BALFactory.quotationBAL.GetParamters(cost.EnquirySampleID),
                });
                i++;
            }
            var TermsCond = BALFactory.costingBAL.GetTermsAndCondition(EnquiryId);
            foreach (var t in TermsCond)
            {
                if (t.IsSelected)
                {
                    model.TermsList.Add(new TermsAndConditionsModel()
                    {
                        TermAndCondtId = t.TermAndCondtId,
                        TermAndCondt = t.TermAndCondt,
                        IsSelected = t.IsSelected
                    });
                }
            }
            var HODetails = BALFactory.quotationBAL.GetHeadOfficeDetails(LIMS.User.LabId);
            model.headOffice = new HeadOfficeModel();
            if (HODetails != null)
            {
                model.headOffice.HOAddress = HODetails.HOAddress;
                model.headOffice.HOPhone = HODetails.HOPhone;
                model.headOffice.HOMobileFax = HODetails.HOMobileFax;
                model.headOffice.HOEmail = HODetails.HOEmail;
                model.headOffice.ManagerName = HODetails.ManagerName;
                model.headOffice.ManagerPhone = HODetails.ManagerPhone;
            }
            return View(model);
        }
        public PartialViewResult _QuotationLog(int EnquiryId)
        {
            List<QuotationLogModel> model = new List<QuotationLogModel>();
            var QuotationLogs = BALFactory.quotationBAL.GetQuotationLog(EnquiryId);
            foreach (var q in QuotationLogs)
            {
                model.Add(new QuotationLogModel()
                {
                    QuotationLogId = q.QuotationLogId,
                    EnquiryId = q.EnquiryId,
                    QuotedAmount = q.QuotedAmount,
                    IsRevised = q.IsActive,
                    RevisedOn = q.RevisedOn,
                    RevisedDate = Convert.ToDateTime(q.RevisedOn).ToString("dd/MM/yyyy"),
                    StatusUpdatedOn = q.StatusUpdatedOn,
                    Remarks = q.Remarks,
                    IsActive = q.IsActive,
                    CustomerName = q.CustomerName,
                    QuotationNo = q.QuotationNo,
                    MailedOn = q.MailedOn,
                });
            }
            return PartialView(model);
        }
        public JsonResult QuotationSent(int EnquiryId, string ClientEmail)
        {
            int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("QuotSENT");

            //var QuotsDetail = BALFactory.quotationBAL.GetQuotationDetails((long)EnquiryId);
            //if (QuotsDetail != null)
            //{
            //    BALFactory.enquiryBAL.GenerateQuotationNo(QuotsDetail.QuotationId, ClientEmail);

            //}

            BALFactory.enquiryBAL.UpdateEnquiryStatus(EnquiryId, (byte)iStatusId);
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult _QuotationActions(int EnquiryId, string tdLinkId)
        {
            ViewBag.tdLinkId = tdLinkId;
            QuotationLogModel model = new QuotationLogModel();
            var QuotsDetail = BALFactory.quotationBAL.GetQuotationDetails((long)EnquiryId);
            if (QuotsDetail != null)
            {
                model.EnquiryId = QuotsDetail.EnquiryId;
                model.QuotedAmount = QuotsDetail.QuotedAmount;
            }
            model.StatusUpdatedOn = DateTime.Now;
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _QuotationActions(QuotationLogModel model)
        {
            QuotationLogEntity quotLog = new QuotationLogEntity();
            quotLog.EnquiryId = model.EnquiryId;
            quotLog.QuotedAmount = model.QuotedAmount;
            quotLog.StatusUpdatedOn = model.StatusUpdatedOn;
            quotLog.Status = model.Status;
            quotLog.Remarks = model.Remarks;
            quotLog.EnteredBy = LIMS.User.UserMasterID;
            if (BALFactory.quotationBAL.AddQuotationLog(quotLog))
            {
                if (model.Status == "Approve")
                {
                    int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("QuotApprov");
                    BALFactory.enquiryBAL.UpdateEnquiryStatus(model.EnquiryId, (byte)iStatusId);
                }
                else if (model.Status == "Reject")
                {
                    if (model.Remarks == "" || model.Remarks == null)
                    {
                        return Json(new { status = "Fail" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("QuotReject");
                        BALFactory.enquiryBAL.UpdateEnquiryStatus(model.EnquiryId, (byte)iStatusId);
                    }
                }
                else
                {
                    if (model.Remarks == "" || model.Remarks == null)
                    {
                        return Json(new { status = "Fail" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("EnqCLOSED");
                        BALFactory.enquiryBAL.UpdateEnquiryStatus(model.EnquiryId, (byte)iStatusId);
                    }
                }
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

    }
}