using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Inventory.Models;

namespace LIMS_DEMO.Areas.Inventory.Controllers
{
    [RouteArea("Inventory")]
    public class InventoryMaintainanceAndCalibrationBreakDownController : Controller
    {
        // GET: Inventory/InventoryMaintainanceAndCalibrationBreakDown
        public ActionResult Index()
        {
            return View();
        }
      
    }
}
