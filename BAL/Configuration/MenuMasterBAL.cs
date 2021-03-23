using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.DAL.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.BAL.Configuration
{
    public class MenuMasterBAL
    {
        public MenuMasterBAL()
        {
            CoreFactory.MenuMaster = new MenuMasterDAL();
        }

        public string AddParentMenu(MenuMasterEntity menuMasterEntity)
        {
           return CoreFactory.MenuMaster.AddParentMenu(menuMasterEntity);
        }
        public List<MenuMasterEntity> GetParentMenuList()
        {
            return CoreFactory.MenuMaster.GetParentMenuList();
        }
        public List<MenuMasterEntity> GetSubMenuList()
        {
            return CoreFactory.MenuMaster.GetSubMenuList();
        }
        public string DeleteParentMenu(int MenuMasterId)
        {
            return CoreFactory.MenuMaster.DeleteParentMenu(MenuMasterId);
        }
        public MenuMasterEntity GetDetailsParent(int MenuMasterId)
        {
            return CoreFactory.MenuMaster.GetDetailsParent(MenuMasterId);
        }
        public string UpdateParentMenu(MenuMasterEntity menuMasterEntity)
        {
            return CoreFactory.MenuMaster.UpdateParentMenu(menuMasterEntity);
        }


        public string AddSubMenu(MenuMasterEntity menuMasterEntity)
        {
            return CoreFactory.MenuMaster.AddSubMenu(menuMasterEntity);
        }
        public string UpdateSubMenu(MenuMasterEntity menuMasterEntity)
        {
            return CoreFactory.MenuMaster.UpdateSubMenu(menuMasterEntity);
        }
        public string DeleteSubMenu(int MenuMasterId)
        {
            return CoreFactory.MenuMaster.DeleteSubMenu(MenuMasterId);
        }
        public MenuMasterEntity GetDetailsSub(int MenuMasterId)
        {
            return CoreFactory.MenuMaster.GetDetailsSub(MenuMasterId);
        }


        public string AddMappingMenu(MenuMasterEntity menuMasterEntity)
        {
            return CoreFactory.MenuMaster.AddMappingMenu(menuMasterEntity);
        }
        public string AddMappingMenu2(MenuMasterEntity menuMasterEntity, int MapSubMenu)
        {
            return CoreFactory.MenuMaster.AddMappingMenu2( menuMasterEntity, MapSubMenu);
        }
        public string DeleteMappingMenu(int MenuRoleBranchMappingId)
        {
            return CoreFactory.MenuMaster.DeleteMappingMenu(MenuRoleBranchMappingId);
        }
        public MenuMasterEntity GetDataall(int RoleId, int ParentId, int SubMenuId)
        {
            return CoreFactory.MenuMaster.GetDataall(RoleId,ParentId,SubMenuId);
        }
        public MenuMasterEntity GetDataill(int RoleId, int ParentId)
        {
            return CoreFactory.MenuMaster.GetDataill( RoleId, ParentId);
        }
        public List<MenuMasterEntity> GetMenuMappingList()
        {
            return CoreFactory.MenuMaster.GetMenuMappingList();
        }
    }
}