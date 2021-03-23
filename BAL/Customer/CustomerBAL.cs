using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core;
using LIMS_DEMO.Core.Customer;
using LIMS_DEMO.DAL;
using LIMS_DEMO.DAL.Customer;


namespace LIMS_DEMO.BAL.Customer
{
    public class CustomerBAL
    {
        public CustomerBAL()
        {
            CoreFactory.customer = new CustomerDAL();
        }

        public CustomerEntity GetCustomerDetails(int CustomerMasterId)
        {
            return CoreFactory.customer.GetCustomerDetails(CustomerMasterId);
        }
        public string AddCustomer(CustomerEntity customerEntity)
        {
            return CoreFactory.customer.AddCustomer(customerEntity);
        }
        public string UpdateCustomer(CustomerEntity customerEntity)
        {
            return CoreFactory.customer.UpdateCustomer(customerEntity);
        }
        public List<CustomerTypeEntity> GetCustomerTypeList()
        {
            return CoreFactory.customer.GetCustomerTypeList();
        }
        public List<CustomerEntity> GetCustomerList()
        {
            return CoreFactory.customer.GetCustomerList();
        }
        public List<CompanyTypeEntity> GetCompanyTypeList()
        {
            return CoreFactory.customer.GetCompanyTypeList();
        }
        public List<CustomerGroupEntity> GetCustomerGroupList()
        {
            return CoreFactory.customer.GetCustomerGroupList();
        }
    }
}