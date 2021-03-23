using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Common;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.Lab;

namespace LIMS_DEMO.DAL.Lab
{

    public class ReviewerDAL : IReviewer 
    {
        readonly LIMSEntities _dbContext;
        public ReviewerDAL()
        {
            _dbContext = new LIMSEntities();
        }

        public IList<ReviewerEntity> GetList(int DisciplineId, int UserMasterId)
        {
            try
            {
                var result = (from spp in _dbContext.SampleParameterPlannings
                              join sc in _dbContext.SampleCollections on spp.SampleCollectionId equals sc.SampleCollectionId
                              join loc in _dbContext.LocationSampleCollections on sc.LocationSampleCollectionID equals loc.LocationSampleCollectionID
                              join esd in _dbContext.EnquirySampleDetails on sc.EnquirySampleID equals esd.EnquirySampleID
                              join ed in _dbContext.EnquiryDetails on esd.EnquiryDetailId equals ed.EnquiryDetailId


                              join epd in _dbContext.EnquiryParameterDetails on esd.EnquirySampleID equals epd.EnquirySampleID
                              join pmp in _dbContext.ParameterMappings on epd.ParameterMappingId equals pmp.ParameterMappingId

                              join pm in _dbContext.ParameterGroupMasters on pmp.ParameterGroupId equals pm.ParameterGroupId
                              join stpm in _dbContext.SampleTypeProductMasters on pm.SampleTypeProductId equals stpm.SampleTypeProductId
                              join pgm in _dbContext.ProductGroupMasters on pm.ProductGroupId equals pgm.ProductGroupId
                              join sgm in _dbContext.SubGroupMasters on pm.SubGroupId equals sgm.SubGroupId
                              join mm in _dbContext.MatrixMasters on pm.MatrixId equals mm.MatrixId
                              join sm in _dbContext.StatusMasters on sc.StatusId equals sm.StatusId

                              join p in _dbContext.ParameterMasters on spp.ParameterMasterId equals p.ParameterMasterId
                              join pm1 in _dbContext.ParameterGroupMasters on
                              new { ParameterGroupId = spp.ParameterGroupId, SubGroupId = sgm.SubGroupId } equals
                              new { ParameterGroupId = pm1.ParameterGroupId, SubGroupId = pm1.SubGroupId }

                              where pm.DisciplineId == DisciplineId
                              where spp.ReviewerUserMasterID == UserMasterId
                              where spp.IsActive == true
                              select new
                              {
                                  sc.SampleCollectionId,
                                  sc.SampleNo,
                                  loc.SampleNameOriginal,
                                  stpm.SampleTypeProductName,
                                  pgm.ProductGroupName,
                                  sgm.SubGroupName,
                                  mm.MatrixName,
                                  sm.StatusName,

                              }).OrderByDescending(sc => sc.SampleCollectionId).ToList().Distinct();
               
                IList<ReviewerEntity> reviewEntities = new List<ReviewerEntity>();

                foreach (var item in result)
                {
                    var TotalCount = (from sp in _dbContext.SampleParameterPlannings
                                      where sp.SampleCollectionId == item.SampleCollectionId
                                      group sp by sp.SampleCollectionId into f
                                      select new { cnt = f.Count() }).FirstOrDefault();

                    var ApprovedCount = (from sp in _dbContext.SampleParameterPlannings
                                         where sp.SampleCollectionId == item.SampleCollectionId && (int)sp.CurrentStatus == 6
                                         group sp by sp.SampleCollectionId into f
                                         select new { cnt = f.Count() }).FirstOrDefault();

                    var ForReviewCount = (from sp in _dbContext.SampleParameterPlannings
                                          where sp.SampleCollectionId == item.SampleCollectionId && (int)sp.CurrentStatus == 2
                                          group sp by sp.SampleCollectionId into f
                                          select new { cnt = f.Count() }).FirstOrDefault();

                    ReviewerEntity reviewEntity = new ReviewerEntity();
                    reviewEntity.SampleCollectionId = item.SampleCollectionId;
                    reviewEntity.SampleNo = item.SampleNo;
                    //reviewEntity.SampleName = item.SampleName;
                    reviewEntity.SampleNameOriginal = item.SampleNameOriginal;
                    reviewEntity.SampleTypeProductName = item.SampleTypeProductName;
                    reviewEntity.ProductGroupName = item.ProductGroupName;
                    reviewEntity.SubGroupName = item.SubGroupName;
                    reviewEntity.MatrixName = item.MatrixName;
                    reviewEntity.StatusName = item.StatusName;
                    reviewEntity.NotApproveCount = (TotalCount == null ? 0 : TotalCount.cnt) - (ApprovedCount == null ? 0 : ApprovedCount.cnt);
                    reviewEntity.ReviewActive = ForReviewCount == null ? 0 : ForReviewCount.cnt;
                    reviewEntities.Add(reviewEntity);
                }
                return reviewEntities;
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }

        public SampleParameterInfoReview GetParameterInfo(int SampleId, int DisciplineId, int UserMasterId)
        {
            try
            {
                var result = (from spp in _dbContext.SampleParameterPlannings
                              join sc in _dbContext.SampleCollections on spp.SampleCollectionId equals sc.SampleCollectionId
                              join loc in _dbContext.LocationSampleCollections on sc.LocationSampleCollectionID equals loc.LocationSampleCollectionID

                              join esd in _dbContext.EnquirySampleDetails on sc.EnquirySampleID equals esd.EnquirySampleID
                              join ed in _dbContext.EnquiryDetails on esd.EnquiryDetailId equals ed.EnquiryDetailId

                              join epd in _dbContext.EnquiryParameterDetails on esd.EnquirySampleID equals epd.EnquirySampleID
                              join pmp in _dbContext.ParameterMappings on epd.ParameterMappingId equals pmp.ParameterMappingId

                              join pm in _dbContext.ParameterGroupMasters on pmp.ParameterGroupId equals pm.ParameterGroupId
                              join stpm in _dbContext.SampleTypeProductMasters on pm.SampleTypeProductId equals stpm.SampleTypeProductId
                              join pgm in _dbContext.ProductGroupMasters on pm.ProductGroupId equals pgm.ProductGroupId
                              join sgm in _dbContext.SubGroupMasters on pm.SubGroupId equals sgm.SubGroupId
                              join mm in _dbContext.MatrixMasters on pm.MatrixId equals mm.MatrixId
                              join sm in _dbContext.StatusMasters on sc.StatusId equals sm.StatusId

                              where sc.SampleCollectionId == SampleId
                              where pm.SubGroupId == sgm.SubGroupId
                              where pm.ProductGroupId == pgm.ProductGroupId
                              //   where sc.IsActive == true
                              where spp.IsActive == true
                              where (int)spp.CurrentStatus != 6

                              select new ReviewerEntity
                              {
                                  SampleCollectionId = sc.SampleCollectionId,
                                  SampleNo = sc.SampleNo,
                                  //SampleName = sc.SampleName,
                                  SampleNameOriginal = loc.SampleNameOriginal,
                                  SampleTypeProductName = stpm.SampleTypeProductName,
                                  ProductGroupName = pgm.ProductGroupName,
                                  SubGroupName = sgm.SubGroupName,
                                  MatrixName = mm.MatrixName,
                                  StatusName = sm.StatusName

                              }).FirstOrDefault();

                SampleParameterInfoReview parameterInfoReview = new SampleParameterInfoReview();
                parameterInfoReview.SampleDetails = result;
                var parameterResult = (from sc in _dbContext.SampleCollections
                                       join epd in _dbContext.EnquiryParameterDetails on sc.EnquirySampleID equals epd.EnquirySampleID
                                       join esd in _dbContext.EnquirySampleDetails on sc.EnquirySampleID equals esd.EnquirySampleID
                                       join ed in _dbContext.EnquiryDetails on esd.EnquiryDetailId equals ed.EnquiryDetailId
                                       join stpm in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stpm.SampleTypeProductId
                                       //join pgm in _dbContext.ProductGroupMasters on ed.ProductGroupId equals pgm.ProductGroupId

                                       join pm in _dbContext.ParameterMappings on epd.ParameterMappingId equals pm.ParameterMappingId
                                       join p in _dbContext.ParameterMasters on pm.ParameterMasterId equals p.ParameterMasterId
                                       join um in _dbContext.UnitMasters on epd.UnitId equals um.UnitId
                                       //join tm in _dbContext.TestMethods on epd.TestMethodId equals tm.TestMethodId
                                       join pamg in _dbContext.ParameterGroupMasters on stpm.SampleTypeProductId equals pamg.SampleTypeProductId
                                       join pgm in _dbContext.ProductGroupMasters on pamg.ProductGroupId equals pgm.ProductGroupId

                                       join spp in _dbContext.SampleParameterPlannings on
                                       //new { ParameterGroupId = pm.ParameterGroupId, ParameterMasterId = p.ParameterMasterId } equals
                                       //new { ParameterGroupId = spp.ParameterGroupId, ParameterMasterId = spp.ParameterMasterId }
                                       new { ParameterGroupId = pm.ParameterGroupId, ParameterMasterId = p.ParameterMasterId, SampleCollectionId = sc.SampleCollectionId } equals
                                       new { ParameterGroupId = spp.ParameterGroupId, ParameterMasterId = spp.ParameterMasterId, SampleCollectionId = spp.SampleCollectionId }
                                       join tm in _dbContext.TestMethods on spp.TestMethodID equals tm.TestMethodId

                                       into parameterDetails
                                       from pd in parameterDetails.DefaultIfEmpty()
                                       where sc.SampleCollectionId == SampleId && pamg.DisciplineId == DisciplineId
                                       && pamg.ParameterGroupId == pm.ParameterGroupId
                                       && spp.ReviewerUserMasterID == UserMasterId

                                       select new LIMS_DEMO.Core.Lab.ParameterInfoReview
                                       {
                                           ParameterGroupId = pm.ParameterGroupId,
                                           ParameterMasterId = p.ParameterMasterId,
                                           ParameterName = p.ParameterName,
                                           UnitId = um.UnitId,
                                           Unit = um.Unit,
                                           Remark = spp.ApproverComment,
                                           TestMethodId = pd.TestMethodId,
                                           TestMethod = pd.TestMethod1,
                                           AnalystUserMasterID = pd == null ? 0 : spp.AnalystUserMasterID,
                                           IsActive = pd == null ? true : pd.IsActive,
                                           CurrentStatus = spp.CurrentStatus
                                       }).ToList();

                parameterInfoReview.ParameterInfosReview = parameterResult;
                return parameterInfoReview;
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }

        public SampleParameterReview GetSampleParameterReview(int SampleId, int DisciplineId, int UserMasterId, long ParameterMasterId)
        {
            SampleParameterReview sampleParameterReview = new SampleParameterReview();
            try
            {
                sampleParameterReview = (from spp in _dbContext.SampleParameterPlannings
                                         join sc in _dbContext.SampleCollections on spp.SampleCollectionId equals sc.SampleCollectionId
                                         join loc in _dbContext.LocationSampleCollections on sc.LocationSampleCollectionID equals loc.LocationSampleCollectionID

                                         join esd in _dbContext.EnquirySampleDetails on sc.EnquirySampleID equals esd.EnquirySampleID
                                         join ed in _dbContext.EnquiryDetails on esd.EnquiryDetailId equals ed.EnquiryDetailId

                                         join stpm in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stpm.SampleTypeProductId

                                         join pm in _dbContext.ParameterGroupMasters on stpm.SampleTypeProductId equals pm.SampleTypeProductId

                                         join pgm in _dbContext.ProductGroupMasters on pm.ProductGroupId equals pgm.ProductGroupId
                                         join sgm in _dbContext.SubGroupMasters on pm.SubGroupId equals sgm.SubGroupId
                                         join mm in _dbContext.MatrixMasters on pm.MatrixId equals mm.MatrixId
                                         join p in _dbContext.ParameterMasters on spp.ParameterMasterId equals p.ParameterMasterId
                                         join tm in _dbContext.TestMethods on spp.TestMethodID equals tm.TestMethodId
                                         join um in _dbContext.UnitMasters on spp.UnitId equals um.UnitId
                                         join um1 in _dbContext.UserMasters on spp.AnalystUserMasterID equals um1.UserMasterID
                                         join um2 in _dbContext.UserMasters on spp.ReviewerUserMasterID equals um2.UserMasterID
                                         join um3 in _dbContext.UserMasters on spp.ApproverUserMasterID equals um3.UserMasterID
                                         //join fss in _dbContext.FDStackEmissions on sc.SampleCollectionId equals fss.SampleCollectionId
                                         //join aam in _dbContext.FieldAmbientAirMonitorings on sc.SampleCollectionId equals aam.SampleCollectionId
                                         join arc in _dbContext.ARCs on sc.SampleCollectionId equals arc.SampleCollectionId
                                         into A
                                         from arc in A.DefaultIfEmpty()
                                         join pma in _dbContext.ParameterMappings on //pm.ParameterGroupId equals pma.ParameterGroupId
                                         new { ParameterGroupId = pm.ParameterGroupId, ParameterMasterId = p.ParameterMasterId, TestMethodID = spp.TestMethodID, UnitId = spp.UnitId } equals
                                         new { ParameterGroupId = pma.ParameterGroupId, ParameterMasterId = pma.ParameterMasterId, TestMethodID = (int)pma.TestMethodId, UnitId = pma.UnitId }


                                         where pm.DisciplineId == DisciplineId
                                         // where pm.SubGroupId == sgm.SubGroupId
                                         where pm.ProductGroupId == pgm.ProductGroupId
                                         where pm.SampleTypeProductId == stpm.SampleTypeProductId
                                         // where sc.IsActive == true
                                         where spp.IsActive == true
                                         where sc.SampleCollectionId == SampleId
                                         where spp.ReviewerUserMasterID == UserMasterId
                                         where spp.ParameterMasterId == ParameterMasterId

                                         select new SampleParameterReview
                                         {
                                             SampleCollectionId = sc.SampleCollectionId,
                                             SampleNo = sc.SampleNo,
                                             SampleName = sc.SampleName,
                                             UnitId = spp.UnitId,
                                             UnitName = um.Unit,
                                             ParameterMasterId = p.ParameterMasterId,
                                             ParameterGroupId = pm.ParameterGroupId,
                                             SampleNameOriginal = loc.SampleNameOriginal,
                                             SampleTypeProductName = stpm.SampleTypeProductName,
                                             ProductGroupName = pgm.ProductGroupName,
                                             SubGroupName = sgm.SubGroupName,
                                             MatrixName = mm.MatrixName,
                                             Analyst = um1.UserName,
                                             Reviewer = um2.UserName,
                                             Approver = um3.UserName,
                                             ParameterName = p.ParameterName,
                                             TestMethodId = tm.TestMethodId,
                                             TestMethodName = tm.TestMethod1,
                                             DateReceiptOfSampling = arc.ActionDate,
                                             LOD = pma.LOD,
                                             PermissibleMin = pma.PermissibleMin,
                                             PermissibleMax = pma.PermissibleMax,
                                             RegulatoryMax = pma.RegulatoryMax,
                                             RegulatoryMin = pma.RegulatoryMin

                                         }).FirstOrDefault();

                var analyst = (from u in _dbContext.UserMasters
                               join ur in _dbContext.UserRoles on u.UserMasterID equals ur.UserMasterId
                               join ud in _dbContext.UserDetails on u.UserMasterID equals ud.UserMasterID
                               join rm in _dbContext.RoleMasters on ur.RoleId equals rm.RoleId
                               where u.IsActive == true && ud.IsActive == true && u.DisciplineId == DisciplineId && rm.RoleName == "Tester"
                               select new User
                               {
                                   UserId = u.UserMasterID,
                                   Username = ud.FirstName + " " + ud.LastName

                               }).ToList();

                sampleParameterReview.Analysts = analyst;

                return sampleParameterReview;
            }
            catch (Exception ex)
            {
               
                return sampleParameterReview;
            }

        }

        public Core.Lab.TestProcessScheduleDetail GetTestProcessScheduler(Core.Lab.TestProcessScheduleDetail testProcessScheduleDetail)
        {
            try
            {
                testProcessScheduleDetail = (from apsd in _dbContext.AnalysisProcessScheduleDetails
                                             where apsd.ParameterGroupId == testProcessScheduleDetail.ParameterGroupId
                                            && apsd.ParameterMasterId == testProcessScheduleDetail.ParameterMasterId
                                            && apsd.SampleCollectionId == testProcessScheduleDetail.SampleCollectionId
                                             select new Core.Lab.TestProcessScheduleDetail
                                             {
                                                 SampleCollectionId = apsd.SampleCollectionId,
                                                 ParameterMasterId = apsd.ParameterMasterId,
                                                 ParameterGroupId = apsd.ParameterGroupId,
                                                 AnalysisProcessScheduleDetailId = apsd.AnalysisProcessScheduleDetailId,
                                                 AnalystUserMasterID = (Int32)apsd.AnalystUserMasterID,
                                                 FinalResult = apsd.FinalResult,
                                                 TotalDuration = apsd.TotalDuration,
                                                 QualityControl = apsd.QualityControl,
                                                 StartOfAnalysis = apsd.AnalysisStartAt,
                                                 EndOfAnalysis = apsd.AnalysisEndAt
                                             }).FirstOrDefault();
                return testProcessScheduleDetail;
            }
            catch (Exception ex)
            {
                
                return testProcessScheduleDetail;
            }
        }
        public IList<Core.Lab.SolutionPreparationData> GetSolutionPreparationData(Core.Lab.SolutionPreparationData solutionPreparationData)
        {
            try
            {
                IList<Core.Lab.SolutionPreparationData> objSolutionPreparationData = new List<Core.Lab.SolutionPreparationData>();

                objSolutionPreparationData = (from spd in _dbContext.SolutionPreparationDatas
                                              where spd.ParameterMasterId == solutionPreparationData.ParameterMasterId
                                             && spd.ParameterGroupId == solutionPreparationData.ParameterGroupId
                                             && spd.SampleCollectionId == solutionPreparationData.SampleCollectionId
                                              select new Core.Lab.SolutionPreparationData
                                              {
                                                  DateOfPreparation = spd.DateOfPreparation,
                                                  //NameofChemicalUsed = spd.NameofChemicalUsed,
                                                  NameOfSolution = spd.NameOfSolution,
                                                  QtyOfSolutionPrepared = spd.QtyOfSolutionPrepared,
                                                  //QualityofChemicalUsed = spd.QualityofChemicalUsed
                                              }
                                         ).Distinct().ToList();

                return objSolutionPreparationData;
            }
            catch (Exception ex)
            {
              
                return null;
            }
        }
        public IList<Core.Lab.TestingHours> GetTestingHoursData(Core.Lab.TestingHours testingData)
        {
            try
            {
                IList<Core.Lab.TestingHours> objtestingData = new List<Core.Lab.TestingHours>();

                objtestingData = (from spd in _dbContext.SampleParameterFormulaValues
                                  where spd.ParameterMasterId == testingData.ParameterMasterId
                                 && spd.ParameterGroupId == testingData.ParameterGroupId
                                 && spd.SampleCollectionId == testingData.SampleCollectionId
                                 && spd.IsFinalResult == true
                                  select new Core.Lab.TestingHours
                                  {
                                      TestingHour = spd.TestingHours,
                                      NotationValue = spd.NotationValue

                                  }
                                         ).ToList();

                return objtestingData;
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }
        public Core.Configuration.ParameterFormulaList GetFormula(Core.Configuration.ParameterFormulaList objParam)
        {
            try
            {
                objParam.FormulaList = (from pf in _dbContext.ParameterFormulas
                                        join spf in _dbContext.SampleParameterFormulaValues on
                                        //pf.ParameterFormulaID equals spf.ParameterFormulaID
                                        new { ParameterFormulaID = pf.ParameterFormulaID, SampleCollectionId = (long)objParam.SampleCollectionId } equals
                                        new { ParameterFormulaID = spf.ParameterFormulaID, SampleCollectionId = spf.SampleCollectionId }
                                        //&& spf.SampleCollectionId == objParam.SampleCollectionId
                                        into parameterSPF
                                        from pd in parameterSPF.DefaultIfEmpty()
                                            //into A
                                            //from spf in A.DefaultIfEmpty()
                                        where pf.TestMethodID == objParam.TestMethodId
                                        && pf.ParameterGroupId == objParam.ParameterGroupId
                                        && pf.ParameterMasterId == objParam.ParameterId
                                        && pf.UnitID == objParam.UnitId
                                        && pd.TestingHours == objParam.TestingHour
                                        select new Core.Configuration.FormulaList
                                        {
                                            Id = pf.ParameterFormulaID,
                                            SrNo = pf.FormulaSrNo,
                                            Notation = pf.Notation,
                                            Formula = pf.Formula,
                                            IsFDV = (bool)pf.IsFDV,
                                            DisplayName = pf.DisplayName,
                                            NotationValue = pd.NotationValue ?? "",
                                            DataType = (int)pf.DataType,
                                            Unit = (int)pf.Unit,
                                            Precision = (int)pf.Precision,
                                            ParameterGroupId = pf.ParameterGroupId,
                                            ParameterMasterId = (int)pf.ParameterMasterId
                                        }
                                      ).ToList();
                return objParam;
            }
            catch (Exception ex)
            {
               
                return objParam;
            }
        }

        public bool UpdateReviewerComment(SampleParameterReview sampleParameterReview)
        {
            try
            {
                var sampleParameter = _dbContext.SampleParameterPlannings.Where(x => x.SampleCollectionId == sampleParameterReview.SampleCollectionId &&
                 x.ParameterGroupId == sampleParameterReview.ParameterGroupId && x.ParameterMasterId == sampleParameterReview.ParameterMasterId
                 && x.TestMethodID == sampleParameterReview.TestMethodId && x.UnitId == sampleParameterReview.UnitId).FirstOrDefault();
                if (sampleParameter != null)
                {
                    sampleParameter.ReviewerApproveSts = sampleParameterReview.ReviewerApprovedStatus;
                    sampleParameter.AnalystApproveSts = sampleParameterReview.ReviewerApprovedStatus;
                    if (sampleParameter.ReviewerComment != "")
                    {
                        sampleParameter.ReviewerComment = sampleParameter.ReviewerComment + " , " + sampleParameterReview.ReviewerComments;
                    }
                    else
                    {
                        sampleParameter.ReviewerComment = sampleParameterReview.ReviewerComments;
                    }

                    if (sampleParameter.ReviewerApproveSts == 1)
                    {
                        sampleParameter.CurrentStatus = (int?)CurrentStatusEnum.Under_Approver;
                    }
                    else if (sampleParameter.ReviewerApproveSts == 0)
                    {
                        sampleParameter.CurrentStatus = (sampleParameterReview.Rejectreviewstatus == "retest") ? (int?)CurrentStatusEnum.Re_Test : (int?)CurrentStatusEnum.Re_Plan;

                        var analysisProcessScheduleDetails = _dbContext.AnalysisProcessScheduleDetails.Where(x => x.SampleCollectionId == sampleParameterReview.SampleCollectionId &&
                        x.ParameterGroupId == sampleParameterReview.ParameterGroupId && x.ParameterMasterId == sampleParameterReview.ParameterMasterId).ToList();
                        if (analysisProcessScheduleDetails != null && analysisProcessScheduleDetails.Count > 0)
                        {
                            _dbContext.AnalysisProcessScheduleDetails.RemoveRange(analysisProcessScheduleDetails);
                        }

                        var sampleParameterFiles = _dbContext.SampleParameterFiles.Where(x => x.SampleCollectionId == sampleParameterReview.SampleCollectionId &&
                        x.ParameterGroupId == sampleParameterReview.ParameterGroupId && x.ParameterMasterId == sampleParameterReview.ParameterMasterId).ToList();
                        if (sampleParameterFiles != null && sampleParameterFiles.Count > 0)
                        {
                            _dbContext.SampleParameterFiles.RemoveRange(sampleParameterFiles);
                        }

                        var sampleParameterFormulaValues = _dbContext.SampleParameterFormulaValues.Where(x => x.SampleCollectionId == sampleParameterReview.SampleCollectionId &&
                        x.ParameterGroupId == sampleParameterReview.ParameterGroupId && x.ParameterMasterId == sampleParameterReview.ParameterMasterId).ToList();
                        if (sampleParameterFormulaValues != null && sampleParameterFormulaValues.Count > 0)
                        {
                            _dbContext.SampleParameterFormulaValues.RemoveRange(sampleParameterFormulaValues);
                        }

                        var solutionPreparationDatas = _dbContext.SolutionPreparationDatas.Where(x => x.SampleCollectionId == sampleParameterReview.SampleCollectionId &&
                        x.ParameterGroupId == sampleParameterReview.ParameterGroupId && x.ParameterMasterId == sampleParameterReview.ParameterMasterId).ToList();
                        if (solutionPreparationDatas != null && solutionPreparationDatas.Count > 0)
                        {
                            _dbContext.SolutionPreparationDatas.RemoveRange(solutionPreparationDatas);
                        }
                    }

                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }
        public long AddNotification(string Msg, string RoleName, SampleParameterReview sampleParameterReview)
        {
            try
            {
                var notific = new NotificationDetail()
                {
                    RoleId = null,
                    NotificationName = sampleParameterReview.SampleNameOriginal,
                    Message = Msg,
                    DateTime = DateTime.Now,
                    IsActive = true,
                    EnteredBy = 0,
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
        public IList<SampleParameterFileInfo> GetParameterFileInfo(SampleParameterFileInfo sampleParameterFileInfo)
        {
            try
            {
                IList<SampleParameterFileInfo> sampleParameterFileInfos = new List<SampleParameterFileInfo>();
                sampleParameterFileInfos = (from spf in _dbContext.SampleParameterFiles
                                            where spf.ParameterMasterId == sampleParameterFileInfo.ParameterMasterId
                                             && spf.ParameterGroupId == sampleParameterFileInfo.ParameterGroupId
                                             && spf.SampleCollectionId == sampleParameterFileInfo.SampleCollectionId
                                            select new SampleParameterFileInfo
                                            {
                                                FileName = spf.FileName,
                                                FileID = spf.SampleParameterFilesID
                                            }
                                         ).ToList();
                return sampleParameterFileInfos;
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
    }
}