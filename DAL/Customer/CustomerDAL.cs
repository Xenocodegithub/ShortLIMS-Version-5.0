using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.Customer;

namespace LIMS_DEMO.DAL.Customer
{
    public class CustomerDAL : ICustomer
    {
        readonly LIMSEntities _dbContext;

        public CustomerDAL()
        {
            _dbContext = new LIMSEntities();
        }

        public List<CustomerEntity> GetCustomerList()
        {
            try
            {

                return (from e in _dbContext.CustomerMasters

                        select new CustomerEntity()
                        {
                            RegistrationName = e.RegistrationName,
                            CustomerMasterId = e.CustomerMasterId,
                            ContactPersonName = e.ContactPersonName,
                            ContactMobileNo = e.ContactMobileNo,
                            IsActive = e.IsActive,
                            ContactEmail = e.ContactEmail,
                            Address1 = e.Address1,
                            CityName = e.CityName,
                            StateName = e.StateName,
                            CountryName = e.CountryName

                        }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<CustomerTypeEntity> GetCustomerTypeList()
        {
            return _dbContext.CustomerTypeMasters.Where(c => c.IsActive == true).Select(c => new CustomerTypeEntity()
            {
                CustomerTypeId = c.CustomerTypeId,
                CustomerTypeName = c.CustomerTypeName
            }).ToList();
        }

        public List<CompanyTypeEntity> GetCompanyTypeList()
        {
            return _dbContext.CompanyTypeMasters.Where(c => c.IsActive == true).Select(c => new CompanyTypeEntity()
            {
                CompanyTypeId = c.CompanyTypeId,
                CompanyType = c.CompanyType
            }).ToList();
        }

        public List<CustomerGroupEntity> GetCustomerGroupList()
        {
            return _dbContext.CustomerGroupMasters.Where(c => c.IsActive == true).Select(c => new CustomerGroupEntity()
            {
                CustomerGroupId = c.CustomerGroupId,
                CustomerGroupType = c.CustomerGroupType
            }).ToList();
        }

        public string AddCustomer(CustomerEntity customerEntity)
        {
            try
            {
                _dbContext.CustomerMasters.Add(new CustomerMaster()
                {
                    CustomerTypeId = customerEntity.CustomerTypeId,
                    CustomerGroupId = customerEntity.CustomerGroupId,
                    CompanyTypeId = customerEntity.CompanyTypeId,
                    RegistrationName = customerEntity.RegistrationName,
                    RegistrationDate = customerEntity.RegistrationDate,
                    ContactPersonName = customerEntity.ContactPersonName,
                    ContactMobileNo = customerEntity.ContactMobileNo,
                    ContactDesignation = customerEntity.ContactDesignation,
                    ContactEmail = customerEntity.ContactEmail,
                    Address1 = customerEntity.Address1,
                    Taluka = customerEntity.Taluka,
                    Village = customerEntity.Village,
                    Area = customerEntity.Area,
                    CityName = customerEntity.CityName,
                    StateName = customerEntity.StateName,
                    CountryName = customerEntity.CountryName,
                    Pincode = customerEntity.Pincode,
                    PhoneNo = customerEntity.PhoneNo,
                    MobileNo = customerEntity.MobileNo,
                    Email = customerEntity.Email,
                    PANNo = customerEntity.PANNo,
                    PANUpload = customerEntity.PANUpload,
                    AddressProofUpload = customerEntity.AddressProofUpload,
                    CINNumber = customerEntity.CINNumber,
                    FaxNo = customerEntity.FaxNo,
                    GSTNo = customerEntity.GSTNo,
                    EnteredBy = customerEntity.EnteredBy,
                    EnteredDate = customerEntity.EnteredDate,
                    IsActive = customerEntity.IsActive
                });
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public string UpdateCustomer(CustomerEntity customerEntity)
        {
            try
            {
                var customermaster = _dbContext.CustomerMasters.Find(customerEntity.CustomerMasterId);
                customermaster.RegistrationName = customerEntity.RegistrationName;
                customermaster.RegistrationDate = customerEntity.RegistrationDate;
                customermaster.ContactPersonName = customerEntity.ContactPersonName;
                customermaster.ContactMobileNo = customerEntity.ContactMobileNo;
                customermaster.ContactDesignation = customerEntity.ContactDesignation;
                customermaster.ContactEmail = customerEntity.ContactEmail;
                customermaster.Address1 = customerEntity.Address1;
                customermaster.Taluka = customerEntity.Taluka;
                customermaster.WardNo = customerEntity.WardNo;
                customermaster.Village = customerEntity.Village;
                customermaster.Area = customerEntity.Area;
                customermaster.CityName = customerEntity.CityName;
                customermaster.StateName = customerEntity.StateName;
                customermaster.CountryName = customerEntity.CountryName;
                customermaster.Pincode = customerEntity.Pincode;
                customermaster.PhoneNo = customerEntity.PhoneNo;
                customermaster.MobileNo = customerEntity.MobileNo;
                customermaster.Email = customerEntity.Email;
                customermaster.PANNo = customerEntity.PANNo;
                customermaster.PANUpload = customerEntity.PANUpload;
                customermaster.AddressProofUpload = customerEntity.AddressProofUpload;
                customermaster.CINNumber = customerEntity.CINNumber;
                customermaster.FaxNo = customerEntity.FaxNo;
                customermaster.GSTNo = customerEntity.GSTNo;
                customermaster.IsActive = customerEntity.IsActive;
                _dbContext.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }


        public CustomerEntity GetCustomerDetails(int CustomerMasterId)
        {
            return _dbContext.CustomerMasters.Where(u => u.CustomerMasterId == CustomerMasterId).Select(u => new CustomerEntity()
            {
                CustomerMasterId = u.CustomerMasterId,
                CustomerTypeId = u.CustomerTypeId,
                CustomerGroupId = u.CustomerGroupId,
                CompanyTypeId = u.CompanyTypeId,
                RegistrationName = u.RegistrationName,
                RegistrationDate = u.RegistrationDate,
                ContactPersonName = u.ContactPersonName,
                ContactMobileNo = u.ContactMobileNo,
                ContactDesignation = u.ContactDesignation,
                ContactEmail = u.ContactEmail,
                NameTitle = u.NameTitle,
                Address1 = u.Address1,
                Taluka = u.Taluka,
                WardNo = u.WardNo,
                Village = u.Village,
                Area = u.Area,
                CityName = u.CityName,
                StateName = u.StateName,
                CountryName = u.CountryName,
                Pincode = u.Pincode,
                PhoneNo = u.PhoneNo,
                MobileNo = u.MobileNo,
                Email = u.Email,
                PANNo = u.PANNo,
                PANUpload = u.PANUpload,
                AddressProofUpload = u.AddressProofUpload,
                CINNumber = u.CINNumber,
                FaxNo = u.FaxNo,
                IsActive = u.IsActive
            }).FirstOrDefault();
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