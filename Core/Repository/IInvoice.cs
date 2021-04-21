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

        List<InvoiceEntity> GetInvoiceList();
    }
}
