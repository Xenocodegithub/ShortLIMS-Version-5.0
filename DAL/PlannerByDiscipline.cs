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
    
    public partial class PlannerByDiscipline
    {
        public int PlannerByDisciplineId { get; set; }
        public long SampleCollectionId { get; set; }
        public Nullable<int> PlannerId { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public Nullable<int> DisciplineId { get; set; }
        public string ParameterName { get; set; }
        public Nullable<int> ParameterGroupId { get; set; }
        public Nullable<long> ParameterMasterId { get; set; }
    
        public virtual SampleCollection SampleCollection { get; set; }
    }
}