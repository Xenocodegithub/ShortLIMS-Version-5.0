using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Areas.Arrival.Models
{
    public class DisposalModel
    {
        public long DisposalCollectionId { get; set; }
        public string SampleName { get; set; }
        [Required(ErrorMessage = "Please Select Sample Code")]
        public long SampleCollectionId { get; set; }
        public Nullable<long> EnquirySampleID { get; set; }
        [Required(ErrorMessage ="Please Select Sample Type")]
        public int SampleTypeProductId { get; set; }
        public string SampleType { get; set; }
        public string SampleCode { get; set; }
        public string DateofRecieptofSample { get; set; }
        public string DateofReporting { get; set; }
        [Required(ErrorMessage = "Please Select Date of Disposal")]
        public Nullable<DateTime> DateofDisposal { get; set; }
        public string DisposalMethod { get; set; }
        public Nullable<int> DisposedByID { get; set; }
        public Nullable<int> SuperwisedByID { get; set; }
        public string DisposedBy { get; set; }
        public string SuperwisedBy { get; set; }
        public string Remark { get; set; }
    }
}