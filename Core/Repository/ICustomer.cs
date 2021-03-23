using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS_DEMO.Core.Customer;

namespace LIMS_DEMO.Core.Repository
{
   public interface ICustomer : IDisposable
    {
        List<CustomerEntity> GetCustomerList();
        List<CustomerTypeEntity> GetCustomerTypeList();
        List<CompanyTypeEntity> GetCompanyTypeList();
        List<CustomerGroupEntity> GetCustomerGroupList();
        string AddCustomer(CustomerEntity customerEntity);
        string UpdateCustomer(CustomerEntity customerEntity);
        CustomerEntity GetCustomerDetails(int CustomerMasterId);
    }
}
