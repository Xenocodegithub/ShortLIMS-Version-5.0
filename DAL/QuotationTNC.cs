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
    
    public partial class QuotationTNC
    {
        public int QuotationTNCId { get; set; }
        public Nullable<long> QuotationId { get; set; }
        public int TermAndCondtId { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
    
        public virtual TermsAndCondition TermsAndCondition { get; set; }
    }
}
