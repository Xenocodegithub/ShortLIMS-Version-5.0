using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.Core.Configuration
{
    public class SampleType
    {
        public int SampleTypeProductId { get; set; }
        public string SampleTypeProductName { get; set; }

    }
    public class ProductGroup
    {
        public int ProductGroupId { get; set; }
        public string ProductGroupName { get; set; }

    }
    public class SubGroup
    {
        public int SubGroupId { get; set; }
        public string SubGroupName { get; set; }

    }
    public class Matrix
    {
        public int MatrixId { get; set; }
        public string MatrixName { get; set; }

    }
    public class ParameterInfo
    {
        public long ParameterGroupId { get; set; }
        public long ParameterMasterId { get; set; }
        public string ParameterName { get; set; }
        public List<Unit> UnitList { get; set; }
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }
        public List<TestMethods> TestMethodList { get; set; }
        public bool IsActive { get; set; }
    }
    public class TestMethods
    {
        public int? TestMethodId { get; set; }
        public string TestMethodName { get; set; }
    }
    public class Unit
    {
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
    }
    public class ParameterFormulaList
    {
        public int ParameterGroupId { get; set; }
        public int SampleCollectionId { get; set; }
        public int ParameterId { get; set; }
        public int UnitId { get; set; }
        public string TestingHour { get; set; }
        public int TestMethodId { get; set; }
        public int DisciplineId { get; set; }
        public int EnteredBy { get; set; }
        //public Dictionary<string, List<FormulaList>> FormulaLists { get; set; }
        public List<FormulaList> FormulaList { get; set; }

    }
    public class FormulaList
    {
        public long Id { get; set; }
        public int SrNo { get; set; }
        public string Notation { get; set; }
        public string DisplayName { get; set; }
        public string Formula { get; set; }
        public bool? IsFDV { get; set; }
        public string NotationValue { get; set; }
        public int Unit { get; set; }
        public int DataType { get; set; }
        public int Precision { get; set; }
        public int ParameterGroupId { get; set; }
        public int ParameterMasterId { get; set; }
    }
    public class UnitComparer : IEqualityComparer<Unit>
    {
        public bool Equals(Unit x, Unit y)
        {
            return x.UnitId == y.UnitId;
        }

        public int GetHashCode(Unit obj)
        {
            return (int)obj.UnitId;
        }
    }

    public class TestMethodComparer : IEqualityComparer<TestMethods>
    {
        public bool Equals(TestMethods x, TestMethods y)
        {
            return x.TestMethodId == y.TestMethodId;
        }

        public int GetHashCode(TestMethods obj)
        {
            return (int)obj.TestMethodId;
        }
    }
}