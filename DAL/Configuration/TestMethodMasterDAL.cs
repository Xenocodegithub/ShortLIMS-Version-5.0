using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIMS_DEMO.DAL.Configuration
{

    public class TestMethodMasterDAL: ITestMethodMaster
    {
        readonly LIMSEntities _dbContext;
        public TestMethodMasterDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public string AddTestMethod(TestMethodMasterEntity testMethodEntity)
        {
            try
            {
                _dbContext.TestMethods.Add(new TestMethod()
                {
                    TestMethodId = testMethodEntity.TestMthodId,
                    TestMethod1 = testMethodEntity.TestMethodName,
                    //ProductGroupCode = productSubMatrixEntity.Code,
                    //Description = configurationEntity.Description,
                    //IsSampleToBeDisposed = configurationEntity.IsSampleToBeDisposed,
                    //SampleRetentionPeriod = configurationEntity.SampleRetentionPeriod,
                    IsActive = testMethodEntity.IsActive,
                    EnteredBy = testMethodEntity.EnteredBy,
                    EnteredDate = testMethodEntity.EnteredDate,
                    ModifiedBy = testMethodEntity.ModifiedBy

                });
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }

        }

        public List<TestMethodMasterEntity> GetTestMethodList()
        {
            try
            {

                return (from e in _dbContext.TestMethods
                        where e.IsActive == true
                        select new TestMethodMasterEntity()
                        {
                            TestMethodName = e.TestMethod1,
                            TestMthodId = e.TestMethodId,
                            IsActive = e.IsActive,

                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }


        }
        public TestMethodMasterEntity GetDetailsTestMethod(int TestMethodId)
        {
            return _dbContext.TestMethods.Where(u => u.TestMethodId == TestMethodId).Select(u => new TestMethodMasterEntity()
            {
                TestMthodId = u.TestMethodId,
                TestMethodName = u.TestMethod1,
                IsActive = u.IsActive
            }).FirstOrDefault();
        }
        public string UpdateTestMethod(TestMethodMasterEntity testMethodEntity)
        {
            try
            {
                var TestMethodMaster = _dbContext.TestMethods.Find(testMethodEntity.TestMthodId);
                TestMethodMaster.TestMethodId = testMethodEntity.TestMthodId;
                TestMethodMaster.TestMethod1 = testMethodEntity.TestMethodName;
                TestMethodMaster.IsActive = testMethodEntity.IsActive;
                TestMethodMaster.EnteredBy = testMethodEntity.EnteredBy;
                TestMethodMaster.EnteredDate = testMethodEntity.EnteredDate;

                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public string DeleteTestMethod(int TestMthodId)
        {
            try
            {
                var TestMethodMaster = _dbContext.TestMethods.Find(TestMthodId);
                TestMethodMaster.IsActive = false;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }



        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {

            if (!this.disposed)
            { if (disposing) { _dbContext.Dispose(); } }

            this.disposed = true;

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}