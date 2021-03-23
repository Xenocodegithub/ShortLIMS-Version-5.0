using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Areas
{
    public class PurchaseViewModel
    {
        public int TermAndCondtId { get; set; }
        public string TermAndCondt { get; set; }
        public string CatagoryName { get; set; }
        public bool IsSelected { get; set; }
        [Display(Name = "All")]
        public bool All { get; set; }
        public int SrNo { get; set; }
        public bool NonApproved { get; set; }
        public bool Approved { get; set; }
        public string FileName { get; set; }
        [Display(Name = "Status (TM)")]
        public string RemarkIfRejectedByTM { get; set; }
        [Display(Name = "Status (HO)")]
        public string RemarkIfRejectedByHO { get; set; }
        public string PurchaseUpload { get; set; }
        [Required(ErrorMessage = "Inventory Type Required")]
        public int InventoryTypeID { get; set; }
        public int PurchaseRequestID { get; set; }
        public int PurchaseMasterID { get; set; }
        public int PurchaseRecordID { get; set; }
        [Required(ErrorMessage = "PurchaseSupplier Required")]
        public int PurchaseSupplierID { get; set; }
        [Required(ErrorMessage = "CatagoryType Required")]
        public int CatagoryMasterId { get; set; }
        public int UnitId { get; set; }
        [Required(ErrorMessage = "Item Required")]
        public int ItemMasterId { get; set; }
        public string InventoryName { get; set; }
        public int CurrentStatus { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "Inventory Type")]
        [Required(ErrorMessage = "Inventory Type Required")]
        //public InventoryType type { get; set; }
        //[Required(ErrorMessage = "CatagoryType Required")]
        public string CatagoryType { get; set; }
        public string Item { get; set; }
        [Display(Name = "Brand Received")]
        public string BrandReceived { get; set; }
        [Display(Name = "Quantity Received")]
        public string QtyReceived { get; set; }
        [Display(Name = "Delivery Time")]
        public int DeliveryTime { get; set; }
        [Display(Name = "Delivery Challan Date")]
        public Nullable<System.DateTime> DeliveryChallanDate { get; set; }
        [Display(Name = "Delivery Challan No")]
        public string DeliveryChallanNo { get; set; }
        [Display(Name = "Grade Received")]
        public string GradeReceived { get; set; }
        [Display(Name = "Condition Of Packaging")]
        public string ConditionOfPackaging { get; set; }
        [Display(Name = "Integrity Of Packaging")]
        public string IntegrityOfPackaging { get; set; }
        [Display(Name = "Bill Date")]
        public Nullable<System.DateTime> BillDate { get; set; }
        [Display(Name = "Received Date")]
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        [Required(ErrorMessage = "Bill No")]
        public string BillNo { get; set; }
        [Required(ErrorMessage = "Grade Required")]
        public string Grade { get; set; }
        [Display(Name = "Remark")]
        public string RemarkRecord { get; set; }
        public string Priority { get; set; }
        public string Specification { get; set; }
        public string Purpose { get; set; }
        public string Remark { get; set; }
        [Display(Name = "Estimated Lag Time(Days)")]
        public string EstimatedLagTime { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        [Display(Name = "Discount %")]
        public decimal? DiscPercent { get; set; }
        [Display(Name = "Discount Amount ")]
        public decimal? DiscAmount { get; set; }
        [Display(Name = "GST % ")]
        public decimal? GSTPercent { get; set; }
        public decimal? AmountAfterDisc { get; set; }
        [Display(Name = "GST Amount ")]
        public decimal? GSTAmount { get; set; }
        [Display(Name = "Net Amount ")]
        public decimal? NetAmount { get; set; }
        [Display(Name = "COA/COC Required")]
        public string COA { get; set; }
        [Display(Name = "Tick If Availabel")]
        public Nullable<bool> TickIf { get; set; }

        [Required(ErrorMessage = "Code Required")]
        public string Code { get; set; }
        public string Unit { get; set; }
        [Display(Name = "Pack Size / Capacity")]
        [Required(ErrorMessage = "PackSize Required")]
        public string PackSize { get; set; }
        [Required(ErrorMessage = "Brand Required")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Quantity Required")]
        public int Quantity { get; set; }
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
        [Display(Name = "Supplier Address")]
        public string SupplierAddress { get; set; }
        [Display(Name = "Supplier ContactPerson ")]
        public string SupplierContactPerson { get; set; }
        [Display(Name = "Supplier Contact No")]
        public string SupplierContactNo { get; set; }
        public string Product { get; set; }
        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }
        [Display(Name = "Purchase Order No")]
        public string PurchaseOrderNo { get; set; }
        [Display(Name = "Date Of Approval")]
        public Nullable<System.DateTime> DateOfApproval { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        //public List<TermsAndConditionPurchaseModel> TermsList { get; set; }
        public int Count { get; set; }
    }
}