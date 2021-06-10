using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.Core.Repository;

namespace LIMS_DEMO.DAL.Enquiry
{
    public class WorkOrderDAL : IWorkOrder
    {

        readonly LIMSEntities _dbContext;

        public WorkOrderDAL()
        {
            _dbContext = new LIMSEntities();
        }

        //for invoice by month
        public WorkOrderEntity GetLastDay(string monthName)
        {
            var res = (from mtd in _dbContext.MonthTotalDays1
                       where mtd.MonthName == monthName
                       select new WorkOrderEntity()
                       {
                           LastDay = mtd.LastDay,
                           MonthTotalDaysId = mtd.MonthTotalDaysId,
                       }
                     ).FirstOrDefault();
            return res;
        }

        public List<WorkOrderEntity> GetSampleLocationCount(int EnquirySampleID)
        {
            return (from sloc in _dbContext.SampleLocations
                    where sloc.EnquirySampleID == EnquirySampleID

                    select new WorkOrderEntity()
                    {
                        EnquirySampleId = (long)sloc.EnquirySampleID,
                        SampleLocationId = sloc.SampleLocationId,
                        Location = sloc.Location,
                    }).ToList();
        }
        public List<WorkOrderEntity> GetLocation(int EnquirySampleID)
        {
            return (from loc in _dbContext.LocationSampleCollections
                    where loc.EnquirySampleId== EnquirySampleID

                    select new WorkOrderEntity()
                    {
                        EnquirySampleId = loc.EnquirySampleId,
                        LocationSampleCollectionID = loc.LocationSampleCollectionID,
                        LocationSampleName = loc.LocationSampleName,
                    }).ToList();
        }
        public int? GetFrequencyDetails(int FrequencyMasterId)
        {
            var tblfreq = _dbContext.FrequencyMasters.Find(FrequencyMasterId);
            return tblfreq.NumFreq;
        }
        public long AddLocation(EnquiryParameterEntity enquiryParameterEntity)
        {

            try
            {
                var addLoc = new SampleLocation()
                {
                    SampleLocationId = enquiryParameterEntity.SampleLocationId,
                    EnquirySampleID = enquiryParameterEntity.EnquirySampleID,
                    Location = enquiryParameterEntity.Location,
                    IsActive = enquiryParameterEntity.IsActive,
                    EnteredBy = enquiryParameterEntity.EnteredBy,
                    EnteredDate = DateTime.Now

                };
                _dbContext.SampleLocations.Add(addLoc);
                _dbContext.SaveChanges();
                return addLoc.SampleLocationId;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        //public bool AddWorkOrder(WorkOrderEntity workOrder)
        //{
        //    try
        //    {
        //        _dbContext.WorkOrders.Add(new WorkOrder()
        //        {
        //            QuotationId = workOrder.QuotationId,
        //            //ExpectSampleCollDate = (DateTime)workOrder.ExpectSampleCollDate,
        //            WORecieveDate = (DateTime)workOrder.WORecieveDate,
        //            WOEndDate = (DateTime)workOrder.WOEndDate,
        //            WOUpload = workOrder.WOUpload,
        //            FileName= workOrder.FileName,
        //            IsActive = true,
        //            EnteredBy = workOrder.EnteredBy,
        //            EnteredDate = DateTime.UtcNow,
        //            WorkOrderNo = workOrder.WorkOrderNo
        //        });
        //        _dbContext.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        public long AddNotification(string Msg,string RoleName,WorkOrderEntity workOrder)
        {
            try
            {
                var notific = new NotificationDetail()
                {
                    RoleId = null,
                    NotificationName = workOrder.WorkOrderNo,
                    Message = Msg,
                    DateTime = (DateTime)workOrder.WORecieveDate,
                    IsActive = true,
                    EnteredBy = workOrder.EnteredBy,
                    EnteredDate = DateTime.Now,
                    RoleName= RoleName,
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
        public long AddWorkOrder(WorkOrderEntity workOrder)
        {
            try
            {
                var workOrderCustomer = new WorkOrder()
                {
                    QuotationId = workOrder.QuotationId,
                    WORecieveDate = (DateTime)workOrder.WORecieveDate,
                    WOEndDate = (DateTime)workOrder.WOEndDate,
                    ExpectSampleCollDate = (DateTime)workOrder.ExpectSampleCollDate,
                    Duration = workOrder.Duration,
                    WOUpload = workOrder.WOUpload,
                    FileName = workOrder.FileName,
                    WorkOrderNo = workOrder.WorkOrderNo,
                    IsActive = workOrder.IsActive,
                    EnteredBy = workOrder.EnteredBy,
                    EnteredDate = DateTime.Now,
                    CustomerMasterId = workOrder.CustomerMasterId,
                    IsIGST = workOrder.IsIGST,
                    ContractAmendment = workOrder.ContractAmendment,
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
        public bool UpdateWorkOrder(WorkOrderEntity workOrder)
        {
            try
            {
                var tblWorkOrder = _dbContext.WorkOrders.Find(workOrder.WorkOrderId);
                tblWorkOrder.ExpectSampleCollDate = (DateTime)workOrder.ExpectSampleCollDate;
                tblWorkOrder.Duration = workOrder.Duration;
                tblWorkOrder.WORecieveDate = (DateTime)workOrder.WORecieveDate;
                tblWorkOrder.WOEndDate = (DateTime)workOrder.WOEndDate;
                tblWorkOrder.WOUpload = workOrder.WOUpload;
                tblWorkOrder.FileName = workOrder.FileName;
                tblWorkOrder.WorkOrderNo = workOrder.WorkOrderNo;
                tblWorkOrder.ModifiedBy = workOrder.EnteredBy;
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public long AddWOSampleCollectionDate(WorkOrderEntity workOrder)
        {
            try
            {
                //_dbContext.WorkOrderSampleCollectionDates.Add(new WorkOrderSampleCollectionDate()
                //{
                //    WorkOrderId = workOrder.WorkOrderId,
                //    ExpectSampleCollDate = (DateTime)workOrder.ExpectSampleCollDate,
                //    Duration = workOrder.Duration,
                //    Status = 0,
                //    IsActive = true,
                //    EnteredBy = workOrder.EnteredBy,
                //    EnteredDate = DateTime.UtcNow,
                //}); ;
                var woCollDate = new WorkOrderSampleCollectionDate()
                {
                    WorkOrderId = workOrder.WorkOrderId,
                    ExpectSampleCollDate = (DateTime)workOrder.ExpectSampleCollDate,
                    Duration = workOrder.Duration,
                    Status = 0,
                    WOMasterSampleCollectionDateId = workOrder.WOMasterSampleCollectionDateId,
                    IsActive = workOrder.IsActive,
                    EnteredBy = workOrder.EnteredBy,
                    EnteredDate = DateTime.UtcNow,
                    LocationSampleCollectionID= workOrder.LocationSampleCollectionID,
                    LocationSampleName = workOrder.LocationSampleName,
                };
                _dbContext.WorkOrderSampleCollectionDates.Add(woCollDate);
                _dbContext.SaveChanges();
                return woCollDate.WorkOrderSampleCollectionDateId;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public long AddWOMasterCollDate(bool IsActive, long EnquirySampleID)
        {
            try
            {
                var woMasterCollDate = new WOMasterSampleCollectionDate()
                {
                    EnquirySampleID = EnquirySampleID,
                    IsActive = IsActive,
                    EnteredBy = 1,
                    EnteredDate = DateTime.UtcNow,
                };
                _dbContext.WOMasterSampleCollectionDates.Add(woMasterCollDate);
                _dbContext.SaveChanges();
                return woMasterCollDate.WOMasterSampleCollectionDateId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //Need to Remove Later
        //public bool UpdateWOSampleCollectionDate(WorkOrderEntity workOrder)
        //{
        //    try
        //    {
        //        var tblWOSampleCollectionDate = _dbContext.WorkOrderSampleCollectionDates.Find(workOrder.WorkOrderSampleCollectionDateId);
        //        tblWOSampleCollectionDate.ExpectSampleCollDate = (DateTime)workOrder.ExpectSampleCollDate;
        //        tblWOSampleCollectionDate.Duration = workOrder.Duration;
        //       //tblWOSampleCollectionDate.ActualSampleCollDate = (DateTime)workOrder.WOEndDate;
        //        //tblWOSampleCollectionDate.Status = workOrder.Status;
        //        tblWOSampleCollectionDate.ModifiedBy = workOrder.EnteredBy;
        //        _dbContext.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        public WorkOrderEntity GetDetail(int WorkOrderId)
        {
            return (from w in _dbContext.WorkOrders
                    where w.WorkOrderId == WorkOrderId
                    select new WorkOrderEntity()
                    {
                        WorkOrderId = w.WorkOrderId,
                        WORecieveDate = w.WORecieveDate,
                        WorkOrderNo = w.WorkOrderNo,
                    }).FirstOrDefault();
        }
        public WorkOrderEntity GetWorkOrderDetail(int EnquiryId) 
        {
            return (from q in _dbContext.Quotations
                    join e in _dbContext.EnquiryMasters on q.EnquiryId equals e.EnquiryId
                    join c in _dbContext.CustomerMasters on e.CustomerMasterId equals c.CustomerMasterId
                    join w in _dbContext.WorkOrders on q.QuotationId equals w.QuotationId
                    into order
                    from o in order.DefaultIfEmpty()
                    where q.EnquiryId == EnquiryId
                    select new WorkOrderEntity()
                    {
                        WorkOrderId = o.WorkOrderId,
                        QuotationId = q.QuotationId,
                        RegistrationName = c.RegistrationName,
                        CustomerMasterId = c.CustomerMasterId,
                        ExpectSampleCollDate = o.ExpectSampleCollDate,
                        Duration = o.Duration,
                        WORecieveDate = o.WORecieveDate,
                        WOEndDate = o.WOEndDate,
                        WOUpload = o.WOUpload,
                        FileName = o.FileName,
                        QuotedAmount = q.QuotedAmount,
                        WorkOrderNo = o.WorkOrderNo,
                        IsIGST = o.IsIGST,
                        ContractAmendment=o.ContractAmendment,
                    }).FirstOrDefault();
        }
        public WorkOrderEntity GetWOSampleCollectionDateDetail(int WorkOrderId)
        {
            return (from woscd in _dbContext.WorkOrderSampleCollectionDates
                    where woscd.WorkOrderId == WorkOrderId
                    select new WorkOrderEntity()
                    {
                        WorkOrderSampleCollectionDateId = woscd.WorkOrderSampleCollectionDateId,
                        ExpectSampleCollDate = woscd.ExpectSampleCollDate,
                    }).FirstOrDefault();
        }
        public void UpdateEnquirySampleDetail(IList<EnquirySampleEntity> enquirySamples)
        {
            foreach (var es in enquirySamples)
            {
                var tblensample = _dbContext.EnquirySampleDetails.Find(es.EnquirySampleID);
                tblensample.NoOfSample = es.NoOfSample;
                List<int> SampleLocationIds = _dbContext.SampleLocations.Where(e => e.EnquirySampleID == es.EnquirySampleID).Select(e => e.SampleLocationId).ToList();
                var LocSampleDetails = (from loc in _dbContext.LocationSampleCollections
                                        where loc.EnquirySampleId == es.EnquirySampleID
                                        select new
                                        {
                                            loc.EnquirySampleId,
                                            loc.SampleLocationId,
                                            loc.LocationSampleCollectionID,
                                            loc.LocationSampleName,
                                            loc.SampleNameOriginal,
                                        }).ToList();

                if (LocSampleDetails.Count== SampleLocationIds.Count)
                {
                    int j = 0;
                    for (int i = 1; i <= SampleLocationIds.Count; i++)
                    {
                        var locs = _dbContext.LocationSampleCollections.Find(LocSampleDetails[j].LocationSampleCollectionID);
                        locs.EnquirySampleId = LocSampleDetails[j].EnquirySampleId;
                        locs.SampleLocationId = LocSampleDetails[j].SampleLocationId;
                        locs.LocationSampleName = LocSampleDetails[j].LocationSampleName;
                        locs.SampleNameOriginal = LocSampleDetails[j].SampleNameOriginal;
                        _dbContext.SaveChanges();
                        j++;
                    }
                }
                else
                {
                    int j = 0;

                    for (int i = 1; i <= SampleLocationIds.Count; i++)
                    {

                        try
                        {
                            int sampLocId = SampleLocationIds[j];
                            var locationSample = new LocationSampleCollection()
                            {
                                WorkOrderId = es.WorkOrderId,
                                EnquirySampleId = es.EnquirySampleID,
                                SampleLocationId = sampLocId,//for Location 
                                LocationSampleName = es.DisplaySampleName + "/" + i,//for Samplename
                                SampleCollectedBy = 1,
                                //SampleName = es.SampleName,
                                IsActive = true,
                                //SampleNameOriginal = es.SampleName + i,
                                SampleNameOriginal = es.SampleTypeProductCode + "/" + GenerateDisplaySampleName(),
                                EnteredBy = es.EnteredBy,
                                EnteredDate = DateTime.Now
                            };
                            j++;
                            _dbContext.LocationSampleCollections.Add(locationSample);
                            _dbContext.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            //return ex.InnerException.Message;
                        }
                    }
                }
                tblensample.FrequencyMasterId = es.FrequencyMasterId;
                tblensample.TotalCharges = es.TotalCharges;
                tblensample.SampleCollectedBy = 1;//By default LAB
                //  tblensample.Location = es.SampleLocation;               
                _dbContext.SaveChanges();
            }
        }

        ///////////For Sample NAME as per Locations in autoincrement way in sequence////////////

        public string GenerateDisplaySampleName()
        {
            try
            {
                // int Year = DateTime.Now.Year;
                long sampleCount = GetSampleCount(Convert.ToInt32(DateTime.Now.Year), Convert.ToInt32(DateTime.Today.Month));
                long SrNumber = 0;
                string DigitCode = "0000";//4 digit code
                if (sampleCount == 0 || sampleCount == null)
                {
                    SrNumber = 1;//Doubt 
                    DigitCode = "0001";
                    AddSampleNo(1, DateTime.Now.Year, DateTime.Now.Month);
                }
                else
                {
                    SrNumber = sampleCount + 1;

                    if (SrNumber.ToString().Length == 1)
                    {
                        DigitCode = "000" + SrNumber.ToString();
                    }
                    else if (SrNumber.ToString().Length == 2)
                    {
                        DigitCode = "00" + SrNumber.ToString();
                    }
                    else if (SrNumber.ToString().Length == 3)
                    {
                        DigitCode = "0" + SrNumber.ToString();
                    }

                    UpdateSampleNo(SrNumber, DateTime.Now.Year, DateTime.Now.Month);
                }

                var DisplaySampleName = DateTime.Now.ToString("yyMM") + "/" + DigitCode;
                return DisplaySampleName;
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public long GetSampleCount(int Year, int Month)
        {
            var sampleNum = (from e in _dbContext.SampleNumber_ReceiptIncharge
                             where e.Year == Year && e.Month == Month
                             select new SampleNameEntity()
                             {
                                 SampleCount = e.SampleCount,
                             }
             ).FirstOrDefault();
            if (sampleNum != null)
            {
                return sampleNum.SampleCount;
            }
            else { return 0; }

        }
        public long AddSampleNo(long SampleCount, int Year, int Month)
        {
            try
            {
                var sample = new SampleNumber_ReceiptIncharge()
                {
                    SampleCount = SampleCount,
                    Year = Year,
                    Month = Month,
                    IsActive = true,
                    EnteredBy = 1,
                    EnteredDate = DateTime.Now
                };
                _dbContext.SampleNumber_ReceiptIncharge.Add(sample);
                _dbContext.SaveChanges();
                return sample.ID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string UpdateSampleNo(long SampleCount, int Year, int Month)
        {
            try
            {
                var sampleNum = (from e in _dbContext.SampleNumber_ReceiptIncharge
                                 where e.Year == Year && e.Month == Month
                                 select new SampleNameEntity()
                                 {
                                     SampleNumberId = e.ID,
                                 }
                     ).FirstOrDefault();

                var sampleMaster = _dbContext.SampleNumber_ReceiptIncharge.Find(sampleNum.SampleNumberId);
                sampleMaster.SampleCount = SampleCount;
                sampleMaster.Year = Year;
                sampleMaster.Month = Month;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        //////////////////////////For Sample NAME as per Locations in autoinrement way in sequence////////////
        public List<WorkOrderHODEntity> GetWorkOrderList(int LabMasterId, DateTime? FromDate, DateTime? ToDate)
        {
            return (from w in _dbContext.WorkOrders
                    join q in _dbContext.Quotations on w.QuotationId equals q.QuotationId
                    join e in _dbContext.EnquiryMasters on q.EnquiryId equals e.EnquiryId
                    join c in _dbContext.CustomerMasters on e.CustomerMasterId equals c.CustomerMasterId
                    join s in _dbContext.StatusMasters on e.StatusId equals s.StatusId
                    into workOrder
                    from wo in workOrder.DefaultIfEmpty()
                    where e.LabMasterId == LabMasterId
                    && ((FromDate == null && ToDate == null) || (e.EnquiryOn >= FromDate && e.EnquiryOn <= ToDate)) && ((wo.StatusCode.Trim() == "WOGenerate") || (wo.StatusCode.Trim() == "WOApproved") || (wo.StatusCode.Trim() == "WORejected"))//IsActive Enquiry Status
                    select new WorkOrderHODEntity()
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
                        EnteredBy = w.EnteredBy,
                        IsIGST=w.IsIGST
                    }).OrderByDescending(e => e.EnquiryId).ToList();
        }

        public void WorkOrderApprove(int WorkOrderId, long EnquiryId, int iStatusId, int UserId)
        {
            try
            {
                //var WorkOrder = _dbContext.WorkOrders.Find(WorkOrderId);
                //WorkOrder.AssignedToId = AssignToId;
                //WorkOrder.ModifiedBy = UserId;
                //_dbContext.SaveChanges();
                List<long> EnquirySampleIds = new List<long>();
                List<long> EnquiryDetailIds = _dbContext.EnquiryDetails.Where(e => e.EnquiryId == EnquiryId).Select(e => e.EnquiryDetailId).ToList();
                var EnquirySample = _dbContext.EnquirySampleDetails.Where(e => EnquiryDetailIds.Contains(e.EnquiryDetailId)).ToList();

                for (int i = 0; i < EnquiryDetailIds.Count; i++)
                {
                    //   int j = 0;
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
                            WorkOrderSampleCollectionDateId= samp.WorkOrderSampleCollectionDateId,
                            LocationSampleCollectionID= samp.LocationSampleCollectionID,
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
        public void AssignSTL(int WorkOrderId, int AssignToId, int UserId)
        {
            try
            {
                var WorkOrder = _dbContext.WorkOrders.Find(WorkOrderId);
                WorkOrder.AssignedToId = AssignToId;
                WorkOrder.ModifiedBy = UserId;
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {

            }
        }
        public void WorkOrderReject(int WorkOrderId, string Remark, int UserId)
        {
            try
            {
                var WorkOrder = _dbContext.WorkOrders.Find(WorkOrderId);
                WorkOrder.Remark = Remark;
                WorkOrder.ModifiedBy = UserId;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public List<ParameterPCBEntity> GetParameterPCBList(long EnquirySampleId)
        {
            return (from EPD in _dbContext.EnquiryParameterDetails
                    join PM in _dbContext.ParameterMappings on EPD.ParameterMappingId equals PM.ParameterMappingId
                    join P in _dbContext.ParameterMasters on PM.ParameterMasterId equals P.ParameterMasterId
                    select new ParameterPCBEntity()
                    {
                        EnquiryParameterDetailId = EPD.EnquiryParameterDetailID,
                        EnquirySampleId = EPD.EnquirySampleID,
                        ParameterMappingId = PM.ParameterMappingId,
                        PCBLimit = EPD.PCBLimit,
                        ParameterMasterId = P.ParameterMasterId,
                        ParameterName = P.ParameterName
                    }).ToList();
        }
        public void UpdateParameterPCBLimit(List<ParameterPCBEntity> PCBLimits, int UserId)
        {
            foreach (var pcb in PCBLimits)
            {
                var EPDetails = _dbContext.EnquiryParameterDetails.Find(pcb.EnquiryParameterDetailId);
                EPDetails.PCBLimit = pcb.PCBLimit;
                EPDetails.ModifiedBy = UserId;
                _dbContext.SaveChanges();
            }
        }
        public string UpdateRemark(long QuotationLogId, string Remark)
        {
            try
            {
                var tblQuotationLog = _dbContext.QuotationLogs.Find(QuotationLogId);
                tblQuotationLog.Remarks = Remark;
                tblQuotationLog.RevisedOn = DateTime.UtcNow;
                tblQuotationLog.IsRevised = true;

                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public UserRoleEntity GetUserRoles(string strUserName)
        {
            try
            {
                return (from ur in _dbContext.UserRoles
                        join u in _dbContext.UserMasters on ur.UserMasterId equals u.UserMasterID
                        join rm in _dbContext.RoleMasters on ur.RoleId equals rm.RoleId
                        into role
                        from r in role.DefaultIfEmpty()
                        where u.UserName == strUserName
                        select new UserRoleEntity()
                        {
                            UserRoleId = ur.UserRoleId,
                            UserMasterId = ur.UserMasterId,
                            RoleId = ur.RoleId,
                            RoleName = r.RoleName,
                          
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<WorkOrderHODEntity> GetTRF_WOList(int LabMasterId, DateTime? FromDate, DateTime? ToDate)
        {
            return (from sc in _dbContext.SampleCollections
                        //join mdc in _dbContext.ModeOfCommunications on w.ModeOfCommunicationId equals mdc.ModeOfCommunicationId
                    join w in _dbContext.WorkOrders on sc.WorkOrderID equals w.WorkOrderId
                    join ctm in _dbContext.CustomerMasters on w.CustomerMasterId equals ctm.CustomerMasterId
                    join es in _dbContext.StatusMasters on w.StatusId equals es.StatusId
                    
                    where w.LabMasterId == LabMasterId && w.OVC == true

                    //where w.LabMasterId == LabMasterId && w.IsActive && w.QuotationId == null && ((FromDate == null && ToDate == null) || (w.WORecieveDate >= FromDate && w.WORecieveDate <= ToDate)) || w.OVC == true
            select new WorkOrderHODEntity()
                    {
                        RegistrationName = ctm.RegistrationName,
                        WorkOrderNo = w.WorkOrderNo,
                        SampleCollectionId = sc.SampleCollectionId,
                        WorkOrderId = w.WorkOrderId,
                        CurrentStatus = es.StatusName,
                        AssignToId = w.AssignedToId,
                        Remarks = w.Remark,
                        WORecieveDate = w.WORecieveDate,
                        WOUpload = w.WOUpload,
                        FileName = w.FileName,
                        EnteredBy = w.EnteredBy,
                        IsIGST = w.IsIGST
                    }).Distinct().OrderByDescending(w => w.WorkOrderId).ToList();
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
