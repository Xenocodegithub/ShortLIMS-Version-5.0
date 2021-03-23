using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Repository;

namespace LIMS_DEMO.DAL.Configuration
{
    public class UnitMasterDAL:IUnitMaster
    {
        readonly LIMSEntities _dbContext;
        readonly LIMSEntities _dbContext1;

        public UnitMasterDAL()
        {
            _dbContext = new LIMSEntities();
            _dbContext1 = new LIMSEntities();

        }
        public UnitMasterEntity GetDetails(int UnitId)
        {
            return _dbContext.UnitMasters.Where(u => u.UnitId == UnitId).Select(u => new UnitMasterEntity()
            {
                UnitId = u.UnitId,
                Unit = u.Unit,
                IsActive = u.IsActive
            }).FirstOrDefault();
        }
        public string AddUnitMaster(UnitMasterEntity unitMasterEntity)
        {
            try
            {
                _dbContext.UnitMasters.Add(new UnitMaster()
                {
                    UnitId = unitMasterEntity.UnitId,
                    Unit = unitMasterEntity.Unit,
                    IsActive = unitMasterEntity.IsActive,
                    EnteredBy = unitMasterEntity.EnteredBy,
                    EnteredDate = unitMasterEntity.EnteredDate,
                    ModifiedBy = unitMasterEntity.ModifiedBy

                });
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }

        }
        public string UpdateUnitMaster(UnitMasterEntity unitMasterEntity)
        {
            try
            {
                var UnitMaster = _dbContext.UnitMasters.Find(unitMasterEntity.UnitId);
                UnitMaster.UnitId = unitMasterEntity.UnitId;
                UnitMaster.Unit = unitMasterEntity.Unit;
                UnitMaster.IsActive = unitMasterEntity.IsActive;
                UnitMaster.EnteredBy = unitMasterEntity.EnteredBy;
                UnitMaster.EnteredDate = unitMasterEntity.EnteredDate;
                UnitMaster.ModifiedBy = unitMasterEntity.ModifiedBy;
                _dbContext.SaveChanges();

                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string DeleteUnitMaster(int UnitId)
        {
            try
            {
                var UnitMaster = _dbContext.UnitMasters.Find(UnitId);
                UnitMaster.IsActive = false;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public List<UnitMasterEntity> GetUnitList()

        {
            try
            {

                return (from e in _dbContext.UnitMasters

                        where e.IsActive == true
                        select new UnitMasterEntity()
                        {
                            UnitId = e.UnitId,
                            Unit = e.Unit,
                            IsActive = e.IsActive,

                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}