using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.DropDowns;
using LIMS_DEMO.Core;
using LIMS_DEMO.DAL;

namespace LIMS_DEMO.Core.Repository
{
    public interface IFormulaBuilder
    {
        IList<SampleType> GetSampleTypeProducts();
        IList<ProductGroup> GetProductGroups(int SampleTypeProductId);
        IList<Unit> GetUnitMaster();
        IList<SubGroup> GetSubGroups(int SampleTypeProductId, int ProductGroupId);
        IList<Matrix> GetMatrix(int SampleTypeProductId, int ProductgroupId, int SubgroupId);
        IList<ParameterInfo> GetParameterDetails(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId);
        bool SaveParameterFormula(ParameterFormulaList parameterFormula);
        List<FormulaList> GetParameterFormula(int ParameterMasterId, int ParameterGroupId, int TestMethodID, int UnitID, int DisciplineId);
        IList<TestMethods> GetTestMethod(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId, int ParameterMasterId);
        IList<Unit> GetUnit(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId, int ParameterMasterId);
        long GetParameterGroup(int sampleTypeProductId, int productgroupId, int subgroupId, int matrixId);
    }
}
