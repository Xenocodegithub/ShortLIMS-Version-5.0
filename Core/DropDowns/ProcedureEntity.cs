using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.DropDowns
{
    public class ProcedureEntity
    {
        public string SampleTypeProductName { get; set; }
        public int SampleTypeProductId { get; set; }
        public Nullable<int> ProcedureId { get; set; }//ProcedureMaster tbl
        public string ProcedureName { get; set; }//ProcedureMaster tbl
        public int ProductGroupId { get; set; }
    }
}
