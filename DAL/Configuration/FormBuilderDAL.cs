using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS_DEMO.DAL.FormBuilder
{
    public class FormBuilderDAL : IFormulaBuilder
    {
        readonly LIMSEntities _dbContext;
        public FormBuilderDAL()
        {
            _dbContext = new LIMSEntities();
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
        public IList<SampleType> GetSampleTypeProducts()
        {
            var result = (from pgm in _dbContext.SampleTypeProductMasters
                          where pgm.IsActive == true
                          select new SampleType
                          {
                              SampleTypeProductId = pgm.SampleTypeProductId,
                              SampleTypeProductName = pgm.SampleTypeProductName
                          }

                          ).ToList();
            return result;
        }

        public IList<ProductGroup> GetProductGroups(int SampleTypeProductId)
        {
            var result = (from pgm in _dbContext.ProductGroupMasters
                          where pgm.IsActive == true && pgm.SampleTypeProductId == SampleTypeProductId
                          select new ProductGroup
                          {
                              ProductGroupId = pgm.ProductGroupId,
                              ProductGroupName = pgm.ProductGroupName
                          }

                          ).ToList();
            return result;
        }
        public IList<Unit> GetUnitMaster()
        {
            var result = (from um in _dbContext.UnitMasters
                          where um.IsActive == true
                          select new Unit
                          {
                              UnitId = um.UnitId,
                              UnitName = um.Unit
                          }
                          ).ToList();
            return result;
        }
        public IList<SubGroup> GetSubGroups(int SampleTypeProductId, int ProductGroupId)
        {
            var result = (from sgm in _dbContext.SubGroupMasters
                          where sgm.IsActive == true && sgm.SampleTypeProductId == SampleTypeProductId && sgm.ProductGroupId == ProductGroupId
                          select new SubGroup
                          {
                              SubGroupId = sgm.SubGroupId,
                              SubGroupName = sgm.SubGroupName
                          }

                          ).ToList();
            return result;
        }
        public IList<Matrix> GetMatrix(int SampleTypeProductId, int ProductgroupId, int SubgroupId)
        {
            var result = (from mm in _dbContext.MatrixMasters
                          where mm.IsActive == true && mm.SampleTypeProductId == SampleTypeProductId && mm.ProductGroupId == ProductgroupId && mm.SubGroupId == SubgroupId
                          select new Matrix
                          {
                              MatrixId = mm.MatrixId,
                              MatrixName = mm.MatrixName
                          }

                          ).ToList();
            return result;
        }
        public IList<ParameterInfo> GetParameterDetails(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId)
        {
            IList<ParameterInfo> parameterInfos = new List<ParameterInfo>();
            var result = (from pgm in _dbContext.ParameterGroupMasters
                          join pm in _dbContext.ParameterMappings on pgm.ParameterGroupId equals pm.ParameterGroupId
                          join dm in _dbContext.DisciplineMasters on pm.DisciplineId equals dm.DisciplineId
                          join p in _dbContext.ParameterMasters on pm.ParameterMasterId equals p.ParameterMasterId
                          where pgm.SampleTypeProductId == SampleTypeProductId && pgm.IsActive == true && pgm.ProductGroupId == ProductgroupId
                          && pgm.SubGroupId == SubgroupId && pgm.MatrixId == MatrixId
                          select new
                          {

                              dm.DisciplineId,
                              dm.Discipline,
                              p.ParameterMasterId,
                              p.ParameterName,
                              pgm.ParameterGroupId

                          }).ToList().Distinct();
            foreach (var item in result)
            {
                ParameterInfo parameterInfo = new ParameterInfo();
                parameterInfo.ParameterGroupId = item.ParameterGroupId;
                parameterInfo.ParameterName = item.ParameterName;
                parameterInfo.ParameterMasterId = item.ParameterMasterId;
                parameterInfo.DisciplineId = item.DisciplineId;
                parameterInfo.DisciplineName = item.Discipline;

                List<Unit> unitList = new List<Unit>();
                var myUnitList = (from pgm in _dbContext.ParameterGroupMasters
                                  join pm in _dbContext.ParameterMappings on pgm.ParameterGroupId equals pm.ParameterGroupId
                                  join dm in _dbContext.DisciplineMasters on pm.DisciplineId equals dm.DisciplineId
                                  join p in _dbContext.ParameterMasters on pm.ParameterMasterId equals p.ParameterMasterId
                                  join um in _dbContext.UnitMasters on pm.UnitId equals um.UnitId
                                  where pgm.IsActive == true && pgm.SampleTypeProductId == SampleTypeProductId && pgm.ProductGroupId == ProductgroupId
                                && pgm.SubGroupId == SubgroupId && pgm.MatrixId == MatrixId
                                && pm.ParameterMasterId == parameterInfo.ParameterMasterId
                                  select new Unit
                                  {
                                      UnitId = um.UnitId,
                                      UnitName = um.Unit
                                  }).ToList();
                unitList = myUnitList.Distinct(new UnitComparer()).ToList();
                parameterInfo.UnitList = unitList;
                List<TestMethods> testMethodList = new List<TestMethods>();
                var mytestMethodList = (from pgm in _dbContext.ParameterGroupMasters
                                        join pm in _dbContext.ParameterMappings on pgm.ParameterGroupId equals pm.ParameterGroupId
                                        join dm in _dbContext.DisciplineMasters on pm.DisciplineId equals dm.DisciplineId
                                        join p in _dbContext.ParameterMasters on pm.ParameterMasterId equals p.ParameterMasterId
                                        join tm in _dbContext.TestMethods on pm.TestMethodId equals tm.TestMethodId
                                        where pgm.IsActive == true && pgm.SampleTypeProductId == SampleTypeProductId && pgm.ProductGroupId == ProductgroupId
                                     && pgm.SubGroupId == SubgroupId && pgm.MatrixId == MatrixId
                                     && pm.ParameterMasterId == parameterInfo.ParameterMasterId
                                        select new TestMethods
                                        {
                                            TestMethodId = tm.TestMethodId,
                                            TestMethodName = tm.TestMethod1

                                        }).ToList();
                testMethodList = mytestMethodList.Distinct(new TestMethodComparer()).ToList();
                parameterInfo.TestMethodList = testMethodList;
                parameterInfos.Add(parameterInfo);

            }
            return parameterInfos;
        }
        public bool SaveParameterFormula(ParameterFormulaList parameterFormula)
        {
            try
            {
                // List<FormulaList> formulaLists = new List<FormulaList>();

                var formulaLists = from pf in _dbContext.ParameterFormulas
                                   where pf.IsActive == true && pf.ParameterMasterId == parameterFormula.ParameterId
                                   && pf.ParameterGroupId == parameterFormula.ParameterGroupId
                                   && pf.UnitID == parameterFormula.UnitId && pf.TestMethodID == parameterFormula.TestMethodId && pf.DisciplineId == parameterFormula.DisciplineId
                                   select pf;
                _dbContext.ParameterFormulas.RemoveRange(formulaLists);
                _dbContext.SaveChanges();


                foreach (var item in parameterFormula.FormulaList)
                {

                    ParameterFormula obj = new ParameterFormula()
                    {
                        ParameterMasterId = parameterFormula.ParameterId,
                        TestMethodID = parameterFormula.TestMethodId,
                        ParameterGroupId = parameterFormula.ParameterGroupId,
                        UnitID = parameterFormula.UnitId,
                        DisciplineId = (byte)parameterFormula.DisciplineId,
                        FormulaSrNo = item.SrNo,
                        Notation = item.Notation,
                        DisplayName = item.DisplayName,
                        Formula = item.Formula,
                        IsFDV = item.IsFDV,
                        Unit = item.Unit,
                        DataType = item.DataType,
                        Precision = item.Precision,
                        IsActive = true,
                        EnteredBy = parameterFormula.EnteredBy,
                        EnteredDate = DateTime.Now

                    };
                    _dbContext.ParameterFormulas.Add(obj);
                    _dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<FormulaList> GetParameterFormula(int ParameterMasterId, int ParameterGroupId, int TestMethodID, int UnitID, int DisciplineId)
        {
            List<FormulaList> formulaLists = new List<FormulaList>();
            try
            {
                formulaLists = (from pf in _dbContext.ParameterFormulas
                                where pf.IsActive == true && pf.ParameterMasterId == ParameterMasterId
                                && pf.ParameterGroupId == ParameterGroupId
                                && pf.UnitID == UnitID && pf.TestMethodID == TestMethodID && pf.DisciplineId == DisciplineId
                                select new FormulaList
                                {
                                    Notation = pf.Notation,
                                    Formula = pf.Formula,
                                    IsFDV = (bool)pf.IsFDV,
                                    SrNo = pf.FormulaSrNo,
                                    DisplayName = pf.DisplayName,
                                    Unit = (Int32)pf.Unit,
                                    DataType = (int)pf.DataType,
                                    Precision = (int)pf.Precision
                                }

                        ).ToList();
                return formulaLists;
            }
            catch (Exception ex)
            {
                return formulaLists;
            }
        }
        public IList<TestMethods> GetTestMethod(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId, int ParameterMasterId)
        {
            List<TestMethods> testMethodList = new List<TestMethods>();
            testMethodList = (from pgm in _dbContext.ParameterGroupMasters
                              join pm in _dbContext.ParameterMappings on pgm.ParameterGroupId equals pm.ParameterGroupId
                              join dm in _dbContext.DisciplineMasters on pm.DisciplineId equals dm.DisciplineId
                              join p in _dbContext.ParameterMasters on pm.ParameterMasterId equals p.ParameterMasterId
                              join tm in _dbContext.TestMethods on pm.TestMethodId equals tm.TestMethodId
                              where pgm.IsActive == true && pgm.SampleTypeProductId == SampleTypeProductId && pgm.ProductGroupId == ProductgroupId
                           && pgm.SubGroupId == SubgroupId && pgm.MatrixId == MatrixId
                           && pm.ParameterMasterId == ParameterMasterId
                              select new TestMethods
                              {
                                  TestMethodId = tm.TestMethodId,
                                  TestMethodName = tm.TestMethod1
                              }).Distinct().ToList();
            return testMethodList;
        }
        public IList<Unit> GetUnit(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId, int ParameterMasterId)
        {
            List<Unit> unitList = new List<Unit>();
            unitList = (from pgm in _dbContext.ParameterGroupMasters
                        join pm in _dbContext.ParameterMappings on pgm.ParameterGroupId equals pm.ParameterGroupId
                        join dm in _dbContext.DisciplineMasters on pm.DisciplineId equals dm.DisciplineId
                        join p in _dbContext.ParameterMasters on pm.ParameterMasterId equals p.ParameterMasterId
                        join um in _dbContext.UnitMasters on pm.UnitId equals um.UnitId
                        where pgm.IsActive == true && pgm.SampleTypeProductId == SampleTypeProductId && pgm.ProductGroupId == ProductgroupId
                      && pgm.SubGroupId == SubgroupId && pgm.MatrixId == MatrixId
                      && pm.ParameterMasterId == ParameterMasterId
                        select new Unit
                        {
                            UnitId = um.UnitId,
                            UnitName = um.Unit
                        }).Distinct().ToList();

            return unitList;
        }
        public long GetParameterGroup(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId)
        {

            var result = (from pgm in _dbContext.ParameterGroupMasters
                          join pm in _dbContext.ParameterMappings on pgm.ParameterGroupId equals pm.ParameterGroupId
                          join dm in _dbContext.DisciplineMasters on pm.DisciplineId equals dm.DisciplineId
                          join p in _dbContext.ParameterMasters on pm.ParameterMasterId equals p.ParameterMasterId
                          where pgm.SampleTypeProductId == SampleTypeProductId && pgm.IsActive == true && pgm.ProductGroupId == ProductgroupId
                          && pgm.SubGroupId == SubgroupId && pgm.MatrixId == MatrixId
                          select new
                          {
                              pgm.ParameterGroupId
                          }).FirstOrDefault();
            return result.ParameterGroupId;
        }
    }
}
