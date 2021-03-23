using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Lab;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Lab;
using LIMS_DEMO.Common;
using Newtonsoft.Json;


namespace LIMS_DEMO.Areas.Lab.Controllers
{
    public class PlannerController : Controller
    {
        // GET: Lab/Planner
        public PlannerController()
        {
            BALFactory.planBAL = new PlanBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }
        public ActionResult SampleList()
        {
            
                var DisciplineId = CoreFactory.userEntity.DisciplineId;
                IList<Core.Lab.PlanEntity> planEntities = BALFactory.planBAL.GetList(DisciplineId, LIMS.User.UserMasterID);
                return View("SampleList", planEntities);
        }

        public JsonResult GetPopUpData(string SampleId)
        {
            try
            {
                var DisciplineId = CoreFactory.userEntity.DisciplineId;
                SampleInfoEntity sampleInfoEntity = BALFactory.planBAL.GetSampleInfoEntity(Convert.ToInt32(SampleId), DisciplineId);
                return Json(sampleInfoEntity, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }

        public string SaveSampleParameterPlan(string model)
        {
            try
            {
                ParameterSelectedDetails psd = JsonConvert.DeserializeObject<ParameterSelectedDetails>(model);
                psd.EnteredBy = CoreFactory.userEntity.UserMasterID;
                bool status = BALFactory.planBAL.SaveParameterSelectedDetails(psd);
                return "success ";
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }

    }
}