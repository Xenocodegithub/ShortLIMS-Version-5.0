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
    
    public partial class EnquiryMasterSampleType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EnquiryMasterSampleType()
        {
            this.EnquiryDetails = new HashSet<EnquiryDetail>();
        }
    
        public long EnquiryMasterSampleTypeId { get; set; }
        public Nullable<long> EnquiryId { get; set; }
        public Nullable<int> SampleTypeProductId { get; set; }
        public string SampleNo { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnquiryDetail> EnquiryDetails { get; set; }
        public virtual EnquiryMaster EnquiryMaster { get; set; }
        public virtual SampleTypeProductMaster SampleTypeProductMaster { get; set; }
    }
}
