using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Invoice.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Enquiry;
using LIMS_DEMO.BAL.DropDown;
using LIMS_DEMO.BAL.WorkOrderCustomer;



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

        public PartialViewResult _CostingDetails(int? EnquirySampleID = 0, int? CostingId = 0)
        {
            //ViewBag.GSTRate = GSTRate;
            InvoiceModel model = new InvoiceModel();
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
                    //model.GSTCharges = (model.TotalAmount * GSTRate) / 100;
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
    }
}