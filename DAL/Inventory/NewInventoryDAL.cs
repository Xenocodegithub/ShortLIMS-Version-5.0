using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Inventory;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.DropDowns;
using System.Data.Entity.Core.Objects;

namespace LIMS_DEMO.DAL.Inventory
{
    public class NewInventoryDAL : IInventory
    {
        readonly LIMSEntities _dbContext;
        public NewInventoryDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public long UpdateMaintainanceDetails(InventoryMaintainanceAndCalibrationEntity imcEntity)
        {
            long MID = 0;
            try
            {
                if (imcEntity.ID != 0)
                {
                    var InventoryMaintainance = _dbContext.InventoryMaintainanceAndCalibrationSchedules.Find(imcEntity.ID);
                    InventoryMaintainance.ID = (short)imcEntity.ID;
                    InventoryMaintainance.AMCVendorName = imcEntity.AMCVendorName;
                    InventoryMaintainance.AMCNumber = imcEntity.AMCNumber;
                    InventoryMaintainance.AMCDate = imcEntity.AMCDate;
                    InventoryMaintainance.AMCValue = imcEntity.AMCValue;
                    InventoryMaintainance.AMCPeriod = (short)imcEntity.AMCPeriod;
                    InventoryMaintainance.Frequency = (short)imcEntity.Frequency;
                    InventoryMaintainance.Type = imcEntity.Type;
                    InventoryMaintainance.InsertedBy = 0;
                    InventoryMaintainance.AMCStartDate = imcEntity.StartDate;
                    InventoryMaintainance.InventoryBasicItemDetailsID = (int)imcEntity.InventoryBasicItemDetailsID;
                    _dbContext.SaveChanges();
                    MID = InventoryMaintainance.ID;
                }
                return MID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
   
        // inventory Category 
        public string AddCategory(Core.Inventory.CategoryEntity categoryEntity)
        {
            try
            {
                _dbContext.InventoryCategoryMasters.Add(new InventoryCategoryMaster()
                {
                    CategoryName = categoryEntity.CategoryName,
                    InventoryTypeID = categoryEntity.InventoryTypeID,
                    IsActive = categoryEntity.IsActive,
                    InsertedBy = categoryEntity.InsertedBy,
                    InsertedDate = categoryEntity.InsertedDate

                });
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public string EditCatagory(Core.Inventory.CategoryEntity categoryEntity)
        {
            try
            {
                if (categoryEntity.CatagoryMasterID != 0)
                {
                    var Inventorymaster = _dbContext.InventoryCategoryMasters.Find(categoryEntity.CatagoryMasterID);
                    Inventorymaster.InventoryTypeID = (short)categoryEntity.InventoryTypeID;
                    Inventorymaster.CategoryName = categoryEntity.CategoryName;
                    Inventorymaster.IsActive = true;
                    Inventorymaster.ModifiedBy = categoryEntity.InsertedBy;
                    Inventorymaster.ModifiedDate = categoryEntity.InsertedDate;
                }
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string DeleteCategory(Core.Inventory.CategoryEntity categoryEntity)
        {
            try
            {
                var categoryId = _dbContext.InventoryCategoryMasters.SingleOrDefault(x => x.ID == categoryEntity.CatagoryMasterID); //returns a single item.

                if (categoryId != null)
                {
                    _dbContext.InventoryCategoryMasters.Remove(categoryId);
                    _dbContext.SaveChanges();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        // Get Data from CategoryMaster Table Data
        public Core.Inventory.CategoryEntity Edit(int CatagoryMasterID)
        {
            return (from e in _dbContext.InventoryCategoryMasters
                    join cat in _dbContext.InventoryTypeMasters on e.InventoryTypeID equals cat.ID
                    where e.ID == CatagoryMasterID
                    select new Core.Inventory.CategoryEntity()
                    {
                        InventoryTypeID = e.InventoryTypeID,
                        CategoryName = e.CategoryName,
                        CatagoryMasterID = (short)e.ID,
                        InventoryTypeName = cat.Name,
                        IsActive = e.IsActive
                    }).FirstOrDefault();
        }

        // Get Category Details List Data (list)
        public List<Core.Inventory.CategoryEntity> GetInventoryCategoryList()
        {
            try
            {
                return (from e in _dbContext.InventoryCategoryMasters
                        join cat in _dbContext.InventoryTypeMasters on e.InventoryTypeID equals cat.ID
                        select new Core.Inventory.CategoryEntity()
                        {
                            CatagoryMasterID = (byte)e.ID,
                            CategoryName = e.CategoryName,
                            InventoryTypeID = e.InventoryTypeID,
                            InventoryTypeName = cat.Name,
                            IsActive = (bool)e.IsActive
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<InventoryItemEntity> SelectInventoryDetails(int ITID, int InventoryCategoryMasterID, int InventoryTypeMasterID, bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool InventoryItemMasterIsActive, bool InventoryItemMasterIsDeleted, bool IsActive, string Mode)
        {
            int ID = ITID;
            short InvcateId = (short)InventoryCategoryMasterID;
            short InvTypeId = (short)InventoryTypeMasterID;
            ObjectParameter objectParameter2 = new ObjectParameter("iErrorCode", typeof(int));
            List<InventoryItemEntity> inventoryiemlist = new List<InventoryItemEntity>();
            var Result = _dbContext.USP_InventoryItemMaster_Select(ID, InvcateId, InvTypeId, InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, objectParameter2, Mode).ToList();
            foreach (var item in Result)
            {
                inventoryiemlist.Add(new InventoryItemEntity()
                {
                    ItemMasterID = (byte)item.ID,
                    ItemID = item.ID,
                    CatagoryMasterID = (byte)item.CategoryID,
                    NameOfStock = item.Name,
                    Code = item.Code,
                    UnitID = item.UnitID,
                    MinimumStock = item.MinimumStock,
                    IsActive = (bool)item.IsActive,
                    InventoryTypeID = (short)item.InventoryTypeID,
                    AvailableStock = item.AvailableStock.ToString(),
                    CategoryName = item.CategoryName,
                    InventoryTypeName = item.Name,
                    Capacity = item.Capacity
                });
            }
            return inventoryiemlist;
        }

        // Inventory Item 
        public string AddInventoryItem(InventoryItemEntity inventoryItemEntity)
        {
            try
            {
                _dbContext.InventoryItemMasters.Add(new InventoryItemMaster()
                {
                    CategoryID = inventoryItemEntity.CatagoryMasterID,
                    InventoryTypeID = inventoryItemEntity.InventoryTypeID,
                    Name = inventoryItemEntity.NameOfStock,
                    Code = inventoryItemEntity.Code,
                    UnitID = inventoryItemEntity.UnitID,
                    Capacity = inventoryItemEntity.Capacity,
                    MinimumStock = inventoryItemEntity.MinimumStock,
                    IsActive = inventoryItemEntity.IsActive,
                    InsertedBy = inventoryItemEntity.InsertedBy,
                    InsertedDate = inventoryItemEntity.InsertedDate,
                    AvailableStock = inventoryItemEntity.AvailableStock
                });
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string EditInventoryItem(InventoryItemEntity inventoryItemEntity)
        {
            try
            {
                if (inventoryItemEntity.ItemID != 0)
                {
                    var InventoryItemmaster = _dbContext.InventoryItemMasters.Find(inventoryItemEntity.ItemID);

                    InventoryItemmaster.CategoryID = inventoryItemEntity.CatagoryMasterID;
                    InventoryItemmaster.InventoryTypeID = (short)inventoryItemEntity.InventoryTypeID;
                    InventoryItemmaster.Name = inventoryItemEntity.NameOfStock;
                    InventoryItemmaster.Code = inventoryItemEntity.Code;
                    InventoryItemmaster.UnitID = (short)inventoryItemEntity.UnitID;
                    InventoryItemmaster.Capacity = inventoryItemEntity.Capacity;
                    InventoryItemmaster.MinimumStock = inventoryItemEntity.MinimumStock;
                    InventoryItemmaster.IsActive = inventoryItemEntity.IsActive;
                    InventoryItemmaster.ModifiedBy = inventoryItemEntity.ModifiedBy;
                    InventoryItemmaster.ModifiedDate = inventoryItemEntity.ModifiedDate;
                    if (inventoryItemEntity.AvailableStock == null)
                    {
                        InventoryItemmaster.AvailableStock = inventoryItemEntity.AvailableStock;
                    }
                    else if (inventoryItemEntity.AvailableStock != null || inventoryItemEntity.AvailableStock != "")
                    {
                        InventoryItemmaster.AvailableStock = inventoryItemEntity.AvailableStock;
                    }
                }
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string DeleteItemCategory(Core.Inventory.InventoryItemEntity inventoryItemEntity)
        {
            try
            {
                var itemMasterId = _dbContext.InventoryItemMasters.SingleOrDefault(c => c.ID == inventoryItemEntity.ItemMasterID);
                if (itemMasterId != null)
                {
                    _dbContext.InventoryItemMasters.Remove(itemMasterId);
                    _dbContext.SaveChanges();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }

            //try
            //{

            //    InventoryItemMaster itemMaster = _dbContext.InventoryItemMasters.First(x => x.ID == inventoryItemEntity.ItemMasterID);

            //    object p = _dbContext.InventoryItemMasters.Remove(itemMaster);
            //    _dbContext.SaveChanges();
            //    return "success";

            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}

        }
        public Core.Inventory.InventoryItemEntity GetItemDetailsWithID(int ItemMasterId)
        {
            return (from e in _dbContext.InventoryItemMasters
                    where e.ID == ItemMasterId
                    select new Core.Inventory.InventoryItemEntity()
                    {
                        NameOfStock = e.Name,
                        ItemMasterID = e.ID,
                        IsActive = e.IsActive,
                        AvailableStock = e.AvailableStock,
                        MinimumStock = e.MinimumStock
                    }).FirstOrDefault();
        }
    
        public List<InventoryItemEntity> GetInventoryItemList(bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool InventoryItemMasterIsActive, bool InventoryItemMasterIsDeleted, bool IsActive, string Mode1)
        {
            List<InventoryItemEntity> InventoryItemList = new List<InventoryItemEntity>();
            int ID1 = 0;
            short InvCatID = 0;
            short InvTypeId = 0;
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));
            var Result = _dbContext.USP_InventoryItemMaster_Select(ID1, InvCatID, InvTypeId, InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, objectParameter, Mode1).ToList();
            foreach (var item in Result)
            {
                InventoryItemList.Add(new InventoryItemEntity()
                {
                    ItemMasterID = item.ID,
                    ItemID = item.ID,
                    CatagoryMasterID =(int)item.CategoryID,
                    NameOfStock = item.Name,
                    UnitID = item.UnitID,
                    Code = item.Code,
                    MinimumStock = item.MinimumStock,
                    IsActive = (bool)item.IsActive,
                    InventoryTypeID = (short)item.InventoryTypeID,
                    AvailableStock = item.AvailableStock,
                    CategoryName = item.CategoryName,
                    InventoryTypeName = item.TypeName,
                    Capacity = item.Capacity
                });
            }
            return InventoryItemList;
        }


        // Item Stock Issue
        public string InsertStockIssueData(InventoryItemEntity inventoryItemEntity)
        {
            try
            {
                long AVALSTOCK = Convert.ToInt64(inventoryItemEntity.AvailableStock);
                DateTime InsertedTime = DateTime.Now.ToLocalTime();
                ObjectParameter objectParameter1 = new ObjectParameter("iErrorMessage", typeof(string));
                var result = _dbContext.USP_StockLogData_Insert((long)inventoryItemEntity.IssueToNameID, inventoryItemEntity.IssueDate, inventoryItemEntity.IssueQty, (long)inventoryItemEntity.ItemMasterID, inventoryItemEntity.IsActive, inventoryItemEntity.InsertedBy, InsertedTime, (long)AVALSTOCK, objectParameter1);
                this._dbContext.SaveChanges();
                return "success"; // return 
            }
            catch (Exception ex)
            {
                return "success";
            }
        }
        public List<InventoryItemEntity> GetStockItemDataList(bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool InventoryItemMasterIsActive, bool InventoryItemMasterIsDeleted, bool IsActive, string Mode)
        {
            List<InventoryItemEntity> InventoryItemList = new List<InventoryItemEntity>();
            try
            {
                return (from e in _dbContext.InventoryItemMasters
                        join s in _dbContext.StockLogDatas on e.ID equals s.ItemID
                        join u in _dbContext.UserDetails on s.IssueToNameID equals u.UserDetailID
                        join c in _dbContext.InventoryCategoryMasters on e.CategoryID equals c.ID
                        join i in _dbContext.InventoryTypeMasters on e.InventoryTypeID equals i.ID
                        where s.IssueQty != 0
                        select new Core.Inventory.InventoryItemEntity()
                        {

                            EmpName = u.FirstName + " " + u.LastName,
                            IssueQty = (int)s.IssueQty,
                            IssueDate = s.IssueDate,
                            CatagoryMasterID = (byte)c.ID,
                            CategoryName = c.CategoryName,
                            InventoryTypeID = i.ID,
                            InventoryTypeName = i.Name,
                            IsActive = (bool)e.IsActive,
                            NameOfStock = e.Name,
                            ItemMasterID = e.ID,
                            EmpId = u.UserDetailID

                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        // Employee list
        public List<Core.Inventory.EmployeeEntity> GetEmployeeList()
        {
            try
            {
                return (from e in _dbContext.UserDetails

                        select new EmployeeEntity()
                        {
                            EmpId = e.UserDetailID,
                            EmpName = e.FirstName + " " + e.LastName,
                            IsActive = e.IsActive,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        // Capacity

        public string InsertCapacityMaster(CapacityEntity capacityEntity)
        {
            try
            {
                _dbContext.InventoryCapacityMasters.Add(new InventoryCapacityMaster()
                {
                    Capacity = capacityEntity.Capacity,
                    Description = capacityEntity.Description,
                    IsActive = capacityEntity.IsActive,
                    InsertedBy = capacityEntity.InsertedBy,
                    InsertedDate = capacityEntity.InsertedDate
                });
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public List<Core.Inventory.CapacityEntity> GetCapacityList()
        {
            try
            {
                return (from e in _dbContext.InventoryCapacityMasters

                        select new CapacityEntity()
                        {
                            Capacity = e.Capacity,
                            IsActive = e.IsActive,
                            InventoryCapacityMasterId = e.InventoryCapacityMasterId,
                            Description = e.Description
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Core.Inventory.CapacityEntity SelectCapacityDataWithID(int InventoryCapacityMasterId)
        {
            return (from e in _dbContext.InventoryCapacityMasters
                    where e.InventoryCapacityMasterId == InventoryCapacityMasterId
                    select new Core.Inventory.CapacityEntity()
                    {
                        Capacity = e.Capacity,
                        InventoryCapacityMasterId = e.InventoryCapacityMasterId,
                        Description = e.Description,
                        IsActive = e.IsActive
                    }).FirstOrDefault();
        }

        public string UpdateCapacityData(CapacityEntity capacityEntity)
        {
            try
            {
                if (capacityEntity.InventoryCapacityMasterId != 0)
                {
                    var invetoryCapacityMaster = _dbContext.InventoryCapacityMasters.Find(capacityEntity.InventoryCapacityMasterId);
                    invetoryCapacityMaster.InventoryCapacityMasterId = capacityEntity.InventoryCapacityMasterId;
                    invetoryCapacityMaster.Capacity = capacityEntity.Capacity;
                    invetoryCapacityMaster.Description = capacityEntity.Description;
                    invetoryCapacityMaster.IsActive = capacityEntity.IsActive;
                    invetoryCapacityMaster.ModifiedBy = capacityEntity.ModifiedBy;
                    invetoryCapacityMaster.ModifiedDate = capacityEntity.ModifiedDate;
                }
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string DeleteInvCapacity(InventoryCapacityEntity capacityEntity)
        {
            try
            {
                var capacityId = _dbContext.InventoryCapacityMasters.SingleOrDefault(x => x.InventoryCapacityMasterId == capacityEntity.InventoryCapacityMasterId); //returns a single item.

                if (capacityId != null)
                {
                    _dbContext.InventoryCapacityMasters.Remove(capacityId);
                    _dbContext.SaveChanges();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        // consumable List

        public List<InventoryBasicDetailEntity> InventoryBasicDetailsList(int TypeId)
        {
            try
            {
                return (from ibd in _dbContext.InventoryBasicDetails
                        join iim in _dbContext.InventoryItemMasters on ibd.ItemID equals iim.ID
                        //join ibid in _dbContext.InventoryBasicItemDetails on ibd.ID equals ibid.InventoryBasicDetailsID
                        //join it in _dbContext.InventoryTypeMasters on ibd.ID equals it.ID
                        join icm in _dbContext.InventoryCategoryMasters on ibd.CategoryID equals icm.ID
                        join um in _dbContext.UnitMasters on ibd.UnitID equals um.UnitId
                        where ibd.TypeID == TypeId
                        select new InventoryBasicDetailEntity()
                        {
                            ID = ibd.ID,
                            ItemID = ibd.ItemID,
                            TypeID = (short)ibd.TypeID,
                            CategoryID = (short)ibd.CategoryID,
                            UnitID = (short)ibd.UnitID,
                            QuantityType = ibd.QuantityType,
                            Quantity = ibd.Quantity,
                            Warranty = (short)ibd.Warranty,
                            IsActive = ibd.IsActive,
                            PurchaseRequestID = ibd.PurchaseRequestID,
                            TotalQuantity = ibd.TotalQuantity,
                            InventoryBasicDetailsNumber = ibd.InventoryBasicDetailsNumber,
                            ItemName = iim.Name,
                            CategoryName = icm.CategoryName,
                            Unit = um.Unit,
                            InventoryBasicDetailsID = ibd.ID
                        }).ToList();

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<InventoryItemEntity> GetInventoryItemList(bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool InventoryItemMasterIsActive, bool InventoryItemMasterIsDeleted, bool IsActive, int InventoryTypeMasterID1, string Mode1)
        {
            List<InventoryItemEntity> InventoryItemList = new List<InventoryItemEntity>();
            int ID1 = 0;
            short InvCatID = 0;
            short InvTypeId = (short)InventoryTypeMasterID1;
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));
            var Result = _dbContext.USP_InventoryItemMaster_Select(ID1, InvCatID, InvTypeId, InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryItemMasterIsActive, InventoryItemMasterIsDeleted, objectParameter, Mode1).ToList();
            foreach (var item in Result)
            {
                InventoryItemList.Add(new InventoryItemEntity()
                {
                    ID = item.ID,
                    ItemMasterID = (byte)item.ID,
                    ItemID = item.ID,
                    CatagoryMasterID = (byte)item.CategoryID,
                    NameOfStock = item.Name,
                    Name = item.Name,
                    Code = item.Code,
                    UnitID = item.UnitID,
                    MinimumStock = item.MinimumStock,
                    IsActive = (bool)item.IsActive,
                    InventoryTypeID = (short)item.InventoryTypeID,
                    AvailableStock = item.AvailableStock,
                    CategoryName = item.CategoryName,
                    InventoryTypeName = item.TypeName,
                    Capacity = item.Capacity
                   
                });
            }
            return InventoryItemList;
        }

        public InventoryBasicDetailEntity GetInventoryItemCategoryDetail(int PurchaseRequestID)
        {
            return (from prd in _dbContext.PurchaseRequestDetails
                    join psd in _dbContext.PurchaseSupplierDetails on prd.PurchaseSupplierID equals psd.PurchaseSupplierID
                    join e in _dbContext.InventoryItemMasters on prd.ItemMasterID equals e.ID
                    join pc in _dbContext.InventoryCategoryMasters on e.CategoryID equals pc.ID
                    join py in _dbContext.InventoryTypeMasters on prd.InventoryTypeID equals py.ID
                    join xyz in _dbContext.UnitMasterInventories on e.UnitID equals xyz.UnitId
                    where prd.PurchaseRequestID == PurchaseRequestID
                    select new InventoryBasicDetailEntity()
                    {
                        PurchaseRequestID = prd.PurchaseRequestID,
                        Brand = prd.Brand,
                        Grade = prd.Grade,
                        PackSize = prd.PackSize,
                        Specification = prd.Specification,
                        SupplierName = psd.SupplierName,
                        PurchaseOrderNo = prd.PONumber,
                        PurchaseOrderDate = prd.EnteredDate,
                        QuantityType = prd.Quantity.ToString(),

                        TypeID = py.ID,
                        InventoryTypeID=py.ID,
                        InventoryTypeName = py.Name,
                        ItemID = e.ID,
                        ItemName = e.Name,
                        CategoryID = pc.ID,
                        CatagoryMasterID=pc.ID,
                        CategoryName = pc.CategoryName,
                        UnitID = xyz.UnitId,
                        Unit = xyz.Unit
                    }).FirstOrDefault();
        }
        // Date: 24/02/2021
        public decimal GetInvAvailableStockbyItemId(int ItemId)
        {
            decimal AvailableStock = 0;
            try
            {
                var InventoryItemMaster = _dbContext.InventoryItemMasters.Find(ItemId);
                {
                    AvailableStock = Convert.ToDecimal(InventoryItemMaster.AvailableStock);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return AvailableStock;
        }
        public string UpdateInvAvailableStock(int ItemId, decimal UpdatedAvailableStock)
        {
            try
            {

                if (ItemId > 0)
                {
                    var InventoryItemMaster = _dbContext.InventoryItemMasters.Find(ItemId);
                    {
                        InventoryItemMaster.AvailableStock = UpdatedAvailableStock.ToString();
                    }
                    _dbContext.SaveChanges();

                }
                return "success";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Generate inventory Number 
        public string GenerateInvBasicNumber(int pid)
        {
            string UpdateBasicDetailsNumber = null;
            var purchase = _dbContext.PurchaseRequestDetails.Find(pid);
            string SerialNumberType = "InventoryBasicDetails";
            bool IsActive = true;
            long SerialNoCount = GetSerialNumber(SerialNumberType, IsActive);
            if (SerialNoCount != 0)
            {
                SerialNoCount = SerialNoCount + 1;
                UpdateSerialNo(SerialNumberType, IsActive, SerialNoCount);
                string date = DateTime.Now.ToShortDateString();
                UpdateBasicDetailsNumber = "MHB / INV /" + date + "/" + SerialNoCount;
            }
            else
            {
                SerialNoCount = 1;
                AddSerialNo(SerialNoCount);
            }
            return UpdateBasicDetailsNumber;
        }
        public long GetSerialNumber(string SrNameType, bool IsActiveField)
        {
            var result = (from e in _dbContext.SerialNumberMasters
                          where e.NumberType == SrNameType && e.IsActive == IsActiveField
                          select new BasicNumberEntity()
                          {
                              NumberValue = e.NumberValue,
                          }
                 ).FirstOrDefault();

            if (result != null)
            {
                long numvalue = (long)result.NumberValue;
                return numvalue;
            }
            else { return 0; }
        }
        // insert code here 
        public long AddSerialNo(long senovalue)
        {
            try
            {
                var insertserialno = new SerialNumberMaster()
                {

                    FinancialYearID = 0,
                    FinancialMonthID = 0,
                    NumberType = "InventoryBasicDetails",
                    NumberValue = senovalue,
                    IsActive = true
                };
                _dbContext.SerialNumberMasters.Add(insertserialno);
                _dbContext.SaveChanges();
                return insertserialno.ID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        //update code
        public string UpdateSerialNo(string SrNameType, bool IsActiveField, long SerialNoUpdate)
        {
            try
            {
                var serilano = (from e in _dbContext.SerialNumberMasters
                                where SrNameType == e.NumberType && IsActiveField == e.IsActive
                                select new BasicNumberEntity()
                                {
                                    SRNOID = e.ID,
                                }
                            ).FirstOrDefault();
                var serialNumberMaster = _dbContext.SerialNumberMasters.Find(serilano.SRNOID);
                serialNumberMaster.NumberValue = SerialNoUpdate;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        // insert inventory basic data -> 24/2/2021
        public long AddInventoryBasicItemDetails(InventoryBasicDetailEntity inventoryBasicDetailEntity)
        {
            try
            {
                var result = new InventoryBasicItemDetail()
                {
                    InventoryBasicDetailsID = inventoryBasicDetailEntity.InventoryBasicDetailsID,
                    BatchNumber = inventoryBasicDetailEntity.BatchNumber,
                    ModelNumber = inventoryBasicDetailEntity.ModelNumber,
                    BarcodeNumber = inventoryBasicDetailEntity.BarcodeNumber,
                    Quantity = inventoryBasicDetailEntity.Quantity,
                    Description = inventoryBasicDetailEntity.Description,
                    IsActive = inventoryBasicDetailEntity.IsActive,
                    InventoryBasicItemDetailsNumber = inventoryBasicDetailEntity.InventoryBasicItemDetailsNumber,
                    ManufacturerName = inventoryBasicDetailEntity.Manufacturer,
                    DOM = inventoryBasicDetailEntity.DOM,
                    DOE = inventoryBasicDetailEntity.DOE,
                    LOTNo = inventoryBasicDetailEntity.LOTNo,
                    SRNO = inventoryBasicDetailEntity.SRNO
                };
                _dbContext.InventoryBasicItemDetails.Add(result);
                _dbContext.SaveChanges();
                return result.ID; // return 
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        // Add CommercialDetails Data
        public long InsertInventoryCommercialData(InventoryCommercialDetailEntity inventoryCommercialDetailEntity)
        {
            try
            {
                var result = new InventoryCommercialDetail()
                {
                    VendorName = inventoryCommercialDetailEntity.VendorName,
                    InventoryBasicDetailsID = inventoryCommercialDetailEntity.InventoryBasicDetailsID,
                    PurchaseOrderNumber = inventoryCommercialDetailEntity.PurchaseOrderNumber,
                    PurchaseOrderValue = inventoryCommercialDetailEntity.PurchaseOrderValue,
                    PurchaseDate = inventoryCommercialDetailEntity.PurchaseDate,
                    DeliveryTime = inventoryCommercialDetailEntity.DeliveryTime,
                    DeliveryChallanNo = inventoryCommercialDetailEntity.DeliveryChallanNo,
                    DeliveryChallanDate = inventoryCommercialDetailEntity.DeliveryChallanDate,
                    InvoiceNumber = inventoryCommercialDetailEntity.InvoiceNumber,
                    BillDate = inventoryCommercialDetailEntity.BillDate,
                    IsActive = inventoryCommercialDetailEntity.IsActive
                };
                _dbContext.InventoryCommercialDetails.Add(result);
                _dbContext.SaveChanges();
                return result.ID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public InventoryBasicDetailEntity GetInvBasicNumber(int invBId, bool IsActiveField)
        {
            return (from e in _dbContext.InventoryBasicDetails
                    where e.ID == invBId
                    select new InventoryBasicDetailEntity
                    {
                        InventoryBasicDetailsNumber = e.InventoryBasicDetailsNumber,
                    }
           ).FirstOrDefault();
        }
        // INSERT BASIC DETAILS DATA
        public long InsertInventoryBasicData(InventoryBasicDetailEntity inventoryBasicDetailEntity)
        {
            //  long InvBasicNumber;
            try
            {
                var result = new InventoryBasicDetail()
                {
                    ItemID = inventoryBasicDetailEntity.ItemID,
                    TypeID = inventoryBasicDetailEntity.TypeID,
                    PurchaseRequestID = inventoryBasicDetailEntity.PurchaseRequestID,
                    CategoryID = inventoryBasicDetailEntity.CategoryID,
                    UnitID = inventoryBasicDetailEntity.UnitID,
                    QuantityType = inventoryBasicDetailEntity.QuantityType,
                    Quantity = inventoryBasicDetailEntity.Quantity,
                    BrandReceived = inventoryBasicDetailEntity.BrandReceived,
                    GradeReceived = inventoryBasicDetailEntity.GradeReceived,
                    Warranty = inventoryBasicDetailEntity.Warranty,
                    ConditionOfPackaging = inventoryBasicDetailEntity.ConditionOfPackaging,
                    IntegrityOfPackaging = inventoryBasicDetailEntity.IntegrityOfPackaging,
                    Manufacturer = inventoryBasicDetailEntity.Manufacturer,
                    DOM = inventoryBasicDetailEntity.DOM,
                    IsActive = inventoryBasicDetailEntity.IsActive,
                    CertifiedConcentration = inventoryBasicDetailEntity.CertifiedConcentration,
                    Praceability = inventoryBasicDetailEntity.Praceability,
                    DOExpiry = inventoryBasicDetailEntity.DOE,
                    Remark = inventoryBasicDetailEntity.Remark,
                    StorageLoccation = inventoryBasicDetailEntity.StorageLocation,
                    InsertedBy = inventoryBasicDetailEntity.InsertedBy,
                    InsertedDate = inventoryBasicDetailEntity.InsertedDate,
                    InventoryBasicDetailsNumber = inventoryBasicDetailEntity.InventoryBasicDetailsNumber
                };
                _dbContext.InventoryBasicDetails.Add(result);
                _dbContext.SaveChanges();
                return result.ID;
                // for return
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
            // Date: 24/02/2021  BY ASHWINI
        public long UpdateInventoryBasicData(InventoryBasicDetailEntity inventoryBasicDetailEntity)
        {           
            long invbasicid = 0;
            decimal AvailableStock = 0;
            decimal Updateqty1 = 0;
            decimal qty = 0;
            decimal qtyleft = 0;
            decimal Totalqty = 0;

            try
            {
                if (inventoryBasicDetailEntity.ID > 0)
                {
                    var InventoryBasicDetailmaster = _dbContext.InventoryBasicDetails.Find(inventoryBasicDetailEntity.ID);
                    {
                        InventoryBasicDetailmaster.ID = inventoryBasicDetailEntity.ID;
                        InventoryBasicDetailmaster.ItemID = inventoryBasicDetailEntity.ItemID;
                        InventoryBasicDetailmaster.TypeID = inventoryBasicDetailEntity.TypeID;
                        InventoryBasicDetailmaster.CategoryID = inventoryBasicDetailEntity.CategoryID;
                        InventoryBasicDetailmaster.UnitID = inventoryBasicDetailEntity.UnitID;
                        InventoryBasicDetailmaster.PurchaseRequestID = inventoryBasicDetailEntity.PurchaseRequestID;
                        InventoryBasicDetailmaster.QuantityType = inventoryBasicDetailEntity.QuantityType;
                        InventoryBasicDetailmaster.Quantity = inventoryBasicDetailEntity.Quantity;
                        inventoryBasicDetailEntity.QuantityLeft = inventoryBasicDetailEntity.QuantityLeft;
                        InventoryBasicDetailmaster.BrandReceived = inventoryBasicDetailEntity.BrandReceived;
                        InventoryBasicDetailmaster.GradeReceived = inventoryBasicDetailEntity.GradeReceived;
                        InventoryBasicDetailmaster.ConditionOfPackaging = inventoryBasicDetailEntity.ConditionOfPackaging;
                        InventoryBasicDetailmaster.IntegrityOfPackaging = inventoryBasicDetailEntity.IntegrityOfPackaging;
                        InventoryBasicDetailmaster.Warranty = inventoryBasicDetailEntity.Warranty;
                        InventoryBasicDetailmaster.Manufacturer = inventoryBasicDetailEntity.Manufacturer;
                        InventoryBasicDetailmaster.DOM = inventoryBasicDetailEntity.DOM;
                        InventoryBasicDetailmaster.CertifiedConcentration = inventoryBasicDetailEntity.CertifiedConcentration;
                        InventoryBasicDetailmaster.Praceability = inventoryBasicDetailEntity.Praceability;
                        InventoryBasicDetailmaster.DOExpiry = inventoryBasicDetailEntity.DOE;
                        InventoryBasicDetailmaster.IsActive = inventoryBasicDetailEntity.IsActive;
                        InventoryBasicDetailmaster.Remark = inventoryBasicDetailEntity.Remark;
                        InventoryBasicDetailmaster.StorageLoccation = inventoryBasicDetailEntity.StorageLocation;
                        InventoryBasicDetailmaster.UpdatedBy = inventoryBasicDetailEntity.UpdatedBy;
                        InventoryBasicDetailmaster.UpdatedDate = DateTime.Now.ToLocalTime();
                        InventoryBasicDetailmaster.TotalQuantity = inventoryBasicDetailEntity.TotalQuantity;
                        InventoryBasicDetailmaster.DeliveryTime = inventoryBasicDetailEntity.DeliveryTime;
                        InventoryBasicDetailmaster.InventoryBasicDetailsNumber = inventoryBasicDetailEntity.InventoryBasicDetailsNumber;
                    }
                    _dbContext.SaveChanges();
                    var ItemMaster = _dbContext.InventoryItemMasters.Find(inventoryBasicDetailEntity.ItemID);
                    {
                        AvailableStock = Convert.ToDecimal(ItemMaster.AvailableStock);
                    }
                    qty =(decimal) inventoryBasicDetailEntity.Quantity;
                    qtyleft = (decimal) inventoryBasicDetailEntity.QuantityLeft;
                    Updateqty1 = qty - qtyleft;

                    Totalqty = AvailableStock + Updateqty1;
                    var ItemMaster1= _dbContext.InventoryItemMasters.Find(inventoryBasicDetailEntity.ItemID);
                    {
                        ItemMaster1.AvailableStock = Totalqty.ToString();
                    }
                    _dbContext.SaveChanges();

                    //              SET @_Quantity = (SELECT AvailableStock FROM InventoryItemMaster WHERE ID = @iItemID);

                    //              set @iQuantity = (@iQuantity - @iTotalQuantity);
                    //              SET @_Quantity = (@_Quantity + @iQuantity);
                    //              UPDATE InventoryItemMaster SET AvailableStock = @_Quantity WHERE ID = @iItemID;

                    //              UPDATE[dbo].[InventoryStockDatewise]

                    //         SET[ItemID] = @iItemID
                    //,[StockDate] = GETDATE()
                    //,[Quantity] = @_Quantity
                    //,[TransectionType] = 'IN'
                    //,[IsActive] = 1
                    //,[SourceID] = @iID
                    // ,[FormName] = 'InventoryBasicDetails'

                    //       WHERE ID = @iID
                    invbasicid = InventoryBasicDetailmaster.ID;
                }
                return invbasicid;
            }
            catch (Exception ex)
            {
                return 0;
            }
            //try
            //{
            //    ObjectParameter objectParameter1 = new ObjectParameter("iErrorCode", typeof(int));
            //    int ID = inventoryBasicDetailEntity.ID;
            //    int ItemID = (Int32)inventoryBasicDetailEntity.ItemID;
            //    int TypeID = (Int32)inventoryBasicDetailEntity.TypeID;
            //    int CategoryID = (Int32)inventoryBasicDetailEntity.CategoryID;
            //    int UnitID = (Int32)inventoryBasicDetailEntity.UnitID;
            //    int PurchaseRequestID = (Int32)inventoryBasicDetailEntity.PurchaseRequestID;
            //    string PackSize = inventoryBasicDetailEntity.PackSize;
            //    string QuantityType = inventoryBasicDetailEntity.QuantityType;
            //    decimal Quantity = (decimal)inventoryBasicDetailEntity.Quantity;
            //    decimal QuantityLeft = (decimal)inventoryBasicDetailEntity.QuantityLeft;
            //    string Brand = inventoryBasicDetailEntity.Brand;
            //    string BrandReceived = inventoryBasicDetailEntity.BrandReceived;
            //    string Grade = inventoryBasicDetailEntity.Grade;
            //    string GradeReceived = inventoryBasicDetailEntity.GradeReceived;
            //    string ConditionOfPackaging = inventoryBasicDetailEntity.ConditionOfPackaging;
            //    string IntegrityOfPackaging = inventoryBasicDetailEntity.IntegrityOfPackaging;
            //    int Warranty = (Int32)inventoryBasicDetailEntity.Warranty;
            //    string Manufacturer = inventoryBasicDetailEntity.Manufacturer;
            //    DateTime DOM = (DateTime)inventoryBasicDetailEntity.DOM;
            //    string CertifiedConcentration = inventoryBasicDetailEntity.CertifiedConcentration;
            //    string Praceability = inventoryBasicDetailEntity.Praceability;
            //    DateTime DOE = (DateTime)inventoryBasicDetailEntity.DOE;
            //    bool IsActive = (bool)inventoryBasicDetailEntity.IsActive;
            //    string Remark = inventoryBasicDetailEntity.Remark;
            //    string StorgaeLocation = inventoryBasicDetailEntity.StorageLocation;
            //    string InventoryBasicDetailsNumber = inventoryBasicDetailEntity.InventoryBasicDetailsNumber;
            //    int UpdatedBy = (Int32)inventoryBasicDetailEntity.UpdatedBy;
            //    DateTime UpdateDate =(DateTime)inventoryBasicDetailEntity.UpdatedDate;
            //    int TotalQuantity = (Int32)inventoryBasicDetailEntity.TotalQuantity;
            //    string DeliveryTime = null;
            //    var Result1 = _dbContext.USP_InventoryBasicDetails_Update(ID, ItemID, TypeID, CategoryID, UnitID, QuantityType, Quantity, Warranty, objectParameter1, UpdatedBy, TotalQuantity, Manufacturer, DOM, DOE, GradeReceived, DeliveryTime, ConditionOfPackaging, IntegrityOfPackaging, CertifiedConcentration, Praceability, Remark, BrandReceived, StorgaeLocation);
            //    this._dbContext.SaveChanges();
            //    return "success";// return 
            //}
            //catch (Exception ex)
            //{
            //    return "success";
            //}
        }

        public string DeleteInventoryBasicItemDetail(InventoryBasicDetailEntity ibdEntity)
        {
            try
            {
                ObjectParameter objectParameter2 = new ObjectParameter("iErrorMessage", typeof(int));
                ObjectParameter objectParameter1 = new ObjectParameter("iErrorCode", typeof(int));
                _dbContext.USP_InventoryBasicItemDetails_Delete(ibdEntity.ID, ibdEntity.InventoryBasicDetailsID, objectParameter1, ibdEntity.UpdatedBy, objectParameter2);
                this._dbContext.SaveChanges();
                //return Result1.ToString(); // return 
                return "success";
            }
            catch (Exception ex)
            {
                return "success";
            }
        }
        public long UpdateInventoryCommercialData(InventoryCommercialDetailEntity inventoryCommercialDetailEntity)
        {
            long invcommid = 0;
            try
            {
                if (inventoryCommercialDetailEntity.ID > 0)
                {
                    var InventoryCommercialDetailmaster = _dbContext.InventoryCommercialDetails.Find(inventoryCommercialDetailEntity.ID);
                    {
                        InventoryCommercialDetailmaster.VendorName = inventoryCommercialDetailEntity.VendorName;
                        InventoryCommercialDetailmaster.InventoryBasicDetailsID = inventoryCommercialDetailEntity.InventoryBasicDetailsID;
                        InventoryCommercialDetailmaster.PurchaseOrderNumber = inventoryCommercialDetailEntity.PurchaseOrderNumber;
                        //PurchaseOrderDate = inventoryCommercialDetailEntity.PurchaseOrderDate,
                        InventoryCommercialDetailmaster.PurchaseOrderValue = inventoryCommercialDetailEntity.PurchaseOrderValue;
                        InventoryCommercialDetailmaster.PurchaseDate = inventoryCommercialDetailEntity.PurchaseDate;
                        InventoryCommercialDetailmaster.DeliveryTime = inventoryCommercialDetailEntity.DeliveryTime;
                        InventoryCommercialDetailmaster.DeliveryChallanNo = inventoryCommercialDetailEntity.DeliveryChallanNo;
                        InventoryCommercialDetailmaster.DeliveryChallanDate = inventoryCommercialDetailEntity.DeliveryChallanDate;
                        InventoryCommercialDetailmaster.InvoiceNumber = inventoryCommercialDetailEntity.InvoiceNumber;
                        InventoryCommercialDetailmaster.BillDate = inventoryCommercialDetailEntity.BillDate;
                        InventoryCommercialDetailmaster.IsActive = inventoryCommercialDetailEntity.IsActive;
                        InventoryCommercialDetailmaster.ID = inventoryCommercialDetailEntity.ID;

                    }
                    _dbContext.SaveChanges();
                    invcommid = InventoryCommercialDetailmaster.ID;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return invcommid;
        }
        public List<InventoryBasicDetailEntity> GetInventoryBasicDetailBySearch(int ID1, string Mode, string SortBy, string Search, string SortOrder, int StartRow, int EndRow, int TypeID)
        {
            List<InventoryBasicDetailEntity> InventoryBasicItemList = new List<InventoryBasicDetailEntity>();
            ObjectParameter objectParameter = new ObjectParameter("iRowCounter", typeof(int));
            var Result = _dbContext.USP_InventoryBasicDetails_Select(Mode, objectParameter, SortBy, Search, SortOrder, StartRow, EndRow, ID1, TypeID).ToList();
            foreach (var item in Result)
            {
                InventoryBasicItemList.Add(new InventoryBasicDetailEntity()
                {
                    ID = item.ID,
                    TypeID = item.TypeID,
                    CategoryID = item.CategoryID,
                    UnitID = item.UnitID,
                    QuantityType = item.QuantityType,
                    Quantity = item.Quantity,
                    QuantityLeft = (decimal)item.Quantity,
                    TQuantity = (decimal)item.Quantity,
                    Warranty = item.Warranty,
                    IsActive = item.IsActive,
                    InsertedBy = item.InsertedBy,
                    InsertedDate = item.InsertedDate,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate,
                    ItemID = item.ItemID,
                    TotalQuantity = item.Quantity,
                    InventoryBasicDetailsNumber = item.InventoryBasicDetailsNumber,
                    Manufacturer = item.Manufacturer,
                    DOM = item.DOM,
                    DOE = item.DOExpiry,
                    GradeReceived = item.GradeReceived,
                    DeliveryTime = item.DeliveryTime,
                    ConditionOfPackaging = item.ConditionOfPackaging,
                    IntegrityOfPackaging = item.IntegrityOfPackaging,
                    CertifiedConcentration = item.CertifiedConcentration,
                    Praceability = item.Praceability,
                    Remark = item.Remark,
                    StorageLocation=item.StorageLoccation,
                    BrandReceived = item.BrandReceived,
                    PurchaseRequestID = item.PurchaseRequestID
                });
            }
            return InventoryBasicItemList;
        }
        public List<InventoryBasicItemDetailEntity> GetInventoryBasicItemDetailBySearch1(int ID, long InventoryBasicDetailsID1, bool InventoryBasicDetailsIsActive, bool InventoryBasicItemDetailsIsActive, string mode, int ItemID1, int TypeID, int CategoryId)
        {
            List<InventoryBasicItemDetailEntity> InventoryBasicItem = new List<InventoryBasicItemDetailEntity>();
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));
            var Result = _dbContext.USP_InventoryBasicItemDetails_Select(mode, objectParameter, InventoryBasicDetailsIsActive, InventoryBasicItemDetailsIsActive, (Int32)InventoryBasicDetailsID1, ItemID1, TypeID, CategoryId, ID).ToList();
            foreach (var item in Result)
            {
                InventoryBasicItem.Add(new InventoryBasicItemDetailEntity()
                {
                    ID = item.ID,
                    InventoryBasicDetailsID = (Int32)item.InventoryBasicDetailsID,
                    BatchNumber = item.BatchNumber,
                    ModelNumber = item.ModelNumber,
                    BarcodeNumber = item.BarcodeNumber,
                    LOTNo = item.LOTNo,
                    SRNO = item.SRNO,
                    Quantity = item.Quantity,
                    Description = item.Description,
                    IsActive = item.IsActive,
                    InsertedBy = item.InsertedBy,
                    InsertedDate = item.InsertedDate,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate,
                    InventoryBasicItemDetailsNumber = item.InventoryBasicItemDetailsNumber,
                });
            }
            return InventoryBasicItem;
        }
        public List<InventoryCommercialDetailEntity> GetInventoryCommercialDetailsBySearch(int InventoryBasicDetailsID1, string Mode2)
        {
            List<InventoryCommercialDetailEntity> InventoryCommercialItemList = new List<InventoryCommercialDetailEntity>();
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));
            var Result = _dbContext.USP_InventoryCommercialDetails_Select(Mode2, objectParameter, InventoryBasicDetailsID1).ToList();
            foreach (var item in Result)
            {
                InventoryCommercialItemList.Add(new InventoryCommercialDetailEntity()
                {
                    ID = item.ID,
                    InventoryBasicDetailsID = item.InventoryBasicDetailsID,
                    VendorName = item.VendorName,
                    PurchaseOrderNumber = item.PurchaseOrderNumber,
                    PurchaseOrderValue = item.PurchaseOrderValue,
                    PurchaseOrderDate = item.PurchaseDate,
                    PurchaseDate = item.PurchaseDate,
                    InvoiceNumber = item.InvoiceNumber,
                    IsActive = item.IsActive,
                    InsertedBy = item.InsertedBy,
                    InsertedDate = item.InsertedDate,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate,
                    DeliveryChallanNo = item.DeliveryChallanNo,
                    DeliveryChallanDate = item.DeliveryChallanDate,
                    BillDate = item.BillDate,
                    DeliveryTime = item.DeliveryTime
                });
            }
            return InventoryCommercialItemList;

        }
        public List<InventoryCommercialFileDetailEntity> GetInventoryCommercialFileDetailBySearch(long InventoryBasicDetailID2, string Mode3)
        {
            List<InventoryCommercialFileDetailEntity> InventoryCommercialItemList = new List<InventoryCommercialFileDetailEntity>();
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));
            var Result = _dbContext.USP_InventoryCommercialFileDetail_Select(Mode3, objectParameter, InventoryBasicDetailID2).ToList();
            foreach (var item in Result)
            {
                InventoryCommercialItemList.Add(new InventoryCommercialFileDetailEntity()
                {
                    ID = item.ID,
                    InventoryBasicDetailID = item.InventoryBasicDetailID,
                    FileName = item.FileName,
                    IsActive = item.IsActive
                });
            }
            return InventoryCommercialItemList;
        }
        public List<InventoryBasicDetailEntity> GetInventoryBasicItemDetails(int ID)
        {
            return (from IT in _dbContext.InventoryBasicItemDetails
                    join IB in _dbContext.InventoryBasicDetails on IT.InventoryBasicDetailsID equals IB.ID
                    where IT.IsActive == true && IB.IsActive == true && IB.ID == ID
                    select new InventoryBasicDetailEntity
                    {

                        InventoryBasicItemDetailsID = (int)IT.ID,
                        ID = (int)IT.ID,
                        InventoryBasicItemDetailsNumber = IT.InventoryBasicItemDetailsNumber
                    }
               ).ToList();
        }
        public List<InventoryBasicDetailEntity> GetInventoryBasicDetailBySearch2(int ID1, int TypeID1, string SortBy1, string SortOrder1, int StartRow1, int EndRow1, string SearchKeywords1, string Mode1)
        {
            List<InventoryBasicDetailEntity> InventoryBasicList = new List<InventoryBasicDetailEntity>();
            ObjectParameter objectParameter = new ObjectParameter("iRowCounter", typeof(int));
            var Result = _dbContext.USP_InventoryBasicDetails_Select(Mode1, objectParameter, SortBy1, SearchKeywords1, SortOrder1, StartRow1, EndRow1, ID1, TypeID1).ToList();
            foreach (var item in Result)
            {
                InventoryBasicList.Add(new InventoryBasicDetailEntity()
                {
                    ID = item.ID,
                    ItemID = item.ItemID,
                    TypeID = item.TypeID,
                    CategoryID = item.CategoryID,
                    UnitID = item.UnitID,
                    QuantityType = item.QuantityType,
                    Quantity = item.Quantity,
                    Warranty = item.Warranty,
                    IsActive = item.IsActive,
                    TotalQuantity = item.TotalQuantity,
                    InventoryBasicDetailsNumber = item.InventoryBasicDetailsNumber,
                    GradeReceived = item.GradeReceived,
                    DeliveryTime = item.DeliveryTime,
                    Manufacturer = item.Manufacturer,
                    ConditionOfPackaging = item.ConditionOfPackaging,
                    IntegrityOfPackaging = item.IntegrityOfPackaging,
                    CertifiedConcentration = item.CertifiedConcentration,
                    Praceability = item.Praceability,
                    Remark = item.Remark,
                    BrandReceived = item.BrandReceived,
                    PurchaseRequestID = item.PurchaseRequestID
                });
            }
            return InventoryBasicList;
        }
        public List<InventoryMaintainanceAndCalibrationScheduleEntity> GetMaintainanceAndCalibrationScheduleByBasicItemDetailsID(int InventoryBasicItemDetailsId, string SType, string Mode)
        {
            List<InventoryMaintainanceAndCalibrationScheduleEntity> MCList = new List<InventoryMaintainanceAndCalibrationScheduleEntity>();

            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));

            var Result = _dbContext.USP_InventoryMaintainanceAndCalibrationSchedule_Select(Mode, objectParameter, SType, InventoryBasicItemDetailsId).ToList();
            foreach (var item in Result)
            {
                MCList.Add(new InventoryMaintainanceAndCalibrationScheduleEntity()
                {
                    ID = item.ID,
                    InventoryBasicItemDetailsID = item.InventoryBasicItemDetailsID,
                    AMCVendorName = item.AMCVendorName,
                    AMCNumber = item.AMCNumber,
                    AMCDate = item.AMCDate,
                    AMCValue = item.AMCValue,
                    AMCPeriod = item.AMCPeriod,
                    AMCStartDate = item.AMCStartDate,
                    Frequency = item.Frequency,
                    Type = item.Type,
                    IsActive = item.IsActive,
                    InsertedBy = item.InsertedBy,
                    InsertedDate = item.InsertedDate,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate,
                    // Frequency = item.Frequency 
                });
            }
            return MCList;
        }

        public string InsertInventoryCommercialFileDetail(int InventoryBasicDetailID, string FileName)
        {
            try
            {
                int ID = 0;
                //   ObjectParameter objectParameter = new ObjectParameter("iID", typeof(long));
                ObjectParameter objectParameter2 = new ObjectParameter("iErrorCode", typeof(int));
                var Result1 = _dbContext.USP_InventoryCommercialFileDetails_Insert(FileName, objectParameter2, ID, InventoryBasicDetailID);
                this._dbContext.SaveChanges();
                return "success"; // return 
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<InventoryMaintainanceAndCalibrationScheduleDatesEntity> GetInventoryMaintanceScheduleDate(int ScheduleID)
        {
            return (from imcs in _dbContext.InventoryMaintainanceAndCalibrationSchedules
                    join ID in _dbContext.InventoryMaintainanceAndCalibrationScheduleDates on imcs.ID equals ID.InventoryMaintainanceAndCalibrationScheduleID
                    where imcs.ID == ScheduleID
                    select new InventoryMaintainanceAndCalibrationScheduleDatesEntity
                    {
                        InventoryBasicItemDetailsID = (Int32)imcs.InventoryBasicItemDetailsID,
                        ID = imcs.ID,
                        InventoryMaintainanceAndCalibrationScheduleID = ID.InventoryMaintainanceAndCalibrationScheduleID,
                        ScheduleDate = ID.ScheduleDate,
                        CompletionStatus = ID.CompletionStatus,
                        IsActive = ID.IsActive,
                    }
              ).ToList();
        }

        public long InsertInventoryMaintainanceDetail(InventoryMaintainanceAndCalibrationEntity imcEntity)
        {
            try
            {
                ObjectParameter objectParameter = new ObjectParameter("iID", typeof(long));
                //ObjectParameter objectParameter2 = new ObjectParameter("iErrorCode", typeof(long));
                var Result1 = _dbContext.USP_InventoryMaintainanceAndCalibrationSchedule_Insert((Int32)imcEntity.InventoryBasicItemDetailsID, imcEntity.AMCVendorName, imcEntity.AMCNumber, imcEntity.AMCDate, imcEntity.StartDate, imcEntity.AMCValue, (short)imcEntity.AMCPeriod, (short)imcEntity.Frequency, imcEntity.Type, objectParameter, imcEntity.InsertedBy);
                this._dbContext.SaveChanges();
                return Result1; // return 
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public long InsertInventoryCalibrationDetails(InventoryMaintainanceAndCalibrationEntity imcEntity)
        {
            try
            {
                ObjectParameter objectParameter = new ObjectParameter("iID", typeof(long));
                //ObjectParameter objectParameter2 = new ObjectParameter("iErrorCode", typeof(long));
                int InventoryBasicItemDetailsID =(int) imcEntity.InventoryBasicItemDetailsID;
                string AMCVendorName = imcEntity.AMCVendorName;
                string AMCNumber = imcEntity.AMCNumber;
                DateTime AMCDate =(DateTime) imcEntity.AMCDate;
                DateTime StartDate = (DateTime)imcEntity.StartDate;
                decimal AMCValue = Convert.ToDecimal(imcEntity.AMCValue);
                short AMCPeriod = Convert.ToInt16(imcEntity.AMCPeriod);
                short Frequency = Convert.ToInt16(imcEntity.Frequency);
                string Type = imcEntity.Type;
                int InsertedBy = (int)imcEntity.InsertedBy;
                var Result1 = _dbContext.USP_InventoryMaintainanceAndCalibrationSchedule_Insert(InventoryBasicItemDetailsID,AMCVendorName,AMCNumber,AMCDate,StartDate,AMCValue,AMCPeriod,Frequency,Type, objectParameter,InsertedBy);
                this._dbContext.SaveChanges();
                return Result1; 
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        // date 26/02/2021
        public List<InventoryMaintenanceEntity> InventoryCalibrationAndMaintenanceList(string sActionType)
        {
            try
            {
                string sMode = "SELECT RECORD BY ACTION TYPE";
                List<InventoryMaintenanceEntity> inventoryMaintenance = new List<InventoryMaintenanceEntity>();
                string sInventoryBasicItemDetailsNumber = null;
                int iItemID = 0;
                //var sActionType = "Maintainance";
                var Result = _dbContext.USP_InventoryCalibrationAndMaintenance_ByItemDetailsID(sMode, sActionType, sInventoryBasicItemDetailsNumber, iItemID).ToList();
                foreach (var item in Result)
                {
                    inventoryMaintenance.Add(new InventoryMaintenanceEntity()
                    {
                       
                        ScheduleID = item.ScheduleID,
                        CompletionDateID = item.CompletionDateID,
                        NextDateID = item.NextDateID,
                        NextDate = item.NextDate,
                        CompletionDate = item.CompletionDate,
                        InventoryBasicItemDetailsID = item.InventoryBasicItemDetailsID,
                        InventoryBasicItemDetailsNumber = item.InventoryBasicItemDetailsNumber,
                        InventoryBasicDetailsID = item.InventoryBasicDetailsID,
                        ItemID = item.ItemID,
                        ItemName = item.ItemName,
                        CategoryID = item.CategoryID,
                        CategoryName = item.CategoryName,
                        // Frequency = item.Frequency 
                    });
                }


                return inventoryMaintenance;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // inventory maintaince and calibration Module
        public List<InventoryMaintainanceAndCalibrationEntity> GetInventoryCalibrationAndMaintenanceUpComingSchedulingDates(string ActionType, string InventoryBasicItemDetailsNumber, string Mode, int ItemID)
        {

            List<InventoryMaintainanceAndCalibrationEntity> InventoryItemList = new List<InventoryMaintainanceAndCalibrationEntity>();
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));
            var Result = _dbContext.USP_InventoryCalibrationAndMaintenance_ByItemDetailsID(Mode, ActionType, InventoryBasicItemDetailsNumber, ItemID).ToList();
            foreach (var item in Result)
            {
                InventoryItemList.Add(new InventoryMaintainanceAndCalibrationEntity()
                {
                    ScheduleID = item.ScheduleID,
                    InventoryBasicDetailsID = item.InventoryBasicDetailsID,
                    InventoryBasicItemDetailsID = item.InventoryBasicItemDetailsID,
                    CompletionDateID = item.CompletionDateID,
                    CompletionDate = item.CompletionDate,
                    NextDateID = item.NextDateID,
                    ItemID = item.ItemID,
                    ItemName = item.ItemName,
                    CategoryID = item.CategoryID,
                    CategoryName = item.CategoryName,
                    NextDate = item.NextDate,
                    InventoryBasicItemDetailsNumber = item.InventoryBasicItemDetailsNumber,

                });
            }
            return InventoryItemList;
        }
        public List<Core.Inventory.InventoryItemEntity> GetInventoryItemMasterBySearch(bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool IsActive, int InventoryTypeMasterID, string Mode)
        {
            List<Core.Inventory.InventoryItemEntity> InventoryItem = new List<Core.Inventory.InventoryItemEntity>();
            int InventoryCategoryMasterID = 0;
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));
            int ID1 = 0;
            var Result = _dbContext.USP_InventoryItemMaster_Select(ID1, (short)InventoryCategoryMasterID, (short)InventoryTypeMasterID, InventoryTypeMasterIsActive, InventoryTypeMasterIsDeleted, InventoryCategoryMasterIsActive, InventoryCategoryMasterIsDeleted, InventoryTypeMasterIsActive, InventoryCategoryMasterIsDeleted, objectParameter, Mode).ToList();

            foreach (var item in Result)
            {
                InventoryItem.Add(new Core.Inventory.InventoryItemEntity()
                {
                    ID = item.ID,
                    CategoryName = item.CategoryName,
                    Name = item.Name,
                    Code = item.Code,
                    CatagoryMasterID =(int) item.CategoryID,
                    InventoryTypeID = item.InventoryTypeID,
                    UnitID = item.UnitID,
                    MinimumStock = item.MinimumStock,
                    AvailableStock = item.AvailableStock,
                    IsActive = item.IsActive,
                    //InsertedBy = (short)item.InsertedBy,
                    //InsertedDate = item.InsertedDate,
                    //ModifiedBy = item.ModifiedBy,
                    //ModifiedDate = item.ModifiedDate,
                    //IsDeleted = item.IsDeleted,
                    //DeletedBy = item.DeletedBy,
                    //DeletedDate = item.DeletedDate,
                  //  InventoryCapacityMasterId = item.InventoryCapacityMasterId,
                    //Capacity = item.Capacity,
                    InventoryTypeName = item.TypeName
                    // Frequency = item.Frequency 
                });
            }
            return InventoryItem;
        }
        public long AddNotification(string Msg, string RoleName, PurchaseEntity purchaseEntity)
        {
            try
            {
                if (purchaseEntity.ItemName != null || purchaseEntity.ItemName != "")
                {
                    var notific = new NotificationDetail()
                    {
                        RoleId = null,
                        NotificationName = purchaseEntity.InventoryBasicItemDetailsNumber + "-" + purchaseEntity.ItemName,
                        Message = Msg,
                        DateTime = purchaseEntity.NextCalibrationDate,
                        IsActive = true,
                        EnteredBy = purchaseEntity.EnteredBy,
                        EnteredDate = DateTime.Now,
                        RoleName = RoleName,
                    };
                    _dbContext.NotificationDetails.Add(notific);
                    _dbContext.SaveChanges();
                    return notific.NotificationDetailId;
                }
                else
                {
                    var notific = new NotificationDetail()
                    {
                        RoleId = null,
                        NotificationName = purchaseEntity.PurchaseMasterID.ToString(),
                        Message = Msg,
                        DateTime = DateTime.Now,
                        IsActive = true,
                        EnteredBy = purchaseEntity.EnteredBy,
                        EnteredDate = DateTime.Now,
                        RoleName = RoleName,
                    };
                    _dbContext.NotificationDetails.Add(notific);
                    _dbContext.SaveChanges();
                    return notific.NotificationDetailId;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string UpdateInventoryMaintainanceAndCalibration(InventoryMaintainanceAndCalibrationEntity imcEntity)
        {
            try
            {

                ObjectParameter objectParameter2 = new ObjectParameter("iErrorCode", typeof(int));
                _dbContext.USP_InventoryMaintainanceAndCalibration_Update(imcEntity.Type, imcEntity.Auditor, imcEntity.CompletionStatus, imcEntity.InventoryBasicDetailsID, imcEntity.InventoryBasicItemDetailsID, imcEntity.AuditDate, imcEntity.AuditObservations, imcEntity.ActionTaken, imcEntity.ItemID, imcEntity.InventoryMaintainanceAndCalibrationScheduleDatesID, imcEntity.ID, imcEntity.UpdatedBy, imcEntity.CalibratorName, imcEntity.NextDate, imcEntity.StartDate, imcEntity.EndDate, objectParameter2);
                this._dbContext.SaveChanges();
                //return Result1.ToString(); // return 
                return "success";
            }
            catch (Exception ex)
            {
                return "success";
            }
        }
        public string InsertInventoryMaintainanceAndCalibration(InventoryMaintainanceAndCalibrationEntity imcEntity)
        {
            

                //ObjectParameter objectParameter1 = new ObjectParameter("iID", typeof(int));

                //var Result1 = _dbContext.USP_InventoryMaintainanceAndCalibration_Insert(imcEntity.Type, imcEntity.Auditor, imcEntity.CompletionStatus, imcEntity.InventoryBasicDetailsID, imcEntity.InventoryBasicItemDetailsID, imcEntity.AuditDate, imcEntity.AuditObservations, imcEntity.ActionTaken, imcEntity.ItemID, imcEntity.InventoryMaintainanceAndCalibrationScheduleDatesID, objectParameter1, imcEntity.InsertedBy, imcEntity.CalibratorName, imcEntity.NextDate, imcEntity.StartDate, imcEntity.EndDate);
                //this._dbContext.SaveChanges();
                //return "success";
                try
                {
                   _dbContext.InventoryMaintainanceAndCalibrations.Add(new InventoryMaintainanceAndCalibration()
                   {
                        Type = imcEntity.Type,
                        InventoryBasicDetailsID = imcEntity.InventoryBasicDetailsID,
                        Auditor = imcEntity.Auditor,
                        AuditDate = imcEntity.AuditDate,
                        AuditObservations = imcEntity.AuditObservations,
                        ActionTaken=imcEntity.ActionTaken,
                        ItemID= imcEntity.ItemID,
                        InventoryMaintainanceAndCalibrationScheduleDatesID =imcEntity.InventoryMaintainanceAndCalibrationScheduleDatesID,
                        InsertedBy=imcEntity.InsertedBy,
                        InsertedDate=imcEntity.InsertedDate,
                        CalibratorName=imcEntity.CalibratorName,
                        NextDate=imcEntity.NextDate,
                        InventoryBasicItemDetailsID=imcEntity.InventoryBasicItemDetailsID,
                        StartDate=imcEntity.StartDate,
                        EndDate=imcEntity.EndDate
                    });
                 _dbContext.SaveChanges();
                if(imcEntity.InventoryMaintainanceAndCalibrationScheduleDatesID > 0)
                {
                    var InventorySchedulateDateId = _dbContext.InventoryMaintainanceAndCalibrationScheduleDates.Find(imcEntity.InventoryMaintainanceAndCalibrationScheduleDatesID);
                    {
                        InventorySchedulateDateId.CompletionStatus = imcEntity.CompletionStatus;
                        InventorySchedulateDateId.ID =(int) imcEntity.InventoryMaintainanceAndCalibrationScheduleDatesID;
                    }   
                 }
                _dbContext.SaveChanges();
                    return "success";
                }
                catch (Exception ex)
                {
                    return ex.InnerException.Message;
                }
        }
        public List<InventoryBasicDetailEntity> GetInventoryBasicItemDetailBySearch(int ID1, long InventoryBasicDetailsID1, bool InventoryBasicDetailsIsActive, bool InventoryBasicItemDetailsIsActive, string mode, int ItemID1, int TypeID, int CategoryId)
        {
            List<InventoryBasicDetailEntity> InventoryBasicItem = new List<InventoryBasicDetailEntity>();
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));
            var Result = _dbContext.USP_InventoryBasicItemDetails_Select(mode, objectParameter, InventoryBasicDetailsIsActive, InventoryBasicItemDetailsIsActive, (Int32)InventoryBasicDetailsID1, ItemID1, TypeID, CategoryId, ID1).ToList();
            foreach (var item in Result)
            {
                InventoryBasicItem.Add(new InventoryBasicDetailEntity()
                {
                    ID = item.ID,
                    InventoryBasicDetailsID = (Int32)item.InventoryBasicDetailsID,
                    BatchNumber = item.BatchNumber,
                    ModelNumber = item.ModelNumber,
                    BarcodeNumber = item.BarcodeNumber,
                    LOTNo = item.LOTNo,
                    SRNO = item.SRNO,
                    Quantity = item.Quantity,
                    Description = item.Description,
                    IsActive = item.IsActive,
                    InsertedBy = item.InsertedBy,
                    InsertedDate = item.InsertedDate,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate,
                    InventoryBasicItemDetailsNumber = item.InventoryBasicItemDetailsNumber,
                });
            }
            return InventoryBasicItem;
        }
        public List<InventoryMaintainanceAndCalibrationScheduleDatesEntity> GetInventoryMaintainanceAndCalibrationScheduleDatesBySearch(int ID, bool InventoryMaintainanceAndCalibrationScheduleIsActive, bool IsActive, string Type1, string AMCNumber, int InventoryBasicItemDetailsID1, int InventoryMaintainanceAndCalibrationScheduleID, string mode)
        {
            List<InventoryMaintainanceAndCalibrationScheduleDatesEntity> InventoryScheduleDateItemList = new List<InventoryMaintainanceAndCalibrationScheduleDatesEntity>();
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));
            var Result = _dbContext.USP_InventoryMaintainanceAndCalibrationScheduleDates_Select(mode, objectParameter, InventoryMaintainanceAndCalibrationScheduleID, InventoryBasicItemDetailsID1, AMCNumber, Type1, ID, InventoryMaintainanceAndCalibrationScheduleIsActive, IsActive).ToList();
            foreach (var item in Result)
            {
                InventoryScheduleDateItemList.Add(new InventoryMaintainanceAndCalibrationScheduleDatesEntity()
                {
                    ID = item.ID,
                    InventoryMaintainanceAndCalibrationScheduleID = item.InventoryMaintainanceAndCalibrationScheduleID,
                    ScheduleDate = item.ScheduleDate,
                    CompletionStatus = item.CompletionStatus,
                    InsertedBy = item.InsertedBy,
                    InsertedDate = item.InsertedDate,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate,
                    AMCPeriods = item.AMCPeriod,
                    Frequency = item.Frequency,
                    IsActive = item.IsActive,
                });
            }
            return InventoryScheduleDateItemList;
        }
        public List<InventoryMaintainanceAndCalibrationEntity> GetInventoryMaintainanceAndCalibrationBySearch(int ID, long InventoryBasicDetailsID1, long InventoryBasicItemDetailsID1, int ItemID1, string Type1, string Mode)
        {
            List<InventoryMaintainanceAndCalibrationEntity> InventoryItemList = new List<InventoryMaintainanceAndCalibrationEntity>();
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));
            var Result = _dbContext.USP_InventoryMaintainanceAndCalibration_Select(Mode, objectParameter, Type1, ItemID1, ID, InventoryBasicDetailsID1, InventoryBasicItemDetailsID1).ToList();
            foreach (var item in Result)
            {
                InventoryItemList.Add(new InventoryMaintainanceAndCalibrationEntity()
                {
                    ID = item.ID,
                    Type = item.Type,
                    InventoryBasicDetailsID = item.InventoryBasicDetailsID,
                    InventoryBasicItemDetailsID = item.InventoryBasicItemDetailsID,
                    Auditor = item.Auditor,
                    AuditDate = item.AuditDate,
                    AuditObservations = item.AuditObservations,
                    ActionTaken = item.ActionTaken,
                    ItemID = item.ItemID,
                    InventoryMaintainanceAndCalibrationScheduleDatesID = item.InventoryMaintainanceAndCalibrationScheduleDatesID,
                    InsertedBy = item.InsertedBy,
                    InsertedDate = item.InsertedDate,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate,
                    CalibratorName = item.CalibratorName,
                    NextCalibrationDate = item.NextDate,
                    CalibrationStartDate = item.StartDate,
                    CalibrationEndDate = item.EndDate,
                    ScheduleDate = item.ScheduleDate,
                    CompletionStatus = item.CompletionStatus,
                    InventoryBasicItemDetailsNumber = item.InventoryBasicItemDetailsNumber,
                    IsActive = item.IsActive,

                });
            }
            return InventoryItemList;
        }

        public List<InventoryMaintenanceEntity> InventoryCalibrationAndMaintenanceList12(string ActionType, string InventoryBasicItemDetailsNumber1, string Mode, int ItemID)
        {
            try
            {
                string sMode = "SELECT RECORD BY ACTION TYPE";
                List<InventoryMaintenanceEntity> inventoryMaintenance = new List<InventoryMaintenanceEntity>();
                string sInventoryBasicItemDetailsNumber = InventoryBasicItemDetailsNumber1;
                int iItemID = ItemID;
                //var sActionType = "Maintainance";
                var Result = _dbContext.USP_InventoryCalibrationAndMaintenance_ByItemDetailsID(sMode, ActionType, sInventoryBasicItemDetailsNumber, iItemID).ToList();
                foreach (var item in Result)
                {
                    inventoryMaintenance.Add(new InventoryMaintenanceEntity()
                    {
                        ScheduleID = item.ScheduleID,
                        CompletionDateID = item.CompletionDateID,
                        NextDateID = item.NextDateID,
                        NextDate = item.NextDate,
                        CompletionDate = item.CompletionDate,
                        InventoryBasicItemDetailsID = item.InventoryBasicItemDetailsID,
                        InventoryBasicItemDetailsNumber = item.InventoryBasicItemDetailsNumber,
                        InventoryBasicDetailsID = item.InventoryBasicDetailsID,
                        ItemID = item.ItemID,
                        ItemName = item.ItemName,
                        CategoryID = item.CategoryID,
                        CategoryName = item.CategoryName,
                        // Frequency = item.Frequency 
                    });
                }
                return inventoryMaintenance;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Core.Inventory.InventoryMaintainanceAndCalibrationScheduleDatesEntity GetScheduleNextDate(long ScheduleDateId)
        {
            return (from e in _dbContext.InventoryMaintainanceAndCalibrationScheduleDates
                    where e.ID == ScheduleDateId
                    select new Core.Inventory.InventoryMaintainanceAndCalibrationScheduleDatesEntity()
                    {
                        ID = e.ID,
                        ScheduleDate = e.ScheduleDate,
                        InventoryMaintainanceAndCalibrationScheduleID =e.InventoryMaintainanceAndCalibrationScheduleID,
                        IsActive = e.IsActive
                    }).FirstOrDefault();
      }
        public Core.Inventory.InventoryMaintainanceAndCalibrationScheduleDatesEntity GetFrequnecyOfDates(long ScheduleDateId,long InventoryBasicItemDetailsID)
        {
            return (from e in _dbContext.InventoryMaintainanceAndCalibrationSchedules
                    where e.ID == ScheduleDateId && e.InventoryBasicItemDetailsID == InventoryBasicItemDetailsID
                    select new Core.Inventory.InventoryMaintainanceAndCalibrationScheduleDatesEntity()
                    {
                        ID = e.ID,
                        Frequency = e.Frequency,
                        AMCPeriods=e.AMCPeriod,
                        InventoryBasicItemDetailsID = (int)e.InventoryBasicItemDetailsID
                       
                    }).FirstOrDefault();
         }
        public Core.Inventory.InventoryMaintainanceAndCalibrationEntity GetInvetoryMaintainceCalibrationData(long ScheduleDateId,long InventoryBasicItemDetailsID)
        {
            return (from e in _dbContext.InventoryMaintainanceAndCalibrations
                    join c in _dbContext.InventoryMaintainanceAndCalibrationScheduleDates on e.InventoryMaintainanceAndCalibrationScheduleDatesID equals c.ID
                    where e.InventoryMaintainanceAndCalibrationScheduleDatesID == ScheduleDateId && e.InventoryBasicItemDetailsID == InventoryBasicItemDetailsID
                    select new Core.Inventory.InventoryMaintainanceAndCalibrationEntity()
                    {
                        ID=e.ID,
                        InventoryBasicItemDetailsID=e.InventoryBasicItemDetailsID,
                        InventoryBasicDetailsID=e.InventoryBasicDetailsID ,
                        ItemID=e.ItemID,
                        InventoryMaintainanceAndCalibrationScheduleDatesID=e.InventoryMaintainanceAndCalibrationScheduleDatesID,
                        NextCalibrationDate=e.NextDate,
                        CalibrationStartDate=e.StartDate,
                        CalibrationEndDate=e.EndDate,
                        AuditDate=e.AuditDate,
                        AuditObservations=e.AuditObservations,
                        Auditor=e.Auditor,
                        CalibratorName=e.CalibratorName,
                        ActionTaken=e.ActionTaken,
                        CompletionStatus=c.CompletionStatus
                    }).FirstOrDefault();
        }
        public List<Core.Inventory.InventoryItemEntity> GetInventoryItemMasterBySearch12(int ID,int InventoryCategoryMasterID,int InventoryTypeMasterID,bool InventoryItemMasterIsActive,bool InventoryItemMasterIsDeleted, bool InventoryCategoryMasterIsActive, bool InventoryCategoryMasterIsDeleted, bool InventoryTypeMasterIsActive, bool InventoryTypeMasterIsDeleted, bool IsActive, string Mode)
        {
            List<Core.Inventory.InventoryItemEntity> InventoryItem = new List<Core.Inventory.InventoryItemEntity>();
            ObjectParameter objectParameter = new ObjectParameter("iErrorCode", typeof(int));
            int ID1 = 0;
            var Result = _dbContext.USP_InventoryItemMaster_Select(ID, (short)InventoryCategoryMasterID,(short)InventoryTypeMasterID,InventoryItemMasterIsActive, InventoryItemMasterIsDeleted,InventoryCategoryMasterIsActive,InventoryCategoryMasterIsDeleted,InventoryTypeMasterIsActive,InventoryTypeMasterIsDeleted,objectParameter,Mode).ToList();

            foreach (var item in Result)
            {
                InventoryItem.Add(new Core.Inventory.InventoryItemEntity()
                {
                    ID = item.ID,
                    CategoryName = item.CategoryName,
                    Name = item.Name,
                    Code = item.Code,
                    CatagoryMasterID = (int)item.CategoryID,
                    CatagoryID=item.CategoryID,
                    ItemID=item.ID,
                    TypeID = item.InventoryTypeID,
                    InventoryTypeID = item.InventoryTypeID,
                    UnitID = item.UnitID,
                    MinimumStock = item.MinimumStock,
                    AvailableStock = item.AvailableStock,
                    IsActive = item.IsActive,
                    Capacity = item.Capacity,
                    InventoryTypeName = item.TypeName
                    // Frequency = item.Frequency 
                }) ;
            }
            return InventoryItem;
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