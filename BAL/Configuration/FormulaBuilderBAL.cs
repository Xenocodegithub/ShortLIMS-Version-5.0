using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Configuration;
using LIMS_DEMO.Core;
using LIMS_DEMO.DAL;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.DAL.Configuration;
using LIMS_DEMO.DAL.FormBuilder;

namespace LIMS_DEMO.BAL.Configuration
{
    public class FormulaBuilderBAL
    {
        public FormulaBuilderBAL()
        {
            CoreFactory.formulaBuilder = new FormBuilderDAL();
        }
        public IList<ProductGroup> GetProductGroups(int SampleTypeProductId)
        {
            return CoreFactory.formulaBuilder.GetProductGroups(SampleTypeProductId);
        }
        public IList<SampleType> GetSampleTypeProducts()
        {
            return CoreFactory.formulaBuilder.GetSampleTypeProducts();
        }
        public IList<Unit> GetUnitMaster()
        {
            return CoreFactory.formulaBuilder.GetUnitMaster();
        }
        public IList<SubGroup> GetSubGroups(int SampleTypeProductId, int ProductgroupId)
        {
            return CoreFactory.formulaBuilder.GetSubGroups(SampleTypeProductId, ProductgroupId);
        }
        public IList<Matrix> GetMatrix(int SampleTypeProductId, int ProductgroupId, int SubgroupId)
        {
            return CoreFactory.formulaBuilder.GetMatrix(SampleTypeProductId, ProductgroupId, SubgroupId);
        }
        public IList<ParameterInfo> GetParameterDetails(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId)
        {
            return CoreFactory.formulaBuilder.GetParameterDetails(SampleTypeProductId, ProductgroupId, SubgroupId, MatrixId);
        }
        public long GetParameterGroup(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId)
        {
            return CoreFactory.formulaBuilder.GetParameterGroup(SampleTypeProductId, ProductgroupId, SubgroupId, MatrixId);
        }
        public bool SaveParameterFormula(ParameterFormulaList parameterFormula)
        {
            return CoreFactory.formulaBuilder.SaveParameterFormula(parameterFormula);
        }
        public List<FormulaList> GetParameterFormula(int ParameterMasterId, int ParameterGroupId, int TestMethodID, int UnitID, int DisciplineId)
        {
            return CoreFactory.formulaBuilder.GetParameterFormula(ParameterMasterId, ParameterGroupId, TestMethodID, UnitID, DisciplineId);

        }
        public IList<TestMethods> GetTestMethod(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId, int ParameterMasterId)
        {
            return CoreFactory.formulaBuilder.GetTestMethod(SampleTypeProductId, ProductgroupId, SubgroupId, MatrixId, ParameterMasterId);
        }
        public IList<Unit> GetUnit(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId, int ParameterMasterId)
        {
            return CoreFactory.formulaBuilder.GetUnit(SampleTypeProductId, ProductgroupId, SubgroupId, MatrixId, ParameterMasterId);
        }
    }
}
