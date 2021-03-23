using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LIMS_DEMO.Areas.Configuration.Controllers
{
    public class CountryMasterController : Controller
    {
        // GET: Configuration/CountryMaster
        public ActionResult AddCountry()
        {
            return View();
        }
    }
}