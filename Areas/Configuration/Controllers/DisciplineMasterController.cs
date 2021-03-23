using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Configuration;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Areas.Configuration.Models;
using LIMS_DEMO.Core;

namespace LIMS_DEMO.Areas.Configuration.Controllers
{
    public class DisciplineMasterController : Controller
    {
        string strStatus = "";
        public DisciplineMasterController()
        {
            BALFactory.disciplineMasterBAL = new DisciplineMasterBAL();
            //BALFactory.dropdownsBAL = new BAL.Dropdowns.DropdownsBAL();
        }
        // GET: Configuration/DisciplineMaster
        public ActionResult AddDiscipline(int? DisciplineId=0)
        {
            DisciplineMasterModel model = new DisciplineMasterModel();
            if(DisciplineId != 0)
            {
                CoreFactory.disciplineMasterEntity = BALFactory.disciplineMasterBAL.GetDetailsDiscipline((Int32)DisciplineId);
                if(CoreFactory.disciplineMasterEntity != null)
                {
                    model.DisciplineId = CoreFactory.disciplineMasterEntity.DisciplineId;
                    model.Discipline = CoreFactory.disciplineMasterEntity.Discipline;
                    model.IsActive = CoreFactory.disciplineMasterEntity.IsActive;
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult DisciplineList()
        {
            CoreFactory.disciplineList = BALFactory.disciplineMasterBAL.GetDisciplineList();
            List<DisciplineMasterModel> modelList = new List<DisciplineMasterModel>();
            foreach (var Item in CoreFactory.disciplineList)
            {
                modelList.Add(new DisciplineMasterModel()
                {
                    Discipline = Item.Discipline,
                    DisciplineId = Item.DisciplineId,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }
        [HttpPost]
        public ActionResult AddDiscipline(DisciplineMasterModel model)
        {
            DisciplineMasterEntity disciplineMasterEntity = new DisciplineMasterEntity();
            disciplineMasterEntity.DisciplineId = model.DisciplineId;
            disciplineMasterEntity.Discipline = model.Discipline;
            disciplineMasterEntity.EnteredBy = 1;
            disciplineMasterEntity.EnteredDate = DateTime.Now;
            disciplineMasterEntity.ModifiedDate = model.ModifiedDate;
            disciplineMasterEntity.IsActive = true;
            var result = BALFactory.disciplineMasterBAL.AddDiscipline(disciplineMasterEntity);
            //if (model.DisciplineId == 0)
            //{
            //    strStatus = BALFactory.disciplineMasterBAL.AddDiscipline(CoreFactory.disciplineMasterEntity);
            //    return Json(new { Status = strStatus, message = "Discipline has been registerd successfully." }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    strStatus = BALFactory.disciplineMasterBAL.UpdateDiscipline(CoreFactory.disciplineMasterEntity);
            //    return Json(new { Status = strStatus, message = "Discipline has been updated successfully." }, JsonRequestBehavior.AllowGet);
            //}
            if (result == "success")
            {
                return RedirectToAction("DisciplineList");
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Json(new { status = "success", message = "Record Updated Failed." });
            }

        }
         public ActionResult DeleteDiscipline(int DisciplineId)
        {
            strStatus = BALFactory.disciplineMasterBAL.DeleteDiscipline(DisciplineId);
            if (strStatus == "success")
            {
                TempData["message"] = "Discipline has been deleted successfully.";
            }
            return  RedirectToAction("DisciplineList");
        }
    }
}
