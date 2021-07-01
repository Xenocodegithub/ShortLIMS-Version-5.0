using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Repository;


namespace LIMS_DEMO.DAL.Configuration
{
    public class UserMasterDAL : IUserMaster
    {
        readonly LIMSEntities _dbContext;
        public UserMasterDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public string AddUser(UserMasterEntity userMasterEntity)
        {
            try
            {
                _dbContext.UserMasters.Add(new UserMaster()
                {

                    UserName = userMasterEntity.UserName,
                    Password = userMasterEntity.Password,
                    DisciplineId = userMasterEntity.DisciplineId,
                    IsActive = userMasterEntity.IsActive,
                    EnteredBy = userMasterEntity.EnteredBy,
                    EnteredDate = userMasterEntity.EnteredDate,
                    ModifiedBy = userMasterEntity.ModifiedBy

                });

                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string Update(UserMasterEntity userEntity)
        {
            try
            {
                var userMaster = _dbContext.UserMasters.Find(userEntity.UserMasterID);
                userMaster.UserName = userEntity.UserName;
                userMaster.Password = userEntity.Password;
                userMaster.DisciplineId = userEntity.DisciplineId;
                userMaster.IsActive = userEntity.IsActive;
                userMaster.ResetActive = false;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public UserMasterEntity GetDetails(int UserMasterID)
        {
            return _dbContext.UserMasters.Where(u => u.UserMasterID == UserMasterID).Select(u => new UserMasterEntity()
            {
                UserName = u.UserName,
                UserMasterID = u.UserMasterID,
                DisciplineId = u.DisciplineId,
                IsActive = u.IsActive
            }).FirstOrDefault();
        }
        public List<UserMasterEntity> GetUserList()
        {
            return _dbContext.UserMasters.Select(u => new UserMasterEntity()
            {
                UserName = u.UserName,
                UserMasterID = u.UserMasterID,
                IsActive = u.IsActive
            }).OrderByDescending(u => u.UserMasterID).ToList();
        }
    }
}