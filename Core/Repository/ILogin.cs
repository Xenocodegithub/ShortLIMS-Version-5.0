using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.User;

namespace LIMS_DEMO.Core.Repository
{
    public interface ILogin : IDisposable
    {
        List<NotificationEntity> GetNotificationDetailList(string RoleName);
        bool ChangeRequest(int UserMasterID, bool ResetActive);
        UserEntity GetUserDetails(string strUserName);
        int GetLabId(int UserMasterId);
        bool IsUserAuthorize(int UserMasterId, string MenuName);
        void Initialize();
    }
}