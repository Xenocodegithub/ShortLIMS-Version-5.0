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
    
    public partial class FDStackEmission_Details
    {
        public long FDStackEmission_DetailsId { get; set; }
        public long FDStackEmissionId { get; set; }
        public Nullable<decimal> TraversePointdistance { get; set; }
        public Nullable<decimal> DifferentialPressure { get; set; }
        public Nullable<decimal> Velocity_V { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    
        public virtual FDStackEmission FDStackEmission { get; set; }
    }
}
