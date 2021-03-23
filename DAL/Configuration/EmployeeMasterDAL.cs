using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Repository;

namespace LIMS_DEMO.DAL.Configuration
{
    public class EmployeeMasterDAL:IEmployeeMaster
    {
        readonly LIMSEntities _dbContext;
        public EmployeeMasterDAL()
        {
            _dbContext = new LIMSEntities();
        }
        public List<EmployeeMasterEntity> GetEmployeeMasterList()
        {
            try
            {

                return (from e in _dbContext.UserDetails
                        join um in _dbContext.UserMasters on e.UserMasterID equals um.UserMasterID
                        select new EmployeeMasterEntity()
                        {
                            UserMasterId = e.UserMasterID,
                            UserName = um.UserName,
                            UserDetailId = e.UserDetailID,
                            Salutation = e.Salutation,
                            FirstName = e.FirstName,
                            MiddleName = e.MiddleName,
                            LastName = e.LastName,
                            Gender = e.Gender,
                            DateOfBirth = e.DateOfBirth,
                            Email = e.Email,
                            Mobile = e.Mobile,
                            AlternateNumber = e.AlternateNumber,
                            EmployeeCode = e.EmployeeCode,
                            MasterDesignationID = e.MasterDesignationID,
                            //MasterDesignation = r.RoleName,
                            AreaOfExpertise = e.AreaOfExpertise,
                            ExperienceInYear = e.ExperienceInYear,
                            Qualification = e.Qualification,
                            Address1 = e.Address1,
                            Address2 = e.Address2,
                            Area = e.Area,
                            CityName = e.CityName,
                            StateName = e.StateName,
                            CountryName = e.CountryName,
                            Pincode = e.Pincode,
                            PANNo = e.PANNo,
                            AdharNo = e.AdharNo,
                            IsActive = e.IsActive,
                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public EmployeeMasterEntity GetDetails(int UserDetailId)
        {
            return _dbContext.UserDetails.Where(u => u.UserDetailID == UserDetailId).Select(u => new EmployeeMasterEntity()
            {
                UserDetailId = u.UserDetailID,
                FirstName = u.FirstName,
                MiddleName = u.MiddleName,
                LastName = u.LastName,
                UserMasterId = u.UserMasterID,
                Gender = u.Gender,
                DateOfBirth = u.DateOfBirth,
                Email = u.Email,
                Mobile = u.Mobile,
                EmployeeCode = u.EmployeeCode,
                AreaOfExpertise = u.AreaOfExpertise,
                ExperienceInYear = u.ExperienceInYear,
                MasterDesignationID = u.MasterDesignationID,
                Qualification = u.Qualification,
                Address1 = u.Address1,
                Address2 = u.Address2,
                Area = u.Area,
                CityName = u.CityName,
                CountryName = u.CountryName,
                StateName = u.StateName,
                Pincode = u.Pincode,
                AlternateNumber = u.AlternateNumber,
                PANNo = u.PANNo,
                AdharNo = u.AdharNo,
                IsActive = u.IsActive
            }).FirstOrDefault();
        }
        public string AddEmployee(EmployeeMasterEntity employeeMasterEntity)
        {
            try
            {
                _dbContext.UserDetails.Add(new UserDetail()
                {
                    UserDetailID = employeeMasterEntity.UserDetailId,
                    FirstName = employeeMasterEntity.FirstName,
                    MiddleName = employeeMasterEntity.MiddleName,
                    LastName = employeeMasterEntity.LastName,
                    UserMasterID = employeeMasterEntity.UserMasterId,
                    Gender = employeeMasterEntity.Gender,
                    DateOfBirth = employeeMasterEntity.DateOfBirth,
                    Email = employeeMasterEntity.Email,
                    Mobile = employeeMasterEntity.Mobile,
                    EmployeeCode = employeeMasterEntity.EmployeeCode,
                    AreaOfExpertise = employeeMasterEntity.AreaOfExpertise,
                    ExperienceInYear = employeeMasterEntity.ExperienceInYear,
                    MasterDesignationID = employeeMasterEntity.MasterDesignationID,
                    Qualification = employeeMasterEntity.Qualification,
                    Address1 = employeeMasterEntity.Address1,
                    Address2 = employeeMasterEntity.Address2,
                    Area = employeeMasterEntity.Area,
                    CityName = employeeMasterEntity.CityName,
                    StateName = employeeMasterEntity.StateName,
                    CountryName = employeeMasterEntity.CountryName,
                    Pincode = employeeMasterEntity.Pincode,
                    AlternateNumber = employeeMasterEntity.AlternateNumber,
                    PANNo = employeeMasterEntity.PANNo,
                    AdharNo = employeeMasterEntity.AdharNo,
                    IsActive = employeeMasterEntity.IsActive,
                    //EnteredBy = (Int32)employeeMasterEntity.EnteredBy,
                    EnteredDate = (DateTime)employeeMasterEntity.EnteredDate,
                    ModifiedBy = employeeMasterEntity.ModifiedBy
                });

                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string Update(EmployeeMasterEntity userEntity)
        {
            try
            {
                var userMaster = _dbContext.UserDetails.Find(userEntity.UserDetailId);
                userMaster.FirstName = userEntity.FirstName;
                userMaster.MiddleName = userEntity.MiddleName;
                userMaster.LastName = userEntity.LastName;
                userMaster.UserMasterID = userEntity.UserID;
                userMaster.Gender = userEntity.Gender;
                userMaster.DateOfBirth = userEntity.DateOfBirth;
                userMaster.Email = userEntity.Email;
                userMaster.Mobile = userEntity.Mobile;
                userMaster.EmployeeCode = userEntity.EmployeeCode;
                userMaster.AreaOfExpertise = userEntity.AreaOfExpertise;
                userMaster.ExperienceInYear = userEntity.ExperienceInYear;
                userMaster.MasterDesignationID = userEntity.MasterDesignationID;
                userMaster.Qualification = userEntity.Qualification;
                userMaster.Address1 = userEntity.Address1;
                userMaster.Address2 = userEntity.Address2;
                userMaster.Area = userEntity.Area;
                userMaster.CityName = userEntity.CityName;
                userMaster.StateName = userEntity.StateName;
                userMaster.CountryName = userEntity.CountryName;
                userMaster.Pincode = userEntity.Pincode;
                userMaster.AlternateNumber = userEntity.AlternateNumber;
                userMaster.PANNo = userEntity.PANNo;
                userMaster.AdharNo = userEntity.AdharNo;
                userMaster.IsActive = userEntity.IsActive;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            { if (disposing) { _dbContext.Dispose(); } }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}