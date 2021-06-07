using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Invoice;
using LIMS_DEMO.DAL.Invoice;
using LIMS_DEMO.Core.User;

namespace LIMS_DEMO.BAL.Invoice
{
    public class InvoiceBAL
    {
        public InvoiceBAL()
        {
            CoreFactory.invoice = new InvoiceDAL();
        }
        public invoice_list GetInvoiceDetails(int PaymentDetailsId)
        {
            return CoreFactory.invoice.GetInvoiceDetails(PaymentDetailsId);
        }
        public InvoiceEntity GetBillDetails(string InvoiceNumber,string  WorkOrderNo)
        {
            return CoreFactory.invoice.GetBillDetails(InvoiceNumber, WorkOrderNo);
        }
        public List<InvoiceEntity> GetInvoiceList()
        {
            return CoreFactory.invoice.GetInvoiceList();
        }
        public bool SavePaymentDetails(InvoiceEntity invoiceEntity)
        {
            return CoreFactory.invoice.SavePaymentDetails(invoiceEntity);
        }
        public bool SaveDetails(InvoiceEntity invoiceEntity)

        {
            return CoreFactory.invoice.SaveDetails(invoiceEntity);
        }
        public List<InvoiceEntity> GetDailyinvoicelist()
        {
            return CoreFactory.invoice.GetDailyinvoicelist();
        }
        public List<InvoiceEntity> GetPaymentMode()
        {

            return CoreFactory.invoice.GetPaymentMode();
        }
        public string DeleteInvoice(string WorkOrderNo)
        {
           
            return CoreFactory.invoice.DeleteInvoice(WorkOrderNo);
        }
        public bool SaveRejectInvoice(InvoiceEntity invoiceEntity)

        {
            return CoreFactory.invoice.SaveRejectInvoice(invoiceEntity);
        }
        public List<InvoiceEntity> GetRejectInvoiceList()
        {
            return CoreFactory.invoice.GetRejectinvoicelist();
        }
        
    }
}