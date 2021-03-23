using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.DropDowns
{
    public class SampleDescriptionEntity
    {
        public int SampleDescriptionId { get; set; }
        public int ProductGroupId { get; set; }
        public int SubGroupId { get; set; }
        public string SampleDescription { get; set; }
        public string MatrixCode { get; set; }
    }
}
