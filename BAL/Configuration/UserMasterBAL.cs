using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core;
using LIMS_DEMO.DAL;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.DAL.Configuration;

namespace LIMS_DEMO.BAL.Configuration
{
    public class UserMasterBAL
    {
        public UserMasterBAL()
        {
            CoreFactory.userMaster = new UserMasterDAL();
        }
        public string AddUser(UserMasterEntity userMasterEntity)
        {
            return CoreFactory.userMaster.AddUser(userMasterEntity);
        }
        public string Update(UserMasterEntity userMasterEntity)
        {
            return CoreFactory.userMaster.Update(userMasterEntity);
        }
        public UserMasterEntity GetDetails(int UserMasterID)
        {
            return CoreFactory.userMaster.GetDetails((Int32)UserMasterID);
        }
        public List<UserMasterEntity> GetUserList()
        {
            return CoreFactory.userMaster.GetUserList();
        }
    }
}