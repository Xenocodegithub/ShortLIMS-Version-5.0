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
    
    public partial class StatusMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StatusMaster()
        {
            this.EnquiryStatus = new HashSet<EnquiryStatu>();
            this.FDStackEmissions = new HashSet<FDStackEmission>();
            this.FieldAmbientAirMonitorings = new HashSet<FieldAmbientAirMonitoring>();
            this.FieldBuildingMaterials = new HashSet<FieldBuildingMaterial>();
            this.FieldCoalCokeSolidFuels = new HashSet<FieldCoalCokeSolidFuel>();
            this.FieldFoodAndAgriCultures = new HashSet<FieldFoodAndAgriCulture>();
            this.FieldMicrobiologicalMonitoringOfAirs = new HashSet<FieldMicrobiologicalMonitoringOfAir>();
            this.FieldNoiseLevelMonitorings = new HashSet<FieldNoiseLevelMonitoring>();
            this.FieldSolidHazardousWasteSoilOils = new HashSet<FieldSolidHazardousWasteSoilOil>();
            this.FieldWasteWaters = new HashSet<FieldWasteWater>();
            this.FieldWorkplaceEnvironmentAndFugitiveEmissions = new HashSet<FieldWorkplaceEnvironmentAndFugitiveEmission>();
            this.SampleCollections = new HashSet<SampleCollection>();
            this.SampleCollections1 = new HashSet<SampleCollection>();
            this.EnquiryMasters = new HashSet<EnquiryMaster>();
        }
    
        public byte StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Module { get; set; }
        public Nullable<int> Hierarchy { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnquiryStatu> EnquiryStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FDStackEmission> FDStackEmissions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldAmbientAirMonitoring> FieldAmbientAirMonitorings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldBuildingMaterial> FieldBuildingMaterials { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldCoalCokeSolidFuel> FieldCoalCokeSolidFuels { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldFoodAndAgriCulture> FieldFoodAndAgriCultures { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldMicrobiologicalMonitoringOfAir> FieldMicrobiologicalMonitoringOfAirs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldNoiseLevelMonitoring> FieldNoiseLevelMonitorings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldSolidHazardousWasteSoilOil> FieldSolidHazardousWasteSoilOils { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldWasteWater> FieldWasteWaters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FieldWorkplaceEnvironmentAndFugitiveEmission> FieldWorkplaceEnvironmentAndFugitiveEmissions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SampleCollection> SampleCollections { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SampleCollection> SampleCollections1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnquiryMaster> EnquiryMasters { get; set; }
    }
}
