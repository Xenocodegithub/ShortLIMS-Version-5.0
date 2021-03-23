using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.DAL.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.BAL.Configuration
{
    public class TestMethodMasterBAL
    {
        public TestMethodMasterBAL()
        {
            CoreFactory.TestMethodMaster = new TestMethodMasterDAL();
        }
        public string AddTestMethod(TestMethodMasterEntity testMethodEntity)
        {
            return CoreFactory.TestMethodMaster.AddTestMethod(testMethodEntity);
        }
        public List<TestMethodMasterEntity> GetTestMethodList()
        {
            return CoreFactory.TestMethodMaster.GetTestMethodList();
        }
        public TestMethodMasterEntity GetDetailsTestMethod(int TestMethodId)
        {
            return CoreFactory.TestMethodMaster.GetDetailsTestMethod(TestMethodId);
        }
        public string UpdateTestMethod(TestMethodMasterEntity testMethodEntity)
        {
            return CoreFactory.TestMethodMaster.UpdateTestMethod(testMethodEntity);
        }
        public string DeleteTestMethod(int TestMthodId)
        {
            return CoreFactory.TestMethodMaster.DeleteTestMethod(TestMthodId);
        }
    }
}