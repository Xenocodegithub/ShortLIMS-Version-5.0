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
    
    public partial class FieldAmbientAirMonitoring
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FieldAmbientAirMonitoring()
        {
            this.FieldAmbientAirGasesSamplings = new HashSet<FieldAmbientAirGasesSampling>();
            this.FieldAmbientAirMatterSizes = new HashSet<FieldAmbientAirMatterSize>();
        }
    
        public int FieldId { get; set; }
        public Nullable<long> EnquiryId { get; set; }
        public long SampleCollectionId { get; set; }
        public Nullable<byte> StatusId { get; set; }
        public string InstrumentId { get; set; }
        public string SamplingLocation { get; set; }
        public Nullable<int> SamplingDuration { get; set; }
        public string SamplingProcedure { get; set; }
        public Nullable<int> WindVelocity { get; set; }
        public string WindDirection_ { get; set; }
        public string RelativeHumidity { get; set; }
        public string Temperature { get; set; }
        public Nullable<int> AverageWindVelocity { get; set; }
        public string SampleReceivedBy { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public string RelativeHumidity24Hr { get; set; }
        public string Temperature24Hr { get; set; }
        public Nullable<int> FilterPaperNo2 { get; set; }
        public Nullable<int> TotalVolumeAirPassed_m3 { get; set; }
        public Nullable<int> TotalVolAirPassed_L { get; set; }
        public Nullable<int> AvgFlowRate { get; set; }
        public Nullable<int> MatterSamplingDuration { get; set; }
        public Nullable<int> EndTime { get; set; }
        public Nullable<int> StartTime { get; set; }
        public Nullable<int> Sampling_Duration { get; set; }
        public Nullable<int> FlowRate_Avg { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldAmbientAirGasesSampling> FieldAmbientAirGasesSamplings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldAmbientAirMatterSize> FieldAmbientAirMatterSizes { get; set; }
        public virtual SampleCollection SampleCollection { get; set; }
        public virtual StatusMaster StatusMaster { get; set; }
    }
}
