using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Repository;

namespace LIMS_DEMO.DAL.Configuration
{
    public class ParameterMasterDAL : IParameterMaster
    {
        readonly LIMSEntities _dbContext;
        readonly LIMSEntities _dbContext1;
        public ParameterMasterDAL()
        {
            _dbContext = new LIMSEntities();
            _dbContext1 = new LIMSEntities();
        }
        public string AddParameter(ParameterMasterEntity parameterMasterEntity)
        {
            try
            {
                _dbContext.ParameterMasters.Add(new ParameterMaster()
                {
                    ParameterMasterId = parameterMasterEntity.ParameterMasterId,
                    ParameterName = parameterMasterEntity.Parameter,
                    //ProductGroupCode = productSubMatrixEntity.Code,
                    //Description = configurationEntity.Description,
                    //IsSampleToBeDisposed = configurationEntity.IsSampleToBeDisposed,
                    //SampleRetentionPeriod = configurationEntity.SampleRetentionPeriod,
                    IsActive = parameterMasterEntity.IsActive,
                    EnteredBy = parameterMasterEntity.EnteredBy,
                    EnteredDate = parameterMasterEntity.EnteredDate,
                    ModifiedBy = parameterMasterEntity.ModifiedBy

                });
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }

        }

        public string UpdateParameter(ParameterMasterEntity parameterMasterEntity)
        {
            try
            {
                var ParameterMaster = _dbContext.ParameterMasters.Find(parameterMasterEntity.ParameterMasterId);
                ParameterMaster.ParameterMasterId = parameterMasterEntity.ParameterMasterId;
                ParameterMaster.ParameterName = parameterMasterEntity.Parameter;
                ParameterMaster.IsActive = parameterMasterEntity.IsActive;
                ParameterMaster.EnteredBy = parameterMasterEntity.EnteredBy;
                ParameterMaster.EnteredDate = parameterMasterEntity.EnteredDate;

                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }

        }

        public List<ParameterMasterEntity> GetParameterList()
        {
            try
            {

                return (from e in _dbContext.ParameterMasters
                        where e.IsActive == true
                        select new ParameterMasterEntity()
                        {
                            Parameter = e.ParameterName,
                            ParameterMasterId = e.ParameterMasterId,
                            IsActive = e.IsActive,

                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public ParameterMasterEntity GetDetailsParameter(int ParameterMasterId)
        {
            return _dbContext.ParameterMasters.Where(u => u.ParameterMasterId == ParameterMasterId).Select(u => new ParameterMasterEntity()
            {
                ParameterMasterId = u.ParameterMasterId,
                Parameter = u.ParameterName,
                IsActive = u.IsActive
            }).FirstOrDefault();
        }

        public string AddParameterMaster(ParameterMasterEntity parametermasterEntity)
        {
            try
            {
                _dbContext.ParameterMappings.Add(new ParameterMapping()
                {
                    ParameterMasterId = parametermasterEntity.ParameterMasterId,
                    ParameterGroupId = parametermasterEntity.ParameterGroupId,
                    TestMethodId = parametermasterEntity.TestMethodId,
                    UnitId = parametermasterEntity.UnitId,
                    InField = parametermasterEntity.IsField,
                    LOD = parametermasterEntity.LOD,
                    MaxTestingRange = parametermasterEntity.MaxRange,
                    PermissibleMin = parametermasterEntity.PermissibleMin,
                    PermissibleMax = parametermasterEntity.PermissibleMax,
                    RegulatoryMin = parametermasterEntity.RegulatoryMin,
                    RegulatoryMax = parametermasterEntity.RegulatoryMax,
                    IsNABLAccredited = parametermasterEntity.IsNABLAccredited,
                    IsActive = parametermasterEntity.IsActive,
                    EnteredBy = parametermasterEntity.EnteredBy,
                    EnteredDate = parametermasterEntity.EnteredDate,
                    ModifiedBy = parametermasterEntity.ModifiedBy

                });

                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }

        }
        public int AddParameterGroup(ParameterMasterEntity parameterMasterEntity)
        {
            try
            {
                var parameterGrpMaster = new ParameterGroupMaster()
                {
                    ParameterGroupId = parameterMasterEntity.ParameterGroupId,
                    SampleTypeProductId = parameterMasterEntity.SampleTypeProductId,
                    ProductGroupId = parameterMasterEntity.ProductGroupId,
                    SubGroupId = parameterMasterEntity.SubGroupId,
                    MatrixId = parameterMasterEntity.MatrixId,
                   
                    IsSetPCBLimit = parameterMasterEntity.IsIndustrySpecified,
                    IsActive = parameterMasterEntity.IsActive,
                    EnteredBy = parameterMasterEntity.EnteredBy,
                    EnteredDate = DateTime.Now
                };
                _dbContext.ParameterGroupMasters.Add(parameterGrpMaster);
                _dbContext.SaveChanges();
                return parameterGrpMaster.ParameterGroupId;

            }
            catch (Exception ex)
            {
                return 1;
            }

        }

        public ParameterMasterEntity GetParameterGroupDetails(int ParameterGroupId)
        {
            return _dbContext.ParameterGroupMasters.Where(u => u.ParameterGroupId == ParameterGroupId).Select(u => new ParameterMasterEntity()
            {
                ParameterGroupId = u.ParameterGroupId,
                ProductGroupId = u.ProductGroupId,
                SubGroupId = u.SubGroupId,
                MatrixId = u.MatrixId,
                SampleTypeProductId = (Int32)u.SampleTypeProductId,
               
                IsIndustrySpecified = u.IsSetPCBLimit,
                IsActive = u.IsActive,
            }).FirstOrDefault();
        }
        public long GetParameterMasterMappingDetail(ParameterMasterEntity model)
        {
            var a = _dbContext.ParameterMappings.Where(u => u.ParameterGroupId == model.ParameterGroupId & u.ParameterMasterId == model.ParameterMasterId & u.TestMethodId == model.TestMethodId & u.UnitId == model.UnitId).Select(u => new ParameterMasterEntity()
            {

                ParameterMappingId = u.ParameterMappingId,

            }).FirstOrDefault();
            if (a == null)
            {
                return 0;
            }
            else
            {
                return a.ParameterMappingId;

            }
        }
        public ParameterMasterEntity GetDetails(int ParameterMappingId)
        {
            return _dbContext.ParameterMappings.Where(u => u.ParameterMappingId == ParameterMappingId).Select(u => new ParameterMasterEntity()
            {
                ParameterMappingId = u.ParameterMappingId,
                ParameterMasterId = u.ParameterMasterId,
                ParameterGroupId = u.ParameterGroupId,
                TestMethodId = u.TestMethodId,
                UnitId = u.UnitId,
                IsField = u.InField,
                IsNABLAccredited = u.IsNABLAccredited,
                LOD = u.LOD,
                RegulatoryMin = u.RegulatoryMin,
                RegulatoryMax = u.RegulatoryMax,
                PermissibleMin = u.PermissibleMin,
                PermissibleMax = u.PermissibleMax,
                IsActive = u.IsActive,
            }).FirstOrDefault();
        }
        public string Update(ParameterMasterEntity parameterMasterEntity)
        {
            try
            {
                var parameterGroup = _dbContext.ParameterGroupMasters.Find(parameterMasterEntity.ParameterGroupId);
                parameterGroup.ProductGroupId = parameterMasterEntity.ProductGroupId;
                parameterGroup.SubGroupId = parameterMasterEntity.SubGroupId;
                parameterGroup.MatrixId = parameterMasterEntity.MatrixId;
                parameterGroup.SampleTypeProductId = parameterMasterEntity.SampleTypeProductId;
               
                parameterGroup.IsSetPCBLimit = parameterMasterEntity.IsIndustrySpecified;

                var parameterMapping = _dbContext1.ParameterMappings.Find(parameterMasterEntity.ParameterMappingId);
                //parameterMapping.ParameterMasterId = parameterMasterEntity.ParameterMasterId;
                parameterMapping.ParameterGroupId = parameterMasterEntity.ParameterGroupId;
                parameterMapping.TestMethodId = parameterMasterEntity.TestMethodId;
                parameterMapping.UnitId = parameterMasterEntity.UnitId;
                parameterMapping.LOD = parameterMasterEntity.LOD;
                parameterMapping.MaxTestingRange = parameterMasterEntity.MaxRange;
                parameterMapping.DisciplineId = parameterMasterEntity.DisciplineId;
                parameterMapping.IsNABLAccredited = parameterMasterEntity.IsNABLAccredited;
                parameterMapping.InField = parameterMasterEntity.IsField;
                parameterMapping.PermissibleMax = parameterMasterEntity.PermissibleMax;
                parameterMapping.PermissibleMin = parameterMasterEntity.PermissibleMin;
                parameterMapping.RegulatoryMax = parameterMasterEntity.RegulatoryMax;
                parameterMapping.RegulatoryMin = parameterMasterEntity.RegulatoryMin;
                parameterMapping.IsActive = parameterMasterEntity.IsActive;
                _dbContext.SaveChanges();
                _dbContext1.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public ParameterMasterEntity GetParameter(int ParameterMasterId)
        {
            return _dbContext.ParameterMasters.Where(u => u.ParameterMasterId == ParameterMasterId).Select(u => new ParameterMasterEntity()
            {
                ParameterMasterId = u.ParameterMasterId,
                Parameter = u.ParameterName,
                IsActive = u.IsActive,
            }).FirstOrDefault();
        }
        public List<ParameterMasterEntity> GetParameterMasterList()
        {
            try
            {

                return (from pm in _dbContext.ParameterMappings
                        where pm.IsActive == true
                        from p in _dbContext.ParameterMasters
                        where pm.ParameterMasterId == p.ParameterMasterId && p.IsActive == true
                        from pgm in _dbContext.ParameterGroupMasters
                        where pm.ParameterGroupId == pgm.ParameterGroupId && pgm.IsActive == true
                        from u in _dbContext.UnitMasters
                        where pm.UnitId == u.UnitId && u.IsActive == true
                        from tm in _dbContext.TestMethods
                        where pm.TestMethodId == tm.TestMethodId && tm.IsActive == true
                        from pg in _dbContext.ProductGroupMasters
                        where pgm.ProductGroupId == pg.ProductGroupId && pg.IsActive == true
                        from sg in _dbContext.SubGroupMasters
                        where pgm.SubGroupId == sg.SubGroupId && sg.IsActive == true
                        from m in _dbContext.MatrixMasters
                        where pgm.MatrixId == m.MatrixId && m.IsActive == true
                        from d in _dbContext.DisciplineMasters
                        where pm.DisciplineId == d.DisciplineId && d.IsActive == true
                        select new ParameterMasterEntity()
                        {
                            ParameterMappingId = pm.ParameterMappingId,
                            ParameterGroupId = pm.ParameterGroupId,
                            ParameterMasterId = pm.ParameterMasterId,
                            Parameter = p.ParameterName,
                            MaxRange = pm.MaxTestingRange,
                            DisciplineId = pm.DisciplineId,
                            DisciplineName = d.Discipline,
                            ProductGroupId = pgm.ProductGroupId,
                            ProductGroupName = pg.ProductGroupName,
                            SubGroupId = pgm.SubGroupId,
                            SampleTypeProductId = pgm.SampleTypeProductId,
                            //SampleTypeProductName = stp.SampleTypeProductName,
                            SubGroupName = sg.SubGroupName,
                            MatrixId = m.MatrixId,
                            MatrixName = m.MatrixName,
                            UnitId = pm.UnitId,
                            UnitName = u.Unit,
                            TestMethodId = pm.TestMethodId,
                            TestMethodName = tm.TestMethod1,
                            //IsNABLAccredited= reqyuired to add edmx for this column
                            Charges = p.Charges,
                            LOD = pm.LOD,
                            PermissibleMin = pm.PermissibleMin,
                            PermissibleMax = pm.PermissibleMax,
                            RegulatoryMax = pm.RegulatoryMax,
                            RegulatoryMin = pm.RegulatoryMin,
                            IsIndustrySpecified = pgm.IsSetPCBLimit,
                            IsActive = pm.IsActive,

                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int GetParameterDetails(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId)
        {
            int ParameterGroupId = 0;
            var mytestMethodList = (from pgm in _dbContext.ParameterGroupMasters

                                    where pgm.SampleTypeProductId == SampleTypeProductId && pgm.ProductGroupId == ProductgroupId
                                    && pgm.SubGroupId == SubgroupId && pgm.MatrixId == MatrixId
                                    select new ParameterMasterEntity
                                    {
                                        ParameterGroupId = pgm.ParameterGroupId
                                    }).FirstOrDefault();

            if (mytestMethodList != null)
            {
                ParameterGroupId = mytestMethodList.ParameterGroupId;
            }
            return ParameterGroupId;
        }

        public string DeleteParameterMaster(int ParameterMappingId)
        {
            try
            {
                var ParameterMaster = _dbContext.ParameterMappings.Find(ParameterMappingId);
                ParameterMaster.IsActive = false;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string DeleteParameter(int ParameterMasterId)
        {
            try
            {
                var Parameter = _dbContext.ParameterMasters.Find(ParameterMasterId);
                Parameter.IsActive = false;
                _dbContext.SaveChanges();
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
    }
}