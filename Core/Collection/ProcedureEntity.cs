using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Collection
{
    public class ProcedureEntity
    {
        public int SampleCollectionProcedureId { get; set; }
        public long SampleCollectionId { get; set; }
        public int ProcedureId { get; set; }
        public string Procedure { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
    }
}
