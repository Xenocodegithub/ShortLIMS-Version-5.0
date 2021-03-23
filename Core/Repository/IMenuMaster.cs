using LIMS_DEMO.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Repository
{
    public interface IMenuMaster
    {
        string AddParentMenu(MenuMasterEntity menuMasterEntity);
        List<MenuMasterEntity> GetParentMenuList();
        List<MenuMasterEntity> GetSubMenuList();
        string DeleteParentMenu(int MenuMasterId);
        MenuMasterEntity GetDetailsParent(int MenuMasterId);
        string UpdateParentMenu(MenuMasterEntity menuMasterEntity);

        string AddSubMenu(MenuMasterEntity menuMasterEntity);
        string UpdateSubMenu(MenuMasterEntity menuMasterEntity);
        string DeleteSubMenu(int MenuMasterId);
        MenuMasterEntity GetDetailsSub(int MenuMasterId);

        string AddMappingMenu(MenuMasterEntity menuMasterEntity);
        string AddMappingMenu2(MenuMasterEntity menuMasterEntity, int MapSubMenu);
        string DeleteMappingMenu(int MenuRoleBranchMappingId);
        MenuMasterEntity GetDataall(int RoleId, int ParentId, int SubMenuId);
        MenuMasterEntity GetDataill(int RoleId, int ParentId);
        List<MenuMasterEntity> GetMenuMappingList();
    }
}
