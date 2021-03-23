using LIMS_DEMO.Core.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Repository
{
    public interface IPurchase : IDisposable
    {
        PurchaseEntity GetPurchaseRequestDetails(int PurchaseRequestID);
        long AddPurchaseMasterDetails(PurchaseEntity purchaseEntity);
        long AddPurchaseDetails(PurchaseEntity purchaseEntity);
        long GetInventoryTypeID(int PurchaseRequestID);
        bool AddPurchaseTermsAndCondition(int PurchaseRequestID, int PurchaseMasterID, int InventoryTypeID, int TermAndCondtId);
        bool UpdatePurchaseTermsAndCondition(int PurchaseRequestID, int PurchaseMasterID, int InventoryTypeID, int TermAndCondtId);
        long UpdatePurchaseDetails(PurchaseEntity purchaseEntity);
        string DeleteContract(long EnquiryDetailId);
        PurchaseEntity GetPurchaseRequestDetailsPI(int PurchaseRequestID);
        IList<PurchaseEntity> GetPurchaseRecordFirstList();
        IList<PurchaseEntity> GetPOList();
        IList<PurchaseEntity> GetTML();
        IList<PurchaseEntity> GetPIL();
        IList<PurchaseEntity> GetInchargeList();
        List<PurchaseEntity> GetTermsAndConditionPurchase(int InventoryTypeID);
        List<PurchaseEntity> GetPurchaseTNCDetails(int PurchaseRequestID);
        PurchaseEntity GetPurchaseRecordDetails(int PurchaseRecordID);
        PurchaseEntity GetPurchaseRequestDetails2(int PurchaseRequestID);
        string AddPurchaseRecordDetails(PurchaseEntity purchaseEntity);
        string UpdatePurchaseRecord(PurchaseEntity purchaseEntity);
        List<PurchaseEntity> GetPurchaseRecordList();
        IList<PurchaseEntity> GetPurchaseRecordSecondList(int PurchaseMasterID, int InventoryTypeID, string PurchaseOrderNo);
        List<PurchaseEntity> GetPurchaseSupplierList();
        PurchaseEntity GetPurchaseSupplierDetails(int PurchaseSupplierID);
        string AddPurchaseSupplierDetails(PurchaseEntity purchaseEntity);
        string UpdatePurchaseSupplier(PurchaseEntity purchaseEntity);
        List<PurchaseEntity> GetPurchaseRequestList();
        bool UpdateStatus(PurchaseEntity purchaseEntity);
        string UpdatePRD(PurchaseEntity purchaseEntity);
        string GetPONumberGlass(int PurchaseMasterID, int InventoryTypeID);
        bool UpdateComment(PurchaseEntity purchaseEntity);
        string GeneratePONumber(int PurchaseRequestID);
        string GetPONumber(int PurchaseMasterID, int InventoryTypeID);
    }
}
