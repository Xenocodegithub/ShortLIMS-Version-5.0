using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.FieldDetermination.Models;
using LIMS_DEMO.Areas.Arrival.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.FieldDetermination;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.FieldDetermination;
using LIMS_DEMO.BAL.Arrival;

namespace LIMS_DEMO.Areas.FieldDetermination.Controllers
{
    [RouteArea("FieldDetermination")]
    public class FieldMicrobiologicalMonitoringOfAirController : Controller
    {
        string strStatus = "";
        public FieldMicrobiologicalMonitoringOfAirController()
        {
            BALFactory.microbiologicalMonitoringOfAirBAL = new MicrobiologicalMonitoringOfAirBAL();
            BALFactory.enquiryBAL = new BAL.Enquiry.EnquiryBAL();
            BALFactory.samplecollectionBAL = new BAL.Collection.SampleCollectionBAL();
            BALFactory.samplearrivalBAL = new SampleArrivalBAL();
        }
        // GET: FieldDetermination/FieldMicrobiologicalMonitoringOfAir
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FieldMicrobiologicalMonitoringOfAir(string Url, int? SampleCollectionId = 0, int? EnquiryId = 0, int? EnquirySampleID = 0, int? FieldId = 0)
        {
            FDMicrobiologicalMonitoringOfAirModel model = new FDMicrobiologicalMonitoringOfAirModel();
            model.GridModel = new List<FDMicrobiologicalMonitoringOfAirModel>();
            model.SampleCollectionId = SampleCollectionId;
            model.EnquiryId = EnquiryId;
            model.EnquirySampleID = (long)EnquirySampleID;
            ViewBag.MatrixName = Url;

            if (FieldId != 0)
            {
                FDMicrobiologicalInfo microbiologicalInfo = BALFactory.microbiologicalMonitoringOfAirBAL.GetAirMonitoringDetails((Int32)FieldId);
                if (microbiologicalInfo != null)
                {
                    //CoreFactory.microbiologicalMonitoringEntity = BALFactory.microbiologicalMonitoringOfAirBAL.GetAirMonitoringDetails((Int32)FieldId);
                    model.MicrobiologicalID = (Int32)FieldId;
                    model.SampleCollectionId = SampleCollectionId;
                    model.EnquiryId = EnquiryId;
                    model.AreaSwabbed = microbiologicalInfo.MicrobiologicalDetails.AreaSwabbed;
                    model.StatusId = microbiologicalInfo.MicrobiologicalDetails.StatusId;
                    model.CurrentStatus = microbiologicalInfo.MicrobiologicalDetails.CurrentStatus;
                    ViewBag.CurrentStatus = microbiologicalInfo.MicrobiologicalDetails.CurrentStatus;
                    model.FlowRate = microbiologicalInfo.MicrobiologicalDetails.FlowRate;
                    model.MediaUsed = microbiologicalInfo.MicrobiologicalDetails.MediaUsed;
                    model.IndustryType = microbiologicalInfo.MicrobiologicalDetails.IndustryType;
                    model.EquipmentId = microbiologicalInfo.MicrobiologicalDetails.EquipmentId;
                    model.RelativeHumidity = microbiologicalInfo.MicrobiologicalDetails.RelativeHumidity;
                    model.AnyOtherObservation = microbiologicalInfo.MicrobiologicalDetails.AnyOtherObservation;

                    int i = 1;
                    foreach (var grid1 in microbiologicalInfo.MicrobiologicalInfos)
                    {
                        try
                        {
                            model.GridModel.Add(new FDMicrobiologicalMonitoringOfAirModel()
                            {
                                SrNo = i,
                                AreaSwabbed = grid1.AreaSwabbed,
                                StatusId = grid1.StatusId,
                                FlowRate = grid1.FlowRate,
                                MediaUsed = grid1.MediaUsed,
                                IndustryType = grid1.IndustryType,
                                EquipmentId = grid1.EquipmentId,
                                RelativeHumidity = grid1.RelativeHumidity,
                            });
                            i++;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

            }
            return View(model);
        }
        [HttpPost]
        public JsonResult FieldMicrobiologicalMonitoringOfAir(FDMicrobiologicalMonitoringOfAirModel model, string Save)
        {
            CoreFactory.microbiologicalMonitoringEntity = new MicrobiologicalMonitoringOfAirEntity();
            CoreFactory.microbiologicalMonitoringEntity.MicrobiologicalID = Convert.ToByte(model.MicrobiologicalID);
            CoreFactory.microbiologicalMonitoringEntity.SampleCollectionId = (long)model.SampleCollectionId;
            CoreFactory.microbiologicalMonitoringEntity.EnquiryId = model.EnquiryId;
            //CoreFactory.microbiologicalMonitoringEntity.StatusId = (byte)iStatusId;
            CoreFactory.microbiologicalMonitoringEntity.AreaSwabbed = model.AreaSwabbed;
            CoreFactory.microbiologicalMonitoringEntity.FlowRate = model.FlowRate;
            CoreFactory.microbiologicalMonitoringEntity.MediaUsed = model.MediaUsed;
            CoreFactory.microbiologicalMonitoringEntity.IndustryType = model.IndustryType;
            CoreFactory.microbiologicalMonitoringEntity.EquipmentId = model.EquipmentId;
            CoreFactory.microbiologicalMonitoringEntity.RelativeHumidity = model.RelativeHumidity;
            CoreFactory.microbiologicalMonitoringEntity.AnyOtherObservation = model.AnyOtherObservation;
            CoreFactory.microbiologicalMonitoringEntity.IsActive = true;
            CoreFactory.microbiologicalMonitoringEntity.EnteredBy = LIMS.User.UserMasterID;
            CoreFactory.microbiologicalMonitoringEntity.EnteredDate = DateTime.UtcNow;
            if (Save == "FieldSave")
            {
                int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("InProgress");
                CoreFactory.microbiologicalMonitoringEntity.StatusId = (byte)iStatusId;
                strStatus = BALFactory.microbiologicalMonitoringOfAirBAL.Add(CoreFactory.microbiologicalMonitoringEntity);
            }
            else if (Save == "FieldSubmit")
            {
                int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("SampleColl");
                CoreFactory.microbiologicalMonitoringEntity.StatusId = (byte)iStatusId;
                strStatus = BALFactory.microbiologicalMonitoringOfAirBAL.Add(CoreFactory.microbiologicalMonitoringEntity);
            }
            return Json(new { Status = strStatus, message = "Values Entered successfully." }, JsonRequestBehavior.AllowGet);
        }
    }
}