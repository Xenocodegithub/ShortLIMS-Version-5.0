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
        invoice_list GetInvoiceDetails(int PaymentDetailsId);
        List<InvoiceEntity> GetInvoiceList();
        bool SaveDetails(InvoiceEntity invoiceEntity);
        List<InvoiceEntity> GetDailyinvoicelist();
        bool SavePaymentDetails(InvoiceEntity invoiceEntity);
       List<InvoiceEntity> GetPaymentMode();
        InvoiceEntity GetBillDetails(string InvoiceNumber, string WorkOrderNo);
        string DeleteInvoice(string WorkOrderNo);
        bool SaveRejectInvoice(InvoiceEntity invoiceEntity);
        List<InvoiceEntity> GetRejectinvoicelist();
    }
}
