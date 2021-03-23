using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Enquiry
{
   public class SampleLocationEntity
    {
        public int SampleLocationId { get; set; }
        public string Location { get; set; }
        public int SrNo { get; set; }
        public long EnquiryId { get; set; }//EnquiryMaster tbl
        public Nullable<long> EnquirySampleID { get; set; }
    }
}
