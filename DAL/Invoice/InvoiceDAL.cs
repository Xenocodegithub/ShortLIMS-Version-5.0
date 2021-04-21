using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.Invoice;
using LIMS_DEMO.Core.Repository;

namespace LIMS_DEMO.DAL.Invoice
{
    public class InvoiceDAL : IInvoice
    {
        readonly LIMSEntities _dbContext;

        public InvoiceDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public List<InvoiceEntity> GetInvoiceList()
        {
            return (from w in _dbContext.WorkOrders
                    join q in _dbContext.Quotations on w.QuotationId equals q.QuotationId
                    join e in _dbContext.EnquiryMasters on q.EnquiryId equals e.EnquiryId
                    join c in _dbContext.CustomerMasters on e.CustomerMasterId equals c.CustomerMasterId
                    join s in _dbContext.StatusMasters on e.StatusId equals s.StatusId
                    into workOrder
                    from wo in workOrder.DefaultIfEmpty()
                     select new InvoiceEntity()
                    {
                        RegistrationName = c.RegistrationName,
                        WorkOrderNo = w.WorkOrderNo,
                        EnquiryId = e.EnquiryId,
                        WorkOrderId = w.WorkOrderId,
                        CurrentStatus = wo.StatusName,
                        AssignToId = w.AssignedToId,
                        Remarks = w.Remark,
                        WORecieveDate = w.WORecieveDate,
                        WOUpload = w.WOUpload,
                        FileName = w.FileName,
                        IsIGST = w.IsIGST
                    }).OrderByDescending(e => e.EnquiryId).ToList();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            { if (disposing) { _dbContext.Dispose(); } }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}