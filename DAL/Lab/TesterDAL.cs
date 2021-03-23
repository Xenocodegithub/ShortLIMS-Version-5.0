using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.Lab;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Common;

namespace LIMS_DEMO.DAL.Lab
{
    public class TesterDAL : ITester
    {
        readonly LIMSEntities _dbContext;

        public TesterDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public IList<Core.Lab.SolutionPreparationData> GetChemicalUsedData(Core.Lab.SolutionPreparationData solutionPreparationData)
        {
            try
            {
                IList<Core.Lab.SolutionPreparationData> objSolutionPreparationData = new List<Core.Lab.SolutionPreparationData>();
                objSolutionPreparationData = (from spd in _dbContext.SolutionPreparationDatas
                                              where spd.ParameterMasterId == solutionPreparationData.ParameterMasterId
                                             && spd.ParameterGroupId == solutionPreparationData.ParameterGroupId
                                             && spd.SampleCollectionId == solutionPreparationData.SampleCollectionId
                                             && spd.NameOfSolution == solutionPreparationData.NameOfSolution
                                              select new Core.Lab.SolutionPreparationData
                                              {
                                                  //DateOfPreparation = spd.DateOfPreparation,
                                                  NameofChemicalUsed = spd.NameofChemicalUsed,
                                                  NameOfSolution = spd.NameOfSolution,
                                                  //QtyOfSolutionPrepared = spd.QtyOfSolutionPrepared
                                                  QualityofChemicalUsed = spd.QualityofChemicalUsed
                                              }
                                         ).ToList();
                return objSolutionPreparationData;
            }
            catch (Exception ex)
            {
               
                return null;
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
                                                  QtyOfSolutionPrepared = spd.QtyOfSolutionPrepared
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
        public IList<TesterEntity> GetList(int DisciplineId, int UserMasterId)
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
                              where spp.AnalystUserMasterID == UserMasterId
                              //where pm.SubGroupId == sgm.SubGroupId
                              //where pm.ProductGroupId == pgm.ProductGroupId
                              //       where sc.IsActive == true
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
                IList<TesterEntity> testerEntities = new List<TesterEntity>();

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

                    var ForAnalyseCount = (from sp in _dbContext.SampleParameterPlannings
                                           where sp.SampleCollectionId == item.SampleCollectionId && ((int)sp.CurrentStatus == 1 || (int)sp.CurrentStatus == 4)
                                           group sp by sp.SampleCollectionId into f
                                           select new { cnt = f.Count() }).FirstOrDefault();


                    TesterEntity testerEntity = new TesterEntity();
                    testerEntity.SampleCollectionId = item.SampleCollectionId;
                    testerEntity.SampleNo = item.SampleNo;
                    testerEntity.SampleNameOriginal = item.SampleNameOriginal;
                    testerEntity.SampleTypeProductName = item.SampleTypeProductName;
                    testerEntity.ProductGroupName = item.ProductGroupName;
                    testerEntity.SubGroupName = item.SubGroupName;
                    testerEntity.MatrixName = item.MatrixName;
                    testerEntity.StatusName = item.StatusName;
                    testerEntity.NotApproveCount = (TotalCount == null ? 0 : TotalCount.cnt) - (ApprovedCount == null ? 0 : ApprovedCount.cnt);
                    testerEntity.AnalysisActive = ForAnalyseCount == null ? 0 : ForAnalyseCount.cnt;
                    testerEntities.Add(testerEntity);
                }
                return testerEntities;
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }
        public SampleParameterInfo GetParameterInfo(int SampleId, int DisciplineId, int UserMasterId)
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
                              //    where sc.IsActive == false
                              where spp.IsActive == true
                              where (int)spp.CurrentStatus != 6
                              select new TesterEntity
                              {
                                  SampleCollectionId = sc.SampleCollectionId,
                                  SampleNo = sc.SampleNo,
                                  SampleNameOriginal = loc.SampleNameOriginal,
                                  SampleTypeProductName = stpm.SampleTypeProductName,
                                  ProductGroupName = pgm.ProductGroupName,
                                  SubGroupName = sgm.SubGroupName,
                                  MatrixName = mm.MatrixName,
                                  StatusName = sm.StatusName

                              }).FirstOrDefault();

                SampleParameterInfo parameterInfo = new SampleParameterInfo();
                parameterInfo.SampleDetails = result;
                var parameterResult = (from sc in _dbContext.SampleCollections
                                       join epd in _dbContext.EnquiryParameterDetails on sc.EnquirySampleID equals epd.EnquirySampleID
                                       join esd in _dbContext.EnquirySampleDetails on sc.EnquirySampleID equals esd.EnquirySampleID
                                       join ed in _dbContext.EnquiryDetails on esd.EnquiryDetailId equals ed.EnquiryDetailId
                                       join stpm in _dbContext.SampleTypeProductMasters on ed.SampleTypeProductId equals stpm.SampleTypeProductId
                                       //join pgm in _dbContext.ProductGroupMasters on ed.ProductGroupId equals pgm.ProductGroupId

                                       join pm in _dbContext.ParameterMappings on epd.ParameterMappingId equals pm.ParameterMappingId
                                       join p in _dbContext.ParameterMasters on pm.ParameterMasterId equals p.ParameterMasterId
                                       join um in _dbContext.UnitMasters on epd.UnitId equals um.UnitId
                                       // join tm in _dbContext.TestMethods on epd.TestMethodId equals tm.TestMethodId
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
                                       && spp.AnalystUserMasterID == UserMasterId

                                       select new LIMS_DEMO.Core.Lab.ParameterInfos
                                       {
                                           ParameterGroupId = pm.ParameterGroupId,
                                           ParameterMasterId = p.ParameterMasterId,
                                           ParameterName = p.ParameterName,
                                           UnitId = um.UnitId,
                                           Unit = um.Unit,
                                           ReviewerComment = spp.ReviewerComment,
                                           TestMethodId = pd.TestMethodId,
                                           TestMethod = pd.TestMethod1,
                                           AnalystUserMasterID = pd == null ? 0 : spp.AnalystUserMasterID,
                                           IsActive = pd == null ? true : pd.IsActive,
                                           CurrentStatus = spp.CurrentStatus
                                       }).ToList();

                parameterInfo.ParameterInfos = parameterResult;
                return parameterInfo;
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }

        public long SaveSolutionPreparationData(Core.Lab.SolutionPreparationData solutionPreparationData)
        {
            try
            {
                SolutionPreparationData obj = new SolutionPreparationData()
                {
                    SampleCollectionId = solutionPreparationData.SampleCollectionId,
                    ParameterGroupId = solutionPreparationData.ParameterGroupId,
                    ParameterMasterId = solutionPreparationData.ParameterMasterId,
                    IsActive = true,
                    DateOfPreparation = solutionPreparationData.DateOfPreparation,
                    NameofChemicalUsed = solutionPreparationData.NameofChemicalUsed,
                    NameOfSolution = solutionPreparationData.NameOfSolution,
                    QtyOfSolutionPrepared = solutionPreparationData.QtyOfSolutionPrepared,
                    QualityofChemicalUsed = solutionPreparationData.QualityofChemicalUsed,
                    EnteredBy = solutionPreparationData.EnteredBy,
                    EnteredDate = DateTime.Now

                };
                _dbContext.SolutionPreparationDatas.Add(obj);
                _dbContext.SaveChanges();
                return obj.SolutionPreparationDataId;
            }
            catch (Exception ex)
            {
                
                return 0;
            }
        }
        public SampleParameterAnalysis GetSampleParameterAnalysis(int SampleId, int DisciplineId, int UserMasterId, long ParameterMasterId)
        {
            SampleParameterAnalysis sampleParameterAnalysis = new SampleParameterAnalysis();
            try
            {
                sampleParameterAnalysis = (from spp in _dbContext.SampleParameterPlannings
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
                                           //where sc.IsActive == false
                                           where spp.IsActive == true
                                           where sc.SampleCollectionId == SampleId
                                           where spp.AnalystUserMasterID == UserMasterId
                                           where spp.ParameterMasterId == ParameterMasterId
                                           select new SampleParameterAnalysis
                                           {
                                               SampleCollectionId = sc.SampleCollectionId,
                                               SampleNo = sc.SampleNo,
                                               UnitId = spp.UnitId,
                                               UnitName = um.Unit,
                                               //FDStackEmissionId = fss.FDStackEmissionId,
                                               //FieldId = aam.FieldId,
                                               ParameterMasterId = p.ParameterMasterId,
                                               ParameterGroupId = pm.ParameterGroupId,
                                               ProductGroupName = pgm.ProductGroupName,
                                               SampleNameOriginal = loc.SampleNameOriginal,
                                               SampleTypeProductName = stpm.SampleTypeProductName,
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
                                               RegulatoryMin = pma.RegulatoryMin,
                                               ReviewerComment = spp.ReviewerComment
                                           }).FirstOrDefault();
                return sampleParameterAnalysis;
            }
            catch (Exception ex)
            {
               
                return sampleParameterAnalysis;
            }
        }
        public Core.Configuration.ParameterFormulaList GetFormula(Core.Configuration.ParameterFormulaList objParam)
        {
            try
            {
                objParam.FormulaList = (from pf in _dbContext.ParameterFormulas
                                            //join spf in _dbContext.SampleParameterFormulaValues on
                                            ////pf.ParameterFormulaID equals spf.ParameterFormulaID
                                            //new { ParameterFormulaID = pf.ParameterFormulaID, SampleCollectionId = (long)objParam.SampleCollectionId } equals
                                            //new { ParameterFormulaID = spf.ParameterFormulaID, SampleCollectionId = spf.SampleCollectionId }
                                            //&& spf.SampleCollectionId == objParam.SampleCollectionId
                                            //into parameterSPF
                                            //from pd in parameterSPF.DefaultIfEmpty()
                                            //into A
                                            //from spf in A.DefaultIfEmpty()
                                        where pf.TestMethodID == objParam.TestMethodId
                                        && pf.ParameterGroupId == objParam.ParameterGroupId
                                        && pf.ParameterMasterId == objParam.ParameterId
                                        && pf.UnitID == objParam.UnitId

                                        select new Core.Configuration.FormulaList
                                        {
                                            Id = pf.ParameterFormulaID,
                                            SrNo = pf.FormulaSrNo,
                                            Notation = pf.Notation,
                                            Formula = pf.Formula,
                                            IsFDV = (bool)pf.IsFDV,
                                            DisplayName = pf.DisplayName,
                                            NotationValue = pf.NotationValue ?? "",
                                            DataType = (int)pf.DataType,
                                            Unit = (int)pf.Unit,
                                            Precision = (int)pf.Precision,
                                            ParameterGroupId = pf.ParameterGroupId,
                                            ParameterMasterId = (int)pf.ParameterMasterId
                                        }
                                      ).Distinct().ToList();
                return objParam;
            }
            catch (Exception ex)
            {
                return objParam;
            }
        }
        public bool UpdateFinalResult(Core.Lab.AnalysisProcessScheduleDetail analysisProcessScheduleDetail, bool isSubmitted)
        {
            try
            {
                if (isSubmitted)
                {
                    var spp = _dbContext.SampleParameterPlannings.Where(x => x.SampleCollectionId == analysisProcessScheduleDetail.SampleCollectionId &&
                    x.ParameterGroupId == analysisProcessScheduleDetail.ParameterGroupId && x.ParameterMasterId == analysisProcessScheduleDetail.ParameterMasterId
                    ).FirstOrDefault();
                    if (spp != null)
                    {
                        spp.AnalystApproveSts = 1;
                        // spp.ReviewerApproveSts = 1;
                        spp.CurrentStatus = (int?)CurrentStatusEnum.Under_Reviewer;
                        spp.ModifiedDate = DateTime.Now;
                        _dbContext.SaveChanges();

                    }
                }
                else
                {
                    _dbContext.AnalysisProcessScheduleDetails.Add(new AnalysisProcessScheduleDetail()
                    {
                        SampleCollectionId = analysisProcessScheduleDetail.SampleCollectionId,
                        ParameterGroupId = analysisProcessScheduleDetail.ParameterGroupId,
                        ParameterMasterId = analysisProcessScheduleDetail.ParameterMasterId,
                        AnalystUserMasterID = analysisProcessScheduleDetail.AnalystUserMasterID,
                        FinalResult = analysisProcessScheduleDetail.FinalResult,
                        TotalDuration = analysisProcessScheduleDetail.TotalDuration,
                        QualityControl = analysisProcessScheduleDetail.QualityControl,
                        AnalysisStartAt = analysisProcessScheduleDetail.AnalysisStartAt,
                        AnalysisEndAt = analysisProcessScheduleDetail.AnalysisEndAt,
                        IsActive = analysisProcessScheduleDetail.IsActive,
                        EnteredDate = analysisProcessScheduleDetail.EnteredDate,
                    });
                    _dbContext.SaveChanges();
                }


            }

            catch (Exception ex)
            {
                
                return false;
            }
            return true;
        }
        public bool UpdateCalculatedValue(List<Core.Configuration.FormulaList> formulaList, int SampleCollectionId, int UserMasterId, string TestingHours)
        {
            try
            {
                if (formulaList.Count > 0)
                {
                    var ParameterGroupId = formulaList[0].ParameterGroupId;
                    var ParameterMasterId = formulaList[0].ParameterMasterId;
                    var ParameterFormulaID = formulaList[0].Id;
                    var formulaLists = (from pf in _dbContext.SampleParameterFormulaValues
                                        where pf.IsActive == true && pf.ParameterMasterId == ParameterMasterId
                                        && pf.ParameterGroupId == ParameterGroupId
                                        && pf.SampleCollectionId == SampleCollectionId
                                        select pf).ToList();
                    //if (formulaLists != null && formulaLists.Count > 0)
                    //{
                    //    _dbContext.SampleParameterFormulaValues.RemoveRange(formulaLists);
                    //    _dbContext.SaveChanges();
                    //}

                    var countx = formulaList.Count;
                    var i = 0;
                    Boolean state = false;
                    foreach (var item in formulaList)
                    {
                        i = i + 1;
                        if (i == countx)
                        {
                            state = true;
                        }
                        else
                        {
                            state = false;
                        }
                        var result = new SampleParameterFormulaValue()
                        {
                            SampleCollectionId = SampleCollectionId,
                            ParameterFormulaID = item.Id,
                            ParameterGroupId = item.ParameterGroupId,
                            ParameterMasterId = item.ParameterMasterId,
                            NotationValue = item.NotationValue,
                            IsFDV = item.IsFDV,
                            IsActive = true,
                            TestingHours = TestingHours,
                            EnteredDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            EnteredBy = UserMasterId,
                            ModifiedBy = UserMasterId,
                            IsFinalResult = state
                        };
                        _dbContext.SampleParameterFormulaValues.Add(result);
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
        public long AddNotification(string Msg, string RoleName, Core.Lab.AnalysisProcessScheduleDetail analysisProcessScheduleDetail)
        {
            try
            {
                var notific = new NotificationDetail()
                {
                    RoleId = null,
                    NotificationName = analysisProcessScheduleDetail.SampleNameOriginal,
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
        public bool UpdateAvgValue(int SampleCollectionId, int ParameterMasterId, int ParameterGroupId, string[] avgResult)
        {
            try
            {
                for (var i = 0; i <= 2; i++)
                {
                    SampleParameterFormulaValuesTemp obj = new SampleParameterFormulaValuesTemp()
                    {

                        SampleCollectionId = SampleCollectionId,
                        ParameterGroupId = ParameterGroupId,
                        ParameterMasterId = ParameterMasterId,
                        NotationValueTemp = avgResult[i]


                    };
                    _dbContext.SampleParameterFormulaValuesTemps.Add(obj);
                    _dbContext.SaveChanges();
                }

                return true;
            }

            catch (Exception ex)
            {
               
                return false;
            }
        }
        public Core.Configuration.ParameterFormulaList GetFinalResultRows(Core.Configuration.ParameterFormulaList objParam)
        {
            try
            {
                objParam.FormulaList = (from pf in _dbContext.ParameterFormulas
                                        join spf in _dbContext.SampleParameterFormulaValues on
                                        pf.ParameterFormulaID equals spf.ParameterFormulaID

                                        where pf.TestMethodID == objParam.TestMethodId
                                        && pf.ParameterGroupId == objParam.ParameterGroupId
                                        && pf.ParameterMasterId == objParam.ParameterId
                                        && pf.UnitID == objParam.UnitId
                                        && spf.SampleCollectionId == objParam.SampleCollectionId
                                        && spf.IsFinalResult == true

                                        select new Core.Configuration.FormulaList
                                        {
                                            NotationValue = spf.NotationValue ?? ""
                                        }
                                        ).Distinct().ToList();
                return objParam;
            }
            catch (Exception ex)
            {
                
                return objParam;
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
                                                 EndOfAnalysis = apsd.AnalysisEndAt,

                                             }).FirstOrDefault();
                return testProcessScheduleDetail;

            }
            catch (Exception ex)
            {
                
                return testProcessScheduleDetail;
            }
        }
        public SampleParameterAnalysis GetFDData(int SampleId)
        {
            SampleParameterAnalysis sampleParameterAnalysis = new SampleParameterAnalysis();
            try
            {
                var fdstack = _dbContext.FDStackEmissions.Where(e => e.SampleCollectionId == SampleId).FirstOrDefault();

                var fdair = _dbContext.FieldAmbientAirMonitorings.Where(e => e.SampleCollectionId == SampleId).FirstOrDefault();
                if (fdair != null)
                {
                    sampleParameterAnalysis.FieldId = fdair.FieldId;
                    return sampleParameterAnalysis;
                }
                if (fdstack != null)
                {
                    sampleParameterAnalysis.FDStackEmissionId = fdstack.FDStackEmissionId;
                    return sampleParameterAnalysis;
                }


                return sampleParameterAnalysis;
            }
            catch (Exception ex)
            {
                
                return sampleParameterAnalysis;
            }
        }
        public bool SaveAnalysisProcessFileInfo(List<SampleParameterFileInfo> sampleParameterFileInfos)
        {
            try
            {
                foreach (var item in sampleParameterFileInfos)
                {
                    SampleParameterFile sampleParameterFile = new SampleParameterFile()
                    {
                        FileName = item.FileName,
                        IsActive = item.IsActive,
                        UserMasterID = item.UserMasterID,
                        EnteredBy = item.UserMasterID,
                        ParameterGroupId = item.ParameterGroupId,
                        ParameterMasterId = item.ParameterMasterId,
                        SampleCollectionId = item.SampleCollectionId,
                        EnteredDate = DateTime.Now

                    };
                    _dbContext.SampleParameterFiles.Add(sampleParameterFile);
                    _dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
               
                return false;
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
        public bool DeleteFile(string FileIdToDelete)
        {
            try
            {
                var fl = _dbContext.SampleParameterFiles.Find(Convert.ToInt64(FileIdToDelete));
                _dbContext.SampleParameterFiles.Remove(fl);
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