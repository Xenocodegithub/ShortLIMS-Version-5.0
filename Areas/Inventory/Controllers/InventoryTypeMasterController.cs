using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Inventory.Models;
namespace LIMS_DEMO.Areas.Inventory.Controllers
{
    public class InventoryTypeMasterController : Controller
    {
        // GET: Inventory/InventoryTypeMaster
        public ActionResult Index()
        {
            return View();
        }
    }
}