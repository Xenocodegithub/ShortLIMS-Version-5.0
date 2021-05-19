using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.DropDowns
{
    public class InvoiceEntity
    {
        public string PaymentMode { get; set; }
        public long PaymentModeMasterId { get; set; }
        //public List<InvoiceEntity> invoiceList { get; set; }
    }
}