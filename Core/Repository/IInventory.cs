using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Inventory;

namespace LIMS_DEMO.Core.Repository
{
    public interface IInventory : IDisposable
    {

        string AddCategory(CategoryEntity categoryEntity);
        CategoryEntity Edit(int CatagoryMasterID);
        string EditCatagory(CategoryEntity categoryEntity);
        List<CategoryEntity> GetInventoryCategoryList();
        string DeleteCategory(Core.Inventory.CategoryEntity categoryEntity);
        string AddInventoryItem(InventoryItemEntity inventoryItemEntity);
        List<InventoryItemEntity> GetInventoryItemList(bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool InventoryItemMasterIsActive, bool InventoryItemMasterIsDeleted, bool IsActive, string Mode1);
        List<InventoryItemEntity> SelectInventoryDetails(int ITID, int InventoryCategoryMasterID, int InventoryTypeMasterID, bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool InventoryItemMasterIsActive, bool InventoryItemMasterIsDeleted, bool IsActive, string Mode);
        List<InventoryItemEntity> GetStockItemDataList(bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool InventoryItemMasterIsActive, bool InventoryItemMasterIsDeleted, bool IsActive, string Mode);
        Core.Inventory.CapacityEntity SelectCapacityDataWithID(int InventoryCapacityMasterId);


        string EditInventoryItem(InventoryItemEntity inventoryItemEntity);
        string DeleteItemCategory(Core.Inventory.InventoryItemEntity inventoryItemEntity);
        string InsertStockIssueData(InventoryItemEntity inventoryItemEntity);
        string UpdateCapacityData(CapacityEntity capacityEntity);
        string InsertCapacityMaster(CapacityEntity capacityEntity);
        List<Core.Inventory.CapacityEntity> GetCapacityList();
        string DeleteInvCapacity(InventoryCapacityEntity capacityEntity);
        List<EmployeeEntity> GetEmployeeList();
        Core.Inventory.InventoryItemEntity GetItemDetailsWithID(int ItemMasterId);
        List<InventoryBasicDetailEntity> InventoryBasicDetailsList(int TypeId);

        List<InventoryItemEntity> GetInventoryItemList(bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool InventoryItemMasterIsActive, bool InventoryItemMasterIsDeleted, bool IsActive, int InventoryTypeMasterID1, string Mode1);

        InventoryBasicDetailEntity GetInventoryItemCategoryDetail(int PurchaseRequestID);
        string GenerateInvBasicNumber(int pid);
        long GetSerialNumber(string SrNameType, bool IsActiveField);
        long AddSerialNo(long senovalue);
        string UpdateSerialNo(string SrNameType, bool IsActiveField, long SerialNoUpdate);
        long AddInventoryBasicItemDetails(InventoryBasicDetailEntity inventoryBasicDetailEntity);
        InventoryBasicDetailEntity GetInvBasicNumber(int invBId, bool IsActiveField);
        // Date: 24/02/2021 -- BY ASHWINI
        decimal GetInvAvailableStockbyItemId(int ItemId);
        string UpdateInvAvailableStock(int ItemId, decimal UpdatedAvailableStock);
        long InsertInventoryCommercialData(InventoryCommercialDetailEntity inventoryCommercialDetailEntity);

        // DATE 25/02/2021---BY ASHWINI
        long UpdateInventoryBasicData(InventoryBasicDetailEntity inventoryBasicDetailEntity);
        string DeleteInventoryBasicItemDetail(InventoryBasicDetailEntity inventoryBasicDetailEntity);
        long UpdateInventoryCommercialData(InventoryCommercialDetailEntity inventoryCommercialDetailEntity);
        List<InventoryBasicDetailEntity> GetInventoryBasicDetailBySearch(int ID1, string Mode, string SortBy, string Search, string SortOrder, int StartRow, int EndRow, int TypeID);
        List<InventoryBasicItemDetailEntity> GetInventoryBasicItemDetailBySearch1(int ID1, long InventoryBasicDetailsID, bool InventoryBasicDetailsIsActive, bool InventoryBasicItemDetailsIsActive, string Mode, int ItemID1, int TypeID, int CategoryID);
        List<InventoryCommercialDetailEntity> GetInventoryCommercialDetailsBySearch(int InventoryBasicDetailsID1, string Mode2);

        List<InventoryCommercialFileDetailEntity> GetInventoryCommercialFileDetailBySearch(long InventoryBasicDetailID2, string Mode3);

        List<InventoryBasicDetailEntity> GetInventoryBasicItemDetails(int ID);
        List<InventoryBasicDetailEntity> GetInventoryBasicDetailBySearch2(int ID1, int TypeID1, string SortBy1, string SortOrder1, int StartRow1, int EndRow1, string SearchKeywords1, string Mode1);
        List<InventoryMaintainanceAndCalibrationScheduleEntity> GetMaintainanceAndCalibrationScheduleByBasicItemDetailsID(int InventoryBasicItemDetailsId, string SType, string Mode);
        long InsertInventoryCalibrationDetails(InventoryMaintainanceAndCalibrationEntity imcEntity);

        List<InventoryMaintainanceAndCalibrationScheduleDatesEntity> GetInventoryMaintanceScheduleDate(int ScheduleID);


        string InsertInventoryCommercialFileDetail(int InventoryBasicDetailID, string FileName);

        long InsertInventoryMaintainanceDetail(InventoryMaintainanceAndCalibrationEntity inventoryMaintainanceAndCalibrationEntity);

        // date 26/02/2021
        List<InventoryMaintenanceEntity> InventoryCalibrationAndMaintenanceList(string sActionType);
        List<InventoryMaintainanceAndCalibrationEntity> GetInventoryCalibrationAndMaintenanceUpComingSchedulingDates(string ActionType, string InventoryBasicItemDetailsNumber, string Mode, int ItemID);
        List<Core.Inventory.InventoryItemEntity> GetInventoryItemMasterBySearch(bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool IsActive, int InventoryTypeMasterID, string Mode);
        long AddNotification(string Msg, string RoleName, PurchaseEntity purchaseEntity);
        string UpdateInventoryMaintainanceAndCalibration(InventoryMaintainanceAndCalibrationEntity inventoryMaintainanceAndCalibrationEntity);
        string InsertInventoryMaintainanceAndCalibration(InventoryMaintainanceAndCalibrationEntity inventoryMaintainanceAndCalibrationEntity);

        // date 27/02/2021
        List<InventoryBasicDetailEntity> GetInventoryBasicItemDetailBySearch(int ID1, long InventoryBasicDetailsID1, bool InventoryBasicDetailsIsActive, bool InventoryBasicItemDetailsIsActive, string mode, int ItemID1, int TypeID, int CategoryId);

        List<InventoryMaintainanceAndCalibrationScheduleDatesEntity> GetInventoryMaintainanceAndCalibrationScheduleDatesBySearch(int ID, bool InventoryMaintainanceAndCalibrationScheduleIsActive, bool IsActive, string Type1, string AMCNumber, int InventoryBasicItemDetailsID1, int InventoryMaintainanceAndCalibrationScheduleID, string mode);

        List<InventoryMaintainanceAndCalibrationEntity> GetInventoryMaintainanceAndCalibrationBySearch(int ID, long InventoryBasicDetailsID1, long InventoryBasicItemDetailsID1, int ItemID1, string Type1, string Mode);


        // date 1/03/2021
        List<InventoryMaintenanceEntity> InventoryCalibrationAndMaintenanceList12(string ActionType, string InventoryBasicItemDetailsNumber1, string Mode, int ItemID);
        Core.Inventory.InventoryMaintainanceAndCalibrationScheduleDatesEntity GetScheduleNextDate(long ScheduleDateId);
        Core.Inventory.InventoryMaintainanceAndCalibrationScheduleDatesEntity GetFrequnecyOfDates(long ScheduleDateId, long InventoryBasicItemDetailsID);
        Core.Inventory.InventoryMaintainanceAndCalibrationEntity GetInvetoryMaintainceCalibrationData(long ScheduleDateId, long InventoryBasicItemDetailsID);
        // DATE 5/03/2021
        List<Core.Inventory.InventoryItemEntity> GetInventoryItemMasterBySearch12(int ID, int InventoryCategoryMasterID, int InventoryTypeMasterID, bool InventoryItemMasterIsActive, bool InventoryItemMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool IsActive, string Mode);
        long InsertInventoryBasicData(InventoryBasicDetailEntity inventoryBasicDetailEntity);
        long UpdateMaintainanceDetails(InventoryMaintainanceAndCalibrationEntity imcEntity);
    }
}
