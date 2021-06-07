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
        public invoice_list GetInvoiceDetails(int PaymentDetailsId)
        {
    
            var invoiceDetail =(from fww in _dbContext.PaymentDetails
                          select new InvoiceEntity()
                          {
                              PaymentDetailsId = fww.PaymentDetailsId,
                              RecieptNo = fww.RecieptNo,
                              PaidAmount = fww.PaidAmount,
                             // Date = fww.Date,
                              PaymentMode = fww.PaymentMode.ToString(),
                              BankName = fww.BankName,
                              DD_ChequeNo_Neft = fww.DD_ChequeNo_Neft,
                              DD_ChequeDate = fww.DD_ChequeDate
                             
                          }
                   ).FirstOrDefault();

            invoice_list invoice_List = new invoice_list();
            invoice_List.invoiceDetails = invoiceDetail;
            var Inv_List = (from fww in _dbContext.PaymentDetails

                            where fww.PaymentDetailsId == PaymentDetailsId
                            select new InvoiceEntity
                            {
                                RecieptNo = fww.RecieptNo,
                                PaidAmount = fww.PaidAmount,
                               PaymentDetailsId = fww.PaymentDetailsId,
                                PaymentMode = fww.PaymentMode.ToString(),
                                BankName = fww.BankName,
                                DD_ChequeNo_Neft = fww.DD_ChequeNo_Neft,
                                DD_ChequeDate = fww.DD_ChequeDate
                                                           
                            }).ToList();
            invoice_List.invoiceDetailsList = Inv_List;
            return invoice_List;

        }
        public InvoiceEntity GetBillDetails(string InvoiceNumber, string WorkOrderNo)
        {

            return (from fww in _dbContext.TBL_DailyInvoice
                                     select new InvoiceEntity()
                                     {
                                         RegistrationName = fww.CustomerName,
                                         WorkOrderNumber = fww.WorkOrderNo,
                                         InvoiceNumber = fww.InvoiceNo,
                                         UnitPrice = fww.NetAmount,
                                         PaidAmount = fww.Paidamount,
                                         BalanceAmount = fww.Balanceamount
                                     }
                  ).FirstOrDefault();
       }
       
        public List<InvoiceEntity> GetInvoiceList()
        {
            return (from w in _dbContext.WorkOrders
                    join q in _dbContext.Quotations on w.QuotationId equals q.QuotationId
                    join e in _dbContext.EnquiryMasters on q.EnquiryId equals e.EnquiryId
                    join ed in _dbContext.EnquiryDetails on e.EnquiryId equals ed.EnquiryId
                    join esd in _dbContext.EnquirySampleDetails on ed.EnquiryDetailId equals esd.EnquiryDetailId
                    join ct in _dbContext.Costings on esd.EnquirySampleID equals ct.EnquirySampleID
                    join c in _dbContext.CustomerMasters on e.CustomerMasterId equals c.CustomerMasterId
                    join s in _dbContext.StatusMasters on e.StatusId equals s.StatusId
                    into workOrder
                    from wo in workOrder.DefaultIfEmpty()
                     select new InvoiceEntity()
                    {
                        RegistrationName = c.RegistrationName,
                        EnquirySampleID = (Int32)ct.EnquirySampleID,
                        CostingId = (Int32)ct.CostingId,
                        UnitPrice = ct.UnitPrice,
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
       

        public bool SaveDetails(InvoiceEntity invoiceEntity)
        {
            try
            {
                _dbContext.TBL_DailyInvoice.Add(new TBL_DailyInvoice()
                {
                    CustomerName = invoiceEntity.RegistrationName,
                    WorkOrderNo = invoiceEntity.WorkOrderNumber,
                    InvoiceNo = invoiceEntity.InvoiceNumber,
                    Totalcharge = invoiceEntity.TotalCharges,
                    SampleCollectioncharge = invoiceEntity.CollectionCharges,
                    TestingCharge = invoiceEntity.TestingCharges,
                    TransportationCharge = invoiceEntity.TransportCharges,
                    NetAmount = invoiceEntity.UnitPrice,
                    Discountamt = invoiceEntity.Discount,
                    GSTAmt = invoiceEntity.GST,
                    Paidamount = invoiceEntity.UnitPrice,
                    Balanceamount = 0,
                    InvoiceDate = DateTime.Now.ToLocalTime(),
                    EnquirySampleID=invoiceEntity.EnquirySampleID,
                    CostingId=invoiceEntity.CostingId,
                    Count = 0
                }) ;
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool SaveRejectInvoice(InvoiceEntity invoiceEntity)
        {
            try
            {
                _dbContext.TBL_REJECTINVOICEDETAILS.Add(new TBL_REJECTINVOICEDETAILS()
                {
                    CustomerName = invoiceEntity.RegistrationName,
                    WorkOrderNo = invoiceEntity.WorkOrderNumber,
                    InvoiceNo = invoiceEntity.InvoiceNumber,
                    Totalcharge = invoiceEntity.TotalCharges,
                    SampleCollectioncharge = invoiceEntity.CollectionCharges,
                    TestingCharge = invoiceEntity.TestingCharges,
                    TransportationCharge = invoiceEntity.TransportCharges,
                    NetAmount = invoiceEntity.UnitPrice,
                    Discountamt = invoiceEntity.Discount,
                    GSTAmt = invoiceEntity.GST,
                    Paidamount = invoiceEntity.UnitPrice,
                    Balanceamount = 0,
                    InvoiceDate = DateTime.Now.ToLocalTime(),
                    EnquirySampleID = invoiceEntity.EnquirySampleID,
                    CostingId = invoiceEntity.CostingId
                    
                });
                _dbContext.SaveChanges();
                if (invoiceEntity.WorkOrderNumber != null)
                {
                    var reject = (from DI in _dbContext.TBL_DailyInvoice
                                  where DI.WorkOrderNo == invoiceEntity.WorkOrderNumber
                                  select new InvoiceEntity() 
                                  {
                                      ID=DI.ID,
                                      RejectStatus=DI.RejectStatus
                                  }).FirstOrDefault();

                    //  var reject1= _dbContext.TBL_DailyInvoice.Find(reject.ID);
                    //reject1.RejectStatus = "Rejected";
                    //_dbContext.SaveChanges();
                    // we delete the record from db when invoice is rejected
                    var  rejectid = _dbContext.TBL_DailyInvoice.SingleOrDefault(x => x.ID == reject.ID);
                    if (rejectid != null)
                    {
                        _dbContext.TBL_DailyInvoice.Remove(rejectid);
                        _dbContext.SaveChanges();
                    }
                }
               return true ;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
        public bool SavePaymentDetails(InvoiceEntity invoiceEntity)
        {
            try
            {
                _dbContext.PaymentDetails.Add(new PaymentDetail()
                {
                  RegistrationName=invoiceEntity.RegistrationName,
                  WorkOrderNo=invoiceEntity.WorkOrderNo,
                  InvoiceNumber=invoiceEntity.InvoiceNumber,
                  TotalAmount=invoiceEntity.TotalCharges,
                  PaidAmount=invoiceEntity.PaidAmount,
                  PaymentMode=Convert.ToInt32(invoiceEntity.PaymentMode),
                  BankName=invoiceEntity.BankName,
                  DD_ChequeNo_Neft=invoiceEntity.DD_ChequeNo_Neft,
                  DD_ChequeDate=invoiceEntity.DD_ChequeDate,
                  BranchName=invoiceEntity.BranchName,
                  PayDate=DateTime.Now.ToLocalTime(),
                  EnteredBy=0,
                  EnteredDate=DateTime.Now.ToLocalTime()
                });
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<InvoiceEntity> GetDailyinvoicelist()
        {
            try
            {
                return (from e in _dbContext.TBL_DailyInvoice
                        where e.RejectStatus == null
                        select new InvoiceEntity()
                        {
                           RegistrationName=e.CustomerName,
                           WorkOrderNumber=e.WorkOrderNo,
                           InvoiceNumber=e.InvoiceNo,
                           UnitPrice=e.NetAmount,
                           PaidAmount=e.Paidamount,
                           BalanceAmount=e.Balanceamount,
                           InvoiceDate=(DateTime)e.InvoiceDate,
                           Count=(int)e.Count,
                           EnquirySampleID=(int)e.EnquirySampleID,
                           CostingId=(int)e.CostingId
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<InvoiceEntity> GetRejectinvoicelist()
        {
            try
            {
                return (from e in _dbContext.TBL_REJECTINVOICEDETAILS

                        select new InvoiceEntity()
                        {
                            RegistrationName = e.CustomerName,
                            WorkOrderNumber = e.WorkOrderNo,
                            InvoiceNumber = e.InvoiceNo,
                            UnitPrice = e.NetAmount,
                            PaidAmount = e.Paidamount,
                            BalanceAmount = e.Balanceamount,
                            InvoiceDate = (DateTime)e.InvoiceDate,
                            EnquirySampleID = (int)e.EnquirySampleID,
                            CostingId = (int)e.CostingId
                        }).Distinct().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

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

        public List<InvoiceEntity> GetPaymentMode()
        {
            throw new NotImplementedException();
        }
        public string DeleteInvoice(string WorkOrderNo)
        {
           
            try
            {
                var workOrderId = _dbContext.TBL_DailyInvoice.SingleOrDefault(x => x.WorkOrderNo == WorkOrderNo); //returns a single item.

                if (workOrderId != null)
                {
                    _dbContext.TBL_DailyInvoice.Remove(workOrderId);
                    _dbContext.SaveChanges();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
            //try
            //{
            //    var reject = _dbContext.TBL_DailyInvoice.Find(WorkOrderId);

            //   _dbContext.TBL_DailyInvoice.Remove();
            //   // _dbContext.remove();
            //    return "success";
            //}
            //catch (Exception ex)
            //{
            //    return ex.InnerException.Message;
            //}

        }
    }
}