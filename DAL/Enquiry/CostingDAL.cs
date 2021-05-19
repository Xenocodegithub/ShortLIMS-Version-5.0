using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.Enquiry;

namespace LIMS_DEMO.DAL.Enquiry
{
    public class CostingDAL : ICosting
    {
        readonly LIMSEntities _dbContext;

        public CostingDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public List<CostingEntity> GetPGSGM(int EnquiryID)
        {
            return (
                        from ems in _dbContext.EnquiryMasterSampleTypes
                       //join ed in _dbContext.EnquiryDetails on ems.EnquiryMasterSampleTypeId equals ed.EnquiryMasterSampleTypeId
                        //join ed in _dbContext.Enquiry on ems.EnquiryMasterSampleTypeId equals ed.EnquiryMasterSampleTypeId
                        join stp in _dbContext.SampleTypeProductMasters on ems.SampleTypeProductId equals stp.SampleTypeProductId
                         where ems.EnquiryId == EnquiryID

                   select new CostingEntity()
                    {
                       SampleName=ems.SampleNo,
                       SampleTypeProductId= (Int32)ems.SampleTypeProductId,
                       EnquiryMasterSampleTypeId=ems.EnquiryMasterSampleTypeId,
                       SampleTypeProductName=stp.SampleTypeProductName,
                   }).ToList();
        }

        public bool AddCosting(CostingEntity costingEntity)
        {
            _dbContext.Costings.Add(new Costing()
            {
                EnquirySampleID = costingEntity.EnquirySampleID,
                CollectionCharges = costingEntity.CollectionCharges,
                TransportCharges = costingEntity.TransportCharges,
                TotalCharges = costingEntity.TotalCharges,
                TestingCharges = costingEntity.TestingCharges,
                GST = costingEntity.GST,
                Discount = costingEntity.Discount,
                UnitPrice = costingEntity.UnitPrice,
                IsActive = true,
                EnteredBy = costingEntity.EnteredBy,
                EnteredDate = DateTime.UtcNow
            });
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateCosting(CostingEntity costingEntity)
        {
            var cost = _dbContext.Costings.Find(costingEntity.CostingId);
            cost.EnquirySampleID = costingEntity.EnquirySampleID;
            cost.CollectionCharges = costingEntity.CollectionCharges;
            cost.TransportCharges = costingEntity.TransportCharges;
            cost.TotalCharges = costingEntity.TotalCharges;
            cost.TestingCharges = costingEntity.TestingCharges;
            cost.GST = costingEntity.GST;
            cost.Discount = costingEntity.Discount;
            cost.UnitPrice = costingEntity.UnitPrice;
            cost.ModifiedBy = costingEntity.EnteredBy;
            _dbContext.SaveChanges();
            return true;
        }

        public CostingEntity GetCosting(int EnquirySampleID, int CostingId)
        {
            //return (from es in _dbContext.EnquirySampleDetails
            //        join c in _dbContext.Costings on es.EnquirySampleID equals c.EnquirySampleID
            //        into costing
            //        from cost in costing.DefaultIfEmpty()
            //        where es.EnquirySampleID == EnquirySampleID && (CostingId == 0 || cost.CostingId == CostingId)
            //        && es.IsActive == true
            return (from w in _dbContext.WorkOrders
                    join q in _dbContext.Quotations on w.QuotationId equals q.QuotationId
                    join e in _dbContext.EnquiryMasters on q.EnquiryId equals e.EnquiryId
                    join c in _dbContext.CustomerMasters on e.CustomerMasterId equals c.CustomerMasterId
                    join ed in _dbContext.EnquiryDetails on e.EnquiryId equals ed.EnquiryId
                    join esd in _dbContext.EnquirySampleDetails on ed.EnquiryDetailId equals esd.EnquiryDetailId
                    join ct in _dbContext.Costings on esd.EnquirySampleID equals ct.EnquirySampleID
                    into  costing
                    from cost in costing.DefaultIfEmpty()
                    where esd.EnquirySampleID == EnquirySampleID && (CostingId == 0 || cost.CostingId == CostingId)
                    && esd.IsActive == true
                    select new CostingEntity()
                    {
                        RegistrationName = c.RegistrationName,
                        WorkOrderNumber=w.WorkOrderNo,
                        InvoiceNumber=w.WorkOrderNo,
                        CostingId = cost.CostingId,
                        EnquirySampleID = esd.EnquirySampleID,
                        TotalCharges = cost.TotalCharges == null ? 0 : cost.TotalCharges,
                        CollectionCharges = cost.CollectionCharges == null ? 0 : cost.CollectionCharges,
                        TransportCharges = cost.TransportCharges == null ? 0 : cost.TransportCharges,
                        TestingCharges = cost.TestingCharges == null ? 0 : cost.TestingCharges,
                        IsIGST = w.IsIGST,
                        Discount = cost.Discount == null ? 0 : cost.Discount,
                        UnitPrice = cost.UnitPrice
                    }).FirstOrDefault();
        }
        
        public List<CostingEntity> GetCostingList(int EnquiryId)
        {
            return (from es in _dbContext.EnquirySampleDetails
                    join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId
                    //join p in _dbContext.ProductGroupMasters on ed.ProductGroupId equals p.ProductGroupId
                    //join s in _dbContext.SubGroupMasters on ed.SubGroupId equals s.SubGroupId
                    //join m in _dbContext.MatrixMasters on ed.MatrixId equals m.MatrixId
                    join stp in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stp.SampleTypeProductId
                    join c in _dbContext.Costings on es.EnquirySampleID equals c.EnquirySampleID                    
                    where ed.EnquiryId == EnquiryId && (c.CostingId != null && c.CostingId != 0)
                    select new CostingEntity()
                    {
                        CostingId = c.CostingId,
                        EnquirySampleID = es.EnquirySampleID,
                        TotalCharges = c.TotalCharges,
                        CollectionCharges = c.CollectionCharges,
                        TransportCharges = c.TransportCharges,
                        TestingCharges = c.TestingCharges,
                        GST = c.GST,
                        Discount = c.Discount,
                        UnitPrice = c.UnitPrice,
                        SampleName = es.SampleName,
                        DisplaySampleName = es.DisplaySampleName,
                        SampleTypeProductName = stp.SampleTypeProductName,
                        SampleTypeProductCode = stp.SampleTypeProductCode,
                        SampleTypeProductId = (Int32)ed.SampleTypeProductId,
                        //ProductGroupName = p.ProductGroupName,
                        //SubGroupName = s.SubGroupName,
                        //MatrixName = m.MatrixName,
                        IsSetPCBLimit = _dbContext.ParameterGroupMasters.Where(pg => pg.SampleTypeProductId == ed.SampleTypeProductId/* && pg.SubGroupId == ed.SubGroupId*/
                        /*&& pg.MatrixId == ed.MatrixId*/).Select(pg => pg.IsSetPCBLimit).FirstOrDefault(),
                        NoOfSample = es.NoOfSample
                    }).ToList();
        }
        //We have to create new method for get costing list
        public List<CostingEntity> GetSampleTypeCostingList(int EnquiryId)
        {
            return (from es in _dbContext.EnquiryMasterSampleTypes
                        //join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId
                        //join p in _dbContext.ProductGroupMasters on ed.ProductGroupId equals p.ProductGroupId
                        //join s in _dbContext.SubGroupMasters on ed.SubGroupId equals s.SubGroupId
                        //join m in _dbContext.MatrixMasters on ed.MatrixId equals m.MatrixId
                    join stp in _dbContext.SampleTypeProductMasters on es.SampleTypeProductId equals stp.SampleTypeProductId
                    join c in _dbContext.Costings on es.EnquiryMasterSampleTypeId equals c.EnquiryMasterSampleTypeId
                    into costing
                    from cost in costing.DefaultIfEmpty()
                    where es.EnquiryId == EnquiryId && (cost.CostingId != null && cost.CostingId != 0)
                    select new CostingEntity()
                    {
                        CostingId = cost.CostingId,
                        EnquiryMasterSampleTypeId = es.EnquiryMasterSampleTypeId,
                        TotalCharges = cost.TotalCharges,
                        CollectionCharges = cost.CollectionCharges,
                        TransportCharges = cost.TransportCharges,
                        TestingCharges = cost.TestingCharges,
                        GST = cost.GST,
                        Discount = cost.Discount,
                        UnitPrice = cost.UnitPrice,
                        SampleName = es.SampleNo,
                        SampleTypeProductName = stp.SampleTypeProductName,
                        SampleTypeProductId = (Int32)es.SampleTypeProductId,
                        //DisplaySampleName=es.DisplaySampleName,
                        //ProductGroupName = p.ProductGroupName,
                        //SubGroupName = s.SubGroupName,
                        //MatrixName = m.MatrixName,
                        //IsSetPCBLimit = _dbContext.ParameterGroupMasters.Where(pg=> pg.ProductGroupId == ed.ProductGroupId && pg.SubGroupId == ed.SubGroupId
                        //&& pg.MatrixId == ed.MatrixId).Select(pg=> pg.IsSetPCBLimit).FirstOrDefault(),
                        //NoOfSample = es.NoOfSample
                    }).ToList();
        }
        public List<TermsAndConditionEntity> GetTermsAndCondition(int EnquiryId)
        {
            var parameters = _dbContext.EnquiryDetails.Where(e => e.EnquiryId == EnquiryId).Select(e =>
                e.ProductGroupId
            ).ToList();

            try
            {
              //var x =   (from t in _dbContext.TermsAndConditions
              //   join qt in _dbContext.QuotationTNCs on t.TermAndCondtId equals qt.TermAndCondtId
              //   into qtt
              //   from a in qtt.DefaultIfEmpty()
              //   where t.IsActive == true && (parameters.Contains((Int32)t.ProductGroupId) || t.ProductGroupId == null)
              //   select new TermsAndConditionEntity()
              //   {
              //       TermAndCondtId = t.TermAndCondtId,
              //       TermAndCondt = t.TermAndCondt,
              //       IsSelected = a.QuotationTNCId == null || a.QuotationTNCId == 0 ? false : true
              //   }).ToList();

              //  long? QuotationId = _dbContext.Quotations.Where(q => q.EnquiryId == EnquiryId).Select(q => q.QuotationId).FirstOrDefault();

              //  var y = (from t in _dbContext.TermsAndConditions
              //           join qt in _dbContext.QuotationTNCs on t.TermAndCondtId equals qt.TermAndCondtId
              //           into qtt
              //           from a in qtt.DefaultIfEmpty()
              //           where t.IsActive == true && (parameters.Contains((Int32)t.ProductGroupId) || t.ProductGroupId == null) && (QuotationId == null || QuotationId == 0 || a.QuotationId == QuotationId)
              //           select new TermsAndConditionEntity()
              //           {
              //               TermAndCondtId = t.TermAndCondtId,
              //               TermAndCondt = t.TermAndCondt,
              //               IsSelected = a.QuotationTNCId == null || a.QuotationTNCId == 0 ? false : true
              //           }).ToList();


                return (from t in _dbContext.TermsAndConditions
                        join qt in _dbContext.QuotationTNCs on t.TermAndCondtId equals qt.TermAndCondtId
                        into qtt
                        from a in qtt.Where(b => b.QuotationId == _dbContext.Quotations.Where(q => q.EnquiryId == EnquiryId).Select(q => q.QuotationId).FirstOrDefault()).DefaultIfEmpty()
                        where t.IsActive == true && (parameters.Contains((Int32)t.ProductGroupId) || t.ProductGroupId == null ) 
                        select new TermsAndConditionEntity()
                        {
                            TermAndCondtId = t.TermAndCondtId,
                            TermAndCondt = t.TermAndCondt,
                            IsSelected = a.QuotationTNCId == null || a.QuotationTNCId == 0 ? false : true,
                            Remark = a.Remarks,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeleteCosting(int CostingId)
        {
            var EnquiryId = (from c in _dbContext.Costings
                             join es in _dbContext.EnquirySampleDetails on c.EnquirySampleID equals es.EnquirySampleID
                             join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId
                             select new
                             {
                                 ed.EnquiryId
                             }).FirstOrDefault().EnquiryId;

            _dbContext.Costings.Remove(_dbContext.Costings.Find(CostingId));
            _dbContext.SaveChanges();

            if (_dbContext.Quotations.Any(q => q.EnquiryId == EnquiryId))
            {
                var QuotationId = _dbContext.Quotations.Where(q => q.EnquiryId == EnquiryId).Select(q => q.QuotationId).FirstOrDefault();
                var tblQuot = _dbContext.Quotations.Find(QuotationId);
                tblQuot.QuotedAmount = GetQuotedAmount((long)EnquiryId.Value);
                _dbContext.SaveChanges();
            }

            return true;
        }

        public long AddQuotation(QuotationEntity quotation)
        {
            if (_dbContext.Quotations.Any(q => q.EnquiryId == quotation.EnquiryId))
            {
                var QuotationId = _dbContext.Quotations.Where(q => q.EnquiryId == quotation.EnquiryId).Select(q => q.QuotationId).FirstOrDefault();
                var tblQuot = _dbContext.Quotations.Find(QuotationId);
                tblQuot.QuotedAmount = quotation.QuotedAmount;
                tblQuot.ModifiedBy = quotation.EnteredBy;
                _dbContext.SaveChanges();
                return QuotationId;
            }
            else
            {
                Quotation quote = new Quotation();
                quote.EnquiryId = quotation.EnquiryId;
                quote.QuotedAmount = quotation.QuotedAmount;
                quote.IsActive = true;
                quote.EnteredBy = quotation.EnteredBy;
                quote.EnteredDate = DateTime.UtcNow;
                _dbContext.Quotations.Add(quote);
                _dbContext.SaveChanges();
                return quote.QuotationId;
            }
        }

        public bool AddQuotationTermsAndCondition(List<QuotationTNCEntity> quotationTerms, long QuotationId,string Remarks)
        {
            var QuotDel = _dbContext.QuotationTNCs.Where(q => q.QuotationId == QuotationId).ToList();
            _dbContext.QuotationTNCs.RemoveRange(QuotDel);
            _dbContext.SaveChanges();

            List<QuotationTNC> quots = new List<QuotationTNC>();
            foreach (var q in quotationTerms)
            {
                quots.Add(new QuotationTNC()
                {
                    QuotationId = QuotationId,
                    TermAndCondtId = q.TermAndCondtId,
                    Remarks= Remarks,
                    IsActive = true,
                    EnteredBy = q.EnteredBy,
                    EnteredDate = DateTime.UtcNow
                });
            }
            _dbContext.QuotationTNCs.AddRange(quots);
            _dbContext.SaveChanges();

            return true;
        }

        public decimal GetQuotedAmount(long EnquiryId)
        {
            return (from c in _dbContext.Costings
                    join s in _dbContext.EnquirySampleDetails on c.EnquirySampleID equals s.EnquirySampleID
                    join e in _dbContext.EnquiryDetails on s.EnquiryDetailId equals e.EnquiryDetailId
                    where e.EnquiryId == EnquiryId && c.IsActive == true && s.IsActive == true && c.IsActive == true
                    select new
                    {
                        UnitPrice = c.UnitPrice == null ? 0 : (decimal)c.UnitPrice
                    }).ToList().Sum(c => c.UnitPrice);
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
