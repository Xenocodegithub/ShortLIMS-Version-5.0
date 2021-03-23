using LIMS_DEMO.Common;
using LIMS_DEMO.Core.Inventory;
using LIMS_DEMO.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.DAL.Inventory
{
    public class InventoryBreakDownDAL: IInventoryBreakDown
    {
        readonly LIMSEntities _dbContext;
        public InventoryBreakDownDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public IList<ItemSerialNumberEntity> GetSerialNumbersByID(int ItemId, int InventoryBasicDetailsId)
        {
            try
            {
                return (from p in _dbContext.InventoryBasicItemDetails
                        join s in _dbContext.InventoryBasicDetails on p.InventoryBasicDetailsID equals s.ID
                        where p.IsActive == true && s.ItemID == ItemId || (s.ItemID == ItemId && p.InventoryBasicDetailsID == InventoryBasicDetailsId)
                        select new ItemSerialNumberEntity()
                        {
                            ID = p.ID,
                            InventoryBasicDetailsID = p.InventoryBasicDetailsID,
                            InventoryBasicItemDetailsNumber = p.InventoryBasicItemDetailsNumber
                        }).OrderBy(p => p.InventoryBasicItemDetailsNumber).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IList<BreakDownEntity> GetBreakDowmDataBySrNo(int InventoryBasicItemDetailsID, int InventorybasicdetailsID, int InventoryBasicDetailID)
        {
            try
            {
                return (from p in _dbContext.InventoryMaintAndCalibBreakDowns
                        join s in _dbContext.InventoryBasicItemDetails on p.InventoryBasicItemDetailsId equals s.ID
                        where p.InventoryBasicItemDetailsId == InventoryBasicItemDetailsID && p.InventoryBasicDetailId == InventorybasicdetailsID && p.ItemMasterId == InventoryBasicDetailID && s.IsActive==true && p.IsRepair==true
                        select new BreakDownEntity()
                        {
                            InventoryMaintAndCalibBreakDownId = p.InventoryMaintAndCalibBreakDownId,
                            NameOfInstrument = s.InventoryBasicItemDetailsNumber,
                            ProblemDescription = p.ProblemDescription,
                            NameOfAgency = p.NameOfAgency,
                            dateOfRepair = p.DateOfStartRepair + " " + p.DateOfCompletionRepair,
                            Expenses = p.Expenses,
                        }).OrderBy(s => s.NameOfInstrument).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool AddBreakdown(BreakDownEntity BreakDown)
        {
            try
            {
                if (BreakDown.ID == null)
                {
                    _dbContext.InventoryMaintAndCalibBreakDowns.Add(new InventoryMaintAndCalibBreakDown()
                    {
                        ItemMasterId = BreakDown.ItemMasterId,
                        InventoryBasicItemDetailsId = BreakDown.InventoryBasicItemDetailsId,
                        InventoryBasicDetailId = BreakDown.InventoryBasicDetailId,
                        ProblemReportedBy = BreakDown.ProblemReportedBy,
                        ProblemReportedDate = BreakDown.ProblemReportedDate,
                        ProblemDescription = BreakDown.ProblemDescription,
                        DateOfStartRepair = BreakDown.DateOfStartRepair,
                        DateOfCompletionRepair = BreakDown.DateOfCompletionRepair,
                        MaintenanceInspectedBy = BreakDown.MaintenanceInspectedBy,
                        NameOfAgency = BreakDown.NameOfAgency,
                        Expenses = BreakDown.Expenses,
                        Remark = BreakDown.Remark,
                        IsRepair= BreakDown.IsRepair
                    });
                    if (BreakDown.IsRepair == false)
                    {
                        var active = _dbContext.InventoryBasicItemDetails.Find(BreakDown.InventoryBasicItemDetailsId);
                        active.IsActive = false;
                    }
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    var breakDown = _dbContext.InventoryMaintAndCalibBreakDowns.Find(BreakDown.ID);
                    breakDown.ItemMasterId = BreakDown.ItemMasterId;
                    breakDown.InventoryBasicDetailId = BreakDown.InventoryBasicDetailId;
                    breakDown.ProblemReportedBy = BreakDown.ProblemReportedBy;
                    breakDown.ProblemDescription = BreakDown.ProblemDescription;
                    breakDown.NameOfAgency = BreakDown.NameOfAgency;
                    breakDown.MaintenanceInspectedBy = BreakDown.MaintenanceInspectedBy;
                    breakDown.Expenses = BreakDown.Expenses;
                    breakDown.Remark = BreakDown.Remark;
                    if (BreakDown.DateOfStartRepair != null)
                    {
                        breakDown.DateOfStartRepair = BreakDown.DateOfStartRepair;
                    }
                    if (BreakDown.DateOfCompletionRepair != null)
                    {
                        breakDown.DateOfCompletionRepair = BreakDown.DateOfCompletionRepair;
                    }
                    if (BreakDown.ProblemReportedDate != null)
                    {
                        breakDown.ProblemReportedDate = BreakDown.ProblemReportedDate;
                    }
                    if (BreakDown.InventoryBasicDetailId != 0)
                    {
                        breakDown.InventoryBasicDetailId = BreakDown.InventoryBasicDetailId;
                    }
                    if (BreakDown.IsRepair == false)
                    {
                        var active = _dbContext.InventoryBasicItemDetails.Find(BreakDown.InventoryBasicItemDetailsId);
                        active.IsActive = false;
                    }
                    _dbContext.SaveChanges();
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public BreakDownEntity GetBreakDowmDataEdit(int ID)
        {
            try
            {
                return (from p in _dbContext.InventoryMaintAndCalibBreakDowns
                        join s in _dbContext.InventoryBasicItemDetails on p.InventoryBasicItemDetailsId equals s.ID
                        join m in _dbContext.InventoryItemMasters on p.ItemMasterId equals m.ID
                        join cm in _dbContext.InventoryCategoryMasters on m.CategoryID equals cm.ID
                        where p.InventoryMaintAndCalibBreakDownId == ID
                        select new BreakDownEntity()
                        {
                            InventoryMaintAndCalibBreakDownId = p.InventoryMaintAndCalibBreakDownId,
                            ItemMasterId = p.ItemMasterId,
                            IDNO = s.InventoryBasicItemDetailsNumber,
                            InventoryBasicItemDetailsId = p.InventoryBasicItemDetailsId,
                            InventoryBasicDetailId = p.InventoryBasicDetailId,
                            ProblemReportedBy = p.ProblemReportedBy,
                            ProblemReportedDate = p.ProblemReportedDate,
                            ProblemDescription = p.ProblemDescription,
                            DateOfStartRepair = p.DateOfStartRepair,
                            DateOfCompletionRepair = p.DateOfCompletionRepair,
                            MaintenanceInspectedBy = p.MaintenanceInspectedBy,
                            NameOfAgency = p.NameOfAgency,
                            Expenses = p.Expenses,
                            Remark = p.Remark,
                            IsRepair = s.IsActive,
                            NameOfInstrument = m.Name
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IList<BreakDownEntity> GetBreakDowmDataList()
        {
            try
            {
                return (from p in _dbContext.InventoryMaintAndCalibBreakDowns
                        join s in _dbContext.InventoryBasicItemDetails on p.InventoryBasicItemDetailsId equals s.ID
                        join m in _dbContext.InventoryItemMasters on p.ItemMasterId equals m.ID
                        join cm in _dbContext.InventoryCategoryMasters on m.CategoryID equals cm.ID

                        select new BreakDownEntity()
                        {
                            InventoryMaintAndCalibBreakDownId = p.InventoryMaintAndCalibBreakDownId,
                            ItemMasterId = p.ItemMasterId,
                            IDNO = s.InventoryBasicItemDetailsNumber,
                            InventoryBasicItemDetailsId = p.InventoryBasicItemDetailsId,
                            InventoryBasicDetailId = p.InventoryBasicDetailId,
                            ProblemReportedBy = p.ProblemReportedBy,
                            ProblemReportedDate = p.ProblemReportedDate,
                            ProblemDescription = p.ProblemDescription,
                            DateOfStartRepair = p.DateOfStartRepair,
                            DateOfCompletionRepair = p.DateOfCompletionRepair,
                            MaintenanceInspectedBy = p.MaintenanceInspectedBy,
                            NameOfAgency = p.NameOfAgency,
                            Expenses = p.Expenses,
                            Remark = p.Remark,
                            IsRepair = s.IsActive,
                            NameOfInstrument = m.Name
                        }).OrderBy(s => s.NameOfInstrument).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IList<ItemSerialNumberEntity> GetSerialNumbersByID_1(int ItemId, int InventoryBasicDetailsId)
        {
            try
            {
                return (from p in _dbContext.InventoryBasicItemDetails
                        join s in _dbContext.InventoryBasicDetails on p.InventoryBasicDetailsID equals s.ID
                        where p.IsActive == true && p.ID == ItemId || (s.ItemID == ItemId && p.InventoryBasicDetailsID == InventoryBasicDetailsId)
                        select new ItemSerialNumberEntity()
                        {
                            ID = p.ID,
                            InventoryBasicDetailsID = p.InventoryBasicDetailsID,
                            InventoryBasicItemDetailsNumber = p.InventoryBasicItemDetailsNumber
                        }).OrderBy(p => p.InventoryBasicItemDetailsNumber).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int _FindPerformanceCheck(int InventoryBasicItemDetailsID)
        {
            try
            {
                var Result = (from p in _dbContext.InventoryMaintainanceAndCalibrationSchedules
                              where InventoryBasicItemDetailsID == p.InventoryBasicItemDetailsID && p.Type == "PerFormance-Check"
                              select new InventoryItemEntity()
                              {
                                  ID=p.ID
                              }).FirstOrDefault();

                return Result.ID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //public long InsertInventoryMaintainanceDetailUpdate(InventoryMaintainanceAndCalibrationEntity imcEntity,int ID)
        //{
        //    try
        //    {
        //        ObjectParameter objectParameter = new ObjectParameter("iID", typeof(long));
        //        //ObjectParameter objectParameter2 = new ObjectParameter("iErrorCode", typeof(long));
        //        var Result1 = _dbContext.USP_InventoryMaintainanceAndCalibrationSchedule_Insert((Int32)imcEntity.InventoryBasicItemDetailsID, imcEntity.AMCVendorName, imcEntity.AMCNumber, imcEntity.AMCDate, imcEntity.StartDate, imcEntity.AMCValue, (short)imcEntity.AMCPeriod, (short)imcEntity.Frequency, imcEntity.Type, objectParameter, imcEntity.InsertedBy);
        //        this._dbContext.SaveChanges();
        //        return Result1; // return 
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}

        public IList<ItemSerialNumberEntity> GetSerialNumbersByIBID(int ItemId, int InventoryBasicDetailsId) // 2
        {
            try
            {
                return (from p in _dbContext.InventoryBasicItemDetails
                        join s in _dbContext.InventoryBasicDetails on p.InventoryBasicDetailsID equals s.ID
                        where p.IsActive == true && s.ItemID == ItemId && p.InventoryBasicDetailsID == InventoryBasicDetailsId
                        select new ItemSerialNumberEntity()
                        {
                            ID = p.ID,
                            InventoryBasicDetailsID = p.InventoryBasicDetailsID,
                            InventoryBasicItemDetailsNumber = p.InventoryBasicItemDetailsNumber
                        }).OrderBy(p => p.InventoryBasicItemDetailsNumber).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public IList<ScheduleDateEntity> GetscheduleDateByItemNumber(int InventoryBasicItemDetailsId) // 3
        {
            try
            {
                return (from p in _dbContext.InventoryMaintainanceAndCalibrationSchedules
                        join s in _dbContext.InventoryMaintainanceAndCalibrationScheduleDates on p.ID equals s.InventoryMaintainanceAndCalibrationScheduleID
                        where p.InventoryBasicItemDetailsID == InventoryBasicItemDetailsId && p.Type == "PerFormance-Check" && (s.CompletionStatus == null|| s.CompletionStatus== "In Progress")
                        select new ScheduleDateEntity()
                        {
                            InventoryMaintainanceAndCalibrationScheduleDatesID = s.InventoryMaintainanceAndCalibrationScheduleID,
                            DateID = s.ID,
                            ScheduleDate = s.ScheduleDate
                            //scdate=s.ScheduleDate== null? "":Convert.ToDateTime(s.ScheduleDate).ToString("ddMMyyyy")
                        }).OrderBy(m => m.ScheduleDate).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool savePerformanceCheck(InventoryMaintainanceAndCalibrationEntity entity)
        {
            if (entity != null)
            {
                var InventoryItemmaster = _dbContext.InventoryMaintainanceAndCalibrationScheduleDates.Find(entity.InventoryMaintainanceAndCalibrationScheduleDatesID);
                if (InventoryItemmaster.CompletionStatus == null || InventoryItemmaster.CompletionStatus == "In Progress")
                {
                    InventoryItemmaster.CompletionStatus = entity.CompletionStatus;
                }

                _dbContext.InventoryMaintainanceAndCalibrations.Add(new InventoryMaintainanceAndCalibration()
                {
                    Type = entity.Type,
                    InventoryBasicDetailsID = entity.InventoryBasicDetailsID,
                    Auditor = entity.Auditor,
                    AuditDate = entity.AuditDate,
                    AuditObservations = entity.AuditObservations,
                    ActionTaken = entity.ActionTaken,
                    ItemID = (int)entity.ItemID,
                    InventoryMaintainanceAndCalibrationScheduleDatesID = entity.InventoryMaintainanceAndCalibrationScheduleDatesID,
                    UpdatedDate = entity.UpdatedDate,
                    CalibratorName = entity.CalibratorName,
                    NextDate = entity.NextDate,
                    InventoryBasicItemDetailsID = entity.InventoryBasicItemDetailsID,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    IsActive = true,
                    InsertedDate = DateTime.Now
                });

                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public InventoryMaintainanceAndCalibrationEntity GetDatabyDateID(int ItemID,long DateID,long BasicDetailsID)
        {
            return(from p in _dbContext.InventoryMaintainanceAndCalibrations
                      where p.ItemID == ItemID && p.InventoryBasicDetailsID == BasicDetailsID && p.InventoryMaintainanceAndCalibrationScheduleDatesID == DateID
                      select new InventoryMaintainanceAndCalibrationEntity()
                      {
                          Auditor=p.Auditor,
                          AuditDate=p.AuditDate,
                          AuditObservations=p.AuditObservations,
                          ActionTaken = p.ActionTaken,
                      }).Distinct().FirstOrDefault();
           
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