using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Inventory;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.DropDowns;
using LIMS_DEMO.DAL.Inventory;


namespace LIMS_DEMO.BAL.Inventory
{
    public class NewInventoryBAL
    {
        public NewInventoryBAL()
        {
            CoreFactory.inventory = new NewInventoryDAL();
        }
        public long UpdateMaintainanceDetails(InventoryMaintainanceAndCalibrationEntity imcEntity) 
        {
            return CoreFactory.inventory.UpdateMaintainanceDetails(imcEntity);

        } 
        // Add Data into CategoryMaster Table
        public string AddCategory(Core.Inventory.CategoryEntity categoryEntity)
        {
            return CoreFactory.inventory.AddCategory(categoryEntity);
        }
        // For Edit Get Data from CategoryMaster Table
        public Core.Inventory.CategoryEntity Edit(int CatagoryMasterID)
        {
            return CoreFactory.inventory.Edit(CatagoryMasterID);
        }
        public string EditCatagory(Core.Inventory.CategoryEntity categoryEntity)
        {
            return CoreFactory.inventory.EditCatagory(categoryEntity);
        }
        public string DeleteCategory(Core.Inventory.CategoryEntity categoryEntity)
        {
            return CoreFactory.inventory.DeleteCategory(categoryEntity);
        }
        // get list of CategoryDetails Data from  CategoryMaster
        public List<Core.Inventory.CategoryEntity> GetInventoryCategoryList()
        {
            return CoreFactory.inventory.GetInventoryCategoryList();
        }
        public string AddInventoryItem(InventoryItemEntity inventoryItemEntity)
        {
            return CoreFactory.inventory.AddInventoryItem(inventoryItemEntity);
        }
        public List<InventoryItemEntity> GetInventoryItemList(bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool InventoryItemMasterIsActive, bool InventoryItemMasterIsDeleted, bool IsActive, string Mode1)
        {
            return CoreFactory.inventory.GetInventoryItemList(InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, IsActive, Mode1);
        }
        public List<InventoryItemEntity> SelectInventoryDetails(int ITID, int InventoryCategoryMasterID, int InventoryTypeMasterID, bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool InventoryItemMasterIsActive, bool InventoryItemMasterIsDeleted, bool IsActive, string Mode)
        {
            return CoreFactory.inventory.SelectInventoryDetails(ITID,InventoryCategoryMasterID,InventoryTypeMasterID, InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted,InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, IsActive, Mode);
        }
        public string EditInventoryItem(InventoryItemEntity inventoryItemEntity)
        {
            return CoreFactory.inventory.EditInventoryItem(inventoryItemEntity);
        }
        public string DeleteItemCategory(Core.Inventory.InventoryItemEntity inventoryItemEntity)
        {
            return CoreFactory.inventory.DeleteItemCategory(inventoryItemEntity);
        }
        public string InsertStockIssueData(InventoryItemEntity inventoryItemEntity)
        {
            return CoreFactory.inventory.InsertStockIssueData(inventoryItemEntity);
        }
        public List<EmployeeEntity> GetEmployeeList()
        {
            return CoreFactory.inventory.GetEmployeeList();

        }
        public Core.Inventory.InventoryItemEntity GetItemDetailsWithID(int ItemMasterId)
        {
            return CoreFactory.inventory.GetItemDetailsWithID(ItemMasterId);
        }
        public List<InventoryItemEntity> GetStockItemDataList(bool InventoryTypeMasterIsActive,bool InventoryTypeMasterIsDeleted,bool InventoryCategoryMasterIsActive,bool InventoryCategoryMasterIsDeleted,bool InventoryItemMasterIsActive,bool InventoryItemMasterIsDeleted,bool IsActive,string Mode)
        {
            return CoreFactory.inventory.GetStockItemDataList(InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, IsActive, Mode);
        }

        // capacity
        public string InsertCapacityMaster(CapacityEntity capacityEntity)
        {
            return CoreFactory.inventory.InsertCapacityMaster(capacityEntity);
        }
        public List<Core.Inventory.CapacityEntity> GetCapacityList()
        {
            return CoreFactory.inventory.GetCapacityList();
        }
        public Core.Inventory.CapacityEntity SelectCapacityDataWithID(int InventoryCapacityMasterId)
        {
            return CoreFactory.inventory.SelectCapacityDataWithID(InventoryCapacityMasterId);
        }
        public string UpdateCapacityData(CapacityEntity capacityEntity)
        {
            return CoreFactory.inventory.UpdateCapacityData(capacityEntity);
        }
        public string DeleteInvCapacity(InventoryCapacityEntity  capacityEntity)
        {
            return CoreFactory.inventory.DeleteInvCapacity(capacityEntity);
        }
        // Consumable List
        public List<InventoryBasicDetailEntity> InventoryBasicDetailsList(int TypeId)
        {
            return CoreFactory.inventory.InventoryBasicDetailsList(TypeId);
        }

        public List<InventoryItemEntity> GetInventoryItemList(bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool InventoryItemMasterIsActive, bool InventoryItemMasterIsDeleted, bool IsActive, int InventoryTypeMasterID1, string Mode1)
        {
            return CoreFactory.inventory.GetInventoryItemList(InventoryTypeMasterIsActive,InventoryTypeMasterIsDeleted,InventoryCategoryMasterIsActive,InventoryCategoryMasterIsDeleted,InventoryItemMasterIsActive,InventoryItemMasterIsDeleted,IsActive,InventoryTypeMasterID1,Mode1);
        }
        public InventoryBasicDetailEntity GetInventoryItemCategoryDetail(int PurchaseRequestID)
        {
            return CoreFactory.inventory.GetInventoryItemCategoryDetail(PurchaseRequestID);
        }
        public string GenerateInvBasicNumber(int pid)
        {
            return CoreFactory.inventory.GenerateInvBasicNumber(pid);
        }
        public long AddInventoryBasicItemDetails(InventoryBasicDetailEntity inventoryBasicDetailEntity)
        {
            return CoreFactory.inventory.AddInventoryBasicItemDetails(inventoryBasicDetailEntity);
        }
        public InventoryBasicDetailEntity GetInvBasicNumber(int invBId, bool IsActiveField)
        {
            return CoreFactory.inventory.GetInvBasicNumber(invBId, IsActiveField);
        }

        // Date: 24/02/2021
        public decimal GetInvAvailableStockbyItemId(int ItemId)
        {
            return CoreFactory.inventory.GetInvAvailableStockbyItemId(ItemId);
        }
        public string UpdateInvAvailableStock(int ItemId, decimal UpdatedAvailableStock)
        {
            return CoreFactory.inventory.UpdateInvAvailableStock(ItemId,UpdatedAvailableStock);
        }

        public long InsertInventoryCommercialData(InventoryCommercialDetailEntity inventoryCommercialDetailEntity)
        {
            return CoreFactory.inventory.InsertInventoryCommercialData(inventoryCommercialDetailEntity);
        }
        // Date: 25/02/2021  BY ASHWINI
        public long InsertInventoryBasicData(InventoryBasicDetailEntity inventoryBasicDetailEntity)
        {
            return CoreFactory.inventory.InsertInventoryBasicData(inventoryBasicDetailEntity);
        }
        public long UpdateInventoryBasicData(InventoryBasicDetailEntity inventoryBasicDetailEntity)
        {
            return CoreFactory.inventory.UpdateInventoryBasicData(inventoryBasicDetailEntity);
        }
        public string DeleteInventoryBasicItemDetail(InventoryBasicDetailEntity inventoryBasicDetailEntity)
        {
            return CoreFactory.inventory.DeleteInventoryBasicItemDetail(inventoryBasicDetailEntity);
        }
        public long UpdateInventoryCommercialData(InventoryCommercialDetailEntity inventoryCommercialDetailEntity)
        {
            return CoreFactory.inventory.UpdateInventoryCommercialData(inventoryCommercialDetailEntity);
        }
        public List<InventoryBasicDetailEntity> GetInventoryBasicDetailBySearch(int ID1, string Mode, string SortBy, string Search, string SortOrder, int StartRow, int EndRow, int TypeID)
        {
            return CoreFactory.inventory.GetInventoryBasicDetailBySearch(ID1, Mode, SortBy, Search, SortOrder, StartRow, EndRow, TypeID);

        }
        public List<InventoryBasicItemDetailEntity> GetInventoryBasicItemDetailBySearch1(int ID1, long InventoryBasicDetailsID, bool InventoryBasicDetailsIsActive, bool InventoryBasicItemDetailsIsActive, string Mode, int ItemID1, int TypeID, int CategoryID)
        {
            return CoreFactory.inventory.GetInventoryBasicItemDetailBySearch1(ID1, InventoryBasicDetailsID, InventoryBasicDetailsIsActive, InventoryBasicItemDetailsIsActive, Mode, ItemID1, TypeID, CategoryID);
        }
        public List<InventoryCommercialDetailEntity> GetInventoryCommercialDetailsBySearch(int InventoryBasicDetailsID1, string Mode2)
        {
            return CoreFactory.inventory.GetInventoryCommercialDetailsBySearch(InventoryBasicDetailsID1, Mode2);
        }
        public List<InventoryCommercialFileDetailEntity> GetInventoryCommercialFileDetailBySearch(long InventoryBasicDetailID2, string Mode3)
        {
            return CoreFactory.inventory.GetInventoryCommercialFileDetailBySearch(InventoryBasicDetailID2, Mode3);
        }
        public List<InventoryBasicDetailEntity> GetInventoryBasicItemDetails(int ID)
        {
            return CoreFactory.inventory.GetInventoryBasicItemDetails(ID);
        }
        public List<InventoryBasicDetailEntity> GetInventoryBasicDetailBySearch2(int ID1, int TypeID1, string SortBy1, string SortOrder1, int StartRow1, int EndRow1, string SearchKeywords1, string Mode1)
        {
            return CoreFactory.inventory.GetInventoryBasicDetailBySearch2(ID1, TypeID1, SortBy1, SortOrder1, StartRow1, EndRow1, SearchKeywords1, Mode1);
        }
        public List<InventoryMaintainanceAndCalibrationScheduleEntity> GetMaintainanceAndCalibrationScheduleByBasicItemDetailsID(int InventoryBasicItemDetailsId, string SType, string Mode)
        {
            return CoreFactory.inventory.GetMaintainanceAndCalibrationScheduleByBasicItemDetailsID(InventoryBasicItemDetailsId, SType, Mode);
        }
        public List<InventoryMaintainanceAndCalibrationScheduleDatesEntity> GetInventoryMaintanceScheduleDate(int ScheduleID)
        {
            return CoreFactory.inventory.GetInventoryMaintanceScheduleDate(ScheduleID);
        }
        public string InsertInventoryCommercialFileDetail(int InventoryBasicDetailID,string  FileName)
        {
            return CoreFactory.inventory.InsertInventoryCommercialFileDetail(InventoryBasicDetailID, FileName);
        }
        public long InsertInventoryMaintainanceDetail(InventoryMaintainanceAndCalibrationEntity inventoryMaintainanceAndCalibrationEntity)
        {
            return CoreFactory.inventory.InsertInventoryMaintainanceDetail(inventoryMaintainanceAndCalibrationEntity);
        }
        public long InsertInventoryCalibrationDetails(InventoryMaintainanceAndCalibrationEntity imcEntity)
        {
            return CoreFactory.inventory.InsertInventoryCalibrationDetails(imcEntity);
        }
           
       
        // date 26/02/2021
        public List<InventoryMaintenanceEntity> InventoryCalibrationAndMaintenanceList(string sActionType)
        {
            return CoreFactory.inventory.InventoryCalibrationAndMaintenanceList(sActionType);
        }
        public List<InventoryMaintainanceAndCalibrationEntity> GetInventoryCalibrationAndMaintenanceUpComingSchedulingDates(string ActionType, string InventoryBasicItemDetailsNumber, string Mode, int ItemID)
        {
            return CoreFactory.inventory.GetInventoryCalibrationAndMaintenanceUpComingSchedulingDates( ActionType,  InventoryBasicItemDetailsNumber,  Mode,  ItemID);
        }
        public List<Core.Inventory.InventoryItemEntity> GetInventoryItemMasterBySearch(bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool IsActive, int InventoryTypeMasterID, string Mode)
        {
            return CoreFactory.inventory.GetInventoryItemMasterBySearch(InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, IsActive, InventoryTypeMasterID, Mode);
        }
        public long AddNotification(string Msg, string RoleName, PurchaseEntity purchaseEntity)
        {
            return CoreFactory.inventory.AddNotification(Msg, RoleName, purchaseEntity);
        }
        public string UpdateInventoryMaintainanceAndCalibration(InventoryMaintainanceAndCalibrationEntity inventoryMaintainanceAndCalibrationEntity)
        {
            return CoreFactory.inventory.UpdateInventoryMaintainanceAndCalibration(inventoryMaintainanceAndCalibrationEntity);
        }
        public string InsertInventoryMaintainanceAndCalibration(InventoryMaintainanceAndCalibrationEntity inventoryMaintainanceAndCalibrationEntity)
        {
            return CoreFactory.inventory.InsertInventoryMaintainanceAndCalibration(inventoryMaintainanceAndCalibrationEntity);
        }
        public List<InventoryBasicDetailEntity> GetInventoryBasicItemDetailBySearch(int ID1, long InventoryBasicDetailsID1, bool InventoryBasicDetailsIsActive, bool InventoryBasicItemDetailsIsActive, string mode, int ItemID1, int TypeID, int CategoryId)
        {
            return CoreFactory.inventory.GetInventoryBasicItemDetailBySearch(ID1, InventoryBasicDetailsID1, InventoryBasicDetailsIsActive, InventoryBasicItemDetailsIsActive, mode, ItemID1, TypeID, CategoryId);
        }
        public List<InventoryMaintainanceAndCalibrationScheduleDatesEntity> GetInventoryMaintainanceAndCalibrationScheduleDatesBySearch(int ID, bool InventoryMaintainanceAndCalibrationScheduleIsActive, bool IsActive, string Type1, string AMCNumber, int InventoryBasicItemDetailsID1, int InventoryMaintainanceAndCalibrationScheduleID, string mode)
        {
            return CoreFactory.inventory.GetInventoryMaintainanceAndCalibrationScheduleDatesBySearch(ID, InventoryMaintainanceAndCalibrationScheduleIsActive, IsActive, Type1, AMCNumber, InventoryBasicItemDetailsID1, InventoryMaintainanceAndCalibrationScheduleID, mode);
        }
        public List<InventoryMaintainanceAndCalibrationEntity> GetInventoryMaintainanceAndCalibrationBySearch(int ID, long InventoryBasicDetailsID1, long InventoryBasicItemDetailsID1, int ItemID1, string Type1, string Mode)
        {
            return CoreFactory.inventory.GetInventoryMaintainanceAndCalibrationBySearch(ID, InventoryBasicDetailsID1, InventoryBasicItemDetailsID1, ItemID1, Type1, Mode);
        }

        // date  1/03/2021
        public List<InventoryMaintenanceEntity> InventoryCalibrationAndMaintenanceList12(string ActionType, string InventoryBasicItemDetailsNumber1, string Mode, int ItemID)
        {
            return CoreFactory.inventory.InventoryCalibrationAndMaintenanceList12(ActionType, InventoryBasicItemDetailsNumber1, Mode, ItemID);
        }
        public Core.Inventory.InventoryMaintainanceAndCalibrationScheduleDatesEntity GetScheduleNextDate(long ScheduleDateId)
        {
            return Core.CoreFactory.inventory.GetScheduleNextDate(ScheduleDateId);
        }
        public Core.Inventory.InventoryMaintainanceAndCalibrationScheduleDatesEntity GetFrequnecyOfDates(long ScheduleDateId, long InventoryBasicItemDetailsID)
        {
            return Core.CoreFactory.inventory.GetFrequnecyOfDates(ScheduleDateId, InventoryBasicItemDetailsID);
        }
        public Core.Inventory.InventoryMaintainanceAndCalibrationEntity GetInvetoryMaintainceCalibrationData(long ScheduleDateId, long InventoryBasicItemDetailsID)
        {
            return Core.CoreFactory.inventory.GetInvetoryMaintainceCalibrationData(ScheduleDateId, InventoryBasicItemDetailsID);
        }
        public List<Core.Inventory.InventoryItemEntity> GetInventoryItemMasterBySearch12(int ID, int InventoryCategoryMasterID, int InventoryTypeMasterID, bool InventoryItemMasterIsActive, bool InventoryItemMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool IsActive, string Mode)
        {
            return Core.CoreFactory.inventory.GetInventoryItemMasterBySearch12(ID, InventoryCategoryMasterID, InventoryTypeMasterID, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryTypeMasterIsActive,InventoryTypeMasterIsDeleted, IsActive, Mode);
        }
    }
}