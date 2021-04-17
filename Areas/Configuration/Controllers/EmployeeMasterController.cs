using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Configuration;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core;
using LIMS_DEMO.Areas.Configuration.Models;


namespace LIMS_DEMO.Areas.Configuration.Controllers
{
    public class EmployeeMasterController : Controller
    {
        string strStatus = "";
        public EmployeeMasterController()
        {
            BALFactory.employeeMasterBAL = new EmployeeMasterBAL();
            BALFactory.dropdownsBAL = new BAL.DropDown.DropDownBal();
        }

        // GET: Configuration/EmployeeMaster
        public ActionResult AddEmployee(int? UserDetailId = 0)
        {
            EmployeeMasterModel model = new EmployeeMasterModel();
            if (UserDetailId != 0)
            {
                CoreFactory.employeeMasterEntity = BALFactory.employeeMasterBAL.GetDetails((Int32)UserDetailId);
                if (CoreFactory.employeeMasterEntity != null)
                {
                    model.UserDetailId = CoreFactory.employeeMasterEntity.UserDetailId;
                    model.FirstName = CoreFactory.employeeMasterEntity.FirstName;
                    model.MiddleName = CoreFactory.employeeMasterEntity.MiddleName;
                    model.LastName = CoreFactory.employeeMasterEntity.LastName;
                    model.UserMasterId = CoreFactory.employeeMasterEntity.UserMasterId;
                    model.MasterDesignationID = CoreFactory.employeeMasterEntity.MasterDesignationID;
                    model.Gender = CoreFactory.employeeMasterEntity.Gender;
                    model.DateOfBirth = CoreFactory.employeeMasterEntity.DateOfBirth;
                    model.Email = CoreFactory.employeeMasterEntity.Email;
                    model.Mobile = CoreFactory.employeeMasterEntity.Mobile;
                    model.EmployeeCode = CoreFactory.employeeMasterEntity.EmployeeCode;
                    model.AreaOfExpertise = CoreFactory.employeeMasterEntity.AreaOfExpertise;
                    model.MiddleName = CoreFactory.employeeMasterEntity.MiddleName;
                    model.ExperienceInYear = CoreFactory.employeeMasterEntity.ExperienceInYear;
                    model.Qualification = CoreFactory.employeeMasterEntity.Qualification;
                    model.Address1 = CoreFactory.employeeMasterEntity.Address1;
                    model.Address2 = CoreFactory.employeeMasterEntity.Address2;
                    model.Area = CoreFactory.employeeMasterEntity.Area;
                    model.CityName = CoreFactory.employeeMasterEntity.CityName;
                    model.StateName = CoreFactory.employeeMasterEntity.StateName;
                    model.CountryName = CoreFactory.employeeMasterEntity.CountryName;
                    model.Pincode = CoreFactory.employeeMasterEntity.Pincode;
                    model.AlternateNumber = CoreFactory.employeeMasterEntity.AlternateNumber;
                    model.PANNo = CoreFactory.employeeMasterEntity.PANNo;
                    model.AdharNo = CoreFactory.employeeMasterEntity.AdharNo;
                    model.IsActive = CoreFactory.employeeMasterEntity.IsActive;
                }
            }
            ViewBag.DesignationList = BALFactory.dropdownsBAL.GetRole();
            ViewBag.UserMasterList = BALFactory.dropdownsBAL.GetUserMasterList();

            return View(model);
        }
        
        [HttpPost]
        public ActionResult AddEmployee(EmployeeMasterModel model)
        {
            EmployeeMasterEntity employeeMasterEntity = new EmployeeMasterEntity();
            employeeMasterEntity.UserDetailId = model.UserDetailId;
            employeeMasterEntity.FirstName = model.FirstName;
            employeeMasterEntity.MiddleName = model.MiddleName;
            employeeMasterEntity.LastName = model.LastName;
            employeeMasterEntity.UserMasterId = model.UserMasterId;
            employeeMasterEntity.Gender = model.Gender;
            employeeMasterEntity.DateOfBirth = model.DateOfBirth;
            employeeMasterEntity.Email = model.Email;
            employeeMasterEntity.Mobile = model.Mobile;
            employeeMasterEntity.AlternateNumber = model.AlternateNumber;
            employeeMasterEntity.EmployeeCode = model.EmployeeCode;
            employeeMasterEntity.AreaOfExpertise = model.AreaOfExpertise;
            employeeMasterEntity.ExperienceInYear = model.ExperienceInYear;
            employeeMasterEntity.MasterDesignationID = model.MasterDesignationID;
            employeeMasterEntity.Qualification = model.Qualification;
            employeeMasterEntity.Address1 = model.Address1;
            employeeMasterEntity.Address2 = model.Address2;
            employeeMasterEntity.Area = model.Area;
            employeeMasterEntity.CityName = model.CityName;
            employeeMasterEntity.StateName = model.StateName;
            employeeMasterEntity.CountryName = model.CountryName;
            employeeMasterEntity.Pincode = model.Pincode;
            employeeMasterEntity.AlternateNumber = model.AlternateNumber;
            employeeMasterEntity.PANNo = model.PANNo;
            employeeMasterEntity.AdharNo = model.AdharNo;
            //employeeMasterEntity.EnteredBy = LIMS.User.UserMasterID;
            employeeMasterEntity.EnteredDate = DateTime.Now;
            employeeMasterEntity.IsActive = model.IsActive;

            if (model.UserDetailId == 0)
            {
                strStatus = BALFactory.employeeMasterBAL.AddEmployee(employeeMasterEntity);
                return Json(new { Status = strStatus, message = "Employee has been registered successfully." }, JsonRequestBehavior.AllowGet);
            }
            //Use for update
            else
            {
                strStatus = BALFactory.employeeMasterBAL.Update(employeeMasterEntity);
                return Json(new { Status = strStatus, message = "Employee has been updated successfully." }, JsonRequestBehavior.AllowGet);
            }
            //return RedirectToAction("EmployeeMasterList");
        }
        public ActionResult EmployeeMasterList()
        {
            CoreFactory.employeeMasterList = BALFactory.employeeMasterBAL.GetEmployeeMasterList();
            List<EmployeeMasterModel> modelList = new List<EmployeeMasterModel>();
            foreach (var Item in CoreFactory.employeeMasterList)
            {
                if (Item.MasterDesignation != null && Item.UserID != null)
                {
                    modelList.Add(new EmployeeMasterModel()
                    {
                        UserDetailId = Item.UserDetailId,
                        Salutation = Item.Salutation,
                        FirstName = Item.FirstName,
                        MiddleName = Item.MiddleName,
                        LastName = Item.LastName,
                        UserName = Item.UserName,
                        MasterDesignation = Item.MasterDesignation,
                        Gender = Item.Gender,
                        DateOfBirth = Item.DateOfBirth,
                        dbo = Item.DateOfBirth == null ? "" : Convert.ToDateTime(Item.DateOfBirth).ToString("dd/MM/yyyy"),
                        Email = Item.Email,
                        Mobile = Item.Mobile,
                        EmployeeCode = Item.EmployeeCode,
                        AreaOfExpertise = Item.AreaOfExpertise,
                        ExperienceInYear = Item.ExperienceInYear,
                        Qualification = Item.Qualification,
                        Address1 = Item.Address1,
                        Address2 = Item.Address2,
                        Area = Item.Area,
                        CityName = Item.CityName,
                        StateName = Item.StateName,
                        CountryName = Item.CountryName,
                        Pincode = Item.Pincode,
                        AlternateNumber = Item.AlternateNumber,
                        ContactPersonName = Item.ContactPersonName,
                        PANNo = Item.PANNo,
                        AdharNo = Item.AdharNo,
                        //DisposedDay = Item.DisposedDay,
                        IsActive = Item.IsActive
                    });
                }
                else
                {
                    modelList.Add(new EmployeeMasterModel()
                    {
                        UserDetailId = Item.UserDetailId,
                        Salutation = Item.Salutation,
                        FirstName = Item.FirstName,
                        MiddleName = Item.MiddleName,
                        LastName = Item.LastName,
                        //UserName = Item.UserName,
                        MasterDesignation = null,
                        Gender = Item.Gender,
                        DateOfBirth = Item.DateOfBirth,
                        dbo = Item.DateOfBirth == null ? "" : Convert.ToDateTime(Item.DateOfBirth).ToString("dd/MM/yyyy"),
                        Email = Item.Email,
                        Mobile = Item.Mobile,
                        EmployeeCode = Item.EmployeeCode,
                        AreaOfExpertise = Item.AreaOfExpertise,
                        ExperienceInYear = Item.ExperienceInYear,
                        Qualification = Item.Qualification,
                        Address1 = Item.Address1,
                        Address2 = Item.Address2,
                        Area = Item.Area,
                        CityName = Item.CityName,
                        StateName = Item.StateName,
                        CountryName = Item.CountryName,
                        Pincode = Item.Pincode,
                        AlternateNumber = Item.AlternateNumber,
                        ContactPersonName = Item.ContactPersonName,
                        PANNo = Item.PANNo,
                        AdharNo = Item.AdharNo,
                        //DisposedDay = Item.DisposedDay,
                        IsActive = Item.IsActive
                    });
                }
            }
            return View(modelList);
        }
    }
}