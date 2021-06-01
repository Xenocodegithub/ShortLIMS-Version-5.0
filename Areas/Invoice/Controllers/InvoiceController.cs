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
                   IsIGST = wo.IsIGST
                    
                });
            }

            return View(model);
        }

        public PartialViewResult _CostingDetails()
        {
            
            InvoiceModel model = new InvoiceModel();
            
            return PartialView(model);
        }
        public JsonResult GetInvoiceData(int EnquirySampleID,int CostingId)
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
                InvoiceEntity psd = JsonConvert.DeserializeObject<InvoiceEntity>(model);
                bool status = BALFactory.invoiceBAL.SaveDetails(psd);
                return "success ";
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}