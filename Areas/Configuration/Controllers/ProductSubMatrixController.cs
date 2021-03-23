using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Configuration.Models;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Configuration;
using LIMS_DEMO.BAL.DropDown;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Configuration;

namespace LIMS_DEMO.Areas.Configuration.Controllers
{
    public class ProductSubMatrixController : Controller
    {
        string strStatus = "";
        public ProductSubMatrixController()
        {
            BALFactory.productSubMatrixBAL = new ProductSubMatrixBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
        }
        //-------------------------------------Sample Type---------------------------------
        #region Sample Type
        public ActionResult AddSampleTypeProduct(int? SampleTypeProductId = 0)
        {
            ProductGroupMasterModel model = new ProductGroupMasterModel();
            if (SampleTypeProductId != 0)
            {
                CoreFactory.productSubMatrixEntity = BALFactory.productSubMatrixBAL.GetDetailsSTP((Int32)SampleTypeProductId);
                if (CoreFactory.productSubMatrixEntity != null)
                {
                    model.SampleTypeProductId = (Int32)CoreFactory.productSubMatrixEntity.SampleTypeProductId;
                    model.SampleTypeProductName = CoreFactory.productSubMatrixEntity.SampleTypeProductName;
                    model.SampleTypeProductCode = CoreFactory.productSubMatrixEntity.SampleTypeProductCode;
                    model.IsActive = CoreFactory.productSubMatrixEntity.IsActive;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddSampleTypeProduct(ProductGroupMasterModel model)
        {
            CoreFactory.productSubMatrixEntity = new ProductSubMatrixEntity();

            CoreFactory.productSubMatrixEntity.SampleTypeProductId = model.SampleTypeProductId;
            CoreFactory.productSubMatrixEntity.SampleTypeProductName = model.SampleTypeProductName;
            CoreFactory.productSubMatrixEntity.SampleTypeProductCode = model.SampleTypeProductCode;
            //CoreFactory.productSubMatrixEntity.EnteredBy = LIMS.User.UserMasterID;
            CoreFactory.productSubMatrixEntity.EnteredDate = DateTime.Now;
            CoreFactory.productSubMatrixEntity.IsActive = model.IsActive;

            if (model.SampleTypeProductId == 0)
            {
                strStatus = BALFactory.productSubMatrixBAL.AddSampleTypeProduct(CoreFactory.productSubMatrixEntity);
                //return Json(new { Status = strStatus, message = "Sample Type Product has been registered successfully." }, JsonRequestBehavior.AllowGet);
                
            }
            //Use for update
            else
            {
                strStatus = BALFactory.productSubMatrixBAL.UpdateSTP(CoreFactory.productSubMatrixEntity);
                //return Json(new { Status = strStatus, message = "Sample Type Product has been updated successfully." }, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("SampleTypeProductList");
        }
        public ActionResult DeleteSTP(int? SampleTypeProductId = 0)
        {
            strStatus = BALFactory.productSubMatrixBAL.DeleteSTP((Int32)SampleTypeProductId);
            if (strStatus == "success")
            {
                TempData["message"] = "Customer has been deleted successfully.";
            }
            return RedirectToAction("SampleTypeProductList");
        }
        public ActionResult SampleTypeProductList()
        {
            CoreFactory.productGroup1List = BALFactory.productSubMatrixBAL.GetSampleTypeProductList();
            List<ProductGroupMasterModel> modelList = new List<ProductGroupMasterModel>();
            foreach (var Item in CoreFactory.productGroup1List)
            {
                modelList.Add(new ProductGroupMasterModel()
                {
                    SampleTypeProductId = (Int32)Item.SampleTypeProductId,
                    SampleTypeProductName = Item.SampleTypeProductName,
                    //DisposedDay = Item.DisposedDay,
                    SampleTypeProductCode = Item.SampleTypeProductCode,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }
        #endregion
        //-------------------------------------Product Group-------------------------------
        #region Product Group
        public ActionResult AddProductGroup(int? SampleTypeProductId = 0, int? ProductGroupId = 0)
        {
            ProcedureMasterModel model = new ProcedureMasterModel();
            if (ProductGroupId != 0)
            {
                CoreFactory.productSubMatrixEntity = BALFactory.productSubMatrixBAL.GetDetailsPG((Int32)SampleTypeProductId, (Int32)ProductGroupId);
                if (CoreFactory.productSubMatrixEntity != null)
                {
                    model.SampleTypeProductId = (Int32)SampleTypeProductId;
                    model.ProductGroupId = CoreFactory.productSubMatrixEntity.ProductGroupId;
                    model.ProductGroupName = CoreFactory.productSubMatrixEntity.ProductGroupName;
                    model.Code = CoreFactory.productSubMatrixEntity.Code;
                    model.IsActive = CoreFactory.productSubMatrixEntity.IsActive;
                }
            }

            ViewBag.SampleTypeProductList = BALFactory.dropdownsBAL.GetSampleTypeProduct();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddProductGroup(ProductGroupMasterModel model)
        {
            CoreFactory.productSubMatrixEntity = new ProductSubMatrixEntity();

            CoreFactory.productSubMatrixEntity.ProductGroupId = model.ProductGroupId;
            CoreFactory.productSubMatrixEntity.ProductGroupName = model.ProductGroupName;
            CoreFactory.productSubMatrixEntity.SampleTypeProductId = model.SampleTypeProductId;
            CoreFactory.productSubMatrixEntity.Code = model.Code;
            //CoreFactory.productSubMatrixEntity.EnteredBy = LIMS.User.UserMasterID;
            CoreFactory.productSubMatrixEntity.EnteredDate = DateTime.Now;
            CoreFactory.productSubMatrixEntity.IsActive = model.IsActive;

            if (model.ProductGroupId == 0)
            {
                strStatus = BALFactory.productSubMatrixBAL.AddProductGroup(CoreFactory.productSubMatrixEntity);
               // return Json(new { Status = strStatus, message = "ProductGroup has been registered successfully." }, JsonRequestBehavior.AllowGet);
            }
            //Use for update
            else
            {
                strStatus = BALFactory.productSubMatrixBAL.UpdatePG(CoreFactory.productSubMatrixEntity);
               // return Json(new { Status = strStatus, message = "ProductGroup has been updated successfully." }, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("ProductGroupList");
        }
        public ActionResult ProductGroupList()
        {
            CoreFactory.productGroup1List = BALFactory.productSubMatrixBAL.GetProductGroupList();
            List<ProcedureMasterModel> modelList = new List<ProcedureMasterModel>();
            foreach (var Item in CoreFactory.productGroup1List)
            {
                modelList.Add(new ProcedureMasterModel()
                {
                    ProductGroupId = Item.ProductGroupId,
                    SampleTypeProductId = (Int32)Item.SampleTypeProductId,
                    SampleTypeProductName = Item.SampleTypeProductName,
                    ProductGroupName = Item.ProductGroupName,
                    //DisposedDay = Item.DisposedDay,
                    Code = Item.Code,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }
        public ActionResult DeletePG(int ProductGroupId, int? SampleTypeProductId = 0)
        {
            strStatus = BALFactory.productSubMatrixBAL.DeletePG((Int32)SampleTypeProductId, (Int32)ProductGroupId);
            if (strStatus == "success")
            {
                TempData["message"] = "Customer has been deleted successfully.";
            }
            return RedirectToAction("ProductGroupList");
        }
        #endregion
        //-------------------------------------Sub Group-----------------------------------
        #region SubGroup

        public ActionResult AddSubGroup(int? SampleTypeProductId = 0, int? ProductGroupId = 0, int? SubgroupId = 0)
        {
            SubGroupMasterModel model = new SubGroupMasterModel();
            if (ProductGroupId != 0)
            {
                CoreFactory.productSubMatrixEntity = BALFactory.productSubMatrixBAL.GetDetailsSG((Int32)SampleTypeProductId, (Int32)ProductGroupId, (Int32)SubgroupId);
                if (CoreFactory.productSubMatrixEntity != null)
                {

                    model.ProductGroupId = CoreFactory.productSubMatrixEntity.ProductGroupId;
                    model.SubGroupId = CoreFactory.productSubMatrixEntity.SubGroupId;
                    model.SampleTypeProductId = (Int32)CoreFactory.productSubMatrixEntity.SampleTypeProductId;
                    model.SubGroupPrintCode = CoreFactory.productSubMatrixEntity.SubGroupCode;
                    model.SubGroupName = CoreFactory.productSubMatrixEntity.SubGroupName;

                    model.IsActive = CoreFactory.productSubMatrixEntity.IsActive;
                }
            }
            ViewBag.SampleTypeProductList = BALFactory.dropdownsBAL.GetSampleTypeProduct();
            ViewBag.ProductGroupList = BALFactory.dropdownsBAL.GetProductGroup((Int32)SampleTypeProductId);
            return View();
        }

        [HttpPost]
        public JsonResult AddSubGroup(SubGroupMasterModel model)
        {
            CoreFactory.productSubMatrixEntity = new ProductSubMatrixEntity();
            CoreFactory.productSubMatrixEntity.SubGroupId = model.SubGroupId;
            CoreFactory.productSubMatrixEntity.SampleTypeProductId = model.SampleTypeProductId;
            CoreFactory.productSubMatrixEntity.ProductGroupId = model.ProductGroupId;
            CoreFactory.productSubMatrixEntity.SubGroupName = model.SubGroupName;
            CoreFactory.productSubMatrixEntity.SubGroupCode = model.SubGroupPrintCode;
            //CoreFactory.productSubMatrixEntity.EnteredBy = LIMS.User.UserMasterID;
            CoreFactory.productSubMatrixEntity.EnteredDate = DateTime.Now;
            CoreFactory.productSubMatrixEntity.IsActive = model.IsActive;

            if (model.SubGroupId == 0)
            {
                strStatus = BALFactory.productSubMatrixBAL.AddSubGroup(CoreFactory.productSubMatrixEntity);
                return Json(new { Status = strStatus, message = "SubGroup has been registered successfully."}, JsonRequestBehavior.AllowGet);
            }
            //Use for update
            else
            {
                strStatus = BALFactory.productSubMatrixBAL.UpdateSG(CoreFactory.productSubMatrixEntity);
                return Json(new { Status = strStatus, message = "SubGroup has been updated successfully." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SubGroupList()
        {
            CoreFactory.subGroup1List = BALFactory.productSubMatrixBAL.GetSubGroupList();
            List<SubGroupMasterModel> modelList = new List<SubGroupMasterModel>();
            foreach (var Item in CoreFactory.subGroup1List)
            {
                modelList.Add(new SubGroupMasterModel()
                {
                    ProductGroupName = Item.ProductGroupName,
                    SubGroupName = Item.SubGroupName,
                    SubGroupId = Item.SubGroupId,
                    SampleTypeProductId = (Int32)Item.SampleTypeProductId,
                    SampleTypeProductName = Item.SampleTypeProductName,
                    ProductGroupId = Item.ProductGroupId,
                    SubGroupPrintCode = Item.SubGroupCode,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }
        public ActionResult DeleteSG(int? SampleTypeProductId = 0, int? ProductGroupId = 0, int? SubGroupId = 0)
        {
            strStatus = BALFactory.productSubMatrixBAL.DeleteSG((Int32)SampleTypeProductId, (Int32)ProductGroupId, (Int32)SubGroupId);
            if (strStatus == "success")
            {
                TempData["message"] = "Customer has been deleted successfully.";
            }
            return RedirectToAction("SubGroupList");
        }

        #endregion
        //-------------------------------------Matrix--------------------------------------
        #region Matrix
        public ActionResult AddMatrix(int? SampleTypeProductId = 0, int? ProductGroupId = 0, int? SubgroupId = 0, int? MatrixId = 0)
        {
            MatrixMasterModel model = new MatrixMasterModel();
            if (ProductGroupId != 0)
            {
                CoreFactory.productSubMatrixEntity = BALFactory.productSubMatrixBAL.GetDetailsMatrix((Int32)SampleTypeProductId, (Int32)ProductGroupId, (Int32)SubgroupId, (Int32)MatrixId);
                if (CoreFactory.productSubMatrixEntity != null)
                {

                    model.ProductGroupId = CoreFactory.productSubMatrixEntity.ProductGroupId;
                    model.SubGroupId = CoreFactory.productSubMatrixEntity.SubGroupId;
                    model.SampleTypeProductId = (Int32)CoreFactory.productSubMatrixEntity.SampleTypeProductId;
                    model.MatrixId = CoreFactory.productSubMatrixEntity.MatrixId;
                    model.MatrixName = CoreFactory.productSubMatrixEntity.MatrixName;
                    model.MatrixCode = CoreFactory.productSubMatrixEntity.MatrixCode;
                    model.IsActive = CoreFactory.productSubMatrixEntity.IsActive;
                }
            }

            ViewBag.SampleTypeProductList = BALFactory.dropdownsBAL.GetSampleTypeProduct();
            ViewBag.ProductGroupList = BALFactory.dropdownsBAL.GetProductGroup((Int32)SampleTypeProductId);
            ViewBag.SubGroupList = BALFactory.dropdownsBAL.GetSubGroup((Int32)SampleTypeProductId, (Int32)ProductGroupId);

            return View(model);
        }
        [HttpPost]
        public ActionResult AddMatrix(MatrixMasterModel model)
        {
            CoreFactory.productSubMatrixEntity = new ProductSubMatrixEntity();

            CoreFactory.productSubMatrixEntity.MatrixId = model.MatrixId;
            CoreFactory.productSubMatrixEntity.ProductGroupId = model.ProductGroupId;
            CoreFactory.productSubMatrixEntity.SubGroupId = model.SubGroupId;
            CoreFactory.productSubMatrixEntity.SampleTypeProductId = model.SampleTypeProductId;
            CoreFactory.productSubMatrixEntity.MatrixName = model.MatrixName;
            CoreFactory.productSubMatrixEntity.MatrixCode = model.MatrixCode;
            CoreFactory.productSubMatrixEntity.EnteredBy = LIMS.User.UserMasterID;
            CoreFactory.productSubMatrixEntity.EnteredDate = DateTime.Now;
            CoreFactory.productSubMatrixEntity.IsActive = model.IsActive;

            if (model.MatrixId == 0)
            {
                strStatus = BALFactory.productSubMatrixBAL.AddMatrix(CoreFactory.productSubMatrixEntity);
                //return Json(new { Status = strStatus, message = "Matrix has been registered successfully." }, JsonRequestBehavior.AllowGet);
            }

            //Use for update
            else
            {
                strStatus = BALFactory.productSubMatrixBAL.UpdateMatrix(CoreFactory.productSubMatrixEntity);
                //return Json(new { Status = strStatus, message = "Matrix has been updated successfully." }, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("MatrixList");
        }
        public ActionResult DeleteMatrix(int? SampleTypeProductId = 0, int? ProductGroupId = 0, int? SubGroupId = 0, int? MatrixId = 0)
        {
            strStatus = BALFactory.productSubMatrixBAL.DeleteMatrix((Int32)SampleTypeProductId, (Int32)ProductGroupId, (Int32)SubGroupId, (Int32)MatrixId);
            if (strStatus == "success")
            {
                TempData["message"] = "Customer has been deleted successfully.";
            }
            return RedirectToAction("MatrixList");
        }
        public ActionResult MatrixList()
        {
            List<MatrixMasterModel> modelList = new List<MatrixMasterModel>();
            CoreFactory.matrix1List = BALFactory.productSubMatrixBAL.GetMatrixList();
            foreach (var Item in CoreFactory.matrix1List)
            {
                modelList.Add(new MatrixMasterModel()
                {
                    ProductGroupName = Item.ProductGroupName,
                    SubGroupName = Item.SubGroupName,
                    ProductGroupId = Item.ProductGroupId,
                    SubGroupId = Item.SubGroupId,
                    SampleTypeProductId = (Int32)Item.SampleTypeProductId,
                    SampleTypeProductName = Item.SampleTypeProductName,
                    MatrixId = Item.MatrixId,
                    MatrixName = Item.MatrixName,
                    MatrixCode = Item.MatrixCode,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }
        #endregion
        //-------------------------------------AJAX CALL--------------------------------------

        public JsonResult ProductGroups(int? SampleTypeProductId = 0)
        {
            var productgroupList = BALFactory.dropdownsBAL.GetProductGroup((Int32)SampleTypeProductId);
            return Json(new { result = productgroupList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubGroups(int? SampleTypeProductId = 0, int? ProductGroupId = 0)
        {
            var subgroupList = BALFactory.dropdownsBAL.GetSubGroup((Int32)SampleTypeProductId, (Int32)ProductGroupId);
            return Json(new { result = subgroupList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Matrix(int? SampleTypeProductId = 0, int? ProductGroupId = 0, int? SubGroupId = 0)
        {
            var MatrixList = BALFactory.dropdownsBAL.GetMatrix((Int32)SampleTypeProductId, (Int32)ProductGroupId, (Int32)SubGroupId);
            return Json(new { result = MatrixList }, JsonRequestBehavior.AllowGet);
        }
    }
}