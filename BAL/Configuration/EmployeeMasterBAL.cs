using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core;
using LIMS_DEMO.DAL;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.DAL.Configuration;

namespace LIMS_DEMO.BAL.Configuration
{
    public class EmployeeMasterBAL
    {
        public EmployeeMasterBAL()
        {
            CoreFactory.employeeMaster = new EmployeeMasterDAL();
        }
        public string AddEmployee(EmployeeMasterEntity employeeMasterEntity)
        {
            return CoreFactory.employeeMaster.AddEmployee(employeeMasterEntity);
        }
        public EmployeeMasterEntity GetDetails(int UserDetailId)
        {
            return CoreFactory.employeeMaster.GetDetails(UserDetailId);
        }
        public List<EmployeeMasterEntity> GetEmployeeMasterList()
        {
            return CoreFactory.employeeMaster.GetEmployeeMasterList();
        }
        public string Update(EmployeeMasterEntity userEntity)
        {
            return CoreFactory.employeeMaster.Update(userEntity);
        }
    }
    
}