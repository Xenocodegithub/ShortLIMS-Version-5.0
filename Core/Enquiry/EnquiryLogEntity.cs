using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Enquiry
{
    public class EnquiryLogEntity
    {
        public long EnquiryLogId { get; set; }
        public long EnquiryId { get; set; }
        public string CommunicationMode { get; set; }//ModeOfCommunication tbl
        public byte ModeOfCommunicationId { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CommunicationDate { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string CustomerName { get; set; }//CustomerTypeMaster & CustomerMaster tbls
        public int? CustomerMasterId { get; set; }
    }
}
