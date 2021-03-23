using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Arrival
{
    public class PlannerByDisciplineEntity
    {
        public long EnquirySampleID { get; set; }
        public string Parameters { get; set; }
        public int PlannerByDisciplineId { get; set; }
        public long SampleCollectionId { get; set; }
        public Nullable<int> PlannerId { get; set; }
        public Nullable<int> DisciplineId { get; set; }
        public Nullable<int> ApproverId { get; set; }
        public string ParameterName { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
       
    }
}
