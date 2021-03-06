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
    
    public partial class EnquirySampleDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EnquirySampleDetail()
        {
            this.Costings = new HashSet<Costing>();
            this.EnquiryParameterDetails = new HashSet<EnquiryParameterDetail>();
            this.LocationSampleCollections = new HashSet<LocationSampleCollection>();
            this.SampleLocations = new HashSet<SampleLocation>();
            this.SampleCollections = new HashSet<SampleCollection>();
            this.SampleCollections1 = new HashSet<SampleCollection>();
        }
    
        public long EnquirySampleID { get; set; }
        public long EnquiryDetailId { get; set; }
        public Nullable<byte> FrequencyMasterId { get; set; }
        public string SampleName { get; set; }
        public Nullable<int> SampleCollectedBy { get; set; }
        public Nullable<int> NoOfSample { get; set; }
        public Nullable<bool> IsReturn { get; set; }
        public Nullable<bool> IsUrgent { get; set; }
        public Nullable<decimal> TotalCharges { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public string Location { get; set; }
        public Nullable<int> SampleTypeProductId { get; set; }
        public string DisplaySampleName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Costing> Costings { get; set; }
        public virtual EnquiryDetail EnquiryDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnquiryParameterDetail> EnquiryParameterDetails { get; set; }
        public virtual FrequencyMaster FrequencyMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocationSampleCollection> LocationSampleCollections { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SampleLocation> SampleLocations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SampleCollection> SampleCollections { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SampleCollection> SampleCollections1 { get; set; }
    }
}
