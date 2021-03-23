using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Core;
using LIMS_DEMO.Areas.Inventory.Models;
using LIMS_DEMO.Core.Inventory;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.DropDowns;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.DropDown;
using LIMS_DEMO.BAL.Inventory;

namespace LIMS_DEMO.Areas.Inventory.Controllers
{
    [RouteArea("Inventory")]
    public class InventoryCapacityMasterController : Controller
    {
        // GET: Inventory/InventoryCapacityMaster
        public InventoryCapacityMasterController()
        {
            BALFactory.NewInventoryBAL = new NewInventoryBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryCapacityMasterModel model)
        {
            CapacityEntity capacityEntity = new CapacityEntity();
            capacityEntity.Capacity = model.Capacity;
            capacityEntity.Description = model.Description;
            capacityEntity.IsActive = true;
            capacityEntity.InsertedBy = 0;
            capacityEntity.InsertedDate = DateTime.Now.ToLocalTime();
            string result = BALFactory.NewInventoryBAL.InsertCapacityMaster(capacityEntity);
            if (result == "success")
            {
                return RedirectToAction("List");
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Json(new { status = "success", message = "Record created Failed." });
            }
        }
        [HttpGet]
        public ActionResult Edit(int InventoryCapacityMasterId = 0)
        {
            InventoryCapacityMasterModel model = new InventoryCapacityMasterModel();
            if(InventoryCapacityMasterId != 0)
            {
                var result = BALFactory.NewInventoryBAL.SelectCapacityDataWithID(InventoryCapacityMasterId);
                if(result != null)
                {
                    model.Capacity = result.Capacity;
                    model.InventoryCapacityMasterId = result.InventoryCapacityMasterId;
                    model.Description = result.Description;
                    model.IsActive = result.IsActive;
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InventoryCapacityMasterModel model)
        {
            CapacityEntity capacityEntity = new CapacityEntity();
            capacityEntity.Capacity = model.Capacity;
            capacityEntity.InventoryCapacityMasterId = model.InventoryCapacityMasterId;
            capacityEntity.Description = model.Description;
            capacityEntity.IsActive = true;
            capacityEntity.ModifiedBy =0;
            capacityEntity.ModifiedDate = DateTime.Now.ToLocalTime();
            string result = BALFactory.NewInventoryBAL.UpdateCapacityData(capacityEntity);
            if(result == "success")
            {
                return RedirectToAction("List");
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Json(new { status = "success", message = "Record created Failed." });
            }     
        }
        [HttpGet]
        public ActionResult List()
        {
            List<InventoryCapacityMasterModel> CapacityList = new List<InventoryCapacityMasterModel>();
            CoreFactory.CapacityList = BALFactory.NewInventoryBAL.GetCapacityList();
            if(CoreFactory.CapacityList != null)
            {
                foreach (var item in CoreFactory.CapacityList)
                {
                    CapacityList.Add(new InventoryCapacityMasterModel()
                    { 
                    Capacity=item.Capacity,
                    InventoryCapacityMasterId=item.InventoryCapacityMasterId,
                    Description=item.Description,
                    IsActive=item.IsActive
                    });
                }
            }
            return View(CapacityList);
        }

        [HttpGet]
        public ActionResult View(int InventoryCapacityMasterId = 0)
        {
            InventoryCapacityMasterModel model = new InventoryCapacityMasterModel();
            if (InventoryCapacityMasterId != 0)
            {
                var result = BALFactory.NewInventoryBAL.SelectCapacityDataWithID(InventoryCapacityMasterId);
                if (result != null)
                {
                    model.Capacity = result.Capacity;
                    model.InventoryCapacityMasterId = result.InventoryCapacityMasterId;
                    model.Description = result.Description;
                    model.IsActive = result.IsActive;
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int InventoryCapacityMasterId = 0)
        {
            InventoryCapacityMasterModel model = new InventoryCapacityMasterModel();
            if (InventoryCapacityMasterId != 0)
            {
                var result = BALFactory.NewInventoryBAL.SelectCapacityDataWithID(InventoryCapacityMasterId);
                if (result != null)
                {
                    model.Capacity = result.Capacity;
                    model.InventoryCapacityMasterId = result.InventoryCapacityMasterId;
                    model.Description = result.Description;
                    model.IsActive = result.IsActive;
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(InventoryCapacityMasterModel model)
        {
            Core.Inventory.InventoryCapacityEntity  capacityEntity = new InventoryCapacityEntity();
            capacityEntity.InventoryCapacityMasterId = model.InventoryCapacityMasterId;
            var result = BALFactory.NewInventoryBAL.DeleteInvCapacity(capacityEntity);
            if (result == "success")
            {
                return RedirectToAction("List");
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return Json(new { status = "success", message = "Record Delete Failed." });
            }
        }
    }
}