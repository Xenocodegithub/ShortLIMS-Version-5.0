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
    
    public partial class UserMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserMaster()
        {
            this.SampleParameterFiles = new HashSet<SampleParameterFile>();
            this.SampleParameterPlannings = new HashSet<SampleParameterPlanning>();
            this.SampleParameterPlannings1 = new HashSet<SampleParameterPlanning>();
            this.UserDetails = new HashSet<UserDetail>();
            this.UserLabs = new HashSet<UserLab>();
            this.UserRoles = new HashSet<UserRole>();
            this.WorkOrders = new HashSet<WorkOrder>();
        }
    
        public int UserMasterID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VerificationCode { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<System.DateTime> EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public Nullable<byte> DisciplineId { get; set; }
        public Nullable<bool> ResetActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SampleParameterFile> SampleParameterFiles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SampleParameterPlanning> SampleParameterPlannings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SampleParameterPlanning> SampleParameterPlannings1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserDetail> UserDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserLab> UserLabs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
        public virtual DisciplineMaster DisciplineMaster { get; set; }
        public virtual DisciplineMaster DisciplineMaster1 { get; set; }
        public virtual DisciplineMaster DisciplineMaster2 { get; set; }
        public virtual DisciplineMaster DisciplineMaster3 { get; set; }
        public virtual DisciplineMaster DisciplineMaster4 { get; set; }
        public virtual DisciplineMaster DisciplineMaster5 { get; set; }
        public virtual DisciplineMaster DisciplineMaster6 { get; set; }
        public virtual DisciplineMaster DisciplineMaster7 { get; set; }
        public virtual DisciplineMaster DisciplineMaster8 { get; set; }
        public virtual DisciplineMaster DisciplineMaster9 { get; set; }
        public virtual DisciplineMaster DisciplineMaster10 { get; set; }
        public virtual DisciplineMaster DisciplineMaster11 { get; set; }
        public virtual DisciplineMaster DisciplineMaster12 { get; set; }
        public virtual DisciplineMaster DisciplineMaster13 { get; set; }
        public virtual DisciplineMaster DisciplineMaster14 { get; set; }
        public virtual DisciplineMaster DisciplineMaster15 { get; set; }
        public virtual DisciplineMaster DisciplineMaster16 { get; set; }
        public virtual DisciplineMaster DisciplineMaster17 { get; set; }
        public virtual DisciplineMaster DisciplineMaster18 { get; set; }
        public virtual DisciplineMaster DisciplineMaster19 { get; set; }
        public virtual DisciplineMaster DisciplineMaster20 { get; set; }
        public virtual DisciplineMaster DisciplineMaster21 { get; set; }
        public virtual DisciplineMaster DisciplineMaster22 { get; set; }
        public virtual DisciplineMaster DisciplineMaster23 { get; set; }
        public virtual DisciplineMaster DisciplineMaster24 { get; set; }
        public virtual DisciplineMaster DisciplineMaster25 { get; set; }
        public virtual DisciplineMaster DisciplineMaster26 { get; set; }
        public virtual DisciplineMaster DisciplineMaster27 { get; set; }
        public virtual DisciplineMaster DisciplineMaster28 { get; set; }
        public virtual DisciplineMaster DisciplineMaster29 { get; set; }
        public virtual DisciplineMaster DisciplineMaster30 { get; set; }
        public virtual DisciplineMaster DisciplineMaster31 { get; set; }
        public virtual DisciplineMaster DisciplineMaster32 { get; set; }
        public virtual DisciplineMaster DisciplineMaster33 { get; set; }
        public virtual DisciplineMaster DisciplineMaster34 { get; set; }
        public virtual DisciplineMaster DisciplineMaster35 { get; set; }
        public virtual DisciplineMaster DisciplineMaster36 { get; set; }
        public virtual DisciplineMaster DisciplineMaster37 { get; set; }
        public virtual DisciplineMaster DisciplineMaster38 { get; set; }
        public virtual DisciplineMaster DisciplineMaster39 { get; set; }
        public virtual DisciplineMaster DisciplineMaster40 { get; set; }
        public virtual DisciplineMaster DisciplineMaster41 { get; set; }
        public virtual DisciplineMaster DisciplineMaster42 { get; set; }
        public virtual DisciplineMaster DisciplineMaster43 { get; set; }
        public virtual DisciplineMaster DisciplineMaster44 { get; set; }
        public virtual DisciplineMaster DisciplineMaster45 { get; set; }
        public virtual DisciplineMaster DisciplineMaster46 { get; set; }
        public virtual DisciplineMaster DisciplineMaster47 { get; set; }
    }
}
