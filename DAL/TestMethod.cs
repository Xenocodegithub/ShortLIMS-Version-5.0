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
    
    public partial class TestMethod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TestMethod()
        {
            this.EnquiryParameterDetails = new HashSet<EnquiryParameterDetail>();
            this.ParameterFormulas = new HashSet<ParameterFormula>();
            this.ParameterMappings = new HashSet<ParameterMapping>();
        }
    
        public int TestMethodId { get; set; }
        public string TestMethod1 { get; set; }
        public string TestStd { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnquiryParameterDetail> EnquiryParameterDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParameterFormula> ParameterFormulas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParameterMapping> ParameterMappings { get; set; }
    }
}
