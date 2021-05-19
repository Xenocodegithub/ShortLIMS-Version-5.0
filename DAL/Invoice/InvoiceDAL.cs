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
        public IList<InvoiceEntity> GetPaymentList(string EnquirySampleId)
        {
            var a= (from pd in _dbContext.PaymentDetails
                    join pm in _dbContext.PaymentModeMasters on pd.PaymentMode equals pm.PaymentModeMasterId
                    where pd.WorkOrderNo == EnquirySampleId
                    select new InvoiceEntity()
                    {
                        PaymentDetailsId = pd.PaymentDetailsId,
                        //EnquirySampleID = (Int32)di.EnquirySampleID,
                        RecieptNo = pd.RecieptNo,
                        PaidAmount = pd.PaidAmount,
                        Date = pd.PayDate,
                        PaymentModeName= pm.PaymentMode,
                        BankName = pd.BankName,
                        DD_ChequeNo_Neft = pd.DD_ChequeNo_Neft,
                        //DD_ChequeDate = pd.DD_ChequeDate
                    }).ToList();
            return a;
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
                              PaymentMode = fww.PaymentMode,
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
                                PaymentMode = fww.PaymentMode,
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
                    where fww.InvoiceNo == InvoiceNumber && fww.WorkOrderNo== WorkOrderNo
                    select new InvoiceEntity()
                                     {
                                         RegistrationName = fww.CustomerName,
                                         WorkOrderNumber = fww.WorkOrderNo,
                                         InvoiceNumber = fww.InvoiceNo,
                                         UnitPrice = fww.NetAmount,
                                         PaidAmount = fww.Paidamount,
                                         Balance = fww.Balanceamount
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
                    Paidamount = invoiceEntity.PaidAmount,
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
        public bool SavePaymentDetails(InvoiceEntity invoiceEntity)
        {
            string receiptno = "";
            receiptno = GenerateInvBasicNumber();

            try
            {
                _dbContext.PaymentDetails.Add(new PaymentDetail()
                {
                    RegistrationName = invoiceEntity.RegistrationName,
                    WorkOrderNo = invoiceEntity.WorkOrderNo,
                    InvoiceNumber = invoiceEntity.InvoiceNumber,
                    TotalAmount = invoiceEntity.TotalAmount,
                    PaidAmount = invoiceEntity.PaidAmount,
                    PaymentMode = (int)invoiceEntity.PaymentModeMasterId,
                    BankName = invoiceEntity.BankName,
                    DD_ChequeNo_Neft = invoiceEntity.DD_ChequeNo_Neft,
                    DD_ChequeDate = invoiceEntity.DD_ChequeDate,
                    //BranchName = invoiceEntity.BranchName,
                    PayDate = invoiceEntity.PayDate,
                    RecieptNo = receiptno,
                    EnteredBy = 0,
                    EnteredDate = DateTime.UtcNow.Date
                }) ;

                _dbContext.SaveChanges();
               if(invoiceEntity.WorkOrderNo != null)
                {
                    var result = (from DI in _dbContext.TBL_DailyInvoice
                                  where DI.WorkOrderNo == invoiceEntity.WorkOrderNo
                                  select new InvoiceEntity
                                  {
                                      DailyinvoiceID = DI.ID, 
                                  }).FirstOrDefault();
                    var result1 = _dbContext.TBL_DailyInvoice.Find(result.DailyinvoiceID);
                    result1.Paidamount = invoiceEntity.PaidAmount;
                    result1.Balanceamount = invoiceEntity.Balance;
                    _dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        //Generate inventory Number 
        public string GenerateInvBasicNumber()
        {
            string UpdateBasicDetailsNumber = null;
            //var purchase = _dbContext.PurchaseRequestDetails.Find(pid);
            string SerialNumberType = "InventoryBasicDetails";
            bool IsActive = true;
            long SerialNoCount = GetSerialNumber(SerialNumberType, IsActive);
            if (SerialNoCount != 0)
            {
                SerialNoCount = SerialNoCount + 1;
                UpdateSerialNo(SerialNumberType, IsActive, SerialNoCount);
                string date = DateTime.Now.ToShortDateString();
                UpdateBasicDetailsNumber = "XENO / INV /" + date + "/" + SerialNoCount;
            }
            else
            {
                SerialNoCount = 1;
                AddSerialNo(SerialNoCount);
            }
             return UpdateBasicDetailsNumber;
        }
        public long GetSerialNumber(string SrNameType, bool IsActiveField)
        {
            var result = (from e in _dbContext.TBL_InvoiceReceiptNoMaster
                          where e.NumberType == SrNameType && e.IsActive == IsActiveField
                          select new BasicNumberEntity()
                          {
                              NumberValue = e.NumberValue,
                          }
                 ).FirstOrDefault();

            if (result != null)
            {
                long numvalue = (long)result.NumberValue;
                return numvalue;
            }
            else { return 0; }
        }
        // insert code here 
        public long AddSerialNo(long senovalue)
        {
            try
            {
                var insertserialno = new TBL_InvoiceReceiptNoMaster()
                {

                    FinancialYearID = 0,
                    FinancialMonthID = 0,
                    NumberType = "InventoryBasicDetails",
                    NumberValue = senovalue,
                    IsActive = true
                };
                _dbContext.TBL_InvoiceReceiptNoMaster.Add(insertserialno);
                _dbContext.SaveChanges();
                return insertserialno.ID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string UpdateSerialNo(string SrNameType, bool IsActiveField, long SerialNoUpdate)
        {
            try
            {
                var serilano = (from e in _dbContext.TBL_InvoiceReceiptNoMaster
                                where SrNameType == e.NumberType && IsActiveField == e.IsActive
                                select new BasicNumberEntity()
                                {
                                    SRNOID = e.ID,
                                }
                            ).FirstOrDefault();
                var serialNumberMaster = _dbContext.TBL_InvoiceReceiptNoMaster.Find(serilano.SRNOID);
                serialNumberMaster.NumberValue = SerialNoUpdate;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        //update code
        public List<InvoiceEntity> GetDailyinvoicelist()
        {
            try
            {
                return (from e in _dbContext.TBL_DailyInvoice
                       
                        select new InvoiceEntity()
                        {
                           RegistrationName=e.CustomerName,
                           WorkOrderNumber=e.WorkOrderNo,
                           InvoiceNumber=e.InvoiceNo,
                           UnitPrice=e.NetAmount,
                           PaidAmount=e.Paidamount,
                           Balance=e.Balanceamount,
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
        public List<InvoiceEntity> GetPaymentReceipt()
        {
            try
            {
                return (from e in _dbContext.PaymentDetails
                        //where e.PaymentDetailsId == PaymentDetailsId
                        select new InvoiceEntity()
                        {
                            RecieptNo = e.RecieptNo,
                            PaidAmount = e.PaidAmount,
                            PayDate = e.PayDate,
                            EnteredBy = e.EnteredBy,
                            EnteredDate = e.EnteredDate,
                            RegistrationName = e.RegistrationName,
                            InvoiceNumber = e.InvoiceNumber,
                            TotalAmount = e.TotalAmount,
                            //EnquirySampleID = (int)e.EnquirySampleId
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
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
                                      ID = DI.ID,
                                      RejectStatus = DI.RejectStatus
                                  }).FirstOrDefault();

                    //  var reject1= _dbContext.TBL_DailyInvoice.Find(reject.ID);
                    //reject1.RejectStatus = "Rejected";
                    //_dbContext.SaveChanges();
                    // we delete the record from db when invoice is rejected
                    var rejectid = _dbContext.TBL_DailyInvoice.SingleOrDefault(x => x.ID == reject.ID);
                    if (rejectid != null)
                    {
                        _dbContext.TBL_DailyInvoice.Remove(rejectid);
                        _dbContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
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
                            Balance = e.Balanceamount,
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
    }
}