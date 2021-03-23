using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Configuration;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core;
using LIMS_DEMO.DAL;
using LIMS_DEMO.Areas.Configuration.Models;
using Newtonsoft.Json;

namespace LIMS_DEMO.Areas.Configuration.Controllers
{
    public class FormulabuilderController : Controller
    {
        public FormulabuilderController()
        {
            BALFactory.formulaBuilderBAL = new FormulaBuilderBAL();
        }
        // GET: Configuration/Formulabuilder
        public ActionResult Formulabuilder()
        {
            return View();
        }
        public JsonResult GetProductGroups(int SampleTypeProductId)
        {
            IList<ProductGroup> productGroup = BALFactory.formulaBuilderBAL.GetProductGroups(SampleTypeProductId);
            return Json(productGroup, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnitMaster()
        {
            IList<Unit> units = BALFactory.formulaBuilderBAL.GetUnitMaster();
            return Json(units, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubGroups(int SampleTypeProductId, int ProductgroupId)
        {
            IList<SubGroup> subGroup = BALFactory.formulaBuilderBAL.GetSubGroups(SampleTypeProductId, ProductgroupId);
            return Json(subGroup, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMatrix(int SampleTypeProductId, int ProductgroupId, int SubgroupId)
        {
            IList<Matrix> matrix = BALFactory.formulaBuilderBAL.GetMatrix(SampleTypeProductId, ProductgroupId, SubgroupId);
            return Json(matrix, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetParameterDetails(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId)
        {
            IList<ParameterInfo> parameterInfo = BALFactory.formulaBuilderBAL.GetParameterDetails(SampleTypeProductId, ProductgroupId, SubgroupId, MatrixId);
            return Json(parameterInfo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductGroup(string searchText, int SampleTypeProductId)
        {
            IList<ProductGroup> productGroup = BALFactory.formulaBuilderBAL.GetProductGroups(SampleTypeProductId);
            return Json(productGroup, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSampleTypeProduct(string searchText)
        {
            IList<SampleType> sampletype = BALFactory.formulaBuilderBAL.GetSampleTypeProducts();
            return Json(sampletype, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveParameterFormula(string FormulaList)
        {
            ParameterFormulaList parameterFormula = JsonConvert.DeserializeObject<ParameterFormulaList>(FormulaList);
            //parameterFormula.EnteredBy = CoreFactory.userEntity.UserMasterID;
            bool result = BALFactory.formulaBuilderBAL.SaveParameterFormula(parameterFormula);
            if (result == true)
            {
                return Json("Parameter Formula Saved!");
            }
            else
            {
                return Json("Something Went Wrong!");
            }
        }
        public JsonResult GetParameterFormula(int ParameterMasterId, int ParameterGroupId, int TestMethodID, int UnitID, int DisciplineId)
        {
            List<FormulaList> result = BALFactory.formulaBuilderBAL.GetParameterFormula(ParameterMasterId, ParameterGroupId, TestMethodID, UnitID, DisciplineId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTestMethod(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId, int ParameterMasterId)
        {
            IList<TestMethods> parameterInfo = BALFactory.formulaBuilderBAL.GetTestMethod(SampleTypeProductId, ProductgroupId, SubgroupId, MatrixId, ParameterMasterId);
            return Json(parameterInfo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnit(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId, int ParameterMasterId)
        {
            IList<Unit> parameterInfo = BALFactory.formulaBuilderBAL.GetUnit(SampleTypeProductId, ProductgroupId, SubgroupId, MatrixId, ParameterMasterId);
            return Json(parameterInfo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetParameterGroupId(int SampleTypeProductId, int ProductgroupId, int SubgroupId, int MatrixId)
        {
            var ParameterGroup = BALFactory.formulaBuilderBAL.GetParameterGroup(SampleTypeProductId, ProductgroupId, SubgroupId, MatrixId);
            return Json(ParameterGroup, JsonRequestBehavior.AllowGet);
        }

    }
}
