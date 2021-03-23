using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Inventory
{
    public class PurchaseEntity
    {
        public string InventoryBasicItemDetailsNumber { get; set; }
        public string ItemName { get; set; }
        public Nullable<DateTime> NextCalibrationDate { get; set; }
        public string SerialNumber { get; set; }//forMaintainance
        public int TermAndCondtId { get; set; }
        public int Count { get; set; }
        public string TMStatus { get; set; }
        public string CategoryName { get; set; }
        public string HOStatus { get; set; }
        public string TermAndCondt { get; set; }
        public string strPurchaseRequestId { get; set; }
        public bool IsSelected { get; set; }
        public int PurchaseRequestID { get; set; }
        public int InventoryTypeID { get; set; }
        public int PurchaseMasterID { get; set; }
        public int PurchaseRecordID { get; set; }
        public int PurchaseSupplierID { get; set; }
        public Nullable<int> PurchaseSupplierID2 { get; set; }
        public int ItemMasterId { get; set; }
        public int CatagoryMasterId { get; set; }
        public decimal? GSTPercent { get; set; }
        public decimal? DiscPercent { get; set; }
        public Nullable<int> UnitId { get; set; }
        public string PackSize { get; set; }
        public string FileName { get; set; }
        public string PurchaseUpload { get; set; }
        public string Item { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public string RemarkRecord { get; set; }
        public string BrandReceived { get; set; }
        public string QtyReceived { get; set; }
        public int DeliveryTime { get; set; }
        public int CurrentStatus { get; set; }
        public Nullable<System.DateTime> DeliveryChallanDate { get; set; }
        public string DeliveryChallanNo { get; set; }
        public string GradeReceived { get; set; }
        public string ConditionOfPackaging { get; set; }
        public string IntegrityOfPackaging { get; set; }
        public Nullable<System.DateTime> BillDate { get; set; }
        public string BillNo { get; set; }
        public string Grade { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public string Priority { get; set; }
        public string Specification { get; set; }
        public string Purpose { get; set; }
        public string Remark { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string EstimatedLagTime { get; set; }
        public decimal? Rate { get; set; }
        public string Comment { get; set; }
        public Nullable<int> Approvests { get; set; }
        public Nullable<int> ApprovePO { get; set; }
        public string COA { get; set; }
        public Nullable<bool> TickIf { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierContactPerson { get; set; }
        public string SupplierContactNo { get; set; }
        public string Product { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> DateOfApproval { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
        public bool IsIGST { get; set; } //1means IGST 0means CGST+SGST
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? GSTAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? Amount { get; set; }
    }
}