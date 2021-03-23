using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Enquiry.Models;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Enquiry;
using LIMS_DEMO.BAL.DropDown;
namespace LIMS_DEMO.Areas.Enquiry.Controllers
{
    [RouteArea("Enquiry")]
    
    public class ContractController : Controller
    {
        string strStatus = "";
        public ContractController()
        {
            BALFactory.contractBAL = new ContractBAL();
            BALFactory.enquiryBAL = new EnquiryBAL();
            BALFactory.dropdownsBAL = new DropDownBal();
        }
        // GET: Enquiry/Contract
      
       
        public ActionResult AddContract(int? EnquiryId = 0)
        {
            ContractModel model = new ContractModel();//Model obj
            CoreFactory.enquiryEntity = BALFactory.enquiryBAL.GetDetails((Int32)EnquiryId);
            model.EnquiryId = CoreFactory.enquiryEntity.EnquiryId;
            model.CustomerName = CoreFactory.enquiryEntity.CustomerName;
            model.EnquiryOn = CoreFactory.enquiryEntity.EnquiryOn;
            var contractList = BALFactory.contractBAL.GetContractDetails((Int32)EnquiryId);
            IList<ContractModel> contractModelList = new List<ContractModel>();
            int iSrNo = 1;
            foreach (var item in contractList)
            {
                contractModelList.Add(new ContractModel()
                {
                    SerialNo = iSrNo,
                    EnquiryId = (long)EnquiryId,
                    EnquiryDetailId = item.EnquiryDetailId,
                    SampleTypeProductId = (Int32)item.SampleTypeProductId,
                    SampleTypeProductName = item.SampleTypeProductName,
                    //ProductGroupId = item.ProductGroupId,
                    //ProductGroupName = item.ProductGroupName,
                    //SubGroupId = item.SubGroupId,
                    //SubGroupName = item.SubGroupName,
                    //MatrixId = item.MatrixId,
                    //MatrixName = item.MatrixName
                });
                iSrNo++;
            }
            TempData["ContractList"] = contractModelList;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddContract(ContractModel model)
        {
            CoreFactory.contractEntity = new ContractEntity();
            var contractList = (List<ContractModel>)TempData.Peek("ContractList");
            if (contractList.Count > 0)
            {
                foreach (var contract in contractList)
                {
                    CoreFactory.contractEntity.EnquiryId = model.EnquiryId;
                    CoreFactory.contractEntity.EnquiryDetailId = contract.EnquiryDetailId;
                    //CoreFactory.contractEntity.ProductGroupId = contract.ProductGroupId;
                    //CoreFactory.contractEntity.SubGroupId = contract.SubGroupId;
                    //CoreFactory.contractEntity.MatrixId = contract.MatrixId;
                    CoreFactory.contractEntity.SampleTypeProductId = contract.SampleTypeProductId;
                    CoreFactory.contractEntity.EnteredBy = LIMS.User.UserMasterID;
                    if (contract.EnquiryDetailId == 0)
                    {
                        BALFactory.contractBAL.AddContract(CoreFactory.contractEntity);

                    }
                    else
                    {
                        BALFactory.contractBAL.UpdateContract(CoreFactory.contractEntity);
                    }
                }
                int iStatusId = BALFactory.dropdownsBAL.GetStatusIdByCode("ContSUBMIT");
                BALFactory.enquiryBAL.UpdateEnquiryStatus(model.EnquiryId, (byte)iStatusId);
                return Json(new { status = "success", EnquiryId = model.EnquiryId, message = "Added Sample " }, JsonRequestBehavior.AllowGet);
            }
             return Json(new { status = "Fail", EnquiryId = model.EnquiryId, message = "Can't Proceed " }, JsonRequestBehavior.AllowGet);

        }

     
       
        public PartialViewResult _Add(int EnquiryId, int? Id = 0)
        {
            ContractModel model = new ContractModel();
            if (Id != 0 && TempData["ContractList"] != null)
            {
                var contractList = (List<ContractModel>)TempData.Peek("ContractList");
                TempData.Keep("ContractList");
                model = contractList.Where(c => c.SerialNo == (Int32)Id).FirstOrDefault();
            }
            model.EnquiryId = (long)EnquiryId;
            ViewBag.SampleTypeProductList = BALFactory.dropdownsBAL.GetSampleTypeProduct();
            ViewBag.ProductGroupList = BALFactory.dropdownsBAL.GetProductGroup(0);//Passing Data to Viewbag for dropdown           
            ViewBag.SubGroupList = BALFactory.dropdownsBAL.GetSubGroup(0, model.ProductGroupId);//Passing Data to Viewbag for dropdown
            return PartialView(model);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult _Add(ContractModel model)
        {
            List<ContractModel> contractList = new List<ContractModel>();
            if (TempData["ContractList"] != null)
            {
                contractList = (List<ContractModel>)TempData["ContractList"];
            }
            if (model.SerialNo == 0)
            {
                model.SerialNo = contractList.Count() + 1;
                contractList.Add(model);
            }
            else
            {
                var contract = contractList.Where(c => c.SerialNo == model.SerialNo).FirstOrDefault();
                contract.SampleTypeProductId = model.SampleTypeProductId;
                contract.SampleTypeProductName = model.SampleTypeProductName;
                //contract.ProductGroupId = model.ProductGroupId;
                //contract.ProductGroupName = model.ProductGroupName;
                //contract.SubGroupId = model.SubGroupId;
                //contract.SubGroupName = model.SubGroupName;
            }
            TempData["ContractList"] = contractList;

             return Json(new { status = "success", message = "Added" });
       
        }

       
        [HttpGet]
        public PartialViewResult _ContractList()
        {
            List<ContractModel> model = new List<ContractModel>();
            if (TempData["ContractList"] != null)
            {
                model = (List<ContractModel>)TempData.Peek("ContractList");
                TempData.Keep("ContractList");
            }
            return PartialView(model);
        }
       
        public JsonResult _DeleteContract(int? Id = 0)
        {
            var contractList = (List<ContractModel>)TempData.Peek("ContractList");
            var contract = contractList.Where(c => c.SerialNo == (Int32)Id).FirstOrDefault();
            if (contract.EnquiryDetailId != 0)
            {
                BALFactory.contractBAL.DeleteContract(contract.EnquiryDetailId);
            }
            contractList.Remove(contract);
            int i = 1;
            foreach (var item in contractList)
            {
                item.SerialNo = i;
                i++;
            }
            TempData["ContractList"] = contractList;
            return Json(new { status = "success", message = "deleted" }, JsonRequestBehavior.AllowGet);
        }

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

    }
}