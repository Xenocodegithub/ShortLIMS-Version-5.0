using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Dashboard;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Dashboard;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web.Configuration;



namespace LIMS_DEMO.Areas.Dashboard.Controllers
{
    public class DashboardController : Controller
    {
        string strStatus = "";
        public DashboardController()
        {
            BALFactory.dashboardBAL = new DashboardBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }
        // GET: Dashboard/Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Admin()
       {


            var totalenquiry = BALFactory.dashboardBAL.GetTotalEnquiryCount();
            LIMS.User.TotalEnquiryCount = totalenquiry.Count();

            var totalsamplecollected = BALFactory.dashboardBAL.GetTotalSampleCollectedCount();
            LIMS.User.TotalSampleCollectedCount = totalsamplecollected.Count();

            decimal? TotalRevenue = BALFactory.dashboardBAL.GetTotalRevenue();
            LIMS.User.TotalRevenue = TotalRevenue;

            var totalWO = BALFactory.dashboardBAL.GetTotalWOCount();
            LIMS.User.TotalWOCount = totalWO.Count();

            decimal? TotalPOAmount = BALFactory.dashboardBAL.GetTotalPOAmount();
            LIMS.User.TotalPOAmount = TotalPOAmount;

            
            return View();
        }
        public ActionResult BDM()
        {


            var totalenquiry = BALFactory.dashboardBAL.GetTotalEnquiryCount();
            LIMS.User.TotalEnquiryCount = totalenquiry.Count();

            var totalWO = BALFactory.dashboardBAL.GetTotalWOCount();
            LIMS.User.TotalWOCount = totalWO.Count();

            var totalQuotation = BALFactory.dashboardBAL.GetTotalQuotationCount();
            LIMS.User.TotalQuotationCount = totalQuotation.Count();

            return View();
        }
        public ActionResult HOD()
        {
            decimal? TotalPOAmount = BALFactory.dashboardBAL.GetTotalPOAmount();
            LIMS.User.TotalPOAmount = TotalPOAmount;

            var totalsamplecollected = BALFactory.dashboardBAL.GetTotalSampleCollectedCount();
            LIMS.User.TotalSampleCollectedCount = totalsamplecollected.Count();

            var totalWO = BALFactory.dashboardBAL.GetTotalWOCount();
            LIMS.User.TotalWOCount = totalWO.Count();

            
            return View();
        }
        public ActionResult STL()
        {
           

            var totalWOComp = BALFactory.dashboardBAL.GetTotalWOCompCount();
            LIMS.User.TotalWOComp = totalWOComp.Count();

            var totalWOExe = BALFactory.dashboardBAL.GetTotalWOExeCount();
            LIMS.User.TotalWOExe = totalWOExe.Count();

            var totalWO = BALFactory.dashboardBAL.GetTotalWOCount();
            LIMS.User.TotalWOCount = totalWO.Count();


            return View();
        }
        public ActionResult TM()
        {

            var totalsamplecollected = BALFactory.dashboardBAL.GetTotalSampleCollectedCount();
            LIMS.User.TotalSampleCollectedCount = totalsamplecollected.Count();
            return View();
        }
        public ActionResult Sampler()
        {
            var totalsamplecollected = BALFactory.dashboardBAL.GetTotalSampleCollectedCount();
            LIMS.User.TotalSampleCollectedCount = totalsamplecollected.Count();
            return View();
        }
        public ActionResult ReceiptIncharge()
        {
            var totalWO = BALFactory.dashboardBAL.GetTotalWOCount();
            LIMS.User.TotalWOCount = totalWO.Count();

            var totalWOComp = BALFactory.dashboardBAL.GetTotalWOCompCount();
            LIMS.User.TotalWOComp = totalWOComp.Count();

            var totalWOExe = BALFactory.dashboardBAL.GetTotalWOExeCount();
            LIMS.User.TotalWOExe = totalWOExe.Count();

            var totalsamplecollected = BALFactory.dashboardBAL.GetTotalSampleCollectedCount();
            LIMS.User.TotalSampleCollectedCount = totalsamplecollected.Count();
            return View();
        }
        public ActionResult SampleReceiver()
        {
           
            var totalsamplecollected = BALFactory.dashboardBAL.GetTotalSampleCollectedCount();
            LIMS.User.TotalSampleCollectedCount = totalsamplecollected.Count();
            return View();
        }
        public ActionResult PurchaseIncharge()
        {

            decimal? TotalPOAmount = BALFactory.dashboardBAL.GetTotalPOAmount();
            LIMS.User.TotalPOAmount = TotalPOAmount;
            return View();
        }
        public ActionResult Lab()
        {
            var totalsamplecollected = BALFactory.dashboardBAL.GetTotalSampleCollectedCount();
            LIMS.User.TotalSampleCollectedCount = totalsamplecollected.Count();
           
            var totalsampleTested = BALFactory.dashboardBAL.GetTotalSampleTestedCount();
            LIMS.User.TotalSampleTestedCount = totalsampleTested.Count();
            return View();
        }
        public ActionResult Approver()
        {
            var totalsamplecollected = BALFactory.dashboardBAL.GetTotalSampleCollectedCount();
            LIMS.User.TotalSampleCollectedCount = totalsamplecollected.Count();

            var totalsampleTested = BALFactory.dashboardBAL.GetTotalSampleTestedCount();
            LIMS.User.TotalSampleTestedCount = totalsampleTested.Count();
            return View();
        }


        public ActionResult GetTotalEnquiry()
        {
            //DashBoard dash1 = new DashBoard();

            DashboardEntity dash1 = new DashboardEntity();

            dash1.Mode = "SELECT Total Enquiry";
            var jsonDashBoardDTO="";
            List<DashboardEntity> baseEntityCollectionResponseDashBoard = BALFactory.dashboardBAL.GetTotalEnquiry(dash1);
            if (baseEntityCollectionResponseDashBoard != null)
            {
                if (baseEntityCollectionResponseDashBoard != null && baseEntityCollectionResponseDashBoard.Count > 0)
                {
                     jsonDashBoardDTO = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(baseEntityCollectionResponseDashBoard);
                    ViewBag.TotalEnquiry = jsonDashBoardDTO;
                }
            }
            return Json(new { data = baseEntityCollectionResponseDashBoard }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTotalSampleTypWise()
        {
            //DashBoard dash1 = new DashBoard();
            DashboardEntity dash1 = new DashboardEntity();
            dash1.Mode = "SELECT Total Sample Type Wise GRAPH";
            var jsonDashBoardDTO = "";
            List<DashboardEntity> baseEntityCollectionResponseSampleMonth = BALFactory.dashboardBAL.GetTotalSampleTypeWise(dash1);
            if (baseEntityCollectionResponseSampleMonth != null)
            {
                if (baseEntityCollectionResponseSampleMonth != null && baseEntityCollectionResponseSampleMonth.Count > 0)
                {
                     jsonDashBoardDTO = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(baseEntityCollectionResponseSampleMonth);
                    ViewBag.SampleMonthItemViewBag = jsonDashBoardDTO;
                }
            }

            return Json(new { data = baseEntityCollectionResponseSampleMonth }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTotalWO()
        {
            DashboardEntity dash1 = new DashboardEntity();
            dash1.Mode ="SELECT TOTAL WO";

            List<DashboardEntity> baseEntityCollectionResponseWO = BALFactory.dashboardBAL.GetTotalWO(dash1);
            if (baseEntityCollectionResponseWO != null)
            {
                if (baseEntityCollectionResponseWO != null && baseEntityCollectionResponseWO.Count > 0)
                {
                    var jsonDashBoardDTOWO = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(baseEntityCollectionResponseWO);
                    ViewBag.TotalWO = jsonDashBoardDTOWO;
                }
            }
            return Json(new { data = baseEntityCollectionResponseWO }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTotalPOAmountMonthWise()
        {
            DashboardEntity dash1 = new DashboardEntity();
            dash1.Mode = "SELECT TOTAL POAmount Month";

            List<DashboardEntity> baseEntityCollectionResponsePOMonth = BALFactory.dashboardBAL.GetTotalPOAmountMonthWise(dash1);
            if (baseEntityCollectionResponsePOMonth != null)
            {
                if (baseEntityCollectionResponsePOMonth != null && baseEntityCollectionResponsePOMonth.Count > 0)
                {
                    var jsonDashBoardDTOWO = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(baseEntityCollectionResponsePOMonth);
                    ViewBag.TotalWO = jsonDashBoardDTOWO;
                }
            }

            return Json(new { data = baseEntityCollectionResponsePOMonth }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTotalSampleMonthWise()
        {
            //DashBoard dash1 = new DashBoard();
            DashboardEntity dash1 = new DashboardEntity();
            dash1.Mode = "SELECT Total Sample Month GRAPH";
            var jsonDashBoardDTO = "";
            List<DashboardEntity> baseEntityCollectionResponseSampleMonth = BALFactory.dashboardBAL.GetTotalSampleMonthWise(dash1);
            if (baseEntityCollectionResponseSampleMonth != null)
            {
                if (baseEntityCollectionResponseSampleMonth != null && baseEntityCollectionResponseSampleMonth.Count > 0)
                {
                    jsonDashBoardDTO = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(baseEntityCollectionResponseSampleMonth);
                    ViewBag.SampleMonthItemViewBag = jsonDashBoardDTO;
                }
            }

            return Json(new { data = baseEntityCollectionResponseSampleMonth }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTotalQuotationMonthWise()
        {
            //DashBoard dash1 = new DashBoard();
            DashboardEntity dash1 = new DashboardEntity();
            dash1.Mode = "SELECT Quotation Sent";
            var jsonDashBoardDTO = "";
            List<DashboardEntity> baseEntityCollectionResponseQuotation = BALFactory.dashboardBAL.GetTotalQuotationMonthWise(dash1);
            if (baseEntityCollectionResponseQuotation != null)
            {
                if (baseEntityCollectionResponseQuotation != null && baseEntityCollectionResponseQuotation.Count > 0)
                {
                    jsonDashBoardDTO = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(baseEntityCollectionResponseQuotation);
                    ViewBag.SampleMonthItemViewBag = jsonDashBoardDTO;
                }
            }

            return Json(new { data = baseEntityCollectionResponseQuotation }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTotalPOAmountItemWise()
        {
            DashboardEntity dash1 = new DashboardEntity();
            dash1.Mode = "SELECT Purchase Total";

            List<DashboardEntity> baseEntityCollectionResponsePOItem = BALFactory.dashboardBAL.GetTotalPOAmountItemWise(dash1);
            if (baseEntityCollectionResponsePOItem != null)
            {
                if (baseEntityCollectionResponsePOItem != null && baseEntityCollectionResponsePOItem.Count > 0)
                {
                    var jsonDashBoardDTOWO = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(baseEntityCollectionResponsePOItem);
                    ViewBag.TotalWO = jsonDashBoardDTOWO;
                }
            }

            return Json(new { data = baseEntityCollectionResponsePOItem }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTotalWOComp()
        {
            DashboardEntity dash1 = new DashboardEntity();
            dash1.Mode = "SELECT WO Completed";

            List<DashboardEntity> baseEntityCollectionResponseWOComp = BALFactory.dashboardBAL.GetTotalWOComp(dash1);
            if (baseEntityCollectionResponseWOComp != null)
            {
                if (baseEntityCollectionResponseWOComp != null && baseEntityCollectionResponseWOComp.Count > 0)
                {
                    var jsonDashBoardDTOWO = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(baseEntityCollectionResponseWOComp);
                    ViewBag.TotalWO = jsonDashBoardDTOWO;
                }
            }
            return Json(new { data = baseEntityCollectionResponseWOComp }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTotalWOExe()
        {
            DashboardEntity dash1 = new DashboardEntity();
            dash1.Mode = "SELECT WO Executed";

            List<DashboardEntity> baseEntityCollectionResponseWOExe = BALFactory.dashboardBAL.GetTotalWOExe(dash1);
            if (baseEntityCollectionResponseWOExe != null)
            {
                if (baseEntityCollectionResponseWOExe != null && baseEntityCollectionResponseWOExe.Count > 0)
                {
                    var jsonDashBoardDTOWO = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(baseEntityCollectionResponseWOExe);
                    ViewBag.TotalWO = jsonDashBoardDTOWO;
                }
            }
            return Json(new { data = baseEntityCollectionResponseWOExe }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTotalSampleTestedMonthWise()
        {
            DashboardEntity dash1 = new DashboardEntity();
            dash1.Mode = "SELECT TOTAL Tested Month Wise";

            List<DashboardEntity> baseEntityCollectionResponseSampleTestedMonth = BALFactory.dashboardBAL.GetTotalSampleTestedMonthWise(dash1);
            if (baseEntityCollectionResponseSampleTestedMonth != null)
            {
                if (baseEntityCollectionResponseSampleTestedMonth != null && baseEntityCollectionResponseSampleTestedMonth.Count > 0)
                {
                    var jsonDashBoardDTOWO = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(baseEntityCollectionResponseSampleTestedMonth);
                    ViewBag.TotalWO = jsonDashBoardDTOWO;
                }
            }
            return Json(new { data = baseEntityCollectionResponseSampleTestedMonth }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTotalSampleTestedSampleTypeWise()
        {
            DashboardEntity dash1 = new DashboardEntity();
            dash1.Mode = "SELECT TOTAL Tested Type Wise";

            List<DashboardEntity> baseEntityCollectionResponseSampleTestedTypeWise = BALFactory.dashboardBAL.GetTotalSampleTestedSampleTypeWise(dash1);
            if (baseEntityCollectionResponseSampleTestedTypeWise != null)
            {
                if (baseEntityCollectionResponseSampleTestedTypeWise != null && baseEntityCollectionResponseSampleTestedTypeWise.Count > 0)
                {
                    var jsonDashBoardDTOWO = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(baseEntityCollectionResponseSampleTestedTypeWise);
                    ViewBag.TotalWO = jsonDashBoardDTOWO;
                }
            }
            return Json(new { data = baseEntityCollectionResponseSampleTestedTypeWise }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTotalSampleApproved()
        {
            DashboardEntity dash1 = new DashboardEntity();
            dash1.Mode = "SELECT TOTAL Approved";

            List<DashboardEntity> baseEntityCollectionResponseSampleApprovedTypeWise = BALFactory.dashboardBAL.GetTotalSampleApproved(dash1);
            if (baseEntityCollectionResponseSampleApprovedTypeWise != null)
            {
                if (baseEntityCollectionResponseSampleApprovedTypeWise != null && baseEntityCollectionResponseSampleApprovedTypeWise.Count > 0)
                {
                    var jsonDashBoardDTOWO = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(baseEntityCollectionResponseSampleApprovedTypeWise);
                    ViewBag.TotalWO = jsonDashBoardDTOWO;
                }
            }
            return Json(new { data = baseEntityCollectionResponseSampleApprovedTypeWise }, JsonRequestBehavior.AllowGet);
        }
    }
}