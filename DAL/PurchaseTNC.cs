//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LIMS_DEMO.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseTNC
    {
        public int PurchaseTNCId { get; set; }
        public Nullable<short> InventoryTypeId { get; set; }
        public Nullable<int> PurchaseMasterId { get; set; }
        public Nullable<int> PurchaseRequestId { get; set; }
        public Nullable<int> TermAndCondtId { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    
        public virtual InventoryTypeMaster InventoryTypeMaster { get; set; }
        public virtual PurchaseMasterDetail PurchaseMasterDetail { get; set; }
        public virtual PurchaseRequestDetail PurchaseRequestDetail { get; set; }
        public virtual TermsAndConditionsPurchase TermsAndConditionsPurchase { get; set; }
    }
}
