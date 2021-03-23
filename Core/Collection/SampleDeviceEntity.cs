using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Collection
{
    public class SampleDeviceEntity
    {
        public int SampleCollectionDevicesId { get; set; }
        public long SampleCollectionId { get; set; }
        public int SampleDeviceId { get; set; }
        public string SampleDevice { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
    }
}
