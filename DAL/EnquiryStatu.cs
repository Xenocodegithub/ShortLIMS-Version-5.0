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
    
    public partial class EnquiryStatu
    {
        public int EnquiryStatusId { get; set; }
        public long EnquiryId { get; set; }
        public byte StatusId { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    
        public virtual EnquiryMaster EnquiryMaster { get; set; }
        public virtual StatusMaster StatusMaster { get; set; }
    }
}