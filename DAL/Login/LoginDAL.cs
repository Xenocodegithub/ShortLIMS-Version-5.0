using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.User;

namespace LIMS_DEMO.DAL.Login
{
    public class LoginDAL : ILogin
    {
        readonly LIMSEntities _dbContext1;
        public LoginDAL()
        {
          
            _dbContext1 = new LIMSEntities();
        }

        public List<NotificationEntity> GetNotificationDetailList(string RoleName)
        {
            try
            {
                LIMSEntities _dbContext1 = new LIMSEntities();
                return (from nd in _dbContext1.NotificationDetails
                        where nd.RoleName == RoleName
                        select new NotificationEntity()
                        {
                            NotificationDetailId = nd.NotificationDetailId,
                            NotificationName = nd.NotificationName,
                            Message = nd.Message,
                            DateTime = nd.DateTime,
                            DisplayDate = nd.DateTime.Value.ToString(),
                            IsActive = nd.IsActive,
                            RoleName = nd.RoleName,
                            EnteredDate = nd.EnteredDate,
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ChangeRequest(int UserMasterID, bool ResetActive)
        {
            try
            {
                var userMaster = _dbContext1.UserMasters.Find(UserMasterID);
                userMaster.ResetActive = ResetActive;
                _dbContext1.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void Initialize()
        {
            LIMSEntities _dbContext1 = new LIMSEntities();
            UserEntity userEntity = new UserEntity();
          
        }
        public UserEntity GetUserDetails(string strUserName)
        {
           
            try
            {
                LIMSEntities _dbContext1 = new LIMSEntities();
                return (from u in _dbContext1.UserMasters
                        join ur in _dbContext1.UserRoles on u.UserMasterID equals ur.UserMasterId
                        join rm in _dbContext1.RoleMasters on ur.RoleId equals rm.RoleId
                        join ud in _dbContext1.UserDetails on u.UserMasterID equals ud.UserMasterID
                        //into user
                        //from user in userDetail.DefaultIfEmpty()
                        where u.UserName == strUserName && u.IsActive == true && ud.IsActive == true

                        select new UserEntity()
                        {
                            UserDetailID = ud.UserDetailID,
                            UserMasterID = u.UserMasterID,
                            UserName = u.UserName,
                            //RoleName = rm.RoleName,
                            Password = u.Password,
                            Salutation = ud.Salutation,
                            FirstName = ud.FirstName,
                            MiddleName = ud.MiddleName,
                            LastName = ud.LastName,
                            Gender = ud.Gender,
                            DateOfBirth = ud.DateOfBirth,
                            Email = ud.Email,
                            Mobile = ud.Mobile,
                            EmployeeCode = ud.EmployeeCode,
                            DisciplineId = (int)(u.DisciplineId == null ? 0 : u.DisciplineId),
                            UserRoleID = ur.RoleId,
                            RoleName = ur.RoleMaster.RoleName,
                        }).FirstOrDefault();
                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetLabId(int UserMasterId)
        {
            LIMSEntities _dbContext1 = new LIMSEntities();
            return _dbContext1.UserLabs.Where(l => l.UserMasterId == UserMasterId).Select(l => new { l.LabMasterId }).FirstOrDefault().LabMasterId;
        }
        public bool IsUserAuthorize(int UserMasterId, string MenuName)
        {
            LIMSEntities _dbContext1 = new LIMSEntities();
            return (from MRB in _dbContext1.MenuRoleBranchMappings
                    join MM in _dbContext1.MenuMasters on MRB.MenuMasterId equals MM.MenuMasterId
                    join UR in _dbContext1.UserRoles on MRB.RoleId equals UR.RoleId
                    where MM.Menu.Trim().ToLower() == MenuName.Trim().ToLower() && MRB.IsActive == true && MM.IsActive == true && UR.IsActive == true
                    && UR.UserMasterId == UserMasterId
                    select new { MRB.MenuRoleBranchMappingId }).Any();

        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            { if (disposing) { _dbContext1.Dispose(); } }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}