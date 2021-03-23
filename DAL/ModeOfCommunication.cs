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
    
    public partial class ModeOfCommunication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ModeOfCommunication()
        {
            this.EnquiryLogs = new HashSet<EnquiryLog>();
            this.EnquiryMasters = new HashSet<EnquiryMaster>();
        }
    
        public byte ModeOfCommunicationId { get; set; }
        public string CommunicationMode { get; set; }
        public string Abbriviation { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnquiryLog> EnquiryLogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnquiryMaster> EnquiryMasters { get; set; }
    }
}
