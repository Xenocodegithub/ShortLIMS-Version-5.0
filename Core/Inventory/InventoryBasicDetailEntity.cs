using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Inventory
{
    public class InventoryBasicDetailEntity
    {
        public int ID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public int RowNumber { get; set; }
        public Nullable<int> TypeID { get; set; }
        public Nullable<int> InventoryTypeID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> CatagoryMasterID { get; set; }
        
        public Nullable<int> UnitID { get; set; }
        public string QuantityType { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<int> Warranty { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<decimal> TotalQuantity { get; set; }
        public Nullable<decimal> TQuantity { get; set; }
        public string InventoryBasicDetailsNumber { get; set; }
        public string Manufacturer { get; set; }
        public Nullable<System.DateTime> DOM { get; set; }
        public Nullable<System.DateTime> DOE { get; set; }
        public Nullable<System.DateTime> PurchaseOrderDate { get; set; }
        public string GradeReceived { get; set; }
        public string DeliveryTime { get; set; }
        public string ConditionOfPackaging { get; set; }
        public string IntegrityOfPackaging { get; set; }
        public string Remark { get; set; }
        public string BrandReceived { get; set; }
        public Nullable<int> PurchaseRequestID { get; set; }
        public string CertifiedConcentration { get; set; }
        public string Praceability { get; set; }
        public string ItemName { get; set; }
        public string Code { get; set; }
        public Nullable<decimal> MinimumStock { get; set; }
        public Nullable<decimal> AvailableStock { get; set; }
        public string CategoryName { get; set; }
        public string InventoryTypeName { get; set; }
        public string Capacity { get; set; }
        public int CapacityId { get; set; }
        public string PackSize { get; set; }
        public decimal QuantityLeft { get; set; }
        public string Brand { get; set; }
        public string Grade { get; set; }
        public string Unit { get; set; }
        public Nullable<int> Purchasemasterid { get; set; }
        public int PurchaseMasterID { get; set; }
        public string Specification { get; set; }
        public string SupplierName { get; set; }
        public string PurchaseOrderNo { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string StorageLocation { get; set; }

        // inventory basic deatil Entity

        public int InventoryBasicDetailsID { get; set; }
        public string BatchNumber { get; set; }
        public string ModelNumber { get; set; }
        public string BarcodeNumber { get; set; }
        public string Description { get; set; }
        public bool IsAssigned { get; set; }
        public string InventoryBasicItemDetailsNumber { get; set; }
        public string LOTNo { get; set; }
        public string SRNO { get; set; }
        public int InventoryBasicItemDetailsID { set; get; }
    }
}