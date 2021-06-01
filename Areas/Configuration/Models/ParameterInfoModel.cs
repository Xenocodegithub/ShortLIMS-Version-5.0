using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.Configuration.Models
{
    public class ParameterInfoModel
    {
        public long ParameterGroupId { get; set; }
        public int ProductGroupId { get; set; }
        public int SubGroupId { get; set; }
        public int SampleTypeProductId { get; set; }
        public long ParameterMasterId { get; set; }
        public string ParameterName { get; set; }

        public short DisciplineId { get; set; }
        public int TestMethodId { get; set; }
        public int InsertedBy { get; set; }
        public string DisciplineName { get; set; }

        public bool IsActive { get; set; }
        public int MatrixId { get; set; }
        public bool IsSetPCBLimit { get; set; }
        public string MatrixName { get; set; }
        public int UnitId { get; set; }
        public bool IsNABLAccredited { get; set; }
        public Nullable<bool> IsField { get; set; }
        public string LOD { get; set; }
        [Display(Name = "Max Range")]
        public string MaxRange { get; set; }
        [Display(Name = "Permissible Min")]
        public string PermissibleMin { get; set; }
        [Display(Name = "Permissible Max")]
        public string PermissibleMax { get; set; }
        [Display(Name = "Regulatory Min")]
        public string RegulatoryMin { get; set; }
        [Display(Name = "Regulatory Max")]
        public string RegulatoryMax { get; set; }
        [Display(Name = "Is Industry Specified")]
        public Nullable<bool> IsIndustrySpecified { get; set; }

        [Required(ErrorMessage = "ProductGroup Name field is Required")]

        [Display(Name = "ProductGroup Name")]
        public string ProductGroupName { get; set; }
        [Required(ErrorMessage = "SubGroup Name field is Required")]

        [Display(Name = "SubGroup Name")]
        public string SubGroupName { get; set; }
        [Required(ErrorMessage = "Matrix Name field is Required")]
        [Display(Name = "All")]
        public bool All { get; set; }

    }
}