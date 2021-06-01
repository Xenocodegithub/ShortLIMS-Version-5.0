using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Inventory;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.DropDowns;
using LIMS_DEMO.Core.Customer;
using LIMS_DEMO.Core;
using LIMS_DEMO.DAL;
using LIMS_DEMO.DAL.DropDown;
using LIMS_DEMO.DAL.Inventory;

namespace LIMS_DEMO.BAL.DropDown
{
    public class DropDownBal
    {
         public DropDownBal()
        {
            CoreFactory.dropdowns = new DropdownsDAL();
        }

        //public List<Core.DropDowns.CatagoryEntity> GetRole()
        //{
        //    return CoreFactory.dropdowns.GetRole();
        //}
        public List<UserListEntity> GetPassChaReqUserList()
        {
            return CoreFactory.dropdowns.GetPassChaReqUserList();
        }
        public IList<InventoryType> GetInventoryCategoryType()
        {
            return CoreFactory.dropdowns.GetInventoryCategoryType();
        }
        public List<SPSMEntity> GetSampleCode()
        {
            return CoreFactory.dropdowns.GetSampleCode();
        }
        public List<UserListEntity> GetGroupInchargeList(string Name, int LabMasterId)
        {
            return CoreFactory.dropdowns.GetGroupInchargeList(Name, LabMasterId);
        }
        public List<UserListEntity> GetApproverList(string RoleName, int LabMasterId)
        {
            return CoreFactory.dropdowns.GetApproverList(RoleName, LabMasterId);
        }
        public List<UserListEntity> GetPlannerList(string RoleName, int LabMasterId, int DisciplineId)
        {
            return CoreFactory.dropdowns.GetPlannerList(RoleName, LabMasterId, DisciplineId);
        }
        public List<CollectorEntity> GetReceiver(int LabMasterId)
        {
            return CoreFactory.dropdowns.GetReceiver(LabMasterId);
        }
        public List<SampleDescriptionEntity> GetSampleDescription(int ProductGroupId, int SubGroupId)
        {
            return CoreFactory.dropdowns.GetSampleDescription(ProductGroupId, SubGroupId);
        }
        public List<SampleDeviceEntity> GetSampleDevice(int SampleTypeProductId)
        {
            return CoreFactory.dropdowns.GetSampleDevice(SampleTypeProductId);
        }
        public List<ProcedureEntity> GetProcedure(int SampleTypeProductId)
        {
            return CoreFactory.dropdowns.GetProcedure(SampleTypeProductId);
        }
        public List<SampleTypeEntity> GetSampleType()
        {
            return CoreFactory.dropdowns.GetSampleType();
        }
        public List<EnvironmentalConditionEntity> GetEnvironmentalCondition()
        {
            return CoreFactory.dropdowns.GetEnvironmentalCondition();
        }
        public List<SPSMEntity> GetSampleTypeProduct()
        {
            return CoreFactory.dropdowns.GetSampleTypeProduct();
        }
        public List<SPSMEntity> GetProductGroup(int SampleTypeProductId)
        {
            return CoreFactory.dropdowns.GetProductGroup(SampleTypeProductId);
        }
        public List<SPSMEntity> GetSubGroup(int SampleTypeProductId, int ProductGroupId)
        {
            return CoreFactory.dropdowns.GetSubGroup(SampleTypeProductId, ProductGroupId);
        }
        //public List<SPSMEntity> GetSampleTypeProduct()
        //{
        //    return CoreFactory.dropdowns.GetSampleTypeProduct();
        //}
        //public List<SPSMEntity> GetProductGroup(int SampleTypeProductId)
        //{
        //    return CoreFactory.dropdowns.GetProductGroup(SampleTypeProductId);
        //}
        //public List<SPSMEntity> GetSubGroup(int SampleTypeProductId, int ProductGroupId)
        //{
        //    return CoreFactory.dropdowns.GetSubGroup(SampleTypeProductId, ProductGroupId);
        //}
        public List<SPSMEntity> GetMatrix(int SampleTypeProductId, int ProductGroupId, int SubGroupId)
        {
            return CoreFactory.dropdowns.GetMatrix(SampleTypeProductId, ProductGroupId, SubGroupId);
        }
        public List<FrequencyEntity> GetFrequency()
        {
            return CoreFactory.dropdowns.GetFrequency();
        }
        public List<UserListEntity> GetUserList(string RoleName, int LabMasterId)
        {
            return CoreFactory.dropdowns.GetUserList(RoleName, LabMasterId);
        }
        public List<BranchEntity> GetBranches(int ParentLabId)
        {
            return CoreFactory.dropdowns.GetBranches(ParentLabId);
        }

        //public List<TestMethodEntity> GetTestMethods()
        //{
        //    return CoreFactory.dropdowns.GetTestMethods();
        //}
        public List<UnitEntity> GetUnits()
        {
            return CoreFactory.dropdowns.GetUnits();
        }
        public List<TestMethodEntity> GetTestMethods(int ParameterGroupId, int ParameterMasterId)
        {
            return CoreFactory.dropdowns.GetTestMethods(ParameterGroupId, ParameterMasterId);
        }
        public List<UnitEntity> GetUnits(int ParameterGroupId, int ParameterMasterId)
        {
            return CoreFactory.dropdowns.GetUnits(ParameterGroupId, ParameterMasterId);
        }
        public IList<CustomerEntity> GetCustomers()
        {
            return CoreFactory.dropdowns.GetCustomers();
        }
        public IList<CommunicationEntity> GetCommunicationMode()
        {
            return CoreFactory.dropdowns.GetCommunicationMode();
        }
        public int GetStatusIdByCode(string StatusCode)
        {
            return CoreFactory.dropdowns.GetStatusIdByCode(StatusCode);
        }
        public IList<InventoryType> GetInventoryTypeList()
        {
            return CoreFactory.dropdowns.GetInventoryTypeList();
        }
        public IList<Core.DropDowns.CatagoryEntity> GetCategoryTypeList()
        {
            return CoreFactory.dropdowns.GetCategoryTypeList();
        }
        public IList<InvUnitEntity> GetUnitTypeList()
        {
            return CoreFactory.dropdowns.GetUnitTypeList();
        }
        public IList<InvCapacityEntity> GetCapacityList()
        {
            return CoreFactory.dropdowns.GetCapacityList();
        }
        public IList<Core.DropDowns.CatagoryEntity> GetFilteredInvCatogaryList(int InventoryTypeId)
        {
            return CoreFactory.dropdowns.GetFilteredInvCatogaryList(InventoryTypeId);
        }
        public List<DisciplineMasterEntity> GetDiscipline()
        {
            return CoreFactory.dropdowns.GetDiscipline();
        }
        public List<RoleMasterEntity> GetRole()
        {
            return CoreFactory.dropdowns.GetRole();
        }
        public List<UserMasterEntity> GetUserMasterList()
        {
            return CoreFactory.dropdowns.GetUserMasterList();
        }
        public List<ParentMenuEntity> GetSubMenu(int ParentMenuId)
        {
            return CoreFactory.dropdowns.GetSubMenu(ParentMenuId);
        }
       
      
        public List<TestMethodEntity> GetTestMethods()
        {
            return CoreFactory.dropdowns.GetTestMethods();
        }
        public List<ParameterMasterEntity> GetParameter()
        {
            return CoreFactory.dropdowns.GetParameter();
        }

        public List<ParentMenuEntity> GetParentMenu()
        {
            return CoreFactory.dropdowns.GetParentMenu();
        }
        public List<ItemEntity> GetInventoryType()
        {
            return CoreFactory.dropdowns.GetInventoryType();
        }
        public List<ItemEntity> GetItemMaster(int InventoryTypeID)
        {
            return CoreFactory.dropdowns.GetItemMaster(InventoryTypeID);
        }
        public List<ItemEntity> GetSupplierName()
        {
            return CoreFactory.dropdowns.GetSupplierName();
        }
    }
}