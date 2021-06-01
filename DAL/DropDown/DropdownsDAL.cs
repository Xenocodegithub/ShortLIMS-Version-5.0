using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.Inventory;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.DropDowns;
using LIMS_DEMO.Core.Customer;
using LIMS_DEMO.Core;
using LIMS_DEMO.DAL;
using LIMS_DEMO.DAL.Inventory;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;


namespace LIMS_DEMO.DAL.DropDown
{
    public class DropdownsDAL:IDropDown
    {
        readonly LIMSEntities _dbContext;      
        public DropdownsDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public List<UserListEntity> GetPassChaReqUserList()
        {
            LIMSEntities _dbContext = new LIMSEntities();
            try
            {
                return (from u in _dbContext.UserMasters
                        where u.ResetActive == true
                        select new UserListEntity()
                        {
                            UserMasterID = u.UserMasterID,
                            UserName = u.UserName,
                            ResetActive = (bool)u.ResetActive,

                        }).OrderBy(e => e.UserName).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<UserListEntity> GetGroupInchargeList(string Name, int LabMasterId)
        {
            try
            {
                if (Name == "SuperwisedBy")
                {
                    return (from u in _dbContext.UserDetails
                            join l in _dbContext.UserLabs on u.UserMasterID equals l.UserMasterId
                            join ur in _dbContext.UserRoles on u.UserMasterID equals ur.UserMasterId
                            join r in _dbContext.RoleMasters on ur.RoleId equals r.RoleId
                            into users
                            from us in users.DefaultIfEmpty()
                            where l.LabMasterId == LabMasterId && (us.RoleName.Trim() == "Reviewer" || us.RoleName.Trim() == "Technical Manager") && u.IsActive == true && l.IsActive == true
                            && ur.IsActive == true
                            group u by u.UserMasterID into k
                            from j in k.DefaultIfEmpty()
                            select new UserListEntity()
                            {
                                UserMasterID = j.UserMasterID,
                                UserName = j.FirstName + " " + (j.LastName == null ? "" : j.LastName)

                            }).Distinct().ToList();
                }
                else
                {
                    return (from u in _dbContext.UserDetails
                            join l in _dbContext.UserLabs on u.UserMasterID equals l.UserMasterId
                            join ur in _dbContext.UserRoles on u.UserMasterID equals ur.UserMasterId
                            join r in _dbContext.RoleMasters on ur.RoleId equals r.RoleId
                            into users
                            from us in users.DefaultIfEmpty()
                            where l.LabMasterId == LabMasterId && (us.RoleName.Trim() == "Reviewer" || us.RoleName.Trim() == "Technical Manager" || us.RoleName.Trim() == "Sample Receipt and Report Incharge") && u.IsActive == true && l.IsActive == true
                            && ur.IsActive == true
                            group u by u.UserMasterID into k
                            from j in k.DefaultIfEmpty()
                            select new UserListEntity()
                            {
                                UserMasterID = j.UserMasterID,
                                UserName = j.FirstName + " " + (j.LastName == null ? "" : j.LastName)

                            }).Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //samplecode
        public List<SPSMEntity> GetSampleCode()
        {
            try
            {
                return (from loc in _dbContext.LocationSampleCollections
                        join sc in _dbContext.SampleCollections on loc.LocationSampleCollectionID equals sc.LocationSampleCollectionID
                        join esd in _dbContext.EnquirySampleDetails on sc.EnquirySampleID equals esd.EnquirySampleID
                        join ed in _dbContext.EnquiryDetails on esd.EnquiryDetailId equals ed.EnquiryDetailId
                        join sm in _dbContext.StatusMasters on sc.StatusId equals sm.StatusId
                        where sm.StatusName == "Approved" && sc.IsReturnedOrIsRetained == "Retained" && sc.IsDisposed == null
                        select new SPSMEntity()
                        {
                            SampleName = loc.SampleNameOriginal,
                            SampleCollectionId = sc.SampleCollectionId,
                        }).Distinct().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<UserListEntity> GetApproverList(string RoleName, int LabMasterId)
        {
            return (from u in _dbContext.UserDetails
                    join um in _dbContext.UserMasters on u.UserMasterID equals um.UserMasterID
                    join l in _dbContext.UserLabs on u.UserMasterID equals l.UserMasterId
                    join ur in _dbContext.UserRoles on u.UserMasterID equals ur.UserMasterId
                    join r in _dbContext.RoleMasters on ur.RoleId equals r.RoleId
                    into users
                    from us in users.DefaultIfEmpty()
                    where l.LabMasterId == LabMasterId && us.RoleName.Trim().ToLower() == RoleName.Trim().ToLower() && u.IsActive == true && l.IsActive == true
                    && ur.IsActive == true
                    group u by u.UserMasterID into k
                    from j in k.DefaultIfEmpty()
                    select new UserListEntity()
                    {
                        UserMasterID = j.UserMasterID,
                        UserName = j.FirstName + " " + (j.LastName == null ? "" : j.LastName)

                    }).Distinct().ToList();
        }
        public List<UserListEntity> GetPlannerList(string RoleName, int LabMasterId, int DisciplineId)
        {
            return (from u in _dbContext.UserDetails
                    join um in _dbContext.UserMasters on u.UserMasterID equals um.UserMasterID
                    join l in _dbContext.UserLabs on u.UserMasterID equals l.UserMasterId
                    join ur in _dbContext.UserRoles on u.UserMasterID equals ur.UserMasterId
                    join r in _dbContext.RoleMasters on ur.RoleId equals r.RoleId
                    into users
                    from us in users.DefaultIfEmpty()
                    where l.LabMasterId == LabMasterId && us.RoleName.Trim().ToLower() == RoleName.Trim().ToLower() && u.IsActive == true && l.IsActive == true
                    && ur.IsActive == true && um.DisciplineId == DisciplineId
                    group u by u.UserMasterID into k
                    from j in k.DefaultIfEmpty()
                    select new UserListEntity()
                    {
                        UserMasterID = j.UserMasterID,
                        UserName = j.FirstName + " " + (j.LastName == null ? "" : j.LastName)

                    }).Distinct().ToList();
        }
        public List<CollectorEntity> GetReceiver(int LabMasterId)
        {
            try
            {
                return (from ur in _dbContext.UserRoles
                        join rm in _dbContext.RoleMasters on ur.RoleId equals rm.RoleId
                        join um in _dbContext.UserMasters on ur.UserMasterId equals um.UserMasterID
                        join l in _dbContext.UserLabs on ur.UserMasterId equals l.UserMasterId
                        into collector
                        from c in collector
                        where rm.RoleName == "Sample Receiver" && c.LabMasterId == LabMasterId
                        //where rm.RoleName == "Sample Receipt and Report Incharge" && c.LabMasterId == LabMasterId
                        select new CollectorEntity()
                        {
                            UserRoleId = ur.UserRoleId,
                            UserMasterId = ur.UserMasterId,
                            RoleId = rm.RoleId,
                            RoleName = rm.RoleName,
                            UserMasterID = ur.UserMaster.UserMasterID,
                            UserName = ur.UserMaster.UserName,
                            IsActive = ur.IsActive,
                        }).Distinct().ToList();

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<EnvironmentalConditionEntity> GetEnvironmentalCondition()
        {
            try
            {
                return (from ec in _dbContext.EnvironmentalConditions

                        select new EnvironmentalConditionEntity()
                        {
                            EnvCondtId = ec.EnvCondtId,
                            EnvCondts = ec.EnvCondts,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<ProcedureEntity> GetProcedure(int SampleTypeProductId)
        {
            try
            {
                return (from p in _dbContext.ProcedureMasters
                        where p.SampleTypeProductId == SampleTypeProductId
                        select new ProcedureEntity()
                        {
                            ProcedureId = p.ProcedureId,
                            SampleTypeProductId = (Int32)p.SampleTypeProductId,
                            ProcedureName = p.ProcedureName,

                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SampleTypeEntity> GetSampleType()
        {
            try
            {
                return (from st in _dbContext.SampleTypeMasters

                        select new SampleTypeEntity()
                        {
                            SampleTypeId = st.SampleTypeId,
                            SampleType = st.SampleType,

                        }).ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SampleDeviceEntity> GetSampleDevice(int SampleTypeProductId)
        {
            try
            {
                return (from sd in _dbContext.SampleDeviceMasters
                        where sd.SampleTypeProductId == SampleTypeProductId
                        select new SampleDeviceEntity()
                        {
                            SampleDeviceId = sd.SampleDeviceId,
                            SampleTypeProductId = (Int32)sd.SampleTypeProductId,
                            SampleDevice = sd.SampleDevice,

                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SampleDescriptionEntity> GetSampleDescription(int ProductGroupId, int SubGroupId)
        {
            try
            {
                return (from sdm in _dbContext.SampleDescriptionMasters
                        where sdm.ProductGroupId == ProductGroupId && sdm.SubGroupId == SubGroupId
                        select new SampleDescriptionEntity()
                        {
                            SampleDescriptionId = sdm.SampleDescriptionId,
                            ProductGroupId = sdm.ProductGroupId,
                            SubGroupId = sdm.SubGroupId,
                            SampleDescription = sdm.SampleDescription,
                            MatrixCode = sdm.MatrixCode,

                        }).ToList();

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<FrequencyEntity> GetFrequency()
        {
            try
            {
                return (from f in _dbContext.FrequencyMasters
                        where f.IsActive == true
                        select new FrequencyEntity()
                        {
                            FrequencyMasterId = f.FrequencyMasterId,
                            Frequency = f.Frequency
                        }).OrderBy(f => f.Frequency).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<UserListEntity> GetUserList(string RoleName, int LabMasterId)
        {
            return (from u in _dbContext.UserDetails
                    join l in _dbContext.UserLabs on u.UserMasterID equals l.UserMasterId
                    join ur in _dbContext.UserRoles on u.UserMasterID equals ur.UserMasterId
                    join r in _dbContext.RoleMasters on ur.RoleId equals r.RoleId
                    into users
                    from us in users.DefaultIfEmpty()
                    where l.LabMasterId == LabMasterId && us.RoleName.Trim().ToLower() == RoleName.Trim().ToLower() && u.IsActive == true && l.IsActive == true
                    && ur.IsActive == true
                    group u by u.UserMasterID into k
                    from j in k.DefaultIfEmpty()
                    select new UserListEntity()
                    {
                        UserMasterID = j.UserMasterID,
                        UserName = j.FirstName + " " + (j.LastName == null ? "" : j.LastName)

                    }).Distinct().ToList();
        }
        public List<BranchEntity> GetBranches(int ParentLabId)
        {
            return _dbContext.LabMasters.Where(l => (l.LabMasterId == ParentLabId || l.ParentID == ParentLabId) && l.IsActive == true).Select(l => new BranchEntity()
            {
                LabMasterId = l.LabMasterId,
                LabBranchName = l.LabBranchName
            }).OrderBy(l => l.LabBranchName).ToList();
        }
        public List<UnitEntity> GetUnits()
        {
            return _dbContext.UnitMasters.Where(u => u.IsActive == true).Select(u => new UnitEntity()
            {
                UnitId = u.UnitId,
                Unit = u.Unit
            }).OrderBy(u => u.Unit).ToList();
        }
        public List<UnitEntity> GetUnits(int ParameterGroupId, int ParameterMasterId)
        {
            return _dbContext.ParameterMappings.Where(u => u.IsActive == true && u.ParameterGroupId == ParameterGroupId && u.ParameterMasterId == ParameterMasterId).Select(u => new UnitEntity()
            {
                UnitId = u.UnitId == null ? 0 : (int)u.UnitId,
                Unit = u.UnitMaster.Unit
            }).OrderBy(u => u.UnitId).ToList();
        }
        //public List<TestMethodEntity> GetTestMethods()
        //{
        //    return _dbContext.TestMethods.Where(t => t.IsActive == true).Select(t => new TestMethodEntity()
        //    {
        //        TestMethodId = t.TestMethodId,
        //        TestMethod = t.TestMethod1
        //    }).OrderBy(t => t.TestMethod).ToList();
        //}
        public List<TestMethodEntity> GetTestMethods(int ParameterGroupId, int ParameterMasterId)
        {
            return _dbContext.ParameterMappings.Where(t => t.IsActive == true && t.ParameterGroupId == ParameterGroupId && t.ParameterMasterId == ParameterMasterId).Select(t => new TestMethodEntity()
            {
                TestMethodId = t.TestMethodId == null ? 0 : (int)t.TestMethodId,
                TestMethod = t.TestMethod.TestMethod1
            }).OrderBy(t => t.TestMethodId).ToList();
        }
        public int GetStatusIdByCode(string StatusCode)
        {
            LIMSEntities _dbContext = new LIMSEntities();
            return _dbContext.StatusMasters.Where(s => s.StatusCode.ToLower() == StatusCode.ToLower() && s.IsActive == true).Select(s => new { s.StatusId }).FirstOrDefault().StatusId;
        }
        public IList<CommunicationEntity> GetCommunicationMode()
        {
            return _dbContext.ModeOfCommunications.Where(m => m.IsActive == true).Select(m => new CommunicationEntity()
            {
                ModeOfCommunicationId = m.ModeOfCommunicationId,
                CommunicationMode = m.CommunicationMode
            }).ToList();
        }
        public IList<CustomerEntity> GetCustomers()
        {
            return _dbContext.CustomerMasters.Where(c => c.IsActive == true).Select(c => new CustomerEntity()
            {
                CustomerMasterId = c.CustomerMasterId,
                RegistrationName = c.RegistrationName
            }).OrderBy(c => c.RegistrationName).ToList();
        }

        //Get Inventory Type List
        public IList<InventoryType> GetInventoryTypeList()
        {
            return _dbContext.InventoryTypeMasters.Where(c => c.IsActive == true).Select(c => new InventoryType()
            {
                InventoryTypeName = c.Name,
                InventoryTypeID = c.ID,
                IsActive = (bool)c.IsActive,
            }).OrderBy(c => c.InventoryTypeName).ToList();
        }

        //get Category Type List
        public IList<Core.DropDowns.CatagoryEntity> GetCategoryTypeList()
        {
            return _dbContext.InventoryCategoryMasters.Where(c => c.IsActive == true).Select(c => new Core.DropDowns.CatagoryEntity()
            {
                CategoryName = c.CategoryName,
                CatagoryMasterID = c.ID,
                IsActive = c.IsActive,
            }).OrderBy(c => c.CategoryName).ToList();
        }

        //get Unit Type List
        public IList<InvUnitEntity> GetUnitTypeList()
        {
            return _dbContext.UnitMasterInventories.Where(c => c.IsActive == true).Select(c => new InvUnitEntity()
            {
                UnitId = c.UnitId,
                Unit = c.Unit,
                IsActive = c.IsActive,
            }).OrderBy(c => c.Unit).ToList();
        }

        // get Capacity Type List
        public IList<InvCapacityEntity> GetCapacityList()
        {
            return _dbContext.InventoryCapacityMasters.Where(c => c.IsActive == true).Select(c => new InvCapacityEntity()
            {
                InventoryCapacityMasterId = c.InventoryCapacityMasterId,
                Capacity = c.Capacity,
                IsActive = c.IsActive,
            }).OrderBy(c => c.Capacity).ToList();
        }
        public IList<Core.DropDowns.CatagoryEntity> GetFilteredInvCatogaryList(int InventoryTypeId)
        {
            return _dbContext.InventoryCategoryMasters.Where(c => c.InventoryTypeID == InventoryTypeId && c.IsActive == true).Select(c => new Core.DropDowns.CatagoryEntity()
            {
                CatagoryMasterID = c.ID,
                InventoryTypeID = c.InventoryTypeID,
                CategoryName = c.CategoryName
            }).OrderBy(c => c.CategoryName).ToList();
        }
        public List<DisciplineMasterEntity> GetDiscipline()

        {
            try
            {
                return (from d in _dbContext.DisciplineMasters

                        select new DisciplineMasterEntity()
                        {
                            DisciplineId = d.DisciplineId,
                            Discipline = d.Discipline,

                        }).OrderBy(e => e.Discipline).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<RoleMasterEntity> GetRole()
        {
            try
            {
                return (from r in _dbContext.RoleMasters
                        where r.IsActive == true
                        select new RoleMasterEntity()
                        {
                            RoleId = r.RoleId,
                            RoleName = r.RoleName,
                        }).OrderBy(p => p.RoleName).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<UserMasterEntity> GetUserMasterList()
        {
            try
            {
                return (from u in _dbContext.UserMasters
                        select new UserMasterEntity()
                        {
                            UserMasterID = u.UserMasterID,
                            UserName = u.UserName
                        }).OrderBy(e => e.UserName).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<ParentMenuEntity> GetSubMenu(int ParentMenuId)
        {
            try
            {
                return (from p in _dbContext.MenuMasters
                        where p.ParentId != null && p.TargetUrl != null && p.IsActive == true
                        select new ParentMenuEntity()
                        {

                            SubMenuId = p.MenuMasterId,
                            SubMenuName = p.Menu,
                        }).OrderBy(p => p.SubMenuName).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SPSMEntity> GetProductGroup(int SampleTypeProductId)
        {
            try
            {
                return (from pg in _dbContext.ProductGroupMasters
                        where pg.SampleTypeProductId == SampleTypeProductId
                        select new SPSMEntity()
                        {
                            ProductGroupId = pg.ProductGroupId,
                            ProductGroupName = pg.ProductGroupName,
                        }).OrderBy(pg => pg.ProductGroupName).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SPSMEntity> GetSampleTypeProduct()
        {
            try
            {
                return (from stp in _dbContext.SampleTypeProductMasters
                        select new SPSMEntity()
                        {
                            SampleTypeProductId = stp.SampleTypeProductId,
                            SampleTypeProductName = stp.SampleTypeProductName,
                        }).OrderBy(stp => stp.SampleTypeProductName).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SPSMEntity> GetSubGroup(int SampleTypeProductId, int ProductGroupId)
        {
            try
            {
                return (from sg in _dbContext.SubGroupMasters
                        where sg.ProductGroupId == ProductGroupId && sg.SampleTypeProductId == SampleTypeProductId
                        select new SPSMEntity()
                        {
                            SubGroupId = sg.SubGroupId,
                            SubGroupName = sg.SubGroupName,
                        }).OrderBy(sg => sg.SubGroupName).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SPSMEntity> GetMatrix(int SampleTypeProductId, int ProductGroupId, int SubGroupId)
        {
            try
            {
                return (from m in _dbContext.MatrixMasters
                        where m.ProductGroupId == ProductGroupId && (m.SubGroupId == SubGroupId || SubGroupId == 0) && m.SampleTypeProductId == SampleTypeProductId
                        select new SPSMEntity()
                        {
                            MatrixId = m.MatrixId,
                            MatrixName = m.MatrixName,
                        }).OrderBy(m => m.MatrixName).ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<TestMethodEntity> GetTestMethods()
        {
            return _dbContext.TestMethods.Where(t => t.IsActive == true).Select(t => new TestMethodEntity()
            {
                TestMethodId = t.TestMethodId,
                TestMethod = t.TestMethod1
            }).OrderBy(t => t.TestMethod).ToList();
        }

        //public List<UnitEntity> GetUnits()
        //{
        //    return _dbContext.UnitMasters.Where(u => u.IsActive == true).Select(u => new UnitEntity()
        //    {
        //        UnitId = u.UnitId,
        //        Unit = u.Unit
        //    }).OrderBy(u => u.Unit).ToList();
        //}
        public List<ParameterMasterEntity> GetParameter()
        {
            try
            {
                return (from p in _dbContext.ParameterMasters
                        select new ParameterMasterEntity()
                        {
                            ParameterMasterId = p.ParameterMasterId,
                            Parameter = p.ParameterName,
                        }).OrderBy(e => e.Parameter).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ParentMenuEntity> GetParentMenu()
        {
            try
            {
                return (from p in _dbContext.MenuMasters
                        where p.ParentId == null && p.IsActive == true
                        select new ParentMenuEntity()
                        {
                            ParentMenuId = p.MenuMasterId,
                            ParentMenuName = p.Menu,
                        }).OrderBy(p => p.ParentMenuName).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ItemEntity> GetInventoryType()
        {
            try
            {
                return _dbContext.InventoryTypeMasters.Where(u => u.IsActive == true).Select(u => new ItemEntity()
                {
                    InventoryType = u.Name,
                    InventoryTypeID = (short)u.ID
                }).OrderBy(u => u.InventoryType).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ItemEntity> GetItemMaster(int InventoryTypeID)
        {
            try
            {
                return (from pg in _dbContext.InventoryItemMasters
                        join cm in _dbContext.InventoryCategoryMasters on pg.CategoryID equals cm.ID
                        where pg.InventoryTypeID == InventoryTypeID
                        select new ItemEntity()
                        {
                            ItemMasterId = pg.ID,
                            CategoryName = cm.CategoryName,
                            Item = pg.Name,
                        }).OrderBy(pg => pg.Item).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ItemEntity> GetSupplierName()
        {
            try
            {
                return _dbContext.PurchaseSupplierDetails.Where(u => u.IsActive == true).Select(u => new ItemEntity()
                {
                    PurchaseSupplierID = (Int32)u.PurchaseSupplierID,
                    SupplierName = u.SupplierName
                }).OrderBy(u => u.SupplierName).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IList<InventoryType> GetInventoryCategoryType()
        {
            return _dbContext.InventoryTypeMasters.Where(c => c.IsActive == true).Select(c => new InventoryType()
            {
                InventoryName = c.Name,
                ID = c.ID,
                IsActive = (bool)c.IsActive,
            }).OrderBy(c => c.InventoryName).ToList();
        }




        public void Dispose(bool v)
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}