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
                    BalanceAmount = wo.BalanceAmount,
                    InvoiceDate = wo.InvoiceDate,
                    EnquirySampleID=wo.EnquirySampleID,
                    CostingId=wo.CostingId,
                    Count = wo.Count

                });

            }

            return View(model);

        }
        [HttpGet]
        public ActionResult Payment(int? PaymentDetailsId = 0,string InvoiceNumber = null,string WorkOrderNo = null)
        {
            InvoiceModel model = new InvoiceModel();
            if (PaymentDetailsId == 0)
            {
                try
                {

                    CoreFactory.invoiceEntity = BALFactory.invoiceBAL.GetBillDetails(InvoiceNumber, WorkOrderNo);
                    if(CoreFactory.invoiceEntity != null)
                    {
                        model.RegistrationName = CoreFactory.invoiceEntity.RegistrationName;
                        model.InvoiceNumber = CoreFactory.invoiceEntity.InvoiceNumber;
                        model.WorkOrderNo = CoreFactory.invoiceEntity.WorkOrderNumber;
                        model.TotalAmount = CoreFactory.invoiceEntity.UnitPrice;
                        model.PaidAmount = CoreFactory.invoiceEntity.PaidAmount;
                        model.Balance = CoreFactory.invoiceEntity.BalanceAmount;
                    }
                }
                catch (Exception ex)
                {

                    return null;
                }
            }
            
            invoice_list PYInfo = BALFactory.invoiceBAL.GetInvoiceDetails((Int32)PaymentDetailsId);
            int i = 1;
            if (PYInfo != null)
            {
                TempData.Remove("Payment_DetailsList");
                foreach (var grid1 in PYInfo.invoiceDetailsList)
                {
                    try
                    {
                        model.GridModel.Add(new InvoiceModel()
                        {
                            SrNo = i,
                            RecieptNo = grid1.RecieptNo,
                            PaidAmount = grid1.PaidAmount,
                            Date = grid1.Date,
                            PaymentMode = grid1.PaymentMode,
                            BankName = grid1.BankName,
                            DD_ChequeNo_Neft = grid1.DD_ChequeNo_Neft,
                            DD_ChequeDate = grid1.DD_ChequeDate,
                            Print = grid1.Print
                        });
                        i++;
                    }
                    catch (Exception ex)
                    {
                    }
                }
                TempData["Payment_DetailsList"] = model.GridModel;
            }
            ViewBag.paymentMode = BALFactory.dropdownsBAL.GetPaymentMode();
            return View(model);
        }



        [HttpPost]
        public string Payment(string model, Int32 PaymentDetailsId)
        {
             InvoiceEntity  invoiceEntity = new InvoiceEntity();
            InvoiceEntity invoiceEntity1 = new InvoiceEntity();
            invoiceEntity = JsonConvert.DeserializeObject<InvoiceEntity>(model);
            invoiceEntity1.RegistrationName = invoiceEntity.RegistrationName;
            invoiceEntity1.WorkOrderNo = invoiceEntity.WorkOrderNo;
            invoiceEntity1.InvoiceNumber = invoiceEntity.InvoiceNumber;
            invoiceEntity1.TotalCharges = invoiceEntity.TotalCharges;
            bool status = BALFactory.invoiceBAL.SavePaymentDetails(invoiceEntity);
            //InvoiceModel model1 = new InvoiceModel();
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


        public PartialViewResult _PaymentList(int PaymentDetailsId = 0)
        {
            List<InvoiceModel> model = new List<InvoiceModel>();
            invoice_list PYInfo = BALFactory.invoiceBAL.GetInvoiceDetails(PaymentDetailsId);
            //CoreFactory.invoiceList = BALFactory.invoiceBAL.GetInvoiceDetails((Int32)PaymentDetailsId);
            //CoreFactory.costingList = BALFactory.costingBAL.GetSampleTypeCostingList(EnquiryId);
            int i = 1;
            foreach (var inv in PYInfo.invoiceDetailsList)
            {
                model.Add(new InvoiceModel()
                {
                    SrNo = i,
                    //PaymentDetailsId = inv.PaymentDetailsId,
                    RecieptNo = inv.RecieptNo,
                    PaidAmount = inv.PaidAmount,
                    Date = inv.Date,
                    PaymentMode = inv.PaymentMode,
                    PaymentDate = inv.Date1,
                    BankName = inv.BankName,
                    DD_ChequeDate = inv.DD_ChequeDate,
                    DD_ChequeNo_Neft = inv.DD_ChequeNo_Neft
                  
                });
                i++;
            }
            
            return PartialView(model);
            //if (TempData["Payment_DetailsList"] != null)
            //{
            //    model.GridModel = (List<InvoiceModel>)TempData.Peek("Payment_DetailsList");
            //    TempData.Keep("Payment_DetailsList");
            //}

            //return PartialView(model);
            //InvoiceModel model = new InvoiceModel();
            //if (TempData["Payment_DetailsList"] != null)
            //{
            //    var PaymentList = (List<InvoiceModel>)TempData.Peek("Payment_DetailsList");
            //    TempData.Keep("PaymentList");
            //    model = PaymentList.Where(c => c.SrNo == (Int32)Id).FirstOrDefault();
            //}

            //return PartialView(model);
        }
        public JsonResult DeleteInvoice(string WorkOrderNo )
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
                    BalanceAmount = wo.BalanceAmount,
                    InvoiceDate = wo.InvoiceDate,
                    EnquirySampleID = wo.EnquirySampleID,
                    CostingId = wo.CostingId
                    

                });

            }

            return View(model);

        }

        }

    }


