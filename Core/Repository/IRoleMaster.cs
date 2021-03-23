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
   public interface IRoleMaster
    {
        string AddUserRole(RoleMasterEntity roleMasterEntity);
        RoleMasterEntity GetData(int RoleId, int UserMasterId);
        RoleMasterEntity GetDetails(int UserRoleId);
        string UpdateUserRole(RoleMasterEntity roleMasterEntity);
        List<RoleMasterEntity> GetUserRoleList();
        string DeleteUserRole(int UserRoleId);
        string AddRoleMaster(RoleMasterEntity roleMasterEntity);
        RoleMasterEntity GetDetailsRole(int RoleID);
        string UpdateRoleMaster(RoleMasterEntity roleMasterEntity);
        string DeleteRoleMaster(int RoleId);
        List<RoleMasterEntity> GetRoleMasterList();
    }
}
