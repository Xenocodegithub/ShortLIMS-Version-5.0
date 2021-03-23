using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Configuration
{
    public class ParameterMasterEntity
    {
        public long ParameterMasterId { get; set; }

        public string Parameter { get; set; }
        public int ParameterMappingId { get; set; }
        public Nullable<decimal> Charges { get; set; }
        public Nullable<int> SampleTypeProductId { get; set; }
        public int ParameterGroupId { get; set; }
        public Nullable<int> UnitId { get; set; }
        public string UnitName { get; set; }
        public Nullable<int> TestMethodId { get; set; }
        public string TestMethodName { get; set; }
        public string SampleTypeProductName { get; set; }
        public Nullable<bool> IsNABLAccredited { get; set; }
        public string LOD { get; set; }
        public string MaxRange { get; set; }
        public string PermissibleMin { get; set; }
        public string PermissibleMax { get; set; }
        public string RegulatoryMin { get; set; }
        public string RegulatoryMax { get; set; }
        public Nullable<bool> IsIndustrySpecified { get; set; }
        public Nullable<bool> IsField { get; set; }

        public int ProductGroupId { get; set; }
        public string ProductGroupName { get; set; }
        public int SubGroupId { get; set; }
        public string SubGroupName { get; set; }
        public Nullable<int> MatrixId { get; set; }
        public string MatrixName { get; set; }
        public Nullable<byte> DisciplineId { get; set; }
        public string DisciplineName { get; set; }

        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}