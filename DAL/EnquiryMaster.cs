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
    
    public partial class EnquiryMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EnquiryMaster()
        {
            this.EnquiryDetails = new HashSet<EnquiryDetail>();
            this.EnquiryLogs = new HashSet<EnquiryLog>();
            this.EnquiryMasterSampleTypes = new HashSet<EnquiryMasterSampleType>();
            this.EnquiryStatus = new HashSet<EnquiryStatu>();
            this.Quotations = new HashSet<Quotation>();
        }
    
        public long EnquiryId { get; set; }
        public byte ModeOfCommunicationId { get; set; }
        public int CustomerMasterId { get; set; }
        public byte StatusId { get; set; }
        public int LabMasterId { get; set; }
        public string ReferenceNumber { get; set; }
        public System.DateTime EnquiryOn { get; set; }
        public string Remarks { get; set; }
        public string LegalObligations { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnquiryDetail> EnquiryDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnquiryLog> EnquiryLogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnquiryMasterSampleType> EnquiryMasterSampleTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnquiryStatu> EnquiryStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quotation> Quotations { get; set; }
        public virtual CustomerMaster CustomerMaster { get; set; }
        public virtual LabMaster LabMaster { get; set; }
        public virtual ModeOfCommunication ModeOfCommunication { get; set; }
        public virtual StatusMaster StatusMaster { get; set; }
    }
}
