using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Inventory.Models
{
    public class InventoryBasicDetailModel
    {
        public int ID { get; set; }
        public int InventoryBasicDetailsID { get; set; }
        public Nullable<System.DateTime> DOE { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> ItemID { get; set; }
        [Required(ErrorMessage = "Please Select Item Name")]
        public string ItemName { get; set; }
        [Required(ErrorMessage = "Please Select Category Name")]
        public string CategoryName { get; set; }

        public Nullable<int> TypeID { get; set; }
        [Required(ErrorMessage = "Please Select Inventory Type")]
        public Nullable<int> InventoryTypeID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> CatagoryMasterID { get; set; }
        [Required(ErrorMessage = "Please Select Unit")]
        public Nullable<int> UnitID { get; set; }
        public string Unit { get; set; }
        [Required(ErrorMessage = "Please Enter Quantity")]
        public string QuantityType { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        [Required(ErrorMessage = "Please Enter Warranty")]
        public Nullable<int> Warranty { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<decimal> TotalQuantity { get; set; }
        public string InventoryBasicDetailsNumber { get; set; }
        [Required(ErrorMessage = "Please Enter Manufacturer")]
        public string Manufacturer { get; set; }
        public Nullable<System.DateTime> DOM { get; set; }
        [Required(ErrorMessage = "Please Select Date of Expiry")]
        public Nullable<System.DateTime> DOExpiry { get; set; }
        [Required(ErrorMessage = "Please Enter GradeReceived")]
        public string GradeReceived { get; set; }
        public string DeliveryTime { get; set; }
        public string ConditionOfPackaging { get; set; }
        public string IntegrityOfPackaging { get; set; }
        public string Remark { get; set; }
        [Required(ErrorMessage = "Please Enter BrandReceived")]
        public string BrandReceived { get; set; }
        public Nullable<int> PurchaseRequestID { get; set; }
        public string CertifiedConcentration { get; set; }
        public string Praceability { get; set; }
        public int InventoryBasicItemDetailsID { get; set; }
        public string PackSize { get; set; }
        public decimal QuantityLeft { get; set; }
        public string Brand { get; set; }
        public string Grade { get; set; }
        public List<InventoryBasicDetailModel> ListInventoryBasicItemDetail { get; set; }
        public string BatchNumber { get; set; }
        public string ModelNumber { get; set; }
        public string BarcodeNumber { get; set; }
        public string Description { get; set; }
        public bool IsAssigned { get; set; }
        public string InventoryBasicItemDetailsNumber { get; set; }
        public string LOTNo { get; set; }
        [Required(ErrorMessage = "Please Select Serial Number")]
        public string SRNO { get; set; }
        public int BasicDetailsID { set; get; }
        [Required(ErrorMessage = "Please Enter Storage Location")]
        public string StorageLocation { get; set; }
    }
}