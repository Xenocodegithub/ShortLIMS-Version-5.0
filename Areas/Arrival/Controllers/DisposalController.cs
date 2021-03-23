using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Arrival.Models;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Arrival;
using LIMS_DEMO.BAL.DropDown;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Arrival;

namespace LIMS_DEMO.Areas.Arrival.Controllers
{
    [RouteArea("Arrival")]
    public class DisposalController : Controller
    {
        string strStatus = "";
        public DisposalController()
        {
            BALFactory.dropdownsBAL = new DropDownBal();
            BALFactory.samplearrivalBAL = new SampleArrivalBAL();
        }
        public ActionResult AddDisposal()
        {
            ViewBag.SuperwisedByList = BALFactory.dropdownsBAL.GetGroupInchargeList("SuperwisedBy", LIMS.User.LabId);// Passing RoleName as Parameters
            ViewBag.DisposedByList = BALFactory.dropdownsBAL.GetGroupInchargeList("DisposedBy", LIMS.User.LabId);// Passing RoleName as Parameters
            ViewBag.SampleTypeProductList = BALFactory.dropdownsBAL.GetSampleTypeProduct();
            ViewBag.SampleCodeList = BALFactory.dropdownsBAL.GetSampleCode();
            return View();
        }

        [HttpPost]
        public JsonResult AddDisposal(DisposalModel model)
        {
            DisposalEntity disposalEntity = new DisposalEntity();
            disposalEntity.SampleCollectionId = model.SampleCollectionId;
            disposalEntity.SampleTypeProductId = model.SampleTypeProductId;
            disposalEntity.SampleType = model.SampleType;
            disposalEntity.SampleCode = model.SampleName;
            disposalEntity.DateofRecieptofSample = model.DateofRecieptofSample;
            disposalEntity.DateofReporting = model.DateofReporting;
            disposalEntity.DateofDisposal = model.DateofDisposal;
            disposalEntity.DisposalMethod = model.DisposalMethod;
            disposalEntity.DisposedBy = model.DisposedBy;
            disposalEntity.SuperwisedBy = model.SuperwisedBy;
            disposalEntity.Remark = model.Remark;
            long Id = BALFactory.samplearrivalBAL.SaveDisposalData(disposalEntity);
            return Json(new { Id = Id }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DisposalList()
        {
            CoreFactory.DisposalList = BALFactory.samplearrivalBAL.GetDisposalData();
            List<DisposalModel> disposallist = new List<DisposalModel>();
            foreach (var item in CoreFactory.DisposalList)
            {
                disposallist.Add(new DisposalModel()
                {
                    DisposalCollectionId = item.DisposalCollectionId,
                    SampleType = item.SampleType,
                    SampleCode = item.SampleCode,
                    DateofRecieptofSample = item.DateofRecieptofSample,
                    DateofReporting = item.DateofReporting,
                    DateofDisposal = item.DateofDisposal,
                    DisposalMethod = item.DisposalMethod,
                    DisposedBy = item.DisposedBy,
                    SuperwisedBy = item.SuperwisedBy,
                    Remark = item.Remark
                });
            }
            return View(disposallist);
        }
        public JsonResult GetCode(int SampleTypeProductId)
        {
            var codeList = BALFactory.samplearrivalBAL.GetCode(SampleTypeProductId);
            return Json(new { result = codeList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDates(long SampleCollectionId)
        {
            var dates = BALFactory.samplearrivalBAL.GetDates(SampleCollectionId);
            return Json(new { d1 = Convert.ToDateTime(dates.DateReciept).ToString("dd/MM/yyyy"), d2 = Convert.ToDateTime(dates.DateReporting).ToString("dd/MM/yyyy") }, JsonRequestBehavior.AllowGet);
        }
    }
}