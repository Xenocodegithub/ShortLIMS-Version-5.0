using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Inventory;
using LIMS_DEMO.DAL.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.BAL.Inventory
{
    public class InventoryBreakDownBAL
    {
        public InventoryBreakDownBAL()
        {
            CoreFactory.inventoryBreakDown = new InventoryBreakDownDAL();
        }



        #region BREAKDOWN

        public IList<ItemSerialNumberEntity> GetSerialNumbersByID(int ItemId, int InventoryBasicDetailsId)
        {
            return CoreFactory.inventoryBreakDown.GetSerialNumbersByID( ItemId, InventoryBasicDetailsId);
        }
        public IList<BreakDownEntity> GetBreakDowmDataBySrNo(int InventoryBasicItemDetailsID, int InventorybasicdetailsID, int InventoryBasicDetailID)
        {
            return CoreFactory.inventoryBreakDown.GetBreakDowmDataBySrNo(InventoryBasicItemDetailsID, InventorybasicdetailsID, InventoryBasicDetailID);
        }
        public bool AddBreakdown(BreakDownEntity BreakDown)
        {
            return CoreFactory.inventoryBreakDown.AddBreakdown(BreakDown);
        }
        public BreakDownEntity GetBreakDowmDataEdit(int ID)
        {
            return CoreFactory.inventoryBreakDown.GetBreakDowmDataEdit(ID);
        }
        public IList<BreakDownEntity> GetBreakDowmDataList()
        {
            return CoreFactory.inventoryBreakDown.GetBreakDowmDataList();
        }
        public IList<ItemSerialNumberEntity> GetSerialNumbersByID_1(int ItemId, int InventoryBasicDetailsId)
        {
            return CoreFactory.inventoryBreakDown.GetSerialNumbersByID_1(ItemId, InventoryBasicDetailsId);
        }
        public int _FindPerformanceCheck(int InventoryBasicItemDetailsID)
        {
            return CoreFactory.inventoryBreakDown._FindPerformanceCheck(InventoryBasicItemDetailsID);
        }
        public IList<ItemSerialNumberEntity> GetSerialNumbersByIBID(int ItemId, int InventoryBasicDetailsId)
        {
            return CoreFactory.inventoryBreakDown.GetSerialNumbersByIBID( ItemId,  InventoryBasicDetailsId);
        }
        public IList<ScheduleDateEntity> GetscheduleDateByItemNumber(int InventoryBasicItemDetailsId)
        {
            return CoreFactory.inventoryBreakDown.GetscheduleDateByItemNumber( InventoryBasicItemDetailsId);
        }
        public bool savePerformanceCheck(InventoryMaintainanceAndCalibrationEntity entity)
        {
            return CoreFactory.inventoryBreakDown.savePerformanceCheck(entity);
        }
        public InventoryMaintainanceAndCalibrationEntity GetDatabyDateID(int ItemID, long DateID, long BasicDetailsID)
        {
            return CoreFactory.inventoryBreakDown.GetDatabyDateID(ItemID, DateID, BasicDetailsID);
        }
        #endregion
    }
}