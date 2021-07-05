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
using Microsoft.Reporting.WebForms;

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
            model.QuotationId = QuotationDetail.QuotationId;
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
        public ActionResult QuotationReport(string Format, int EnquiryId)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reports"), "QuotationReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return RedirectToAction("Quotation", new { EnquiryId = EnquiryId });
            }

            var QuotDetail = BALFactory.quotationBAL.GetQuotationPreview(EnquiryId);
            QuotDetail.RevisedDates = BALFactory.quotationBAL.GetQuotationRevisedDates(EnquiryId);
            Reports.QuotationData quotData = new Reports.QuotationData();
            quotData.QuotationDetails.AddQuotationDetailsRow(QuotDetail.EnquiryId, QuotDetail.LabName, QuotDetail.RefName, QuotDetail.CompanyName, QuotDetail.ContactPerson
                , QuotDetail.Designation, QuotDetail.ClientMobileNo, QuotDetail.ContactEmail, QuotDetail.PAN, QuotDetail.GSTNO, QuotDetail.SACNO, QuotDetail.BANKERS,
                QuotDetail.PFNO, QuotDetail.ESICNO, QuotDetail.PTNO, QuotDetail.TDSNO, QuotDetail.MSMEDA, QuotDetail.IECNO, QuotDetail.LABAddress, QuotDetail.LABPhone,
                QuotDetail.LABMobileFax, QuotDetail.LABEmail, QuotDetail.RevisedDates, QuotDetail.ContactMobile, QuotDetail.LABCity, QuotDetail.ClientFax);
            quotData.QuotationDetails.AcceptChanges();

            CoreFactory.costingList = BALFactory.costingBAL.GetCostingList(EnquiryId);
            int i = 1;
            foreach (var cost in CoreFactory.costingList)
            {
                string Particulars = /*cost.SampleTypeProductName + " / " + cost.SubGroupName + " / " + cost.MatrixName +*/ " \n " + BALFactory.quotationBAL.GetParamters(cost.EnquirySampleID);
                quotData.QuotationCosting.AddQuotationCostingRow(i, (Int64)cost.CostingId, cost.EnquirySampleID, (decimal)cost.UnitPrice, cost.SampleName,
                    cost.SampleTypeProductName, cost.SubGroupName, cost.MatrixName, Particulars);
                quotData.QuotationCosting.AcceptChanges();
                i++;
            }

            var TermsCond = BALFactory.costingBAL.GetTermsAndCondition(EnquiryId);
            foreach (var t in TermsCond)
            {
                if (t.IsSelected)
                {
                    quotData.QuotationTMC.AddQuotationTMCRow(t.TermAndCondtId, t.TermAndCondt, t.Remark);
                    quotData.QuotationTMC.AcceptChanges();
                }
            }

            var HODetails = BALFactory.quotationBAL.GetHeadOfficeDetails(LIMS.User.LabId);
            if (HODetails != null)
            {
                quotData.HeadOfficeDetail.AddHeadOfficeDetailRow(HODetails.HOAddress, HODetails.HOPhone, HODetails.HOMobileFax, HODetails.HOEmail,
                    HODetails.ManagerName, HODetails.ManagerPhone);
                quotData.HeadOfficeDetail.AcceptChanges();
            }

            ReportDataSource rd = new ReportDataSource("QuotationDetails", (System.Data.DataTable)quotData.QuotationDetails);
            lr.DataSources.Add(rd);

            ReportDataSource rd1 = new ReportDataSource("QuotationCosting", (System.Data.DataTable)quotData.QuotationCosting);
            lr.DataSources.Add(rd1);

            ReportDataSource rd2 = new ReportDataSource("QuotationTMC", (System.Data.DataTable)quotData.QuotationTMC);
            lr.DataSources.Add(rd2);

            ReportDataSource rd3 = new ReportDataSource("HeadOfficeDetail", (System.Data.DataTable)quotData.HeadOfficeDetail);
            lr.DataSources.Add(rd3);

            string reportType = Format;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + Format + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11.0in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>0.0in</MarginLeft>" +
            "  <MarginRight>0.0in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "  <EmbedFonts>None</EmbedFonts>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }

    }
}
