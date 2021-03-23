using LIMS_DEMO.Common;
using LIMS_DEMO.Areas.Configuration.Models;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Configuration;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LIMS_DEMO.Areas.Configuration.Controllers
{
    public class TestMethodMasterController : Controller
    {
        string strStatus = "";
        public TestMethodMasterController()
        {
            BALFactory.testMethodMasterBAL = new TestMethodMasterBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }
        // GET: Configuration/TestMethodMaster
        public ActionResult AddTestMethod(int? TestMethodId = 0)
        {
            TestMethodMasterModel model = new TestMethodMasterModel();

            if (TestMethodId != 0)
            {
                CoreFactory.testMethodMasterEntity = BALFactory.testMethodMasterBAL.GetDetailsTestMethod((Int32)TestMethodId);
                if (CoreFactory.testMethodMasterEntity != null)
                {
                    model.TestMethodId = CoreFactory.testMethodMasterEntity.TestMthodId;
                    model.TestMethod = CoreFactory.testMethodMasterEntity.TestMethodName;

                    model.IsActive = CoreFactory.testMethodMasterEntity.IsActive;
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddTestMethod(TestMethodMasterModel model)
        {
            CoreFactory.testMethodMasterEntity = new TestMethodMasterEntity();
            UnicodeConverter _conUniCode = new UnicodeConverter();
            string strTestMethod = _conUniCode.HtmlTagConverter(model.TestMethod);
            strTestMethod = RemoveHtmlTags.RemoveUnwantedTags(strTestMethod);
            strTestMethod = System.Web.HttpUtility.HtmlDecode(strTestMethod);
            model.TestMethod = strTestMethod;
            CoreFactory.testMethodMasterEntity.TestMthodId = model.TestMethodId;
            CoreFactory.testMethodMasterEntity.TestMethodName = model.TestMethod;
            //CoreFactory.testMethodMasterEntity.EnteredBy = LIMS.User.UserMasterID;
            CoreFactory.testMethodMasterEntity.EnteredDate = DateTime.Now;
            CoreFactory.testMethodMasterEntity.IsActive = model.IsActive;

            if (model.TestMethodId == 0)
            {
                strStatus = BALFactory.testMethodMasterBAL.AddTestMethod(CoreFactory.testMethodMasterEntity);
                //return Json(new { Status = strStatus, message = "TestMethod has been registered successfully." }, JsonRequestBehavior.AllowGet);
            }

            //Use for update
            else
            {
                strStatus = BALFactory.testMethodMasterBAL.UpdateTestMethod(CoreFactory.testMethodMasterEntity);
                //return Json(new { Status = strStatus, message = "TestMethod has been updated successfully." }, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("TestMethodList");
        }

        public ActionResult DeleteTestMethod(int TestMethodId)
        {
            strStatus = BALFactory.testMethodMasterBAL.DeleteTestMethod((Int32)TestMethodId);
            if (strStatus == "success")
            {
                TempData["message"] = "Discipline has been deleted successfully.";
            }
            return RedirectToAction("TestMethodList");
        }
        public ActionResult TestMethodList()
        {
            CoreFactory.testmethodList = BALFactory.testMethodMasterBAL.GetTestMethodList();
            List<TestMethodMasterModel> modelList = new List<TestMethodMasterModel>();
            foreach (var Item in CoreFactory.testmethodList)
            {
                modelList.Add(new TestMethodMasterModel()
                {
                    TestMethodId = Item.TestMthodId,
                    TestMethod = Item.TestMethodName,
                    //DisposedDay = Item.DisposedDay,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);

        }
    }
}