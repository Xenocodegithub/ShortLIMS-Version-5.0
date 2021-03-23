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
    public class RoleMasterBAL
    {
        public RoleMasterBAL()
        {
            CoreFactory.roleMaster = new RoleMasterDAL();
        }
        public string AddUserRole(RoleMasterEntity roleMasterEntity)
        {
            return CoreFactory.roleMaster.AddUserRole(roleMasterEntity);
        }

        public RoleMasterEntity GetData(int RoleId, int UserMasterId)
        {
            return CoreFactory.roleMaster.GetData(RoleId, UserMasterId);
        }
        public RoleMasterEntity GetDetails(int UserRoleId)
        {
            return CoreFactory.roleMaster.GetDetails(UserRoleId);
        }
        public string UpdateUserRole(RoleMasterEntity roleMasterEntity)
        {
            return CoreFactory.roleMaster.UpdateUserRole(roleMasterEntity);
        }
        public List<RoleMasterEntity> GetUserRoleList()
        {
            return CoreFactory.roleMaster.GetUserRoleList();
        }
        public string DeleteUserRole(int UserRoleId)
        {
            return CoreFactory.roleMaster.DeleteUserRole(UserRoleId);
        }
        public string DeleteRoleMaster(int RoleId)
        {
            return CoreFactory.roleMaster.DeleteRoleMaster(RoleId);
        }
        public string AddRoleMaster(RoleMasterEntity roleMasterEntity)
        {
            return CoreFactory.roleMaster.AddRoleMaster(roleMasterEntity);
        }
        public RoleMasterEntity GetDetailsRole(int RoleID)
        {
            return CoreFactory.roleMaster.GetDetailsRole(RoleID);
        }
        public string UpdateRoleMaster(RoleMasterEntity roleMasterEntity)
        {
            return CoreFactory.roleMaster.UpdateRoleMaster(roleMasterEntity);
        }
        public List<RoleMasterEntity> GetRoleMasterList()
        {
            return CoreFactory.roleMaster.GetRoleMasterList();
        }
    }
}