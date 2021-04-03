using LIMS_DEMO.Common;
using LIMS_DEMO.Core.Inventory;
using LIMS_DEMO.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.DAL.Inventory
{
    public class PurchaseDAL : IPurchase
    {
        readonly LIMSEntities _dbContext;
        public PurchaseDAL()
        {
            _dbContext = new LIMSEntities();
        }

        public PurchaseEntity GetPurchaseRequestDetails(int PurchaseRequestID)
        {
            return _dbContext.PurchaseRequestDetails.Where(u => u.PurchaseRequestID == PurchaseRequestID).Select(u => new PurchaseEntity()
            {
                PurchaseRequestID = PurchaseRequestID,
                ItemMasterId = (Int32)u.ItemMasterID,
                PurchaseSupplierID = (Int32)u.PurchaseSupplierID,
                InventoryTypeID = (Int32)u.InventoryTypeID,
                PurchaseMasterID = (Int32)u.PurchaseMasterID,
                CategoryName = u.CatagoryName,
                Brand = u.Brand,
                PackSize = u.PackSize,
                Quantity = (Int32)u.Quantity,
                Grade = u.Grade,
                Priority = u.Priority,
                PurchaseUpload = u.PurchaseUpload,
                FileName = u.FileName,
                Purpose = u.Purpose,
                Amount = u.Amount,
                DiscAmount = u.DiscAmount,
                GSTAmount = u.GSTAmount,
                NetAmount = u.NetAmount,
                Specification = u.Specification,
                Remark = u.Remark,
                COA = u.COA_COC,
                Rate = (decimal)u.Rate,
                TickIf = (bool)u.TickIf,
                EstimatedLagTime = u.EstimatedLagTime,
                IsActive = (bool)u.IsActive
            }).FirstOrDefault();
        }

        public long AddPurchaseDetails(PurchaseEntity purchaseEntity)
        {

            try
            {
                var result = new PurchaseRequestDetail()
                {


                    ItemMasterID = purchaseEntity.ItemMasterId,
                    InventoryTypeID = (short)purchaseEntity.InventoryTypeID,
                    PurchaseSupplierID = purchaseEntity.PurchaseSupplierID,
                    PurchaseMasterID = purchaseEntity.PurchaseMasterID,
                    Brand = purchaseEntity.Brand,
                    PackSize = purchaseEntity.PackSize,
                    Grade = purchaseEntity.Grade,
                    CatagoryName = purchaseEntity.CategoryName,
                    Quantity = purchaseEntity.Quantity,
                    DiscPercent = purchaseEntity.DiscPercent,
                    GSTPercent = purchaseEntity.GSTPercent,
                    Specification = purchaseEntity.Specification,
                    Remark = purchaseEntity.Remark,
                    Rate = purchaseEntity.Rate,
                    Amount = purchaseEntity.Amount,
                    GSTAmount = purchaseEntity.GSTAmount,
                    DiscAmount = purchaseEntity.DiscAmount,
                    NetAmount = purchaseEntity.NetAmount,
                    EstimatedLagTime = purchaseEntity.EstimatedLagTime,
                    COA_COC = purchaseEntity.COA,
                    IsIGST = purchaseEntity.IsIGST,
                    Priority = purchaseEntity.Priority,
                    Purpose = purchaseEntity.Purpose,
                    TickIf = purchaseEntity.TickIf,
                    FileName = purchaseEntity.FileName,
                    PurchaseUpload = purchaseEntity.PurchaseUpload,
                    RecordStatus = (int?)CurrentStatusEnum.notsubmit,
                    EnteredBy = purchaseEntity.EnteredBy,
                    EnteredDate = purchaseEntity.EnteredDate,
                    IsActive = purchaseEntity.IsActive
                };
                _dbContext.PurchaseRequestDetails.Add(result);
                _dbContext.SaveChanges();
                return result.PurchaseRequestID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public long AddPurchaseMasterDetails(PurchaseEntity purchaseEntity)
        {
            try
            {
                var result = new PurchaseMasterDetail()
                {
                    PurchaseMasterID = purchaseEntity.PurchaseMasterID,
                    EnteredBy = purchaseEntity.EnteredBy,
                    EnteredDate = purchaseEntity.EnteredDate,
                    IsActive = purchaseEntity.IsActive
                };
                _dbContext.PurchaseMasterDetails.Add(result);
                _dbContext.SaveChanges();
                return result.PurchaseMasterID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public long GetInventoryTypeID(int PurchaseRequestID)
        {
            try
            {
                long? InventoryTypeID = _dbContext.PurchaseRequestDetails.Where(e => e.PurchaseRequestID == PurchaseRequestID).Select(e => e.InventoryTypeID).FirstOrDefault();

                return (long)InventoryTypeID;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public bool AddPurchaseTermsAndCondition(int PurchaseRequestID, int PurchaseMasterID, int InventoryTypeID, int TermAndCondtId)
        {
            var result = new PurchaseTNC()
            {
                TermAndCondtId = TermAndCondtId,
                PurchaseRequestId = PurchaseRequestID,
                PurchaseMasterId = PurchaseMasterID,
                InventoryTypeId = (short)InventoryTypeID,
                IsActive = true,
                EnteredBy = 1,
                EnteredDate = DateTime.UtcNow

            };
            _dbContext.PurchaseTNCs.Add(result);
            _dbContext.SaveChanges();

            return true;
        }
        public long UpdatePurchaseDetails(PurchaseEntity purchaseEntity)
        {

            try
            {
                var PurchaseRequest = _dbContext.PurchaseRequestDetails.Find(purchaseEntity.PurchaseRequestID);
                PurchaseRequest.ItemMasterID = purchaseEntity.ItemMasterId;
                PurchaseRequest.InventoryTypeID = (short)purchaseEntity.InventoryTypeID;
                PurchaseRequest.PurchaseSupplierID = purchaseEntity.PurchaseSupplierID2;
                PurchaseRequest.PurchaseMasterID = purchaseEntity.PurchaseMasterID;
                PurchaseRequest.Brand = purchaseEntity.Brand;
                PurchaseRequest.PackSize = purchaseEntity.PackSize;
                PurchaseRequest.Grade = purchaseEntity.Grade;
                PurchaseRequest.CatagoryName = purchaseEntity.CategoryName;
                PurchaseRequest.Quantity = purchaseEntity.Quantity;
                PurchaseRequest.DiscPercent = purchaseEntity.DiscPercent;
                PurchaseRequest.GSTPercent = purchaseEntity.GSTPercent;
                PurchaseRequest.Specification = purchaseEntity.Specification;
                PurchaseRequest.Remark = purchaseEntity.Remark;
                PurchaseRequest.Approvests = purchaseEntity.Approvests;
                PurchaseRequest.COA_COC = purchaseEntity.COA;
                PurchaseRequest.Priority = purchaseEntity.Priority;
                PurchaseRequest.Purpose = purchaseEntity.Purpose;
                PurchaseRequest.FileName = purchaseEntity.FileName;
                PurchaseRequest.PurchaseUpload = purchaseEntity.PurchaseUpload;
                PurchaseRequest.RecordStatus = (int?)CurrentStatusEnum.notsubmit;
                PurchaseRequest.EnteredBy = purchaseEntity.EnteredBy;
                PurchaseRequest.EnteredDate = purchaseEntity.EnteredDate;
                PurchaseRequest.IsActive = purchaseEntity.IsActive;
                _dbContext.SaveChanges();
                return PurchaseRequest.PurchaseRequestID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public bool UpdatePurchaseTermsAndCondition(int PurchaseRequestID, int PurchaseMasterID, int InventoryTypeID, int TermAndCondtId)
        {
            var PurchaseTNC = _dbContext.PurchaseTNCs.Find(TermAndCondtId);
            PurchaseTNC.TermAndCondtId = TermAndCondtId;
            PurchaseTNC.PurchaseRequestId = PurchaseRequestID;
            PurchaseTNC.PurchaseMasterId = PurchaseMasterID;
            PurchaseTNC.InventoryTypeId = (short)InventoryTypeID;
            PurchaseTNC.IsActive = true;
            PurchaseTNC.EnteredBy = 1;
            PurchaseTNC.EnteredDate = DateTime.UtcNow;
            _dbContext.SaveChanges();
            return true;
        }
        public string DeleteContract(long EnquiryDetailId)
        {
            try
            {
                var contract = _dbContext.EnquiryDetails.Find(EnquiryDetailId);
                contract.IsActive = false;
                //_dbContext.EnquiryDetails.Remove(contract);
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public PurchaseEntity GetPurchaseRequestDetailsPI(int PurchaseRequestID)
        {
            return _dbContext.PurchaseRequestDetails.Where(u => u.PurchaseRequestID == PurchaseRequestID).Select(u => new PurchaseEntity()
            {
                PurchaseRequestID = PurchaseRequestID,
                ItemMasterId = (Int32)u.ItemMasterID,
                PurchaseSupplierID = (Int32)u.PurchaseSupplierID == null ? 0 : (Int32)u.PurchaseSupplierID,
                InventoryTypeID = (Int32)u.InventoryTypeID,
                PurchaseMasterID = (Int32)u.PurchaseMasterID,
                Brand = u.Brand,
                PackSize = u.PackSize,
                Quantity = (Int32)u.Quantity,
                Grade = u.Grade,
                Priority = u.Priority,
                PurchaseUpload = u.PurchaseUpload,
                FileName = u.FileName,
                Purpose = u.Purpose,
                Amount = u.Amount,
                DiscAmount = u.DiscAmount,
                GSTAmount = u.GSTAmount,
                NetAmount = u.NetAmount,
                Specification = u.Specification,
                Remark = u.Remark,
                COA = u.COA_COC,
                Rate = (decimal)u.Rate,
                TickIf = (bool)u.TickIf,
                EstimatedLagTime = u.EstimatedLagTime,
                IsActive = (bool)u.IsActive
            }).FirstOrDefault();
        }
        public IList<PurchaseEntity> GetPOList()
        {
            var result = (from prd in _dbContext.PurchaseRequestDetails
                          join pmd in _dbContext.PurchaseMasterDetails on prd.PurchaseMasterID equals pmd.PurchaseMasterID
                          join psd in _dbContext.PurchaseSupplierDetails on prd.PurchaseSupplierID equals psd.PurchaseSupplierID
                          join iim in _dbContext.InventoryItemMasters on prd.ItemMasterID equals iim.ID
                          where prd.IsActive == true && prd.ApprovePO == null && prd.Approvests == 1
                          select new
                          {
                              prd.PurchaseRequestID,
                              prd.PurchaseMasterID,
                              prd.InventoryTypeID,
                              iim.Name,
                              prd.PONumber,
                              prd.Quantity,
                              prd.Rate,
                              psd.SupplierName
                          }).OrderByDescending(prd => prd.PurchaseRequestID).ToList();

            IList<PurchaseEntity> purchaseEntities = new List<PurchaseEntity>();

            foreach (var Item in result)
            {

                PurchaseEntity purchaseEntity = new PurchaseEntity();
                purchaseEntity.PurchaseRequestID = Item.PurchaseRequestID;
                purchaseEntity.InventoryTypeID = (Int32)Item.InventoryTypeID;
                purchaseEntity.PurchaseMasterID = (Int32)Item.PurchaseMasterID;
                purchaseEntity.SupplierName = Item.SupplierName;
                purchaseEntity.Item = Item.Name;
                purchaseEntities.Add(purchaseEntity);

            }
            return purchaseEntities;
        }
        public IList<PurchaseEntity> GetTML()
        {
            var result = (from prd in _dbContext.PurchaseRequestDetails
                          join psd in _dbContext.PurchaseSupplierDetails on prd.PurchaseSupplierID equals psd.PurchaseSupplierID
                          join iim in _dbContext.InventoryItemMasters on prd.ItemMasterID equals iim.ID
                          join icm in _dbContext.InventoryCategoryMasters on iim.CategoryID equals icm.ID
                          where prd.IsActive == true && prd.Approvests == null
                          select new
                          {
                              prd.PurchaseRequestID,
                              prd.InventoryTypeID,
                              iim.Name,
                              icm.CategoryName,
                              prd.Quantity,
                              prd.Brand,
                              prd.Specification,
                              prd.NetAmount,
                              psd.SupplierName
                          }).OrderByDescending(prd => prd.PurchaseRequestID).ToList();

            IList<PurchaseEntity> purchaseEntities = new List<PurchaseEntity>();
            foreach (var Item in result)
            {

                PurchaseEntity purchaseEntity = new PurchaseEntity();
                purchaseEntity.PurchaseRequestID = Item.PurchaseRequestID;
                purchaseEntity.SupplierName = Item.SupplierName;
                purchaseEntity.CategoryName = Item.CategoryName;
                purchaseEntity.Item = Item.Name;
                purchaseEntity.Brand = Item.Brand;
                purchaseEntity.Specification = Item.Specification;
                purchaseEntity.InventoryTypeID = (Int32)Item.InventoryTypeID;
                purchaseEntity.Quantity = (Int32)Item.Quantity;
                purchaseEntity.NetAmount = Item.NetAmount;
                purchaseEntities.Add(purchaseEntity);
            }
            return purchaseEntities;
        }
        public IList<PurchaseEntity> GetPurchaseRecordFirstList()
        {
            var result = (from prd in _dbContext.PurchaseRequestDetails
                          join pmd in _dbContext.PurchaseMasterDetails on prd.PurchaseMasterID equals pmd.PurchaseMasterID
                          join psd in _dbContext.PurchaseSupplierDetails on prd.PurchaseSupplierID equals psd.PurchaseSupplierID
                          join im in _dbContext.InventoryItemMasters on prd.ItemMasterID equals im.ID
                          where prd.Approvests == 1
                          select new
                          {
                              prd.PurchaseRequestID,
                              prd.RecordStatus,
                              prd.PurchaseMasterID,
                              prd.ItemMasterID,
                              prd.InventoryTypeID,
                              im.Name,
                              prd.PONumber,
                              prd.Quantity,
                              psd.SupplierName,
                              prd.EnteredDate,

                          }).OrderByDescending(prd => prd.PurchaseRequestID).ToList();

            IList<PurchaseEntity> purchaseEntities = new List<PurchaseEntity>();
            int PrevPurchaseMasterID = 0;
            foreach (var Item in result)
            {
                var TotalCount1 = (from sp in _dbContext.PurchaseRequestDetails
                                   where sp.PurchaseRequestID == Item.PurchaseRequestID
                                   group sp by sp.PurchaseRequestID into f
                                   select new { cnt = f.Count() }).FirstOrDefault();

                var ApprovedCount1 = (from sp in _dbContext.PurchaseRequestDetails
                                      where sp.PurchaseRequestID == Item.PurchaseRequestID && (int)sp.RecordStatus == 8
                                      group sp by sp.PurchaseRequestID into f
                                      select new { cnt = f.Count() }).FirstOrDefault();

                var ForAnalyseCount1 = (from sp in _dbContext.PurchaseRequestDetails
                                        where sp.PurchaseRequestID == Item.PurchaseRequestID && ((int)sp.RecordStatus == 9)
                                        group sp by sp.PurchaseRequestID into f
                                        select new { cnt = f.Count() }).FirstOrDefault();
                if (PrevPurchaseMasterID != Item.PurchaseRequestID)
                {
                    PurchaseEntity purchaseEntity = new PurchaseEntity();
                    purchaseEntity.PurchaseRequestID = Item.PurchaseRequestID;
                    purchaseEntity.SupplierName = Item.SupplierName;
                    purchaseEntity.ItemMasterId = (Int32)Item.ItemMasterID;
                    purchaseEntity.Item = Item.Name;
                    purchaseEntity.CurrentStatus = (TotalCount1 == null ? 0 : TotalCount1.cnt) - (ApprovedCount1 == null ? 0 : ApprovedCount1.cnt);
                    purchaseEntity.PurchaseMasterID = (Int32)Item.PurchaseMasterID;
                    purchaseEntity.PurchaseOrderNo = Item.PONumber;
                    purchaseEntity.Quantity = (Int32)Item.Quantity;
                    purchaseEntity.InventoryTypeID = (Int32)Item.InventoryTypeID;
                    purchaseEntity.EnteredDate = (DateTime)Item.EnteredDate;
                    purchaseEntities.Add(purchaseEntity);
                }
                PrevPurchaseMasterID = Item.PurchaseRequestID;
            }
            return purchaseEntities;
        }
        public IList<PurchaseEntity> GetPIL()
        {
            var result = (from prd in _dbContext.PurchaseRequestDetails
                          join iim in _dbContext.InventoryItemMasters on prd.ItemMasterID equals iim.ID
                          where prd.IsActive == true && prd.Approvests == null && prd.PurchaseSupplierID == null
                          select new
                          {
                              prd.PurchaseRequestID,
                              prd.InventoryTypeID,
                              iim.Name,
                              prd.Quantity,
                              prd.Brand,
                              prd.Specification,
                          }).OrderByDescending(prd => prd.PurchaseRequestID).ToList();

            IList<PurchaseEntity> purchaseEntities = new List<PurchaseEntity>();
            foreach (var Item in result)
            {

                PurchaseEntity purchaseEntity = new PurchaseEntity();
                purchaseEntity.PurchaseRequestID = Item.PurchaseRequestID;
                purchaseEntity.Item = Item.Name;
                purchaseEntity.Brand = Item.Brand;
                purchaseEntity.Specification = Item.Specification;
                purchaseEntity.InventoryTypeID = (Int32)Item.InventoryTypeID;
                purchaseEntity.Quantity = (Int32)Item.Quantity;
                purchaseEntities.Add(purchaseEntity);
            }
            return purchaseEntities;
        }

        public IList<PurchaseEntity> GetInchargeList()
        {
            var result = (from prd in _dbContext.PurchaseRequestDetails
                          join pmd in _dbContext.PurchaseMasterDetails on prd.PurchaseMasterID equals pmd.PurchaseMasterID
                          join psd in _dbContext.PurchaseSupplierDetails on prd.PurchaseSupplierID equals psd.PurchaseSupplierID
                          join iim in _dbContext.InventoryItemMasters on prd.ItemMasterID equals iim.ID
                          join icm in _dbContext.InventoryCategoryMasters on iim.CategoryID equals icm.ID
                          where prd.IsActive == true & prd.Approvests == 1
                          select new
                          {
                              prd.PurchaseRequestID,
                              prd.PurchaseMasterID,
                              prd.InventoryTypeID,
                              iim.Name,
                              prd.PONumber,
                              prd.Quantity,
                              prd.Rate,
                              iim.CategoryID,
                              icm.CategoryName,
                              psd.SupplierName,
                              prd.IsIGST
                          }).OrderByDescending(prd => prd.PurchaseRequestID).ToList();

            IList<PurchaseEntity> purchaseEntities = new List<PurchaseEntity>();
            int PrevPurchaseMasterID = 0;
            int InventoryId = 0;
            string CategoryName = "";
            foreach (var Item in result)
            {
                string ItemName = "";

                if (PrevPurchaseMasterID != Item.PurchaseMasterID && CategoryName != Item.CategoryName)
                {
                    if (InventoryId != Item.InventoryTypeID)
                    {
                        PurchaseEntity purchaseEntity = new PurchaseEntity();
                        purchaseEntity.PurchaseRequestID = Item.PurchaseRequestID;
                        purchaseEntity.InventoryTypeID = (Int32)Item.InventoryTypeID;
                        purchaseEntity.CatagoryMasterId = (Int32)Item.CategoryID;
                        purchaseEntity.CategoryName = Item.CategoryName;
                        purchaseEntity.PurchaseMasterID = (Int32)Item.PurchaseMasterID;
                        purchaseEntity.SupplierName = Item.SupplierName;
                        //IList<string> strItemName = new List<string>();
                        var strItemNameList = (from a in result
                                               where a.PurchaseMasterID == Item.PurchaseMasterID && a.InventoryTypeID == Item.InventoryTypeID && a.CategoryName == Item.CategoryName
                                               select new
                                               {
                                                   a.Name,
                                                   a.CategoryName,
                                                   a.PurchaseRequestID
                                               }).ToList();
                        string strItemName = string.Empty;
                        string strPurchaseRequestId = string.Empty;
                        for (int i = 0; i < strItemNameList.Count; i++)
                        {
                            if (i == 0)
                            {
                                strItemName = strItemNameList[i].Name;
                                strPurchaseRequestId = strItemNameList[i].PurchaseRequestID.ToString();
                            }
                            else
                            {
                                strItemName = strItemName + ", " + strItemNameList[i].Name;
                                strPurchaseRequestId = strPurchaseRequestId + "," + strItemNameList[i].PurchaseRequestID.ToString();
                            }
                        }
                        //purchaseEntity.Item = ItemName;
                        purchaseEntity.PurchaseOrderNo = Item.PONumber;
                        purchaseEntity.Quantity = (Int32)Item.Quantity;
                        purchaseEntity.Rate = Item.Rate;
                        purchaseEntity.IsIGST = Item.IsIGST == null ? false : (bool)Item.IsIGST;
                        purchaseEntity.Item = strItemName;
                        purchaseEntity.strPurchaseRequestId = strPurchaseRequestId;
                        purchaseEntities.Add(purchaseEntity);
                    }
                    else
                    {
                        PurchaseEntity purchaseEntity = new PurchaseEntity();
                        purchaseEntity.PurchaseRequestID = Item.PurchaseRequestID;
                        purchaseEntity.InventoryTypeID = (Int32)Item.InventoryTypeID;
                        purchaseEntity.CatagoryMasterId = (Int32)Item.CategoryID;
                        purchaseEntity.CategoryName = Item.CategoryName;
                        purchaseEntity.PurchaseMasterID = (Int32)Item.PurchaseMasterID;
                        purchaseEntity.SupplierName = Item.SupplierName;
                        //IList<string> strItemName = new List<string>();
                        var strItemNameList = (from a in result
                                               where a.PurchaseMasterID == Item.PurchaseMasterID && a.InventoryTypeID == Item.InventoryTypeID && a.CategoryName == Item.CategoryName
                                               select new
                                               {
                                                   a.Name,
                                                   a.CategoryName,
                                                   a.PurchaseRequestID
                                               }).ToList();
                        string strItemName = string.Empty;
                        string strPurchaseRequestId = string.Empty;
                        for (int i = 0; i < strItemNameList.Count; i++)
                        {
                            if (i == 0)
                            {
                                strItemName = strItemNameList[i].Name;
                                strPurchaseRequestId = strItemNameList[i].PurchaseRequestID.ToString();
                            }
                            else
                            {
                                strItemName = strItemName + ", " + strItemNameList[i].Name;
                                strPurchaseRequestId = strPurchaseRequestId + "," + strItemNameList[i].PurchaseRequestID.ToString();
                            }
                        }
                        //purchaseEntity.Item = ItemName;
                        purchaseEntity.PurchaseOrderNo = Item.PONumber;
                        purchaseEntity.Quantity = (Int32)Item.Quantity;
                        purchaseEntity.Rate = Item.Rate;
                        purchaseEntity.Item = strItemName;
                        purchaseEntity.IsIGST = Item.IsIGST == null ? false : (bool)Item.IsIGST;
                        purchaseEntity.strPurchaseRequestId = strPurchaseRequestId;
                        purchaseEntities.Add(purchaseEntity);
                    }


                }

                else
                {
                    ItemName = ItemName + ", " + Item.Name;
                }
                PrevPurchaseMasterID = Item.PurchaseRequestID;
                InventoryId = (Int32)Item.InventoryTypeID;
                CategoryName = Item.CategoryName;
                //purchaseEntity.Item = ItemName;
            }
            return purchaseEntities;
        }
        public List<PurchaseEntity> GetTermsAndConditionPurchase(int InventoryTypeID)
        {
            try
            {
                return (from t in _dbContext.TermsAndConditionsPurchases
                        join qt in _dbContext.InventoryTypeMasters on t.InventoryTypeId equals qt.ID
                        where t.IsActive == true && t.InventoryTypeId == InventoryTypeID
                        select new PurchaseEntity()
                        {
                            TermAndCondtId = t.TermAndCondtId,
                            TermAndCondt = t.TermAndCondt,
                            InventoryTypeID = (short)t.InventoryTypeId
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<PurchaseEntity> GetPurchaseTNCDetails(int PurchaseRequestID)
        {
            try
            {
                return (from t in _dbContext.PurchaseTNCs
                        join ptnc in _dbContext.TermsAndConditionsPurchases on t.TermAndCondtId equals ptnc.TermAndCondtId
                        where t.IsActive == true && t.PurchaseRequestId == PurchaseRequestID
                        select new PurchaseEntity()
                        {
                            TermAndCondtId = (Int32)t.TermAndCondtId,
                            TermAndCondt = ptnc.TermAndCondt

                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public PurchaseEntity GetPurchaseRecordDetails(int PurchaseRecordID)
        {
            return _dbContext.PurchaseRecords.Where(u => u.PurchaseRecordID == PurchaseRecordID).Select(u => new PurchaseEntity()
            {
                PurchaseRecordID = PurchaseRecordID,
                GradeReceived = u.GradeReceived,
                QtyReceived = u.QtyReceived,
                BrandReceived = u.BrandReceived,
                ReceivedDate = u.DateOfReceive,
                RemarkRecord = u.Remark,
                DeliveryChallanDate = u.DeliveryChallanDate,
                DeliveryChallanNo = u.DeliveryChallanNo,
                BillDate = u.BillDate,
                BillNo = u.BillNo,
                DeliveryTime = (Int32)u.DeliveryTime,
                IntegrityOfPackaging = u.IntegrityOfPackaging,
                ConditionOfPackaging = u.ConditionOfPackaging,
                IsActive = (bool)u.IsActive
            }).FirstOrDefault();
        }
        public PurchaseEntity GetPurchaseRequestDetails2(int PurchaseRequestID)
        {
            return (from prd in _dbContext.PurchaseRequestDetails
                    join psd in _dbContext.PurchaseSupplierDetails on prd.PurchaseSupplierID equals psd.PurchaseSupplierID
                    where prd.PurchaseRequestID == PurchaseRequestID
                    select new PurchaseEntity()
                    {
                        PurchaseRequestID = prd.PurchaseRequestID,
                        PurchaseMasterID = (Int32)prd.PurchaseMasterID,
                        Brand = prd.Brand,
                        Grade = prd.Grade,
                        PackSize = prd.PackSize,
                        Specification = prd.Specification,
                        SupplierName = psd.SupplierName,
                        PurchaseOrderNo = prd.PONumber,
                        Quantity = (Int32)prd.Quantity,
                    }
                    ).FirstOrDefault();
        }

        public string AddPurchaseRecordDetails(PurchaseEntity purchaseEntity)
        {
            try
            {
                var enquiryMaster = _dbContext.PurchaseRequestDetails.Find(purchaseEntity.PurchaseRequestID);
                enquiryMaster.RecordStatus = (int?)CurrentStatusEnum.submit;
                _dbContext.SaveChanges();
                _dbContext.PurchaseRecords.Add(new PurchaseRecord()
                {


                    PurchaseRecordID = purchaseEntity.PurchaseRecordID,
                    PurchaseRequestID = purchaseEntity.PurchaseRequestID,
                    DateOfReceive = purchaseEntity.ReceivedDate,
                    Remark = purchaseEntity.RemarkRecord,
                    QtyReceived = purchaseEntity.QtyReceived,
                    BrandReceived = purchaseEntity.BrandReceived,
                    GradeReceived = purchaseEntity.GradeReceived,
                    DeliveryChallanDate = purchaseEntity.DeliveryChallanDate,
                    DeliveryChallanNo = purchaseEntity.DeliveryChallanNo,
                    DeliveryTime = purchaseEntity.DeliveryTime,
                    ConditionOfPackaging = purchaseEntity.ConditionOfPackaging,
                    IntegrityOfPackaging = purchaseEntity.IntegrityOfPackaging,
                    BillDate = purchaseEntity.BillDate,
                    BillNo = purchaseEntity.BillNo,
                    EnteredBy = purchaseEntity.EnteredBy,
                    EnteredDate = purchaseEntity.EnteredDate,
                    CurrentStatus = (int?)CurrentStatusEnum.submit,
                    IsActive = purchaseEntity.IsActive
                });
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string UpdatePurchaseRecord(PurchaseEntity purchaseEntity)
        {
            try
            {
                var purchaserecord = _dbContext.PurchaseRecords.Find(purchaseEntity.PurchaseRecordID);
                purchaserecord.QtyReceived = purchaseEntity.QtyReceived;
                purchaserecord.BrandReceived = purchaseEntity.BrandReceived;
                purchaserecord.GradeReceived = purchaseEntity.GradeReceived;
                purchaserecord.DeliveryTime = purchaseEntity.DeliveryTime;
                purchaserecord.DeliveryChallanNo = purchaseEntity.DeliveryChallanNo;
                purchaserecord.IsActive = purchaseEntity.IsActive;
                purchaserecord.DeliveryChallanDate = purchaseEntity.DeliveryChallanDate;
                purchaserecord.BillNo = purchaseEntity.BillNo;
                purchaserecord.DateOfReceive = purchaseEntity.ReceivedDate;
                purchaserecord.Remark = purchaseEntity.RemarkRecord;
                purchaserecord.BillDate = purchaseEntity.BillDate;
                purchaserecord.IntegrityOfPackaging = purchaseEntity.IntegrityOfPackaging;
                purchaserecord.ConditionOfPackaging = purchaseEntity.ConditionOfPackaging;
                purchaserecord.EnteredBy = purchaseEntity.EnteredBy;
                purchaserecord.CurrentStatus = (int?)CurrentStatusEnum.submit;
                purchaserecord.EnteredDate = purchaseEntity.EnteredDate;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public List<PurchaseEntity> GetPurchaseRecordList()
        {
            try
            {

                return (from stp in _dbContext.PurchaseRecords
                        join prd in _dbContext.PurchaseRequestDetails on stp.PurchaseRequestID equals prd.PurchaseRequestID
                        join itm in _dbContext.InventoryItemMasters on prd.ItemMasterID equals itm.ID
                        join psd in _dbContext.PurchaseSupplierDetails on prd.PurchaseSupplierID equals psd.PurchaseSupplierID
                        where stp.IsActive == true
                        select new PurchaseEntity()
                        {
                            PurchaseRecordID = stp.PurchaseRecordID,
                            PurchaseRequestID = (Int32)stp.PurchaseRequestID,
                            Quantity = (Int32)prd.Quantity,
                            ItemMasterId = (Int32)prd.ItemMasterID,
                            Brand = prd.Brand,
                            Grade = prd.Grade,
                            Item = itm.Name,
                            SupplierName = psd.SupplierName,
                            PurchaseOrderNo = prd.PONumber,
                            Specification = prd.Specification,
                            QtyReceived = stp.QtyReceived,
                            RemarkRecord = stp.Remark,
                            ReceivedDate = stp.DateOfReceive,
                            BrandReceived = stp.BrandReceived,
                            GradeReceived = stp.GradeReceived,
                            DeliveryChallanNo = stp.DeliveryChallanNo,
                            DeliveryTime = (Int32)stp.DeliveryTime,
                            IntegrityOfPackaging = stp.IntegrityOfPackaging,
                            ConditionOfPackaging = stp.ConditionOfPackaging,
                            DeliveryChallanDate = stp.DeliveryChallanDate,
                            //CurrentStatus = (Int32)stp.CurrentStatus,
                            BillNo = stp.BillNo,
                            BillDate = stp.BillDate,
                            IsActive = (bool)stp.IsActive,
                        }).OrderByDescending(stp => stp.PurchaseRecordID).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public IList<PurchaseEntity> GetPurchaseRecordSecondList(int PurchaseMasterID, int InventoryTypeID, string PurchaseOrderNo)
        {
            var result = (from prd in _dbContext.PurchaseRequestDetails
                          join pmd in _dbContext.PurchaseMasterDetails on prd.PurchaseMasterID equals pmd.PurchaseMasterID
                          join psd in _dbContext.PurchaseSupplierDetails on prd.PurchaseSupplierID equals psd.PurchaseSupplierID
                          join im in _dbContext.InventoryItemMasters on prd.ItemMasterID equals im.ID
                          where prd.PurchaseMasterID == PurchaseMasterID & prd.Approvests == 1 & prd.InventoryTypeID == InventoryTypeID & prd.PONumber == PurchaseOrderNo
                          select new
                          {
                              prd.PurchaseRequestID,
                              prd.RecordStatus,
                              pmd.PurchaseMasterID,
                              prd.InventoryTypeID,
                              prd.ItemMasterID,
                              im.Name,
                              prd.PONumber,
                              prd.Quantity,
                              psd.SupplierName,
                              prd.EnteredDate
                          }).OrderByDescending(prd => prd.PurchaseRequestID).ToList();

            IList<PurchaseEntity> purchaseEntities = new List<PurchaseEntity>();
            foreach (var Item in result)
            {

                PurchaseEntity purchaseEntity = new PurchaseEntity();
                purchaseEntity.PurchaseRequestID = Item.PurchaseRequestID;
                purchaseEntity.PurchaseMasterID = Item.PurchaseMasterID;
                purchaseEntity.SupplierName = Item.SupplierName;
                purchaseEntity.ItemMasterId = (Int32)Item.ItemMasterID;
                purchaseEntity.Item = Item.Name;
                purchaseEntity.CurrentStatus = (Int32)Item.RecordStatus;
                purchaseEntity.PurchaseOrderNo = Item.PONumber;
                purchaseEntity.InventoryTypeID = (Int32)Item.InventoryTypeID;
                purchaseEntity.Quantity = (Int32)Item.Quantity;
                purchaseEntity.EnteredDate = (DateTime)Item.EnteredDate;
                purchaseEntities.Add(purchaseEntity);
            }
            return purchaseEntities;
        }
        public List<PurchaseEntity> GetPurchaseSupplierList()
        {
            try
            {

                return (from stp in _dbContext.PurchaseSupplierDetails
                        where stp.IsActive == true
                        select new PurchaseEntity()
                        {
                            PurchaseSupplierID = stp.PurchaseSupplierID,
                            SupplierName = stp.SupplierName,
                            SupplierContactNo = stp.SupplierContactNo,
                            SupplierAddress = stp.SupplierAddress,
                            SupplierContactPerson = stp.SupplierContactPerson,
                            Product = stp.Product,
                            IsApproved = (bool)stp.IsApproved,
                            Remark = stp.Remarks,
                            ApprovedBy = stp.ApprovedBy,
                            DateOfApproval = stp.DateOfApproval,
                            IsActive = (bool)stp.IsActive,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public PurchaseEntity GetPurchaseSupplierDetails(int PurchaseSupplierID)
        {
            return _dbContext.PurchaseSupplierDetails.Where(u => u.PurchaseSupplierID == PurchaseSupplierID).Select(u => new PurchaseEntity()
            {
                PurchaseSupplierID = PurchaseSupplierID,
                SupplierName = u.SupplierName,
                SupplierContactPerson = u.SupplierContactPerson,
                SupplierAddress = u.SupplierAddress,
                SupplierContactNo = u.SupplierContactNo,
                Product = u.Product,
                ApprovedBy = u.ApprovedBy,
                IsApproved = (bool)u.IsApproved,
                Remark = u.Remarks,
                DateOfApproval = u.DateOfApproval,
                IsActive = (bool)u.IsActive
            }).FirstOrDefault();
        }
        public string AddPurchaseSupplierDetails(PurchaseEntity purchaseEntity)
        {
            try
            {
                _dbContext.PurchaseSupplierDetails.Add(new PurchaseSupplierDetail()
                {


                    PurchaseSupplierID = purchaseEntity.PurchaseSupplierID,
                    SupplierName = purchaseEntity.SupplierName,
                    SupplierContactPerson = purchaseEntity.SupplierContactPerson,
                    SupplierAddress = purchaseEntity.SupplierAddress,
                    SupplierContactNo = purchaseEntity.SupplierContactNo,
                    IsApproved = purchaseEntity.IsApproved,
                    Remarks = purchaseEntity.Remark,
                    Product = purchaseEntity.Product,
                    ApprovedBy = purchaseEntity.ApprovedBy,
                    DateOfApproval = purchaseEntity.DateOfApproval,
                    EnteredBy = purchaseEntity.EnteredBy,
                    EnteredDate = purchaseEntity.EnteredDate,
                    IsActive = purchaseEntity.IsActive
                });
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string UpdatePurchaseSupplier(PurchaseEntity purchaseEntity)
        {
            try
            {
                var purchasesupplier = _dbContext.PurchaseSupplierDetails.Find(purchaseEntity.PurchaseSupplierID);
                purchasesupplier.SupplierName = purchaseEntity.SupplierName;
                purchasesupplier.SupplierAddress = purchaseEntity.SupplierAddress;
                purchasesupplier.SupplierContactNo = purchaseEntity.SupplierContactNo;
                purchasesupplier.SupplierContactPerson = purchaseEntity.SupplierContactPerson;
                purchasesupplier.Product = purchaseEntity.Product;
                purchasesupplier.IsActive = purchaseEntity.IsActive;
                purchasesupplier.IsApproved = purchaseEntity.IsApproved;
                purchasesupplier.Remarks = purchaseEntity.Remark;
                purchasesupplier.ApprovedBy = purchaseEntity.ApprovedBy;
                purchasesupplier.DateOfApproval = purchaseEntity.DateOfApproval;
                purchasesupplier.EnteredBy = purchaseEntity.EnteredBy;
                purchasesupplier.EnteredDate = purchaseEntity.EnteredDate;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public List<PurchaseEntity> GetPurchaseRequestList()
        {
            try
            {

                return (from stp in _dbContext.PurchaseRequestDetails
                        join pmd in _dbContext.PurchaseMasterDetails on stp.PurchaseMasterID equals pmd.PurchaseMasterID
                        join iim in _dbContext.InventoryItemMasters on stp.ItemMasterID equals iim.ID
                        join psd in _dbContext.PurchaseSupplierDetails on stp.PurchaseSupplierID equals psd.PurchaseSupplierID
                        where stp.IsActive == true
                        select new PurchaseEntity()
                        {
                            PurchaseRequestID = stp.PurchaseRequestID,
                            PurchaseMasterID = (Int32)stp.PurchaseMasterID,
                            Item = iim.Name,
                            SupplierName = psd.SupplierName,
                            Brand = stp.Brand,
                            PackSize = stp.PackSize,
                            NetAmount = stp.NetAmount,
                            Quantity = (Int32)stp.Quantity,
                            Grade = stp.Grade,
                            Priority = stp.Priority,
                            Purpose = stp.Purpose,
                            TMStatus = stp.Approvests == 0 ? "Rejected" : stp.Approvests == 1 ? "Accepted" : "NA",
                            //HOStatus = stp.ApproveStatus == 7 ? "Rejected" : stp.ApproveStatus == 6 ? "Accepted" : "NA",
                            Specification = stp.Specification,
                            Remark = stp.Remark,
                            COA = stp.COA_COC,
                            Rate = stp.Rate,
                            TickIf = (bool)stp.TickIf,
                            EstimatedLagTime = stp.EstimatedLagTime,
                            IsActive = (bool)stp.IsActive,
                        }).OrderByDescending(stp => stp.PurchaseRequestID).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public bool UpdateStatus(PurchaseEntity purchaseEntity)
        {
            var comment = _dbContext.PurchaseRequestDetails.Where(x => x.PurchaseRequestID == purchaseEntity.PurchaseRequestID).FirstOrDefault();
            if (comment != null)
            {
                comment.ApprovePO = purchaseEntity.ApprovePO;


                if (comment.ApprovePO == 1)
                {
                    comment.ApproveStatus = (int?)CurrentStatusEnum.Approved;

                }
                else if (comment.ApprovePO == 0)
                {
                    comment.ApproveStatus = (int?)CurrentStatusEnum.Reject;
                }
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }
        public string UpdatePRD(PurchaseEntity purchaseEntity)
        {
            var comment = _dbContext.PurchaseRequestDetails.Where(x => x.PurchaseRequestID == purchaseEntity.PurchaseRequestID).FirstOrDefault();
            if (comment != null)
            {
                comment.TickIf = purchaseEntity.TickIf;
                comment.Rate = purchaseEntity.Rate;
                comment.Amount = purchaseEntity.Amount;
                comment.NetAmount = purchaseEntity.NetAmount;
                comment.DiscAmount = purchaseEntity.DiscAmount;
                comment.GSTAmount = purchaseEntity.GSTAmount;
                comment.GSTPercent = purchaseEntity.GSTPercent;
                comment.DiscPercent = purchaseEntity.DiscPercent;
                comment.PurchaseSupplierID = purchaseEntity.PurchaseSupplierID;
                comment.EstimatedLagTime = purchaseEntity.EstimatedLagTime;
                comment.IsIGST = purchaseEntity.IsIGST;
                _dbContext.SaveChanges();

            }
            return "success";
        }
        public string GetPONumberGlass(int PurchaseMasterID, int InventoryTypeID)
        {
            try
            {
                short InventoryTypeId = 0;
                if (InventoryTypeID == 2)
                {
                    InventoryTypeId = 1;
                }
                else
                {
                    InventoryTypeId = (short)InventoryTypeID;
                }
                var po = _dbContext.PurchaseRequestDetails.Where(e => e.PurchaseMasterID == PurchaseMasterID && e.PONumber != null && (e.InventoryTypeID == InventoryTypeId || e.CatagoryName == "Glassware")).Select(e => e.PONumber).ToList();

                string strPONo = string.Empty;
                if (po != null)
                {
                    if (po.Count > 0)
                    {
                        strPONo = po[0];
                    }
                }
                return strPONo;

            }

            catch (Exception ex)
            {
                return null;
            }
        }
        public bool UpdateComment(PurchaseEntity purchaseEntity)
        {
            var comment = _dbContext.PurchaseRequestDetails.Where(x => x.PurchaseRequestID == purchaseEntity.PurchaseRequestID).FirstOrDefault();
            if (comment != null)
            {
                comment.Approvests = purchaseEntity.Approvests;

                if (comment.Comment != "")
                {
                    comment.Comment = comment.Comment + " , " + purchaseEntity.Comment;
                }
                else
                {
                    comment.Comment = purchaseEntity.Comment;
                }

                if (comment.Approvests == 1)
                {
                    comment.CurrentStatus = (int?)CurrentStatusEnum.Approved;


                    comment.PONumber = purchaseEntity.PurchaseOrderNo;


                }
                else if (comment.Approvests == 0)
                {
                    comment.CurrentStatus = (int?)CurrentStatusEnum.Reject;
                }
                comment.Comment = purchaseEntity.Comment;


                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public string GeneratePONumber(int PurchaseRequestID)
        {
            var Purchase = _dbContext.PurchaseRequestDetails.Find(PurchaseRequestID);
            string PO = "INFO-LIMS/LAB/";
            //string Month = DateTime.Now.ToString("yyyyMM");
            long poCount = GetPOCount(Convert.ToInt32(DateTime.Now.Year), Convert.ToInt32(DateTime.Now.Month));
            long SrNumber = 0;
            string Year = GetCurrentFinancialYear();


            if (poCount == 0)
            {
                SrNumber = 1;//Doubt 
                AddPONo(1, DateTime.Now.Year, DateTime.Now.Month);
            }
            else
            {
                SrNumber = poCount + 1;
                UpdatePONo(SrNumber, DateTime.Now.Year, DateTime.Now.Month);
            }
            Purchase.PONumber = PO + Year + "/" + SrNumber;
            _dbContext.SaveChanges();
            return Purchase.PONumber;
        }
        public long GetPOCount(int Year, int Month)
        {
            var quotNumber = (from e in _dbContext.PONumberCounts
                              where e.Year == Year && e.Month == Month
                              select new PONumberEntity()
                              {
                                  POCount = e.POCount,
                              }
                  ).FirstOrDefault();
            if (quotNumber != null)
            {
                return quotNumber.POCount;
            }
            else { return 0; }
        }
        public string GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            string FinYear = null;
            int CurrentMonth = DateTime.Today.Month;
            int PreviousMonth = DateTime.Today.Month - 1;
            int NextMonth = DateTime.Today.Month + 1;
            string PreMonth = PreviousMonth.ToString();
            string NexMonth = NextMonth.ToString();
            string CurMonth = CurrentMonth.ToString();

            if (DateTime.Today.Month > 3)
                FinYear = CurYear + CurMonth;
            else
                FinYear = PreYear + CurMonth;
            return FinYear.Trim();

        }
        public long AddPONo(long POCount, int Year, int Month)
        {
            try
            {
                var quotation = new PONumberCount()
                {
                    POCount = POCount,
                    Month = Month,
                    Year = Year,
                    IsActive = true,
                    EnteredBy = 1,
                    EnteredDate = DateTime.Now

                };
                _dbContext.PONumberCounts.Add(quotation);
                _dbContext.SaveChanges();
                return quotation.PONumberId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string UpdatePONo(long POCount, int Year, int Month)
        {
            try
            {
                var quotNumber = (from e in _dbContext.PONumberCounts
                                  where e.Year == Year & e.Month == Month
                                  select new PONumberEntity()
                                  {
                                      PONumberId = e.PONumberId,
                                  }
                 ).FirstOrDefault();

                var quotationMaster = _dbContext.PONumberCounts.Find(quotNumber.PONumberId);
                quotationMaster.POCount = POCount;
                quotationMaster.Year = Year;
                quotationMaster.Month = Month;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public string GetPONumber(int PurchaseMasterID, int InventoryTypeID)
        {
            try
            {
                var po = _dbContext.PurchaseRequestDetails.Where(e => e.PurchaseMasterID == PurchaseMasterID && e.PONumber != null && e.InventoryTypeID == InventoryTypeID && e.CatagoryName != "Glassware").Select(e => e.PONumber).ToList();
                string strPONo = string.Empty;
                if (po != null)
                {
                    if (po.Count > 0)
                    {
                        strPONo = po[0];
                    }
                }
                return strPONo;
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