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

namespace LIMS_DEMO.Areas.Enquiry.Controllers
{
    [RouteArea("Enquiry")]
    public class EnquiryController : Controller
    {
        string strStatus = "";
        public EnquiryController()
        {
            BALFactory.enquiryBAL = new EnquiryBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
        }

        //GET: Enquiry/Enquiry
        public ActionResult EnquiryList()
        {
            CoreFactory.enquiryList = BALFactory.enquiryBAL.GetEnquiries();
            List<EnquiryModel> modelList = new List<EnquiryModel>();

            foreach (var Item in CoreFactory.enquiryList)
            {
                modelList.Add(new EnquiryModel()
                {
                    EnquiryId = Item.EnquiryId,
                    CustomerMasterId = Item.CustomerMasterId,
                    CustomerName = Item.CustomerName,
                    CommunicationMode = Item.CommunicationMode,
                    EnquiryOn = Item.EnquiryOn,
                    EnquiryDate = Convert.ToDateTime(Item.EnquiryOn).ToString("dd/MM/yyyy"),
                    Remarks = Item.Remarks,
                    CurrentStatus = Item.CurrentStatus
                });
            }
            return View(modelList);
        }
        public ActionResult AddEnquiry(int? EnquiryId = 0)
        {
            EnquiryModel model = new EnquiryModel();
            if (EnquiryId != 0)
            {
                CoreFactory.enquiryEntity = BALFactory.enquiryBAL.GetDetails((Int32)EnquiryId);
                if (CoreFactory.userEntity != null)
                {
                    model.EnquiryId = CoreFactory.enquiryEntity.EnquiryId;
                    model.CustomerMasterId = CoreFactory.enquiryEntity.CustomerMasterId;
                    model.CustomerName = CoreFactory.enquiryEntity.CustomerName;
                    model.CommunicationMode = CoreFactory.enquiryEntity.CommunicationMode;
                    model.EnquiryOn = CoreFactory.enquiryEntity.EnquiryOn;
                    model.Remarks = CoreFactory.enquiryEntity.Remarks;
                }
            }
            ViewBag.Customers = BALFactory.dropdownsBAL.GetCustomers();
            ViewBag.CommunicationMode = BALFactory.dropdownsBAL.GetCommunicationMode();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddEnquiry(EnquiryModel model)
        {
            CoreFactory.enquiryEntity = new EnquiryEntity();
            CoreFactory.enquiryEntity.EnquiryId = model.EnquiryId;
            CoreFactory.enquiryEntity.CustomerName = model.CustomerName;
            CoreFactory.enquiryEntity.CommunicationMode = model.CommunicationMode;
            CoreFactory.enquiryEntity.ModeOfCommunicationId = model.ModeOfCommunicationId;
            CoreFactory.enquiryEntity.CustomerMasterId = (Int32)model.CustomerMasterId;
            CoreFactory.enquiryEntity.EnquiryOn = Convert.ToDateTime(model.EnquiryOn);
            CoreFactory.enquiryEntity.Remarks = model.Remarks;
            CoreFactory.enquiryEntity.StatusId = (byte)BALFactory.dropdownsBAL.GetStatusIdByCode(Enum.GetName(typeof(ManageMessageId), ManageMessageId.EnqRAISED));
            CoreFactory.enquiryEntity.LabMasterId = LIMS.User.LabId;//doubt 
            CoreFactory.enquiryEntity.IsActive = true;
            CoreFactory.enquiryEntity.EnteredBy = LIMS.User.UserMasterID;
            if (model.EnquiryId == 0)
            {
                long EnquiryId = BALFactory.enquiryBAL.Add(CoreFactory.enquiryEntity);
                CoreFactory.enquiryEntity.EnquiryId = EnquiryId;
                return Json(new { EnquiryId = EnquiryId, message = "Enquiry created." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                strStatus = BALFactory.enquiryBAL.Update(CoreFactory.enquiryEntity);
                return Json(new { EnquiryId = model.EnquiryId, message = "Enquiry updated." }, JsonRequestBehavior.AllowGet);
            }


        }
        public PartialViewResult _CommunicationLog(int? EnquiryId = 0, string CustomerName = "", int EnquiryLogId = 0)
        {
            EnquiryLogModel model = new EnquiryLogModel();
            model.EnquiryId = (long)EnquiryId;
            model.CustomerName = CustomerName;
            ViewBag.CommunicationMode = BALFactory.dropdownsBAL.GetCommunicationMode();
            return PartialView(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _CommunicationLog(EnquiryLogModel model)
        {
            CoreFactory.enquiryLogEntity = new EnquiryLogEntity();
            CoreFactory.enquiryLogEntity.EnquiryLogId = model.EnquiryLogId;
            CoreFactory.enquiryLogEntity.EnquiryId = model.EnquiryId;
            CoreFactory.enquiryLogEntity.ModeOfCommunicationId = model.ModeOfCommunicationId;
            CoreFactory.enquiryLogEntity.CommunicationDate = Convert.ToDateTime(model.CommunicationDate);
            CoreFactory.enquiryLogEntity.Remarks = model.Remarks;
            CoreFactory.enquiryLogEntity.IsActive = true;
            CoreFactory.enquiryLogEntity.EnteredBy = LIMS.User.UserMasterID;
            CoreFactory.enquiryLogEntity.EnteredDate = System.DateTime.UtcNow;

            if (model.ModeOfCommunicationId == 0 || model.Remarks == "" || model.Remarks == null || model.CommunicationDate == null)
            {
                return Json(new { status = "Fail" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                long EnquiryLogId = BALFactory.enquiryBAL.AddLog(CoreFactory.enquiryLogEntity);

                return Json(new { EnquiryLogId = EnquiryLogId, status = "success", message = "Log Added" }, JsonRequestBehavior.AllowGet);
            }

        }
        public PartialViewResult _CommunicationLogList(int EnquiryId)
        {
            CoreFactory.enquiryLogList = BALFactory.enquiryBAL.GetEnquirLog((Int32)EnquiryId);
            List<EnquiryLogModel> modelList = new List<EnquiryLogModel>();
            if (CoreFactory.enquiryLogList.Count != 0)
            {
                int iSrNo = 1;
                foreach (var Item in CoreFactory.enquiryLogList)
                {
                    modelList.Add(new EnquiryLogModel()
                    {
                        SerialNo = iSrNo,
                        EnquiryLogId = Item.EnquiryLogId,
                        EnquiryId = Item.EnquiryId,
                        ModeOfCommunicationId = Item.ModeOfCommunicationId,
                        CommunicationMode = Item.CommunicationMode,
                        CommunicationDate = Item.CommunicationDate,
                        Date = Convert.ToDateTime(Item.CommunicationDate).ToString("dd/MM/yyyy"),
                        CustomerMasterId = Item.CustomerMasterId,
                        CustomerName = Item.CustomerName,
                        Remarks = Item.Remarks,
                    });
                    iSrNo++;
                }

            }
            return PartialView(modelList);
        }
        public JsonResult Delete(long EnquiryId = 0)
        {

            string strStatus = BALFactory.enquiryBAL.DeleteEnquiry(EnquiryId, false);
            return Json(new { status = strStatus, message = ManageMessageId.EnquiryDeleted }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(long EnquiryId)
        {
            EnquiryModel model = new EnquiryModel();
            CoreFactory.enquiryEntity = BALFactory.enquiryBAL.GetDetails((Int32)EnquiryId);
            model.EnquiryId = CoreFactory.enquiryEntity.EnquiryId;
            model.StatusId = CoreFactory.enquiryEntity.StatusId;
            string StatusCode = CoreFactory.enquiryEntity.StatusCode;

            if (StatusCode == Enum.GetName(typeof(ManageMessageId), ManageMessageId.EnqRAISED))//ContractPage
            {
                return RedirectToAction("AddContract", "Contract", new { area = "Enquiry", model.EnquiryId });
            }
            if (StatusCode == Enum.GetName(typeof(ManageMessageId), ManageMessageId.ContSUBMIT))//SampleAndParameterORCosting
            {
                CoreFactory.enquiryList = BALFactory.enquiryBAL.GetParameterDetails((Int32)EnquiryId);
                if (CoreFactory.enquiryList.Count() == 0)
                {
                    return Redirect("/Enquiry/SampleAndParameter/SampleAndParameter?EnquiryId=" + model.EnquiryId);
                }
                else
                {
                    return RedirectToAction("AddCosting", "Costing", new { area = "Enquiry", model.EnquiryId });
                }

            }
            if (StatusCode == Enum.GetName(typeof(ManageMessageId), ManageMessageId.QuotCreate))//Quotation
            {
                return RedirectToAction("Quotation", "Quotation", new { area = "Enquiry", model.EnquiryId });
            }
            if (StatusCode == Enum.GetName(typeof(ManageMessageId), ManageMessageId.QuotApprov))//AddWorkOrder
            {
                return RedirectToAction("AddWorkOrder", "WorkOrder", new { area = "Enquiry", model.EnquiryId });
            }
            if (StatusCode == Enum.GetName(typeof(ManageMessageId), ManageMessageId.RevisingQt))//Quotation
            {
                return RedirectToAction("Quotation", "Quotation", new { area = "Enquiry", model.EnquiryId });
            }
            if (StatusCode == Enum.GetName(typeof(ManageMessageId), ManageMessageId.WOGenerate))//WorkOrderList
            {
                return RedirectToAction("WorkOrderList", "WorkOrder", new { area = "Enquiry", model.EnquiryId });
            }

            return RedirectToAction("EnquiryList");
        }
        public enum ManageMessageId
        {
            EnqRAISED,
            EnquiryCreated,
            EnquiryUpdated,
            EnquiryDeleted,
            EnquiryNotFound,
            success,
            Error,
            ContSUBMIT,
            ParamAdded,
            QuotCreate,
            QuotApprov,
            WOGenerate,
            RevisingQt,
        }
    }
}