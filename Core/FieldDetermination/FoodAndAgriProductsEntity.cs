using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.FieldDetermination
{
    public class FoodAndAgriProductsEntity
    {
        public Nullable<bool> IsNABLAccredited { get; set; }
        public Nullable<bool> InField { get; set; }
        public string TestMethodName { get; set; }
        public string CurrentStatus { get; set; }
        public int FieldFoodAndAgriCultureId { get; set; }
        //public int SolidHazardousWasteSoilOilId { get; set; }
        public Nullable<long> EnquiryId { get; set; }
        public long SampleCollectionId { get; set; }
        public int SrNo { get; set; }
        public string Parameters { get; set; }
        public string TestResults { get; set; }
        public Nullable<byte> StatusId { get; set; }
        public string AnyOtherObservation { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
    }
    public class FDFoodInfo
    {
        public FoodAndAgriProductsEntity FoodDetails { get; set; }
        public List<FoodAndAgriProductsEntity> FoodInfos { get; set; }
    }
}
