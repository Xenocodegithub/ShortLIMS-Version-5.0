using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using LIMS_DEMO.Core.DropDowns;

namespace LIMS_DEMO.Areas.Enquiry.Models
{
    public class EnquiryParameterModel
    {
        public bool Infield { get; set; }
        public string Remarks { get; set; }
        public bool IsSelected { get; set; }
        public long? EnquiryParameterDetailID { get; set; }
        public long? EnquirySampleID { get; set; }
        public long? ParameterMasterId { get; set; }
        public long ParameterGroupId { get; set; }
        public int? ParameterMappingId { get; set; }
        public string ParameterName { get; set; }
        public byte? DisciplineId { get; set; }
        public string Discipline { get; set; }
        public Nullable<int> UnitId { get; set; }
        public Nullable<decimal> LowerLimit { get; set; }
        public Nullable<decimal> UpperLimit { get; set; }

        public int ProductGroupId { get; set; }
        public int MatrixId { get; set; }
        public int SubGroupId { get; set; }
        public Nullable<decimal> Charges { get; set; }
        public Nullable<int> TestMethodId { get; set; }
        public Nullable<int> LabMasterId { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }

        public string PCBLimit { get; set; }
        public List<TestMethodEntity> TestMethods { get; set; }
        public List<UnitEntity> Units { get; set; }

    }
}