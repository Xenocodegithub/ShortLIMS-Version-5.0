using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.WorkOrderCustomer;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.Enquiry;

namespace LIMS_DEMO.DAL.WorkOrderCustomer
{
    public class WorkOrderCustomerDAL : IWorkOrderCustomer
    {
        readonly LIMSEntities _dbContext;

        public WorkOrderCustomerDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public long AddNotification(string Msg, string RoleName, WorkOrderCustomerEntity workOrderCustomerEntity)
        {
            try
            {
                var notific = new NotificationDetail()
                {
                    RoleId = null,
                    NotificationName = workOrderCustomerEntity.WorkOrderNo,
                    Message = Msg,
                    DateTime = (DateTime)workOrderCustomerEntity.WORecieveDate,
                    IsActive = true,
                    EnteredBy = workOrderCustomerEntity.EnteredBy,
                    EnteredDate = DateTime.Now,
                    RoleName = RoleName,
                };
                _dbContext.NotificationDetails.Add(notific);
                _dbContext.SaveChanges();
                return notific.NotificationDetailId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public bool DeleteCosting(int CostingId)
        {
            _dbContext.Costings.Remove(_dbContext.Costings.Find(CostingId));//Complete Deleted from DB
            _dbContext.SaveChanges();
            return true;
        }
        public void DeleteSample(long EnquirySampleId, bool isActive)
        {
            try
            {
                _dbContext.EnquirySampleDetails.Remove(_dbContext.EnquirySampleDetails.Find(EnquirySampleId));//Complete Deleted from DB
                _dbContext.SaveChanges();

                //only Status is changed : isActive = false
                //var sampleDetail = _dbContext.EnquirySampleDetails.Find(EnquirySampleId);
                //if (sampleDetail != null)
                //{
                //  sampleDetail.IsActive = isActive;
                //  _dbContext.SaveChanges();
                //}
            }
            catch (Exception ex)
            {
               // ex.InnerException.Message;
            }
        }
        public string DeleteContract(long EnquiryDetailId)
        {
            try
            {
                _dbContext.EnquiryDetails.Remove(_dbContext.EnquiryDetails.Find(EnquiryDetailId));//Complete Deleted from DB
                _dbContext.SaveChanges();

                //only Status is changed : isActive = false
                // var contract = _dbContext.EnquiryDetails.Find(EnquiryDetailId);
                // contract.IsActive = false;
                //_dbContext.EnquiryDetails.Remove(contract);
                //_dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public long Add(WorkOrderCustomerEntity workOrderCustomerEntity)
        {
            try
            {
                var workOrderCustomer = new WorkOrder()
                {
                    //WorkOrderId= workOrderCustomerEntity.WorkOrderId,
                    //Remark = workOrderCustomerEntity.Remark,
                    //ModeOfCommunicationId = workOrderCustomerEntity.ModeOfCommunicationId,
                    //QuotationId = workOrderCustomerEntity.QuotationId,
                    OVC = workOrderCustomerEntity.OVC,
                    CustomerMasterId = workOrderCustomerEntity.CustomerMasterId,
                    DeliverId = workOrderCustomerEntity.DeliverId,
                    StatusId = workOrderCustomerEntity.StatusId,
                    LabMasterId = workOrderCustomerEntity.LabMasterId,
                    WORecieveDate = (DateTime)workOrderCustomerEntity.WORecieveDate,
                    WOEndDate = (DateTime)workOrderCustomerEntity.WOEndDate,
                   // ExpectSampleCollDate = (DateTime)workOrderCustomerEntity.ExpectSampleCollDate,
                    Duration = workOrderCustomerEntity.Duration,
                    WOUpload = workOrderCustomerEntity.WOUpload,
                    FileName = workOrderCustomerEntity.FileName,
                    WorkOrderNo = workOrderCustomerEntity.WorkOrderNo,
                    IsActive = workOrderCustomerEntity.IsActive,
                    EnteredBy = workOrderCustomerEntity.EnteredBy,
                    EnteredDate = DateTime.Now,
                    ExpectSampleCollDate = workOrderCustomerEntity.ExpectSampleCollDate
                };
                _dbContext.WorkOrders.Add(workOrderCustomer);
                _dbContext.SaveChanges();
                return workOrderCustomer.WorkOrderId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string AddCostPercentage(WorkOrderCustomerEntity workOrderCustomerEntity)
        {
            try
            {
                var workOrderCustomer = _dbContext.WorkOrders.Find(workOrderCustomerEntity.WorkOrderId);
                workOrderCustomer.IsIGST = workOrderCustomerEntity.IsIGST;
                workOrderCustomer.ContractAmendment = workOrderCustomerEntity.ContractAmendment;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string Update(WorkOrderCustomerEntity workOrderCustomerEntity)
        {
            try
            {
                var workOrderCustomer = _dbContext.WorkOrders.Find(workOrderCustomerEntity.WorkOrderId);
                workOrderCustomer.WorkOrderId = workOrderCustomerEntity.WorkOrderId;
                //workOrderCustomer.Remark = workOrderCustomerEntity.Remark;
                workOrderCustomer.WorkOrderNo = workOrderCustomerEntity.WorkOrderNo;
                workOrderCustomer.ModeOfCommunicationId = workOrderCustomerEntity.ModeOfCommunicationId;
                workOrderCustomer.CustomerMasterId = workOrderCustomerEntity.CustomerMasterId;
                workOrderCustomer.DeliverId = workOrderCustomerEntity.DeliverId;
                workOrderCustomer.WORecieveDate = (DateTime)workOrderCustomerEntity.WORecieveDate;
                workOrderCustomer.WOEndDate = (DateTime)workOrderCustomerEntity.WOEndDate;
                workOrderCustomer.ExpectSampleCollDate = (DateTime)workOrderCustomerEntity.ExpectSampleCollDate;
                workOrderCustomer.Duration = workOrderCustomerEntity.Duration;
                workOrderCustomer.WOUpload = workOrderCustomerEntity.WOUpload;
                workOrderCustomer.FileName = workOrderCustomerEntity.FileName;
                workOrderCustomer.StatusId = workOrderCustomerEntity.StatusId;
                workOrderCustomer.LabMasterId = workOrderCustomerEntity.LabMasterId;
                workOrderCustomer.IsActive = workOrderCustomerEntity.IsActive;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public WorkOrderSampleEntity AddContract(WorkOrderSampleEntity entity)
        {
            //try
            //{
                EnquiryDetail Detail = new EnquiryDetail();
                Detail.SampleTypeProductId = entity.SampleTypeProductId;
            //Detail.ProductGroupId = entity.ProductGroupId;
            //Detail.SubGroupId = entity.SubGroupId;
            //Detail.MatrixId = entity.MatrixId;
            //Detail.EnquiryId = entity.EnquiryId;
                Detail.WorkOrderID = entity.WorkOrderID;
                Detail.EnquiryDetailId = entity.EnquiryDetailId;
                Detail.IsActive = true;
                Detail.EnteredBy = entity.EnteredBy;
                Detail.EnteredDate = DateTime.UtcNow;
                _dbContext.EnquiryDetails.Add(Detail);
                _dbContext.SaveChanges();
                entity.EnquiryDetailId = Detail.EnquiryDetailId;
          
                return entity;

                //_dbContext.EnquiryDetails.Add(new EnquiryDetail()
                //{
                //    ProductGroupId = entity.ProductGroupId,
                //    SubGroupId = entity.SubGroupId,
                //    MatrixId = entity.MatrixId,
                //    WorkOrderID= entity.WorkOrderID,
                //    IsActive = true,
                //    EnteredBy = entity.EnteredBy,
                //    EnteredDate = System.DateTime.UtcNow
                //});

               //_dbContext.SaveChanges();
               // return "success";
            //}
            //catch (Exception ex)
            //{
            //    return ex.InnerException.Message;
            //}

        }
        public List<WorkOrderSampleEntity> GetContractList(int WorkOrderID)
        {
            try
            {
                return (from e in _dbContext.EnquiryDetails
                        join stp in _dbContext.SampleTypeProductMasters on e.SampleTypeProductId equals stp.SampleTypeProductId
                        //join p in _dbContext.ProductGroupMasters on e.ProductGroupId equals p.ProductGroupId
                        //join s in _dbContext.SubGroupMasters on e.SubGroupId equals s.SubGroupId
                        //join m in _dbContext.MatrixMasters on e.MatrixId equals m.MatrixId
                        //into contracts
                        //from c in contracts
                        where e.WorkOrderID == WorkOrderID && e.IsActive == true
                        select new WorkOrderSampleEntity()
                        {
                            WorkOrderID=e.WorkOrderID,
                            EnquiryDetailId = e.EnquiryDetailId,
                            //ProductGroupId = (Int32)e.ProductGroupId,
                            SampleTypeProductId = e.SampleTypeProductId,
                            SampleTypeProductName = stp.SampleTypeProductName,
                            SampleTypeProductCode = stp.SampleTypeProductCode,
                            //ProductGroupName = p.ProductGroupName,
                            //SubGroupId = (Int32)e.SubGroupId,
                            //SubGroupName = s.SubGroupName,
                            //SubGroupCode = s.SubGroupCode,
                            //MatrixId = e.MatrixId,
                            //MatrixName = c.MatrixName,

                        }).ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string UpdateWorkOrderCustomerStatus(long WorkOrderID, int StatusId)
        {
            try
            {
                var workorderMaster = _dbContext.WorkOrders.Find(WorkOrderID);
                workorderMaster.StatusId = (byte)StatusId;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public List<EnquirySampleEntity> GetWorkOrderCustomerSampleList(int WorkOrderID)
        {
            return (from es in _dbContext.EnquirySampleDetails
                    join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId
                    join stp in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stp.SampleTypeProductId
                    //join p in _dbContext.ProductGroupMasters on ed.ProductGroupId equals p.ProductGroupId
                    //join s in _dbContext.SubGroupMasters on ed.SubGroupId equals s.SubGroupId
                    //join m in _dbContext.MatrixMasters on ed.MatrixId equals m.MatrixId
                    //join e in _dbContext.EnquiryMasters on ed.EnquiryId equals e.EnquiryId//Added by Nivedita
                    //join status in _dbContext.StatusMasters on e.StatusId equals status.StatusId//Added by Nivedita
                    into parameters
                    from par in parameters.DefaultIfEmpty()
                    where ed.WorkOrderID == WorkOrderID && es.IsActive == true && ed.IsActive == true
                    select new EnquirySampleEntity()
                    {
                        EnquirySampleID = es.EnquirySampleID,
                        EnquiryDetailId = es.EnquiryDetailId,
                        SampleName = es.SampleName,
                        DisplaySampleName = es.DisplaySampleName,
                        ProcedureId = ed.ProductGroupId,
                        SampleTypeProductName = par.SampleTypeProductName,
                        SampleTypeProductId = par.SampleTypeProductId,
                        //ProductGroupName = p.ProductGroupName,
                        //SubGroupName = s.SubGroupName,
                        //MatrixName = par.MatrixName,
                        // MatrixName = ed.MatrixMaster.MatrixName,//Added by Nivedita
                        //CurrentStatus = e.StatusMaster.StatusName,//Added by Nivedita
                        //Cost = es.TotalCharges == null ?0:(decimal)es.TotalCharges
                        //Cost = _dbContext.EnquiryParameterDetails.Where(epd => epd.EnquirySampleID == es.EnquirySampleID).Select(c => c.Charges == null ? 0 : c.Charges).Sum() == null ? 0 : (decimal)_dbContext.EnquiryParameterDetails.Where(epd => epd.EnquirySampleID == es.EnquirySampleID).Select(c => c.Charges == null ? 0 : c.Charges).Sum(),

                    }).ToList();
        }
        public List<TermsAndConditionEntity> GetWorkOrderCustomerTermsAndCondition(int WorkOrderID)
        {
            var parameters = _dbContext.EnquiryDetails.Where(e => e.WorkOrderID == WorkOrderID).Select(e =>e.ProductGroupId).ToList();

            try
            {

                return (from t in _dbContext.TermsAndConditions
                        join qt in _dbContext.QuotationTNCs on t.TermAndCondtId equals qt.TermAndCondtId
                        into qtt
                        from a in qtt.Where(b => b.WorkOrderID == WorkOrderID).DefaultIfEmpty()
                        where t.IsActive == true && (parameters.Contains((Int32)t.ProductGroupId) || t.ProductGroupId == null)
                        select new TermsAndConditionEntity()
                        {
                            TermAndCondtId = t.TermAndCondtId,
                            TermAndCondt = t.TermAndCondt,
                            IsSelected = a.QuotationTNCId == null || a.QuotationTNCId == 0 ? false : true
                        }).ToList();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<CostingEntity> GetWorkOrderCustomerCostingList(int WorkOrderID)
        {
            return (from es in _dbContext.EnquirySampleDetails
                    join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId
                    join stp in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stp.SampleTypeProductId
                    //join p in _dbContext.ProductGroupMasters on ed.ProductGroupId equals p.ProductGroupId
                    //join s in _dbContext.SubGroupMasters on ed.SubGroupId equals s.SubGroupId
                    //join m in _dbContext.MatrixMasters on ed.MatrixId equals m.MatrixId
                    join c in _dbContext.Costings on es.EnquirySampleID equals c.EnquirySampleID
                    into costing
                    from cost in costing.DefaultIfEmpty()
                    where ed.WorkOrderID == WorkOrderID && (cost.CostingId != null && cost.CostingId != 0)
                    select new CostingEntity()
                    {
                        CostingId = cost.CostingId,
                        EnquirySampleID = es.EnquirySampleID,
                        TotalCharges = cost.TotalCharges,
                        CollectionCharges = cost.CollectionCharges,
                        TransportCharges = cost.TransportCharges,
                        TestingCharges = cost.TestingCharges,
                        GST = cost.GST,
                        Discount = cost.Discount,
                        UnitPrice = cost.UnitPrice,
                        SampleName = es.SampleName,
                        DisplaySampleName = es.DisplaySampleName,
                        SampleTypeProductId = (Int32)ed.SampleTypeProductId,
                        SampleTypeProductName = stp.SampleTypeProductName,
                        SampleTypeProductCode = stp.SampleTypeProductCode,
                        //ProductGroupName = p.ProductGroupName,
                        //SubGroupName = s.SubGroupName,
                        //MatrixName = m.MatrixName,
                        IsSetPCBLimit = _dbContext.ParameterGroupMasters.Where(pg => pg.SampleTypeProductId == ed.SampleTypeProductId /*&& pg.SubGroupId == ed.SubGroupId*/
                       /* && pg.MatrixId == ed.MatrixId*/).Select(pg => pg.IsSetPCBLimit).FirstOrDefault(),
                        NoOfSample = es.NoOfSample
                    }).ToList();
        }
        public WorkOrderEntity GetWorkOrderCustomerDetail(int WorkOrderID)
        {
            return (from w in _dbContext.WorkOrders
                    join c in _dbContext.CustomerMasters on w.CustomerMasterId equals c.CustomerMasterId
                    into order
                    from o in order.DefaultIfEmpty()
                    where w.WorkOrderId == WorkOrderID
                    select new WorkOrderEntity()
                    {
                        WorkOrderId = w.WorkOrderId,
                        RegistrationName = o.RegistrationName,
                        ExpectSampleCollDate = w.ExpectSampleCollDate,
                        WORecieveDate = w.WORecieveDate,
                        WOEndDate = w.WOEndDate,
                        
                        Duration=w.Duration,
                        WOUpload = w.WOUpload,
                        FileName = w.FileName,
                        //QuotedAmount = q.QuotedAmount,
                        WorkOrderNo = w.WorkOrderNo,
                        IsIGST = w.IsIGST,
                    }).FirstOrDefault();
        }
        public List<WorkOrderCustomerEntity> GetWorkOrderCustomerList(int LabMasterId, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                return (from w in _dbContext.WorkOrders
                        //join mdc in _dbContext.ModeOfCommunications on w.ModeOfCommunicationId equals mdc.ModeOfCommunicationId
                        join ctm in _dbContext.CustomerMasters on w.CustomerMasterId equals ctm.CustomerMasterId
                        join es in _dbContext.StatusMasters on w.StatusId equals es.StatusId
                        into workOrder
                        from wo in workOrder.DefaultIfEmpty()
               
                        where w.LabMasterId == LabMasterId && w.IsActive && w.QuotationId == null && ((FromDate == null && ToDate == null) || (w.WORecieveDate >= FromDate && w.WORecieveDate <= ToDate)) || w.OVC==true
                        select new WorkOrderCustomerEntity()
                        {  
                            WorkOrderId = w.WorkOrderId,
                            WorkOrderNo = w.WorkOrderNo,
                            WORecieveDate = w.WORecieveDate,
                            WOEndDate = w.WOEndDate,
                            CustomerMasterId =w.CustomerMasterId,
                            IsActive =w.IsActive,
                            //CommunicationMode = mdc.CommunicationMode,
                            CustomerName = ctm.RegistrationName,
                            CurrentStatus = wo.StatusName == null ? "" : wo.StatusName,
                            AssignToId = w.AssignedToId,
                            Remark = w.Remark,
                            IsIGST = w.IsIGST,
                            WOUpload = w.WOUpload,
                            FileName = w.FileName,
                            EnteredBy=w.EnteredBy,
                        }).OrderByDescending(wo => wo.WorkOrderId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void WorkOrderApprove(int WorkOrderId, int iStatusId, int UserId)
        {
            try
            {

                List<long> EnquirySampleIds = new List<long>();
                List<long> EnquiryDetailIds = _dbContext.EnquiryDetails.Where(e => e.WorkOrderID == WorkOrderId).Select(e => e.EnquiryDetailId).ToList();
                var EnquirySample = _dbContext.EnquirySampleDetails.Where(e => EnquiryDetailIds.Contains(e.EnquiryDetailId)).ToList();


                for (int i = 0; i < EnquiryDetailIds.Count; i++)
                {
                    //int j = 0;
                    long enquiryDetaailsId = EnquiryDetailIds[i];
                    var _enquirySampleIds = _dbContext.EnquirySampleDetails.Where(e => e.EnquiryDetailId == enquiryDetaailsId).Select(e => e.EnquirySampleID).ToList();
                    EnquirySampleIds.Add(_enquirySampleIds[0]);
                    // j++;
                }
                var LocationSample = _dbContext.LocationSampleCollections.Where(e => EnquirySampleIds.Contains(e.EnquirySampleId)).ToList();
                List<int> SampleCollectionDateIds = _dbContext.WorkOrderSampleCollectionDates.Where(e => e.WorkOrderId == WorkOrderId).Select(e => e.WorkOrderSampleCollectionDateId).ToList();
               
                var wodates = (from wodate in _dbContext.WorkOrderSampleCollectionDates
                               join womaster in _dbContext.WOMasterSampleCollectionDates on wodate.WOMasterSampleCollectionDateId equals womaster.WOMasterSampleCollectionDateId

                               where wodate.WorkOrderId == WorkOrderId
                               select new
                               {
                                   womaster.EnquirySampleID,
                                   wodate.WorkOrderSampleCollectionDateId,
                                   wodate.LocationSampleCollectionID,
                                   wodate.LocationSampleName,
                               }

                          ).ToList();

                List<SampleCollection> samples = new List<SampleCollection>();
                //int iCount = 0;
                foreach (var samp in wodates)
                {
                    samples.Add(new SampleCollection()
                    {
                        //Adding Values in SampleCollection tbl first time frm here
                        WorkOrderID = WorkOrderId,
                        EnquirySampleID = samp.EnquirySampleID,
                        WorkOrderSampleCollectionDateId = samp.WorkOrderSampleCollectionDateId,
                        LocationSampleCollectionID = samp.LocationSampleCollectionID,
                        SampleNo = samp.LocationSampleName,
                        IsSampleIntact = true,
                        Source = "",
                        Iteration = 1,
                        StatusId = (byte)iStatusId,
                        EnteredBy = UserId,
                        EnteredDate = DateTime.UtcNow,
                        IsActive = true
                    });
                }
                _dbContext.SampleCollections.AddRange(samples);
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {

            }
        }
        public bool AddWorkOrderCosting(WorkOderCostingEntity costingEntity)
        {
            _dbContext.Costings.Add(new Costing()
            {
                EnquirySampleID = costingEntity.EnquirySampleID,
                TotalCharges = costingEntity.TotalCharges,
                CollectionCharges = costingEntity.CollectionCharges,
                TransportCharges = costingEntity.TransportCharges,
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

        public bool UpdateWorkOrderCosting(WorkOderCostingEntity costingEntity)
        {
            var cost = _dbContext.Costings.Find(costingEntity.CostingId);
            cost.EnquirySampleID = costingEntity.EnquirySampleID;
            cost.TotalCharges = costingEntity.TotalCharges;
            cost.CollectionCharges = costingEntity.CollectionCharges;
            cost.TransportCharges = costingEntity.TransportCharges;
            cost.TestingCharges = costingEntity.TestingCharges;
            cost.GST = costingEntity.GST;
            cost.Discount = costingEntity.Discount;
            cost.UnitPrice = costingEntity.UnitPrice;
            cost.ModifiedBy = costingEntity.EnteredBy;
            _dbContext.SaveChanges();
            return true;
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
