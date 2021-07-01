using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Repository;


namespace LIMS_DEMO.DAL.Configuration
{
    public class RoleMasterDAL : IRoleMaster
    {
        readonly LIMSEntities _dbContext;
        readonly LIMSEntities _dbContext1;
        public RoleMasterDAL()
        {
            _dbContext = new LIMSEntities();
            _dbContext1 = new LIMSEntities();

        }
        public string AddUserRole(RoleMasterEntity roleMasterEntity)
        {
            try
            {

                _dbContext.UserRoles.Add(new UserRole()
                {
                    RoleId = roleMasterEntity.RoleId,
                    UserMasterId = roleMasterEntity.UserMasterId,
                    IsActive = roleMasterEntity.IsActive,
                    EnteredBy = roleMasterEntity.EnteredBy,
                    EnteredDate = roleMasterEntity.EnteredDate,
                    ModifiedBy = roleMasterEntity.ModifiedBy

                });
                _dbContext.SaveChanges();
                _dbContext1.UserLabs.Add(new UserLab()
                {
                    LabMasterId = roleMasterEntity.LabId,
                    UserMasterId = roleMasterEntity.UserMasterId,
                    IsActive = roleMasterEntity.IsActive,
                    EnteredBy = roleMasterEntity.EnteredBy,
                    EnteredDate = roleMasterEntity.EnteredDate,
                    ModifiedBy = roleMasterEntity.ModifiedBy

                });
                _dbContext1.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }

        }
        public RoleMasterEntity GetData(int RoleId, int UserMasterId)
        {
            return _dbContext.UserRoles.Where(u => u.RoleId == RoleId & u.UserMasterId == UserMasterId).Select(u => new RoleMasterEntity()
            {
                UserRoleId = u.UserRoleId,
                RoleId = u.RoleId,
                UserMasterId = u.UserMasterId,
                IsActive = u.IsActive
            }).FirstOrDefault();
        }
        public RoleMasterEntity GetDetails(int UserRoleId)
        {
            return _dbContext.UserRoles.Where(u => u.UserRoleId == UserRoleId).Select(u => new RoleMasterEntity()
            {
                UserRoleId = u.UserRoleId,
                RoleId = u.RoleId,
                UserMasterId = u.UserMasterId,
                IsActive = u.IsActive
            }).FirstOrDefault();
        }
        public string UpdateUserRole(RoleMasterEntity roleMasterEntity)
        {
            try
            {
                var UserRoleMaster = _dbContext.UserRoles.Find(roleMasterEntity.UserRoleId);
                UserRoleMaster.UserRoleId = roleMasterEntity.UserRoleId;
                UserRoleMaster.RoleId = roleMasterEntity.RoleId;
                UserRoleMaster.UserMasterId = roleMasterEntity.UserMasterId;
                UserRoleMaster.IsActive = roleMasterEntity.IsActive;
                UserRoleMaster.EnteredBy = roleMasterEntity.EnteredBy;
                UserRoleMaster.EnteredDate = roleMasterEntity.EnteredDate;

                var UserLabMaster = _dbContext1.UserLabs.Find(roleMasterEntity.LabId);
                UserLabMaster.LabMasterId = roleMasterEntity.LabId;
                UserLabMaster.UserMasterId = roleMasterEntity.UserMasterId;

                UserLabMaster.IsActive = roleMasterEntity.IsActive;
                UserLabMaster.EnteredBy = roleMasterEntity.EnteredBy;
                UserLabMaster.EnteredDate = roleMasterEntity.EnteredDate;
                UserLabMaster.ModifiedBy = roleMasterEntity.ModifiedBy;
                _dbContext.SaveChanges();
                _dbContext1.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string DeleteUserRole(int UserRoleId)
        {
            try
            {
                var UserRoleMaster = _dbContext.UserRoles.Find(UserRoleId);
                UserRoleMaster.IsActive = false;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public List<RoleMasterEntity> GetUserRoleList()

        {
            try
            {

                return (from e in _dbContext.UserRoles
                        join r in _dbContext.RoleMasters on e.RoleId equals r.RoleId
                        join u in _dbContext.UserMasters on e.UserMasterId equals u.UserMasterID
                        where e.IsActive == true
                        select new RoleMasterEntity()
                        {
                            RoleId = e.RoleId,
                            RoleName = r.RoleName,
                            UserRoleId = e.UserRoleId,
                            UserMasterId = e.UserMasterId,
                            UserName = u.UserName,
                            IsActive = e.IsActive,

                        }).OrderByDescending(e => e.UserRoleId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<RoleMasterEntity> GetRoleMasterList()

        {
            try
            {

                return (from e in _dbContext.RoleMasters
                        where e.IsActive == true
                        select new RoleMasterEntity()
                        {
                            RoleId = e.RoleId,
                            RoleName = e.RoleName,
                            IsActive = e.IsActive,

                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

            //throw new NotImplementedException();
        }
        public string AddRoleMaster(RoleMasterEntity roleMasterEntity)
        {
            try
            {
                _dbContext.RoleMasters.Add(new RoleMaster()
                {
                    RoleId = roleMasterEntity.RoleId,
                    RoleName = roleMasterEntity.RoleName,
                    IsActive = roleMasterEntity.IsActive,
                    EnteredBy = roleMasterEntity.EnteredBy,
                    EnteredDate = roleMasterEntity.EnteredDate,
                    ModifiedBy = roleMasterEntity.ModifiedBy

                });
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }

        }
        public RoleMasterEntity GetDetailsRole(int RoleId)
        {
            return _dbContext.RoleMasters.Where(u => u.RoleId == RoleId).Select(u => new RoleMasterEntity()
            {

                RoleId = u.RoleId,
                RoleName = u.RoleName,
                IsActive = u.IsActive
            }).FirstOrDefault();
        }
        public string UpdateRoleMaster(RoleMasterEntity roleMasterEntity)
        {
            try
            {
                var UserRoleMaster = _dbContext.RoleMasters.Find(roleMasterEntity.RoleId);
                UserRoleMaster.RoleId = roleMasterEntity.RoleId;
                UserRoleMaster.RoleName = roleMasterEntity.RoleName;
                UserRoleMaster.IsActive = roleMasterEntity.IsActive;
                UserRoleMaster.EnteredBy = roleMasterEntity.EnteredBy;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public string DeleteRoleMaster(int RoleId)
        {
            try
            {
                var UserRoleMaster = _dbContext.RoleMasters.Find(RoleId);
                UserRoleMaster.IsActive = false;
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