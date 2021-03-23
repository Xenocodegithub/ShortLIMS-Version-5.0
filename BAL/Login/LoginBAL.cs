using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.User;
using LIMS_DEMO.DAL.Login;

namespace LIMS_DEMO.BAL.Login
{
    public class LoginBAL
    {
        public LoginBAL()
        {
            CoreFactory.login = new LoginDAL();
        }
        public List<NotificationEntity> GetNotificationDetailList(string RoleName)
        {
            return CoreFactory.login.GetNotificationDetailList(RoleName);

        }
        public bool ChangeRequest(int UserMasterID, bool ResetActive)
        {
            return CoreFactory.login.ChangeRequest(UserMasterID, ResetActive);

        }
        public UserEntity GetUserDetails(string strUserName)
        {
            return CoreFactory.login.GetUserDetails(strUserName);
        }

        public int GetLabId(int UserMasterId)
        {
            return CoreFactory.login.GetLabId(UserMasterId);
        }

        public bool IsUserAuthorize(int UserMasterId, string MenuName)
        {
            return CoreFactory.login.IsUserAuthorize(UserMasterId, MenuName);
        }
        public void Initialize()
        {
            CoreFactory.login.Initialize();
        }
    }
}