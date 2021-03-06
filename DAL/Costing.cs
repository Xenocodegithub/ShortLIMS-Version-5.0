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
    
    public partial class Costing
    {
        public long CostingId { get; set; }
        public Nullable<long> EnquirySampleID { get; set; }
        public Nullable<decimal> CollectionCharges { get; set; }
        public Nullable<decimal> TransportCharges { get; set; }
        public Nullable<decimal> TestingCharges { get; set; }
        public Nullable<decimal> GST { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public Nullable<decimal> TotalCharges { get; set; }
        public Nullable<long> EnquiryMasterSampleTypeId { get; set; }
    
        public virtual EnquirySampleDetail EnquirySampleDetail { get; set; }
    }
}
