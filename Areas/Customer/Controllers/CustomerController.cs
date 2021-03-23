using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.Areas.Customer.Models;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Customer;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Customer;


namespace LIMS_DEMO.Areas.Customer.Controllers
{
    public class CustomerController : Controller
    {
        string strStatus = "";
        public CustomerController()
        {
            BALFactory.customerBAL = new CustomerBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }
        // GET: Customer/Customer
        public ActionResult CustomerList()
        {
            CoreFactory.customerList = BALFactory.customerBAL.GetCustomerList();
            List<CustomerModel> modelList = new List<CustomerModel>();
            foreach (var Item in CoreFactory.customerList)
            {
                modelList.Add(new CustomerModel()
                {
                    CustomerMasterId = Item.CustomerMasterId,
                    RegistrationName = Item.RegistrationName,
                    ContactPersonName = Item.ContactPersonName,
                    Address1 = Item.Address1,
                    CityName = Item.CityName,
                    StateName = Item.StateName,
                    CountryName = Item.CountryName,
                    ContactMobileNo = Item.ContactMobileNo,
                    IsActive = Item.IsActive
                });
            }
            return View(modelList);
        }
        public ActionResult AddCustomer(int? CustomerMasterId = 0)
        {
            CustomerModel model = new CustomerModel();
            if (CustomerMasterId != 0)
            {
                CoreFactory.customerEntity = BALFactory.customerBAL.GetCustomerDetails((Int32)CustomerMasterId);
                if (CoreFactory.customerEntity != null)
                {
                    model.CustomerMasterId = CoreFactory.customerEntity.CustomerMasterId;
                    model.CustomerTypeId = CoreFactory.customerEntity.CustomerTypeId;
                    model.CustomerGroupId = CoreFactory.customerEntity.CustomerGroupId;
                    model.CompanyTypeId = CoreFactory.customerEntity.CompanyTypeId;
                    model.RegistrationName = CoreFactory.customerEntity.RegistrationName;
                    model.RegistrationDate = CoreFactory.customerEntity.RegistrationDate;
                    model.ContactPersonName = CoreFactory.customerEntity.ContactPersonName;
                    model.ContactMobileNo = CoreFactory.customerEntity.ContactMobileNo;
                    model.ContactDesignation = CoreFactory.customerEntity.ContactDesignation;
                    model.ContactEmail = CoreFactory.customerEntity.ContactEmail;
                    model.Address1 = CoreFactory.customerEntity.Address1;
                    model.WardNo = CoreFactory.customerEntity.WardNo;
                    model.Taluka = CoreFactory.customerEntity.Taluka;
                    model.Village = CoreFactory.customerEntity.Village;
                    model.LandMark = CoreFactory.customerEntity.Area;
                    model.CityName = CoreFactory.customerEntity.CityName;
                    model.StateName = CoreFactory.customerEntity.StateName;
                    model.CountryName = CoreFactory.customerEntity.CountryName;
                    model.Pincode = CoreFactory.customerEntity.Pincode;
                    model.PhoneNo = CoreFactory.customerEntity.PhoneNo;
                    model.MobileNo = CoreFactory.customerEntity.MobileNo;
                    model.Email = CoreFactory.customerEntity.Email;
                    model.PANNo = CoreFactory.customerEntity.PANNo;
                    model.PANUpload = CoreFactory.customerEntity.PANUpload;
                    model.AddressProofUpload = CoreFactory.customerEntity.AddressProofUpload;
                    model.CINNumber = CoreFactory.customerEntity.CINNumber;
                    model.FaxNo = CoreFactory.customerEntity.FaxNo;
                    model.GSTNo = CoreFactory.customerEntity.GSTNo;
                    model.IsActive = CoreFactory.customerEntity.IsActive;
                }
            }
            else
            {
                model.IsActive = true;
            }
            ViewBag.CustomerTypes = BALFactory.customerBAL.GetCustomerTypeList();
            ViewBag.CompanyTypes = BALFactory.customerBAL.GetCompanyTypeList();
            ViewBag.CustomerGroups = BALFactory.customerBAL.GetCustomerGroupList();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddCustomer(CustomerModel model)
        {
            CoreFactory.customerEntity = new CustomerEntity();
            CoreFactory.customerEntity.CustomerMasterId = model.CustomerMasterId;
            CoreFactory.customerEntity.CustomerGroupId = (byte)model.CustomerGroupId;
            CoreFactory.customerEntity.CustomerTypeId = (byte)model.CustomerTypeId;
            CoreFactory.customerEntity.CompanyTypeId = (byte)model.CompanyTypeId;
            CoreFactory.customerEntity.RegistrationName = model.RegistrationName;
            CoreFactory.customerEntity.RegistrationDate = model.RegistrationDate;
            CoreFactory.customerEntity.ContactPersonName = model.ContactPersonName;
            CoreFactory.customerEntity.ContactDesignation = model.ContactDesignation;
            CoreFactory.customerEntity.ContactMobileNo = model.ContactMobileNo;
            CoreFactory.customerEntity.ContactEmail = model.ContactEmail;
            CoreFactory.customerEntity.Address1 = model.Address1;
            CoreFactory.customerEntity.WardNo = model.WardNo;
            CoreFactory.customerEntity.Taluka = model.Taluka;
            CoreFactory.customerEntity.Village = model.Village;
            CoreFactory.customerEntity.Area = model.LandMark;
            CoreFactory.customerEntity.CityName = model.CityName;
            CoreFactory.customerEntity.StateName = model.StateName;
            CoreFactory.customerEntity.CountryName = model.CountryName;
            CoreFactory.customerEntity.Pincode = model.Pincode;
            CoreFactory.customerEntity.PhoneNo = model.PhoneNo;
            CoreFactory.customerEntity.MobileNo = model.MobileNo;
            CoreFactory.customerEntity.Email = model.Email;
            CoreFactory.customerEntity.PANNo = model.PANNo;
            CoreFactory.customerEntity.PANUpload = model.PANUpload;
            CoreFactory.customerEntity.AddressProofUpload = model.AddressProofUpload;
            CoreFactory.customerEntity.CINNumber = model.CINNumber;
            CoreFactory.customerEntity.FaxNo = model.FaxNo;
            CoreFactory.customerEntity.GSTNo = model.GSTNo;
            CoreFactory.customerEntity.EnteredBy = 1;
            CoreFactory.customerEntity.EnteredDate = DateTime.Now;
            CoreFactory.customerEntity.IsActive = true;
            if (model.CustomerMasterId == 0)
            {
                strStatus = BALFactory.customerBAL.AddCustomer(CoreFactory.customerEntity);
                //return Json(new { Status = strStatus, message = "Customer has been registered successfully." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                strStatus = BALFactory.customerBAL.UpdateCustomer(CoreFactory.customerEntity);
                //return Json(new { Status = strStatus, message = "Customer has been updated successfully." }, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("CustomerList");

        }
    }
}