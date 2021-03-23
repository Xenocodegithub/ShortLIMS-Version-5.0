using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Inventory;
using LIMS_DEMO.DAL.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.BAL.Inventory
{
    public class PurchaseBAL
    {
        public PurchaseBAL()
        {
          
            CoreFactory.Purchase = new PurchaseDAL();
        }

        public PurchaseEntity GetPurchaseRequestDetails(int PurchaseRequestID)
        {
             return CoreFactory.Purchase.GetPurchaseRequestDetails(PurchaseRequestID);
        }
        public long AddPurchaseMasterDetails(PurchaseEntity purchaseEntity)
        {
            return CoreFactory.Purchase.AddPurchaseMasterDetails(purchaseEntity);
        }
        public long AddPurchaseDetails(PurchaseEntity purchaseEntity)
        {
            return CoreFactory.Purchase.AddPurchaseDetails(purchaseEntity);
        }
        public long GetInventoryTypeID(int PurchaseRequestID)
        {
            return CoreFactory.Purchase.GetInventoryTypeID(PurchaseRequestID);
        }
        public bool AddPurchaseTermsAndCondition(int PurchaseRequestID, int PurchaseMasterID, int InventoryTypeID, int TermAndCondtId)
        {
            return CoreFactory.Purchase.AddPurchaseTermsAndCondition( PurchaseRequestID,  PurchaseMasterID,  InventoryTypeID,  TermAndCondtId);
        }
        public long UpdatePurchaseDetails(PurchaseEntity purchaseEntity)
        {
            return CoreFactory.Purchase.UpdatePurchaseDetails(purchaseEntity);
        }
        public bool UpdatePurchaseTermsAndCondition(int PurchaseRequestID, int PurchaseMasterID, int InventoryTypeID, int TermAndCondtId)
        {
            return CoreFactory.Purchase.UpdatePurchaseTermsAndCondition(PurchaseRequestID, PurchaseMasterID, InventoryTypeID, TermAndCondtId);
        }
        public string DeleteContract(long EnquiryDetailId)
        {
            return CoreFactory.Purchase.DeleteContract(EnquiryDetailId);
        }
        public PurchaseEntity GetPurchaseRequestDetailsPI(int PurchaseRequestID)
        {
            return CoreFactory.Purchase.GetPurchaseRequestDetailsPI(PurchaseRequestID);
        }
        public IList<PurchaseEntity> GetTML()
        {
            return CoreFactory.Purchase.GetTML();
        }
        public IList<PurchaseEntity> GetPOList()
        {
            return CoreFactory.Purchase.GetPOList();
        }
        public IList<PurchaseEntity> GetPurchaseRecordFirstList()
        {
            return CoreFactory.Purchase.GetPurchaseRecordFirstList();
        }
        public IList<PurchaseEntity> GetPIL()
        {
            return CoreFactory.Purchase.GetPIL();
        }
        public IList<PurchaseEntity> GetInchargeList()
        {
            return CoreFactory.Purchase.GetInchargeList();
        }
        public List<PurchaseEntity> GetTermsAndConditionPurchase(int InventoryTypeID)
        {
            return CoreFactory.Purchase.GetTermsAndConditionPurchase(InventoryTypeID);
        }
        public List<PurchaseEntity> GetPurchaseTNCDetails(int PurchaseRequestID)
        {
            return CoreFactory.Purchase.GetPurchaseTNCDetails(PurchaseRequestID);
        }
        public PurchaseEntity GetPurchaseRecordDetails(int PurchaseRecordID)
        {
            return CoreFactory.Purchase.GetPurchaseRecordDetails( PurchaseRecordID);
        }
        public PurchaseEntity GetPurchaseRequestDetails2(int PurchaseRequestID)
        {
            return CoreFactory.Purchase.GetPurchaseRequestDetails2(PurchaseRequestID);
        }
        public string AddPurchaseRecordDetails(PurchaseEntity purchaseEntity)
        {
            return CoreFactory.Purchase.AddPurchaseRecordDetails(purchaseEntity);
        }
        public string UpdatePurchaseRecord(PurchaseEntity purchaseEntity)
        {
            return CoreFactory.Purchase.UpdatePurchaseRecord(purchaseEntity);
        }
        public List<PurchaseEntity> GetPurchaseRecordList()
        {
            return CoreFactory.Purchase.GetPurchaseRecordList();
        }
        public IList<PurchaseEntity> GetPurchaseRecordSecondList(int PurchaseMasterID, int InventoryTypeID, string PurchaseOrderNo)
        {
            return CoreFactory.Purchase.GetPurchaseRecordSecondList( PurchaseMasterID,  InventoryTypeID, PurchaseOrderNo);
        }
        public List<PurchaseEntity> GetPurchaseSupplierList()
        {
            return CoreFactory.Purchase.GetPurchaseSupplierList();
        }
        public PurchaseEntity GetPurchaseSupplierDetails(int PurchaseSupplierID)
        {
            return CoreFactory.Purchase.GetPurchaseSupplierDetails(PurchaseSupplierID);
        }
        public string AddPurchaseSupplierDetails(PurchaseEntity purchaseEntity)
        {
            return CoreFactory.Purchase.AddPurchaseSupplierDetails(purchaseEntity);
        }
        public string UpdatePurchaseSupplier(PurchaseEntity purchaseEntity)
        {
            return CoreFactory.Purchase.UpdatePurchaseSupplier(purchaseEntity);
        }
        public List<PurchaseEntity> GetPurchaseRequestList()
        {
            return CoreFactory.Purchase.GetPurchaseRequestList();
        }
        public bool UpdateStatus(PurchaseEntity purchaseEntity)
        {
            return CoreFactory.Purchase.UpdateStatus(purchaseEntity);
        }
        public string UpdatePRD(PurchaseEntity purchaseEntity)
        {
            return CoreFactory.Purchase.UpdatePRD(purchaseEntity);
        }
        public string GetPONumberGlass(int PurchaseMasterID, int InventoryTypeID)
        {
            return CoreFactory.Purchase.GetPONumberGlass( PurchaseMasterID, InventoryTypeID);
        }
        public bool UpdateComment(PurchaseEntity purchaseEntity)
        {
            return CoreFactory.Purchase.UpdateComment(purchaseEntity);
        }
        public string GeneratePONumber(int PurchaseRequestID)
        {
            return CoreFactory.Purchase.GeneratePONumber(PurchaseRequestID);
        }
        public string GetPONumber(int PurchaseMasterID, int InventoryTypeID)
        {
            return CoreFactory.Purchase.GetPONumber( PurchaseMasterID, InventoryTypeID);
        }
    }
    
}