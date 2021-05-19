using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Inventory;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.DropDowns;
using LIMS_DEMO.Core;
using LIMS_DEMO.DAL;
using LIMS_DEMO.DAL.Inventory;

namespace LIMS_DEMO.Core.Repository
{
   public  interface IDropDown
    {
        List<InvoiceEntity> GetPaymentMode();
        IList<InventoryType> GetInventoryCategoryType();
        List<UserListEntity> GetPassChaReqUserList();
        List<SPSMEntity> GetSampleCode();
        List<UserListEntity> GetGroupInchargeList(string Name, int LabMasterId);
        List<UserListEntity> GetApproverList(string RoleName, int LabMasterId);
        List<UserListEntity> GetPlannerList(string RoleName, int LabMasterId, int DisciplineId);
        List<CollectorEntity> GetReceiver(int LabMasterId);
        List<EnvironmentalConditionEntity> GetEnvironmentalCondition();
        List<SampleTypeEntity> GetSampleType();
        List<ProcedureEntity> GetProcedure(int SampleTypeProductId);
        List<SampleDeviceEntity> GetSampleDevice(int SampleTypeProductId);
        List<SampleDescriptionEntity> GetSampleDescription(int ProductGroupId, int SubGroupId);
        List<UserListEntity> GetUserList(string RoleName, int LabMasterId);
        List<FrequencyEntity> GetFrequency();
        List<BranchEntity> GetBranches(int ParentLabId);
        List<TestMethodEntity> GetTestMethods();
        List<TestMethodEntity> GetTestMethods(int ParameterGroupId, int ParameterMasterId);
        List<UnitEntity> GetUnits();
        List<UnitEntity> GetUnits(int ParameterGroupId, int ParameterMasterId);
       
        int GetStatusIdByCode(string StatusCode);
        IList<CommunicationEntity> GetCommunicationMode();
        IList<Customer.CustomerEntity> GetCustomers();
        IList<InventoryType> GetInventoryTypeList();
        IList<Core.DropDowns.CatagoryEntity> GetCategoryTypeList();
        IList<InvUnitEntity> GetUnitTypeList();
        IList<InvCapacityEntity> GetCapacityList();
        IList<Core.DropDowns.CatagoryEntity> GetFilteredInvCatogaryList(int InventoryTypeId);
        List<DisciplineMasterEntity> GetDiscipline();
        List<RoleMasterEntity> GetRole();
        List<UserMasterEntity> GetUserMasterList();
        List<ParentMenuEntity> GetSubMenu(int ParentMenuId);
        List<SPSMEntity> GetProductGroup(int SampleTypeProductId);
        List<SPSMEntity> GetSubGroup(int SampleTypeProductId, int ProductGroupId);
        List<SPSMEntity> GetMatrix(int SampleTypeProductId, int ProductGroupId, int SubGroupId);
        List<SPSMEntity> GetSampleTypeProduct();
        //List<SPSMEntity> GetSampleTypeProduct();
        //List<UnitEntity> GetUnits();
        //List<TestMethodEntity> GetTestMethods();
        List<ParameterMasterEntity> GetParameter();
        List<ParentMenuEntity> GetParentMenu();
        List<ItemEntity> GetInventoryType();
        List<ItemEntity> GetItemMaster(int InventoryTypeID);
        List<ItemEntity> GetSupplierName();
    }
}
