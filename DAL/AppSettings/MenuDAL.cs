using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.AppSettings;
using LIMS_DEMO.Core.Repository;

namespace LIMS_DEMO.DAL.AppSettings
{
    public class MenuDAL : IMenu
    {
        readonly LIMSEntities _dbContext;
        public MenuDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public IList<MenuEntity> Get(int iUserId)
        {
            LIMSEntities _dbContext = new LIMSEntities();
            var MainMenus = (from MRB in _dbContext.MenuRoleBranchMappings
                             join MM in _dbContext.MenuMasters on MRB.MenuMasterId equals MM.MenuMasterId
                             join UR in _dbContext.UserRoles on MRB.RoleId equals UR.RoleId
                             where MRB.IsActive == true && MM.IsActive == true && UR.IsActive == true
                             && UR.UserMasterId == iUserId && MM.ParentId == null
                             orderby MM.SequenceNo
                             select new MenuEntity()
                             {
                                 MenuMasterId = MM.MenuMasterId,
                                 ParentId = MM.ParentId,
                                 Menu = MM.Menu,
                                 Logo = MM.IconValues,
                                 TargetUrl = MM.TargetUrl
                             }).Distinct().ToList();

            //var MainMenus = _dbContext.MenuMasters.Where(m => m.IsActive == true && m.ParentId == null).Select(m => new MenuEntity()
            //{
            //    MenuMasterId = m.MenuMasterId,
            //    ParentId = m.ParentId,
            //    Menu = m.Menu,
            //    Logo = m.Logo,
            //    TargetUrl = m.TargetUrl
            //}).ToList();

            foreach (var menu in MainMenus)
            {
                menu.SubMenu = (from MRB in _dbContext.MenuRoleBranchMappings
                                join MM in _dbContext.MenuMasters on MRB.MenuMasterId equals MM.MenuMasterId
                                join UR in _dbContext.UserRoles on MRB.RoleId equals UR.RoleId
                                where MRB.IsActive == true && MM.IsActive == true && UR.IsActive == true
                                && UR.UserMasterId == iUserId && MM.ParentId == menu.MenuMasterId
                                orderby MM.SequenceNo
                                select new MenuEntity()
                                {
                                    MenuMasterId = MM.MenuMasterId,
                                    ParentId = MM.ParentId,
                                    Menu = MM.Menu,
                                    Logo = MM.IconValues,
                                    TargetUrl = MM.TargetUrl
                                }).Distinct().ToList();
                //menu.SubMenu = _dbContext.MenuMasters.Where(m => m.ParentId == menu.MenuMasterId && m.IsActive == true).Select(m => new MenuEntity()
                //{
                //    MenuMasterId = m.MenuMasterId,
                //    ParentId = m.ParentId,
                //    Menu = m.Menu,
                //    Logo = m.Logo,
                //    TargetUrl = m.TargetUrl
                //}).ToList();
            }
            return MainMenus;
        }
        public IList<MenuEntity> GetMenu(int iUserId)
        {
            var ParentMenus = (from MRB in _dbContext.MenuRoleBranchMappings
                               join MM in _dbContext.MenuMasters on MRB.MenuMasterId equals MM.MenuMasterId
                               join UR in _dbContext.UserRoles on MRB.RoleId equals UR.RoleId
                               where MRB.IsActive == true && MM.IsActive == true && UR.IsActive == true
                               && UR.UserMasterId == iUserId && MM.ParentId == null && MM.MenuHead == null
                               orderby MM.SequenceNo
                               select new MenuEntity()
                               {
                                   MenuMasterId = MM.MenuMasterId,
                                   ParentId = MM.ParentId,
                                   Menu = MM.Menu,
                                   Logo = MM.IconValues,
                                   TargetUrl = MM.TargetUrl,
                                   MenuHead = MM.MenuHead
                               }).Distinct().ToList();

            foreach (var PMenu in ParentMenus)
            {
                PMenu.MainMenus = (from MRB in _dbContext.MenuRoleBranchMappings
                                   join MM in _dbContext.MenuMasters on MRB.MenuMasterId equals MM.MenuMasterId
                                   join UR in _dbContext.UserRoles on MRB.RoleId equals UR.RoleId
                                   where MRB.IsActive == true && MM.IsActive == true && UR.IsActive == true
                                   && UR.UserMasterId == iUserId && MM.ParentId == null && MM.MenuHead == PMenu.MenuMasterId
                                   orderby MM.SequenceNo
                                   select new MenuEntity()
                                   {
                                       MenuMasterId = MM.MenuMasterId,
                                       ParentId = MM.ParentId,
                                       Menu = MM.Menu,
                                       Logo = MM.IconValues,
                                       TargetUrl = MM.TargetUrl,
                                       MenuHead = MM.MenuHead
                                   }).Distinct().ToList();


                foreach (var menu in PMenu.MainMenus)
                {
                    menu.SubMenu = (from MRB in _dbContext.MenuRoleBranchMappings
                                    join MM in _dbContext.MenuMasters on MRB.MenuMasterId equals MM.MenuMasterId
                                    join UR in _dbContext.UserRoles on MRB.RoleId equals UR.RoleId
                                    where MRB.IsActive == true && MM.IsActive == true && UR.IsActive == true
                                    && UR.UserMasterId == iUserId && MM.ParentId == menu.MenuMasterId
                                    orderby MM.SequenceNo
                                    select new MenuEntity()
                                    {
                                        MenuMasterId = MM.MenuMasterId,
                                        ParentId = MM.ParentId,
                                        Menu = MM.Menu,
                                        Logo = MM.IconValues,
                                        TargetUrl = MM.TargetUrl
                                    }).Distinct().ToList();

                }
            }
            return ParentMenus;
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            { if (disposing) { _dbContext.Dispose(); } }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}