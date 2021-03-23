using LIMS_DEMO.Core.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Repository
{
    public interface IInventoryBreakDown
    {
        #region BREAKDOWN
        IList<ItemSerialNumberEntity> GetSerialNumbersByID(int ItemId, int InventoryBasicDetailsId);
        IList<BreakDownEntity> GetBreakDowmDataBySrNo(int InventoryBasicItemDetailsID, int InventorybasicdetailsID, int InventoryBasicDetailID);
        IList<BreakDownEntity> GetBreakDowmDataList();
        IList<ItemSerialNumberEntity> GetSerialNumbersByID_1(int ItemId, int InventoryBasicDetailsId);
        bool AddBreakdown(BreakDownEntity BreakDown);
        BreakDownEntity GetBreakDowmDataEdit(int ID);

        #endregion

        int _FindPerformanceCheck(int InventoryBasicItemDetailsID);
        IList<ItemSerialNumberEntity> GetSerialNumbersByIBID(int ItemId, int InventoryBasicDetailsId);
        IList<ScheduleDateEntity> GetscheduleDateByItemNumber(int InventoryBasicItemDetailsId);
        bool savePerformanceCheck(InventoryMaintainanceAndCalibrationEntity entity);
        InventoryMaintainanceAndCalibrationEntity GetDatabyDateID(int ItemID, long DateID, long BasicDetailsID);
    }
}
