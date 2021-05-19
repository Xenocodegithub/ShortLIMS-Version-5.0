using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Invoice;


namespace LIMS_DEMO.Core.Repository
{
   public interface IInvoice : IDisposable
    {
        IList<InvoiceEntity> GetPaymentList(string PaymentDetailsId);
        invoice_list GetInvoiceDetails(int PaymentDetailsId);
        List<InvoiceEntity> GetInvoiceList();
        bool SaveDetails(InvoiceEntity invoiceEntity);
        List<InvoiceEntity> GetDailyinvoicelist();
        bool SavePaymentDetails(InvoiceEntity invoiceEntity);
       List<InvoiceEntity> GetPaymentMode();
        InvoiceEntity GetBillDetails(string InvoiceNumber, string WorkOrderNo);
        List<InvoiceEntity> GetPaymentReceipt();
        string DeleteInvoice(string WorkOrderNo);
        bool SaveRejectInvoice(InvoiceEntity invoiceEntity);
        List<InvoiceEntity> GetRejectinvoicelist();
    }
}
