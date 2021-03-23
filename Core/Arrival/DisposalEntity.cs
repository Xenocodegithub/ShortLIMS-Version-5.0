using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Arrival
{
    public class DisposalEntity
    {
        public long DisposalCollectionId { get; set; }
        public long SampleCollectionId { get; set; }
        public Nullable<long> EnquirySampleID { get; set; }
        public string SampleName { get; set; }
        public int SampleTypeProductId { get; set; }
        public string SampleType { get; set; }
        public string SampleCode { get; set; }
        public Nullable<DateTime> DateReciept { get; set; }
        public Nullable<DateTime> DateReporting { get; set; }
        public string DateofRecieptofSample { get; set; }
        public string DateofReporting { get; set; }
        public Nullable<DateTime> DateofDisposal { get; set; }
        public string DisposalMethod { get; set; }
        public string DisposedBy { get; set; }
        public string SuperwisedBy { get; set; }
        public string Remark { get; set; }
    }
}