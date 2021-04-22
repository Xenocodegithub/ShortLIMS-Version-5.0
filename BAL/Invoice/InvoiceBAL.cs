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
        public List<InvoiceEntity> GetInvoiceList()
        {
            return CoreFactory.invoice.GetInvoiceList();
        }
    }
}