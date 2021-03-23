using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.DAL.Configuration
{
    public class MenuMasterDAL: IMenuMaster
    {
        readonly LIMSEntities _dbContext;
        public MenuMasterDAL()
        {
            _dbContext = new LIMSEntities();
        }

        public string AddParentMenu(MenuMasterEntity menuMasterEntity)
        {
            try
            {
                _dbContext.MenuMasters.Add(new MenuMaster()
                {
                    MenuMasterId = menuMasterEntity.MenuMasterId,
                    Menu = menuMasterEntity.MenuName,
                    IsActive = menuMasterEntity.IsActive,
                    EnteredBy = menuMasterEntity.EnteredBy,
                    EnteredDate = menuMasterEntity.EnteredDate,
                    ModifiedBy = menuMasterEntity.ModifiedBy

                });
                _dbContext.SaveChanges();
                return "success";

            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public List<MenuMasterEntity> GetParentMenuList()
        {
            try
            {
                return (from e in _dbContext.MenuMasters
                        where e.ParentId == null && e.IsActive == true
                        select new MenuMasterEntity()
                        {
                            MenuName = e.Menu,
                            MenuMasterId = e.MenuMasterId,
                            IsActive = e.IsActive,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<MenuMasterEntity> GetSubMenuList()
        {
            try
            {

                return (from s in _dbContext.MenuMasters
                        where s.ParentId == null
                        join e in _dbContext.MenuMasters on s.MenuMasterId equals e.MenuMasterId
                        where e.ParentId == null
                        from p in _dbContext.MenuMasters
                        where p.ParentId != null && p.ParentId == s.MenuMasterId && p.IsActive == true
                        select new MenuMasterEntity()
                        {
                            MenuMasterId = s.MenuMasterId,
                            ParentMenuId = (Int32)p.ParentId,
                            ParentId = s.MenuMasterId,
                            ParentMenuName = s.Menu,
                            SubMenuId = p.MenuMasterId,
                            SubMenuName = p.Menu,
                            TargetURL = p.TargetUrl,
                            IsActive = p.IsActive,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string DeleteParentMenu(int MenuMasterId)
        {
            try
            {
                var MenuMaster = _dbContext.MenuMasters.Find(MenuMasterId);
                MenuMaster.IsActive = false;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public MenuMasterEntity GetDetailsParent(int MenuMasterId)
        {
            return _dbContext.MenuMasters.Where(u => u.MenuMasterId == MenuMasterId).Select(u => new MenuMasterEntity()
            {
                MenuMasterId = u.MenuMasterId,
                MenuName = u.Menu,
                IsActive = u.IsActive
            }).FirstOrDefault();
        }
        public string UpdateParentMenu(MenuMasterEntity menuMasterEntity)
        {
            try
            {
                var MenuMaster = _dbContext.MenuMasters.Find(menuMasterEntity.MenuMasterId);
                MenuMaster.MenuMasterId = menuMasterEntity.MenuMasterId;
                MenuMaster.Menu = menuMasterEntity.MenuName;
                MenuMaster.IsActive = menuMasterEntity.IsActive;
                MenuMaster.EnteredBy = menuMasterEntity.EnteredBy;
                MenuMaster.EnteredDate = menuMasterEntity.EnteredDate;

                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string AddSubMenu(MenuMasterEntity menuMasterEntity)
        {
            try
            {
                _dbContext.MenuMasters.Add(new MenuMaster()
                {
                    MenuMasterId = menuMasterEntity.MenuMasterId,
                    Menu = menuMasterEntity.MenuName,
                    ParentId = menuMasterEntity.ParentId,
                    TargetUrl = menuMasterEntity.TargetURL,

                    IsActive = menuMasterEntity.IsActive,
                    EnteredBy = menuMasterEntity.EnteredBy,
                    EnteredDate = menuMasterEntity.EnteredDate,
                    ModifiedBy = menuMasterEntity.ModifiedBy
                });
                _dbContext.SaveChanges();
                return "success";

            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string UpdateSubMenu(MenuMasterEntity menuMasterEntity)
        {
            try
            {
                var MenuMaster = _dbContext.MenuMasters.Find(menuMasterEntity.MenuMasterId);
                MenuMaster.MenuMasterId = menuMasterEntity.MenuMasterId;
                MenuMaster.Menu = menuMasterEntity.MenuName;
                MenuMaster.ParentId = menuMasterEntity.ParentId;
                MenuMaster.TargetUrl = menuMasterEntity.TargetURL;
                MenuMaster.IsActive = menuMasterEntity.IsActive;
                MenuMaster.EnteredBy = menuMasterEntity.EnteredBy;
                MenuMaster.EnteredDate = menuMasterEntity.EnteredDate;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string DeleteSubMenu(int MenuMasterId)
        {
            try
            {
                var MenuMaster = _dbContext.MenuMasters.Find(MenuMasterId);
                MenuMaster.IsActive = false;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public MenuMasterEntity GetDetailsSub(int MenuMasterId)
        {
            try
            {
                return (from s in _dbContext.MenuMasters

                        where s.MenuMasterId == MenuMasterId
                        select new MenuMasterEntity()
                        {
                            MenuMasterId = s.MenuMasterId,
                            ParentMenuId = s.ParentId,
                            SubMenuName = s.Menu,
                            TargetURL = s.TargetUrl,
                            IsActive = s.IsActive
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        //---------------------------------------------------------------------------------------

        public string AddMappingMenu(MenuMasterEntity menuMasterEntity)
        {
            try
            {
                _dbContext.MenuRoleBranchMappings.Add(new MenuRoleBranchMapping()
                {
                    MenuRoleBranchMappingId = menuMasterEntity.MenuRoleBranchMappingId,
                    MenuMasterId = (Int32)menuMasterEntity.ParentMenuId,
                    RoleId = menuMasterEntity.RoleId,
                    IsActive = menuMasterEntity.IsActive,
                    EnteredBy = menuMasterEntity.EnteredBy,
                    EnteredDate = menuMasterEntity.EnteredDate,
                    ModifiedBy = menuMasterEntity.ModifiedBy
                });
                _dbContext.SaveChanges();
                return "success";

            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string AddMappingMenu2(MenuMasterEntity menuMasterEntity, int MapSubMenu)
        {
            try
            {
                _dbContext.MenuRoleBranchMappings.Add(new MenuRoleBranchMapping()
                {
                    MenuRoleBranchMappingId = menuMasterEntity.MenuRoleBranchMappingId,
                    MenuMasterId = menuMasterEntity.SubMenuId,
                    RoleId = menuMasterEntity.RoleId,
                    IsActive = menuMasterEntity.IsActive,
                    EnteredBy = menuMasterEntity.EnteredBy,
                    EnteredDate = menuMasterEntity.EnteredDate,
                    ModifiedBy = menuMasterEntity.ModifiedBy
                });
                _dbContext.SaveChanges();
                return "success";

            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public string DeleteMappingMenu(int MenuRoleBranchMappingId)
        {
            try
            {
                var MappingMenuMaster = _dbContext.MenuRoleBranchMappings.Find(MenuRoleBranchMappingId);
                MappingMenuMaster.IsActive = false;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public MenuMasterEntity GetDataall(int RoleId, int ParentId, int SubMenuId)
        {
            return _dbContext.MenuRoleBranchMappings.Where(u => (u.RoleId == RoleId & u.MenuMasterId == SubMenuId) & (u.RoleId == RoleId & u.MenuMasterId == ParentId)).Select(u => new MenuMasterEntity()
            {
                SubMenuId = u.MenuMasterId,
                RoleId = u.RoleId,
                //ParentId = u.MenuMasterId,
                IsActive = u.IsActive
            }).FirstOrDefault();
        }
        public MenuMasterEntity GetDataill(int RoleId, int ParentId)
        {
            return _dbContext.MenuRoleBranchMappings.Where(u => (u.RoleId == RoleId & u.MenuMasterId == ParentId)).Select(u => new MenuMasterEntity()
            {
                ParentMenuId = u.MenuMasterId,
                RoleId = u.RoleId,
                //ParentId = u.MenuMasterId,
                IsActive = u.IsActive
            }).FirstOrDefault();
        }
        public List<MenuMasterEntity> GetMenuMappingList()
        {
            try
            {
                return (from mrp in _dbContext.MenuRoleBranchMappings
                        from m in _dbContext.MenuMasters
                        where m.MenuMasterId == mrp.MenuMasterId
                        from r in _dbContext.RoleMasters
                        where r.RoleId == mrp.RoleId && mrp.IsActive == true
                        select new MenuMasterEntity()
                        {
                            MenuRoleBranchMappingId = mrp.MenuRoleBranchMappingId,
                            MenuMasterId = m.MenuMasterId,
                            MenuName = m.Menu,
                            ParentId = m.ParentId,
                            TargetURL = m.TargetUrl,
                            RoleId = r.RoleId,
                            RoleName = r.RoleName,
                            IsActive = mrp.IsActive
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

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