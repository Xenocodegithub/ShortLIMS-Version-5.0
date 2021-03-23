using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.DropDowns
{
    public class SampleDeviceEntity
    {

        public string SampleTypeProductName { get; set; }
        public int SampleTypeProductId { get; set; }
        public Nullable<int> SampleDeviceId { get; set; }//SampleDeviceMaster tbl
        public string SampleDevice { get; set; }//SampleDeviceMaster tbl
        public int ProductGroupId { get; set; }
    }
}
