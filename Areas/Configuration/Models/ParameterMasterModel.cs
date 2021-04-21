using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class ParameterMasterModel
    {
      
        [Required(ErrorMessage = "Parameter field is Required")]
        public long ParameterMasterId { get; set; }

        [Display(Name = "ParameterName")]
        public string Parameter { get; set; }
        [Display(Name = "SampleTypeProductName")]
        public string SampleTypeProductName { get; set; }
        public Nullable<decimal> Charges { get; set; }
        public int ParameterMappingId { get; set; }
        public int ParameterGroupId { get; set; }
        public string TestMethod { get; set; }
        public int SampleTypeProductId { get; set; }
        [Required(ErrorMessage = "Please Select Unit")]
        public Nullable<int> UnitId { get; set; }
        //public List<int> lstUnitId { get; set; }
        public string Unit { get; set; }
        public int TestMethodId { get; set; }
        public Nullable<bool> IsNABLAccredited { get; set; }
        public Nullable<bool> IsField { get; set; }
        public string LOD { get; set; }
        [Display(Name = "Max Range")]
        public string MaxRange { get; set; }
        public string PermissibleMin { get; set; }
        public string PermissibleMax { get; set; }
        public string RegulatoryMin { get; set; }
        public string RegulatoryMax { get; set; }
        public Nullable<bool> IsIndustrySpecified { get; set; }
        public int ProductGroupId { get; set; }
        public string ProductGroupName { get; set; }
        public int SubGroupId { get; set; }
        public string SubGroupName { get; set; }
        public int MatrixId { get; set; }
        public string MatrixName { get; set; }
        public byte DisciplineId { get; set; }
        public string DisciplineName { get; set; }
        public bool IsActive { get; set; }
        public bool All { get; set; }

    }
}