using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Arrival;
using LIMS_DEMO.Core.Repository;

namespace LIMS_DEMO.DAL.Arrival
{
    public class SampleArrivalDAL : ISampleArrival
    {
        readonly LIMSEntities _dbContext;
        public SampleArrivalDAL()
        {
            _dbContext = new LIMSEntities();
        }

        //disposal Save 
        public long SaveDisposalData(DisposalEntity disposalEntity)
        {
            try
            {
                Lims_DisposalCollection obj = new Lims_DisposalCollection()
                {
                    SampleCollectionId = disposalEntity.SampleCollectionId,
                    SampleTypeProductId = disposalEntity.SampleTypeProductId,
                    SampleTypeProductName = disposalEntity.SampleType,
                    SampleCode = disposalEntity.SampleCode,
                    DateofRecieptofSample = disposalEntity.DateofRecieptofSample,
                    DateofReporting = disposalEntity.DateofReporting,
                    DateofDisposal = disposalEntity.DateofDisposal,
                    DisposalMethod = disposalEntity.DisposalMethod,
                    DisposedBy = disposalEntity.DisposedBy,
                    SuperwisedBy = disposalEntity.SuperwisedBy,
                    Remarks = disposalEntity.Remark
                };
                _dbContext.Lims_DisposalCollection.Add(obj);
                _dbContext.SaveChanges();

                var scoll = _dbContext.SampleCollections.Find(disposalEntity.SampleCollectionId);
                scoll.IsDisposed = true;
                _dbContext.SaveChanges();

                return obj.DisposalCollectionId;
            }
            catch (Exception ex)
            {
                
                return 0;
            }
        }
        public List<DisposalEntity> GetDisposalData()
        {
            try
            {
                return (from dc in _dbContext.Lims_DisposalCollection
                        select new DisposalEntity()
                        {
                            DisposalCollectionId = dc.DisposalCollectionId,
                            SampleType = dc.SampleTypeProductName,
                            SampleCode = dc.SampleCode,
                            DateofRecieptofSample = dc.DateofRecieptofSample,
                            DateofReporting = dc.DateofReporting,
                            DateofDisposal = dc.DateofDisposal,
                            DisposalMethod = dc.DisposalMethod,
                            DisposedBy = dc.DisposedBy,
                            SuperwisedBy = dc.SuperwisedBy,
                            Remark = dc.Remarks,
                        }).OrderByDescending(dc => dc.DisposalCollectionId).ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<DisposalEntity> GetCode(int SampleTypeProductId)
        {
            try
            {
                return (from loc in _dbContext.LocationSampleCollections
                        join sc in _dbContext.SampleCollections on loc.LocationSampleCollectionID equals sc.LocationSampleCollectionID
                        join esd in _dbContext.EnquirySampleDetails on sc.EnquirySampleID equals esd.EnquirySampleID
                        join ed in _dbContext.EnquiryDetails on esd.EnquiryDetailId equals ed.EnquiryDetailId
                        join sm in _dbContext.StatusMasters on sc.StatusId equals sm.StatusId
                        where ed.SampleTypeProductId == SampleTypeProductId && sm.StatusName == "Approved" && sc.IsReturnedOrIsRetained == "Retained" && sc.IsDisposed == null
                        select new DisposalEntity()
                        {
                            SampleName = loc.SampleNameOriginal,
                            SampleCollectionId = sc.SampleCollectionId,
                        }).Distinct().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DisposalEntity GetDates(long SampleCollectionId)
        {
            try
            {
                return (from sc in _dbContext.SampleCollections
                        join arc in _dbContext.ARCs on sc.SampleCollectionId equals arc.SampleCollectionId
                        where sc.SampleCollectionId == SampleCollectionId
                        select new DisposalEntity()
                        {
                            DateReciept = sc.SampleApprovedDate,
                            DateReporting = arc.ActionDate,
                        }
                 ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SampleArrivalEntity> GetSampleReturnedList()
        {
            try
            {
                return (from arc in _dbContext.ARCs
                        join scoll in _dbContext.SampleCollections on arc.SampleCollectionId equals scoll.SampleCollectionId
                        join loc in _dbContext.LocationSampleCollections on scoll.LocationSampleCollectionID equals loc.LocationSampleCollectionID
                        join sc in _dbContext.SampleLocations on loc.SampleLocationId equals sc.SampleLocationId
                        join es in _dbContext.EnquirySampleDetails on scoll.EnquirySampleID equals es.EnquirySampleID
                        join sts in _dbContext.StatusMasters on scoll.StatusId equals sts.StatusId
                        join wo in _dbContext.WorkOrders on scoll.WorkOrderID equals wo.WorkOrderId

                        join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId
                        join stp in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stp.SampleTypeProductId
                        //join em in _dbContext.EnquiryMasters on ed.EnquiryId equals em.EnquiryId
                        join em in _dbContext.EnquiryMasters on ed.EnquiryId equals em.EnquiryId into em
                        from emt in em.DefaultIfEmpty()
                            //join ctm in _dbContext.CustomerMasters on em.CustomerMasterId equals ctm.CustomerMasterId
                        join ctm in _dbContext.CustomerMasters on emt.CustomerMasterId equals ctm.CustomerMasterId into ctm
                        from a in ctm.DefaultIfEmpty()

                        join ctmwo in _dbContext.CustomerMasters on wo.CustomerMasterId equals ctmwo.CustomerMasterId into temp
                        from last in temp.DefaultIfEmpty()

                        where scoll.IsReturnedOrIsRetained== "Returned"
                        select new SampleArrivalEntity()
                        {
                            ARCId = arc.ARCId,
                            SampleTypeProductId = (Int32)ed.SampleTypeProductId,
                            SampleTypeProductName = stp.SampleTypeProductName,
                            SampleNameOriginal = loc.SampleNameOriginal,
                            ActionDate = arc.ActionDate == null? null: arc.ActionDate,
                            ReturnedDate=scoll.ReturnedDate,
                            ReturnedRemark=scoll.ReturnedRemark,
                            ProbableDateOfReport=scoll.ProbableDateOfReport,
                            IsReturnedOrIsRetained=scoll.IsReturnedOrIsRetained,
                            SampleCollectionId = scoll.SampleCollectionId,
                            EnquirySampleID = scoll.EnquirySampleDetail.EnquirySampleID,
                            WorkOrderID = scoll.WorkOrderID,
                            EnquiryId = emt.EnquiryId == null ? 0 : emt.EnquiryId,
                            ContactNO = a.ContactMobileNo == "" || a.ContactMobileNo == null ? last.ContactMobileNo : a.ContactMobileNo,
                            CustomerName = a.RegistrationName == "" || a.RegistrationName == null ? last.RegistrationName : a.RegistrationName,
                            StatusId = scoll.StatusMaster.StatusId,
                            CurrentStatus = sts.StatusName,
                            StatusCode = sts.StatusCode,
                        }).OrderByDescending(scoll => scoll.SampleCollectionId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public long AddNotification(string Msg, string RoleName, SampleArrivalEntity samplearrivalEntity)
        {
            try
            {
                var notific = new NotificationDetail()
                {
                    RoleId = null,
                    NotificationName = samplearrivalEntity.SampleNameOriginal == null ? samplearrivalEntity.SampleName : samplearrivalEntity.SampleNameOriginal,
                    Message = Msg,
                    DateTime = samplearrivalEntity.ProbableDateOfReport == null ? DateTime.Now : samplearrivalEntity.ProbableDateOfReport,
                    IsActive = true,
                    EnteredBy = samplearrivalEntity.EnteredBy,
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
        public List<SampleArrivalEntity> GetDisciplineList()
        {
            try
            {
                return (from dm in _dbContext.DisciplineMasters
                       
                        select new SampleArrivalEntity()
                        {
                           DisciplineId=dm.DisciplineId,
                           Discipline = dm.Discipline,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string GetParameterByDiscipline(int EnquirySampleID, int DisciplineId)
        {
            try
            {
                if (DisciplineId == 0)
                {
                    var par = (from epd in _dbContext.EnquiryParameterDetails
                               join pm in _dbContext.ParameterMappings on epd.ParameterMappingId equals pm.ParameterMappingId
                               into epm
                               from a in epm.DefaultIfEmpty()
                               join p in _dbContext.ParameterMasters on a.ParameterMasterId equals p.ParameterMasterId
                               into ep
                               from pr in ep.DefaultIfEmpty()
                               join pmgm in _dbContext.ParameterGroupMasters on a.ParameterGroupId equals pmgm.ParameterGroupId
                               join dis in _dbContext.DisciplineMasters on pmgm.DisciplineId equals dis.DisciplineId
                               into d
                               from dis in d.DefaultIfEmpty()
                               where epd.EnquirySampleID == EnquirySampleID
                               select new
                               {
                                   pr.ParameterName,
                                   pr.ParameterMasterId,
                                   epd.ParameterMappingId,
                                   dis.Discipline,
                                   dis.DisciplineId,
                               }).ToList();
                    return string.Join(",", par.Select(p => p.ParameterName));
                }
                else
                {
                    var par = (from epd in _dbContext.EnquiryParameterDetails
                               join pm in _dbContext.ParameterMappings on epd.ParameterMappingId equals pm.ParameterMappingId
                               into epm
                               from a in epm.DefaultIfEmpty()
                               join p in _dbContext.ParameterMasters on a.ParameterMasterId equals p.ParameterMasterId
                               into ep
                               from pr in ep.DefaultIfEmpty()
                               join pmgm in _dbContext.ParameterGroupMasters on a.ParameterGroupId equals pmgm.ParameterGroupId
                               join dis in _dbContext.DisciplineMasters on pmgm.DisciplineId equals dis.DisciplineId
                               into d
                               from dis in d.DefaultIfEmpty()
                               where epd.EnquirySampleID == EnquirySampleID
                               select new
                               {
                                   pr.ParameterName,
                                   pr.ParameterMasterId,
                                   epd.ParameterMappingId,
                                   dis.Discipline,
                                   dis.DisciplineId,
                               }).ToList();
                    return string.Join(",", par.Where(p => p.DisciplineId == DisciplineId).Select(p => p.ParameterName));
                }
               
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public List<SampleArrivalEntity> GetArrivalParameterUnitList(int EnquirySampleID)
        {
            try
            {
                return (from epd in _dbContext.EnquiryParameterDetails
                        join um in _dbContext.UnitMasters on epd.UnitId equals um.UnitId

                        join pm in _dbContext.ParameterMappings on epd.ParameterMappingId equals pm.ParameterMappingId
                        join tm in _dbContext.TestMethods on pm.TestMethodId equals tm.TestMethodId

                        join pmaster in _dbContext.ParameterMasters on pm.ParameterMasterId equals pmaster.ParameterMasterId
                        join pmgm in _dbContext.ParameterGroupMasters on pm.ParameterGroupId equals pmgm.ParameterGroupId

                        join dis in _dbContext.DisciplineMasters on pmgm.DisciplineId equals dis.DisciplineId
                        where epd.EnquirySampleID == EnquirySampleID
                        select new SampleArrivalEntity()
                        {
                            EnquiryParameterDetailID = epd.EnquiryParameterDetailID,
                            EnquirySampleID = epd.EnquirySampleID,
                            ParameterMappingId = epd.ParameterMappingId,
                            UnitId = (Int32)epd.UnitId,
                            Unit = um.Unit,
                            ParameterMasterId = pm.ParameterMasterId,
                            ParameterName = pmaster.ParameterName,
                            ParameterGroupId = pm.ParameterGroupId,
                            DisciplineId = pmgm.DisciplineId,
                            Discipline = pmgm.DisciplineMaster.Discipline,
                            TestMethodId = pm.TestMethodId,
                            TestMethodName = tm.TestMethod1,
                            InField = pm.InField,
                            IsNABLAccredited = pm.IsNABLAccredited,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public SampleArrivalEntity GetInFieldIsNabl(int EnquirySampleID, int ParameterMasterId,int TestMethodId)
        {
            try
            {
                return (from epd in _dbContext.EnquiryParameterDetails
                        join um in _dbContext.UnitMasters on epd.UnitId equals um.UnitId

                        join pm in _dbContext.ParameterMappings on epd.ParameterMappingId equals pm.ParameterMappingId
                        join tm in _dbContext.TestMethods on pm.TestMethodId equals tm.TestMethodId

                        join pmaster in _dbContext.ParameterMasters on pm.ParameterMasterId equals pmaster.ParameterMasterId
                        join pmgm in _dbContext.ParameterGroupMasters on pm.ParameterGroupId equals pmgm.ParameterGroupId

                        join dis in _dbContext.DisciplineMasters on pmgm.DisciplineId equals dis.DisciplineId
                        where epd.EnquirySampleID == EnquirySampleID && pm.ParameterMasterId == ParameterMasterId && pm.TestMethodId == TestMethodId
                        select new SampleArrivalEntity()
                        {
                            InField = pm.InField,
                            IsNABLAccredited = pm.IsNABLAccredited,
                            TestMethodName = tm.TestMethod1,
                            ParameterName = pmaster.ParameterName,
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SampleArrivalEntity> GetTestMethod(int EnquirySampleID,int ParameterMasterId)
        {
            try
            {
                return (from epd in _dbContext.EnquiryParameterDetails
                        join um in _dbContext.UnitMasters on epd.UnitId equals um.UnitId

                        join pm in _dbContext.ParameterMappings on epd.ParameterMappingId equals pm.ParameterMappingId
                        join tm in _dbContext.TestMethods on pm.TestMethodId equals tm.TestMethodId

                        join pmaster in _dbContext.ParameterMasters on pm.ParameterMasterId equals pmaster.ParameterMasterId
                        join pmgm in _dbContext.ParameterGroupMasters on pm.ParameterGroupId equals pmgm.ParameterGroupId

                        join dis in _dbContext.DisciplineMasters on pmgm.DisciplineId equals dis.DisciplineId
                        where epd.EnquirySampleID == EnquirySampleID && pm.ParameterMasterId == ParameterMasterId
                        select new SampleArrivalEntity()
                        {
                            TestMethodId = pm.TestMethodId,
                            TestMethodName = tm.TestMethod1,
                            UnitId = (Int32)epd.UnitId,
                            Unit = um.Unit,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string GetReportULRNumber(int EnquiryId, int SampleTypeProductId, int EnquirySampleID, int WorkOrderID)
        {
            try
            {
                var ulr = _dbContext.SampleCollections.Where(e => e.EnquirySampleID == EnquirySampleID && e.ULRNo != null).Select(e => e.ULRNo).ToList();
                //return (from scoll in _dbContext.SampleCollections
                //            //join es in _dbContext.EnquirySampleDetails on scoll.EnquirySampleID equals es.EnquirySampleID
                //            //join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId
                //        where /*ed.EnquiryId == EnquiryId && ed.SampleTypeProductId == SampleTypeProductId &&*/ scoll.EnquirySampleID == EnquirySampleID && scoll.WorkOrderID == WorkOrderID
                //        select new SampleArrivalEntity()
                //        {
                //            ULRNo = scoll.ULRNo,
                //            RequestNo = scoll.RequestNo,
                //        }).FirstOrDefault();


                string strULRNo = string.Empty;
                if (ulr != null) {
                    if (ulr.Count > 0) { 
                    strULRNo = ulr[0];
                    }
                }
                return strULRNo;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SampleArrivalEntity> GetReceiverList(int UserMasterID)
        {
            try
            {
                return (from arc in _dbContext.ARCs
                        join userrole in _dbContext.UserRoles on arc.UserRoleId equals userrole.UserRoleId
                        join scoll in _dbContext.SampleCollections on arc.SampleCollectionId equals scoll.SampleCollectionId
                        join loc in _dbContext.LocationSampleCollections on scoll.LocationSampleCollectionID equals loc.LocationSampleCollectionID
                        join sc in _dbContext.SampleLocations on loc.SampleLocationId equals sc.SampleLocationId
                        join es in _dbContext.EnquirySampleDetails on scoll.EnquirySampleID equals es.EnquirySampleID
                        join sts in _dbContext.StatusMasters on scoll.StatusId equals sts.StatusId
                        join wo in _dbContext.WorkOrders on scoll.WorkOrderID equals wo.WorkOrderId

                        join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId
                        join stp in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stp.SampleTypeProductId
                        //join em in _dbContext.EnquiryMasters on ed.EnquiryId equals em.EnquiryId

                        join em in _dbContext.EnquiryMasters on ed.EnquiryId equals em.EnquiryId into em
                        from emt in em.DefaultIfEmpty()

                            //join ctm in _dbContext.CustomerMasters on em.CustomerMasterId equals ctm.CustomerMasterId
                        join ctm in _dbContext.CustomerMasters on emt.CustomerMasterId equals ctm.CustomerMasterId into ctm
                        from a in ctm.DefaultIfEmpty()

                        join ctmwo in _dbContext.CustomerMasters on wo.CustomerMasterId equals ctmwo.CustomerMasterId into temp
                        from last in temp.DefaultIfEmpty()

                        where userrole.UserMasterId == UserMasterID

                        select new SampleArrivalEntity()
                        {
                            ARCId = arc.ARCId,
                            SampleCollectionId = scoll.SampleCollectionId,
                            LocationSampleCollectionID = (Int32)scoll.LocationSampleCollectionID,
                            SampleTypeProductId = (Int32)ed.SampleTypeProductId,
                            SampleTypeProductName = stp.SampleTypeProductName,
                            SampleNameOriginal = loc.SampleNameOriginal,
                            EnquirySampleID = scoll.EnquirySampleDetail.EnquirySampleID,
                            EnquiryDetailId = es.EnquiryDetailId,
                            SampleNo = scoll.SampleNo,//to be Changed to SampleName wrt Iteration Number,
                            SampleName = es.SampleName,
                            //ULRNo = scoll.ULRNo,//doubt for backend storage only
                            //FieldDeterminationId = scoll.FieldDeterminationId,//Doubt
                            RequestNo = scoll.RequestNo,
                            SampleLocation = sc.Location,
                            CollectionDate = scoll.CollectionDate,
                            //EnquiryId = em.EnquiryId,
                            EnquiryId = emt.EnquiryId == null ? 0 : emt.EnquiryId,
                            //ContactNO = ctm.ContactMobileNo,
                            ContactNO = a.ContactMobileNo == "" || a.ContactMobileNo == null ? last.ContactMobileNo : a.ContactMobileNo,
                            //CustomerName = ctm.RegistrationName,
                            CustomerName = a.RegistrationName == "" || a.RegistrationName == null ? last.RegistrationName : a.RegistrationName,
                            StatusId = scoll.StatusMaster.StatusId,
                            CurrentStatus = sts.StatusName,
                            StatusCode = sts.StatusCode,
                            //MatrixId = ed.MatrixId,
                            //MatrixName = ed.MatrixMaster.MatrixName,
                            WorkOrderID = wo.WorkOrderId,
                        }).OrderByDescending(wo => wo.WorkOrderID).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SampleArrivalEntity> GetArrivalList()
        {
            try
            {
                return (from arc in _dbContext.ARCs
                        join scoll in _dbContext.SampleCollections on arc.SampleCollectionId equals scoll.SampleCollectionId
                        join loc in _dbContext.LocationSampleCollections on scoll.LocationSampleCollectionID equals loc.LocationSampleCollectionID
                        join sc in _dbContext.SampleLocations on loc.SampleLocationId equals sc.SampleLocationId
                        join es in _dbContext.EnquirySampleDetails on scoll.EnquirySampleID equals es.EnquirySampleID
                        join sts in _dbContext.StatusMasters on scoll.StatusId equals sts.StatusId
                        join wo in _dbContext.WorkOrders on scoll.WorkOrderID equals wo.WorkOrderId

                        join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId
                        join stp in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stp.SampleTypeProductId
                        //join em in _dbContext.EnquiryMasters on ed.EnquiryId equals em.EnquiryId
                        join em in _dbContext.EnquiryMasters on ed.EnquiryId equals em.EnquiryId into em
                        from emt in em.DefaultIfEmpty()
                            //join ctm in _dbContext.CustomerMasters on em.CustomerMasterId equals ctm.CustomerMasterId
                        join ctm in _dbContext.CustomerMasters on emt.CustomerMasterId equals ctm.CustomerMasterId into ctm
                        from a in ctm.DefaultIfEmpty()

                        join ctmwo in _dbContext.CustomerMasters on wo.CustomerMasterId equals ctmwo.CustomerMasterId into temp
                        from last in temp.DefaultIfEmpty()

                        select new SampleArrivalEntity()
                        {
                            ARCId = arc.ARCId,
                            LocationSampleCollectionID = (Int32)scoll.LocationSampleCollectionID,
                            SampleTypeProductId = (Int32)ed.SampleTypeProductId,
                            SampleTypeProductName = stp.SampleTypeProductName,
                            SampleLocation = sc.Location,
                            SampleNameOriginal = loc.SampleNameOriginal,
                            SampleCollectionId = scoll.SampleCollectionId,
                            EnquirySampleID = scoll.EnquirySampleDetail.EnquirySampleID,
                            EnquiryDetailId = es.EnquiryDetailId,
                            SampleNo = scoll.SampleNo,//to be Changed to SampleName wrt Iteration Number,
                            SampleName = es.SampleName,
                            WorkOrderID = scoll.WorkOrderID,
                            //ULRNo = scoll.ULRNo,//doubt for backend storage only
                            //FieldDeterminationId = scoll.FieldDeterminationId,//Doubt
                            RequestNo = scoll.RequestNo,
                            CollectionDate = scoll.CollectionDate,
                            //EnquiryId = em.EnquiryId,
                            EnquiryId = emt.EnquiryId == null ? 0 : emt.EnquiryId,
                            //ContactNO = ctm.ContactMobileNo,
                            ContactNO = a.ContactMobileNo == "" || a.ContactMobileNo == null ? last.ContactMobileNo : a.ContactMobileNo,
                            //CustomerName = ctm.RegistrationName,
                            CustomerName = a.RegistrationName == "" || a.RegistrationName == null ? last.RegistrationName : a.RegistrationName,
                            StatusId = scoll.StatusMaster.StatusId,
                            CurrentStatus = sts.StatusName,
                            StatusCode = sts.StatusCode,
                            //MatrixId = ed.MatrixId,
                            MatrixName = ed.MatrixMaster.MatrixName,

                        }).OrderByDescending(scoll => scoll.SampleCollectionId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<FinalReportEntity> GetFinalReportsList()
        {
            try
            {
                return (from scoll in _dbContext.SampleCollections
                        join loc in _dbContext.LocationSampleCollections on scoll.LocationSampleCollectionID equals loc.LocationSampleCollectionID
                        join sc in _dbContext.SampleLocations on loc.SampleLocationId equals sc.SampleLocationId
                        join es in _dbContext.EnquirySampleDetails on scoll.EnquirySampleID equals es.EnquirySampleID
                        join wscd in _dbContext.WorkOrderSampleCollectionDates on scoll.WorkOrderSampleCollectionDateId equals wscd.WorkOrderSampleCollectionDateId
                        join womaster in _dbContext.WOMasterSampleCollectionDates on wscd.WOMasterSampleCollectionDateId equals womaster.WOMasterSampleCollectionDateId

                        join sts in _dbContext.StatusMasters on scoll.StatusId equals sts.StatusId
                        join wo in _dbContext.WorkOrders on scoll.WorkOrderID equals wo.WorkOrderId
                       

                        join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId
                        //join stp in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stp.SampleTypeProductId

                        join epd in _dbContext.EnquiryParameterDetails on es.EnquirySampleID equals epd.EnquirySampleID
                        join pmp in _dbContext.ParameterMappings on epd.ParameterMappingId equals pmp.ParameterMappingId
                        join pm in _dbContext.ParameterGroupMasters on pmp.ParameterGroupId equals pm.ParameterGroupId
                        join stp in _dbContext.SampleTypeProductMasters on pm.SampleTypeProductId equals stp.SampleTypeProductId
                        join pgm in _dbContext.ProductGroupMasters on pm.ProductGroupId equals pgm.ProductGroupId
                        join sgm in _dbContext.SubGroupMasters on pm.SubGroupId equals sgm.SubGroupId
                        join mm in _dbContext.MatrixMasters on pm.MatrixId equals mm.MatrixId
                        // join em in _dbContext.EnquiryMasters on ed.EnquiryId equals em.EnquiryId
                        join em in _dbContext.EnquiryMasters on ed.EnquiryId equals em.EnquiryId into em
                        from emt in em.DefaultIfEmpty()
                            //join ctm in _dbContext.CustomerMasters on em.CustomerMasterId equals ctm.CustomerMasterId
                        join ctm in _dbContext.CustomerMasters on emt.CustomerMasterId equals ctm.CustomerMasterId into ctm
                        from a in ctm.DefaultIfEmpty()

                        join ctmwo in _dbContext.CustomerMasters on wo.CustomerMasterId equals ctmwo.CustomerMasterId into temp
                        from last in temp.DefaultIfEmpty()
                        where  sts.StatusCode == "APPROVED" 
                        select new FinalReportEntity()
                        {  
                            WOMasterSampleCollectionDateId = (Int32)wscd.WOMasterSampleCollectionDateId,
                            LocationSampleCollectionID = (Int32)scoll.LocationSampleCollectionID,
                            SampleApprovedDate = scoll.SampleApprovedDate==null? null: scoll.SampleApprovedDate,
                            SampleTypeProductId = (Int32)ed.SampleTypeProductId,
                            SampleTypeProductName = stp.SampleTypeProductName,
                            SampleLocation = sc.Location,
                            SampleCollectionId = scoll.SampleCollectionId,
                            EnquirySampleID = scoll.EnquirySampleDetail.EnquirySampleID,
                            EnquiryDetailId = es.EnquiryDetailId,
                            SampleNo = scoll.SampleNo,
                            MatrixName = mm.MatrixName,
                            SampleName = loc.SampleNameOriginal,
                            //EnquiryId = em.EnquiryId,
                            EnquiryId = emt.EnquiryId == null ? 0 : emt.EnquiryId,
                            //ContactNO = ctm.ContactMobileNo,
                            ContactNO = a.ContactMobileNo == "" || a.ContactMobileNo == null ? last.ContactMobileNo : a.ContactMobileNo,
                            //CustomerName = ctm.RegistrationName,
                            CustomerName = a.RegistrationName == "" || a.RegistrationName == null ? last.RegistrationName : a.RegistrationName,
                            StatusId = scoll.StatusMaster.StatusId,
                            CurrentStatus = sts.StatusName,
                            StatusCode = sts.StatusCode,
                            WorkOrderNo = scoll.WorkOrder.WorkOrderNo,
                            //MatrixId = ed.MatrixId,
                            //MatrixName = ed.MatrixMaster.MatrixName,

                        }).OrderByDescending(scoll => scoll.LocationSampleCollectionID).Distinct().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SampleArrivalEntity> GetArrivalQtyPreservativeList(int SampleCollectionId)
        {
            try
            {
                return (from qp in _dbContext.QuantityPreservatives
                        join sqm in _dbContext.SampleQtyMasters on qp.SampleQtyId equals sqm.SampleQtyId
                        where qp.SampleCollectionId == SampleCollectionId
                        select new SampleArrivalEntity()
                        {
                            QtyPreservativeId = qp.QtyPreservativeId,
                            SampleCollectionId = qp.SampleCollectionId,
                            SampleQtyId = qp.SampleQtyId,
                            ISGivenPreservative = qp.ISGivenPreservative,
                            SampleQty = sqm.SampleQty,
                            Preservation = sqm.Preservation,
                            No = qp.No,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SampleArrivalEntity> GetCollectionDevicesList(int SampleCollectionId)
        {
            try
            {
                return (from scd in _dbContext.SampleCollectionDevices
                        join sdm in _dbContext.SampleDeviceMasters on scd.SampleDeviceId equals sdm.SampleDeviceId
                        where scd.SampleCollectionId == SampleCollectionId
                        select new SampleArrivalEntity()
                        {
                            SampleCollectionDevicesId = scd.SampleCollectionDevicesId,
                            SampleCollectionId = scd.SampleCollectionId,
                            SampleDeviceId = scd.SampleDeviceId,
                            SampleDevice = sdm.SampleDevice,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //To be Removed not in use
        public string AddSampleArrival(SampleArrivalEntity samplearrivalEntity)
        {
            try
            {

                _dbContext.ARCs.Add(new ARC()
                {
                    ARCId = samplearrivalEntity.ARCId,
                    SampleCollectionId = samplearrivalEntity.SampleCollectionId,
                    UserRoleId = samplearrivalEntity.UserRoleId,
                    //ActionDate = samplearrivalEntity.ActionDate,
                    IsActive = samplearrivalEntity.IsActive,
                    EnteredBy = samplearrivalEntity.EnteredBy,
                    EnteredDate = DateTime.Now,
                });
                _dbContext.SaveChanges();

                var sampleCollection = _dbContext.SampleCollections.Find(samplearrivalEntity.SampleCollectionId);
                sampleCollection.RequestNo = samplearrivalEntity.RequestNo;
                sampleCollection.ULRNo = samplearrivalEntity.ULRNo;
                sampleCollection.IsReturnedOrIsRetained = samplearrivalEntity.IsReturnedOrIsRetained;
                sampleCollection.StatutoryLimits = samplearrivalEntity.StatutoryLimits;
                sampleCollection.SubContractedParameters = samplearrivalEntity.SubContractedParameters;
                sampleCollection.AckRemarks = samplearrivalEntity.AckRemarks;
               _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }

        }
        public string UpdateSampleArrival(SampleArrivalEntity samplearrivalEntity)
        {
            try
            {
                
                var arc = _dbContext.ARCs.Find(samplearrivalEntity.ARCId);
                arc.ARCId = samplearrivalEntity.ARCId;
                arc.SampleCollectionId = samplearrivalEntity.SampleCollectionId;
                //arc.UserRoleId = samplearrivalEntity.UserRoleId;
                arc.ActionDate = samplearrivalEntity.ActionDate;
                arc.IsActive = samplearrivalEntity.IsActive;
                arc.EnteredBy = samplearrivalEntity.EnteredBy;
                arc.EnteredDate = DateTime.Now;
                _dbContext.SaveChanges();

                var sampleCollection = _dbContext.SampleCollections.Find(samplearrivalEntity.SampleCollectionId);
                if (samplearrivalEntity.IsReturnedOrIsRetained == "Retained")
                {
                    sampleCollection.SampleCollectionId = samplearrivalEntity.SampleCollectionId;
                    sampleCollection.RequestNo = samplearrivalEntity.RequestNo;
                    sampleCollection.ULRNo = samplearrivalEntity.ULRNo;
                    sampleCollection.IsSampleIntact = samplearrivalEntity.IsSampleIntact;
                    sampleCollection.ProbableDateOfReport = samplearrivalEntity.ProbableDateOfReport;
                    sampleCollection.PlannerId = samplearrivalEntity.PlannerId;//to be removed later
                    _dbContext.SaveChanges();
                }
                else
                {
                    sampleCollection.SampleCollectionId = samplearrivalEntity.SampleCollectionId;
                    sampleCollection.RequestNo = samplearrivalEntity.RequestNo;
                    sampleCollection.ULRNo = samplearrivalEntity.ULRNo;
                    sampleCollection.ReturnedDate = DateTime.UtcNow;
                    sampleCollection.ReturnedRemark = samplearrivalEntity.ReturnedRemark;
                    sampleCollection.IsDisposed = false;
                    sampleCollection.IsSampleIntact = samplearrivalEntity.IsSampleIntact;
                    sampleCollection.ProbableDateOfReport = samplearrivalEntity.ProbableDateOfReport;
                    sampleCollection.PlannerId = samplearrivalEntity.PlannerId;//to be removed later
                    _dbContext.SaveChanges();
                }
               
                var scol = _dbContext.SampleCollections.Find(samplearrivalEntity.SampleCollectionId);
                if (scol.IsReturnedOrIsRetained == "" || scol.IsReturnedOrIsRetained == null || scol.SubContractedParameters == "" || scol.SubContractedParameters == null|| scol.AckRemarks ==""|| scol.AckRemarks == null)
                {
                    var scolldetails = (from sc in _dbContext.SampleCollections
                                        where sc.WorkOrderID == samplearrivalEntity.WorkOrderID && sc.EnquirySampleID == samplearrivalEntity.EnquirySampleID
                                        select new
                                        {
                                            sc.SampleCollectionId,
                                            sc.EnquirySampleID,
                                            sc.WorkOrderID,
                                        }

                               ).ToList();

                    List<SampleCollection> samples = new List<SampleCollection>();
                    SampleCollection s1 = new SampleCollection();
                    foreach (var samp in scolldetails)
                    {
                        s1 = _dbContext.SampleCollections.Find(samp.SampleCollectionId);
                        s1.IsReturnedOrIsRetained = samplearrivalEntity.IsReturnedOrIsRetained;
                        s1.StatutoryLimits = samplearrivalEntity.StatutoryLimits;
                        s1.SubContractedParameters = samplearrivalEntity.SubContractedParameters;
                        s1.AckRemarks = samplearrivalEntity.AckRemarks;
                        _dbContext.SaveChanges();
                    }
                }

                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public SampleArrivalEntity GetSampleArrivalDetails(int SampleCollectionId)
        {
            return (
                   from loc in _dbContext.LocationSampleCollections
                   join scoll in _dbContext.SampleCollections on loc.LocationSampleCollectionID equals scoll.LocationSampleCollectionID
                   join es in _dbContext.EnquirySampleDetails on scoll.EnquirySampleID equals es.EnquirySampleID
                   join wo in _dbContext.WorkOrders on scoll.WorkOrderID equals wo.WorkOrderId
                   join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId

                   join sts in _dbContext.StatusMasters on scoll.StatusId equals sts.StatusId

                    into scollArrival
                   from scollArr in scollArrival.DefaultIfEmpty()

                   join stm in _dbContext.SampleTypeMasters on scoll.SampleTypeId equals stm.SampleTypeId
                   into type
                   from t in type.DefaultIfEmpty()

                       //join sdm in _dbContext.SampleDeviceMasters on scoll.SampleDeviceId equals sdm.SampleDeviceId
                   join env in _dbContext.EnvironmentalConditions on scoll.EnvCondtId equals env.EnvCondtId
                   into Env
                   from e in Env.DefaultIfEmpty()

                       //join pgm in _dbContext.ProductGroupMasters on ed.ProductGroupId equals pgm.ProductGroupId
                       //join sgm in _dbContext.SubGroupMasters on ed.SubGroupId equals sgm.SubGroupId
                       //join mm in _dbContext.MatrixMasters on ed.MatrixId equals mm.MatrixId
                   join stp in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stp.SampleTypeProductId

                   //join em in _dbContext.EnquiryMasters on ed.EnquiryId equals em.EnquiryId
                   //join ctm in _dbContext.CustomerMasters on em.CustomerMasterId equals ctm.CustomerMasterId

                   into arrival
                   from arriv in arrival.DefaultIfEmpty()

                   join em in _dbContext.EnquiryMasters on ed.EnquiryId equals em.EnquiryId into em
                   from emt in em.DefaultIfEmpty()
                       //join ctm in _dbContext.CustomerMasters on em.CustomerMasterId equals ctm.CustomerMasterId
                   join ctm in _dbContext.CustomerMasters on emt.CustomerMasterId equals ctm.CustomerMasterId into ctm
                   from a in ctm.DefaultIfEmpty()

                   join ctmwo in _dbContext.CustomerMasters on wo.CustomerMasterId equals ctmwo.CustomerMasterId into temp
                   from last in temp.DefaultIfEmpty()

                   where scoll.SampleCollectionId == SampleCollectionId

                   select new SampleArrivalEntity()
                   {
                       LocationSampleCollectionID = (Int32)scoll.LocationSampleCollectionID,
                       SampleTypeProductId = (Int32)ed.SampleTypeProductId,
                       SampleTypeProductName = arriv.SampleTypeProductName,

                       SampleCollectionId = scoll.SampleCollectionId,
                       EnquirySampleID = scoll.EnquirySampleDetail.EnquirySampleID,
                       EnquiryDetailId = es.EnquiryDetailId,
                       EmployeeId = scoll.EmployeeId,//doubt
                       SampleCollectedBy = es.SampleCollectedBy,
                       CollectionDate = scoll.CollectionDate,
                       SampleCollectionTime = scoll.SampleCollectionTime,//doubt
                       Duration = scoll.Duration,
                       SampleTypeId = scoll.SampleTypeId == null ? 0 : scoll.SampleTypeId,
                       SampleType = t.SampleType == "" || t.SampleType == null ? t.SampleType : t.SampleType,
                       //SampleDeviceId = scoll.SampleDeviceId,
                       //SampleDevice = sdm.SampleDevice,
                       EnvCondtId = scoll.EnvCondtId == null ? 0 : scoll.EnvCondtId,
                       EnvCondts = e.EnvCondts == "" || e.EnvCondts == null ? e.EnvCondts : e.EnvCondts,
                       StatusId = scoll.StatusMaster.StatusId,
                       CurrentStatus = scollArr.StatusName,
                       StatusCode = scollArr.StatusCode,//FoLabStatus
                       WorkOrderID = scoll.WorkOrder.WorkOrderId,
                       //SampleNo = scoll.SampleNo,
                       SampleName = es.SampleName,
                       SampleNameOriginal=loc.SampleNameOriginal,
                       //FieldDeterminationId = scoll.FieldDeterminationId,
                       // ULRNo = scoll.ULRNo,
                       WitnessName = scoll.WitnessName,
                       //ProductGroupId = (Int32)ed.ProductGroupId,
                       //ProductGroupName = pgm.ProductGroupName,
                       //SubGroupId = (Int32)ed.SubGroupId,
                       //SubGroupName = sgm.SubGroupName,
                       //MatrixId = ed.MatrixId,
                       //MatrixName = arriv.MatrixName,
                       SampleLocation = scoll.SampleLocation,
                       //SampleReceviedLabBy= arc.UserRole.UserRoleId,
                       ProbableDateOfReport = scoll.ProbableDateOfReport,
                       //EnquiryId = (long)ed.EnquiryId.Value,
                       EnquiryId = emt.EnquiryId == null ? 0 : emt.EnquiryId,
                       //CustomerName = em.CustomerMaster.RegistrationName,
                       CustomerName = a.RegistrationName == "" || a.RegistrationName == null ? last.RegistrationName : a.RegistrationName,
                       //CityName = em.CustomerMaster.CityName,
                       CityName = a.CityName == "" || a.CityName == null ? last.CityName : a.CityName,
                       RequestNo = scoll.RequestNo,
                       ULRNo = scoll.ULRNo,
                       PlannerId = scoll.PlannerId,
                       IsSampleIntact = scoll.IsSampleIntact,
                       IsReturnedOrIsRetained=scoll.IsReturnedOrIsRetained,
                       StatutoryLimits=scoll.StatutoryLimits,
                       SubContractedParameters=scoll.SubContractedParameters,
                       AckRemarks=scoll.AckRemarks,
                   }
                   ).FirstOrDefault();

        }
        public SampleArrivalEntity GetARCDetails(int ARCId)
        {
            return (
                     from arc in _dbContext.ARCs
                     join scoll in _dbContext.SampleCollections on arc.SampleCollectionId equals scoll.SampleCollectionId
                     into arrival
                     from arriv in arrival.DefaultIfEmpty()
                     where arc.ARCId == ARCId
                     select new SampleArrivalEntity()
                     {
                         ARCId = arc.ARCId,
                         SampleCollectionId = arc.SampleCollectionId,
                         UserRoleId = (Int32)arc.UserRoleId == null ? 0 : (Int32)arc.UserRoleId,
                         ActionDate = arc.ActionDate,
                         //Remarks = arc.Remarks,
                     }).FirstOrDefault();

        }
        public string AddApprover(List<PlannerByDisciplineEntity> approvers,long EnquirySampleID,long SampleCollectionId)
        {
            try
            {
                var par = (from epd in _dbContext.EnquiryParameterDetails
                           join pm in _dbContext.ParameterMappings on epd.ParameterMappingId equals pm.ParameterMappingId
                           into epm
                           from a in epm.DefaultIfEmpty()
                           join p in _dbContext.ParameterMasters on a.ParameterMasterId equals p.ParameterMasterId
                           into ep
                           from pr in ep.DefaultIfEmpty()
                           join pmgm in _dbContext.ParameterGroupMasters on a.ParameterGroupId equals pmgm.ParameterGroupId
                           join dis in _dbContext.DisciplineMasters on pmgm.DisciplineId equals dis.DisciplineId
                           into d
                           from dis in d.DefaultIfEmpty()
                           where epd.EnquirySampleID == EnquirySampleID
                           select new
                           {
                               pr.ParameterName,
                               pr.ParameterMasterId,
                               epd.ParameterMappingId,
                               epd.UnitId,
                               epd.TestMethodId,
                               pmgm.ParameterGroupId,
                           }).ToList();
               
                foreach (var item in par)
                {
                    SampleParameterPlanning spp = new SampleParameterPlanning();
                    spp.SampleCollectionId = SampleCollectionId;
                    spp.ApproverUserMasterID = 1;
                    spp.ApproverApproveSts = 0;
                    spp.CurrentStatus = 3;
                    spp.ParameterGroupId = item.ParameterGroupId;
                    spp.ParameterMasterId = item.ParameterMasterId;
                    spp.TestMethodID = (Int32)item.TestMethodId;
                    spp.UnitId = item.UnitId;
                    spp.AnalystUserMasterID = 1;
                    spp.ReviewerUserMasterID =1;
                    spp.IsActive = true;
                    spp.EnteredBy = 1;
                    spp.EnteredDate = DateTime.UtcNow;
                    _dbContext.SampleParameterPlannings.Add(spp);
                    _dbContext.SaveChanges();
                }

                List<long> SampleParameterPlanningIDs = _dbContext.SampleParameterPlannings.Where(e => e.SampleCollectionId == SampleCollectionId).Select(e => e.SampleParameterPlanningID).ToList();
                foreach (var item in approvers)
                {
                    for (int i = 0; i < SampleParameterPlanningIDs.Count; i++)
                    {
                        long sampleparameterplanningId = SampleParameterPlanningIDs[i];
                        var result = _dbContext.SampleParameterPlannings.Find(sampleparameterplanningId);
                        result.ApproverUserMasterID =(Int32)item.ApproverId;
                        _dbContext.SaveChanges();
                    }
                }

                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        //////////////For Received Person////////////////////////////////////////////////////////////////////////////////
        public string UpdateSampleReceived(SampleArrivalEntity samplearrivalEntity)
        {
            try
            {
                var arc = _dbContext.ARCs.Find(samplearrivalEntity.ARCId);
                arc.ARCId = samplearrivalEntity.ARCId;
                arc.SampleCollectionId = samplearrivalEntity.SampleCollectionId;
                arc.ActionDate = samplearrivalEntity.ActionDate;
                arc.IsActive = samplearrivalEntity.IsActive;
                arc.EnteredBy = samplearrivalEntity.EnteredBy;
                arc.EnteredDate = DateTime.Now;
                _dbContext.SaveChanges();

                var sampleCollection = _dbContext.SampleCollections.Find(samplearrivalEntity.SampleCollectionId);
                sampleCollection.SampleCollectionId = samplearrivalEntity.SampleCollectionId;
                sampleCollection.IsSampleIntact = samplearrivalEntity.IsSampleIntact;
                sampleCollection.ProbableDateOfReport = samplearrivalEntity.ProbableDateOfReport;
                sampleCollection.PlannerId = samplearrivalEntity.PlannerId;//to be removed later
                _dbContext.SaveChanges();

                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string AddPlannerByDiscipline(List<PlannerByDisciplineEntity> planners)
        {
            try
            {
                foreach (var item in planners)
                {
                    _dbContext.PlannerByDisciplines.Add(new PlannerByDiscipline()
                    {
                        SampleCollectionId = item.SampleCollectionId,
                        PlannerId = item.PlannerId,
                        DisciplineId = item.DisciplineId,
                        ParameterName = item.Parameters,
                        IsActive = item.IsActive,
                        EnteredBy = item.EnteredBy,
                        EnteredDate = item.EnteredDate,
                    }); ;

                    _dbContext.SaveChanges();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        ///////////////////SampleNo////////////////////Not in Used here////////////////////////////////////////
        public string GenerateSampleNo(int SampleCollectionId, string SampleNo)
        {
            try
            {
                var scoll = _dbContext.SampleCollections.Find(SampleCollectionId);
                long sampleCount = GetSampleCount(Convert.ToInt32(DateTime.Now.Year), Convert.ToInt32(DateTime.Today.Month));
                long SrNumber = 0;
                if (sampleCount == 0 || sampleCount == null)
                {
                    SrNumber = 1;//Doubt 
                    AddSampleNo(1, DateTime.Now.Year, DateTime.Now.Month);
                }
                else
                {
                    SrNumber = sampleCount + 1;
                    UpdateSampleNo(SrNumber, DateTime.Now.Year, DateTime.Now.Month);
                }

                scoll.SampleNo = SampleNo + "/" + DateTime.Now.ToString("yyMM") + "/" + Convert.ToString(SrNumber);
                _dbContext.SaveChanges();
                return scoll.SampleNo;
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public long GetSampleCount(int Year, int Month)
        {
            var sampleNum = (from e in _dbContext.SampleNumbers
                             where e.Year == Year && e.Month == Month
                             select new SampleNumberEntity()
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
                var sample = new SampleNumber()
                {
                    SampleCount = SampleCount,
                    Year = Year,
                    Month = Month,
                    IsActive = true,
                    EnteredBy = 1,
                    EnteredDate = DateTime.Now

                };
                _dbContext.SampleNumbers.Add(sample);
                _dbContext.SaveChanges();
                return sample.SampleNumberId;
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
                var sampleNum = (from e in _dbContext.SampleNumbers
                                 where e.Year == Year && e.Month == Month
                                 select new SampleNumberEntity()
                                 {
                                     SampleNumberId = e.SampleNumberId,
                                 }
                ).FirstOrDefault();


                var sampleMaster = _dbContext.SampleNumbers.Find(sampleNum.SampleNumberId);
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

        //////////////////////////////////////////////
        ///////////////////ULRNo////////////////////
        public string GenerateULRNo(int SampleCollectionId,int EnquiryDetailId)
        {
            try
            {
               // var scoll = _dbContext.SampleCollections.Find(SampleCollectionId);
                string NablNo = "TC7487";
                string Value;
                string ISNabl= IsNABL(EnquiryDetailId);
               
                    if (ISNabl != string.Empty)
                    {
                        Value = ISNabl;
                    }
                    else
                    {
                        return null;
                    }

                    int Year = DateTime.Now.Year;
                    long ulrCount = GetULRCount(Convert.ToInt32(DateTime.Now.Year));
                    long SrNumber = 0;
                    string DigitCode = "0000000";//7 digit code
                    if (ulrCount == 0 || ulrCount == null)
                    {
                        SrNumber = 1;//Doubt 
                        DigitCode = "00000001";
                        AddULRNo(1, DateTime.Now.Year);
                    }
                    else
                    {
                        SrNumber = ulrCount + 1;

                        if (SrNumber.ToString().Length == 1)
                        {
                            DigitCode = "0000000" + SrNumber.ToString();
                        }
                        else if (SrNumber.ToString().Length == 2)
                        {
                            DigitCode = "000000" + SrNumber.ToString();
                        }
                        else if (SrNumber.ToString().Length == 3)
                        {
                            DigitCode = "00000" + SrNumber.ToString();
                        }
                        else if (SrNumber.ToString().Length == 4)
                        {
                            DigitCode = "0000" + SrNumber.ToString();
                        }
                        else if (SrNumber.ToString().Length == 5)
                        {
                            DigitCode = "000" + SrNumber.ToString();
                        }
                        else if (SrNumber.ToString().Length == 6)
                        {
                            DigitCode = "00" + SrNumber.ToString();
                        }
                        else if (SrNumber.ToString().Length == 7)
                        {
                            DigitCode = "0" + SrNumber.ToString();
                        }

                        UpdateULRNo(SrNumber, DateTime.Now.Year);

                    }

                  //scoll.ULRNo = "ULR-" + NablNo + Year.ToString().Substring(2, 2) + "0" + DigitCode + Value;
                var ULRNo = "ULR-" + NablNo + Year.ToString().Substring(2, 2) + "0" + DigitCode + Value;
                return ULRNo;
            }
            catch (Exception ex)
            {
                 return ex.InnerException.Message;
            }
        }
        public string IsNABL(int EnquiryDetailId)
        {
            var nabl = (from epd in _dbContext.EnquiryParameterDetails
                             join pmapp in _dbContext.ParameterMappings on epd.ParameterMappingId equals pmapp.ParameterMappingId
                             join pm in _dbContext.ParameterMasters on pmapp.ParameterMasterId equals pm.ParameterMasterId
                             join es in _dbContext.EnquirySampleDetails on epd.EnquirySampleID equals es.EnquirySampleID
                             join ed in _dbContext.EnquiryDetails on es.EnquiryDetailId equals ed.EnquiryDetailId
                             where ed.EnquiryDetailId == EnquiryDetailId
                             select new
                             {
                                 pmapp.ParameterMasterId,
                                 pmapp.ParameterMaster.ParameterName,
                                 pmapp.IsNABLAccredited,

                             }).ToList();

            string str = string.Empty;
            var count = 0;
            if (nabl != null)
            {
                foreach (var Item in nabl)
                {
                    if (Item.IsNABLAccredited == true)
                    {
                        count++;
                    }                    
                }
                if (count == nabl.Count)
                {
                    str = "F";
                }
                else if(count > 0)
                {
                    str = "F";
                }
                else {
                    str = "F";
                }
            }
            else {
                str = "F";
            }
            return str;
        }
        public long GetULRCount(int Year)
        {
            var ulrNumber = (from e in _dbContext.ULRNumbers
                             where e.Year == Year
                             select new ULRNoEntity()
                             {
                                 ULRCount = e.ULRCount,
                             }
                  ).FirstOrDefault();
            if (ulrNumber != null)
            {
                return ulrNumber.ULRCount;
            }
            else { return 0; }
        }
        public long AddULRNo(long ULRCount, int Year)
        {
            try
            {
                var ulr = new ULRNumber()
                {
                    ULRCount = ULRCount,
                    Year = Year,
                    IsActive = true,
                    EnteredBy = 1,
                    EnteredDate = DateTime.Now
                };
                _dbContext.ULRNumbers.Add(ulr);
                _dbContext.SaveChanges();
                return ulr.ULRNumberId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string UpdateULRNo(long ULRCount, int Year)
        {
            try
            {
                var ulrNumber = (from e in _dbContext.ULRNumbers
                                 where e.Year == Year
                                 select new ULRNoEntity()
                                 {
                                     ULRNumberId = e.ULRNumberId,
                                 }
                ).FirstOrDefault();

                var ulrNum = _dbContext.ULRNumbers.Find(ulrNumber.ULRNumberId);
                ulrNum.ULRCount = ULRCount;
                ulrNum.Year = Year;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            string FinYear = null;

            if (DateTime.Today.Month > 3)
                FinYear = CurYear + "-" + NexYear;
            else
                FinYear = PreYear + "-" + CurYear;
            return FinYear.Trim();
        }

        ///////////////////ReportNo////////////////////
        public string GenerateReportNo(int SampleCollectionId, string SampleName, string CollectedBy, string CustomerName, string CityName)
        {
            try
            {
               // var scoll = _dbContext.SampleCollections.Find(SampleCollectionId);
                string CompanyCode = "ME";
                string CityCode = "NG";
                //string Year = DateTime.Now.Year.ToString();
                long reportCount = GetReportCount(Convert.ToInt32(DateTime.Now.Year));
                long SrNumber = 0;
                string DigitCode = "00000";//5 digit code

                string Coll;
                if (CollectedBy == "Lab")
                {
                    Coll = "AT";
                }
                else
                {
                    Coll = "SA";
                }

                string _customerName = "";
                _customerName = CustomerName;
                if (_customerName == null || _customerName == "")
                {
                    _customerName = "";
                }
                else if (_customerName.Length > 5)
                {
                    _customerName = CustomerName.Substring(0, 5).ToUpper();
                }

                string _cityName = "";
                _cityName = CityName;
                if (_cityName == null || _cityName == "")
                {
                    _cityName = "";
                }
                else if (_cityName.Length > 5)
                {
                    _cityName = CityName.Substring(0, 5).ToUpper();
                }

                if (reportCount == 0 || reportCount == null)
                {
                    SrNumber = 1;//Doubt 
                    DigitCode = "00001";
                    AddReportNo(1, DateTime.Now.Year);
                }
                else
                {
                    SrNumber = reportCount + 1;

                    if (SrNumber.ToString().Length == 1)
                    {
                        DigitCode = "0000" + SrNumber.ToString();
                    }
                    else if (SrNumber.ToString().Length == 2)
                    {
                        DigitCode = "000" + SrNumber.ToString();
                    }
                    else if (SrNumber.ToString().Length == 3)
                    {
                        DigitCode = "00" + SrNumber.ToString();
                    }
                    else if (SrNumber.ToString().Length == 4)
                    {
                        DigitCode = "0" + SrNumber.ToString();
                    }
                    UpdateReportNo(SrNumber, DateTime.Now.Year);
                }
                   //ReportNumber Format => [ME - NG - 000002 - 210109 - SA - MSEB - NAGPU]
                //scoll.RequestNo = CompanyCode + "-" + CityCode + "-" + SampleName + "-" + DateTime.Now.ToString("yyMMdd") + "-" + Coll + "-" + _customerName + "-" + _cityName;
                var RequestNo = CompanyCode + "-" + CityCode + "-" + DigitCode + "-" + DateTime.Now.ToString("yyMMdd") + "-" + Coll + "-" + _customerName + "-" + _cityName;
                return RequestNo;
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public long GetReportCount(int Year)
        {
            var reportNumber = (from e in _dbContext.ReportNumbers
                                where e.Year == Year
                                select new ReportNoEntity()
                                {
                                    ReportCount = e.ReportCount,
                                }
                  ).FirstOrDefault();

            if (reportNumber != null)
            {
                return reportNumber.ReportCount;
            }
            else { return 0; }
        }
        public long AddReportNo(long ReportCount, int Year)
        {
            try
            {
                var reprt = new ReportNumber()
                {
                    ReportCount = ReportCount,
                    Year = Year,
                    IsActive = true,
                    EnteredBy = 1,
                    EnteredDate = DateTime.Now

                };
                _dbContext.ReportNumbers.Add(reprt);
                _dbContext.SaveChanges();
                return reprt.ReportNumberId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string UpdateReportNo(long ReportCount, int Year)
        {
            try
            {
                var reportNumber = (from e in _dbContext.ReportNumbers
                                    where e.Year == Year
                                    select new ReportNoEntity()
                                    {
                                        ReportNumberId = e.ReportNumberId,
                                    }
                 ).FirstOrDefault();

                var report = _dbContext.ReportNumbers.Find(reportNumber.ReportNumberId);
                report.ReportCount = ReportCount;
                report.Year = Year;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        ///////////////////ReportNo////////////////////

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
