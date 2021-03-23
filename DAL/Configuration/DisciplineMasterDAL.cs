using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Repository;

namespace LIMS_DEMO.DAL.Configuration
{
    public class DisciplineMasterDAL:IDiscipline
    {
        readonly LIMSEntities _dbContext;
        public DisciplineMasterDAL()
        {
            _dbContext = new LIMSEntities();
        }

        public DisciplineMasterEntity GetDetailsDiscipline(int DisciplineId)
        {
            return _dbContext.DisciplineMasters.Where(u => u.DisciplineId == DisciplineId).Select(u => new DisciplineMasterEntity()
            {
                DisciplineId = u.DisciplineId,
                Discipline = u.Discipline,
                IsActive = u.IsActive
            }).FirstOrDefault();
        }
        public string AddDiscipline(DisciplineMasterEntity disciplineMasterEntity)
        {
            try
            {
                _dbContext.DisciplineMasters.Add(new DisciplineMaster()
                {
                   
                    Discipline = disciplineMasterEntity.Discipline,
                    IsActive = disciplineMasterEntity.IsActive,
                    EnteredBy = disciplineMasterEntity.EnteredBy,
                    EnteredDate = disciplineMasterEntity.EnteredDate,
                    ModifiedBy = disciplineMasterEntity.ModifiedBy
                    
                }); 

                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public List<DisciplineMasterEntity> GetDisciplineList()
        {
            try
            {

                return (from e in _dbContext.DisciplineMasters
                        where e.IsActive == true
                        select new DisciplineMasterEntity()
                        {
                            Discipline = e.Discipline,
                            DisciplineId = e.DisciplineId,
                            IsActive = e.IsActive,

                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string UpdateDiscipline(DisciplineMasterEntity disciplineMasterEntity)
        {
            try
            {
                var DisciplineMaster = _dbContext.DisciplineMasters.Find(disciplineMasterEntity.DisciplineId);
                DisciplineMaster.DisciplineId = disciplineMasterEntity.DisciplineId;
                DisciplineMaster.Discipline = disciplineMasterEntity.Discipline;
                DisciplineMaster.IsActive = disciplineMasterEntity.IsActive;
                DisciplineMaster.EnteredBy = disciplineMasterEntity.EnteredBy;
                DisciplineMaster.EnteredDate = disciplineMasterEntity.EnteredDate;

                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string DeleteDiscipline(int DisciplineId)
        {
            try
            {
                var DisciplineMaster = _dbContext.DisciplineMasters.Find(DisciplineId);
                DisciplineMaster.IsActive = false;
                _dbContext.SaveChanges();
                return "Success";
            }
            catch(Exception ex)
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
