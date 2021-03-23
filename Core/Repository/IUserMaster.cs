using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.DropDowns;
using LIMS_DEMO.Core;
using LIMS_DEMO.DAL;


namespace LIMS_DEMO.Core.Repository
{
     public interface IUserMaster
    {
        string AddUser(UserMasterEntity userMasterEntity);
        string Update(UserMasterEntity userMasterEntity);
        List<UserMasterEntity> GetUserList();
        UserMasterEntity GetDetails(int UserMasterID);
    }
}
