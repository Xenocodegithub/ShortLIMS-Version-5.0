using LIMS_DEMO.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.Core.Repository
{
   public interface ITestMethodMaster : IDisposable
    {
        string AddTestMethod(TestMethodMasterEntity testMethodEntity);
        List<TestMethodMasterEntity> GetTestMethodList();
        TestMethodMasterEntity GetDetailsTestMethod(int TestMethodId);
        string UpdateTestMethod(TestMethodMasterEntity testMethodEntity);
        string DeleteTestMethod(int TestMthodId);
    }
}
