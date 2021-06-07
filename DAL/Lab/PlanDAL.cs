using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.Lab;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Common;





namespace LIMS_DEMO.DAL.Lab
{
    public class PlanDAL : IPlan
    {
        readonly LIMSEntities _dbContext;
       
        public PlanDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public IList<PlanEntity> GetList(int DisciplineId, int UserMasterID)
        {
            try
            {
                var result = (from pbd in _dbContext.PlannerByDisciplines
                              join sc in _dbContext.SampleCollections on pbd.SampleCollectionId equals sc.SampleCollectionId
                              join loc in _dbContext.LocationSampleCollections on sc.LocationSampleCollectionID equals loc.LocationSampleCollectionID
                              join esd in _dbContext.EnquirySampleDetails on sc.EnquirySampleID equals esd.EnquirySampleID
                              join ed in _dbContext.EnquiryDetails on esd.EnquiryDetailId equals ed.EnquiryDetailId
                              //join stpm in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stpm.SampleTypeProductId
                              join epd in _dbContext.EnquiryParameterDetails on esd.EnquirySampleID equals epd.EnquirySampleID
                              join pmp in _dbContext.ParameterMappings on epd.ParameterMappingId equals pmp.ParameterMappingId

                              join sm in _dbContext.StatusMasters on sc.StatusId equals sm.StatusId

                              join pm in _dbContext.ParameterGroupMasters on pmp.ParameterGroupId equals pm.ParameterGroupId
                              join stp in _dbContext.SampleTypeProductMasters on pm.SampleTypeProductId equals stp.SampleTypeProductId
                              join pgm in _dbContext.ProductGroupMasters on pm.ProductGroupId equals pgm.ProductGroupId
                              join sgm in _dbContext.SubGroupMasters on pm.SubGroupId equals sgm.SubGroupId
                              join mm in _dbContext.MatrixMasters on pm.MatrixId equals mm.MatrixId

                              where pmp.DisciplineId == DisciplineId
                              where pm.SampleTypeProductId == stp.SampleTypeProductId
                              where pm.SubGroupId == sgm.SubGroupId
                              where pm.ProductGroupId == pgm.ProductGroupId && pmp.InField == false
                              where sc.IsActive == true && pbd.PlannerId == UserMasterID
                              select new
                              {
                                  sc.SampleCollectionId,
                                  sc.SampleNo,
                                  esd.SampleName,
                                  loc.SampleNameOriginal,
                                  stp.SampleTypeProductName,
                                  pgm.ProductGroupName,
                                  sgm.SubGroupName,
                                  mm.MatrixName,
                                  sm.StatusName
                              }).OrderByDescending(sc => sc.SampleCollectionId).ToList().Distinct();
                IList<PlanEntity> PlanEntities = new List<PlanEntity>();

                foreach (var item in result)
                {
                    PlanEntity PlanEntity = new PlanEntity();
                    PlanEntity.SampleCollectionId = item.SampleCollectionId;
                    PlanEntity.SampleNo = item.SampleNo;
                    PlanEntity.SampleName = item.SampleName;
                    PlanEntity.SampleNameOriginal = item.SampleNameOriginal;
                    PlanEntity.SampleTypeProductName = item.SampleTypeProductName;
                    PlanEntity.ProductGroupName = item.ProductGroupName;
                    PlanEntity.SubGroupName = item.SubGroupName;
                    PlanEntity.MatrixName = item.MatrixName;
                    PlanEntity.StatusName = item.StatusName;
                    PlanEntities.Add(PlanEntity);
                }
                return PlanEntities;
            }
            catch (Exception ex)
            {
               
                return null;
            }

        }
        public SampleInfoEntity GetSampleInfoEntity(int SampleId, int DisciplineId)
        {
            SampleInfoEntity sampleInfoEntity = new SampleInfoEntity();
            try
            {
                var result = (from sc in _dbContext.SampleCollections
                              join esd in _dbContext.EnquirySampleDetails on sc.EnquirySampleID equals esd.EnquirySampleID
                              join ed in _dbContext.EnquiryDetails on esd.EnquiryDetailId equals ed.EnquiryDetailId

                              join us in _dbContext.UserMasters on sc.EnteredBy equals us.UserMasterID
                              join arc in _dbContext.ARCs on sc.SampleCollectionId equals arc.SampleCollectionId
                              into A
                              from arc in A.DefaultIfEmpty()

                              join ec in _dbContext.EnvironmentalConditions on sc.EnvCondtId equals ec.EnvCondtId
                              into EnvCond
                              from ec in EnvCond.DefaultIfEmpty()

                              join sqm in _dbContext.SampleQtyMasters on sc.SampleQtyId equals sqm.SampleQtyId
                                into sampleQtyM
                              from sqm in sampleQtyM.DefaultIfEmpty()

                              join pm in _dbContext.ProcedureMasters on sc.ProcedureId equals pm.ProcedureId
                                into ProcMaster
                              from pm in ProcMaster.DefaultIfEmpty()
                              where sc.SampleCollectionId == SampleId
                              select new
                              {
                                  us.UserName,
                                  sc.SampleLocation,
                                  // sqm.SampleQty == null ? "" : sqm.SampleQty,
                                  sqm.SampleQty,
                                  pm.ProcedureName,
                                  ec.EnvCondts,
                                  sc.ProbableDateOfReport,
                                  sc.CollectionDate,
                                  sc.SampleCollectionTime,
                                  arc.ActionDate
                              }).FirstOrDefault();
                if (result != null)
                {
                    DateTime? collectionDate = result.CollectionDate;
                    TimeSpan? sampleCollectionTime = result.SampleCollectionTime;
                    if (collectionDate != null && sampleCollectionTime != null)
                    {
                        DateTime? SampleCollectiondt = new DateTime(collectionDate.Value.Year, collectionDate.Value.Month, collectionDate.Value.Day, sampleCollectionTime.Value.Hours, sampleCollectionTime.Value.Minutes, sampleCollectionTime.Value.Seconds, sampleCollectionTime.Value.Milliseconds);
                        sampleInfoEntity.SamplingDateTime = SampleCollectiondt;
                    }
                    sampleInfoEntity.SampleLocation = result.SampleLocation;
                    sampleInfoEntity.CollectedBy = result.UserName;
                    sampleInfoEntity.Quantity = result.SampleQty == null ? "" : result.SampleQty;
                    sampleInfoEntity.Procedure = result.ProcedureName == null ? "" : result.ProcedureName;
                    sampleInfoEntity.EnvCondition = result.EnvCondts == null ? "" : result.EnvCondts;
                    sampleInfoEntity.ReportSent = result.ProbableDateOfReport;

                    sampleInfoEntity.DateReceiptOfSampling = result.ActionDate;
                }
                var parameterResult = (from sc in _dbContext.SampleCollections
                                       join epd in _dbContext.EnquiryParameterDetails on sc.EnquirySampleID equals epd.EnquirySampleID
                                       join esd in _dbContext.EnquirySampleDetails on sc.EnquirySampleID equals esd.EnquirySampleID
                                       join ed in _dbContext.EnquiryDetails on esd.EnquiryDetailId equals ed.EnquiryDetailId
                                       join stp in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stp.SampleTypeProductId
                                       join pm in _dbContext.ParameterMappings on epd.ParameterMappingId equals pm.ParameterMappingId
                                       join p in _dbContext.ParameterMasters on pm.ParameterMasterId equals p.ParameterMasterId
                                       join um in _dbContext.UnitMasters on epd.UnitId equals um.UnitId
                                       join tm in _dbContext.TestMethods on epd.TestMethodId equals tm.TestMethodId
                                       join pamg in _dbContext.ParameterGroupMasters on stp.SampleTypeProductId equals pamg.SampleTypeProductId
                                       join spp in _dbContext.SampleParameterPlannings on
                                       new { ParameterGroupId = pm.ParameterGroupId, ParameterMasterId = p.ParameterMasterId, SampleCollectionId = sc.SampleCollectionId } equals
                                       new { ParameterGroupId = spp.ParameterGroupId, ParameterMasterId = spp.ParameterMasterId, SampleCollectionId = spp.SampleCollectionId }
                                       into parameterDetails

                                       from pd in parameterDetails.DefaultIfEmpty()
                                       where sc.SampleCollectionId == SampleId && pm.DisciplineId == DisciplineId && pamg.ParameterGroupId == pm.ParameterGroupId && pm.InField == false
                                       // && pd.IsActive==true
                                       select new ParameterInfo
                                       {
                                           ParameterGroupId = pm.ParameterGroupId,
                                           ParameterMasterId = p.ParameterMasterId,
                                           ParameterName = p.ParameterName,
                                           UnitId = um.UnitId,
                                           Unit = um.Unit,
                                           TestMethodId = tm.TestMethodId,
                                           TestMethod = tm.TestMethod1,
                                           IsField = pm.InField,
                                           AnalystUserMasterID = pd == null ? 0 : pd.AnalystUserMasterID,
                                           ReviewerUserMasterID = pd == null ? 0 : pd.ReviewerUserMasterID,
                                           ApproverUserMasterID = pd == null ? 0 : pd.ApproverUserMasterID,
                                           SelectedTestMethodId = pd == null ? 0 : pd.TestMethodID,
                                           CurrentStatus = pd.CurrentStatus,
                                           // IsActive = pd == null ? true : pd.IsActive
                                           //AnalystUserMasterID = pd == null ? 0 : pd.AnalystUserMasterID,
                                           //ReviewerUserMasterID = pd == null ? 0 : pd.ReviewerUserMasterID,
                                           //ApproverUserMasterID = pd == null ? 0 : pd.ApproverUserMasterID,
                                           //SelectedTestMethodId = pd == null ? 0 : pd.TestMethodID,
                                           IsActive = pd.IsActive

                                       }).ToList();

                foreach (var item in parameterResult)
                {
                    var ParameterGroupId = item.ParameterGroupId;
                    var ParameterMasterId = item.ParameterMasterId;
                    var UnitId = item.UnitId;
                    var testMethods = (from pm in _dbContext.ParameterMappings
                                       join tm in _dbContext.TestMethods on pm.TestMethodId equals tm.TestMethodId
                                       where pm.ParameterGroupId == ParameterGroupId && pm.ParameterMasterId == ParameterMasterId
                                       && pm.UnitId == UnitId
                                       select new TestMethods
                                       {
                                           TestMethodId = pm.TestMethodId,
                                           TestMethodName = tm.TestMethod1

                                       }
                                       ).ToList();

                    item.TestMethodList = testMethods;
                }

                sampleInfoEntity.ParameterInfos = parameterResult;

                var analyst = (from u in _dbContext.UserMasters
                               join ur in _dbContext.UserRoles on u.UserMasterID equals ur.UserMasterId
                               join ud in _dbContext.UserDetails on u.UserMasterID equals ud.UserMasterID
                               join rm in _dbContext.RoleMasters on ur.RoleId equals rm.RoleId
                               where u.IsActive == true && ud.IsActive == true && u.DisciplineId == DisciplineId && rm.RoleName == "Tester"
                               select new User
                               {
                                   UserId = u.UserMasterID,
                                   Username = ud.FirstName + " " + ud.LastName

                               }).Distinct().ToList();
                var reviewer = (from u in _dbContext.UserMasters
                                join ur in _dbContext.UserRoles on u.UserMasterID equals ur.UserMasterId
                                join ud in _dbContext.UserDetails on u.UserMasterID equals ud.UserMasterID
                                join rm in _dbContext.RoleMasters on ur.RoleId equals rm.RoleId
                                where u.IsActive == true && ud.IsActive == true && u.DisciplineId == DisciplineId && rm.RoleName == "Reviewer"
                                select new User
                                {
                                    UserId = u.UserMasterID,
                                    Username = ud.FirstName + " " + ud.LastName

                                }).Distinct().ToList();
                var approver = (from u in _dbContext.UserMasters
                                join ur in _dbContext.UserRoles on u.UserMasterID equals ur.UserMasterId
                                join ud in _dbContext.UserDetails on u.UserMasterID equals ud.UserMasterID
                                join rm in _dbContext.RoleMasters on ur.RoleId equals rm.RoleId
                                where u.IsActive == true && ud.IsActive == true && u.DisciplineId == DisciplineId && rm.RoleName == "Approver"
                                select new User
                                {
                                    UserId = u.UserMasterID,
                                    Username = ud.FirstName + " " + ud.LastName

                                }).Distinct().ToList();
                sampleInfoEntity.Analysts = analyst;
                sampleInfoEntity.Reviewers = reviewer;
                sampleInfoEntity.Approvers = approver;
                return sampleInfoEntity;
            }
            catch (Exception ex)
            {
                
                return sampleInfoEntity;
            }
        }
        public bool SaveParameterSelectedDetails(ParameterSelectedDetails parameterSelectedDetails)
        {
            try
            {
                int iStatusId = _dbContext.StatusMasters.Where(s => s.StatusCode.ToLower() == "Planned" && s.IsActive == true).Select(s => new { s.StatusId }).FirstOrDefault().StatusId;
                var scoll = _dbContext.SampleCollections.Find(parameterSelectedDetails.SampleCollectionId);
                scoll.StatusId = (byte)iStatusId;
                _dbContext.SaveChanges();

                var result = _dbContext.SampleParameterPlannings.Where(x => x.ParameterGroupId == parameterSelectedDetails.ParameterGroupId
                && x.ParameterMasterId == parameterSelectedDetails.ParameterMasterId
                && x.SampleCollectionId == parameterSelectedDetails.SampleCollectionId
                );
                if (result != null)
                {
                    _dbContext.SampleParameterPlannings.RemoveRange(result);
                    _dbContext.SaveChanges();
                }

                _dbContext.SampleParameterPlannings.Add(new SampleParameterPlanning
                {
                    ParameterGroupId = parameterSelectedDetails.ParameterGroupId,
                    ParameterMasterId = parameterSelectedDetails.ParameterMasterId,
                    SampleCollectionId = parameterSelectedDetails.SampleCollectionId,
                    TestMethodID = parameterSelectedDetails.TestMethodID,
                    UnitId = parameterSelectedDetails.UnitId,
                    AnalystUserMasterID = parameterSelectedDetails.AnalystUserMasterID,
                    //ReviewerUserMasterID = parameterSelectedDetails.ReviewerUserMasterID,
                    ApproverUserMasterID = parameterSelectedDetails.ApproverUserMasterID,
                    IsActive = true,
                    EnteredBy = parameterSelectedDetails.EnteredBy,
                    AnalystApproveSts = 0,
                    //ReviewerApproveSts = 0,
                    ApproverApproveSts = 0,
                    CurrentStatus = (int)CurrentStatusEnum.Under_Testing,
                    AnalystComment = "",
                    //ReviewerComment = "",
                    ApproverComment = "",
                    EnteredDate = DateTime.Now
                }); ;
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }
    }
}