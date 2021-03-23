using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.FieldDetermination
{
    public class CoalCokeSolidFuelEntity
    {
        public Nullable<bool> IsNABLAccredited { get; set; }
        public Nullable<bool> InField { get; set; }
        public string TestMethodName { get; set; }
        public int FieldId { get; set; }
        public Nullable<long> EnquiryId { get; set; }
        public Nullable<long> SampleCollectionId { get; set; }
        public Nullable<byte> StatusId { get; set; }
        public string Parameters { get; set; }
        public string TestResults { get; set; }
        public string AnyOtherObservations { get; set; }
        public string CurrentStatus { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
    public class FDCoalCokeInfo
    {
        public CoalCokeSolidFuelEntity CoalCokeDetails { get; set; }
        public List<CoalCokeSolidFuelEntity> CoalCokeInfos { get; set; }
    }
}
