using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Invoice.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Invoice;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Enquiry;
using LIMS_DEMO.BAL.DropDown;
using LIMS_DEMO.BAL.WorkOrderCustomer;
using Newtonsoft.Json;

namespace LIMS_DEMO.Areas.Invoice.Controllers
{
    public class InvoiceController : Controller
    {
        string strStatus = "";
        public InvoiceController()
        {
            BALFactory.enquiryBAL = new EnquiryBAL();
            BALFactory.costingBAL = new CostingBAL();
            BALFactory.workOrderBAL = new WorkOrderBAL();
            BALFactory.sampleParameterBAL = new SampleParameterBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
            BALFactory.quotationBAL = new QuotationBAL();
            BALFactory.workordercustomerBAL = new WorkOrderCustomerBAL();
            BALFactory.invoiceBAL = new BAL.Invoice.InvoiceBAL();
        }
        // GET: Invoice/Invoice
        public ActionResult Index()
        {
            List<InvoiceModel> model = new List<InvoiceModel>();
            var WorkOrderList = BALFactory.invoiceBAL.GetInvoiceList();
            foreach (var wo in WorkOrderList)
            {

                model.Add(new InvoiceModel()
                {
                    RegistrationName = wo.RegistrationName,
                    WorkOrderNo = wo.WorkOrderNo,
                    EnquirySampleID = wo.EnquirySampleID,
                    CostingId = wo.CostingId,
                    EnquiryId = wo.EnquiryId,
                    WorkOrderId = wo.WorkOrderId,
                    CurrentStatus = wo.CurrentStatus,
                    AssignToId = wo.AssignToId,// Doubt for BDM
                    Remarks = wo.Remarks,
                    WORecieveDate = wo.WORecieveDate,
                    WOUpload = wo.WOUpload,
                    FileName = wo.FileName,
                    IsIGST = wo.IsIGST,
                    UnitPrice = wo.UnitPrice
                });
            }

            return View(model);
        }

        public PartialViewResult _CostingDetails()
        {

            InvoiceModel model = new InvoiceModel();

            return PartialView(model);
        }
        public JsonResult GetInvoiceData(int EnquirySampleID, int CostingId)
        {
            try
            {

                CoreFactory.costingEntity = BALFactory.costingBAL.GetCosting((Int32)EnquirySampleID, (Int32)CostingId);
               
                return Json(CoreFactory.costingEntity, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public string SaveDetails(string model)
        {
            try
            {
                InvoiceEntity invoiceEntity = new InvoiceEntity();
                invoiceEntity = JsonConvert.DeserializeObject<InvoiceEntity>(model);
                bool status = BALFactory.invoiceBAL.SaveDetails(invoiceEntity);
                return "success";
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public ActionResult DailyInvoiceList()
        {
            List<InvoiceModel> model = new List<InvoiceModel>();
            var Dailyinvoicelist = BALFactory.invoiceBAL.GetDailyinvoicelist();
            foreach (var wo in Dailyinvoicelist)
            {

                model.Add(new InvoiceModel()
                {
                    RegistrationName = wo.RegistrationName,
                    WorkOrderNo = wo.WorkOrderNumber,
                    InvoiceNumber = wo.InvoiceNumber,
                    UnitPrice = wo.UnitPrice,
                    PaidAmount = wo.PaidAmount,
                    BalanceAmount = wo.Balance,
                    InvoiceDate = wo.InvoiceDate,
                    EnquirySampleID = wo.EnquirySampleID,
                    CostingId = wo.CostingId,
                    Count = wo.Count

                });

            }

            return View(model);

        }
        [HttpGet]
        public ActionResult Payment(int? PaymentDetailsId = 0, string InvoiceNumber = null, string WorkOrderNo = null)
        {
            InvoiceModel model = new InvoiceModel();
            if (PaymentDetailsId == 0)
            {
                try
                {

                    CoreFactory.invoiceEntity = BALFactory.invoiceBAL.GetBillDetails(InvoiceNumber, WorkOrderNo);
                    if (CoreFactory.invoiceEntity != null)
                    {
                        model.RegistrationName = CoreFactory.invoiceEntity.RegistrationName;
                        model.InvoiceNumber = CoreFactory.invoiceEntity.InvoiceNumber;
                        model.WorkOrderNo = CoreFactory.invoiceEntity.WorkOrderNumber;
                        model.TotalAmount = CoreFactory.invoiceEntity.UnitPrice;
                        model.PaidAmount = CoreFactory.invoiceEntity.PaidAmount;
                        model.Balance = CoreFactory.invoiceEntity.Balance;
                    }
                }
                catch (Exception ex)
                {

                    return null;
                }
            }
            ViewBag.paymentMode = BALFactory.dropdownsBAL.GetPaymentMode();
            return View(model);
        }



        [HttpPost]
        public string Payment(InvoiceModel model)
        {
            InvoiceEntity invoiceEntity = new InvoiceEntity();
            if(model.BankName==null)
            {
                CoreFactory.invoiceEntity.BankName = "-";
            }
            else
            {
                CoreFactory.invoiceEntity.BankName = model.BankName;
            }
            CoreFactory.invoiceEntity.RegistrationName = model.RegistrationName;
            CoreFactory.invoiceEntity.InvoiceNumber = model.InvoiceNumber;
            CoreFactory.invoiceEntity.TotalAmount = model.TotalAmount;
            CoreFactory.invoiceEntity.WorkOrderNo = model.WorkOrderNo;
            CoreFactory.invoiceEntity.PaidAmount = model.ActualAmount;
            CoreFactory.invoiceEntity.Balance = model.Balance;
            CoreFactory.invoiceEntity.PaymentModeMasterId = model.PaymentModeMasterId;
            // CoreFactory.invoiceEntity.BankName = model.BankName;
            CoreFactory.invoiceEntity.DD_ChequeDate = model.DD_ChequeDate;
            CoreFactory.invoiceEntity.DD_ChequeNo_Neft = model.DD_ChequeNo_Neft;
            CoreFactory.invoiceEntity.PayDate = model.PaymentDate;
            CoreFactory.invoiceEntity.TotalAmount = model.TotalAmount;
            bool status = BALFactory.invoiceBAL.SavePaymentDetails(CoreFactory.invoiceEntity);
            InvoiceModel model1 = new InvoiceModel();
            //invoice_list PYInfo = BALFactory.invoiceBAL.GetInvoiceDetails(PaymentDetailsId);
            //int i = 1;
            //if (PYInfo != null)
            //{
            //    TempData.Remove("Payment_DetailsList");
            //    foreach (var grid1 in PYInfo.invoiceDetailsList)
            //    {
            //        try
            //        {
            //            model1.GridModel.Add(new InvoiceModel()
            //            {
            //                SrNo = i,
            //                PaymentDetailsId = 5,
            //                RecieptNo = grid1.RecieptNo,
            //                PaidAmount = grid1.PaidAmount,
            //                Date = grid1.Date,
            //                PaymentMode = grid1.PaymentMode,
            //                BankName = grid1.BankName,
            //                DD_ChequeNo_Neft = grid1.DD_ChequeNo_Neft,
            //                DD_ChequeDate = grid1.DD_ChequeDate,
            //                Print = grid1.Print
            //            });
            //            i++;
            //        }
            //        catch (Exception ex)
            //        {
            //        }
            //    }
            //    TempData["Payment_DetailsList"] = model1.GridModel;
            //}
            if (status == true)
            {
                return "success";
            }
            else
            {
                return "fail";
            }

        }


        //if (model.PaymentDetailsId == 0)
        //{
        //    bool strStatus = BALFactory.invoiceBAL.SavePaymentDetails(CoreFactory.invoiceEntity);
        //    if (strStatus == true)
        //    {
        //        return Json(new { Status = strStatus, message = "Payment has been registered successfully." }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { Status = strStatus, message = "Payment has not been registered successfully." }, JsonRequestBehavior.AllowGet);
        //    }
        //}



        //Use for update
        //else
        //{
        //    strStatus = BALFactory.parameterMasterBAL.UpdateParameter(CoreFactory.parameterMasterEntity);
        //    //return Json(new { Status = strStatus, message = "TestMethod has been updated successfully." }, JsonRequestBehavior.AllowGet);
        //}
        //return RedirectToAction("_PaymentList");
        ////return Json(new { Status = strStatus, message = "Parameter has been registered successfully." }, JsonRequestBehavior.AllowGet);


        public PartialViewResult _PaymentList(string WorkorderNumber)
        {
            //WorkorderNumber = "w-90051";
            List<InvoiceModel> model = new List<InvoiceModel>();
            CoreFactory.invoiceList1 = BALFactory.invoiceBAL.GetPaymentList(WorkorderNumber);
            //CoreFactory.costingList = BALFactory.invoiceBAL.GetymentList();
            int i = 1;
            foreach (var inv in CoreFactory.invoiceList1)
            {
                model.Add(new InvoiceModel()
                {
                    SrNo = i,
                    PaymentDetailsId = inv.PaymentDetailsId,
                    EnquirySampleID = inv.EnquirySampleID,
                    RecieptNo = inv.RecieptNo,
                    PaidAmount = inv.PaidAmount,
                    Date = inv.Date,
                    PaymentModeName = inv.PaymentModeName,
                    PaymentDate = inv.Date1,
                    BankName = inv.BankName,
                    //DD_ChequeDate = inv.DD_ChequeDate,
                    DD_ChequeNo_Neft = inv.DD_ChequeNo_Neft
                });
                i++;
            }
            return PartialView(model);

        }

        //public PartialViewResult _PaymentReceipt(int PaymentDetailsId, string WOID)
        //{
        //    List<InvoiceModel> model = new List<InvoiceModel>();
        //    var paymentreciept = BALFactory.invoiceBAL.GetPaymentReceipt(PaymentDetailsId);
            
        //    return PartialView("model");
        //}
        public ActionResult ReceiptList()
        {
            List<InvoiceModel> model = new List<InvoiceModel>();
            var WorkOrderList = BALFactory.invoiceBAL.GetPaymentReceipt();
            foreach (var rl in WorkOrderList)
            {
                model.Add(new InvoiceModel()
                {
                    RegistrationName = rl.RegistrationName,
                    EnquirySampleID = rl.EnquirySampleID,
                    InvoiceNumber = rl.InvoiceNumber,
                    RecieptNo = rl.RecieptNo,
                    PaymentDate = rl.PayDate,
                    TotalAmount = rl.TotalAmount,
                    PaidAmount = rl.PaidAmount
                });
            }
            return View(model);
        }
        public JsonResult DeleteInvoice(string WorkOrderNo)
        {
            var rejectList = (List<InvoiceModel>)TempData.Peek("DailyInvoiceList");
            var reject = rejectList.Where(c => c.WorkOrderNo == WorkOrderNo).FirstOrDefault();
            if (reject.WorkOrderId != 0)
            {
                BALFactory.invoiceBAL.DeleteInvoice(reject.WorkOrderNo);
            }
            rejectList.Remove(reject);
            int i = 1;
            foreach (var item in rejectList)
            {
                item.WorkOrderId = i;
                i++;
            }
            TempData["DailyInvoiceList"] = rejectList;
            return Json(new { status = "success", message = "deleted" }, JsonRequestBehavior.AllowGet);
        }
        public string RejectInvoice(string model)
        {
            try
            {
                InvoiceEntity invoiceEntity = new InvoiceEntity();
                invoiceEntity = JsonConvert.DeserializeObject<InvoiceEntity>(model);
                bool status = BALFactory.invoiceBAL.SaveRejectInvoice(invoiceEntity);
                return "success";
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public ActionResult RejectInvoiceList()
        {
            List<InvoiceModel> model = new List<InvoiceModel>();
            var Rejectinvoicelist = BALFactory.invoiceBAL.GetRejectInvoiceList();
            foreach (var wo in Rejectinvoicelist)
            {

                model.Add(new InvoiceModel()
                {
                    RegistrationName = wo.RegistrationName,
                    WorkOrderNo = wo.WorkOrderNumber,
                    InvoiceNumber = wo.InvoiceNumber,
                    UnitPrice = wo.UnitPrice,
                    PaidAmount = wo.PaidAmount,
                    BalanceAmount = wo.Balance,
                    InvoiceDate = wo.InvoiceDate,
                    EnquirySampleID = wo.EnquirySampleID,
                    CostingId = wo.CostingId
                });
            }
            return View(model);
        }
    }
}

